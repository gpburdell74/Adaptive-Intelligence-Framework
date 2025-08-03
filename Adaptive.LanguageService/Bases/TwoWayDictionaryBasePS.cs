using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.LanguageService.Dictionaries;

/// <summary>
/// Provides the base definition for a two-way look-up dictionary implementation.
/// </summary>
/// <typeparam name="PrimaryValue">
/// The primary data type being mapped to the secondary data type.
/// </typeparam>
/// <typeparam name="SecondaryValue">
/// The secondary data type being mapped to the primary data type.
/// </typeparam>
/// <seealso cref="DisposableObjectBase" />
public abstract class TwoWayDictionaryBase<PrimaryValue, SecondaryValue> : DisposableObjectBase
    where PrimaryValue : notnull
    where SecondaryValue : notnull
{
    #region Private Member Declarations
    /// <summary>
    /// The first list of items.
    /// </summary>
    private Dictionary<PrimaryValue, SecondaryValue>? _list;
    /// <summary>
    /// The list of items keyed by the second data type, allowing reverse look-up of the first data type.
    /// </summary>
    private Dictionary<SecondaryValue, PrimaryValue>? _reverseList;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="TwoWayDictionaryBase{T, U}"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    protected TwoWayDictionaryBase()
    {
        _list = new Dictionary<PrimaryValue, SecondaryValue>();
        _reverseList = new Dictionary<SecondaryValue, PrimaryValue>();
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
            _list?.Clear();
            _reverseList?.Clear();
        }

        _list = null;
        _reverseList = null;
        base.Dispose(disposing);

    }
    #endregion

    #region Protected Abstract Methods / Functions    
    /// <summary>
    /// Normalizes the specified value for use in a dictionary as a key value.
    /// </summary>
    /// <param name="value">
    /// A <typeparamref name="PrimaryValue"/> variable containing the value.
    /// </param>
    /// <returns>
    /// The normalized version of the value for use in a dictionary as a key.
    /// </returns>
    protected abstract PrimaryValue NormalizeKeyValue(PrimaryValue value);
    #endregion

    #region Protected Methods / Functions
    /// <summary>
    /// Performs the basic initialization of the dictionary.
    /// </summary>
    /// <remarks>
    /// It is expected that both lists will be the same length, and that the elements will match one to one for each index.
    /// </remarks>
    /// <param name="keyValues">
    /// A <see cref="List{T}"/> of <typeparamref name="PrimaryValue"/> values containing the keys for the dictionary.
    /// </param>
    /// <param name="dataValues">
    /// A <see cref="List{T}"/> of <typeparamref name="SecondaryValue"/> values containing the data for the dictionary.
    /// </param>
    /// <exception cref="Exception">
    /// Thrown if the length of the lists do not match.
    /// </exception>
    protected void BaseInitialize(List<PrimaryValue> keyValues, List<SecondaryValue> dataValues)
    {
        if (keyValues.Count != dataValues.Count)
            throw new ArgumentOutOfRangeException(nameof(keyValues), dataValues.Count, "Lists do not match.");

        int length = dataValues.Count;
        for (int count = 0; count < length; count++)
        {
            PrimaryValue key = keyValues[count];
            SecondaryValue data = dataValues[count];
            AddEntry(key,NormalizeKeyValue(key),  data);
        }
    }
    /// <summary>
    /// Gets the data from the initial dictionary.
    /// </summary>
    /// <param name="keyValue">
    /// A <typeparamref name="PrimaryValue"/> value acting as the key.
    /// </param>
    /// <returns>
    /// The matching <typeparamref name="SecondaryValue"/> value, if found.
    /// </returns>
    protected SecondaryValue? Get(PrimaryValue? keyValue)
    {
        SecondaryValue? result = default;
        if (keyValue != null)
        {
            // Return the type if known.
            if (_list!.ContainsKey(keyValue))
                result = _list[keyValue];
        }
        return result;
    }
    /// <summary>
    /// Gets the key value for the matching item.
    /// </summary>
    /// <param name="item">
    /// The item value to search for.
    /// </param>
    /// <returns>
    /// The matching <typeparamref name="PrimaryValue"/> value, if found.
    /// </returns>
    protected PrimaryValue? ReverseGet(SecondaryValue? item)
    {
        PrimaryValue? result = default;

        if (item != null)
        {
            if (_reverseList!.ContainsKey(item))
                result = _reverseList[item];
        }

        return result;
    }

    /// <summary>
    /// Converts a list of integers to a list of strongly-typed enumerated values.
    /// </summary>
    /// <typeparam name="DataType">
    /// The data type of the enumeration.
    /// </typeparam>
    /// <param name="idValues">
    /// The <see cref="List{T}"/> of integer values to convert.
    /// </param>
    /// <returns>
    /// A <see cref="List{T}"/> of <typeparamref name="DataType"/> containing the resulting enumerated values.
    /// </returns>
    protected virtual List<DataType> IdsToEnum<DataType>(List<int> idValues)
        where DataType : Enum
    {
        List<DataType> convertedList = new List<DataType>(idValues.Count);
        for (int count = 0; count < idValues.Count; count++)
        {
            convertedList.Add((DataType)(object)idValues[count]);
        }
        return convertedList;
    }
    /// <summary>
    /// Gets a value indicating whether the specified code is a data type in the language being implemented.
    /// </summary>
    /// <param name="code">
    /// The <typeparamref name="PrimaryValue"/> value to be evaluated.</param>
    /// <returns>
    /// <c>true</c> if the text can be matched to a known data type; otherwise, <c>false</c>.
    /// </returns>
    protected bool IsInDictionary(PrimaryValue? code)
    {
        if (code is null)
            return false;

        return _list!.ContainsKey(NormalizeKeyValue(code));
    }
    /// <summary>
    /// Normalizes the specified string value for use as a key.
    /// </summary>
    /// <param name="value">
    /// A string containing the value to be normalized for key usage.
    /// </param>
    /// <returns>
    /// The normalized version of the specified string.
    /// </returns>
    protected static string? NormalizeString(string? value)
    {
        if (value == null)
            return null;

        return value.ToLower().Trim();
    }
    /// <summary>
    /// Normalizes the specified string value for use as a key in upper case.
    /// </summary>
    /// <param name="value">
    /// A string containing the value to be normalized for key usage.
    /// </param>
    /// <returns>
    /// The normalized version of the specified string.
    /// </returns>
    protected static string? NormalizeUpperString(string? value)
    {
        if (value == null)
            return null;

        return value.ToUpper().Trim();
    }

    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Adds the entry to the dictionary.
    /// </summary>
    /// <param name="primary">
    /// A value of <typeparamref name="PrimaryValue}"/> containing the original key value.
    /// </param>
    /// <param name="secondary">
    /// A value of <typeparamref name="SecondaryValue"/> containing the value to be referenced.
    /// </param>
    public void AddEntry(PrimaryValue primary, SecondaryValue secondary)
    {
        if (_list == null)
            _list = new Dictionary<PrimaryValue, SecondaryValue>();

        if (_reverseList == null)
            _reverseList = new Dictionary<SecondaryValue, PrimaryValue>();

        if (!_list.ContainsKey(primary))
            _list.Add(primary, secondary);

        if (!_reverseList.ContainsKey(secondary))
            _reverseList.Add(secondary, primary);
    }

    /// <summary>
    /// Adds the entry to the dictionary.
    /// </summary>
    /// <param name="primary">
    /// A value of <typeparamref name="PrimaryValue"/> containing the original key value.
    /// </param>
    /// <param name="normalizedPrimary">
    /// The <paramref name="primary"/> normalized for using as a key value.
    /// </param>
    /// <param name="secondary">
    /// A value of <typeparamref name="SecondaryValue"/> containing the value to be referenced.
    /// </param>
    public void AddEntry(PrimaryValue primary, PrimaryValue normalizedPrimary, SecondaryValue secondary)
    {
        if (_list == null)
            _list = new Dictionary<PrimaryValue, SecondaryValue>();

        if (_reverseList == null)
            _reverseList = new Dictionary<SecondaryValue, PrimaryValue>();

        if (!_list.ContainsKey(normalizedPrimary))
            _list.Add(normalizedPrimary, secondary);

        if (!_reverseList.ContainsKey(secondary))
            _reverseList.Add(secondary, primary);
    }
    /// <summary>
    /// Renders the list of unique key values as strings.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="string"/> containing the list.
    /// </returns>
    public List<string> RenderUniqueKeys()
    {
        List<string> keyList = new List<string>(30);
        if (_list != null)
        {

            foreach (PrimaryValue key in _list.Keys)
            {
                if (key is string keyValue)
                    keyList.Add(keyValue);
                else if (key != null)
                    keyList.Add(key.ToString()!);
            }
        }
        return keyList;
    }
    #endregion
}
