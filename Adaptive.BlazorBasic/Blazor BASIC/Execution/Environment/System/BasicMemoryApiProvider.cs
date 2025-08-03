using Adaptive.Intelligence.LanguageService.Execution;
using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Logging;

namespace Adaptive.Intelligence.BlazorBasic.Execution;

/// <summary>
/// Provides the implementation of the memory API for the language interpreter.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
/// <seealso cref="IMemoryProvider" />
public sealed class BasicMemoryApiProvider : DisposableObjectBase, IMemoryProvider
{
    /// <summary>
    /// Allocates a section of memory of the specified size.
    /// </summary>
    /// <param name="size">An integer specifying the size of memory to allocate.</param>
    /// <returns>
    /// A byte array containing the allocated memory, or null if the allocation fails.
    /// </returns>
    public byte[]? Allocate(int size)
    {
        byte[]? returnArray = null;

        try
        {
            returnArray = new byte[size];
            Array.Clear(returnArray, 0, size);
        }
        catch(Exception ex )
        {
            ExceptionLog.LogException(ex);
        }

        return returnArray;
    }

    /// <summary>
    /// Invokes garbage collection on the underlying runtime system.
    /// </summary>
    public void Collect()
    {
        try
        {
            GC.Collect();
        }
        catch (Exception ex)
        {
            ExceptionLog.LogException(ex);
        }
    }

    /// <summary>
    /// Frees the specified memory.
    /// </summary>
    /// <param name="memory">
    /// The reference to the memory content to free.
    /// </param>
    public void Free(byte[]? memory)
    {
        try
        {
            if (memory != null)
            {
                Array.Clear(memory, 0, memory.Length);
                GC.ReRegisterForFinalize(memory);
            }
        }
        catch (Exception ex)
        {
            ExceptionLog.LogException(ex);
        }
    }
}
