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
        public IEnumerable<Customer> GetAllCustomer()
        {
            return db.Customers.Select(cus => cus);
        }
        public void AddNewCustomer(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();            
        }
        public Customer GetCustomerByUsername(string username)
        {
            return db.Customers.SingleOrDefault(cus => cus.Username == username);
        }
        public Customer GetCustomerByEmail(string email)
        {
            return db.Customers.SingleOrDefault(cus => cus.Email == email);
        }
        public void UpdateCustomerInfo(Customer customer)
        {
            Customer cus = db.Customers.SingleOrDefault(c => c.Username == customer.Username);
            if (cus != null)
            {
                cus.Name = customer.Name;
                cus.PhoneNumber = customer.PhoneNumber;
                cus.Email = customer.Email;
                cus.DateOfBirth = customer.DateOfBirth;
                cus.Gender = customer.Gender;
                db.SaveChanges();               
            }
        }
        public void UpdateCustomerAddress(Customer customer)
        {
            Customer cus = db.Customers.SingleOrDefault(c => c.Username == customer.Username);
            cus.Address = customer.Address;
            db.SaveChanges();         
        }
        public void AddResetPasswordCode(Customer customer)
        {
            Customer cus = db.Customers.SingleOrDefault(c => c.Email == customer.Email);
            cus.ResetPasswordCode = customer.ResetPasswordCode;
            db.SaveChanges();         
        }
    }
}
