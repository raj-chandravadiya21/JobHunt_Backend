namespace JobHunt.Domain.DataModels.Response.User.JobApplication
{
    public class JobListResponse
    {
        public int JobId { get; set; }

        public string CompanyName { get; set; } = null!;

        public int CtcStart { get; set; }

        public int CtcEnd { get; set; }

        public int NoOfOpening { get; set; }

        public DateOnly LastDateToApply { get; set; }

        public string JobName { get; set; } = null!;

        public long NoOfApplicant { get; set; }

        public Double Experience { get; set; }
    }
}
