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
        public string GetTheLastEmployeeID()
        {

            List<Employee> lstEmployee = db.Employees.Select(emp => emp).ToList();
            Employee employee = lstEmployee.LastOrDefault();
            if (employee == null)
            {
                return "EMP10000";
            }
            return employee.EmployeeID;
        }
        public IEnumerable<Employee> GetAllEmployee()
        {
            return db.Employees.Select(emp => emp);
        }
        public void AddNewEmployee(Employee employee)
        {
            employee.Status = true;
            db.Employees.Add(employee);
            db.SaveChanges();
        }
        public void UpdateEmployee(Employee employee)
        {
            Employee emp = db.Employees.SingleOrDefault(e => e.EmployeeID == employee.EmployeeID);
            if (emp != null)
            {
                    emp.Name = employee.Name;
                    emp.Email = employee.Email;
                    emp.Address = employee.Address;
                    emp.PhoneNumber = employee.PhoneNumber;
                    emp.Gender = employee.Gender;
                    emp.DateOfBirth = employee.DateOfBirth;
                    emp.Status = employee.Status;
                    db.SaveChanges();
            }
        }
    }
}
