using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BLL_CellPhoneStore.BLL;
namespace API_CellphoneStore.Controllers
{
    [RoutePrefix("api/API_User")]
    public class API_UserController : ApiController
    {
        BLL_Account bllAccount = new BLL_Account();
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
    }
}
