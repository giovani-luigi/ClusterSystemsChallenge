using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
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
        private string _searchText;
        private ObservableCollection<MenuItem> _listItems;

        public MainViewModel() {
            // this is for design-time only
            ListItems = new ObservableCollection<MenuItem>();
        }
        
        public MainViewModel(Window view, IMenuService menuService) {
            _menuService = menuService;
            
            // retrieve all menu items
            ListItems = _menuService.GetAllItems();
            ListItems.CollectionChanged += ListItemsOnCollectionChanged;

            // create commands
            CommandAdd = new Command(OnCommandAdd);
            CommandDelete = new Command(OnCommandDelete, x => SelectedItem != null);
            CommandUpdate = new Command(OnCommandUpdate, x => SelectedItem != null);
            CommandShowJson = new Command(OnCommandShowJson);
            CommandExit = new Command(OnCommandExit);
        }

        private void ListItemsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (SelectedItem != null) {
                if (e.NewItems.Cast<MenuItem>().All(x => x.IdMenuItem != SelectedItem.IdMenuItem)) {
                    ClearSelection();
                }
            }
        }
        
        #region Bound Properties

        public MenuItem SelectedItem {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }

        public ObservableCollection<MenuItem> ListItems {
            get => _listItems;
            private set {
                Set(ref _listItems, value);
            }
        }

        public string SearchText {
            get => _searchText;
            set {
                Set(ref _searchText, value);
                FilterItems(_searchText);
            }
        }
        
        #endregion

        #region Commands

        public ICommand CommandShowJson { get; }

        public ICommand CommandAdd { get; }
        
        public ICommand CommandDelete { get; }

        public ICommand CommandUpdate { get; }

        public ICommand CommandExit { get; }

        #endregion

        #region CommandHandlers

        private void OnCommandAdd(object obj) {
            new AddView().ShowDialog();
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

        private void OnCommandExit() {
            
            Logger.LogInfo($"User requested application to exit");

            Application.Current.Shutdown();
        }

        #endregion

        private void FilterItems(string searchText) {

            ListItems = new ObservableCollection<MenuItem>(_menuService.GetAllItems().Where(x => x.Name.Contains(searchText)));

            /*
            // repopulate the list of items with only matching items
            ListItems.Clear();
            foreach (var item in _menuService.GetAllItems().Where(x => x.Name.Contains(searchText))) {
                ListItems.Add(item); // this needs to be optimized. Create an observable collection that has AddRange()
            }
            */
        }
        
        private void ClearSelection() {
            SelectedItem = MenuItem.NewItem("", decimal.Zero);
        }
    }
}
