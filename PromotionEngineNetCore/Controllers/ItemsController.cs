using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromotionEngineNetCore.Models;

namespace PromotionEngineNetCore.Controllers
{
    public class ItemsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ProductModel productModel = new ProductModel();
            ViewBag.products = productModel.GetAllItems();
            return View();
        }
    }
}