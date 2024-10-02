using TestApplication.Model;

namespace TestApplication.Interface
{
    public interface IEmployeeService
    {
       Task<bool> VerifyEmployeeAsync(int employeeId, string companyName, string verficationCode);

        Task<bool> AddEmployeeAsync(EmployeeDetail employeeDetails);
        Task<List<EmployeeDetail>> GetAllEmployeesAsync();
    }
}   
