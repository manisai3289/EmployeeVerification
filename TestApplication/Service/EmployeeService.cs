using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApplication.ApplicationContext;
using TestApplication.Interface;
using TestApplication.Model;

namespace TestApplication.Service
{
    public class EmployeeService :IEmployeeService
    {
        private readonly DataContext _context;

        public EmployeeService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> VerifyEmployeeAsync(int employeeId, string companyName, string verificationCode)
        {
            var employee = await _context.employeeDetails
                .FirstOrDefaultAsync(e => e.employeeId == employeeId &&
                                           e.companyName == companyName &&
                                           e.verficationCode == verificationCode);
            return employee != null;
        }


        public async Task<bool> AddEmployeeAsync(EmployeeDetail employeeDetails)
        {
            await _context.employeeDetails.AddAsync(employeeDetails);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<EmployeeDetail>> GetAllEmployeesAsync()
        {            
            return await _context.employeeDetails.ToListAsync();
        }
    }
}
