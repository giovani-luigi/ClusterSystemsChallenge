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
            
            Task.Run(async () => {
                
                string jsonString = _menuService.GetMenuAsJson();

                await Task.Delay(1000); // just simulate some loading time...

                App.Current.Dispatcher.Invoke(() => Json = jsonString);
            });
        }

        public string Json {
            get => _json;
            set {
                Set(ref _json, value);
            }
        } 

    }
}
