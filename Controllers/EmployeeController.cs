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

        public IActionResult Index()
        {
            return View();
        }

        //GET: Search employees for department and filter by age
        public async Task<IActionResult> SearchEmployeeByDepartment(string query)
        {
            var employees = await _context.Employees
                            .Include(e => e.Department)
                            .Where(e => e.Department.Name.Contains(query))
                            .ToListAsync();

            return View("Index", employees);
        }

        //POST: Soft Delete employee record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            employee.IsActive = false;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }//end of class EmployeeController
}//end of namespace EmployeeManagementSystem.Controllers
