using System.Windows;
using ClusterMenu.DataAccess;
using ClusterMenu.ViewModel;

namespace ClusterMenu.View {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window {
        public MainView() {
            InitializeComponent();
            DataContext = new MainViewModel(App.Current.Services.GetInstance<IMenuItemRepository>());
        }
    }
}
