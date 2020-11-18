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
        EntityMapper<Customer, CustomerMapped> convertToCusMapped = new EntityMapper<Customer, CustomerMapped>();
        EntityMapper<CustomerMapped, Customer> convertToCus = new EntityMapper<CustomerMapped, Customer>();
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
                    string customerID = AutoGen.CreateID("CUS", lastCustomerID);
                    customerMapped.CustomerID = customerID;
                    Customer cus = convertToCus.Translate(customerMapped);
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
        public bool UpdateCustomerInfo(CustomerMapped customerMapped)
        {
            try
            {
                Customer cus = convertToCus.Translate(customerMapped);
                dalCustomer.UpdateCustomerInfo(cus);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateCustomerAddress(CustomerMapped customerMapped)
        {
            try
            {
                Customer cus = convertToCus.Translate(customerMapped);
                dalCustomer.UpdateCustomerAddress(cus);
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
            CustomerMapped customerMapped = convertToCusMapped.Translate(customer);
            return customerMapped;

        }
        public CustomerMapped GetCustomerByEmail(string email)
        {
            Customer customer = dalCustomer.GetCustomerByEmail(email);
            CustomerMapped customerMapped = convertToCusMapped.Translate(customer);
            return customerMapped;
        }
        public bool AddResetPasswordCode(CustomerMapped customerMapped)
        {
            try
            {
                Customer cus = convertToCus.Translate(customerMapped);
                dalCustomer.AddResetPasswordCode(cus);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
