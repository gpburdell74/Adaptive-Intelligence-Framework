using Adaptive.Intelligence.Shared.Logging;
using System.ComponentModel.DataAnnotations;

namespace Adaptive.Intelligence.Shared
{
    /// <summary>
    /// Provides a base implementation for read-only business objects.
    /// </summary>
    /// <seealso cref="PropertyAwareBase" />
    /// <seealso cref="DisposableObjectBase"/>
    /// <seealso cref="IValidatableObject"/>
    public abstract class ReadOnlyBusinessBase : PropertyAwareBase
    {
        #region Abstract / Protected Methods
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
        /// <b>true</b> if the load operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        protected abstract bool PerformLoad<IdType>(IdType? id);

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
        /// <b>true</b> if the load operation is successful; otherwise, returns <b>false</b>.
        /// </returns>
        protected abstract Task<bool> PerformLoadAsync<IdType>(IdType? id);

        /// <summary>
        /// Records, logs, or otherwise stores the exception information when an exception is caught.
        /// </summary>
        /// <param name="ex">
        /// The <see cref="Exception"/> instance that was caught.
        /// </param>
        protected virtual void RecordException(Exception ex)
        {
            ExceptionLog.LogException(ex);
        }
        #endregion

        #region Public Methods / Functions
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
        public virtual bool Load<IdType>(IdType? id)
        {
            bool success;
            try
            {
                success = PerformLoad(id);
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
        public virtual async Task<bool> LoadAsync<IdType>(IdType? id)
        {
            bool success;
            try
            {
                success = await PerformLoadAsync(id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                RecordException(ex);
                success = false;
            }

            return success;
        }
        #endregion
    }
}
