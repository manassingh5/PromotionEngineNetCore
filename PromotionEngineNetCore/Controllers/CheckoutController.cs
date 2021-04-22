using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromotionEngineNetCore.Entities;
using PromotionEngineNetCore.Helpers;
using PromotionEngineNetCore.Models;

namespace PromotionEngineNetCore.Controllers
{
    public class CheckoutController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<CheckoutModel> listBill = new List<CheckoutModel>();
            List<ShoppingCartModel> cart = SessionHelper.GetObjectFromJson<List<ShoppingCartModel>>(HttpContext.Session, "cartdata");
            List<Promotion> listPromotion = SessionHelper.GetObjectFromJson<List<Promotion>>(HttpContext.Session, "promotiondata");
            int counter = 0;
            if (listPromotion.Any(x => x.ItemName.Contains("&")))
            {
                var listPromotionItemName = listPromotion.Where(x => x.ItemName.Contains("&")).Select(x => new { CombinedItemName = x.ItemName, CombinedItemPrice = x.ItemPrice, CombinedQuantity = x.ItemQuantity }).FirstOrDefault();
                var splitName = listPromotionItemName.CombinedItemName.Split('&');
                var combiledtotalPrice = listPromotionItemName.CombinedItemPrice;
                foreach (var name in splitName)
                {
                    if (cart.Any(x => x.ItemName == name))
                    {
                        counter++;
                    }
                }
                if (counter >= 2)
                {
                    listBill.Add(new CheckoutModel() { ItemName = listPromotionItemName.CombinedItemName, TotalPrice = combiledtotalPrice, TotalQuantity = listPromotionItemName.CombinedQuantity });
                }
                else
                {
                    var element = cart.FirstOrDefault(x => x.ItemName == splitName[0]);
                    listBill.Add(new CheckoutModel() { ItemName = element.ItemName, TotalPrice = element.ItemPrice, TotalQuantity = element.ItemQuantity });
                }
            }

            var data = from a in listPromotion join b in cart on a.ItemName equals b.ItemName select new { promiq = a.ItemQuantity, promPrice = a.ItemPrice, b.ItemName, b.ItemQuantity, b.ItemPrice };
            foreach (var item in data)
            {
                string name = item.ItemName;
                int quantity = item.ItemQuantity;
                int promQuantity = item.promiq;
                decimal promPrice = item.promPrice;
                decimal totalPrice = 0;
                int totalQuantity = 0;
                while (quantity > promQuantity)
                {
                    totalPrice = totalPrice + item.promPrice;
                    promQuantity = promQuantity + promQuantity;
                }
                if (quantity == promQuantity)
                {
                    totalPrice = promPrice;
                    totalQuantity = quantity;
                    listBill.Add(new CheckoutModel() { ItemName = name, TotalPrice = totalPrice, TotalQuantity = totalQuantity });
                    continue;
                }
                totalPrice = totalPrice + item.ItemPrice;
                totalQuantity = quantity;

                listBill.Add(new CheckoutModel() { ItemName = name, TotalPrice = totalPrice, TotalQuantity = totalQuantity });
            }
            return View(listBill.OrderBy(x => x.ItemName));
        }
    }
}