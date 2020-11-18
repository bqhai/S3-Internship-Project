using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL_CellPhoneStore.BLL;
using Model_CellphoneStore;

namespace API_CellphoneStore.Controllers
{
    [RoutePrefix("api/API_Admin")]
    public class API_AdminController : ApiController
    {
        BLL_Employee bllEmployee = new BLL_Employee();
        [HttpGet]
        [Route("GetAllEmployee")]
        public List<EmployeeMapped> GetAllEmployee()
        {
            return bllEmployee.GetAllEmployee();
        }
    }
}
