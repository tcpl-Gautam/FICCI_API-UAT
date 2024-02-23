using FICCI_API.DTO;
using FICCI_API.ModelsEF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FICCI_API.Controller.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApproveInvoiceController : BaseController
    {
        private readonly FICCI_DB_APPLICATIONSContext _dbContext;
        public ApproveInvoiceController(FICCI_DB_APPLICATIONSContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string email)
        {
            try
            {
                if (email == null)
                {
                    var response = new
                    {
                        status = true,
                        message = "Email is Mandatory field",
                    };
                    return Ok(response);
                }

                var list = await _dbContext.VwInvoiceApprovalLists.Where(x => x.ApproverEmail == email).ToListAsync();
                var res = new
                {
                    status = true,
                    message = "Approval list is successfully fetched.",
                    data = list
                };
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "An error occurred while fetching the list of Approval." });

            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(ApproveInvoice cust)
        {
            ApproverInvoice_Crud crud = new ApproverInvoice_Crud();

            try
            {
                var res = await _dbContext.GetProcedures().prc_Approval_InvoiceAsync(cust.HeaderId.ToString(), cust.IsApproved, cust.LoginId, cust.StatusId, cust.Remarks);
                //if (res[0].returncode == 1)
                //{
                //    var result = await _dbContext.FicciErpCustomerDetails.Where(x => x.CustomerId == Convert.ToInt32(res[0].CustomerId)).FirstOrDefaultAsync();

                //    string htmlbody = htmlBody(res[0].Status, result.CusotmerNo, result.CustomerName, result.CityCode, result.CustomerPanNo, result.CustomerGstNo);
                //    //  SendEmail(res[0].InitiatedBy, result.CustomerEmailId, "http", $"New Customer Assigned for Approval : {result.CustomerName}", htmlbody,);

                //}
                crud.status = res[0].returncode == 1 ? true : false;
                crud.message = res[0].Message;
                return StatusCode(200, crud);

            }
            catch (Exception ex)
            {
                crud.status = false;
                crud.message = ex.InnerException.Message.ToString();
                return StatusCode(500, crud);
            }
        }
    }
}
