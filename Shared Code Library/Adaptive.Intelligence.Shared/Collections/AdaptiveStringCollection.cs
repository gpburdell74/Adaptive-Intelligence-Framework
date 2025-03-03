using System.Collections.Specialized;

namespace Adaptive.Intelligence.Shared
{
#pragma warning disable CS0659
    /// <summary>
    /// Provides the mechanism for storing a list of strings in objects.
    /// </summary>
    /// <seealso cref="StringCollection" />
    public sealed class AdaptiveStringCollection : StringCollection, ICloneable
    {
        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="AdaptiveStringCollection"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public AdaptiveStringCollection()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AdaptiveStringCollection"/> class.
        /// </summary>
        /// <param name="sourceList">
        /// An <see cref="IEnumerable{T}"/> of <see cref="string"/> containing the source list
        /// used to populate the collection.
        /// </param>
        public AdaptiveStringCollection(IEnumerable<string>? sourceList)
        {
            if (sourceList != null)
            {
                foreach (var data in sourceList)
                    Add(data);
            }
        }

        #endregion

        /// <summary>
        /// Adds the content of the provided collection to the current collection.
        /// </summary>
        /// <param name="sourceData">
        /// An <see cref="AdaptiveStringCollection"/> instance containing the source data.
        /// </param>
        public void AddRange(AdaptiveStringCollection? sourceData)
        {
            if (sourceData != null)
            {
                foreach (var data in sourceData)
                {
                    if (data != null)
                        Add(data);
                }
            }
        }
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new <see cref="AdaptiveStringCollection"/> that contains the same items as the current
        /// collection.
        /// </returns>
        public AdaptiveStringCollection Clone()
        {
            var newCollection = new AdaptiveStringCollection();
            newCollection.AddRange(this);
            return newCollection;
        }
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new <see cref="AdaptiveStringCollection"/> that contains the same items as the current
        /// collection.
        /// </returns>
        object ICloneable.Clone()
        {
            return Clone();
        }
        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (!(obj is AdaptiveStringCollection))
            {
                return false;
            }
            else
            {
                return
                    Equals((AdaptiveStringCollection)obj);
            }
        }
        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(AdaptiveStringCollection? other)
        {
            var isEqual = false;

            if (other != null && Count == other.Count)
            {
                isEqual = true;
                if (Count > 0)
                {
                    var len = Count;
                    var pos = 0;
                    do
                    {
                        isEqual = this[pos]?.CompareTo(other[pos]) == 0;
                        pos++;
                    } while (pos < len && isEqual);
                }
            }

            return isEqual;
        }
        /// <summary>
        /// Sets the contents of the collection to that of the supplied list.
        /// </summary>
        /// <param name="originalList">
        /// An <see cref="IEnumerable{T}"/> of <see cref="string"/> containing the original string list.
        /// </param>
        public void Set(IEnumerable<string>? originalList)
        {
            if (originalList != null)
            {
                Clear();
                foreach (var item in originalList)
                    Add(item);
            }
        }
        /// <summary>
        /// Returns the contents of the collection as a list of strings.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="string"/> values populated from the current
        /// instance.
        /// </returns>
        public List<string> ToList()
        {
            var list = new List<string>(Count);
            foreach (var item in this)
            {
                if (item != null)
                    list.Add(item);
            }
            return list;
        }
    }
}
