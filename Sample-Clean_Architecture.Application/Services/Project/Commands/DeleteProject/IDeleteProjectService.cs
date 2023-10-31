using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Project.Commands.DeleteProject
{
    public interface IDeleteProjectService
    {
        ResultDto Execute(int projects_id);
    }

    public class DeleteProjectService : IDeleteProjectService
    {
        private readonly IDatabaseContext _context;


        public DeleteProjectService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto Execute(int projects_id)
        {
            try
            {
                bool error = false;
                if (_context.sp_Projects_Delete(projects_id, out error) == 2)
                {
                    if (error == true)
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
                            Message = AppMessages.UNABLE_DELETE,
                        };

                    }
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
