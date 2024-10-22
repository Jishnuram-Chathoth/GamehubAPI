namespace GamehubAPI.Model
{
    public class PaginationParams
    {
        public int PageNumber { get; set; } = 1; // Default to the first page
        public int PageSize { get; set; } = 10; // Default page size

    }
}
