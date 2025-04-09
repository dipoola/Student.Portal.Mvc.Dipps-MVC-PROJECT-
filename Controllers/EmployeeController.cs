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

            var employee = await dbContext.employees.ToListAsync();
            return View(employee);
        }


        [HttpGet]
        public async  Task<IActionResult> Edit( Guid id)
         {

           var employee=  await dbContext.employees.FindAsync(id);
            return View(employee);  
        }

        [HttpPost]
        public async Task <IActionResult> Edit(Employee viewmodel)
        {
           var employee=  await dbContext.employees.FindAsync(viewmodel.Id);
            if(employee is not null)
             {

                employee.Email= viewmodel.Email;    
                employee.Phone= viewmodel.Phone;
                employee.Name= viewmodel.Name;
               employee.Subscribed = viewmodel.Subscribed;  

                await dbContext.SaveChangesAsync(); 

            }

            return RedirectToAction("List", "Employee");
        }

        [HttpPost]

        public async  Task<IActionResult> Delete(Employee viewmodel)
        {

            var employee = await dbContext.employees.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == viewmodel.Id);   

            if(employee is not null) 
            { 
               dbContext.employees.Remove(employee);
                await dbContext.SaveChangesAsync(); 

            }
            return RedirectToAction("List", "Employee");

        }
    }
}
