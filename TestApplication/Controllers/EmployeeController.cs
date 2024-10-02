using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApplication.ApplicationContext;
using TestApplication.Interface;
using TestApplication.Model;

namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmploymentVerificationController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly DataContext _context;

        public EmploymentVerificationController(IEmployeeService employeeService , DataContext context)
        {
            _employeeService = employeeService;
             _context = context;
        }

        [HttpPost("verify-employment")]
        public async Task<IActionResult> VerifyEmployment([FromBody] EmployeeDetail request)
        {
            var isVerified = await _employeeService.VerifyEmployeeAsync(request.employeeId, request.companyName, request.verficationCode);
            return Ok(new { isVerified });
        }


        [HttpPost("addEmployees")]
        public async Task<IActionResult> AddEmployees([FromBody] EmployeeDetail employeeDetails)
        {
            var value = await _employeeService.AddEmployeeAsync(employeeDetails);
            return Ok(value);
        }

        [HttpGet("getAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }
    }
}
