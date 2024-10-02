using System.ComponentModel.DataAnnotations;

namespace TestApplication.Model
{
    public class EmployeeDetail
    {
        [Key]
        public int employeeId {  get; set; }
        public string companyName { get; set; } = null!;
        public string  verficationCode {  get; set; }

    }
}
