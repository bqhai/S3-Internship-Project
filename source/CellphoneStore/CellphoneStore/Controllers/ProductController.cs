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
using System.Collections;
using System.Threading.Tasks;

namespace CellphoneStore.Controllers
{
    public class ProductController : Controller
    {
        ServiceRepository serviceObj = new ServiceRepository();
        HttpResponseMessage response;
        public ActionResult AllProduct(int pageIndex = 1, int pageSize = 15)
        {
            var url = "api/API_Product/GetAllProductVersion";
            response = serviceObj.GetResponse(url);
            response.EnsureSuccessStatusCode();
            List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
            ViewBag.Title = "All Products";
            ViewData["State"] = "AllProduct";
            return View(productVersionMappeds.ToPagedList(pageIndex, pageSize));
        }
        public ActionResult ProductDetails(string productVersionID)
        {
            var url = "api/API_Product/GetProductVersionByID/" + productVersionID;
            response = serviceObj.GetResponse(url);
            response.EnsureSuccessStatusCode();
            ViewBag.Title = "Product details";
            var productVersionInfo = response.Content.ReadAsAsync<ProductVersionInfoMapped>().Result;
            return View(productVersionInfo);

        }
        public ActionResult GetAllBrand()
        {
            var url = "api/API_Product/GetAllBrand";
            response = serviceObj.GetResponse(url);
            response.EnsureSuccessStatusCode();
            List<BrandMapped> brandMappeds = response.Content.ReadAsAsync<List<BrandMapped>>().Result;
            return PartialView(brandMappeds);
        }
        public ActionResult ListProductVersion(string productID)
        {
            var url = "api/API_Product/GetListProductVersionByProductID/" + productID;
            response = serviceObj.GetResponse(url);
            response.EnsureSuccessStatusCode();
            List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
            return PartialView(productVersionMappeds);
        }
        public ActionResult ProductIntroduce(string productID)
        {
            var url = "api/API_Product/GetProductIntroduceByID/" + productID;
            response = serviceObj.GetResponse(url);
            response.EnsureSuccessStatusCode();
            ProductIntroduceMapped productIntroduceMapped = response.Content.ReadAsAsync<ProductIntroduceMapped>().Result;
            return PartialView(productIntroduceMapped);
        }
        public ActionResult SortProductVersionByPrice(string sortOption, int pageIndex = 1, int pageSize = 15)
        {
            var url = "api/API_Product/GetAllProductVersion";
            response = serviceObj.GetResponse(url);
            response.EnsureSuccessStatusCode();
            List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
            if(sortOption == "desPrice")
            {
                productVersionMappeds = productVersionMappeds.OrderByDescending(prdv => prdv.Price).ToList();
                ViewData["Option"] = "desPrice";
            }
            else
            {
                productVersionMappeds = productVersionMappeds.OrderBy(prdv => prdv.Price).ToList();
                ViewData["Option"] = "ascPrice";
            }
            ViewData["State"] = "SortByPrice";
            return View("~/Views/Product/AllProduct.cshtml", productVersionMappeds.ToPagedList(pageIndex, pageSize));
        }
        public ActionResult FilterProductVersionByRAM(string ram, int pageIndex = 1, int pageSize = 15)
        {
            var url = "api/API_Product/FilterProductVersionByRAM/" + ram;
            response = serviceObj.GetResponse(url);
            List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
            ViewData["State"] = "FilterByRam";
            ViewData["Option"] = ram;
            return View("~/Views/Product/AllProduct.cshtml", productVersionMappeds.ToPagedList(pageIndex, pageSize));
        }
        public ActionResult FilterProductVersionByROM(string rom, int pageIndex = 1, int pageSize = 15)
        {
            var url = "api/API_Product/FilterProductVersionByROM/" + rom;
            response = serviceObj.GetResponse(url);
            List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
            ViewData["State"] = "FilterByRom";
            ViewData["Option"] = rom;
            return View("~/Views/Product/AllProduct.cshtml", productVersionMappeds.ToPagedList(pageIndex, pageSize));
        }
        public ActionResult FilterProductVersionByOS(string os, int pageIndex = 1, int pageSize = 15)
        {
            var url = "api/API_Product/FilterProductVersionByOS/" + os;
            response = serviceObj.GetResponse(url);
            List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
            ViewData["State"] = "FilterByOs";
            ViewData["Option"] = os;
            return View("~/Views/Product/AllProduct.cshtml", productVersionMappeds.ToPagedList(pageIndex, pageSize));
        }
        public ActionResult FilterProductVersionByScreenSize(double minScreenSize, double maxScreenSize, int pageIndex = 1, int pageSize = 15)
        {
            var url = "api/API_Product/FilterProductVersionByScreenSize/" + minScreenSize + "/" + maxScreenSize + "/";
            response = serviceObj.GetResponse(url);
            List<ProductVersionMapped> productVersionMappeds = response.Content.ReadAsAsync<List<ProductVersionMapped>>().Result;
            ViewData["State"] = "FilterByScreenSize";
            ViewData["Option"] = minScreenSize;
            ViewData["Option2"] = maxScreenSize;
            return View("~/Views/Product/AllProduct.cshtml", productVersionMappeds.ToPagedList(pageIndex, pageSize));
        }
    }
}