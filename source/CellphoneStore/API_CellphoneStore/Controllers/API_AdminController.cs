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
        BLL_Customer bllCustomer = new BLL_Customer();

        #region Employee Management
        [HttpGet]
        [Route("GetAllEmployee")]
        public List<EmployeeMapped> GetAllEmployee()
        {
            return bllEmployee.GetAllEmployee();
        }

        [HttpPost]
        [Route("AddNewEmployee")]
        public bool AddNewEmployee(EmployeeMapped employeeMapped)
        {
            return bllEmployee.AddNewEmployee(employeeMapped);
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public bool UpdateEmployee(EmployeeMapped employeeMapped)
        {
            return bllEmployee.UpdateEmployee(employeeMapped);
        }
        #endregion
        #region Customer Managerment
        [HttpGet]
        [Route("GetAllCustomer")]
        public List<CustomerMapped> GetAllCustomer()
        {
            return bllCustomer.GetAllCustomer();
        }
        #endregion
    }
}
