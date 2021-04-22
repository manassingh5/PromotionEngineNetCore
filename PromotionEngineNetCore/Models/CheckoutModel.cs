using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngineNetCore.Models
{
    public class CheckoutModel
    {
        public string ItemName { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
    }
}
