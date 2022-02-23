using System.Collections.Generic;
using ClusterMenu.DataAccess;
using ClusterMenu.Model;

namespace ClusterMenu.Tests.DataAccess.Mock {
    public class MenuItemTestRepository : MenuItemInMemoryRepository {

        private static Dictionary<int, MenuItem> GenerateFakeItems() {
            return new Dictionary<int, MenuItem>() {
                { 1, new MenuItem { IdMenuItem = 1, Active = true, Name = "French Fries", Price = 4.00M } } ,
                { 2, new MenuItem { IdMenuItem = 2, Active = true, Name = "Chips & French Onion Dip", Price = 9.00M } } ,
                { 3, new MenuItem { IdMenuItem = 3, Active = true, Name = "Cheese Curds", Price = 9.00M } } ,
                { 4, new MenuItem { IdMenuItem = 4, Active = true, Name = "Battered Onion Rings", Price = 8.00M } } ,
                { 5, new MenuItem { IdMenuItem = 5, Active = true, Name = "Soft Pretzel Bites", Price = 9.00M } } ,
                { 6, new MenuItem { IdMenuItem = 6, Active = true, Name = "Chicken Tenders", Price = 9.00M } } ,
                { 7, new MenuItem { IdMenuItem = 7, Active = true, Name = "Mini Corn Dogs", Price = 7.00M } } ,
                { 8, new MenuItem { IdMenuItem = 8, Active = true, Name = "Cheese and Fruit Plate", Price = 12.00M } } ,
                { 9, new MenuItem { IdMenuItem = 9, Active = true, Name = "Spinach and Artichoke Dip", Price = 11.00M } } ,
                { 10, new MenuItem { IdMenuItem = 10, Active = true, Name = "Cubed Tenderloin", Price = 16.00M } } ,
                { 11, new MenuItem { IdMenuItem = 11, Active = true, Name = "Crab Cakes", Price = 13.00M } } ,
                { 12, new MenuItem { IdMenuItem = 12, Active = true, Name = "Calamari", Price = 12.00M } } ,
                { 13, new MenuItem { IdMenuItem = 13, Active = true, Name = "Jumbo Shrimp Cocktail", Price = 16.00M } } ,
                { 14, new MenuItem { IdMenuItem = 14, Active = true, Name = "Bacon Wrapped Scallops", Price = 16.00M } } ,
                { 15, new MenuItem { IdMenuItem = 15, Active = true, Name = "Seafood Sampler", Price = 27.00M } } ,
                { 16, new MenuItem { IdMenuItem = 16, Active = true, Name = "Special Nachos", Price = 12.00M } } ,
                { 17, new MenuItem { IdMenuItem = 17, Active = true, Name = "Tacos", Price = 9.00M } } ,
                { 18, new MenuItem { IdMenuItem = 18, Active = true, Name = "Tenderloin Filet", Price = 39.00M } } ,
                { 19, new MenuItem { IdMenuItem = 19, Active = true, Name = "Rib Eye Steak", Price = 32.00M } } ,
                { 20, new MenuItem { IdMenuItem = 20, Active = true, Name = "New York Strip", Price = 28.00M } } ,
                { 21, new MenuItem { IdMenuItem = 21, Active = true, Name = "Cedar Plank Salmon", Price = 28.00M } } ,
                { 22, new MenuItem { IdMenuItem = 22, Active = true, Name = "Pan Fried Walleye", Price = 23.00M } } ,
                { 23, new MenuItem { IdMenuItem = 23, Active = true, Name = "Pecan Chicken", Price = 25.00M } } ,
                { 24, new MenuItem { IdMenuItem = 24, Active = true, Name = "Broccoli Alfredo", Price = 20.00M } } ,
                { 25, new MenuItem { IdMenuItem = 25, Active = true, Name = "Portabella Ravioli", Price = 22.00M } } ,
                { 26, new MenuItem { IdMenuItem = 26, Active = true, Name = "Cajun Pasta", Price = 22.00M } } ,
            };
        }

        public MenuItemTestRepository() : base(GenerateFakeItems()) {
        }
    }
}
