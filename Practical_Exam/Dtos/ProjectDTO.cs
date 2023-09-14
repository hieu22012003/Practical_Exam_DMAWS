using System.ComponentModel.DataAnnotations;

namespace Practical_Exam.Dtos
{
    public class ProjectDTO
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string ProjectName { get; set; }
        [Required]
        public DateTime? ProjectStartDate { get; set; }
        [DateGreaterThan("ProjectStartDate", ErrorMessage = "End date must be greater than start date")]
        public DateTime? ProjectEndDate { get; set; }
    }
}
