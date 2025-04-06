using Microsoft.EntityFrameworkCore;
using Student.Portal.Mvc.Dipps.Models.Entities;
using System.Diagnostics;

namespace Student.Portal.Mvc.Dipps.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> employees { get; set; }
    }
}