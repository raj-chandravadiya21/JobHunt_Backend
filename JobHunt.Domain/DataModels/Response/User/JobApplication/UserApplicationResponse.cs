namespace JobHunt.Domain.DataModels.Response.User.JobApplication
{
    public class UserApplicationResponse
    {
        public int ApplicationId { get; set; }

        public int JobId { get; set; }

        public string CompanyName { get; set; } = null!;

        public int CtcStart { get; set; }

        public int CtcEnd { get; set; }

        public int NoOfOpening { get; set; }

        public DateOnly LastDateToApply { get; set; }

        public DateOnly AppliedDate { get; set; }

        public string JobName { get; set; } = null!;

        public Double NoOfApplicant { get; set; }

        public string ApplicationStatus { get; set; } = null!;

        public int ConversationId { get; set; }
    }
}
