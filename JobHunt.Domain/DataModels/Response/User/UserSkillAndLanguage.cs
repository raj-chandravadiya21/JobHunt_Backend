namespace JobHunt.Domain.DataModels.Response.User
{
    public class UserSkillAndLanguage
    {
        public int UserId { get; set; } 

        public List<string>? Skills { get; set; }

        public List<string>? Languages { get; set; }
    }
}
