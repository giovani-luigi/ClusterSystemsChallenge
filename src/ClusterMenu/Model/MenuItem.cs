namespace ClusterMenu.Model {
    public class MenuItem {

        public int IdMenuItem { get; set; } = -1;

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool Active { get; set; }

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
