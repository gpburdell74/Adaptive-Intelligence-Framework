using Adaptive.Intelligence.Shared;

namespace Adaptive.CodeDom.Model
{
    /// <summary>
    /// Contains a list of <see cref="ClassModel"/> instances, stored by name.
    /// </summary>
    /// <seealso cref="NameIndexCollection{Task}" />
    public sealed class ClassModelCollection : NameIndexCollection<ClassModel>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassModelCollection"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public ClassModelCollection()
        {
        }
        #endregion

        #region Protected Method Overrides		
        /// <summary>
        /// Gets the name / key value of the specified instance.
        /// </summary>
        /// <param name="item">The <see cref="ClassModel"/> item to be stored in the collection.</param>
        /// <returns>
        /// The name / key value of the specified instance.
        /// </returns>
        /// <remarks>
        /// This is called from several methods, including the Add() method, to identify the instance
        /// being added.
        /// </remarks>
        protected override string GetName(ClassModel item)
        {
            // Store things by namespace and class name.
            if (item.ClassName == null)
                return string.Empty;
            else if (!string.IsNullOrEmpty(item.Namespace))
                return item.Namespace + "." + item.ClassName;
            else
                return item.ClassName;
        }
        #endregion
    }
}
