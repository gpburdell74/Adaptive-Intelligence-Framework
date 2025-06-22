using Adaptive.Intelligence.Shared;
using Adaptive.LanguageService;
using Adaptive.LanguageService.Providers;

namespace Adaptive.BlazorBasic.LanguageService;

/// <summary>
/// Provides the implementation for the provider that defines the list of operators.
/// </summary>
/// <seealso cref="DisposableObjectBase"/>
/// <seealso cref="IComparisonOperatorProvider"/>
public sealed class OperatorProvider : DisposableObjectBase, IOperatorProvider
{
    #region Private Member Declarations
    private IAssignmentOperatorProvider? _assignmentProvider;
    private IBitwiseOperatorProvider? _bitwiseProvider;
    private IComparisonOperatorProvider? _comparisonProvider;
    private ILogicalOperatorProvider? _logicalProvider;
    private IMathOperatorProvider? _mathProvider;
    private IOperationOperatorProvider? _operationalProvider;
    #endregion

    #region Constructor / Dispose Methods
    /// <summary>
    /// Initializes a new instance of the <see cref="OperatorProvider"/> class.
    /// </summary>
    /// <param name="assignmentProvider">
    /// The reference to the <see cref="IAssignmentOperatorProvider"/> instance.
    /// </param>
    /// <param name="bitwiseProvider">
    /// The reference to the <see cref="IBitwiseOperatorProvider"/> instance.
    /// </param>
    /// <param name="comparisonProvider">
    /// The reference to the <see cref="IComparisonOperatorProvider"/> instance.
    /// </param>
    /// <param name="logicalProvider">
    /// The reference to the <see cref="ILogicalOperatorProvider"/> instance.
    /// </param>
    /// <param name="mathProvider">
    /// The reference to the <see cref="IMathOperatorProvider"/> instance.
    /// </param>
    /// <param name="operationalProvider">
    /// The reference to the <see cref="IOperationOperatorProvider"/> instance.
    /// </param>
    public OperatorProvider(
            IAssignmentOperatorProvider assignmentProvider,
            IBitwiseOperatorProvider bitwiseProvider,
            IComparisonOperatorProvider comparisonProvider,
            ILogicalOperatorProvider logicalProvider,
            IMathOperatorProvider mathProvider,
            IOperationOperatorProvider operationalProvider)
    {
        _assignmentProvider = assignmentProvider;
        _bitwiseProvider = bitwiseProvider;
        _comparisonProvider = comparisonProvider;
        _logicalProvider = logicalProvider;
        _mathProvider = mathProvider;
        _operationalProvider = operationalProvider;
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
    /// <b>false</b> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        _assignmentProvider = null;
        _bitwiseProvider = null;
        _comparisonProvider = null;
        _logicalProvider = null;
        _mathProvider = null;
        _operationalProvider = null;
        base.Dispose(disposing);
    }
    #endregion

    #region Public Properties    
    /// <summary>
    /// Gets the reference to the provider for assignment operators.
    /// </summary>
    /// <value>
    /// The <see cref="IAssignmentOperatorProvider" /> instance that provides the list of assignment operators.
    /// </value>
    public IAssignmentOperatorProvider AssignmentOperators
    {
        get
        {
            if (_assignmentProvider == null)
                throw new Exception();
            return _assignmentProvider;
        }
    }

    /// <summary>
    /// Gets the reference to the provider for bitwise operators.
    /// </summary>
    /// <value>
    /// The <see cref="IBitwiseOperatorProvider" /> instance that provides the list of assignment operators.
    /// </value>
    public IBitwiseOperatorProvider BitwiseOperators
    {
        get
        {
            if (_bitwiseProvider == null)
                throw new Exception();
            return _bitwiseProvider;
        }
    }

    /// <summary>
    /// Gets the reference to the provider for comparison operators.
    /// </summary>
    /// <value>
    /// The <see cref="IComparisonOperatorProvider" /> instance that provides the list of assignment operators.
    /// </value>
    public IComparisonOperatorProvider ComparisonOperators
    {
        get
        {
            if (_comparisonProvider == null)
                throw new Exception();
            return _comparisonProvider;
        }
    }

    /// <summary>
    /// Gets the reference to the provider for logical operators.
    /// </summary>
    /// <value>
    /// The <see cref="ILogicalOperatorProvider" /> instance that provides the list of assignment operators.
    /// </value>
    public ILogicalOperatorProvider LogicalOperators
    {
        get
        {
            if (_logicalProvider == null)
                throw new Exception();
            return _logicalProvider;
        }
    }

    /// <summary>
    /// Gets the reference to the provider for arithmetic operators.
    /// </summary>
    /// <value>
    /// The <see cref="IMathOperatorProvider" /> instance that provides the list of assignment operators.
    /// </value>
    public IMathOperatorProvider MathOperators
    {
        get
        {
            if (_mathProvider == null)
                throw new Exception();
            return _mathProvider;
        }
    }

    /// <summary>
    /// Gets the reference to the provider for operation operators.
    /// </summary>
    /// <value>
    /// The <see cref="IOperationOperatorProvider" /> instance that provides the list of assignment operators.
    /// </value>
    public IOperationOperatorProvider OperationOperators
    {
        get
        {
            if (_operationalProvider == null)
                throw new Exception();
            return _operationalProvider;
        }
    }

    #endregion

    /// <summary>
    /// Initializes the content of the provider.
    /// </summary>
    public void Initialize()
    {
    }

    /// <summary>
    /// Renders the list of operator ID values for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of unique ID values used for mapping ID to text content.
    /// </returns>
    public List<int> RenderOperatorIds()
    {
        List<int> idList = new List<int>(25);

        StandardOperators[] valueList = Enum.GetValues<StandardOperators>();

        foreach (StandardOperators item in valueList)
            idList.Add((int)item);

        return idList;
    }
    /// <summary>
    /// Renders the list of operator names for mapping.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}" /> of unique name values used for mapping text to ID values.
    /// </returns>
    public List<string> RenderOperatorNames()
    {
        List<string> list = new List<string>();

        list.AddRange(_assignmentProvider!.RenderOperatorNames());
        list.AddRange(_bitwiseProvider!.RenderOperatorNames());
        list.AddRange(_comparisonProvider!.RenderOperatorNames());
        list.AddRange(_logicalProvider!.RenderOperatorNames());
        list.AddRange(_mathProvider!.RenderOperatorNames());
        list.AddRange(_operationalProvider!.RenderOperatorNames());

        return list;
    }
}