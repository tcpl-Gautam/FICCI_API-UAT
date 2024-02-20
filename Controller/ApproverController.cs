using FICCI_API.DTO;
using FICCI_API.Models;
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
        //[HttpPost]
        //public async Task<IActionResult> Post(ApproveCustomer cust)
        //{
        //    try
        //    {
        //        ApproverCustomer_Crud crud = new ApproverCustomer_Crud();
        //        var res = await _dbContext.GetProcedures().prc_Approval_CustomerAsync(cust.CustomerId.ToString(), cust.IsApproved, cust.LoginId, cust.StatusId, cust.Remarks);
               



        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}
