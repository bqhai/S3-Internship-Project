using CellphoneStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;
using Model_CellphoneStore;
using Newtonsoft.Json;

namespace CellphoneStore.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult AllProduct()
        {
            var url = "api/API_Product";
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse(url);
            response.EnsureSuccessStatusCode();
            List<ProductMapped> products = response.Content.ReadAsAsync<List<ProductMapped>>().Result;         
            ViewBag.Title = "All Products";
            return View(products);
        }
        public ActionResult ProductDetails()
        {
            return View();
        }
    }
}