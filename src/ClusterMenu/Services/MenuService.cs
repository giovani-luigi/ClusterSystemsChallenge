using System;
using System.Collections.ObjectModel;
using System.Linq;
using ClusterMenu.DataAccess;
using ClusterMenu.Exceptions;
using ClusterMenu.Model;
using ClusterMenu.Validators;
using Newtonsoft.Json;

namespace ClusterMenu.Services {

    /// <summary>
    /// Implements a service layer for the Menu.
    /// </summary>
    /// <remarks>
    /// This layer mediates repository access, providing validation and other features
    /// not available directly from the repository layer.
    /// </remarks>
    public class MenuService : IMenuService {
        
        private readonly IMenuItemRepository _menuItemRepository;

        private readonly ObservableCollection<MenuItem> _cache;

        public MenuService(IMenuItemRepository menuItemRepository) {
            _menuItemRepository = menuItemRepository;
            _cache = new ObservableCollection<MenuItem>(_menuItemRepository.GetAll());
        }

        /// <inheritdoc />
        public ObservableCollection<MenuItem> GetAllItems() {
            return _cache;
        }

        /// <inheritdoc />
        public string GetMenuAsJson() {
            return JsonConvert.SerializeObject(_cache);
        }

        /// <inheritdoc />
        public int Insert(MenuItem item) {
            if (item == null) throw new ArgumentNullException(nameof(item));

            // validate model
            var validation = new MenuItemValidator().Validate(item);
            if (!validation.IsValid) {
                throw new ModelValidationException(validation.Errors.First().ErrorMessage);
            }

            int index = _menuItemRepository.Insert(item);
            _cache.Add(item); // we add in cache after repository insertion was successful (i.e. skip if throws)
            return index;
        }

        /// <inheritdoc />
        public void Update(MenuItem item) {
            if (item == null) throw new ArgumentNullException(nameof(item));

            // validate model
            var validation = new MenuItemValidator().Validate(item);
            if (!validation.IsValid) {
                throw new ModelValidationException(validation.Errors.First().ErrorMessage);
            }

            // find the first item that matches ID, remove and replace with new one
            for (int i = 0; i < _cache.Count; i++) {
                if (_cache[i].IdMenuItem == item.IdMenuItem) {
                    // remove and add the item in the colletion to trigger its change notification
                    _cache.RemoveAt(i);
                    _cache.Insert(i, item);
                    return;
                }
            }
        }

        /// <inheritdoc />
        public void DeleteById(int itemId) {

            // first remove from repository layer
            _menuItemRepository.DeleteById(itemId);

            // now on the cached, find the first item that matches ID and remove
            for (int i = 0; i < _cache.Count; i++) {
                if (_cache[i].IdMenuItem == itemId) {
                    _cache.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
