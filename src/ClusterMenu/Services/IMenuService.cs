using System.Collections.Generic;
using ClusterMenu.Model;

namespace ClusterMenu.Services {

    /// <summary>
    /// This class mediates the access to the data layer and also provides useful methods
    /// that can be consumed directly from the ViewModel
    /// </summary>
    public interface IMenuService {

        /// <summary>
        /// Get all the items in the database
        /// </summary>
        IEnumerable<MenuItem> GetAllItems();

        /// <summary>
        /// Get all menu items from the database as a JSON file
        /// </summary>
        string GetMenuAsJson();
        
        /// <summary>
        /// Insert an item.
        /// </summary>
        /// <remarks>
        /// Notice that the item gets its ID updated by this method upon successful insertion
        /// </remarks>
        /// <returns>
        /// Returns the ID of the inserted item
        /// </returns>
        int Insert(MenuItem item);

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
