// Ignore Spelling: Poco

using Adaptive.Intelligence.Shared;

namespace Adaptive.CodeDom.Model
{
    /// <summary>
    /// Represents and manages the data and meta data for a class being generated.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public sealed class ClassModel : DisposableObjectBase
    {
        #region Private Member Declarations		
        /// <summary>
        /// The access modifier for the class (public/protected/private, etc.)
        /// </summary>
        private TypeAccessModifier _accessModifier = TypeAccessModifier.NotSpecified;
        /// <summary>
        /// The base class definition.
        /// </summary>
        private BaseClassModel? _baseClass;
        /// <summary>
        /// The class name.
        /// </summary>
        private string? _className;
        /// <summary>
        /// The code sections list.
        /// </summary>
        private CodeSectionModelCollection? _codeSections;
        /// <summary>
        /// The list of usings / Imports for the class code file.
        /// </summary>
        private List<string>? _imports;
        /// <summary>
        /// The inheritance modified (abstract, sealed, or neither).
        /// </summary>
        private InheritanceModifier _inheritance = InheritanceModifier.NotSpecified;
        /// <summary>
        /// The list of interfaces to be implemented by the class.
        /// </summary>
        private List<string>? _interfaces;
        /// <summary>
        /// The namespace the class belongs to.
        /// </summary>
        private string? _namespaceName;
        /// <summary>
        /// The XML Remarks comment text for the class.
        /// </summary>
        private string? _remarks;
        /// <summary>
        /// The list of XML See Also text comments.
        /// </summary>
        private List<string>? _seeAlsoList;
        /// <summary>
        /// The XML Summary comment text for the class.
        /// </summary>
        private string? _summary;
        #endregion

        #region Constructor / Dispose Methods		
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassModel"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public ClassModel()
        {
            _baseClass = new BaseClassModel();
            _baseClass.IsPOCO = true;
            _seeAlsoList = new List<string>();
            _interfaces = new List<string>();
            _imports = new List<string>();
            _codeSections = new CodeSectionModelCollection(true);
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
                _baseClass?.Dispose();
                _seeAlsoList?.Clear();
                _interfaces?.Clear();
                _imports?.Clear();
                _codeSections?.Clear();
            }
            _baseClass = null;
            _seeAlsoList = null;
            _interfaces = null;
            _imports = null;
            _codeSections = null;

            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties		
        /// <summary>
        /// Gets the reference to the base class definition.
        /// </summary>
        /// <value>
        /// The <see cref="BaseClassModel"/> instance.
        /// </value>
        public BaseClassModel? BaseClass => _baseClass;
        /// <summary>
        /// Gets or sets the name of the class.
        /// </summary>
        /// <value>
        /// A string containing the name of the class.
        /// </value>
        public string? ClassName
        {
            get => _className;
            set
            {
                _className = value;

                //Ensure the name change propagates.
                if (_codeSections != null)
                {
                    foreach (CodeSectionModel section in _codeSections)
                    {
                        section.ClassName = value;
                    }
                }
            }
        }
        /// <summary>
        /// Gets the reference to the list of code sections.
        /// </summary>
        /// <value>
        /// A <see cref="CodeSectionModelCollection"/> containing the list of code sections.	
        /// </value>
        public CodeSectionModelCollection? CodeSections { get => _codeSections; }
        /// <summary>
        /// Gets the reference to the list of imports/usings for the class file.
        /// </summary>
        /// <value>
        /// A <see cref="List{T}"/> of <see cref="string"/> containing the namespace names.
        /// </value>
        public List<string>? Imports => _imports;
        /// <summary>
        /// Gets the reference to the list of interfaces to be implemented.
        /// </summary>
        /// <value>
        /// A <see cref="List{T}"/> of <see cref="string"/> containing the interface names.
        /// </value>
        public List<string>? Interfaces => _interfaces;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is declared as an abstract class.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the class is abstract; otherwise, <c>false</c>.
        /// </value>
        public bool IsAbstract
        {
            get => _inheritance == InheritanceModifier.Abstract;
            set
            {
                if (value)
                    _inheritance = InheritanceModifier.Abstract;
                else if (_inheritance == InheritanceModifier.Abstract)
                {
                    _inheritance = InheritanceModifier.NotSpecified;
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether the class is a private class.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the class is private; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrivate
        {
            get => _accessModifier == TypeAccessModifier.Private;
            set
            {
                if (value)
                    _accessModifier = TypeAccessModifier.Private;
                else if (_accessModifier == TypeAccessModifier.Private)
                    _accessModifier = TypeAccessModifier.NotSpecified;
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether the class is a protected class.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the class is protected; otherwise, <c>false</c>.
        /// </value>
        public bool IsProtected
        {
            get => _accessModifier == TypeAccessModifier.Protected;
            set
            {
                if (value)
                    _accessModifier = TypeAccessModifier.Protected;
                else if (_accessModifier == TypeAccessModifier.Protected)
                    _accessModifier = TypeAccessModifier.NotSpecified;
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether the class is a public class.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the class is public; otherwise, <c>false</c>.
        /// </value>
        public bool IsPublic
        {
            get => _accessModifier == TypeAccessModifier.Public;
            set
            {
                if (value)
                    _accessModifier = TypeAccessModifier.Public;
                else if (_accessModifier == TypeAccessModifier.Public)
                    _accessModifier = TypeAccessModifier.NotSpecified;
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is declared as a sealed / final class.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the class is sealed/final; otherwise, <c>false</c>.
        /// </value>
        public bool IsSealed
        {
            get => _inheritance == InheritanceModifier.SealedFinal;
            set
            {
                if (value)
                    _inheritance = InheritanceModifier.SealedFinal;
                else if (_inheritance == InheritanceModifier.SealedFinal)
                {
                    _inheritance = InheritanceModifier.NotSpecified;
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is declared as a static class.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the class is static; otherwise, <c>false</c>.
        /// </value>
        public bool IsStatic
        {
            get => _inheritance == InheritanceModifier.Static;
            set
            {
                if (value)
                    _inheritance = InheritanceModifier.Static;
                else if (_inheritance == InheritanceModifier.Static)
                {
                    _inheritance = InheritanceModifier.NotSpecified;
                }
            }
        }

        public string Namespace
        {
            get
            {
                if (_namespaceName == null)
                    return string.Empty;
                else
                    return _namespaceName;
            }
            set => _namespaceName = value;
        }
        /// <summary>
        /// Gets or sets the XML Remarks comment text.
        /// </summary>
        /// <value>
        /// A string containing the remarks text, or <b>null</b> if the XML remarks are not used.
        /// </value>
        public string? Remarks
        {
            get => _remarks;
            set => _remarks = value;
        }
        /// <summary>
        /// Gets the reference to the list of XML See Also comments.
        /// </summary>
        /// <value>
        /// A <see cref="List{T}"/> of string containing the XML "see also" comments.
        /// </value>
        public List<string>? SeeAlsoList => _seeAlsoList;
        /// <summary>
        /// Gets or sets the XML Summary comment text.
        /// </summary>
        /// <value>
        /// A string containing the remarks text, or <b>null</b> if the XML summary is not used.
        /// </value>
        public string? Summary
        {
            get => _summary;
            set => _summary = value;
        }
        #endregion

        #region Public Methods / Functions		
        /// <summary>
        /// Adds the import specification.
        /// </summary>
        /// <param name="namespaceName">
        /// A string containing the name of the namespace to import.
        /// </param>
        public void AddImport(string namespaceName)
        {
            if (_imports != null)
                _imports.Add(namespaceName);
        }
        /// <summary>
        /// Adds the specified interface to the list of interfaces to be implemented.
        /// </summary>
        /// <param name="interfaceType">
        /// A <see cref="Type"/> specifying the data type of the interface.
        /// </param>
        public void AddInterface(Type? interfaceType)
        {
            if (interfaceType != null && interfaceType.IsInterface)
            {
                AddInterface(interfaceType.Name);
            }
        }
        /// <summary>
        /// Adds the specified interface to the list of interfaces to be implemented.
        /// </summary>
        /// <param name="interfaceName">
        /// A string containing the name of the data type of the interface.
        /// </param>
        public void AddInterface(string? interfaceName)
        {
            if (!string.IsNullOrEmpty(interfaceName))
            {
                if (_interfaces == null)
                    _interfaces = new List<string>();
                _interfaces.Add(interfaceName);
            }
        }
        /// <summary>
        /// Adds the "See Also" XML text comment.
        /// </summary>
        /// <param name="seeAlsoText">
        /// A string containing the text of the see also reference.
        /// </param>
        public void AddSeeAlsoText(string seeAlsoText)
        {
            if (_seeAlsoList != null)
                _seeAlsoList.Add(seeAlsoText);
        }
        /// <summary>
        /// Gets the code section of the specified type.
        /// </summary>
        /// <param name="sectionType">
        /// A <see cref="CodeSectionType"/> enumerated value indicating the section type to find.
        /// </param>
        /// <returns>
        /// A <see cref="CodeSectionModel"/> instance if found; otherwise, returns <b>null</b>.
        /// </returns>
        public CodeSectionModel? GetCodeSectionByType(CodeSectionType sectionType)
        {
            CodeSectionModel? model = null;

            if (_codeSections != null)
                model = _codeSections.GetSectionByType(sectionType);
            return model;
        }
        /// <summary>
        /// Sets the base class properties.
        /// </summary>
        /// <param name="dataAccessBaseClass">
        /// A string containing the name of the data type of the data access base class, or <b>null</b> to use "object".
        /// </param>
        /// <param name="isPoco">
        /// <b>true</b> if the base class represents <see cref="System.Object"/>.
        /// </param>
        /// <param name="isGeneric">
        /// <b>true</b> if the base class definition requires a generic type parameter.
        /// </param>
        /// <param name="genericTypeName">
        /// A string containing the name of the data type for the generic type.
        /// </param>
        public void SetBaseClassProperties(string? dataAccessBaseClass, bool isPoco, bool isGeneric, string? genericTypeName = null)
        {
            if (_baseClass == null)
                _baseClass = new BaseClassModel();

            if (dataAccessBaseClass == null)
            {
                _baseClass.TypeName = CodeDomConstants.DefaultObjectName;
                _baseClass.IsPOCO = true;
            }
            else
            {
                _baseClass.TypeName = dataAccessBaseClass;
                _baseClass.IsPOCO = isPoco;
                _baseClass.IsGeneric = isGeneric;
                _baseClass.GenericTypeName = genericTypeName;
            }
        }
        #endregion
    }
}
