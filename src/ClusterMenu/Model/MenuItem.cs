namespace ClusterMenu.Model {
    public class MenuItem {

        public int IdMenuItem { get; set; } = -1;

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool Active { get; set; }

        public MenuItem() {
        }
    }
}
