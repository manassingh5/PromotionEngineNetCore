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
    public class PromotionController : Controller
    {
        [HttpGet]
        public IActionResult GetPromotions()
        {
            var listPromotion = SessionHelper.GetObjectFromJson<List<Promotion>>(HttpContext.Session, "promotiondata");
            ViewBag.Promotions = listPromotion;
            return View();
        }

        [HttpPost]
        public IActionResult GetPromotions(PromotionViewModel model, string addButton)
        {
            if (ModelState.IsValid)
            {
                List<Promotion> listPromotion = new List<Promotion>();
                Promotion promotion = new Promotion();
                if (SessionHelper.GetObjectFromJson<List<Promotion>>(HttpContext.Session, "promotiondata") != null)
                {
                    listPromotion = SessionHelper.GetObjectFromJson<List<Promotion>>(HttpContext.Session, "promotiondata");

                }
                promotion.ItemName = model.ItemName;
                promotion.ItemPrice = model.ItemPrice;
                promotion.ItemQuantity = model.ItemQuantity;
                promotion.PromotionId = listPromotion.Count + 1;
                listPromotion.Add(promotion);
                ViewBag.Promotions = listPromotion;
                SessionHelper.SetObjectAsJson(HttpContext.Session, "promotiondata", listPromotion);
            }
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public IActionResult DeletePromotions(int id)
        {
            List<Promotion> listPromotion = SessionHelper.GetObjectFromJson<List<Promotion>>(HttpContext.Session, "promotiondata");
            for (int i = 0; i < listPromotion.Count; i++)
            {
                if (listPromotion[i].PromotionId.Equals(id))
                {
                    listPromotion.RemoveAt(i);
                }
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "promotiondata", listPromotion);
            return RedirectToAction("GetPromotions");
        }

        [HttpPost]
        public IActionResult Remove(string name)
        {
            List<PromotionViewModel> promCart = SessionHelper.GetObjectFromJson<List<PromotionViewModel>>(HttpContext.Session, "promdata");
            for (int i = 0; i < promCart.Count; i++)
            {
                if (promCart[i].ItemName.Equals(name))
                {
                    promCart.RemoveAt(i);
                }
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "promdata", promCart);
            return RedirectToAction("GetPromotions");
        }

    }
}