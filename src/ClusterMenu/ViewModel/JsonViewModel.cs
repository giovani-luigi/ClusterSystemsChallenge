using System.Threading.Tasks;
using System.Windows;
using ClusterMenu.Services;
using ClusterMenu.Utils;

namespace ClusterMenu.ViewModel {
    public class JsonViewModel : ViewModelBase {
        
        private readonly IMenuService _menuService;
        private string _json;
        
        public JsonViewModel(IMenuService menuService, ILogger logger = null) : base(logger) {
            _menuService = menuService;

            _json = "Loading...";
            
            // populate the JSON text from the Menu Service inside a task that takes some Delay to load... just for demo purposes.
            Task.Run(async () => {
                
                string jsonString = _menuService.GetMenuAsJson();

                await Task.Delay(1000); // just simulate some loading time...

                App.Current.Dispatcher.Invoke(() => Json = jsonString); // dispatch to UI thread the update in the property
            });
        }

        public string Json {
            get => _json;
            set => Set(ref _json, value);
        } 

    }
}
