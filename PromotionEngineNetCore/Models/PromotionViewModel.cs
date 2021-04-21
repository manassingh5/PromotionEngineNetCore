using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngineNetCore.Models
{
    public class PromotionViewModel
    {
        public int PromotionId { get; set; }
        public string ItemName { get; set; }
        public int ItemQuantity { get; set; }
        public int ItemPrice { get; set; }
    }
}
