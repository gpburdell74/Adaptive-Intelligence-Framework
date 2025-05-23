﻿namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Represents a SQL expression that performs an assignment.
    /// </summary>
    /// <example>
    /// [column] = 12332
    /// </example>
    /// <seealso cref="SqlCodeExpression" />
    public sealed class SqlCodeAssignmentExpression : SqlCodeExpression
    {
        #region Private Member Declarations        
        /// <summary>
        /// The expression indicating the item being assigned to.
        /// </summary>
        private SqlCodeExpression? _assignedToExpression;
        /// <summary>
        /// The expression indicating the value being assigned.
        /// </summary>
        private SqlCodeExpression? _valueExpression;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeAssignmentExpression"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public SqlCodeAssignmentExpression()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCodeAssignmentExpression"/> class.
        /// </summary>
        /// <param name="assignedToExpression">
        /// A <see cref="SqlCodeExpression"/>-derived instance representing the item being assigned to.
        /// </param>
        /// <param name="valueExpression">
        /// A <see cref="SqlCodeExpression"/>-derived instance representing the value being assigned.
        /// </param>
        public SqlCodeAssignmentExpression(SqlCodeExpression? assignedToExpression, SqlCodeExpression? valueExpression)
        {
            _assignedToExpression = assignedToExpression ?? throw new ArgumentNullException(nameof(assignedToExpression));
            _valueExpression = valueExpression ?? throw new ArgumentNullException(nameof(valueExpression));
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
                _assignedToExpression?.Dispose();
                _valueExpression?.Dispose();
            }

            _assignedToExpression = null;
            _valueExpression = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the reference to the expression indicating the item being assigned to.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeExpression"/> defining the item being assigned to.
        /// </value>
        public SqlCodeExpression? AssignedToExpression
        {
            get => _assignedToExpression;
            set
            {
                _assignedToExpression?.Dispose();
                if (value == null)
                    _assignedToExpression = null;
                else
                    _assignedToExpression = value.Clone();
            }
        }
        /// <summary>
        /// Gets or sets the reference to the expression indicating the value being assigned.
        /// </summary>
        /// <value>
        /// A <see cref="SqlCodeExpression"/> defining the value being assigned.
        /// </value>
        public SqlCodeExpression? ValueExpression
        {
            get => _valueExpression;
            set
            {
                _valueExpression?.Dispose();
                if (value == null)
                    _valueExpression = null;
                else
                    _valueExpression = value.Clone();
            }
        }
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override SqlCodeAssignmentExpression Clone()
        {
            return new SqlCodeAssignmentExpression(_assignedToExpression, _valueExpression);
        }
        #endregion
    }
}