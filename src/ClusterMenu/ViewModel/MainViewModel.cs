using ClusterMenu.DataAccess;
using ClusterMenu.Utils;

namespace ClusterMenu.ViewModel {
    public class MainViewModel : ViewModelBase {
        private readonly IMenuItemRepository _menuItemsRepository;
        
        public MainViewModel(IMenuItemRepository menuItemsRepository) {
            _menuItemsRepository = menuItemsRepository;
        }
    }
}
