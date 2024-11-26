using Adaptive.Intelligence.Shared;

namespace Adaptive.Bible.Security
{
	/// <summary>
	/// Provides a container for credentials used to access secure Bible files.
	/// </summary>
	/// <seealso cref="DisposableObjectBase" />
	public sealed class BibleLogin : DisposableObjectBase
	{
		#region Private Member Declarations
		private Adaptive.Intelligence.Shared.Security.SecureString? _primary;
		private Adaptive.Intelligence.Shared.Security.SecureString? _secondary;
		private Adaptive.Intelligence.Shared.Security.SecureString? _tertiary;
		#endregion

		#region Constructor / Dispose Methods		
		/// <summary>
		/// Initializes a new instance of the <see cref="BibleLogin"/> class.
		/// </summary>
		/// <remarks>
		/// This is the default constructor.
		/// </remarks>
		public BibleLogin()
		{

		}
		/// <summary>
		/// Initializes a new instance of the <see cref="BibleLogin"/> class.
		/// </summary>
		/// <param name="primary">
		/// The primary security code value.
		/// </param>
		/// <param name="secondary">
		/// The secondary security code value.
		/// </param>
		/// <param name="tertiary">
		/// The tertiary security code value.
		/// </param>
		public BibleLogin(string primary, string secondary, string tertiary)
		{
			Primary = primary;
			Secondary = secondary;
			Tertiary = tertiary;
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
				_primary?.Dispose();
				_secondary?.Dispose();
				_tertiary?.Dispose();
			}
			_primary = null;
			_secondary = null;
			_tertiary = null;
#pragma warning disable S1215 // "GC.Collect" should not be called
			GC.Collect();
#pragma warning restore S1215 // "GC.Collect" should not be called
			base.Dispose(disposing);
		}
		#endregion

		#region Public Properties		
		/// <summary>
		/// Gets or sets the value of the primary security code.
		/// </summary>
		/// <value>
		/// A string containing the value of the primary security code, or <b>null</b>.
		/// </value>
		public string? Primary
		{
			get
			{
				if (_primary == null)
					return null;
				else
					return _primary.Value;
			}
			set
			{
				if (_primary == null)
					_primary = new Intelligence.Shared.Security.SecureString(1024, value);
				else
					_primary.Value = value;
			}
		}
		/// <summary>
		/// Gets or sets the value of the primary security code.
		/// </summary>
		/// <value>
		/// A string containing the value of the primary security code, or <b>null</b>.
		/// </value>
		public string? Secondary
		{
			get
			{
				if (_secondary == null)
					return null;
				else
					return _secondary.Value;
			}
			set
			{
				if (_secondary == null)
					_secondary = new Intelligence.Shared.Security.SecureString(1024, value);
				else
					_secondary.Value = value;
			}
		}
		/// <summary>
		/// Gets or sets the value of the tertiary security code.
		/// </summary>
		/// <value>
		/// A string containing the value of the tertiary security code, or <b>null</b>.
		/// </value>
		public string? Tertiary
		{
			get
			{
				if (_tertiary == null)
					return null;
				else
					return _tertiary.Value;
			}
			set
			{
				if (_tertiary == null)
					_tertiary = new Intelligence.Shared.Security.SecureString(1024, value);
				else
					_tertiary.Value = value;
			}
		}
		#endregion
	}
}
