using FICCI_API.DTO;
using FICCI_API.ModelsEF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FICCI_API.Controller.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : BaseController
    {
        private readonly FICCI_DB_APPLICATIONSContext _dbContext;
        public ProjectController(FICCI_DB_APPLICATIONSContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id=0)
        {
            var result = new ProjectDTO();
            var resu = new List<AllProjectList>();
            try
            {
                if (id > 0)
                {
                    result = await _dbContext.FicciErpProjectDetails.Where(x => x.IsDelete != true && x.ProjectActive != false)
                        .Select(project => new ProjectDTO
                        {
                            ProjectId = project.ProjectId,
                            ProjectCode = project.ProjectNo,
                            GST = project.ProjectGst ?? "",
                            PAN = project.ProjectPan ?? "",
                            Department = project.ProjectDepartment,
                            Division = project.ProjectDivision,

                        }).FirstOrDefaultAsync(x => x.ProjectId == id);

                    if(result == null)
                    {
                        var respons = new
                        {
                            status = false,
                            message = "No Projects found for the given Id",
                            data = result
                        };
                        return NotFound(respons);
                    }
                    var response = new
                    {
                        status = true,
                        message = "Project Detail fetch successfully",
                        data = result
                    };
                    return Ok(response);
                }
                else if(id == 0)
                {
                    resu = await _dbContext.FicciErpProjectDetails.Where(x => x.IsDelete != true && x.ProjectActive != false)
                    .Select(project => new AllProjectList
                    {
                        ProjectId = project.ProjectId,
                        ProjectName = project.ProjectName,
                        ProjectCode = project.ProjectNo
                    })
                    .ToListAsync();
                    if (resu.Count <= 0)
                    {
                        var respons = new
                        {
                            status = false,
                            message = "No Projects found for the given Id",
                            data = result
                        };
                        return NotFound(respons);
                    }
                    var response = new
                    {
                        status = true,
                        message = "List fetch successfully",
                        data = resu
                    };

                    return Ok(response);
                }
                else
                {
                    var response = new
                    {
                        status = false,
                        message = "Invalid Id",
                        data = resu
                    };
                    return NotFound(response);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "An error occurred while fetching the detail of projects." });
            }
        }
    }
}
