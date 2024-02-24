namespace FICCI_API.DTO
{
    public class ProjectDTO
    {
        public int ProjectId { get; set; }

        public string ProjectCode { get; set; }

        public string ProjectName { get; set; }

        public string ProjectDepartmentCode { get; set; }

        public string ProjectDepartmentName { get; set; }

        public string ProjectDivisionCode { get; set; }

        public string ProjectDivisionName { get; set; }

        public string TlApprover { get; set; }

        public string ChApprover { get; set; }

        public string FinanceApprover { get; set; }

        public string SupportApprover { get; set; }

        public bool? ProjectActive { get; set; }

    }
    public class AllProjectList
    {
        public int ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string? Divison { get; set; }
        public string? Department { get; set; }
    }
}
