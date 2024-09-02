using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using theSanju.Resposistry;
using DataAccessLayer;

namespace theSanju.Controllers
{
    [Route("api/")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly IEmpoloyeeReposistry _EmpContext;
        public HomeController(IEmpoloyeeReposistry EmpContext)
        {
            _EmpContext = EmpContext;
        }
        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                return Ok( await _EmpContext.GetEmployees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erroe Retrive data From data base");
            }
        }
        [HttpGet]
        [Route("GetAllEmployee/{id}")]
        public async Task<IActionResult> GetAllEmployee(int id )
        {
            try
            {
                return Ok(await _EmpContext.GetEmployee(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Created Problem in Data filed");
            }
        }
        [HttpPost]
        [Route("Postemployee")]
        public async Task<ActionResult> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest("In the Form does Hass not value ");
                }
                var createdEmployee = await _EmpContext.AddEmployee(employee);
                return CreatedAtAction(nameof(GetAllEmployee), new { id = employee.Id }, createdEmployee);
              
                
            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Data Hass been  not Created ");
            }
        }
        [HttpPut]
        [Route("UpdateEmployee/{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return BadRequest("id Not match");

                }
                var UpdateEmployeedata = await _EmpContext.GetEmployee(id);
                if (UpdateEmployeedata == null)
                {
                    return BadRequest("Id not found");

                }
                return  await _EmpContext.UpdateEmployee(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Data Note Updae"); 
            }
        }
        [HttpDelete]
        [Route("DeleteEmployee/{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var deleteEmp = await _EmpContext.GetEmployee(id);
                if (deleteEmp == null)
                {
                    return BadRequest("id does not Matching in Data Base{id}");
                }
                return await _EmpContext.DeleteEmploye(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Data Note Delete");
            }
        }
       
    }
}
