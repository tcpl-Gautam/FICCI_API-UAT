namespace FICCI_API.DTO
{
    public class ProjectDTO
    {
        public int ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string Division { get; set; }
        public string Department { get; set; }
        public string GST { get; set; }
        public string PAN { get; set; }

    }
    public class AllProjectList
    {
        public int ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
    }
}
