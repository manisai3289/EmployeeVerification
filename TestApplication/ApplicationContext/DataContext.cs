using Microsoft.EntityFrameworkCore;
using TestApplication.Model;

namespace TestApplication.ApplicationContext
{
    public class DataContext : DbContext
    {
        public  DataContext(DbContextOptions<DataContext> options) :base(options)
        {

        }
        public DbSet<EmployeeDetail> employeeDetails { get; set; }
        //public object Employees { get; internal set; }
    }
}

