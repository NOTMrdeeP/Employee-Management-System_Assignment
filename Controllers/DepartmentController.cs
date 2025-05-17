// Rohit Aroonslam (s227876075)
// Amity Brown (s227649087)
// Alyssa Damons (s219344892)
// Martin Du Preez (s227147030)
// Marcellos Von Buchenroder (s228139759)

using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly EmployeeDbContext _context;
        public DepartmentController(EmployeeDbContext context)
        {
            _context = context;
        }
        //GET all department
        public async Task<IActionResult> Index()
        {
            var departments = await _context.Departments.ToListAsync();
            return View(departments);
        }
        //GET:Create Department
        public IActionResult Create()
        {
            return View();
        }

        //POST: Create Department
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(department);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        //GET:Edit Department
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();

            ViewBag.EmployeeList = new SelectList(_context.Employees, "EmployeeID", "FirstName", department.Employees);
            return View(department);
        }

        //POST: Edit Department
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.DepartmentID) return NotFound();

            try
            {
                _context.Update(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Departments.Any(d => d.DepartmentID == id))
                    return NotFound();
                else
                    throw;
            }

            ViewBag.EmployeeList = new SelectList(_context.Employees, "EmployeeID", "FirstName", department.Employees);
            return View(department);
        }


        public IActionResult Details()
        {
            return View();
        }
    }
}
