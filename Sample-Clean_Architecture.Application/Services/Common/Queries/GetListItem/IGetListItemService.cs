using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Application.Services.Account.Commands.AddNewAccountGroup;
using Sample_Clean_Architecture.Application.Services.Account.Commands.AddNewAccountLedger;
using Sample_Clean_Architecture.Application.Services.Companies.Queries.GetCompanies;
using Sample_Clean_Architecture.Application.Services.CostCenter.Commands.AddNewCostCenter;
using Sample_Clean_Architecture.Application.Services.ExchangeRate.Queries.GetExchangeRate;
using Sample_Clean_Architecture.Application.Services.MemoryCash;
using Sample_Clean_Architecture.Application.Services.Project.Commands.AddNewProject;
using Sample_Clean_Architecture.Application.Services.SuffixPrefix.Queries.LoadSuffixPrefix;
using Sample_Clean_Architecture.Application.Services.Vouchers.JournalVoucher.Queries.LoadJournalVoucher;
using Sample_Clean_Architecture.Application.Services.Vouchers.PaymentVoucher.Queries.LoadPaymentlVoucher;
using Sample_Clean_Architecture.Common.Dtos;
using static Sample_Clean_Architecture.Common.Enums;

namespace Sample_Clean_Architecture.Application.Services.Common.Queries.GetListItem
{
    public interface IGetListItemService
    {
        ResultDto<List<ListItemDto>> Execute(ListType listType, object list = null);
    }

    public class GetListItemService : IGetListItemService
    {
        private readonly IDatabaseContext _context;
        private IFlushableMemoryCache _memoryCache;
        public GetListItemService(IDatabaseContext context, IFlushableMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;

        }
        public ResultDto<List<ListItemDto>> Execute(ListType listType, object list = null)
        {
            bool isSuccess = true;
            List<ListItemDto> items = new List<ListItemDto>();
            switch (listType)
            {
                case ListType.AccountLedgerList:
                    if (!_memoryCache.TryGetValue("AccountLedgerList", out items))
                    {

                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (AccountLedger item in (List<AccountLedger>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.Ledger_Id, Description = item.Ledger_Name });
                            }
                            items.Insert(0, new ListItemDto() { Id = 0, Description = "                     " });
                            _memoryCache.Set("AccountLedgerList", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            //Try Get List From Database 
                            // _memoryCache.Set("CategoryList", _context.Category_Get().ToList(), TimeSpan.FromHours(1));
                            // items = new List<ListItemDto>() {new ListItemDto() { Id = 0, Description = "NA" }, new ListItemDto() { Id = 1, Description = "Cat1" },
                            // new ListItemDto() {Id=2,Description="Cat2" }};

                            isSuccess = false;
                        }

                    }

                    break;
                case ListType.PaymentReceiptAccountList:
                    if (!_memoryCache.TryGetValue("PaymentReceiptAccountList", out items))
                    {

                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (PaymentReceiptAccount item in (List<PaymentReceiptAccount>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.Ledger_Id, Description = item.Ledger_Name });
                            }
                            items.Insert(0, new ListItemDto() { Id = 0, Description = "                     " });
                            _memoryCache.Set("PaymentReceiptAccountList", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            //Try Get List From Database 
                            // _memoryCache.Set("CategoryList", _context.Category_Get().ToList(), TimeSpan.FromHours(1));
                            // items = new List<ListItemDto>() {new ListItemDto() { Id = 0, Description = "NA" }, new ListItemDto() { Id = 1, Description = "Cat1" },
                            // new ListItemDto() {Id=2,Description="Cat2" }};

                            isSuccess = false;
                        }

                    }

