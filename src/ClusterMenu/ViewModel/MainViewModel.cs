using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ClusterMenu.Model;
using ClusterMenu.Services;
using ClusterMenu.Utils;
using ClusterMenu.View;

namespace ClusterMenu.ViewModel {

    public class MainViewModel : ViewModelBase {
        
        private readonly IMenuService _menuService;
        private MenuItem _selectedItem;

        public MainViewModel() {
            // this is for design-time only
            MenuItems = new ObservableCollection<MenuItem>();
        }
        
        public MainViewModel(IMenuService menuService) {
            _menuService = menuService;
            
            // retrieve all menu items
            MenuItems = _menuService.GetAllItems();

            // create commands
            CommandAdd = new Command(OnCommandAdd);
            CommandDelete = new Command(OnCommandDelete, x => SelectedItem != null);
            CommandUpdate = new Command(OnCommandUpdate, x => SelectedItem != null);
            CommandShowJson = new Command(OnCommandShowJson);
        }
        
        #region Properties

        public MenuItem SelectedItem {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }

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
            
            Logger.LogInfo($"Adding menu item to the system. Name={item.Name}, Price={item.Price}");

            try {
                _menuService.Insert(item);
            } catch (ApplicationException ex) {
                Logger.LogError("Application exception", ex);
                MessageBox.Show(ex.Message);
            } catch (Exception e) {
                Logger.LogError("Error", e);
                MessageBox.Show("Error inserting into the database.");
            }
        }

        private void OnCommandShowJson(object obj) {
            new JsonView().ShowDialog();
        }

        private void OnCommandUpdate(object obj) {

            var item = SelectedItem;
            if (item == null) return;

            Logger.LogInfo($"User is updating the menu item with ID={item.IdMenuItem}");

            try {
                _menuService.Update(item);
            } catch (ApplicationException ex) {
                Logger.LogError("Application exception", ex);
                MessageBox.Show(ex.Message);
            } catch (Exception e) {
                Logger.LogError("Error", e);
                MessageBox.Show("Error inserting into the database.");
            }
        }

        private void OnCommandDelete(object obj) {
            
            var item = SelectedItem;
            if (item == null) return;
            
            Logger.LogInfo($"User is deleting the menu item with Name={item.Name}");

            try {
                _menuService.Update(item);
            } catch (ApplicationException ex) {
                Logger.LogError("Application exception", ex);
                MessageBox.Show(ex.Message);
            } catch (Exception e) {
                Logger.LogError("Error", e);
                MessageBox.Show("Error inserting into the database.");
            }
        }

        #endregion

    }
}
