using System;
using System.Windows.Input;

namespace ClusterMenu.Utils {

    /// <summary>
    /// Provides a default implementation for <see cref="ICommand"/> interface.
    /// </summary>
    /// <remarks>
    /// This class provides useful methods to simplify MVVM implementation.
    /// </remarks>
    public class Command : ICommand {

        /// <inheritdoc />
        /// <remarks>
        /// Handlers are held by weak references. Objects that listen for this event should keep a strong reference
        /// to their event handler to avoid it being garbage collected. This can be accomplished by having a private
        /// field and assigning the handler as the value before or after attaching to this event.
        /// </remarks>
        public event EventHandler CanExecuteChanged {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
            /* forward the handlers to the CommandManager.RequerySuggested from System.Windows.Input
             namespace which detects conditions that might change the ability of a command to execute.
             Remark: The CommandManager holds onto the handlers as weak references */
        }

        private readonly Action<object> _methodToExecute;
        private readonly Func<object, bool> _canExecuteEvaluator;

        /// <summary>
        /// Create a command that receives an <see cref="Action"/> to be executed.
        /// </summary>
        /// <param name="methodToExecute">The action to execute</param>
        /// <param name="canExecuteEvaluator">A function that returns whether the action can be executed.</param>
        public Command(Action<object> methodToExecute, Func<object, bool> canExecuteEvaluator = null) {
            _methodToExecute = methodToExecute ?? throw new ArgumentNullException(nameof(methodToExecute));
            _canExecuteEvaluator = canExecuteEvaluator;
        }

        /// <summary>
        /// Create a command that receives an <see cref="Action"/> to be executed.
        /// </summary>
        /// <param name="methodToExecute">The action to execute</param>
        public Command(Action methodToExecute) : this(o => methodToExecute()) {
        }

        /// <inheritdoc />
        public virtual bool CanExecute(object parameter) {
            if (_canExecuteEvaluator is null) return true;
            return _canExecuteEvaluator.Invoke(parameter);
        }

        /// <inheritdoc />
        public virtual void Execute(object parameter) {
            _methodToExecute.Invoke(parameter);
        }

    }
}
