namespace Sample_Clean_Architecture.Application.Interfaces.FacadPatterns
{
    public interface ICompanyFacad
    {
        IAddNewCompanyFinancialCycleServices AddNewCompanyFinancialCycleServices { get; }

        IAddNewCompanyBranchServices AddNewCompanyBranchServices { get; }

        IGetCompanyFinancialCycleServices GetCompanyFinancialCycleServices { get; }

        IGetCompanyBranchServices GetCompanyBranchServices { get; }
       
        IAddNewCompanyService AddNewCompanyService { get; }
        IGetCompanyService GetCompanyService { get; }
       
        IGetCompanyInfoService GetCompanyInfoService { get; }
       
        IGetCompanyFinancialCycleInfoService GetCompanyFinancialCycleInfoService { get; }
       
        IGetCompanyBranchInfoService GetCompanyBranchInfoService { get; }

        public IGetCompanyUsersServices GetCompanyUsersServices { get; }

        public IAddNewCompanyUserServices AddNewCompanyUserServices { get; }

        IGetCompanyUserInfoService GetCompanyUserInfoService { get; }

        public IDeleteCompanyUserService DeleteCompanyUserService { get; }
    }
}
