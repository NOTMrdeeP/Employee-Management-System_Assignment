using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        //Navigation Property
        public ICollection<Employee> Employees { get; set; }

        public bool IsActive { get; set; } = true;
    }//end of class Department
}//end of namespace EmployeeManagementSystem.Models
