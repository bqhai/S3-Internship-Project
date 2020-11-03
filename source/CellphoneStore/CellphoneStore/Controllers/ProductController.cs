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
using PagedList;
using PagedList.Mvc;

namespace CellphoneStore.Controllers
{
    public class ProductController : Controller
    {
        ServiceRepository serviceObj = new ServiceRepository();
        public ActionResult AllProduct(int pageIndex = 1, int pageSize = 15)
        {
            var url = "api/API_ProductVersion";        
            HttpResponseMessage response = serviceObj.GetResponse(url);
            response.EnsureSuccessStatusCode();
            List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;         
            ViewBag.Title = "All Products";          
            return View(productVersionMappeds.ToPagedList(pageIndex, pageSize));
        }
        public ActionResult ProductDetails()
        {
            return View();
        }
        public ActionResult GetAllBrand()
        {
            var url = "api/API_Brand";
            HttpResponseMessage response = serviceObj.GetResponse(url);
            response.EnsureSuccessStatusCode();
            List<BrandMapped> brands = response.Content.ReadAsAsync<List<BrandMapped>>().Result;
            return PartialView(brands);
        }
    }
}