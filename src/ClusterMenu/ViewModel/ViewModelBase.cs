using System.Windows;
using ClusterMenu.Utils;

namespace ClusterMenu.ViewModel {
    /// <summary>
    /// Provides a base for all ViewModel classes.
    /// This base class add common services such as Logging mechanism for example.
    /// </summary>
    public class ViewModelBase : ObservableObject {

        /// <summary>
        /// Provides loggind mechanisms to the ViewModel
        /// </summary>
        protected ILogger Logger { get; set; }

        public ViewModelBase() {
            //Logger = App.Current.Services.GetService();
        }
    }
}
