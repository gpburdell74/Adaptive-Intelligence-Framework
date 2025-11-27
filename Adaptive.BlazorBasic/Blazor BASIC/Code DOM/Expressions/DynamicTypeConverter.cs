using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence;

/// <summary>
/// Provides static methods and functions for converting various data types when the 
/// source data type is unknown.
/// </summary>
public static class DynamicTypeConverter
{
    #region Private Constants

    private const int SIZE_BYTE = 1;
    private const int SIZE_CHAR = 2;
    private const int SIZE_INT = 4;
    private const int SIZE_LONG = 8;

    #endregion

    #region Public Static Methods / Functions
    /// <summary>
    /// Converts the object to a boolean value.
    /// </summary>
    /// <param name="data">
    /// The data content to be converted.
    /// </param>
    /// <returns>
    /// The resulting boolean value.
    /// </returns>
    public static bool ToBoolean(object? data)
    {
        bool value = false;

        if (data != null)
        {
            byte[]? sourceArray = MallocAndWrite(data, SIZE_BYTE);
            if (sourceArray != null)
                value = ReadBoolean(sourceArray);
            ByteArrayUtil.Clear(sourceArray);
        }
        return value;
    }

    /// <summary>
    /// Converts the object to a byte value.
    /// </summary>
    /// <param name="data">
    /// The data content to be converted.
    /// </param>
    /// <returns>
    /// The resulting byte value.
    /// </returns>
    public static byte ToByte(object? data)
    {
        byte value = 0;

        if (data != null)
        {
            byte[]? sourceArray = MallocAndWrite(data, SIZE_BYTE);
            if (sourceArray != null)
                value = ReadByte(sourceArray);
            ByteArrayUtil.Clear(sourceArray);
        }
        return value;
    }

    /// <summary>
    /// Converts the object to a character value.
    /// </summary>
    /// <param name="data">
    /// The data content to be converted.
    /// </param>
    /// <returns>
    /// The resulting <see cref="char"/> value.
    public static char ToChar(object? data)
    {
        char value = '\0';

        if (data != null)
        {
            byte[]? sourceArray = MallocAndWrite(data, SIZE_CHAR);
            if (sourceArray != null)
                value = ReadChar(sourceArray);
            ByteArrayUtil.Clear(sourceArray);
        }
        return value;
    }

    /// <summary>
    /// Converts the object to a short integer value.
    /// </summary>
    /// <param name="data">
    /// The data content to be converted.
    /// </param>
    /// <returns>
    /// The resulting <see cref="short"/> value.
    public static short ToInt16(object? data)
    {
        short value = 0;

        if (data != null)
        {
            byte[]? sourceArray = MallocAndWrite(data, SIZE_CHAR);
            if (sourceArray != null)
                value = ReadInt16(sourceArray);
            ByteArrayUtil.Clear(sourceArray);
        }
        return value;
    }

    /// <summary>
    /// Converts the object to an integer value.
    /// </summary>
    /// <param name="data">
    /// The data content to be converted.
    /// </param>
    /// <returns>
    /// The resulting <see cref="int"/> value.
    public static int ToInt32(object? data)
    {
        int value = 0;

        if (data != null)
        {
            byte[]? sourceArray = MallocAndWrite(data, SIZE_INT);
            if (sourceArray != null)
                value = ReadInt32(sourceArray);
            ByteArrayUtil.Clear(sourceArray);
        }
        return value;
    }

    /// <summary>
    /// Converts the object to a long integer value.
    /// </summary>
    /// <param name="data">
    /// The data content to be converted.
    /// </param>
    /// <returns>
    /// The resulting <see cref="long"/> value.
    public static long ToInt64(object? data)
    {
        long value = 0;

        if (data != null)
        {
            byte[]? sourceArray = MallocAndWrite(data, SIZE_LONG);
            if (sourceArray != null)
                value = ReadInt64(sourceArray);
            ByteArrayUtil.Clear(sourceArray);
        }
        return value;
    }

    /// <summary>
    /// Converts the object to a single-precision floating point value.
    /// </summary>
    /// <param name="data">
    /// The data content to be converted.
    /// </param>
    /// <returns>
    /// The resulting <see cref="float"/> value.
    public static float ToSingle(object? data)
    {
        float value = 0;

        if (data != null)
        {
            byte[]? sourceArray = MallocAndWrite(data, SIZE_INT);
            if (sourceArray != null)
                value = ReadSingle(sourceArray);
            ByteArrayUtil.Clear(sourceArray);
        }
        return value;
    }

