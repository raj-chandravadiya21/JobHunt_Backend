namespace JobHunt.Domain.DataModels.Response.Company
{
    public class EditJobDetailsResponse
    {
        public int JobId { get; set; }

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public DateOnly JobStartDate { get; set; } 

        public int CtcStart {  get; set; }

        public int CtcEnd { get; set;}

        public Double Experience { get; set; }

        public DateOnly LastDate { get; set; }   

        public int NoOfOpening {  get; set; }

        public string Description { get; set; } = null!;

        public string Requirement { get; set; } = null!;

        public List<string> Perks { get; set; } = null!;

        public List<string> Responsibility { get; set; } = null!;

        public List<int> Skills { get; set; } = null!;

    }
}
