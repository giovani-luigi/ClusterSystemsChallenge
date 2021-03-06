using System;
using System.Windows;
using System.Windows.Input;
using ClusterMenu.Exceptions;
using ClusterMenu.Model;
using ClusterMenu.Services;
using ClusterMenu.Utils;

namespace ClusterMenu.ViewModel {
    public class AddViewModel : ViewModelBase {
        
        private readonly IMenuService _menuService;

        private MenuItem _item;

        public AddViewModel(IMenuService menuService, ILogger logger = null) : base(logger) {
            _menuService = menuService;
            
            Item = MenuItem.NewItem("", 0M);

            CommandAdd = new Command(OnCommandAdd);
            CommandCancel = new Command(OnCommandCancel);
        }
        
        #region Properties

        public MenuItem Item {
            get => _item;
            set => Set(ref _item, value);
        }

        #endregion

        #region Commands

        public ICommand CommandAdd { get; }

        public ICommand CommandCancel { get; }

        #endregion

        #region Command Handlers

        /// <summary>
        /// Handler invoked for Command: Add new menu item
        /// </summary>
        private void OnCommandAdd(object o) {

            var item = Item;
            Logger.LogInfo($"Adding menu item to the system. Name={item.Name}, Price={item.Price}");

            try {
                _menuService.Insert(item);
            } catch (ModelValidationException ex) {
                MessageBox.Show(ex.Message);
                return;
            } catch (ApplicationException ex) {
                Logger.LogError("Application exception", ex);
                MessageBox.Show(ex.Message);
                return;
            } catch (Exception e) {
                Logger.LogError("Error", e);
                MessageBox.Show("Error inserting into the database.");
                return;
            }

            base.RequestViewToClose(true);
        }

        /// <summary>
        /// Handler invoked for Command: Cancel add new item, and close
        /// </summary>
        private void OnCommandCancel() {
            base.RequestViewToClose(false);
        }

        #endregion
        
    }
}
