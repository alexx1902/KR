

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using WebApiClean.Data;

    namespace WebApiClean.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly Context _context;

        public EmployeeController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get()
        {
            return Ok(await _context.Employees.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var hero = await _context.Employees.FindAsync(id);
            if (hero == null)
                return BadRequest("Employee not found.");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return Ok(await _context.Employees.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee request)
        {
            var dbEmployee = await _context.Employees.FindAsync(request.EmployeeId);
            if (dbEmployee == null)
                return BadRequest("Employee not found.");

            dbEmployee.EmployeeName = request.EmployeeName;
            dbEmployee.Department = request.Department;
            dbEmployee.DateOfJoining = request.DateOfJoining;
            //dbEmployee.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.Employees.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employee>>> Delete(int Employeeid)
        {
            var dbEmployee = await _context.Employees.FindAsync(Employeeid);
            if (dbEmployee == null)
                return BadRequest("Employee not found.");

            _context.Employees.Remove(dbEmployee);
            await _context.SaveChangesAsync();

            return Ok(await _context.Employees.ToListAsync());
        }

    }
}