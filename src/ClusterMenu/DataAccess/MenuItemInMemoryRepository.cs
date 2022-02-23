using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ClusterMenu.Exceptions;
using ClusterMenu.Model;

namespace ClusterMenu.DataAccess {

    /// <summary>
    /// Implements an <see cref="IMenuItemRepository"/> with in-memory storage.
    /// </summary>
    public class MenuItemInMemoryRepository : IMenuItemRepository {

        private readonly Dictionary<int, MenuItem> _menuItems;
        private int _idCounter;

        public MenuItemInMemoryRepository() {
            _menuItems = new Dictionary<int, MenuItem>();
            _idCounter = 0;
        }

        public MenuItemInMemoryRepository(IDictionary<int, MenuItem> items) {
            _menuItems = new Dictionary<int, MenuItem>(items);
            _idCounter = items.Keys.Max();
        }

        /// <inheritdoc />
        public IEnumerable<MenuItem> GetAll() {
            return _menuItems.Values;
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DataAccessException"></exception>
        public int Insert(MenuItem item) {
            
            // validate model
            if (item == null) 
                throw new ArgumentNullException(nameof(item));
            if (item.IdMenuItem >= 0) {
                throw new DataAccessException("Item already have an assigned ID.");
            }
            
            // insert into memory
            item.IdMenuItem = GetNextId();
            _menuItems.Add(item.IdMenuItem, item);
            return item.IdMenuItem;
        }

        /// <inheritdoc />
        public void Update(MenuItem item) {

            // validate model
            if (item == null) 
                throw new ArgumentNullException(nameof(item));
            if (item.IdMenuItem < 0) {
                throw new DataAccessException("Item is missing an ID.");
            }

            // insert update into memory
            if (!_menuItems.ContainsKey(item.IdMenuItem)) {
                throw new DataAccessException("Cannot update. Item not found in the database");
            }
            _menuItems[item.IdMenuItem] = item;
        }

        /// <inheritdoc />
        public void DeleteById(int itemId) {
            if (!_menuItems.Remove(itemId)) {
                throw new DataAccessException("Could not locate the item in the database.");
            }
        }

        /// <summary>
        /// Generates IDs for each item inserted, simulating database primary key
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetNextId() => _idCounter++;
    }
}
