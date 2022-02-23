using System.Linq;
using ClusterMenu.DataAccess;
using ClusterMenu.Model;
using NUnit.Framework;

namespace ClusterMenu.Tests.DataAccess {

    [TestFixture]
    public class MenuItemsRepositoryTests {

        private IMenuItemRepository GetNewRepository() => new MenuItemInMemoryRepository();

        [Test]
        public void Test_Insert() {

            var repo = GetNewRepository();

            var itemToInsert = MenuItem.NewItem("French Fries", 15.00M);

            repo.Insert(itemToInsert);

            var allItems = repo.GetAll().ToArray(); // enumerate once

            Assert.That(allItems.Count(), Is.Not.GreaterThan(1), () => "Failed to insert item");

            Assert.That(allItems.First(), Is.EqualTo(itemToInsert), () => "Inserted and read items don't not match");
            // comparison above is being done by object base class, and thus its by reference
        }

        [Test]
        public void Test_Update() {

            var repo = GetNewRepository();

            var itemToInsert = MenuItem.NewItem("French Fries", 15.00M);
            
            repo.Insert(itemToInsert);

            var foundItem = repo.GetAll().First();

            // make a copy of the model, and change the price
            // (we need a copy because this may be in-memory object and thus is held by reference,
            // therefore updating it would update the in-memory model directly without the DataAccess layer)
            var updatedItem = new MenuItem() {
                IdMenuItem = foundItem.IdMenuItem,
                Active = foundItem.Active,
                Name = foundItem.Name,
                Price = 18.00M
            };
            
            // update, passing the entirely new model object, with matching ID
            repo.Update(updatedItem);

            foundItem = repo.GetAll().First();

            // assert new model has the correct id, name and value
            Assert.That(foundItem.IdMenuItem, Is.EqualTo(updatedItem.IdMenuItem));
            Assert.That(foundItem.Name, Is.EqualTo(updatedItem.Name));
            Assert.That(foundItem.Price, Is.EqualTo(18.00M));
        }

        [Test]
        public void Test_Delete() {
            
            var repo = GetNewRepository();

            var insertedId = repo.Insert(MenuItem.NewItem("French Fries", 15.00M));

            Assert.That(repo.GetAll().Count(), Is.EqualTo(1));

            repo.DeleteById(insertedId);

            Assert.That(repo.GetAll().Count(), Is.EqualTo(0));
            
        }
    }
}
