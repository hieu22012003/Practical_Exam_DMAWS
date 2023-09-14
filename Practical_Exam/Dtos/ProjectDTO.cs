namespace Practical_Exam.Dtos
{
    public class ProjectDTO
    {
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public List<ProjectEmployeeDTO>? projectEmployee { get; set; }

    }
}
