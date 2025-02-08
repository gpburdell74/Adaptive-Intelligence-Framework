// Ignore Spelling: Namespaces

using Adaptive.CodeDom.Properties;
using Adaptive.Intelligence.Shared;

namespace Adaptive.CodeDom
{
    /// <summary>
    /// Contains and manages the global Code DOM settings for an application.
    /// </summary>
    public sealed class GenerationSettings : DisposableObjectBase
    {
        private static GenerationSettings _current = new GenerationSettings();

        private NetLanguage _languages = NetLanguage.CSharp;
        private List<string>? _importNamespaces;
        private string? _defaultNamespace;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerationSettings"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public GenerationSettings()
        {
            _importNamespaces = new List<string>();
            _importNamespaces.Add(Resources.NsSystem);
            _importNamespaces.Add(Resources.NsSystemData);
            _importNamespaces.Add(Resources.NsSystemCollectionsGeneric);
            _importNamespaces.Add(Resources.NsSystemThreadingTasks);

            _defaultNamespace = Resources.DefaultNamespace;
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
                _importNamespaces?.Clear();
            }
            _defaultNamespace = null;

            _importNamespaces = null;
            base.Dispose(disposing);
        }
        /// <summary>
        /// Gets the reference to the current settings instance.
        /// </summary>
        /// <value>
        /// The current <see cref="GenerationSettings"/> instance in use.
        /// </value>
        public static GenerationSettings Current => _current;

        /// <summary>
        /// Gets or sets the default namespace.
        /// </summary>
        /// <value>
        /// The default namespace to include the generated classes in.
        /// </value>
        public string? DefaultNamespace
        {
            get => _defaultNamespace;
            set => _defaultNamespace = value;
        }
        /// <summary>
        /// Gets the reference to the list of default namespaces to be included in the generated code, either
        /// as usings in C#, or Imports in Visual Basic.
        /// </summary>
        /// <value>
        /// A <see cref="List{T}"/> of <see cref="string"/> values containing the namespace names.
        /// </value>
        public List<string> DefaultUsingsOrImports
        {
            get
            {
                if (_importNamespaces == null)
                    _importNamespaces = new List<string>();
                return _importNamespaces;
            }
        }
        /// <summary>
        /// Gets or sets the language used to generate the code.
        /// </summary>
        /// <value>
        /// An <see cref="NetLanguage"/> enumerated value indicating C# or Visual Basic.
        /// </value>
        public NetLanguage Language
        {
            get => _languages;
            set => _languages = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [generate sealed classes].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [generate sealed classes]; otherwise, <c>false</c>.
        /// </value>
        public bool GenerateSealedClasses { get; set; } = true;
        /// <summary>
        /// Gets or sets the region name for private members.
        /// </summary>
        /// <value>
        /// The region name for private members.
        /// </value>
        public string RegionNameForPrivateMembers { get; set; } = "Private Member Declarations";
        /// <summary>
        /// Gets or sets the region name for constructor dispose methods.
        /// </summary>
        /// <value>
        /// The region name for constructor dispose methods.
        /// </value>
        public string RegionNameForConstructorDisposeMethods { get; set; } = "Constructor / Dispose Methods";
        /// <summary>
        /// Gets or sets the region name for public properties.
        /// </summary>
        /// <value>
        /// The region name for public properties.
        /// </value>
        public string RegionNameForPublicProperties { get; set; } = "Public Properties";
        /// <summary>
        /// Gets or sets the region name for protected methods.
        /// </summary>
        /// <value>
        /// The region name for protected methods.
        /// </value>
        public string RegionNameForProtectedMethods { get; set; } = "Protected Methods";
        /// <summary>
        /// Gets or sets the region name for public methods.
        /// </summary>
        /// <value>
        /// The region name for public methods.
        /// </value>
        public string RegionNameForPublicMethods { get; set; } = "Public Methods / Functions";
        /// <summary>
        /// Gets or sets the region name for private methods.
        /// </summary>
        /// <value>
        /// The region name for private methods.
        /// </value>
        public string RegionNameForPrivateMethods { get; set; } = "Private Methods / Functions";
        /// <summary>
        /// Gets or sets the region name for public events.
        /// </summary>
        /// <value>
        /// The region name for public events.
        /// </value>
        public string RegionNameForPublicEvents { get; set; } = "Public Events";
        /// <summary>
        /// Gets or sets a value indicating whether [use regions].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use regions]; otherwise, <c>false</c>.
        /// </value>
        public bool UseRegions { get; set; } = true;
        /// <summary>
        /// Gets or sets a value indicating whether to use XML comments.
        /// </summary>
        /// <value>
        ///   <c>true</c> to generate XML comments; otherwise, <c>false</c>.
        /// </value>
        public bool UseXmlComments { get; set; } = true;
        /// <summary>
        /// Gets or sets a value indicating whether [inherits from base class].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [inherits from base class]; otherwise, <c>false</c>.
        /// </value>
        public bool InheritsFromBaseClass { get; set; } = true;
        /// <summary>
        /// Gets or sets the default name of the base class.
        /// </summary>
        /// <value>
        /// The default name of the base class.
        /// </value>
        public string DefaultBaseClassName { get; set; } = "BaseClass";
    }
}
