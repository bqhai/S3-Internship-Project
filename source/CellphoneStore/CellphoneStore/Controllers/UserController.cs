using CellphoneStore.Repository;
using Microsoft.Ajax.Utilities;
using Model_CellphoneStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CellphoneStore.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        ServiceRepository serviceObj = new ServiceRepository();
        HttpResponseMessage response;
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult ProcessLogin(AccountMapped accountMapped)
        //{          
        //    var url = "api/API_User/ProcessLogin/" + accountMapped.Username + "/" + accountMapped.Password;
        //    response = serviceObj.GetResponse(url);
        //    response.EnsureSuccessStatusCode();
        //    var resultLogin = response.Content.ReadAsAsync<bool>().Result;
        //    if (resultLogin)
        //    {
                
        //    }
        //}
        //public int GetAccountType(string username)
        //{
        //    var url = "api/API_User/GetAccountType"
        //}
    }
}