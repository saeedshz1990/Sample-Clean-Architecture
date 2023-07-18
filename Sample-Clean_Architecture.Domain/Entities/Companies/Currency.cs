namespace Sample_Clean_Architecture.Domain.Entities.Companies
{
    public class Currency
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Subunit { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;

    }
}
