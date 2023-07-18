namespace Sample_Clean_Architecture.Domain.Entities.Companies
{
    public class CompanyFinancialCycle
    {
        public int Id { get; set; }
        public string Title { get; set; }= string.Empty;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool isActive { get; set; }
        public int Company_Id { get; set; }
    }
}
