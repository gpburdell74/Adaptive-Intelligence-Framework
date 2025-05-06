using Adaptive.AspNet.Identity.Data;
using Adaptive.Intelligence.Shared;

namespace Adaptive.AspNet.Identity
{
    /// <summary>
    /// Represents a person entity in the data store.
    /// </summary>
    /// <seealso cref="DisposableObjectBase" />
    public class Person : DisposableObjectBase
    {
        #region Private Member Declarations

        /// <summary>
        /// The data access instance.
        /// </summary>
        private PersonDataAccess? _da;

        /// <summary>
        /// The data entity containing the person's information.
        /// </summary>
        private PersonDto? _dto;
        #endregion

        #region Constructor / Dispose Methods        
        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        /// <remarks>
        /// This is the default constructor.
        /// </remarks>
        public Person()
        {
            _dto = new PersonDto();
            _da = new PersonDataAccess();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="PersonDto"/> data transfer object containing the data entity for the person.
        /// </param>
        public Person(PersonDto dto)
        {
            _dto = dto;
            _da = new PersonDataAccess();
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
                _dto?.Dispose();
                _da?.Dispose();
            }

            _dto = null;
            _da = null;
            base.Dispose(disposing);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the email address for the person.
        /// </summary>
        /// <value>
        /// A string containing the email address value, or <b>null</b>.
        /// </value>
        public string? EmailAddress { get => _dto?.EmailAddress; set => _dto!.EmailAddress = value; }

        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        /// <value>
        /// A string containing the first name value, or <b>null</b>.
        /// </value>
        public string? FirstName { get => _dto?.FirstName; set => _dto!.FirstName = value; }

        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        /// <value>
        /// A string containing the last name value, or <b>null</b>.
        /// </value>
        public string? LastName { get => _dto?.LastName; set => _dto!.LastName = value; }

        /// <summary>
        /// Gets or sets the middle name/initial of the person.
        /// </summary>
        /// <value>
        /// A string containing the middle name value, or <b>null</b>.
        /// </value>
        public string? MiddleName { get => _dto?.MiddleName; set => _dto!.MiddleName = value; }

        /// <summary>
        /// Gets or sets the nickname or preferred name for the person.
        /// </summary>
        /// <value>
        /// A string containing the nickname value, or <b>null</b>.
        /// </value>
        public string? Nickname { get => _dto?.Nickname; set => _dto!.Nickname = value; }

        /// <summary>
        /// Gets or sets the ID of the person record.
        /// </summary>
        /// <value>
        /// A <see cref="Guid" /> specifying the person ID value, or <b>null</b> if not yet created.
        /// </value>
        public Guid? PersonId { get => _dto?.PersonId; set => _dto!.PersonId = value; }

        /// <summary>
        /// Gets or sets the name suffix value for the person.
        /// </summary>
        /// <value>
        /// A string containing the name suffix value, or <b>null</b>.
        /// </value>
        public string? Suffix { get => _dto?.Suffix; set => _dto!.Suffix = value; }

        /// <summary>
        /// Gets or sets the title for the person.
        /// </summary>
        /// <value>
        /// A string containing the title value, or <b>null</b>.
        /// </value>
        public string? Title { get => _dto?.Title; set => _dto!.Title = value; }

        /// <summary>
        /// Gets or sets the ID of the related user record.
        /// </summary>
        /// <value>
        /// A <see cref="Guid" /> specifying the user ID value, or <b>null</b> if not yet created.
        /// </value>
        public Guid? UserId { get => _dto?.UserId; set => _dto!.UserId = value; }
        #endregion

        #region
        #endregion

        #region
        #endregion

        #region
        #endregion

    }
}
