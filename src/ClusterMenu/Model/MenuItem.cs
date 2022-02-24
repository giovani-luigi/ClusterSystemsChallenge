using ClusterMenu.Utils;
using Newtonsoft.Json;

namespace ClusterMenu.Model {

    [JsonObject(MemberSerialization.OptIn)]
    public class MenuItem : ObservableObject {

        private int _idMenuItem = -1;
        private string _name;
        private decimal _price;
        private bool _active;

        [JsonProperty("idMenuItem")]
        public int IdMenuItem {
            get => _idMenuItem;
            set => Set(ref _idMenuItem, value);
        }

        [JsonProperty("name")]
        public string Name {
            get => _name;
            set => Set(ref _name, value);
        }

        [JsonProperty("price")]
        public decimal Price {
            get => _price;
            set => Set(ref _price, value);
        }

        [JsonProperty("active")]
        public bool Active {
            get => _active;
            set => Set(ref _active, value);
        }

        public MenuItem() {
        }

        public static MenuItem NewItem(string name, decimal price, bool active = true) {
            return new MenuItem() {
                Name = name,
                Price = price,
                Active = active,
            };
        }

    }
}
