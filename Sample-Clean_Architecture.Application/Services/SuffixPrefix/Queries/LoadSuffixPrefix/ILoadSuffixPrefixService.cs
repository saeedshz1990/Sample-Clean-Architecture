using Demo.Application.Interfaces.Contexts;
using Demo.Application.Services.SuffixPrefix.Queries.GetSuffixPrefix;
using Demo.Common;
using Demo.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Clean_Architecture.Application.Services.SuffixPrefix.Queries.LoadSuffixPrefix
{
    public interface ILoadSuffixPrefixService
    {
        ResultDto<List<VoucherTypeDto>> Execute();

    }
    public class LoadSuffixPrefixService : ILoadSuffixPrefixService
    {
        private readonly IDatabaseContext _context;

        public LoadSuffixPrefixService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<List<VoucherTypeDto>> Execute()
        {
            var data = _context.sp_SuffixPrefix_Load();

            return new ResultDto<List<VoucherTypeDto>>()
            {
                Data = data,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }

    public class VoucherTypeDto
    {
        public int voucherType_Id { get; set; }
        public string voucherType_Name { get; set; }
    }
}
