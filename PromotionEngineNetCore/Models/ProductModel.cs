using PromotionEngineNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngineNetCore.Models
{
    public class ProductModel
    {
        public List<Product> products { get; set; }
        public List<Product> GetAllItems()
        {
            products = new List<Product> { new Product()
            {
                Id = "1" , ItemName = "A", ItemPrice = 50, ItemImage = "A.jpg", ItemDescription= "Apples"
            },
            new Product()
            {
                Id = "2" , ItemName = "B", ItemPrice = 30, ItemImage = "B.jpg", ItemDescription= "Ball"
            },
            new Product()
            {
                Id = "3" , ItemName = "C", ItemPrice = 20, ItemImage = "C.jpg", ItemDescription = "Cap"
            },
            new Product()
            {
                Id = "4" , ItemName = "D", ItemPrice = 15, ItemImage = "D.jpg", ItemDescription="Dog (Toy)"
            },
            new Product()
            {
                Id = "5" , ItemName = "E", ItemPrice = 10, ItemImage = "E.jpg", ItemDescription="Elephant (Toy)"
            }
            };

            return products;
        }

        public Product find(string id)
        {
            List<Product> products = GetAllItems();
            var prod = products.Where(x => x.Id == id).FirstOrDefault();
            return prod;
        }
    }
}
