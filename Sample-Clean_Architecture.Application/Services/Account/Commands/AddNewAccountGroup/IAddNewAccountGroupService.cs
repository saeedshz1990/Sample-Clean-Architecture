using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Account.Commands.AddNewAccountGroup
{
    public interface IAddNewAccountGroupService
    {
        ResultDto Execute(RequestAccountGroup request);
    }
    public class AddNewAccountGroupService : IAddNewAccountGroupService
    {
        private readonly IDatabaseContext _context;


        public AddNewAccountGroupService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(RequestAccountGroup request)
        {
            try
            {
                if (_context.sp_AccountGroup_Insert(request) == 2)
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

    public class RequestAccountGroupDto
    {
        public RequestAccountGroup RequestAccountGroup { get; set; }
        //public RequestAccountGroupParent RequestAccountGroupParent { get; set; }
        public List<Nature> Natures { get; set; }
        public List<AccountGroup> AccountGroups { get; set; }

        public bool HasChild { get; set; }
        public RequestAccountGroupDto()
        {
            RequestAccountGroup = new RequestAccountGroup();
            Natures = new List<Nature>();
            AccountGroups = new List<AccountGroup>();
            //RequestAccountGroupParent = new RequestAccountGroupParent();
        }

    }
    public class RequestAccountGroup
    {
        public int AccountGroup_Id { get; set; }
        public int AccountGroup_Parent { get; set; }

        public string AccountGroup_Name { get; set; }
        public string AccountGroup_Narration { get; set; }
        public byte Nature_Id { get; set; }

        public int Company_Id { get; set; }
        public bool GrossProfit { get; set; }

    }
    public class RequestAccountGroupParent
    {
        public int AccountGroup_Parent { get; set; }
        public byte Nature_Id { get; set; }
        public bool GrossProfit { get; set; }
    }
    public class Nature
    {
        public int Nature_Id { get; set; }
        public string Nature_Description { get; set; }
    }
    public class AccountGroup
    {
        public int AccountGroup_Id { get; set; }
        public string AccountGroup_Name { get; set; }

    }
}
