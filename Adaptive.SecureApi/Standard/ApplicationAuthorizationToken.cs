using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.IO;
using Adaptive.Intelligence.Shared.Logging;
using Adaptive.SecureApi.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace Adaptive.SecureApi;

/// <summary>
/// Represents and manages an application authorization token.
/// </summary>
/// <remarks>
/// This token is used to validate and ensure a client application has invoked the APIs correctly and
/// has performed the initial key exchange process successfully.
/// </remarks>
/// <seealso cref="DisposableObjectBase" />
public class ApplicationAuthorizationToken : DisposableObjectBase, IApplicationAuthorizationToken
{
    #region Private Member Declarations
    /// <summary>
    /// The size of the randomized data for the token.
    /// </summary>
    private const int TOKEN_SIZE = 128;

    /// <summary>
    /// The token creation date.
    /// </summary>
    private DateTime? _tokenCreationDate;
    /// <summary>
    /// The machine name on which the token was created.
    /// </summary>
    private string? _machineName;
    /// <summary>
    /// The original randomized data content.
    /// </summary>
    private byte[]? _originalData;
    /// <summary>
    /// The hash of the original randomized data content.
    /// </summary>
    private byte[]? _hashData;
    #endregion

    #region Constructor / Dispose Methods        
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationAuthorizationToken"/> class.
    /// </summary>
    /// <remarks>
    /// This is the default constructor.
    /// </remarks>
    public ApplicationAuthorizationToken()
    {
        CreateData();
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
            ByteArrayUtil.Clear(_originalData);
            ByteArrayUtil.Clear(_hashData);
        }

        _tokenCreationDate = null;
        _machineName = null;
        _originalData = null;
        _hashData = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the reference to the hash data.
    /// </summary>
    /// <value>
    /// A byte array containing the SHA-512 hash for the instance, or <b>null</b>.
    /// </value>
    public byte[]? HashData
    {
        get => _hashData;
        set => _hashData = value;
    }
    /// <summary>
    /// Gets or sets the name of the machine on which the token is created.
    /// </summary>
    /// <value>
    /// A string containing the name of the machine, or <b>null</b>.
    /// </value>
    public string? MachineName
    {
        get => _machineName;
        set => _machineName = value;
    }
    /// <summary>
    /// Gets or sets the reference to the original randomized data.
    /// </summary>
    /// <value>
    /// A byte array of <see cref="TOKEN_SIZE"/> containing the random data.
    /// </value>
    public byte[]? OriginalData
    {
        get => _originalData;
        set => _originalData = value;
    }

    /// <summary>
    /// Gets or sets the ID of the session the token is related to.
    /// </summary>
    /// <value>
    /// A <see cref="Guid" /> containing the session ID, or <b>null</b>.
    /// </value>
    public Guid? SessionId { get; set; }

    /// <summary>
    /// Gets or sets the token creation date.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime"/> containing the token creation date, or <b>null</b>.
    /// </value>
    public DateTime? TokenCreationDate
    {
        get => _tokenCreationDate;
        set => _tokenCreationDate = value;
    }
    /// <summary>
    /// Gets the token data.
    /// </summary>
    /// <value>
    /// A byte array containing the token data, or <b>null</b>.
    /// </value>
    public byte[]? TokenData => CreateTokenData();
    #endregion

