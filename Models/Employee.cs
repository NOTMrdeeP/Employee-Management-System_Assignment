using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(70)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Range(18, 65)]
        public int Age { get; set; }

        [Required]
        [ForeignKey("Department")]
        public int DepartmentID { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        //Navigation Property
        public virtual Department Department { get; set; }
    }//end of class Employee
}//end of namespace EmployeeManagementSystem.Models
