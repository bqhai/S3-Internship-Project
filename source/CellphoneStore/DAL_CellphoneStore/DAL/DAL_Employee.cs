using DAL_CellPhoneStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CellPhoneStore.DAL
{
    public class DAL_Employee
    {
        DbCellPhoneStoreEntities db = new DbCellPhoneStoreEntities();
        public DAL_Employee()
        {
            
        }
        public IEnumerable<Employee> GetAllEmployee()
        {
            return db.Employees.Select(emp => emp);
        }
    }
}
