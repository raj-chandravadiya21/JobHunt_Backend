namespace JobHunt.Domain.DataModels.Request.UserRequest.Application
{
    public class UserApplicationsRequest : PaginationParameter
    {
        public int CtcStart { get; set; }

        public int CtcEnd { get; set;}

        public int ApplicationStatus { get; set; }

        public string? JobName { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }
    }
}
