using Abp.Application.Services;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using theSanju.dataAccess;

namespace theSanju.Resposistry
{
    public class EmployeeReposistry: IEmpoloyeeReposistry
    {
        private readonly ApplicationRervices _context;
        public EmployeeReposistry(ApplicationRervices context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var Result =await _context.employees.AddAsync(employee);
             await  _context.SaveChangesAsync();
            return Result.Entity;
        }

        public async Task<Employee> DeleteEmploye(int id)
        {
            var result = await _context.employees.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(result!= null)
            {
                 _context.Remove(result);
               await _context.SaveChangesAsync();
                return result;

            }
            return null;
        }

        public Task<ActionResult<Employee>> DeleteEmployee(Employee employee)
        {
            throw new NotImplementedException();
        } 

        public async Task<Employee> GetEmployee(int id)
        {
           return await _context.employees.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.employees.ToListAsync();
           
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var Result = await _context.employees.Where(x => x.Id == employee.Id).FirstOrDefaultAsync();
            if( Result != null)
            {
                Result.Id = employee.Id;
                Result.Name = employee.Name;
                Result.City = employee.City;
                Result.Address = employee.Address;
               await _context.SaveChangesAsync();
                return Result;
                   
            }
            return null;
        }
    }
}
