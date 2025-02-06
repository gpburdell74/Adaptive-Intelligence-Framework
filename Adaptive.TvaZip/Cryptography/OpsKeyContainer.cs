using Adaptive.Intelligence.Shared;
using Adaptive.Intelligence.Shared.Security;

namespace Adaptive.Taz.Cryptography
{
    /// <summary>
    /// Provides a container mechanism for storing and passing the users' AES key and key variant data.
    /// </summary>
    /// <seealso cref="ExceptionTrackingBase" />
    internal sealed class OpsKeyContainer : ExceptionTrackingBase
    {
        #region Private Member Declarations		
        /// <summary>
        /// The (primary) key data store.
        /// </summary>
        private AesKeyStore? _keyData;
        /// <summary>
        /// The variant key data store.
        /// </summary>
        private AesKeyStore? _keyVariantData;
        #endregion

        #region Constructor / Dispose Methods		
        /// <summary>
        /// Initializes a new instance of the <see cref="OpsKeyContainer"/> class.
        /// </summary>
        /// <param name="keyData">
        /// The reference to the <see cref="AesKeyStore"/> containing the key data.
        /// </param>
        /// <param name="variantData">
        /// The reference to the <see cref="AesKeyStore"/> containing the key variant data.
        /// </param>
        public OpsKeyContainer(AesKeyStore keyData, AesKeyStore variantData)
        {
            _keyData = keyData;
            _keyVariantData = variantData;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="OpsKeyContainer"/> class.
        /// </summary>
        /// <param name="keyData">
        /// A byte array containing the AES Key and IV values to be stored as a single 48-element array.
        /// </param>
        /// <param name="variantData">
        /// A byte array containing the variant AES Key and IV values to be stored as a single 48-element array.
        /// </param>
        public OpsKeyContainer(byte[] keyData, byte[] variantData)
        {
            _keyData = new AesKeyStore();
            _keyData.SetKeyIVData(keyData);

            _keyVariantData = new AesKeyStore();
            _keyVariantData.SetKeyIVData(variantData);
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
                _keyData?.Dispose();
                _keyVariantData?.Dispose();
            }

            _keyData = null;
            _keyVariantData = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties		
        /// <summary>
        /// Gets the reference to the key store containing the key data.
        /// </summary>
        /// <value>
        /// The <see cref="AesKeyStore"/> storing the user key data.
        /// </value>
        public AesKeyStore? Key => _keyData;
        /// <summary>
        /// Gets the reference to the key variant containing the key variant data.
        /// </summary>
        /// <value>
        /// The <see cref="AesKeyStore"/> storing the user key variant data.
        /// </value>
        public AesKeyStore? Variant => _keyVariantData;
        #endregion
    }
}