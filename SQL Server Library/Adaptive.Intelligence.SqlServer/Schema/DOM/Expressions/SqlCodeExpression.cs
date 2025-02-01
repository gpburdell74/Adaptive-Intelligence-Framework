using Adaptive.Intelligence.Shared;

namespace Adaptive.Intelligence.SqlServer.CodeDom
{
    /// <summary>
    /// Provides the base definition for objects that represent expressions in T-SQL.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public abstract class SqlCodeExpression : DisposableObjectBase, ICloneable
    {
        #region Public Abstract Methods
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public abstract SqlCodeExpression Clone();
        #endregion

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
