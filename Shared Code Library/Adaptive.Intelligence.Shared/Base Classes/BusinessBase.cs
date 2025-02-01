using Adaptive.Intelligence.Shared.Logging;
using System.ComponentModel.DataAnnotations;

namespace Adaptive.Intelligence.Shared
{
    /// <summary>
    /// Provides a base implementation for business objects.
    /// </summary>
    /// <seealso cref="PropertyAwareBase" />
    /// <seealso cref="DisposableObjectBase"/>
    /// <seealso cref="IValidatableObject"/>
    public abstract class BusinessBase : PropertyAwareBase, IValidatableObject
    {
        #region Public Properties
        /// <summary>
        /// Gets a value indicating whether the data on the business object is valid data.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsValid => Validate();
        #endregion

        #region Abstract / Protected Methods
        /// <summary>
        /// When overridden in a derived class, performs the operation to delete the current instance.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b> .
        /// </returns>
        public abstract bool PerformDelete();
        /// <summary>
        /// When overridden in a derived class, performs the operation to delete the current instance.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b> .
        /// </returns>
        public abstract Task<bool> PerformDeleteAsync();
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
        /// When overridden in a derived class, performs the operation to save / store the current instance.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b> .
        /// </returns>
        protected abstract bool PerformSave();
        /// <summary>
        /// When overridden in a derived class, performs the operation to save / store the current instance.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b> .
        /// </returns>
        protected abstract Task<bool> PerformSaveAsync();
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
        /// Performs the operation to delete the current instance.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b> .
        /// </returns>
        public virtual bool Delete()
        {
            bool success;
            try
            {
                success = PerformDelete();
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
        public virtual async Task<bool> DeleteAsync()
        {
            bool success;
            try
            {
                success = await PerformDeleteAsync().ConfigureAwait(false);
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
        /// <summary>
        /// Saves the current instance.
        /// </summary>
        /// <returns>
        /// <b>true</b> if the operation is successful; otherwise, returns <b>false</b> .
        /// </returns>
        public virtual bool Save()
        {
            bool success;
            try
            {
                success = PerformSave();
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
        public virtual async Task<bool> SaveAsync()
        {
            bool success;
            try
            {
                success = await PerformSaveAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                RecordException(ex);
                success = false;
            }

            return success;
        }

        /// <summary>
        /// Performs a simple data validation on the instance.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool Validate()
        {
            IEnumerable<ValidationResult> resultList = Validate(new ValidationContext(this));
            return !resultList.Any();
        }
        /// <summary>
        /// Determines whether the specified object is valid.
        /// </summary>
        /// <param name="validationContext">
        /// The <see cref="ValidationContext"/> instance, or <b>null</b>.
        /// </param>
        /// <returns>
        /// A collection that holds failed-validation information, if any.
        /// </returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
        #endregion
    }
}
