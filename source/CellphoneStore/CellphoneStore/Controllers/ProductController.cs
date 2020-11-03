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
        HttpResponseMessage response;
        public ActionResult AllProduct(int pageIndex = 1, int pageSize = 15)
        {
            var url = "api/API_ProductVersion/GetAllProductVersion";        
            response = serviceObj.GetResponse(url);
            response.EnsureSuccessStatusCode();
            List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;         
            ViewBag.Title = "All Products";          
            return View(productVersionMappeds.ToPagedList(pageIndex, pageSize));
        }
        public ActionResult ProductDetails(string productVersionID)
        {
            var url = "api/API_ProductVersion/GetProductVersionByID/" + productVersionID;
            response = serviceObj.GetResponse(url);
            response.EnsureSuccessStatusCode();
            var productVersionMapped = response.Content.ReadAsAsync<ProductVersionMapped>().Result;
            ViewBag.Title = "Product details";
            return View(productVersionMapped);
        }
        public ActionResult GetAllBrand()
        {
            var url = "api/API_Brand/GetAllBrand";
            response = serviceObj.GetResponse(url);
            response.EnsureSuccessStatusCode();
            List<BrandMapped> brandMappeds = response.Content.ReadAsAsync<List<BrandMapped>>().Result;
            return PartialView(brandMappeds);
        }
        public ActionResult ListProductVersion(string productID)
        {
            var url = "api/API_ProductVersion/GetListProductVersionByProductID/" + productID;
            response = serviceObj.GetResponse(url);
            response.EnsureSuccessStatusCode();
            List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
            return PartialView(productVersionMappeds);
        }
    }
}