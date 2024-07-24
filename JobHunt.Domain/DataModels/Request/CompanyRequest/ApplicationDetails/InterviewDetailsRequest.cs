namespace JobHunt.Domain.DataModels.Request.CompanyRequest.ApplicationDetails
{
    public class InterviewDetailsRequest
    {
        public int ApplicationId { get; set; }

        public DateOnly InterviewDate { get; set; }

        public string Location { get; set;} = string.Empty;

        public TimeOnly StartTime { get; set;}

        public TimeOnly EndTime { get; set;}

        public string Notes { get; set;} = string.Empty;
    }
}
