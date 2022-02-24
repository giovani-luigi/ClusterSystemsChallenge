using System.Threading.Tasks;
using ClusterMenu.Services;

namespace ClusterMenu.ViewModel {
    public class JsonViewModel : ViewModelBase {
        
        private readonly IMenuService _menuService;

        public JsonViewModel(IMenuService menuService) {
            _menuService = menuService;

            Task.Run(() => {
                
                string jsonText = _menuService.GetMenuAsJson();

                Task.Delay(500); // just simulate some loading time...

                App.Current.Dispatcher.InvokeAsync(() => { // dispatch to UI thread
                    Json = jsonText;
                });
            });
        }

        public string Json { get; set; } = "Loading...";

    }
}
