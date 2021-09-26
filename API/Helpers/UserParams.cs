namespace API.Helpers
{
    public class UserParams : PaginationParams
    {
        public string CurrentUserName {get; set;}
        public string OrderBy { get; set; }
        public bool Pagination { get; set; }
    }
}