namespace Practical_Exam.Dtos
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime? EmployeeDob { get; set; }
        public string? EmployeeDepartment { get; set; }
        public List<ProjectEmployeeDTO>? projectEmployee { get; set; }

    }
}
