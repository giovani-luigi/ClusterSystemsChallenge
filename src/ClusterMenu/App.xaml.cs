using System.Windows;
using ClusterMenu.DataAccess;
using ClusterMenu.Services;
using ClusterMenu.Utils;
using SimpleInjector;

namespace ClusterMenu {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        
        /// <summary>
        /// Get the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Services' container, to be used for resolving Dependencies
        /// </summary>
        public Container Services { get; }

        public App() {
            InitializeComponent();
            Services = ConfigureServices();
        }
        
        private Container ConfigureServices() {
            
            var container = new Container();

            container.Register<ILogger, ConsoleLogger>(Lifestyle.Singleton);

            // for in-memory repository, we need it to be singleton, for persisted storage use transient instead
            container.Register<IMenuItemRepository, MenuItemInMemoryRepository>(Lifestyle.Singleton);
            
            // since the service is caching data, we need to make it a singleton
            container.Register<IMenuService, MenuService>(Lifestyle.Singleton);
            
            return container;
        }

    }
}
