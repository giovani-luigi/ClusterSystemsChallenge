using System.Windows;
using ClusterMenu.Services;
using ClusterMenu.ViewModel;

namespace ClusterMenu.View {

    /// <summary>
    /// Interaction logic for AddView.xaml
    /// </summary>
    public partial class AddView : Window {
        public AddView() {
            InitializeComponent();
            DataContext = new AddViewModel(this, App.Current.Services.GetInstance<IMenuService>());
        }
    }
}
