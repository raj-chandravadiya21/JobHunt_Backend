namespace JobHunt.Domain.Enum
{
    public enum Role
    {
        Admin = 1,
        User = 2,
        Company = 3,
    }

    public enum ApplicationStatuses
    {
        Applied = 1,
        UnderReview = 2,
        ScheduleInterview = 3,
        Interviewed = 4,
        Selected = 5,
        Rejected = 6,
    }
}
