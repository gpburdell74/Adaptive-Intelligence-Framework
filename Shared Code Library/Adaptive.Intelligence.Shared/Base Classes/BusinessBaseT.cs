using Adaptive.Intelligence.Shared.Logging;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Adaptive.Intelligence.Shared
{
    /// <summary>
    /// Provides a base implementation for business objects.
    /// </summary>
    /// <seealso cref="PropertyAwareBase" />
    /// <seealso cref="DisposableObjectBase"/>
    /// <seealso cref="IValidatableObject"/>
    /// <typeparam name="T">
    /// The data type of the underlying entity.
    /// </typeparam>
    public abstract class BusinessBase<T> : BusinessBase
            where T : class
    {
        #region Private Member Declarations		
        /// <summary>
        /// The entity containing the raw data.
        /// </summary>
        private T? _entity;

        /// <summary>
        /// The reflection property information cache.
        /// </summary>
        private Dictionary<string, PropertyInfo>? _propertyInfoCache;
        #endregion

        #region Constructor / Dispose Methods		
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessBase{T}"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        protected BusinessBase()
        {
            // Cache the property information for type: T.
            CachePropertyInfo();

            _entity = Activator.CreateInstance<T>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessBase{T}"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected BusinessBase(T entity)
        {
            // Cache the property information for type: T.
            CachePropertyInfo();

            _entity = entity;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
        /// <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed && disposing)
                _propertyInfoCache?.Clear();

            _propertyInfoCache = null;
            _entity = default;
            base.Dispose(disposing);
        }
        #endregion

        #region Protected Properties		
        /// <summary>
        /// Gets the reference to the underlying entity instance.
        /// </summary>
        /// <value>
        /// The entity of <typeparamref name="T"/> used to contain the data for the class.
        /// </value>
        internal protected T? Entity => _entity;


        /// <summary>
        /// Gets the reference to the underlying entity instance.
        /// </summary>
        /// <returns>
        /// The reference to the <typeparamref name="T"/> instance containing the data for the object.
        /// </returns>
        public virtual T? GetEntity()
        {
            if (_entity is ICloneable cloneable)
                return (T)cloneable.Clone();

            return _entity;
        }
        #endregion

        #region Abstract / Protected Methods
        /// <summary>
        /// When overridden in a derived class, performs the operation to delete the current instance.
        /// </summary>
        /// <param name="entity">
        /// The <typeparamref name="T"/> instance being deleted.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b> .
        /// </returns>
        public abstract bool PerformDelete(T entity);
        /// <summary>
        /// When overridden in a derived class, performs the operation to delete the current instance.
        /// </summary>
        /// <param name="entity">
        /// The <typeparamref name="T"/> instance being deleted.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b> .
        /// </returns>
        public abstract Task<bool> PerformDeleteAsync(T entity);
        /// <summary>
        /// When overridden in a derived class, perform the process of loading the content of the instance.
        /// </summary>
        /// <typeparam name="IdType">
        /// The data type of the parameter used as an identity value to load the instance.
        /// </typeparam>
        /// <param name="id">
        /// The <typeparamref name="IdType"/> data content used to identify the content to be loaded.
        /// </param>
        /// <returns>
        /// The reference to the <typeparamref name="T"/> entity that was loaded, or <b>null</b>.
        /// </returns>
        protected abstract Task<T?> PerformLoadAsync<IdType>(IdType? id);
        /// <summary>
        /// When overridden in a derived class, performs the operation to save / store the current instance.
        /// </summary>
        /// <param name="entity">
        /// The <typeparamref name="T"/> instance to be saved.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b> .
        /// </returns>
        protected abstract bool PerformSave(T entity);
        /// <summary>
        /// When overridden in a derived class, performs the operation to save / store the current instance.
        /// </summary>
        /// <param name="entity">
        /// The <typeparamref name="T"/> instance to be saved.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b> .
        /// </returns>
        protected abstract Task<bool> PerformSaveAsync(T entity);
        #endregion

        #region Property Methods

        /// <summary>
        /// Reads the value of the specified property.
        /// </summary>
        /// <remarks>
        /// This is used to apply the null checks, type checks, etc.  universally for all string properties.
        /// </remarks>
        /// <param name="propertyName">
        /// A string containing the name of the property on the underlying entity to be read.
        /// </param>
        /// <returns>
        /// The property value of <typeparamref name="TProperty"/>, if successful.
        /// </returns>
        protected virtual TProperty? ReadProperty<TProperty>(string propertyName)
        {
            var propValue = default(TProperty);
            PropertyInfo? propInfo = GetPropertyInfo(propertyName);

            // Ensure we can actually access and read the specified property.
            if (propInfo != null && _entity != null && propInfo.CanRead)
            {
                // Get the value or null.
                try
                {
                    propValue = (TProperty?)propInfo.GetValue(_entity);
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                    propValue = default(TProperty);
                }
            }

            return propValue;
        }

        /// <summary>
        /// Sets the value of the specified property.
        /// </summary>
        /// <remarks>
        /// This is used to apply the null checks, type checks, etc.  universally for all string properties.
        /// </remarks>
        /// <param name="propertyName">
        /// A string containing the name of the property on the underlying entity to be written.
        /// </param>
        /// <param name="value">
        /// The value to store in the property.
        /// </param>
        /// <typeparam name="TProperty">
        /// The data type of the property value.
        /// </typeparam>
        protected virtual void SetProperty<TProperty>(string propertyName, TProperty? value)
        {
            PropertyInfo? propInfo = GetPropertyInfo(propertyName);

            // Ensure we can actually access and read the specified property.
            if (propInfo != null && _entity != null && propInfo.CanWrite)
            {
                // Do nothing if the value is not actually changed.
                object? box = propInfo.GetValue(_entity);

                int original = -1;
                int newHash = -1;
                if (value is not null)
                    original = value.GetHashCode();

                if (box != null)
                    newHash = box.GetHashCode();

                if (original != newHash)
                {
                    try
                    {
                        propInfo.SetValue(_entity, value);
                        OnPropertyChanged(propertyName);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog.LogException(ex);
                    }
                }
            }
        }
        #endregion

        #region Public Methods / Functions		
        /// <summary>
        /// Performs the operation to delete the current instance.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b> .
        /// </returns>
        public override bool Delete()
        {
            bool success = false;
            try
            {
                if (_entity != null)
                    success = PerformDelete(_entity);
            }
            catch (Exception ex)
            {
                RecordException(ex);
                success = false;
            }

            return success;

        }
        /// <summary>
        /// When overridden in a derived class, performs the operation to delete the current instance.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b> .
        /// </returns>
        public override async Task<bool> DeleteAsync()
        {
            bool success = false;
            try
            {
                if (_entity != null)
                    success = await PerformDeleteAsync(_entity).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                RecordException(ex);
                success = false;
            }

            return success;

        }
        /// <summary>
        /// Loads the content of the instance.
        /// </summary>
        /// <typeparam name="IdType">
        /// The data type of the parameter used as an identity value to load the instance.
        /// </typeparam>
        /// <param name="id">
        /// The <typeparamref name="IdType"/> data content used to identify the content to be loaded.
        /// </param>
        /// <returns>
        /// <b>true</b> if the load operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public new T? Load<IdType>(IdType? id)
        {
            _entity = default(T);
            try
            {
                _entity = PerformLoad<IdType, T>(id);
            }
            catch (Exception ex)
            {
                RecordException(ex);
                _entity = default;

            }

            return _entity;
        }
        /// <summary>
        /// Loads the content of the instance.
        /// </summary>
        /// <typeparam name="IdType">
        /// The data type of the parameter used as an identity value to load the instance.
        /// </typeparam>
        /// <param name="id">
        /// The <typeparamref name="IdType"/> data content used to identify the content to be loaded.
        /// </param>
        /// <returns>
        /// <b>true</b> if the load operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public new async Task<T?> LoadAsync<IdType>(IdType? id)
        {
            _entity = default(T);
            try
            {
                _entity = await PerformLoadAsync(id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                RecordException(ex);
                _entity = default;
            }

            return _entity;
        }
        /// <summary>
        /// Saves the current instance.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b> .
        /// </returns>
        public override bool Save()
        {
            bool success = false;
            try
            {
                if (_entity != null)
                    success = PerformSave(_entity);
            }
            catch (Exception ex)
            {
                RecordException(ex);
                success = false;
            }

            return success;
        }
        /// <summary>
        /// Saves the current instance.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b> .
        /// </returns>
        public override async Task<bool> SaveAsync()
        {
            bool success = false;
            try
            {
                if (_entity != null)
                    success = await PerformSaveAsync(_entity).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                RecordException(ex);
                success = false;
            }

            return success;
        }

        #endregion

        #region Private Methods / Functions
        /// <summary>
        /// Caches the property information for the underlying entity instance.
        /// </summary>
        private void CachePropertyInfo()
        {
            _propertyInfoCache?.Clear();
            _propertyInfoCache = new Dictionary<string, PropertyInfo>();

            try
            {
                PropertyInfo[] propertyList = typeof(T).GetProperties();

                foreach (PropertyInfo propertyInfo in propertyList)
                {
                    _propertyInfoCache.Add(propertyInfo.Name, propertyInfo);
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.LogException(ex);
            }
        }

        /// <summary>
        /// Gets the property information instance for the specified property.
        /// </summary>
        /// <returns>
        /// A <see cref="PropertyInfo"/> instance for the specified property, or <b>null</b> if the operation fails.
        /// </returns>
        private PropertyInfo? GetPropertyInfo(string propertyName)
        {
            PropertyInfo? propInfo = null;

            if (_propertyInfoCache != null && _propertyInfoCache.ContainsKey(propertyName))
            {
                propInfo = _propertyInfoCache[propertyName];
            }

            // If not yet created, reference and add it.
            if (propInfo == null)
            {
                if (_propertyInfoCache == null)
                    _propertyInfoCache = new Dictionary<string, PropertyInfo>();

                try
                {
                    propInfo = typeof(T).GetProperty(propertyName);
                    if (propInfo != null)
                    {
                        _propertyInfoCache.Add(propertyName, propInfo);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLog.LogException(ex);
                    propInfo = null;
                }
            }
            return propInfo;
        }
        #endregion
    }
}
