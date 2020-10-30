using CellphoneStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;
using DAL_CellPhoneStore.Model;
using Newtonsoft.Json;

namespace CellphoneStore.Controllers
{
    public class ProductController : Controller
    {

        public ActionResult AllProduct()
        {
            var url = "https://localhost:44356/api/Product";
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse(url);
            response.EnsureSuccessStatusCode();
            List<Product> products = response.Content.ReadAsAsync<List<Product>>().Result;
            //var products = response.Content.ReadAsStringAsync().Result;
            //List<Product> listProduct = JsonConvert.DeserializeObject<List<Product>>(products);
            
            ViewBag.Title = "All Products";
            return View(products);
        }
    }
}