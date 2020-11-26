using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL_CellPhoneStore.BLL;
using Model_CellphoneStore;
using API_CellphoneStore.TokenAPI;

namespace API_CellphoneStore.Controllers
{
    [RoutePrefix("api/API_Admin")]
    public class API_AdminController : ApiController
    {
        BLL_Employee bllEmployee = new BLL_Employee();
        BLL_Customer bllCustomer = new BLL_Customer();
        BLL_Product bllProduct = new BLL_Product();
        BLL_ProductVersion bllProductVersion = new BLL_ProductVersion();

        #region Employee Management
        [HttpGet]
        [Route("GetAllEmployee/{username}/{token}")]
        public List<EmployeeMapped> GetAllEmployee(string username, string token)
        {
            string tokenUsername = TokenManager.ValidateToken(token);
            if (username.Equals(tokenUsername))
            {
                return bllEmployee.GetAllEmployee();
            }
            return null;
        }
        //[HttpGet]
        //[Route("GetAllEmployee/{username}")]
        //public List<EmployeeMapped> GetAllEmployee(string username)
        //{
        //    string token = Request.Headers.GetValues("Token").ToString();
        //    string tokenUsername = TokenManager.ValidateToken(token);
        //    if (username.Equals(tokenUsername))
        //    {
        //        return bllEmployee.GetAllEmployee();
        //    }
        //    return null;
        //}


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
        #region Product Management
        [HttpGet]
        [Route("GetAllProduct")]
        public List<ProductInfoMapped> GetAllProduct()
        {
            return bllProduct.GetAllProduct();
        }
        [HttpGet]
        [Route("GetListProductVersionByProductID/{productID}")]
        public List<ProductVersionMapped> GetListProductVersionByProductID(string productID)
        {
            return bllProductVersion.GetListProductVersionByProductID(productID);
        }
        [HttpPut]
        [Route("UpdateProductVersion")]
        public bool UpdateProductVersion(ProductVersionMapped productVersionMapped)
        {
            return bllProductVersion.UpdateProductVersion(productVersionMapped);
        }
        #endregion
    }
}
