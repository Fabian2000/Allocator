using System.Runtime.CompilerServices;

namespace HeapAllocator
{
    /// <summary>
    /// Provides memory allocation utilities using Span&lt;T&gt; for safer memory management.
    /// </summary>
    public static class Allocator
    {
        /// <summary>
        /// Allocates memory for a single instance of type T.
        /// </summary>
        /// <typeparam name="T">The unmanaged type to allocate memory for.</typeparam>
        /// <returns>A Span containing the allocated memory.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SkipLocalsInit]
        public static unsafe Span<T> New<T>() where T : unmanaged
        {
            return New<T>(1);
        }

        /// <summary>
        /// Allocates memory for a specified number of instances of type T.
        /// </summary>
        /// <typeparam name="T">The unmanaged type to allocate memory for.</typeparam>
        /// <param name="count">The number of instances to allocate memory for.</param>
        /// <returns>A Span containing the allocated memory.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SkipLocalsInit]
        public static unsafe Span<T> New<T>(nuint count) where T : unmanaged
        {
            T* ptr = AllocatorRaw.New<T>(count);
            return new Span<T>(ptr, (int)count);
        }

        /// <summary>
        /// Allocates memory for a single instance of type T with optional zero initialization.
        /// </summary>
        /// <typeparam name="T">The unmanaged type to allocate memory for.</typeparam>
        /// <param name="zeroedAllocation">If true, initializes the allocated memory to zero.</param>
        /// <returns>A Span containing the allocated memory.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SkipLocalsInit]
        public static unsafe Span<T> New<T>(bool zeroedAllocation) where T : unmanaged
        {
            return New<T>(1, zeroedAllocation);
        }

        /// <summary>
        /// Allocates memory for a specified number of instances of type T with optional zero initialization.
        /// </summary>
        /// <typeparam name="T">The unmanaged type to allocate memory for.</typeparam>
        /// <param name="count">The number of instances to allocate memory for.</param>
        /// <param name="zeroedAllocation">If true, initializes the allocated memory to zero.</param>
        /// <returns>A Span containing the allocated memory.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SkipLocalsInit]
        public static unsafe Span<T> New<T>(nuint count, bool zeroedAllocation) where T : unmanaged
        {
            T* ptr = AllocatorRaw.New<T>(count, zeroedAllocation);
            return new Span<T>(ptr, (int)count);
        }

        /// <summary>
        /// Clears the memory in the specified span.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the span elements.</typeparam>
        /// <param name="span">The span containing the memory to clear.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SkipLocalsInit]
        public static unsafe void Clear<T>(Span<T> span) where T : unmanaged
        {
            AllocatorRaw.Clear((T*)Unsafe.AsPointer(ref span[0]));
        }

        /// <summary>
        /// Copies memory from a source span to a destination span.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the span elements.</typeparam>
        /// <param name="source">The source span to copy from.</param>
        /// <param name="destination">The destination span to copy to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SkipLocalsInit]
        public static unsafe void Copy<T>(Span<T> source, Span<T> destination) where T : unmanaged
        {
            AllocatorRaw.Copy(
                (T*)Unsafe.AsPointer(ref source[0]),
                (T*)Unsafe.AsPointer(ref destination[0]));
        }

        /// <summary>
        /// Frees the memory held by the specified span.
        /// </summary>
        /// <typeparam name="T">The unmanaged type of the span elements.</typeparam>
        /// <param name="span">The span containing the memory to free.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SkipLocalsInit]
        public static unsafe void Free<T>(Span<T> span) where T : unmanaged
        {
            AllocatorRaw.Free((T*)Unsafe.AsPointer(ref span[0]));
        }
    }
}