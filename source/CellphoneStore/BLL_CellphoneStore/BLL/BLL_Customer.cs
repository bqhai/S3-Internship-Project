using BLL_CellPhoneStore.Lib;
using DAL_CellPhoneStore.DAL;
using DAL_CellPhoneStore.Model;
using Model_CellphoneStore;
using Model_CellphoneStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_CellPhoneStore.BLL
{
    public class BLL_Customer
    {
        DAL_Customer dalCustomer = new DAL_Customer();
        EntityMapper<Customer, CustomerMapped> cusToCusMapped = new EntityMapper<Customer, CustomerMapped>();
        EntityMapper<CustomerMapped, Customer> cusMappedToCus = new EntityMapper<CustomerMapped, Customer>();
        public BLL_Customer()
        {

        }
        public bool AddNewCustomer(CustomerMapped customerMapped)
        {
            string lastCustomerID = dalCustomer.GetTheLastCustomerID();
            if (lastCustomerID != null)
            {
                try
                {
                    string customerID = Auto.CreateID("CUS", lastCustomerID);
                    customerMapped.CustomerID = customerID;
                    Customer cus = cusMappedToCus.Translate(customerMapped);
                    dalCustomer.AddNewCustomer(cus);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
        public bool UpdateCustomer(CustomerMapped customerMapped)
        {
            try
            {
                Customer cus = cusMappedToCus.Translate(customerMapped);
                dalCustomer.UpdateCustomer(cus);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public CustomerMapped GetCustomerByUsername(string username)
        {
            Customer customer = dalCustomer.GetCustomerByUsername(username);
            CustomerMapped customerMapped = cusToCusMapped.Translate(customer);
            return customerMapped;

        }
    }
}
