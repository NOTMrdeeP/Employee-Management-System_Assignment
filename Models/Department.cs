// Rohit Aroonslam (s227876075)
// Amity Brown (s227649087)
// Alyssa Damons (s219344892)
// Martin Du Preez (s227147030)
// Marcellos Von Buchenroder (s228139759)

using System.ComponentModel.DataAnnotations;

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
    }//end of class Department
}//end of namespace EmployeeManagementSystem.Models
