using EmployeeManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _context;

        
        public EmployeeController(EmployeeDbContext context)
        {
            _context = context;
        }

        //GET: Create Employee
        public IActionResult Create()
        {
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "Name");
            return View();
        }

        //POST: Create Employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "Name", employee.DepartmentID);
            return View(employee);
        }



        //Soft Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            

            if (employee != null)
            {
                employee.IsActive = false;
                await _context.SaveChangesAsync();
            }
            else return NotFound();

            return RedirectToAction("Index");
        }


        //Search department and filter by age
        public async Task<IActionResult> Index(string SearchDepart, int? MinAge)
        {
            
            var query = _context.Employees
                .Include(e => e.Department)
                .Where(e => e.IsActive)  // Only shows active employees
                .AsQueryable();

            
            if (!string.IsNullOrEmpty(SearchDepart))
            {
                query = query.Where(e => e.Department.Name.Contains(SearchDepart));
            }

            
            if (MinAge.HasValue)
            {
                query = query.Where(e => e.Age >= MinAge);
            }

            // Execute the query and pass results to view
            var employees = await query.ToListAsync();
            return View(employees);
        }
    }//end of class EmployeeController
}//end of namespace EmployeeManagementSystem.Controllers
