using EmployeeManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly EmployeeDbContext _context;

        public DepartmentController(EmployeeDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var departments = _context.Departments.ToList();
            return View(departments);
        }

        public IActionResult Details()
        {
            return View();
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
        //GET: Edit Department
        
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var department = _context.Departments.Find(id);
            if (department == null) return NotFound();

            return View(department);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Department department)
        {
            if (id != department.DepartmentID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(department);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }
        //POST: Delete Department
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = _context.Departments.Find(id);
            if (department != null)
            {
                department.IsActive = false;
                _context.Departments.Update(department);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
