using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace theSanju.Resposistry
{
   public interface IEmpoloyeeReposistry
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int id);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<Employee> DeleteEmploye(int id);
        Task<ActionResult<Employee>> DeleteEmployee(Employee employee);
  
        
    }
}
