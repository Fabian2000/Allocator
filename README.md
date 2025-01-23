# HeapAllocator

A lightweight C# library for efficient heap memory management using Span<T> and raw pointers.

## Overview

HeapAllocator provides two levels of memory management:
- High-level API using `Span<T>` for safe memory operations
- Low-level API using raw pointers for direct memory access

## Usage

High-level API with Span<T>:
```csharp
using HeapAllocator;

// Allocate memory
Span<int> numbers = Allocator.New<int>(100);

// Zero-initialized allocation
Span<byte> buffer = Allocator.New<byte>(1024, zeroedAllocation: true);

// Clear memory
Allocator.Clear(numbers);

// Copy memory
Allocator.Copy(source, destination);

// Free memory
Allocator.Free(numbers);
```

Low-level API with raw pointers:
```csharp
using HeapAllocator;

unsafe
{
    // Allocate memory
    int* ptr = AllocatorRaw.New<int>(100);

    // Zero-initialized allocation
    byte* buffer = AllocatorRaw.New<byte>(1024, zeroedAllocation: true);

    // Clear memory
    AllocatorRaw.Clear(ptr);

    // Copy memory
    AllocatorRaw.Copy(sourcePtr, destinationPtr);

    // Free memory
    AllocatorRaw.Free(ptr);
}
```

## Requirements
- .NET 6/7/8/9

---
*This README was generated using AI assistance*
