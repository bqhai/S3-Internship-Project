using CellphoneStore.Repository;
using Model_CellphoneStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CellphoneStore.Controllers
{
    public class HomeController : Controller
    {
        ServiceRepository serviceObj = new ServiceRepository();
        HttpResponseMessage response;
        public ActionResult Index()
        {
            var url = "api/API_Product/GetListHotSale";
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
                ViewBag.Title = "All Products";
                return View(productVersionMappeds);
            }
            return RedirectToAction("Error", "Home");
           
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}