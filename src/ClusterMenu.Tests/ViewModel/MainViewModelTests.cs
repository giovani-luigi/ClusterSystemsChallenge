using System.Linq;
using ClusterMenu.Services;
using ClusterMenu.Tests.DataAccess.Mock;
using ClusterMenu.ViewModel;
using NUnit.Framework;

namespace ClusterMenu.Tests.ViewModel {
    
    [TestFixture]
    public class MainViewModelTests {

        [Test]
        [TestCase("Cal", ExpectedResult = "Calamari")]
        [TestCase("Corn", ExpectedResult = "Mini Corn Dogs")]
        [TestCase("New York", ExpectedResult = "New York Strip")]
        [TestCase("Alfredo", ExpectedResult = "Broccoli Alfredo")]
        public string Test_Search_FirstResult_Name(string searchText) {

            var vm = new MainViewModel(new MenuService(new MenuItemTestRepository()));

            vm.SearchText = searchText;

            var found = vm.ListItems.First();

            Assert.That(found, Is.Not.Null, $"Found no object for search criteria '{searchText}'");

            return found.Name;
        }

    }
}
