using EmployeeManagement.Api.Models;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                return Ok(await _employeeRepository.GetEmployees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error retrieving data from the database.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmplyee(int id)
        {
            try
            {
                var result = await _employeeRepository.GetEmployee(id);

                if (result is null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error retrieving data from the database.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee is null)
                {
                    return BadRequest();
                }

                var emp = await _employeeRepository.GetEmployeeByEmail(employee.Email);

                if (emp is not null)
                {
                    ModelState.AddModelError("email", "Employee email is already in use.");
                    return BadRequest(ModelState);
                }

                var createdEmployee = await _employeeRepository.AddEmployee(employee);

                return CreatedAtAction(nameof(GetEmplyee), new { id = createdEmployee.EmployeeId }, createdEmployee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error retrieving data from the database.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                if (id != employee.EmployeeId)
                {
                    return BadRequest("Employee ID mismatch.");
                }

                var employeeToUpdate = await _employeeRepository.GetEmployee(id);

                if (employeeToUpdate is null)
                {
                    return NotFound("Employee to update was not found.");
                }

                return await _employeeRepository.UpdateEmployee(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error updating data from the database.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var employeeToDelete = await _employeeRepository.GetEmployee(id);

                if (employeeToDelete is null)
                {
                    return NotFound("Employee to delete was not found.");
                }

                return await _employeeRepository.DeleteEmployee(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error updating data from the database.");
            }
        }
    }
}
