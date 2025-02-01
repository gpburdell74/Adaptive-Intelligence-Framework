using Adaptive.Intelligence.Shared;

namespace Adaptive.CodeDom.Model
{
    /// <summary>
    /// Provides a definition for a base class reference when generating code.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public class BaseClassModel : DisposableObjectBase
    {
        #region Private Member Declarations		
        /// <summary>
        /// Gets a value indicating whether the instance represents <see cref="System.Object"/>.
        /// </summary>
        private bool _isPoco;
        /// <summary>
        /// Gets a value indicating whether the instance represents a generic type.
        /// </summary>
        private bool _isGeneric;
        /// <summary>
        /// A string containing the base class' data type name.
        /// </summary>
        private string? _typeName;
        /// <summary>
        /// A string containing the data type name of the generic parameter.
        /// </summary>
        private string? _genericTypeName;
        #endregion

        #region Constructor / Dispose Methods		
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseClassModel"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public BaseClassModel()
        {
            _typeName = CodeDomConstants.DefaultObjectName;
            _isPoco = true;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseClassModel"/> class.
        /// </summary>
        /// <param name="baseClassName">
        /// A string containing the name of the data type of the base class.
        /// </param>
        public BaseClassModel(string baseClassName)
        {
            _typeName = baseClassName;
            _isGeneric = false;
            _isPoco = false;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseClassModel"/> class.
        /// </summary>
        /// <param name="baseClassName">
        /// A string containing the name of the data type of the base class.
        /// </param>
        /// <param name="genericTypeName">
        /// A string containing the name of the data type of the generic parameter.
        /// </param>
        public BaseClassModel(string baseClassName, string genericTypeName)
        {
            _typeName = baseClassName;
            _genericTypeName = genericTypeName;
            _isGeneric = true;
            _isPoco = false;

        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _typeName = null;
            _genericTypeName = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a value indicating whether the instance represents <see cref="System.Object"/>.
        /// </summary>
        /// <value>
        /// <b>true</b> if this definition represents the standard <see cref="System.Object"/>; otherwise,
        /// <b>false</b>.
        /// </value>
        public bool IsPOCO
        {
            get => _isPoco;
            set
            {
                _isPoco = value;
                if (value)
                {
                    _isGeneric = false;
                    _typeName = CodeDomConstants.DefaultObjectName;
                }
            }
        }
        /// <summary>
        /// Gets or sets the name of the data type of the generic parameter.
        /// </summary>
        /// <remarks>
        /// This value is ignored if <see cref="IsGeneric"/> is <b>false</b>.
        /// </remarks>
        /// <value>
        /// A string containing the base class' data type name.
        /// </value>
        public string? GenericTypeName
        {
            get => _genericTypeName;
            set
            {
                _genericTypeName = value;
            }
        }
        /// <summary>
        /// Gets a value indicating whether the instance represents a generic type.
        /// </summary>
        /// <value>
        /// <b>true</b> if the base class takes a generic type parameter; otherwise, <b>false</b>.
        /// </value>
        public bool IsGeneric
        {
            get => _isGeneric;
            set
            {
                _isGeneric = value;
                if (value)
                    _isPoco = false;
            }
        }
        /// <summary>
        /// Gets or sets the name of the data type of the base class.
        /// </summary>
        /// <value>
        /// A string containing the base class' data type name.
        /// </value>
        public string? TypeName
        {
            get => _typeName;
            set
            {
                _typeName = value;
            }
        }
        #endregion
    }
}
