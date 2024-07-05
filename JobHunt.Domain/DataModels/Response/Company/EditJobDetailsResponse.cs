namespace JobHunt.Domain.DataModels.Response.Company
{
    public class EditJobDetailsResponse
    {
        public int JobId { get; set; }

        public string JobName { get; set; } = null!;

        public string Location { get; set; } = null!;

        public DateOnly StartDate { get; set; } 

        public int CtcStart {  get; set; }

        public int CtcEnd { get; set;}

        public Double Experience { get; set; }

        public DateOnly LastDateToApply { get; set; }   

        public int NoOfOpening {  get; set; }

        public string JobDescription { get; set; } = null!;

        public string JobRequirements { get; set; } = null!;

        public List<string> JobPerks { get; set; } = null!;

        public List<string> JobResponsibility { get; set; } = null!;

        public List<int> JobSkill { get; set; } = null!;

    }
}
