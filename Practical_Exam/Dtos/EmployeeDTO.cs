using System.ComponentModel.DataAnnotations;

namespace Practical_Exam.Dtos
{
    public class EmployeeDTO
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string? EmployeeName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [MinimumAge(16)]
        public DateTime? EmployeeDob { get; set; }

        [Required]
        [EmailAddress]
        public string? EmployeeDepartment { get; set; }
    }
}
