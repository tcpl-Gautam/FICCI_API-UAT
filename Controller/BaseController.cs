
using Microsoft.AspNetCore.Mvc;
using FICCI_API.ModelsEF;
namespace FICCI_API.Controller
{
    public class BaseController : ControllerBase
    {
      public readonly FICCI_DB_APPLICATIONSContext _context;
        public BaseController(FICCI_DB_APPLICATIONSContext context)
        {
            this._context = context;
        }   
    }
}
