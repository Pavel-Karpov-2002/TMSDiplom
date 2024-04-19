namespace Diploma.Models
{
    public class PaginatorOptionsViewModel
    {
        public int CurrentPage { get; set; }
        public List<int> AvailablePages { get; set; } = new();
    }
}