    /// <summary>
    /// Converts the object to a double-precision floating point value.
    /// </summary>
    /// <param name="data">
    /// The data content to be converted.
    /// </param>
    /// <returns>
    /// The resulting <see cref="double"/> value.
    public static double ToDouble(object? data)
    {
        double value = 0;

        if (data != null)
        {
            byte[]? sourceArray = MallocAndWrite(data, SIZE_LONG);
            if (sourceArray != null)
                value = ReadDouble(sourceArray);
            ByteArrayUtil.Clear(sourceArray);
        }
        return value;
    }

    /// <summary>
    /// Converts the object to a date value.
    /// </summary>
    /// <param name="data">
    /// The data content to be converted.
    /// </param>
    /// <returns>
    /// The resulting <see cref="DateTime"/> value containing just the date value.
    public static DateTime ToDate(object? data)
    {
        return ToDateTime(data).Date;
    }

    /// <summary>
    /// Converts the object to a date/time value.
    /// </summary>
    /// <param name="data">
    /// The data content to be converted.
    /// </param>
    /// <returns>
    /// The resulting <see cref="DateTime"/> value.
    public static DateTime ToDateTime(object? data)
    {
        DateTime value = DateTime.MinValue;

        if (data != null)
        {
            byte[]? sourceArray = MallocAndWrite(data, SIZE_LONG);
            if (sourceArray != null)
                value = ReadDateTime(sourceArray);
            ByteArrayUtil.Clear(sourceArray);
        }
        return value;
    }

    /// <summary>
    /// Converts the object to a string value.
    /// </summary>
    /// <param name="data">
    /// The data content to be converted.
    /// </param>
    /// <returns>
    /// The resulting <see cref="string"/> value.
    public static string ToString(object? data)
    {
        string value = string.Empty;

        if (data != null)
        {
            value = data.ToString();
        }
        return value;
    }
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Converts the provided object instance into a byte array.
    /// </summary>
    /// <param name="data">
    /// The data to be written as a byte array.
    /// </param>
    /// <returns>
    /// A byte array containing the data in <paramref name="data"/>.
    /// </returns>
    private static byte[] DataToBytes(object data)
    {
        byte[] content;

        switch (data)
        {
            case bool boolValue:
                content = BitConverter.GetBytes(boolValue);
                break;

            case byte byteValue:
                content = new byte[1] { byteValue };
                break;

            case char charValue:
                content = BitConverter.GetBytes(charValue);
                break;

            case short shortValue:
                content = BitConverter.GetBytes(shortValue);
                break;

            case int intValue:
                content = BitConverter.GetBytes(intValue);
                break;

            case long longValue:
                content = BitConverter.GetBytes(longValue);
                break;

            case float floatValue:
                content = BitConverter.GetBytes(floatValue);
                break;

            case double doubleValue:
                content = BitConverter.GetBytes(doubleValue);
                break;

            case string stringValue:
                content = System.Text.Encoding.ASCII.GetBytes(stringValue);
                break;

            case DateTime dateTimeValue:
                content = BitConverter.GetBytes(dateTimeValue.ToFileTime());
                break;

            case Time timeValue:
                content = BitConverter.GetBytes(timeValue.TotalSeconds);
                break;

            default:
                content = new byte[0];
                break;

        }

        return content;
    }

    /// <summary>
    /// Allocates a byte array for the specified size, and writes the original data content
    /// to the array.
    /// </summary>
    /// <param name="data">
    /// The original data variable.
    /// </param>
    /// <param name="typeSize">
    /// An integer specifying the size of the data type to be converted to, in bytes.
    /// </param>
    private static byte[] MallocAndWrite(object data, int typeSize)
    {
        // Convert the original value to a byte array.
        byte[] original = DataToBytes(data);

        // Ensure the sizing data is correct.
        if (original.Length < typeSize)
            typeSize = original.Length;

        // Allocate the new byte array and set all bytes to zero.
        byte[] memory = new byte[typeSize];
        Array.Clear(memory);

        // Copy as much of the original data as will fit.
        Array.Copy(original, 0, memory, 0, typeSize);

        return memory;
    }

