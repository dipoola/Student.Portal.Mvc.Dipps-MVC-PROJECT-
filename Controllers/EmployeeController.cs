using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student.Portal.Mvc.Dipps.Data;
using Student.Portal.Mvc.Dipps.Models;
using Student.Portal.Mvc.Dipps.Models.Entities;

namespace Student.Portal.Mvc.Dipps.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]  
        public async Task<IActionResult> Add(AddEmployeeViewModel viewModel) 
        {

            var employee = new Employee
            {
                Name = viewModel.Name,
                Phone = viewModel.Phone,
                Email = viewModel.Email,
                Subscribed = viewModel.Subscribed,

            };
            await dbContext.AddAsync(employee);    
            await dbContext.SaveChangesAsync(); 

              return View();
        }


        [HttpGet]   
        public async Task<IActionResult> List()
        {

          var employee =  await dbContext.employees.ToListAsync();
            return View(employee);  
        }
    }
}
