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
	/// <typeparam name="T">
	/// The data type of the underlying entity.
	/// </typeparam>
	public abstract class BusinessBase<T> : PropertyAwareBase, IValidatableObject
	{
		#region Private Member Declarations		
		/// <summary>
		/// The entity containing the raw data.
		/// </summary>
		private T? _entity;
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
			_entity = Activator.CreateInstance<T>();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="BusinessBase{T}"/> class.
		/// </summary>
		/// <param name="entity">The entity.</param>
		protected BusinessBase(T entity)
		{
			_entity = entity;
		}
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><b>true</b> to release both managed and unmanaged resources;
		/// <b>false</b> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
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
		protected T? Entity => _entity;
		#endregion

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
		protected abstract T? PerformLoad<IdType>(IdType? id);
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
		public virtual async Task<bool> DeleteAsync()
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
		public virtual T? Load<IdType>(IdType? id)
		{
			_entity = default(T);
			try
			{
				_entity = PerformLoad(id);
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
		public virtual async Task<T?> LoadAsync<IdType>(IdType? id)
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
		public virtual bool Save()
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
		public virtual async Task<bool> SaveAsync()
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
