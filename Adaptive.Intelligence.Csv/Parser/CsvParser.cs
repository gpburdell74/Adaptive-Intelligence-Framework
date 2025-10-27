using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Unsafe;

namespace Adaptive.Intelligence.Csv.Parser;

/// <summary>
/// Provides the mechanism for parsing the content of a CSV file.
/// </summary>
/// <seealso cref="DisposableObjectBase" />
public class CsvParser : DisposableObjectBase, ICsvParser
{
    #region Private Member Declarations
    /// <summary>
    /// The default delimiter character.
    /// </summary>
    private const char DefaultDelimiter = ',';
    /// <summary>
    /// The double quote character.
    /// </summary>
    private const char DoubleQuote = '\"';

    /// <summary>
    /// The default value for a maximum column value size.
    /// </summary>
    private const int DefaultMaxColumnSize = 1048576; // 1 MB

    /// <summary>
    /// The delimiter used to seperate items.  The default for CSV is the comma.
    /// </summary>
    private char _delimiter = DefaultDelimiter;

    /// <summary>
    /// The string builder to be used for string concatenation operations.
    /// </summary>
    private FastStringBuilder? _builder;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="CsvParser"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public CsvParser() : this(DefaultDelimiter, DefaultMaxColumnSize)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CsvParser"/> class.
    /// </summary>
    /// <param name="delimiter">
    /// A <see cref="char"/> specifying the delimiter used to seperate column values.
    /// </param>
    public CsvParser(char delimiter)
        : this(delimiter, DefaultMaxColumnSize)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CsvParser"/> class.
    /// </summary>
    /// <param name="delimiter">
    /// A <see cref="char"/> specifying the delimiter used to seperate column values.
    /// </param>
    /// <param name="maxColumnSize">
    /// An integer specifying the maximum number of characters in a column value.  This can be tweaked to 
    /// optimize the operation(s) by limiting the memory allocated.  Ensure enough memory is specified - 
    /// if the value is not large enough some data may be lost.
    /// </param>
    public CsvParser(char delimiter, int maxColumnSize)
    {
        _delimiter = delimiter;
        _builder = new FastStringBuilder(maxColumnSize);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            _builder?.Dispose();
        }
        _builder = null;
        _delimiter = '\0';

        base.Dispose(disposing);
    }
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Parses the line of text into the individual data elements.
    /// </summary>
    /// <param name="originalContent">
    /// A string containing the content to be parsed.
    /// </param>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> values parsed from the original.
    /// </returns>
    public List<string>? ParseLine(string? originalContent)
    {
        List<string>? dataElements = null;

        if (_builder != null && originalContent != null)
        {
            _builder.Clear();

            dataElements = new List<string>();
            bool insideQuotes = false;

            // Iterate over each character in the line.
            int lineLength = originalContent.Length;
            for (int charIndex = 0; charIndex < lineLength; charIndex++)
            {
                char currentChar = originalContent[charIndex];
                if (currentChar == DoubleQuote)
                {
                    // Toggle the insideQuotes flag when encountering a quote, unless a double-quote is encountered.
                    if (charIndex+1 < lineLength && originalContent[charIndex + 1] == DoubleQuote)
                    {
                        // It's an escaped quote, so add a single quote to the current element and skip the next character.
                        _builder.Append(DoubleQuote);
                        charIndex++; // Skip the next quote
                    }
                    else
                        insideQuotes = !insideQuotes;
                }
                else if (currentChar == _delimiter && !insideQuotes)
                {
                    // If we encounter a delimiter and we're not inside quotes, it's a delimiter.
                    string? parsedContent = _builder.ToString();
                    if (parsedContent != null)
                        dataElements.Add(parsedContent);

                    // Reset the builder for the next column.
                    _builder.Clear();
                }
                else
                {
                    // Otherwise, just add the character to the current element
                    _builder.Append(currentChar);
                }
            }

            // Add the last element if there's any content left
            if (_builder.Length > 0)
            {
                string? parsedContent = _builder.ToString();
                if (parsedContent != null)
                    dataElements.Add(parsedContent);
            }
        }

        return dataElements;
    }
    #endregion
}