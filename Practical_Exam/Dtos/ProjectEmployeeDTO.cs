namespace Practical_Exam.Dtos
{
    public class ProjectEmployeeDTO
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public string? Tasks { get; set; }
        public ProjectDTO Project { get; set; }
        public EmployeeDTO Employee { get; set; }
    }
}
