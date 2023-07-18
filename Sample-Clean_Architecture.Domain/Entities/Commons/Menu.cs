namespace Sample_Clean_Architecture.Domain.Entities.Commons
{
    public class Menu
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public int ParentId { get; set; }
    }
}
