﻿using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Users.Commands.UserAccess
{
    public interface IUserAccessService
    {
        ResultDto Execute(int CompanyUsers_Id, string MenuOptions_Ids);
    }
    public class UserAccessService : IUserAccessService
    {
        private readonly IDatabaseContext _context;


        public UserAccessService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(int CompanyUsers_Id, string MenuOptions_Ids)
        {
            try
            {


                if (_context.Sp_MenuOptionsUsers_Insert(CompanyUsers_Id, MenuOptions_Ids) == 1)
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
}
