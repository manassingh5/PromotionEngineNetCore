using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngineNetCore.Models
{
    public class ShoppingCartModel
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ImageName { get; set; }
        public int ItemQuantity { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal Total { get; set; }

    }
}
