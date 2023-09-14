using System;
using System.Collections.Generic;

namespace Practical_Exam.Entities;

public partial class ProjectEmployee
{
    public int EmployeeId { get; set; }

    public int ProjectId { get; set; }

    public string? Tasks { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
