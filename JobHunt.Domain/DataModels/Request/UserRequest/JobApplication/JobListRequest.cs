namespace JobHunt.Domain.DataModels.Request.UserRequest.JobApplication
{
    public class JobListRequest : PaginationParameter
    {
        public int CtcStart { get; set; }   

        public int CtcEnd { get; set; }

        public List<int>? Skills { get; set; } = null;

        public string? JobName { get; set; }

        public Double Experience { get; set; }
    }
}
