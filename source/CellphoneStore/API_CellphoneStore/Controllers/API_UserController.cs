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
        BLL_Address bllAddress = new BLL_Address();
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

        [HttpGet]
        [Route("AccountAlreadyExists/{username}")]
        public bool AccountAlreadyExists(string username)
        {
            return bllAccount.AccountAlreadyExists(username);
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

        [HttpPut]
        [Route("UpdateCustomer")]
        public bool UpdateCustomer(CustomerMapped customerMapped)
        {
            if (ModelState.IsValid)
            {
                bool updateCustomer = bllCustomer.UpdateCustomer(customerMapped);
                return updateCustomer;
            }
            return false;
        }

        [HttpPut]
        [Route("ChangePassword")]
        public int ChangePassword(AccountMapped accountMapped)
        {
            if (ModelState.IsValid)
            {
                int resultChangePassword = bllAccount.ChangePassword(accountMapped);
                return resultChangePassword;
            }
            return 404;
        }

        [HttpGet]
        [Route("GetProvinces")]
        public List<ProvinceMapped> GetProvinces()
        {
            return bllAddress.GetProvinces();
        }

        [HttpGet]
        [Route("GetDistrictsByProvinceID/{provinceID}")]
        public List<DistrictMapped> GetDistrictsByProvinceID(int provinceID)
        {
            return bllAddress.GetDistrictsByProvinceID(provinceID);
        }

        [HttpGet]
        [Route("GetWardsByDistrictID/{districtID}")]
        public List<WardMapped> GetWardsByDistrictID(int districtID)
        {
            return bllAddress.GetWardsByDistrictID(districtID);
        }

    }
}
