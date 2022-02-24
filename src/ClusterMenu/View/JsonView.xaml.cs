using System.Windows;
using ClusterMenu.Services;
using ClusterMenu.ViewModel;

namespace ClusterMenu.View {

    /// <summary>
    /// Interaction logic for JsonView.xaml
    /// </summary>
    public partial class JsonView : Window {
        public JsonView() {
            InitializeComponent();
            DataContext = new JsonViewModel(App.Current.Services.GetInstance<IMenuService>());
        }
    }
}
