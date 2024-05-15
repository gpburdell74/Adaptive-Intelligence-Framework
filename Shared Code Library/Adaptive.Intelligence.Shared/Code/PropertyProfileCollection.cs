namespace Adaptive.Intelligence.Shared.Code
{
    /// <summary>
    /// Contains a list of <see cref="PropertyProfile"/> instances.
    /// </summary>
    /// <seealso cref="List{T}" />
    public sealed class PropertyProfileCollection : List<PropertyProfile>
    {
        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyProfileCollection"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public PropertyProfileCollection()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyProfileCollection"/> class.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        public PropertyProfileCollection(int capacity) : base(capacity)
        {
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets a value indicating whether this instance has property definitions with data types that
        /// are lists or are <see cref="IDisposable"/>.
        /// </summary>
        /// <value>
        ///   <b>true</b> if this instance at least one property whose data type is a list or is 
        ///   <see cref="IDisposable"/>; otherwise, <b>false</b>.
        /// </value>
        public bool HasClearableOrDisposableItems
        {
            get
            {
                int count = 0;
                int index = 0;
                do
                {
                    PropertyProfile item = this[index];
                    if (item.IsList || item.IsDisposable)
                        count++;
                    index++;
                } while (index < Count && count == 0);

                return (count > 0);
            }
        }

        #endregion

        #region Public Methods / Functions                
        /// <summary>
        /// Gets a list of the property definitions whose data type is a list.
        /// </summary>
        /// <returns>
        /// A <see cref="PropertyProfileCollection"/> instance containing the list.
        /// </returns>
        public PropertyProfileCollection ClearableProperties()
        {
            PropertyProfileCollection list = new PropertyProfileCollection(Count);
            foreach (PropertyProfile item in this)
            {
                if (item.IsList)
                    list.Add(item);
            }
            return list;
        }
        /// <summary>
        /// Gets a list of the property definitions whose data type implements <see cref="IDisposable"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="PropertyProfileCollection"/> instance containing the list.
        /// </returns>
        public PropertyProfileCollection DisposableProperties()
        {
            PropertyProfileCollection list = new PropertyProfileCollection(Count);
            foreach (PropertyProfile item in this)
            {
                if (item.IsDisposable)
                    list.Add(item);
            }
            return list;
        }
        /// <summary>
        /// Gets a list of the property definitions whose data type is nullable.
        /// </summary>
        /// <returns>
        /// A <see cref="PropertyProfileCollection"/> instance containing the list.
        /// </returns>
        public PropertyProfileCollection NullableProperties()
        {
            PropertyProfileCollection list = new PropertyProfileCollection(Count);
            foreach (PropertyProfile item in this)
            {
                if (item.IsNullable)
                    list.Add(item);
            }
            return list;
        }
        /// <summary>
        /// Sorts the items in collection by property name in ascending alphabetical order.
        /// </summary>
        public void SortAlphabetical()
        {
            List<PropertyProfile> list = this.OrderBy(x => x.PropertyName).ToList();
            Clear();
            AddRange(list);
        }
        #endregion
    }
}
