using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApiConsumeApp.Models;
using DataAccessLayer;
using System.Net.Http;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;

namespace WebApiConsumeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult>  Index()
        {
            List<Employee> employees = new List<Employee>();
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri("https://localhost:44380/");
            HttpResponseMessage respone = await http.GetAsync("api/GetAllEmployees");
            if(respone.IsSuccessStatusCode)
            {
                var result = await respone.Content.ReadAsStringAsync();
                employees = JsonConvert.DeserializeObject<List<Employee>>(result);
            }
            return View(employees);
        }
         
        public async Task<IActionResult> Detail(int id)
        {

            Employee employee = await GetEmployeeById(id);
            return View(employee);


        }

        private static async Task<Employee> GetEmployeeById(int id)
        {
            Employee employee = new Employee();
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri("https://localhost:44380");
            HttpResponseMessage respone = await http.GetAsync($"/api/GetAllEmployee/{id}");
            if (respone.IsSuccessStatusCode)
            {
                var result = await respone.Content.ReadAsStringAsync();
                employee = JsonConvert.DeserializeObject<Employee>(result);
            }

            return employee;

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {

           
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri("https://localhost:44380/");
            var response = await http.PostAsJsonAsync<Employee>("/api/Postemployee", employee);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task <IActionResult> Edit(int id)
        {
            Employee employee = await GetEmployeeById(id);
            return View(employee);
        }
        [HttpPost]
        public async Task<IActionResult> Edit( Employee employee)
        {
           
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri("https://localhost:44380/");
                var response = await http.PutAsJsonAsync($"api/UpdateEmployee/{employee.Id}", employee);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            return View();
            
          
                

            
        }
        public async Task<IActionResult> Delete(int id)
        {
           
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri("https://localhost:44380/");
                var response = await http.DeleteAsync($"/api/DeleteEmployee/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
           
                return View();
          
            
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
