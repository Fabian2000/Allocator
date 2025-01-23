using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HeapAllocator
{
    /// <summary>
    /// Provides low-level memory allocation utilities for unmanaged types.
    /// </summary>
    public static class AllocatorRaw
    {
        /// <summary>
        /// Allocates memory for a single instance of type T.
        /// </summary>
        /// <typeparam name="T">The unmanaged type to allocate memory for.</typeparam>
        /// <returns>A pointer to the allocated memory.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SkipLocalsInit]
        public static unsafe T* New<T>() where T : unmanaged
        {
            return New<T>(1);
        }

        /// <summary>
        /// Allocates memory for a specified number of instances of type T.
        /// </summary>
        /// <typeparam name="T">The unmanaged type to allocate memory for.</typeparam>
        /// <param name="count">The number of instances to allocate memory for.</param>
        /// <returns>A pointer to the allocated memory.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when count is 0.</exception>
        /// <exception cref="ArgumentException">Thrown when the allocation size would exceed system limits.</exception>
        /// <exception cref="OutOfMemoryException">Thrown when memory allocation fails.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SkipLocalsInit]
        public static unsafe T* New<T>(nuint count) where T : unmanaged
        {
            if (count is 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than zero");
            }

            nuint size;
            try
            {
                checked
                {
                    size = count * (nuint)sizeof(T);
                }
            }
            catch (OverflowException)
            {
                throw new ArgumentException("Allocation size would exceed system limits", nameof(count));
            }

            T* ptr = (T*)NativeMemory.Alloc(count, (nuint)sizeof(T));
            if (ptr is null)
            {
                throw new OutOfMemoryException("Failed to allocate native memory");
            }
            return ptr;
        }

        /// <summary>
        /// Allocates memory for a single instance of type T with optional zero initialization.
        /// </summary>
        /// <typeparam name="T">The unmanaged type to allocate memory for.</typeparam>
        /// <param name="zeroedAllocation">If true, initializes the allocated memory to zero.</param>
        /// <returns>A pointer to the allocated memory.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SkipLocalsInit]
        public static unsafe T* New<T>(bool zeroedAllocation) where T : unmanaged
        {
            return New<T>(1, zeroedAllocation);
        }

        /// <summary>
        /// Allocates memory for a specified number of instances of type T with optional zero initialization.
        /// </summary>
        /// <typeparam name="T">The unmanaged type to allocate memory for.</typeparam>
        /// <param name="count">The number of instances to allocate memory for.</param>
        /// <param name="zeroedAllocation">If true, initializes the allocated memory to zero.</param>
        /// <returns>A pointer to the allocated memory.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when count is 0.</exception>
        /// <exception cref="ArgumentException">Thrown when the allocation size would exceed system limits.</exception>
        /// <exception cref="OutOfMemoryException">Thrown when memory allocation fails.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SkipLocalsInit]
        public static unsafe T* New<T>(nuint count, bool zeroedAllocation) where T : unmanaged
        {
            if (count is 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than zero");
            }

            if (!zeroedAllocation)
            {
                return New<T>(count);
            }

            nuint size;
            try
            {
                checked
                {
                    size = count * (nuint)sizeof(T);
                }
            }
            catch (OverflowException)
            {
                throw new ArgumentException("Allocation size would exceed system limits", nameof(count));
            }

            T* ptr = (T*)NativeMemory.AllocZeroed(count, (nuint)sizeof(T));
            if (ptr is null)
            {
                throw new OutOfMemoryException("Failed to allocate zeroed native memory");
            }
            return ptr;
        }

        /// <summary>
        /// Clears the memory at the specified pointer location.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the pointed memory.</typeparam>
        /// <param name="pointer">The pointer to the memory to clear.</param>
        /// <exception cref="ArgumentNullException">Thrown when pointer is null.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SkipLocalsInit]
        public static unsafe void Clear<T>(T* pointer) where T : unmanaged
        {
            if (pointer is null)
            {
                throw new ArgumentNullException(nameof(pointer), "Pointer cannot be null");
            }
            NativeMemory.Clear(pointer, (nuint)sizeof(T));
        }

        /// <summary>
        /// Copies memory from a source pointer to a destination pointer.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the pointed memory.</typeparam>
        /// <param name="sourcePointer">The source pointer to copy from.</param>
        /// <param name="destinationPointer">The destination pointer to copy to.</param>
        /// <exception cref="ArgumentNullException">Thrown when either pointer is null.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SkipLocalsInit]
        public static unsafe void Copy<T>(T* sourcePointer, T* destinationPointer) where T : unmanaged
        {
            if (sourcePointer is null)
            {
                throw new ArgumentNullException(nameof(sourcePointer), "Source pointer cannot be null");
            }
            if (destinationPointer is null)
            {
                throw new ArgumentNullException(nameof(destinationPointer), "Destination pointer cannot be null");
            }
            NativeMemory.Copy(sourcePointer, destinationPointer, (nuint)sizeof(T));
        }

        /// <summary>
        /// Frees the memory at the specified pointer location.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the pointed memory.</typeparam>
        /// <param name="pointer">The pointer to the memory to free.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SkipLocalsInit]
        public static unsafe void Free<T>(T* pointer) where T : unmanaged
        {
            if (pointer is null)
            {
                return;
            }
            NativeMemory.Free(pointer);
        }
    }
}