using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Application.Interfaces.FacadPatterns;
using Sample_Clean_Architecture.Application.Services.Companies.Commands.AddNewCompany;
using Sample_Clean_Architecture.Application.Services.Companies.Commands.AddNewCompanyBranch;
using Sample_Clean_Architecture.Application.Services.Companies.Commands.AddNewCompanyFinancialCycle;
using Sample_Clean_Architecture.Application.Services.Companies.Commands.AddNewCompanyUser;
using Sample_Clean_Architecture.Application.Services.Companies.Commands.DeleteCompanyUser;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanies;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyBranches;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyFinancialCycle;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanyUsers;

namespace Sample_Clean_Architecture.Application.Services.Companies.FacadPattern
{
    public class CompanyFacad : ICompanyFacad
    {
        private readonly IDatabaseContext _context;

        public CompanyFacad(IDatabaseContext context)
        {
            _context = context;


        }
        private IGetCompanyUsersServices _getCompanyUsersServices;
        public IGetCompanyUsersServices GetCompanyUsersServices
        {
            get
            {
                return _getCompanyUsersServices = _getCompanyUsersServices ?? new GetCompanyUsersServices(_context);
            }
        }
        private IGetCompanyFinancialCycleServices _getCompanyFinancialCycleServices;
        public IGetCompanyFinancialCycleServices GetCompanyFinancialCycleServices
        {
            get
            {
                return _getCompanyFinancialCycleServices = _getCompanyFinancialCycleServices ?? new GetCompanyFinancialCycleServices(_context);
            }
        }

        private IGetCompanyBranchServices _getCompanyBranchServices;
        public IGetCompanyBranchServices GetCompanyBranchServices
        {
            get
            {
                return _getCompanyBranchServices = _getCompanyBranchServices ?? new GetCompanyBranchServices(_context);
            }
        }

        private IGetCompanyFinancialCycleInfoService _getCompanyFinancialCycleInfoService;
        public IGetCompanyFinancialCycleInfoService GetCompanyFinancialCycleInfoService
        {
            get
            {
                return _getCompanyFinancialCycleInfoService = _getCompanyFinancialCycleInfoService ?? new GetCompanyFinancialCycleInfoService(_context);
            }
        }

        private IGetCompanyBranchInfoService _getCompanyBranchInfoService;
        public IGetCompanyBranchInfoService GetCompanyBranchInfoService
        {
            get
            {
                return _getCompanyBranchInfoService = _getCompanyBranchInfoService ?? new GetCompanyBranchInfoService(_context);
            }
        }


        private IDeleteCompanyUserService _deleteCompanyUserService;
        public IDeleteCompanyUserService DeleteCompanyUserService
        {
            get
            {
                return _deleteCompanyUserService = _deleteCompanyUserService ?? new DeleteCompanyUserService(_context);
            }
        }




        private IGetCompanyUserInfoService _getCompanyUserInfoService;
        public IGetCompanyUserInfoService GetCompanyUserInfoService
        {
            get
            {
                return _getCompanyUserInfoService = _getCompanyUserInfoService ?? new GetCompanyUserInfoService(_context);
            }
        }

        private IAddNewCompanyUserServices _addNewCompanyUserServices;
        public IAddNewCompanyUserServices AddNewCompanyUserServices
        {
            get
            {
                return _addNewCompanyUserServices = _addNewCompanyUserServices ?? new AddNewCompanyUserServices(_context/*, _environment*/);
            }
        }
        private IAddNewCompanyService _addNewCompanyService;
        public IAddNewCompanyService AddNewCompanyService
        {
            get
            {
                return _addNewCompanyService = _addNewCompanyService ?? new AddNewCompanyService(_context/*, _environment*/);
            }
        }

        private IAddNewCompanyFinancialCycleServices _addNewCompanyFinancialCycleServices;
        public IAddNewCompanyFinancialCycleServices AddNewCompanyFinancialCycleServices
        {
            get
            {
                return _addNewCompanyFinancialCycleServices = _addNewCompanyFinancialCycleServices ?? new AddNewCompanyFinancialCycleServices(_context/*, _environment*/);
            }
        }

        private IAddNewCompanyBranchServices _addNewCompanyBranchServices;
        public IAddNewCompanyBranchServices AddNewCompanyBranchServices
        {
            get
            {
                return _addNewCompanyBranchServices = _addNewCompanyBranchServices ?? new AddNewCompanyBranchServices(_context);
            }
        }



        private IGetCompanyService _getCompanyService;
        public IGetCompanyService GetCompanyService
        {
            get
            {
                return _getCompanyService = _getCompanyService ?? new GetCompanyService(_context);
            }
        }

        private IGetCompanyInfoService _getCompanyInfoService;
        public IGetCompanyInfoService GetCompanyInfoService
        {
            get
            {
                return _getCompanyInfoService = _getCompanyInfoService ?? new GetCompanyInfoService(_context);
            }
        }

    }
}
