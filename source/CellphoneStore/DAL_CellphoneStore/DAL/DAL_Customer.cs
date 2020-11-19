using DAL_CellPhoneStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CellPhoneStore.DAL
{
    public class DAL_Customer
    {
        DbCellPhoneStoreEntities db = new DbCellPhoneStoreEntities();
        public DAL_Customer()
        {

        }
        public string GetTheLastCustomerID()
        {
            List<Customer> lstCustomer = db.Customers.Select(cus => cus).ToList();
            Customer customer = lstCustomer.LastOrDefault();
            if(customer == null)
            {
                return "CUS10000";
            }
            return customer.CustomerID;
        }
        public bool AddNewCustomer(Customer customer)
        {
            try
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Customer GetCustomerByUsername(string username)
        {
            return db.Customers.SingleOrDefault(cus => cus.Username == username);
        }
        public Customer GetCustomerByEmail(string email)
        {
            return db.Customers.SingleOrDefault(cus => cus.Email == email);
        }
        public bool UpdateCustomerInfo(Customer customer)
        {
            Customer cus = db.Customers.SingleOrDefault(c => c.Username == customer.Username);
            if (cus != null)
            {
                try
                {
                    cus.Name = customer.Name;
                    cus.PhoneNumber = customer.PhoneNumber;
                    cus.Email = customer.Email;
                    cus.DateOfBirth = customer.DateOfBirth;
                    cus.Gender = customer.Gender;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
        public bool UpdateCustomerAddress(Customer customer)
        {
            try
            {
                Customer cus = db.Customers.SingleOrDefault(c => c.Username == customer.Username);
                cus.Address = customer.Address;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool AddResetPasswordCode(Customer customer)
        {
            try
            {
                Customer cus = db.Customers.SingleOrDefault(c => c.Email == customer.Email);
                cus.ResetPasswordCode = customer.ResetPasswordCode;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