    /// <summary>
    /// Reads the boolean value from the supplied byte array.
    /// </summary>
    /// <param name="sourceArray">
    /// The source byte array to read from.
    /// </param>
    /// <returns>
    /// <b>true</b> or <b>false</b>.
    /// </returns>
    private static bool ReadBoolean(byte[] sourceArray)
    {
        return (sourceArray[0] == (byte)1);
    }

    /// <summary>
    /// Reads the byte value from the supplied byte array.
    /// </summary>
    /// <param name="sourceArray">
    /// The source byte array to read from.
    /// </param>
    /// <returns>
    /// The first <see cref="byte"/> value in the array.
    /// </returns>
    private static byte ReadByte(byte[] sourceArray)
    {
        return sourceArray[0];
    }

    /// <summary>
    /// Reads the character value from the supplied byte array.
    /// </summary>
    /// <param name="sourceArray">
    /// The source byte array to read from.
    /// </param>
    /// <returns>
    /// The <see cref="char"/> value read from the first 2 bytes of the supplied array.
    /// </returns>
    private static char ReadChar(byte[] sourceArray)
    {
        return BitConverter.ToChar(sourceArray, 0);
    }

    /// <summary>
    /// Reads the short integer value from the supplied byte array.
    /// </summary>
    /// <param name="sourceArray">
    /// The source byte array to read from.
    /// </param>
    /// <returns>
    /// The <see cref="short"/> value read from the first 2 bytes of the supplied array.
    /// </returns>
    private static short ReadInt16(byte[] sourceArray)
    {
        return BitConverter.ToInt16(sourceArray, 0);
    }

    /// <summary>
    /// Reads the integer value from the supplied byte array.
    /// </summary>
    /// <param name="sourceArray">
    /// The source byte array to read from.
    /// </param>
    /// <returns>
    /// The <see cref="short"/> value read from the first 4 bytes of the supplied array.
    /// </returns>
    private static int ReadInt32(byte[] sourceArray)
    {
        return BitConverter.ToInt32(sourceArray, 0);
    }

    /// <summary>
    /// Reads the long integer value from the supplied byte array.
    /// </summary>
    /// <param name="sourceArray">
    /// The source byte array to read from.
    /// </param>
    /// <returns>
    /// The <see cref="long"/> value read from the first 8 bytes of the supplied array.
    /// </returns>
    private static long ReadInt64(byte[] sourceArray)
    {
        return BitConverter.ToInt16(sourceArray, 0);
    }

    /// <summary>
    /// Reads the single-precision floating point value from the supplied byte array.
    /// </summary>
    /// <param name="sourceArray">
    /// The source byte array to read from.
    /// </param>
    /// <returns>
    /// The <see cref="float"/> value read from the first 4 bytes of the supplied array.
    /// </returns>
    private static float ReadSingle(byte[] sourceArray)
    {
        return BitConverter.ToSingle(sourceArray, 0);
    }

    /// <summary>
    /// Reads the double-precision floating point value from the supplied byte array.
    /// </summary>
    /// <param name="sourceArray">
    /// The source byte array to read from.
    /// </param>
    /// <returns>
    /// The <see cref="double"/> value read from the first 4 bytes of the supplied array.
    /// </returns>
    private static double ReadDouble(byte[] sourceArray)
    {
        return BitConverter.ToDouble(sourceArray, 0);
    }

    /// <summary>
    /// Reads the Date value from the supplied byte array.
    /// </summary>
    /// <param name="sourceArray">
    /// The source byte array to read from.
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/> value read from the first 8 bytes of the supplied array.
    /// </returns>
    private static DateTime ReadDate(byte[] sourceArray)
    {
        return ReadDateTime(sourceArray).Date;
    }

    /// <summary>
    /// Reads the Date/Time value from the supplied byte array.
    /// </summary>
    /// <param name="sourceArray">
    /// The source byte array to read from.
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/> value read from the first 8 bytes of the supplied array.
    /// </returns>
    private static DateTime ReadDateTime(byte[] sourceArray)
    {
        return DateTime.FromFileTime(BitConverter.ToInt64(sourceArray));
    }

    /// <summary>
    /// Reads the Time value from the supplied byte array.
    /// </summary>
    /// <param name="sourceArray">
    /// The source byte array to read from.
    /// </param>
    /// <returns>
    /// The <see cref="Time"/> value read from the first 4 bytes of the supplied array.
    /// </returns>
    private static Time ReadTime(byte[] sourceArray)
    {
        return new Time(BitConverter.ToInt32(sourceArray));
    }
    #endregion

}
