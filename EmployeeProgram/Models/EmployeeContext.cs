using Microsoft.EntityFrameworkCore;

namespace EmployeeProgram.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions options) : base(options) { }

        public DbSet<Employees> Employees { get; set; }
    }
}
