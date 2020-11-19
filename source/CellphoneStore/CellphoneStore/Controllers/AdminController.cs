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
    public class AdminController : Controller
    {
        ServiceRepository serviceObj = new ServiceRepository();
        HttpResponseMessage response;
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Employee()
        {
            var url = "api/API_Admin/GetAllEmployee";
            response = serviceObj.GetResponse(url);
            if (response.IsSuccessStatusCode)
            {
                List<EmployeeMapped> employeeMappeds = response.Content.ReadAsAsync<List<EmployeeMapped>>().Result;
                return View(employeeMappeds);
            }
            return RedirectToAction("Index", "Admin");
        }
    }
}