                    break;
                case ListType.CompanyBranchList:
                    if (!_memoryCache.TryGetValue("CompanyBranchList", out items))
                    {
                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (CompanyBranch item in (List<CompanyBranch>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.CompanyBranch_Id, Description = item.CompanyBranch_Title });
                            }
                            _memoryCache.Set("CompanyBranchList", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            isSuccess = false;
                        }

                    }
                    break;
                case ListType.CostCenterList:
                    if (!_memoryCache.TryGetValue("CostCenterList", out items))
                    {
                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (CostCenterDto item in (List<CostCenterDto>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.CostCenter_Id, Description = item.CostCenter_Name });
                            }
                            items.Insert(0, new ListItemDto() { Id = 0, Description = "NA" });
                            _memoryCache.Set("CostCenterList", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            isSuccess = false;
                        }

                    }
                    break;

                case ListType.CurrencyCompanyListWithoutDefault:
                    if (!_memoryCache.TryGetValue("CurrencyCompanyListWithoutDefault", out items))
                    {
                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (CurrencyCompany item in (List<CurrencyCompany>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.Currency_Id, Description = item.Currency_Name, Tag = item.Rate.ToString() });
                            }
                            _memoryCache.Set("CurrencyCompanyListWithoutDefault", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            isSuccess = false;
                        }

                    }
                    break;
                case ListType.CurrencyCompanyList:
                    if (!_memoryCache.TryGetValue("CurrencyCompanyList", out items))
                    {
                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (CurrencyCompany item in (List<CurrencyCompany>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.Currency_Id, Description = item.Currency_Name, Tag = item.Rate.ToString() });
                            }
                            _memoryCache.Set("CurrencyCompanyList", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            isSuccess = false;
                        }

                    }
                    break;
                case ListType.ProjectList:
                    if (!_memoryCache.TryGetValue("ProjectList", out items))
                    {
                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (Vouchers.JournalVoucher.Queries.LoadJournalVoucher.Project item in (List<Vouchers.JournalVoucher.Queries.LoadJournalVoucher.Project>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.Projects_Id, Description = item.Projects_Name });
                            }
                            _memoryCache.Set("ProjectList", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            isSuccess = false;
                        }

                    }
                    break;
                case ListType.VoucherTypeList:
                    if (!_memoryCache.TryGetValue("VoucherTypeList", out items))
                    {
                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (VoucherType item in (List<VoucherType>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.VoucherLabel_Id, Description = item.VoucherLabel_Title });
                            }
                            items.Insert(0, new ListItemDto() { Id = 0, Description = "NA" });
                            _memoryCache.Set("VoucherTypeList", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            isSuccess = false;
                        }

                    }
                    break;
                case ListType.VoucherTypes:
                    if (!_memoryCache.TryGetValue("VoucherTypes", out items))
                    {
                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (VoucherTypeDto item in (List<VoucherTypeDto>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.voucherType_Id, Description = item.voucherType_Name });
                            }

                            _memoryCache.Set("VoucherTypes", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            isSuccess = false;
                        }

                    }
                    break;
                case ListType.CurrencyList:
                    if (!_memoryCache.TryGetValue("CurrencyList", out items))
                    {
                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (ProjectStatusDto item in (List<ProjectStatusDto>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.Status_Id, Description = item.Status_Description });
                            }

                            _memoryCache.Set("CurrencyList", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            isSuccess = false;
                        }

                    }
                    break;

                case ListType.TermsAndConditionList:
                    if (!_memoryCache.TryGetValue("TermsAndConditionList", out items))
                    {
                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (TermsAndCondition item in (List<TermsAndCondition>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.TermsAndCondition_Id, Description = item.TermsAndCondition_Name });
                            }

                            _memoryCache.Set("TermsAndConditionList", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            isSuccess = false;
                        }

                    }
                    break;
                case ListType.CostCenterStatusList:
                    if (!_memoryCache.TryGetValue("CostCenterStatusList", out items))
                    {
                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (CostCenterStatusDto item in (List<CostCenterStatusDto>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.Status_Id, Description = item.Status_Description });
                            }

                            _memoryCache.Set("CostCenterStatusList", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            isSuccess = false;
                        }

                    }
                    break;

                case ListType.Natures:
                    if (!_memoryCache.TryGetValue("Natures", out items))
                    {
                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (Nature item in (List<Nature>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.Nature_Id, Description = item.Nature_Description });
                            }

                            _memoryCache.Set("Natures", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            isSuccess = false;
                        }

                    }
                    break;
                case ListType.AccountGroups:
                    if (!_memoryCache.TryGetValue("AccountGroups", out items))
                    {
                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (AccountGroup item in (List<AccountGroup>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.AccountGroup_Id, Description = item.AccountGroup_Name });
                            }

                            _memoryCache.Set("AccountGroups", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            isSuccess = false;
                        }

                    }
                    break;


                case ListType.ProjectStatusList:
                    if (!_memoryCache.TryGetValue("ProjectStatusList", out items))
                    {
                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (ProjectStatusDto item in (List<ProjectStatusDto>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.Status_Id, Description = item.Status_Description });
                            }

                            _memoryCache.Set("ProjectStatusList", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            isSuccess = false;
                        }

                    }
                    break;
                case ListType.CountryList:
                    if (!_memoryCache.TryGetValue("CountryList", out items))
                    {
                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (CountryDto item in (List<CountryDto>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.Country_Id, Description = item.Country_Name });
                            }

                            _memoryCache.Set("CountryList", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            isSuccess = false;
                        }

                    }
                    break;
                case ListType.ExchangeRateInfoDetail:
                    if (!_memoryCache.TryGetValue("ExchangeRateInfoDetail", out items))
                    {
                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (ExchangeRateInfoDetail_Dto item in (List<ExchangeRateInfoDetail_Dto>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.Currency_Id, Description = item.Currency_Name });
                            }

                            _memoryCache.Set("ExchangeRateInfoDetail", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            isSuccess = false;
                        }

                    }
                    break;
                case ListType.CountryCurrencyList:
                    if (!_memoryCache.TryGetValue("CountryCurrencyList", out items))
                    {
                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (CountryDto item in (List<CountryDto>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.Country_Id, Description = item.Currency_Name });
                            }

                            _memoryCache.Set("CountryCurrencyList", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            isSuccess = false;
                        }

                    }
                    break;
                case ListType.CurrencySubunitList:
                    if (!_memoryCache.TryGetValue("CurrencySubunitList", out items))
                    {
                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (CountryDto item in (List<CountryDto>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.Country_Id, Description = item.Currency_Subunit });
                            }

                            _memoryCache.Set("CurrencySubunitList", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            isSuccess = false;
                        }

                    }
                    break;
                case ListType.DateFormatList:
                    if (!_memoryCache.TryGetValue("DateFormatList", out items))
                    {
                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (DateFormatDto item in (List<DateFormatDto>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.DateFormats_Id, Description = item.DateFormats_Description });
                            }

                            _memoryCache.Set("DateFormatList", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            isSuccess = false;
                        }

                    }
                    break;
                case ListType.DefaultLedgerList:
                    if (!_memoryCache.TryGetValue("DefaultLedgerList", out items))
                    {
                        if (list != null)
                        {
                            items = new List<ListItemDto>();
                            foreach (DefaultLedgerDto item in (List<DefaultLedgerDto>)list)
                            {
                                items.Add(new ListItemDto() { Id = item.Id, Description = item.Title });
                            }

                            _memoryCache.Set("DefaultLedgerList", items, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            isSuccess = false;
                        }

                    }
                    break;
                case ListType.DrCr:
                    if (!_memoryCache.TryGetValue("DrCrList", out items))
                    {
                        items = new List<ListItemDto>();
                        items.Add(new ListItemDto() { Id = 0, Description = "" });
                        items.Add(new ListItemDto() { Id = 1, Description = "Dr" });
                        items.Add(new ListItemDto() { Id = 2, Description = "Cr" });
                        _memoryCache.Set("DrCrList", items, TimeSpan.FromHours(1));
                    }
                    break;
            }



            return new ResultDto<List<ListItemDto>>()
            {
                Data = items,
                IsSuccess = isSuccess,
            };
        }
    }
    public class ListItemDto
    {
        public long Id { set; get; }
        public string Description { set; get; }
        public string Tag { set; get; }
    }
}
