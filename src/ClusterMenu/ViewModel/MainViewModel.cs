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

        // backing field for observable properties
        private MenuItem _selectedItem;
        private string _searchText;
        private ObservableCollection<MenuItem> _listItems;
        private string _itemEditorName;
        private decimal _itemEditorPrice;
        private bool _itemEditorActive;
        private int _itemEditorId;

        public MainViewModel() : base(null) {
            // this is for design-time only
            _listItems = new ObservableCollection<MenuItem>();
        }
        
        public MainViewModel(IMenuService menuService, ILogger logger = null) : base(logger) {
            _menuService = menuService;
            
            // retrieve all menu items
            _listItems = new ObservableCollection<MenuItem>(_menuService.GetAllItems());
            _listItems.CollectionChanged += ListItemsOnCollectionChanged;

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
            set {
                Set(ref _selectedItem, value);
                if (value is null) {
                    ItemEditorId = -1;
                    ItemEditorName = string.Empty;
                    ItemEditorPrice = decimal.Zero;
                    ItemEditorActive = false;
                    return;
                }
                ItemEditorId = value.IdMenuItem;
                ItemEditorName = value.Name;
                ItemEditorPrice = value.Price;
                ItemEditorActive = value.Active;
            }
        }

        public int ItemEditorId {
            get => _itemEditorId;
            set => Set(ref _itemEditorId, value);
        }

        public string ItemEditorName {
            get => _itemEditorName;
            set => Set(ref _itemEditorName, value);
        }

        public decimal ItemEditorPrice {
            get => _itemEditorPrice;
            set => Set(ref _itemEditorPrice, value);
        }

        public bool ItemEditorActive {
            get => _itemEditorActive;
            set => Set(ref _itemEditorActive, value);
        }

        public ObservableCollection<MenuItem> ListItems {
            get => _listItems;
            set => Set(ref _listItems, value);
        }

        public string SearchText {
            get => _searchText;
            set {
                Set(ref _searchText, value);
                ReloadList(_searchText);
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
            var result = new AddView().ShowDialog();
            if (result.HasValue && result.Value) {
                ClearSearch();
            }
        }
        
        private void OnCommandShowJson(object obj) {
            new JsonView().ShowDialog();
        }

        private void OnCommandUpdate(object obj) {
            
            var itemToUpdate = new MenuItem {
                IdMenuItem = ItemEditorId,
                Active = ItemEditorActive,
                Name = ItemEditorName,
                Price = ItemEditorPrice,
            };

            Logger.LogInfo($"User is updating the menu item with ID={itemToUpdate.IdMenuItem}");

            try {
                _menuService.Update(itemToUpdate);
                ReloadList();
            } catch (ApplicationException ex) {
                Logger.LogError("Application exception", ex);
                MessageBox.Show(ex.Message);
            } catch (Exception e) {
                Logger.LogError("Error", e);
                MessageBox.Show("Error inserting into the database.");
            }

            // clear search criteria
            ClearSearch();

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
            
            // clear search criteria
            ClearSearch();
        }

        private void OnCommandExit() {
            
            Logger.LogInfo($"User requested application to exit");

            Application.Current.Shutdown();
        }

        #endregion

        private void ClearSearch() {
            SearchText = "";
            ReloadList();
        }

        private void ReloadList(string searchText = null) {
            if (string.IsNullOrWhiteSpace(searchText)) {
                ListItems = new ObservableCollection<MenuItem>(_menuService.GetAllItems());
                return;
            }
            ListItems = new ObservableCollection<MenuItem>(_menuService.GetAllItems().Where(x => x.Name.Contains(searchText)));
        }
        
        private void ClearSelection() {
            SelectedItem = null;
        }
    }
}
