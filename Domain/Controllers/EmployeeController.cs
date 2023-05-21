using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace Domain.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("")]
    public class EmployeeController : ControllerBase
    {
        private readonly string? _dalUrl;
        private readonly HttpEmployee _employee;

        public 
            EmployeeController(IConfiguration conf)
        {
            _dalUrl = conf.GetValue<string>("DalUrl");
            _employee = new HttpEmployee();
        }

        [HttpGet("List")]
        public async Task<ActionResult<Employee[]>> GetEmployeesByname(string? name)
        {
            var response = await _employee.GetAsync($"{_dalUrl}=WebApiClean/DAL/{name}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<Product[]>() ?? Array.Empty<Employee>();
            if (string.IsNullOrWhiteSpace(name)) return result;
            return Array.FindAll(result, p => !string.IsNullOrWhiteSpace(p.Name) && p.Name.Contains(name));
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            JsonContent content = JsonContent.Create(employee);
            using var result = await _employee.PostAsync($"{_dalUrl}/WebApiClean/DAL/Product/AddProduct", content);
            var dalEmployee = await result.Content.ReadFromJsonAsync<Employee>();
            Console.WriteLine($"{dalEmployee?.Name}");

            if (dalEmployee == null)
                return BadRequest();
            else
                return dalEmployee;
        }

        
        }

    }
}
