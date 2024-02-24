using FICCI_API.DTO;
using FICCI_API.Models;
using FICCI_API.ModelsEF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FICCI_API.Controller.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApproveInvoiceController : BaseController
    {
        private readonly FICCI_DB_APPLICATIONSContext _dbContext;
        private readonly MySettings _mySettings;
        public ApproveInvoiceController(FICCI_DB_APPLICATIONSContext dbContext, IOptions<MySettings> mySettings) : base(dbContext)
        {
            _dbContext = dbContext;
            _mySettings = mySettings.Value;
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
                string htmlbody = "";
                var res = await _dbContext.GetProcedures().prc_Approval_InvoiceAsync(cust.HeaderId.ToString(), cust.IsApproved, cust.LoginId, cust.StatusId, cust.Remarks);
                if (res[0].returncode == 1)
                {
                   // var result = await _dbContext.FicciErpCustomerDetails.Where(x => x.CustomerId == Convert.ToInt32(res[0].CustomerId)).FirstOrDefaultAsync();
                  //  string subject = "";
                    //if (cust.IsApproved)
                    //{
                    //    if (cust.StatusId == 2)
                    //    {
                    //        subject = "New Customer Approved by TL : " + result.CustomerName + "";
                    //        htmlbody = ApprovalhtmlBody(res[0].Status, _mySettings.Website_link, result.CusotmerNo, result.CustomerName, result.CityCode, result.CustomerPanNo, result.CustomerGstNo);
                    //        SendEmail(result.CustomerClusterApprover, result.CustomerEmailId, subject, htmlbody, _mySettings);
                    //    }
                    //    else if (cust.StatusId == 3)
                    //    {
                    //        subject = "New Customer Approved by CH : " + result.CustomerName + "";
                    //        htmlbody = ApprovalhtmlBody(res[0].Status, _mySettings.Website_link, result.CusotmerNo, result.CustomerName, result.CityCode, result.CustomerPanNo, result.CustomerGstNo);
                    //        SendEmail(res[0].InitiatedBy, result.CustomerEmailId, subject, htmlbody, _mySettings);
                    //    }
                    //    else if (cust.StatusId == 11)
                    //    {
                    //        subject = "New Customer Approved by Finance : " + result.CustomerName + "";
                    //        htmlbody = ApprovalhtmlBody("approved by Finance approver", _mySettings.Website_link, result.CusotmerNo, result.CustomerName, result.CityCode, result.CustomerPanNo, result.CustomerGstNo);
                    //        SendEmail(result.CustomerEmailId, "" + result.CustomerTlApprover + "," + result.CustomerClusterApprover + "," + result.CustomerSgApprover + "", subject, htmlbody, _mySettings);
                    //        return StatusCode(200, crud);
                    //    }
                    //    else if (cust.StatusId == 5)
                    //    {
                    //        subject = "New Customer Approved by Account : " + result.CustomerName + "";
                    //        htmlbody = ApprovalhtmlBody("approved by Accounts approver", _mySettings.Website_link, result.CusotmerNo, result.CustomerName, result.CityCode, result.CustomerPanNo, result.CustomerGstNo);
                    //        SendEmail(result.CustomerEmailId, "" + result.CustomerTlApprover + "," + result.CustomerClusterApprover + "," + result.CustomerSgApprover + "", subject, htmlbody, _mySettings);
                    //        return StatusCode(200, crud);
                    //    }

                    //}
                    //if (!cust.IsApproved)
                    //{
                    //    if (cust.StatusId == 2)
                    //    {
                    //        subject = "New Customer Rejected by TL : " + result.CustomerName + "";
                    //    }
                    //    else if (cust.StatusId == 3)
                    //    {
                    //        subject = "New Customer Rejected by CH : " + result.CustomerName + "";
                    //    }
                    //    else if (cust.StatusId == 5)
                    //    {
                    //        subject = "New Customer Rejected by Account : " + result.CustomerName + "";
                    //    }
                    //    htmlbody = ApprovalhtmlBody("rejected by the approver", _mySettings.Website_link, result.CusotmerNo, result.CustomerName, result.CityCode, result.CustomerPanNo, result.CustomerGstNo);
                    //    SendEmail(result.CustomerEmailId, "" + result.CustomerTlApprover + "," + result.CustomerClusterApprover + "," + result.CustomerSgApprover + "", subject, htmlbody, _mySettings);
                    //    return StatusCode(200, crud);
                    //}



                }
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
