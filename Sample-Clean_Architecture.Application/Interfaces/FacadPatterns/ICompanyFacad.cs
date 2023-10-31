using Sample_Clean_Architecture.Application.Services.Companies.Commands.AddNewCompany;
using Sample_Clean_Architecture.Application.Services.Companies.Commands.AddNewCompanyBranch;
using Sample_Clean_Architecture.Application.Services.Companies.Commands.AddNewCompanyFinancialCycle;
using Sample_Clean_Architecture.Application.Services.Companies.Commands.AddNewCompanyUser;
using Sample_Clean_Architecture.Application.Services.Companies.Commands.DeleteCompanyUser;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanies;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyBranches;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyFinancialCycle;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyUsers;

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
