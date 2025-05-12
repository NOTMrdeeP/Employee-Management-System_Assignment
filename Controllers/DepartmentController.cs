using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Data;

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

        public IActionResult Details()
        {
            return View();
        }
    }
}
