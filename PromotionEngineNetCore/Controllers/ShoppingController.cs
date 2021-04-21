using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromotionEngineNetCore.Helpers;
using PromotionEngineNetCore.Models;

namespace PromotionEngineNetCore.Controllers
{
    public class ShoppingController : Controller
    {
        private List<ShoppingCartModel> listShoppingCart;

        public ShoppingController()
        {
            listShoppingCart = new List<ShoppingCartModel>();
        }

        [HttpPost]
        public JsonResult Index(string itemId)
        {
            ShoppingCartModel shoppingCartModel = new ShoppingCartModel();
            ProductModel productModel = new ProductModel();
            if (SessionHelper.GetObjectFromJson<List<ShoppingCartModel>>(HttpContext.Session, "cartdata") != null)
            {
                listShoppingCart = SessionHelper.GetObjectFromJson<List<ShoppingCartModel>>(HttpContext.Session, "cartdata");
            }
            if (listShoppingCart.Any(x => x.ItemId == itemId))
            {
                shoppingCartModel = listShoppingCart.Single(x => x.ItemId == itemId);
                shoppingCartModel.ItemQuantity = shoppingCartModel.ItemQuantity + 1;
                shoppingCartModel.Total = shoppingCartModel.ItemQuantity * shoppingCartModel.ItemPrice;
            }
            else
            {
                var data = productModel.find(itemId);
                shoppingCartModel.ItemId = data.Id;
                shoppingCartModel.ItemName = data.ItemName;
                shoppingCartModel.ImageName = data.ItemImage;
                shoppingCartModel.ItemPrice = data.ItemPrice;
                shoppingCartModel.ItemQuantity = 1;
                shoppingCartModel.Total = data.ItemPrice;
                listShoppingCart.Add(shoppingCartModel);
            }

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cartdata", listShoppingCart);
            ViewBag.IsSuccess = true;
            return Json(data: new { Success = true });
        }

        [HttpGet]
        public ActionResult ShoppingCart()
        {
            List<ShoppingCartModel> cart = SessionHelper.GetObjectFromJson<List<ShoppingCartModel>>(HttpContext.Session, "cartdata");
            return View(cart);
        }

        [HttpGet]
        public IActionResult Remove(string id)
        {
            List<ShoppingCartModel> cart = SessionHelper.GetObjectFromJson<List<ShoppingCartModel>>(HttpContext.Session, "cartdata");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ItemId.Equals(id))
                {
                    cart.RemoveAt(i);
                }
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cartdata", cart);
            return RedirectToAction("ShoppingCart");
        }
    }
}