using System;
using ClusterMenu.Utils;

namespace ClusterMenu.ViewModel {
    /// <summary>
    /// Provides a base for all ViewModel classes.
    /// This base class add common services such as Logging mechanism for example.
    /// </summary>
    public class ViewModelBase : ObservableObject {

        public event EventHandler<bool?> ViewCloseRequested;

        /// <summary>
        /// Provides loggind mechanisms to the ViewModel
        /// </summary>
        protected ILogger Logger { get; set; }

        public ViewModelBase() {
            Logger = App.Current.Services.GetInstance<ILogger>();
        }

        protected virtual void RequestViewToClose(bool? dialogResult) {
            ViewCloseRequested?.Invoke(this, dialogResult);
        }
    }
}
