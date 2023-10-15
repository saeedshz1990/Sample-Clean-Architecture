using Demo.Application.Interfaces.Contexts;
using Demo.Common;
using Demo.Common.Dtos;
using Demo.Domain.Entities.Companies;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.Companies.Commands.AddNewCompany
{
    public interface IAddNewCompanyService
    {
        ResultDto Execute(RequestCompanyDto request);
    }
    public class AddNewCompanyService : IAddNewCompanyService
    {
        private readonly IDatabaseContext _context;


        public AddNewCompanyService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(RequestCompanyDto request)
        {
            try
            {


                if (_context.Sp_Company_Insert(request) == 1)
                {
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = AppMessages.SUCCESS,
                    };
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = AppMessages.ERROR,
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResultDto
                {
                    IsSuccess = false,
                    Message = AppMessages.ERROR,
                };
            }

        }
    }

    public class RequestCompanyDto
    {
        public int Company_Id { get; set; }
        public int Accounts_Id { get; set; }
        public string Company_BusinessName { get; set; }
        public string Company_AliasName { get; set; }
        public string Company_Address { get; set; }
        public string Company_PhoneNo { get; set; }
        public string Company_Fax { get; set; }
        public string Company_Email { get; set; }
        public string Company_Mobile { get; set; }
        public string Company_WebAddress { get; set; }
        public byte DateFormats_Id { get; set; }
        public byte Company_DateSeperator { get; set; }
        // public byte TimeZone { get; set; }
        public string Company_PostalCode { get; set; }
        public byte Country_Id { get; set; }
        public byte Company_TransactionType { get; set; }
        public byte[] Company_Logo { get; set; }
        public string Company_Tax1 { get; set; }
        public string Company_Tax2 { get; set; }
        public string Company_Tax3 { get; set; }

        public string Currency_Symbol { get; set; }

        public string Currency_Subunit { get; set; }

        public byte Country_Currency_Id { get; set; }
        public string Currency_Name { get; set; }

        public DateTime FinancialCycle_FromDate { get; set; }
        public bool Company_LedgerInserted { get; set; }
        public int DefaultLedger_Id { get; set; }

        public bool Company_CurrencyAutoupdate { get; set; }
        public RequestCompanyDto()
        {
            FinancialCycle_FromDate = DateTime.Now;

        }
    }

}
