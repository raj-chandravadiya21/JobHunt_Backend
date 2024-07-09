namespace JobHunt.Domain.DataModels.Request
{
    public class PaginationParameter
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize  { get; set; } = 6;

        public string? OrderBy { get; set; }

        public string? Direction { get; set; } = "ASC";

    }
}
