using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngineNetCore.Models
{
    public class ShoppingViewModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemImage { get; set; }
        public decimal ItemPrice { get; set; }
        public string Description { get; set; }
    }
}
