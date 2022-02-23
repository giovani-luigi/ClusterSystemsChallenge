using System.Collections.Generic;
using ClusterMenu.Model;

namespace ClusterMenu.DataAccess {
    public interface IMenuItemRepository {

        /// <summary>
        /// Retrieve all items from menu.
        /// </summary>
        IEnumerable<MenuItem> GetAll();

        /// <summary>
        /// Insert an item.
        /// </summary>
        void Insert(MenuItem item);

        /// <summary>
        /// Update an item
        /// </summary>
        void Update(MenuItem item);

        /// <summary>
        /// Delete an item by its database ID
        /// </summary>
        void DeleteById(int itemId);
        
    }
}
