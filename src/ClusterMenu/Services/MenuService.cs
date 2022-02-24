using System;
using System.Collections.Generic;
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
        
        public MenuService(IMenuItemRepository menuItemRepository) {
            _menuItemRepository = menuItemRepository;
        }

        /// <inheritdoc />
        public IEnumerable<MenuItem> GetAllItems() {
            return _menuItemRepository.GetAll();
        }

        /// <inheritdoc />
        public string GetMenuAsJson() {
            return JsonConvert.SerializeObject(_menuItemRepository.GetAll(), Formatting.Indented);
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

            _menuItemRepository.Update(item);
        }

        /// <inheritdoc />
        public void DeleteById(int itemId) {

            // remove from repository layer
            _menuItemRepository.DeleteById(itemId);
        }
    }
}
