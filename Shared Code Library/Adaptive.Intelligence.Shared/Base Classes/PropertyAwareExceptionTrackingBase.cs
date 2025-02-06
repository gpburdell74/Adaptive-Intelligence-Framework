using System.ComponentModel;

namespace Adaptive.Intelligence.Shared
{
    /// <summary>
    /// Provides the base implementation for classes that can notify subscribers when a property value changes that
    /// can also track its own exceptions.
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    public abstract class PropertyAwareExceptionTrackingBase : ExceptionTrackingBase, INotifyPropertyChanged
    {
        #region Public Events		
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Event Methods		
        /// <summary>
        /// Called when a property value is changed.
        /// </summary>
        /// <param name="propertyName">
        /// A string containing the name of the property.
        /// </param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// Raises the <see cref="E:PropertyChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        #endregion
    }
}