    #region Public Methods / Functions
    /// <summary>
    /// Creates the data content for the new token.
    /// </summary>
    public void CreateData()
    {
        _tokenCreationDate = DateTime.Now;
        _machineName = Environment.MachineName;

        _originalData = ByteArrayUtil.CreateRandomArray(TOKEN_SIZE);
        _hashData = CreateHashData();
    }
    /// <summary>
    /// Creates the byte array to represent the token instance.
    /// </summary>
    /// <returns>
    /// A byte array representing this token.
    /// </returns>
    public byte[]? CreateTokenData()
    {
        byte[]? returnData = null;

        MemoryStream stream = new MemoryStream(2048);
        try
        {
            byte[]? content = GetContentArray();
            if (content != null && _hashData != null)
            {
                stream.Write(BitConverter.GetBytes(content.Length));
                stream.Write(content);

                stream.Write(BitConverter.GetBytes(_hashData.Length));
                stream.Write(_hashData);

                stream.Flush();
                returnData = stream.ToArray();
            }
        }
        catch (Exception ex)
        {
            ExceptionLog.LogException(ex);
        }
        finally
        {
            stream.Dispose();
        }

        return returnData;
    }
    /// <summary>
    /// Sets the token data from the provided byte array.
    /// </summary>
    /// <param name="originalData">
    /// A byte array containing the original data used to constitute the token.
    /// </param>
    public void SetTokenData(byte[]? originalData)
    {
        // Clear any existing data.
        ByteArrayUtil.Clear(_originalData);
        ByteArrayUtil.Clear(_hashData);
        _originalData = null;
        _hashData = null;
        _tokenCreationDate = null;
        _machineName = null;

        if (originalData != null && originalData.Length > 0)
        {
            MemoryStream stream = new MemoryStream(originalData);
            SafeBinaryReader reader = new SafeBinaryReader(stream);
            try
            {
                // Date
                int length = reader.ReadInt32();
                byte[]? dateBytes = reader.ReadBytes(length);
                if (dateBytes != null)
                    _tokenCreationDate = DateTime.FromFileTime(BitConverter.ToInt64(dateBytes));

                // Name
                length = reader.ReadInt32();
                byte[]? nameBytes = reader.ReadBytes(length);
                if (nameBytes != null)
                    _machineName = Encoding.UTF8.GetString(nameBytes);

                // Original Random Data.
                length = reader.ReadInt32();
                _originalData = reader.ReadBytes(length);

                // Hash Data.
                length = reader.ReadInt32();
                _hashData = reader.ReadBytes(length);
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }
            finally
            {
                reader.Dispose();
                stream.Dispose();
            }
        }
    }
    #endregion

    #region Private Methods / Functions
    /// <summary>
    /// Gets the token creation date as a byte array.
    /// </summary>
    /// <returns>
    /// A byte array containing the <see cref="TokenCreationDate"/> value, or <b>null</b>.
    /// </returns>
    private byte[]? GetDateBytes()
    {
        if (_tokenCreationDate == null)
            return null;

        byte[]? dateBits = null;
        try
        {
            long fileTime = _tokenCreationDate.Value.ToFileTime();
            dateBits = BitConverter.GetBytes(fileTime);
        }
        catch (Exception ex)
        {
            ExceptionLog.LogException(ex);
        }
        return dateBits;
    }
    /// <summary>
    /// Gets the machine name as a byte array.
    /// </summary>
    /// <returns>
    /// A byte array containing the name value, or <b>null</b>.
    /// </returns>
    private byte[]? GetNameBytes()
    {
        byte[]? nameBits = null;
        if (_machineName != null)
        {
            try
            {
                nameBits = Encoding.UTF8.GetBytes(_machineName);
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }
        }
        return nameBits;
    }
    /// <summary>
    /// Gets the byte array of data used to calculate the hash value.
    /// </summary>
    /// <returns>
    /// A byte array containing the data content.
    /// </returns>
    private byte[]? GetContentArray()
    {
        byte[]? content = null;

        byte[]? dateBits = GetDateBytes();
        byte[]? nameBits = GetNameBytes();

        if (dateBits != null && nameBits != null && _originalData != null)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            try
            {
                writer.Write(dateBits.Length);
                writer.Write(dateBits);
                writer.Write(nameBits.Length);
                writer.Write(nameBits);
                writer.Write(_originalData.Length);
                writer.Write(_originalData);
                writer.Flush();

                content = stream.ToArray();
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }
            finally
            {
                writer.Dispose();
                stream.Dispose();
            }
        }
        return content;
    }
    /// <summary>
    /// Creates the hash data for the instance.
    /// </summary>
    /// <returns>
    /// A byte array containing the hash data, if successful; otherwise, returns <b>null</b>.
    /// </returns>
    private byte[]? CreateHashData()
    {
        byte[]? hashData = null;

        byte[]? content = GetContentArray();
        if (content != null)
        {
            SHA512 sha = SHA512.Create();
            try
            {
                hashData = sha.ComputeHash(content);
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }
            finally
            {
                sha.Dispose();
            }
            ByteArrayUtil.Clear(content);
        }
        return hashData;
    }
}
#endregion
