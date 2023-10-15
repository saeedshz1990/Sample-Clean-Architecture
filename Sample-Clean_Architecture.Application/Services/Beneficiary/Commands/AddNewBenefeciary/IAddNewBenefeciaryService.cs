using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Beneficiary.Commands.AddNewBenefeciary
{
    public interface IAddNewBenefeciaryService
    {
        ResultDto Execute(Benefeciary_Dto request);
    }

    public class AddNewBenefeciaryService : IAddNewBenefeciaryService
    {
        private readonly IDatabaseContext _context;


        public AddNewBenefeciaryService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(Benefeciary_Dto request)
        {
            try
            {
                if (_context.sp_Beneficiary_Insert(request) == 1)
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

    public class Benefeciary_Dto
    {
        public int Beneficiary_Id { get; set; }
        public int Company_Id { get; set; }
        public string Beneficiary_Name { get; set; } = string.Empty;
        public string Beneficiary_Mobile { get; set; } = string.Empty;
        public string Beneficiary_Passport { get; set; } = string.Empty;
        public string Beneficiary_RefNo { get; set; } = string.Empty;
        public string Beneficiary_Remark { get; set; } = string.Empty;
        public string Beneficiary_IdNumber { get; set; } = string.Empty;
    }
}
