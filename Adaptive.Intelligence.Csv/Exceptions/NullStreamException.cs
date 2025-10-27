namespace Adaptive.Intelligence.Csv.Exceptions;

/// <summary>
/// Represents an exception that occurs because a <b>null</b> <see cref="Stream"/> instance was specified.
/// </summary>
public sealed class NullStreamException : CsvException
{
    private const string ErrorMessage = "No stream was supplied to read from.";

    /// <summary>
    /// Initializes a new instance of the <see cref="NullStreamException"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public NullStreamException() : base(ErrorMessage)
    {
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="NullStreamException"/> class.
    /// </summary>
    /// <param name="message">
    /// A string containing the error description.
    /// </param>
    public NullStreamException(string message) : base(message)
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NullStreamException"/> class.
    /// </summary>
    /// <param name="innerException">
    /// The <see cref="Exception"/> to be stored as the inner exception.
    /// </param>
    public NullStreamException(Exception innerException) : base(ErrorMessage, innerException)
    {

    }
}
