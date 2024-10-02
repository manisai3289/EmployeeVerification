using System.ComponentModel.DataAnnotations;

namespace EmployeeWeb.Models
{
    public class EmployeeViewModel
    {
        public int employeeId { get; set; }
        public string companyName { get; set; } = string.Empty;
        public string verficationCode { get; set; } = string.Empty;
        public List<EmployeeViewModel> SubmittedDetails { get; set; } = new List<EmployeeViewModel>();


    }
}
