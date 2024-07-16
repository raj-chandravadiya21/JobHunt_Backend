namespace JobHunt.Domain.DataModels.Response.User
{
    public class UserEducationModel
    {
        public int UserId { get; set; }

        public string DegreeName { get; set; } = null!;

        public string InstituteName { get; set; } = null!;

        public string PercentageGrade { get; set; } = null!;

        public int StartYear { get; set; }

        public int EndYear { get; set; }    
    }
}
