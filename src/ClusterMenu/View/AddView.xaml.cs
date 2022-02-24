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
            var viewModel = new AddViewModel(App.Current.Services.GetInstance<IMenuService>());
            DataContext = viewModel;
            viewModel.ViewCloseRequested += (sender, dialogResult) => {
                DialogResult = dialogResult;
                Close();
            };
        }
    }
}
