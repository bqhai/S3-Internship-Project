using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BLL_CellPhoneStore.BLL;
using Model_CellphoneStore;

namespace API_CellphoneStore.Controllers
{
    [RoutePrefix("api/API_User")]
    public class API_UserController : ApiController
    {
        BLL_Account bllAccount = new BLL_Account();
        BLL_Customer bllCustomer = new BLL_Customer();
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        [Route("ProcessLogin/{username}/{password}")]
        public bool ProcessLogin(string username, string password)
        {
            return bllAccount.ProcessLogin(username, password);
        }
        [HttpGet]
        [Route("GetAccountType/{username}")]
        public int GetAccountType(string username)
        {
            return bllAccount.GetAccountType(username);
        }
        [HttpPost]
        [Route("AddNewAccount")]
        public bool AddNewAccount(AccountMapped accountMapped)
        {
            if (ModelState.IsValid)
            {
                bool addAccount = bllAccount.AddNewAccount(accountMapped);
                return addAccount;
            }
            return false;
        }
        [HttpPost]
        [Route("AddNewCustomer")]
        public bool AddNewCustomer(CustomerMapped customerMapped)
        {
            if (ModelState.IsValid)
            {
                bool addCustomer = bllCustomer.AddNewCustomer(customerMapped);
                return addCustomer;
            }
            return false;
        }
        [HttpGet]
        [Route("GetCustomerByUsername/{username}")]
        public CustomerMapped GetCustomerByUsername(string username)
        {
            return bllCustomer.GetCustomerByUsername(username);
        }

    }
}
