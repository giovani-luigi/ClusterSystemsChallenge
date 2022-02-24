using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClusterMenu.Utils {

    /// <summary>
    /// Base class for classes that require <see cref="INotifyPropertyChanged"/> implementation.
    /// </summary>
    /// <remarks>
    /// This class provides useful methods to simplify MVVM implementation.
    /// </remarks>
    public abstract class ObservableObject : INotifyPropertyChanged {

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        protected ObservableObject() {
        }

        /// <summary>
        /// Sets the value of a property with a backing field.
        /// </summary>
        /// <remarks>
        /// Use this method to properly trigger <see cref="PropertyChanged"/> event.
        /// </remarks>
        /// <param name="backingField">The reference to the property's backing field</param>
        /// <param name="newValue">The new value to assign to it</param>
        /// <param name="validator">Any validator function. The value will be validated before being assigned.
        /// If validation fails, no assignment is done, and the method returns false.</param>
        /// <returns>
        /// Returns true if assignment was successful, false if new value is identical to previous, or failed validation.
        /// </returns>
        public bool Set<T>(ref T backingField, T newValue, Func<T, T, bool> validator = null, [CallerMemberName] string propertyName = null) {

            // parameters validation
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            // if the new value is the same as previous value, ignore
            if (EqualityComparer<T>.Default.Equals(backingField, newValue)) {
                return false;
            }

            // if value changed but is not valid, ignore
            if (validator != null && !validator(backingField, newValue)) {
                return false;
            }

            backingField = newValue;
            RaisePropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Raise PropertyChanged event
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
