using EmployeeManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _context;

        public EmployeeController(EmployeeDbContext context)
        {
            _context = context;
        }

        //Soft Delete
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int  id)
        {
            var employee = await _context.Employees.FindAsync(id);
            employee.IsActive = false;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        //Search department and filter by age
        public async Task<IActionResult> Index(string SearchDepart,int? MinAge)
        {
            var employees = await _context.Employees
                            .Include(e => e.Department)
                            .ToListAsync();

            if(!string.IsNullOrEmpty(SearchDepart))
            {
                employees = employees.Where(e =>e.Department.Name == SearchDepart).ToList();
            }

            if (MinAge.HasValue)
            {
                employees = employees.Where(e=>e.Age > MinAge).ToList();
            }

            return View(employees);
        }
    }//end of class EmployeeController
}//end of namespace EmployeeManagementSystem.Controllers
