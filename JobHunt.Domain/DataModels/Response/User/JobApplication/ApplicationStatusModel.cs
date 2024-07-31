namespace JobHunt.Domain.DataModels.Response.User.JobApplication
{
    public class ApplicationStatusModel
    {
        public int StatusId { get; set; }

        public string Notes { get; set; } = string.Empty;

        public string StatusName { get; set; } = string.Empty;  
            
        public DateTime? CreatedDate { get; set; }
    }
}
