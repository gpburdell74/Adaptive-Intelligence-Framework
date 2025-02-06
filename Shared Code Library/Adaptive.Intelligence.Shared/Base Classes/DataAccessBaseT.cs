namespace Adaptive.Intelligence.Shared
{
    /// <summary>
    /// Provides a base definition for standard data access methods for Mobile Service objects where
    /// only one record is involved.
    /// </summary>
    /// <remarks>
    /// This is generally for the settings objects, where there is just one entry per customer.
    /// </remarks>
    /// <typeparam name="T">
    /// The data type of the object being read form and saved to the data store.
    /// </typeparam>
    public abstract class DataAccessBase<T> : DataAccessBase
        where T : BusinessBase
    {
        #region Protected Abstract Methods		
        /// <summary>
        /// When overridden in a derived class, performs the delete operation on the data source.
        /// </summary>
        /// <param name="instance">
        /// The <typeparamref name="T"/> business object instance to be deleted.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        protected abstract bool PerformDelete(T instance);
        /// <summary>
        /// When overridden in a derived class, performs the delete operation on the data source.
        /// </summary>
        /// <param name="instance">
        /// The <typeparamref name="T"/> business object instance to be deleted.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        protected abstract Task<bool> PerformDeleteAsync(T instance);
        /// <summary>
        /// When overridden in a derived class, performs the operation to load a business object instance by ID value. 
        /// </summary>
        /// <typeparam name="IdType">
        /// The data type of the value used as the identity for the instance to be loaded.
        /// </typeparam>
        /// <param name="idType">
        /// The value that is the identity of the instance to be loaded.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T"/> if successful; otherwise, returns <b>null</b>.
        /// </returns>
        protected abstract T? PerformLoad<IdType>(IdType idType);
        /// <summary>
        /// When overridden in a derived class, performs the operation to load a business object instance by ID value. 
        /// </summary>
        /// <typeparam name="IdType">
        /// The data type of the value used as the identity for the instance to be loaded.
        /// </typeparam>
        /// <param name="idType">
        /// The value that is the identity of the instance to be loaded.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T"/> if successful; otherwise, returns <b>null</b>.
        /// </returns>
        protected abstract Task<T?> PerformLoadAsync<IdType>(IdType idType);
        /// <summary>
        /// When overridden in a derived class, performs the save/update operation on the data source.
        /// </summary>
        /// <param name="instance">
        /// The <typeparamref name="T"/> business object instance to be saved/updated.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        protected abstract bool PerformSave(T instance);
        /// <summary>
        /// When overridden in a derived class, performs the save/update operation on the data source.
        /// </summary>
        /// <param name="instance">
        /// The <typeparamref name="T"/> business object instance to be saved/updated.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        protected abstract Task<bool> PerformSaveAsync(T instance);
        #endregion

        #region Public Methods / Functions
        /// <summary>
        /// Deletes the specified business object definition from the data store.
        /// </summary>
        /// <param name="item">
        /// The <typeparamref name="T"/> instance to be deleted.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public bool Delete(T? item)
        {
            LastOperationSuccess = false;
            LastOperationError = null;

            bool success = false;
            try
            {
                if (item != null)
                    success = PerformDelete(item);
            }
            catch (Exception ex)
            {
                RecordException(ex);
            }

            LastOperationSuccess = success;
            return success;
        }
        /// <summary>
        /// Deletes the specified business object definition from the data store.
        /// </summary>
        /// <param name="item">
        /// The <typeparamref name="T"/> instance to be deleted.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        public virtual async Task<bool> DeleteAsync(T? item)
        {
            LastOperationSuccess = false;
            LastOperationError = null;

            bool success = false;
            if (item != null)
            {
                OnAsyncQueryStarted(nameof(DeleteAsync));
                try
                {

                    success = await PerformDeleteAsync(item).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    RecordException(ex);
                }
                OnAsyncQueryCompleted(nameof(DeleteAsync));
            }
            LastOperationSuccess = success;
            return success;
        }
        /// <summary>
        /// Gets the instance from data source.
        /// </summary>
        /// <param name="id">
        /// A value serving as the identity value used to load the instance.
        /// </param>
        /// <returns>
        /// A <see cref="BusinessBase"/>-derived instance.
        /// </returns>
        public T? LoadItem<IdType>(IdType id)
        {
            LastOperationSuccess = false;
            LastOperationError = null;

            T? newItem;

            try
            {
                newItem = PerformLoad(id);
                LastOperationSuccess = true;
            }
            catch (Exception ex)
            {
                RecordException(ex);
                newItem = null;
                LastOperationSuccess = false;
            }

            return newItem;
        }
        /// <summary>
        /// Gets the instance from the data source.
        /// </summary>
        /// <param name="id">
        /// A value serving as the identity value used to load the instance.
        /// </param>
        /// <returns>
        /// A <see cref="BusinessBase"/>-derived instance.
        /// </returns>
        public async Task<T?> LoadItemAsync<IdType>(IdType id)
        {
            LastOperationSuccess = false;
            LastOperationError = null;

            T? newItem;
            OnAsyncQueryStarted(nameof(LoadItemAsync));
            try
            {
                newItem = await PerformLoadAsync(id).ConfigureAwait(false);
                LastOperationSuccess = true;
            }
            catch (Exception ex)
            {
                RecordException(ex);
                newItem = null;
                LastOperationSuccess = false;
            }
            OnAsyncQueryCompleted(nameof(LoadItemAsync));
            return newItem;
        }
        /// <summary>
        /// Saves the specified item to the data store.
        /// </summary>
        /// <param name="item">
        /// The business object instance to be saved.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation completes successfully; otherwise, returns <b>false</b>.
        /// </returns>
        public bool Save(T? item)
        {
            LastOperationSuccess = false;
            LastOperationError = null;

            bool success = false;
            if (item != null)
            {
                try
                {
                    success = PerformSave(item);
                }
                catch (Exception ex)
                {
                    RecordException(ex);
                }
            }

            LastOperationSuccess = success;
            return success;
        }
        /// <summary>
        /// Saves the specified item to the data store.
        /// </summary>
        /// <param name="item">
        /// The business object instance to be saved.
        /// </param>
        /// <returns>
        /// <b>true</b> if the operation completes successfully; otherwise, returns <b>false</b>.
        /// </returns>
        public async Task<bool> SaveAsync(T? item)
        {
            LastOperationSuccess = false;
            LastOperationError = null;

            bool success = false;
            if (item != null)
            {
                OnAsyncQueryStarted(nameof(SaveAsync));
                try
                {
                    success = await PerformSaveAsync(item).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    RecordException(ex);
                }
                OnAsyncQueryCompleted(nameof(SaveAsync));
            }

            LastOperationSuccess = success;
            return success;
        }
        #endregion
    }
}