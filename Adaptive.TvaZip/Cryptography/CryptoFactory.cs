using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Logging;
using System.Security.Cryptography;

namespace Adaptive.Taz.Cryptography
{
    /// <summary>
    /// Provides static methods and functions for instantiating cryptographic object instances.
    /// </summary>
    internal static class CryptoFactory
    {
        /// <summary>
        /// Converts the byte array to an array of <see cref="long"/> integers.
        /// </summary>
        /// <param name="originalData">
        /// A byte array containing the original data.
        /// </param>
        /// <returns>
        /// An array of <see cref="long"/> values.
        /// </returns>
        public static long[] BytesToInt64Array(byte[] originalData)
        {
            int length = originalData.Length;

            //Translate the bytes to numbers.
            List<long> longList = new List<long>();
            for (int count = 0; count < length; count += 8)
            {
                // Read the next 8 bytes and translate to an Int64.
                long longValue = BitConverter.ToInt64(originalData, count);
                longList.Add(longValue);
            }

            return longList.ToArray();
        }
        /// <summary>
        /// Creates the standard password derivator instance.
        /// </summary>
        /// <param name="sourceData">
        /// A string containing the source "password" data used to generate encryption keys.
        /// </param>
        /// <param name="salt">
        /// A byte array containing the random salt value.
        /// </param>
        /// <returns>
        /// A <see cref="PasswordDeriveBytes"/> instance if successful; otherwise, returns <b>null</b>.
        /// </returns>
        public static PasswordDeriveBytes? CreateStandardPasswordDerivator(string sourceData, byte[] salt)
        {
            PasswordDeriveBytes? derivator = null;

            try
            {
                derivator = new PasswordDeriveBytes(
                                    sourceData,
                                    salt,
                                    CryptoConstants.AlgorithmSHA512,
                                    CryptoConstants.Iterations,
                                    null);
            }
            catch
            {
                // Log Exception.
            }
            return derivator;
        }
        /// <summary>
        /// Derives an encryption key from the provided data and salt.
        /// </summary>
        /// <param name="sourceData">
        /// A string containing the source "password" data used to generate encryption keys.
        /// </param>
        /// <param name="salt">
        /// A byte array containing the random salt value.
        /// </param>
        /// <returns>
        /// A byte array containing the new encryption key data.
        /// </returns>
        public static byte[]? DeriveEncryptionKey(string sourceData, byte[] salt)
        {
            byte[]? newKey = null;

            // Derive a partial encryption key from the provided string data.
            PasswordDeriveBytes? deriviator = CreateStandardPasswordDerivator(sourceData, salt);
            if (deriviator != null)
            {
                try
                {
                    newKey = deriviator.CryptDeriveKey(
                        CryptoConstants.DeriveKeyAlgorithm,
                        HashAlgorithmName.SHA256.Name,
                        CryptoConstants.IVSize,
                        salt);
                }
                catch (Exception ex)
                {
                    // Log Exception.
                    ExceptionLog.LogException(ex);
                }

                deriviator.Dispose();
            }

            return newKey;
        }
        /// <summary>
        /// Translates the array of <see cref="long"/> values to a single byte array.
        /// </summary>
        /// <param name="numbers">
        /// An <see cref="IEnumerable{T}"/> of <see cref="long"/> values.
        /// </param>
        /// <returns>
        /// A byte array representing the long integer values in sequence.
        /// </returns>
        public static byte[] Int64ArrayToBytes(IEnumerable<long> numbers)
        {
            List<byte[]> list = new List<byte[]>();

            // Translate each int64 to an 8 element byte array.
            foreach (long value in numbers)
            {
                // Translate back to a byte array.
                byte[] asByteArray = BitConverter.GetBytes(value);
                list.Add(asByteArray);
            }

            // Concatenate the arrays.
            byte[] combined = ByteArrayUtil.ConcatenateArrays(list.ToArray());
            list.Clear();

            return combined;
        }
    }
}
