using System.ComponentModel.DataAnnotations;

namespace EmployeeProgram.Models
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "ContactNo should contain exactly 10 digits.")]
        public string ContactNo { get; set; }
    }
}
