namespace Diploma.Models
{
    public class PaginatorViewModel<T>
    {
        public List<T> Items { get; set; }

        public PaginatorOptionsViewModel Options { get; set; }
    }
}
