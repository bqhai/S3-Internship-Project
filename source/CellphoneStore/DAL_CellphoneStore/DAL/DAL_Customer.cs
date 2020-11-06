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
            return customer.CustomerID;
        }
        public void AddNewCustomer(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
        }
    }
}
