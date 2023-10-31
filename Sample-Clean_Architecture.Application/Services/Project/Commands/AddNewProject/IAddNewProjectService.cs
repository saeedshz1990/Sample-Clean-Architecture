using Sample_Clean_Architecture.Application.Interfaces.Contexts;
using Sample_Clean_Architecture.Application.Services.CostCenter.Commands.AddNewCostCenter;
using Sample_Clean_Architecture.Common;
using Sample_Clean_Architecture.Common.Dtos;

namespace Sample_Clean_Architecture.Application.Services.Project.Commands.AddNewProject
{
    public interface IAddNewProjectService
    {
        ResultDto Execute(int Company_Id, ProjectDto project);
    }
    public class AddNewProjectService : IAddNewProjectService
    {
        private readonly IDatabaseContext _context;

        public AddNewProjectService(IDatabaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(int Company_Id, ProjectDto project)
        {
            try
            {
                if (_context.sp_Project_Insert(Company_Id, project) == 2)
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

    public class ProjectDto
    {
        public int Projects_Id { get; set; }
        public string Projects_Number { get; set; }
        public string Projects_Name { get; set; }
        public string Projects_Description { get; set; }
        public DateTime Projects_StartDate { get; set; }
        public DateTime Projects_EndDate { get; set; }
        public int CostCenter_Id { get; set; }
        public byte Projects_Status { get; set; }
        public List<CostCenterDto> CostCenterList { get; set; }

        public List<ProjectStatusDto> ProjectStatusList { get; set; }

        public ProjectDto()
        {
            Projects_Id = 0;
            Projects_Number = "";
            Projects_Name = "";
            Projects_Description = "";
            CostCenter_Id = 0;
            Projects_Status = 0;
            CostCenterList = new List<CostCenterDto>();
            ProjectStatusList = new List<ProjectStatusDto>();
        }
    }

    public class ProjectStatusDto
    {
        public byte Status_Id { get; set; }
        public string Status_Description { get; set; }
    }
}
