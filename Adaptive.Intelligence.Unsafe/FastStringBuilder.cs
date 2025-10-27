using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Logging;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Adaptive.Intelligence.Unsafe;

/// <summary>
/// Provides a simple string builder class for higher performance.
/// </summary>
public class FastStringBuilder : DisposableObjectBase
{
    #region Private Member Declarations
    /// <summary>
    /// The character buffer.
    /// </summary>
    private unsafe char* _characterBuffer = null;
    /// <summary>
    /// The maximum amount of memory to allocate for this instance.
    /// </summary>
    private uint _maximumBufferSize = 0;
    /// <summary>
    /// The current index.
    /// </summary>
    private int _index = 0;
    /// <summary>
    /// The actualt length of the string being built.
    /// </summary>
    private int _length = 0;

    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Creates a new instance of the <see cref="FastStringBuilder"/> class.
    /// </summary>
    /// <param name="maximumSize">
    /// An integer specifying the maximum amount of memory to allocate for this instance.
    /// </param>
    public FastStringBuilder(int maximumSize)
    {
        _maximumBufferSize = (uint)maximumSize * sizeof(char);
        unsafe
        {
            _characterBuffer = (char*)NativeMemory.AllocZeroed(_maximumBufferSize);
        }
    }
    /// <summary>
    /// De-allocates the native memory and other resources.
    /// </summary>
    /// <param name="disposing">
    /// A value indicating whether to dispose of native resources.
    /// </param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            SafeFree();
        }

        _maximumBufferSize = 0;
        _length = 0;
        _index = 0;

        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets the size of the string being built.
    /// </summary>
    /// <value>
    /// An integer indicating the current string size.
    /// </value>
    public int Length => _length;
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Appends the specfied character value to the current buffer.
    /// </summary>
    /// <param name="value">
    /// A character containing the value to be appended.
    /// </param>
    public void Append(char value)
    {
        // Don't exceed the buffer size...
        if (_length + (sizeof(char)) < _maximumBufferSize)
        {
            try
            {
                unsafe
                {
                    _characterBuffer[_index] = value;
                    _index++;
                    _length++;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }
        }
    }
    /// <summary>
    /// Appends the specfied string value to the current buffer.
    /// </summary>
    /// <param name="value">
    /// A string containing the value to be appended.
    /// </param>
    public void Append(string value)
    {
        // Don't exceed the buffer size...
        if (_length + (value.Length * sizeof(char)) < _maximumBufferSize)
        {
            try
            {
                unsafe
                {
                    // Re-cast the .NET string to a character array.
                    fixed (char* ptr = value)
                    {
                        nuint valueLen = (nuint)value.Length * 2;
                        NativeMemory.Copy(ptr, _characterBuffer + _index, valueLen);
                        _index += value.Length;
                        _length += value.Length;
                    }
                }
            }
            catch(Exception ex)
            {
                ExceptionLog.LogException(ex);
            }
        }
    }

    /// <summary>
    /// Appends the system new line indicator(s) to the current string being built.
    /// </summary>
    public void AppendLine()
    {
        Append(System.Environment.NewLine);
    }

    /// <summary>
    /// Appends the value and the system new line indicator(s) to the current string being built.
    /// </summary>
    /// <param name="value">
    /// A string containing the value to be written.
    /// </param>
    public void AppendLine(string value)
    {
        Append(value + Environment.NewLine);
    }

    /// <summary>
    /// Clears the currently allocated buffer, but does not de-allocate it.
    /// </summary>
    public void Clear()
    {
            // Wipe the buffer to contain all zeroes.
        unsafe
        {
            try
            {
                NativeMemory.Clear(_characterBuffer, (uint)_length);
                _index = 0;
                _length = 0;
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }
        }
    }

    /// <summary>
    /// Returns the content of the buffer as a single (managed) string.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> containing the buffer contents.
    /// </returns>
    public override string ToString()
    {
        string concatenated = string.Empty;

        try
        {
            unsafe
            {
                concatenated = new string(_characterBuffer, 0, _length);
            }
        }
        catch(Exception ex)
        {
            ExceptionLog.LogException(ex);
            concatenated = string.Empty;
        }

        return concatenated;
    }    
    #endregion

    #region Private Methods / Functions

    /// <summary>
    /// Frees the native memory that was allocated.
    /// </summary>
    private void SafeFree()
    {
        unsafe
        {
            try
            {
                if (_characterBuffer != null)
                {
                    NativeMemory.Clear(_characterBuffer, _maximumBufferSize);
                    NativeMemory.Free(_characterBuffer);
                    _characterBuffer = null;
                }
            } catch(Exception ex)
            {
                ExceptionLog.LogException(ex);
                // Don't crash if the native operation fails.
            }
        }
    }
    #endregion
}
