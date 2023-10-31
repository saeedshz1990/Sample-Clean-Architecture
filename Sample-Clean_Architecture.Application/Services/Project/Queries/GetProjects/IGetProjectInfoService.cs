using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Application.Services.Project.Commands.AddNewProject;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Project.Queries.GetProjects
{
    public interface IGetProjectInfoService
    {
        ResultDto<ProjectDto> Execute(int Company_Id, int Project_Id);
    }
    public class GetProjectInfoService : IGetProjectInfoService
    {
        private readonly IDatabaseContext _context;

        public GetProjectInfoService(IDatabaseContext context)
        {
            _context = context;

        }
        public ResultDto<ProjectDto> Execute(int Company_Id, int Project_Id)
        {
            var project = _context.sp_Projects_GetById(Company_Id, Project_Id);

            return new ResultDto<ProjectDto>()
            {
                Data = project,
                IsSuccess = true,
                Message = AppMessages.SUCCESS,
            };
        }
    }
}
