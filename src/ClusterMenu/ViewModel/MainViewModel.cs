using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ClusterMenu.DataAccess;
using ClusterMenu.Model;
using ClusterMenu.Utils;

namespace ClusterMenu.ViewModel {

    public class MainViewModel : ViewModelBase {
        
        private readonly IMenuItemRepository _menuItemsRepository;

        public MainViewModel() {
            MenuItems = new ObservableCollection<MenuItem>();
        }
        
        public MainViewModel(IMenuItemRepository menuItemsRepository) {
            _menuItemsRepository = menuItemsRepository;
            
            // retrieve all menu items
            MenuItems = new ObservableCollection<MenuItem>(_menuItemsRepository.GetAll());

            // create commands
            CommandAdd = new Command(OnCommandAdd);
            CommandDelete = new Command(OnCommandDelete, x => SelectedItem != null);
            CommandUpdate = new Command(OnCommandUpdate, x => SelectedItem != null);
            CommandShowJson = new Command(OnCommandShowJson);
        }
        
        #region Properties

        public MenuItem SelectedItem { get; set; }

        public ObservableCollection<MenuItem> MenuItems { get; private set; }

        #endregion

        #region Commands

        public ICommand CommandShowJson { get; }

        public ICommand CommandAdd { get; }
        
        public ICommand CommandDelete { get; }

        public ICommand CommandUpdate { get; }

        #endregion

        #region CommandHandlers

        private void OnCommandAdd(object obj) {
            var item = MenuItem.NewItem("Giovani", 12m);
            try {
                _menuItemsRepository.Insert(item);
            } catch (Exception e) {
                Logger.LogError("Error", e);
                MessageBox.Show("Error inserting into the database.");
                return;
            }
            MenuItems.Add(item);
        }

        private void OnCommandShowJson(object obj) {

        }

        private void OnCommandUpdate(object obj) {
            
        }

        private void OnCommandDelete(object obj) {
            
        }

        #endregion

    }
}
