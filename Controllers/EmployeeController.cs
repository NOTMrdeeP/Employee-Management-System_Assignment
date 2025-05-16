using EmployeeManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _context;

        public EmployeeController(EmployeeDbContext context)
        {
            _context = context;
        }

        //GET all employee records
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.ToListAsync();

            return View(employees);
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

        //POST: Soft Delete employee record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeID == id);

            if (employee != null)
            {
                employee.IsActive = false;
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        //GET: Search employees for department and filter by age
        public async Task<IActionResult> SearchEmployeesByDepartment(string query)
        {
            var employees = await _context.Employees
                            .Include(e => e.Department)
                            .Where(e => e.Department.Name.Contains(query))
                            .ToListAsync();

            return View("Index", employees);
        }

        //GET: Filter employees by certain age
        public async Task<IActionResult> SearchEmployeesByAge(int age)
        {
            var filteredEmployees = await _context.Employees
                                    .Where(e => e.Age == age)
                                    .ToListAsync();

            return View("Index", filteredEmployees);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            // Re-populate ViewBag in case of validation errors
            ViewBag.Departments = new SelectList(_context.Departments, "ID", "Name", employee.DepartmentID);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeID)
                return NotFound();

            try
            {
                _context.Update(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Employees.Any(e => e.EmployeeID == id))
                    return NotFound();
                else
                    throw;
            }

            // Re-populate ViewBag in case of validation errors
            ViewBag.Departments = new SelectList(_context.Departments, "ID", "Name", employee.DepartmentID);
            return View(employee);
        }
    }//end of class EmployeeController
}//end of namespace EmployeeManagementSystem.Controllers
