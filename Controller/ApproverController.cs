using FICCI_API.ModelsEF;
using Microsoft.AspNetCore.Mvc;

namespace FICCI_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApproverController : BaseController
    {
        private readonly FICCI_DB_APPLICATIONSContext _dbContext;
        public ApproverController(FICCI_DB_APPLICATIONSContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
