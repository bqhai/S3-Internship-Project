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
    public class BLL_Employee
    {
        DAL_Employee dalEmployee = new DAL_Employee();
        EntityMapper<Employee, EmployeeMapped> convertToEmpMapped = new EntityMapper<Employee, EmployeeMapped>();
        EntityMapper<EmployeeMapped, Employee> convertToEmp = new EntityMapper<EmployeeMapped, Employee>();
        public BLL_Employee()
        {

        }
        
        public List<EmployeeMapped> GetAllEmployee()
        {
            IEnumerable<Employee> employees = dalEmployee.GetAllEmployee();
            List<EmployeeMapped> employeeMappeds = new List<EmployeeMapped>();
            foreach (var item in employees)
            {
                employeeMappeds.Add(convertToEmpMapped.Translate(item));
            }
            return employeeMappeds;
        }
        public bool AddNewEmployee(EmployeeMapped employeeMapped)
        {
            string lastEmployeeID = dalEmployee.GetTheLastEmployeeID();
            try
            {
                string employeeID = AutoGen.CreateID("EMP", lastEmployeeID);
                employeeMapped.EmployeeID = employeeID;
                Employee emp = convertToEmp.Translate(employeeMapped);
                dalEmployee.AddNewEmployee(emp);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateEmployee(EmployeeMapped employeeMapped)
        {
            try
            {
                Employee emp = convertToEmp.Translate(employeeMapped);
                dalEmployee.UpdateEmployee(emp);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
