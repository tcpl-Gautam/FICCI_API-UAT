using FICCI_API.Models;
using FICCI_API.ModelsEF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FICCI_API.Controller.API
{

    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseInvoice_NewController : BaseController
    {
        private readonly FICCI_DB_APPLICATIONSContext _dbContext;
        public PurchaseInvoice_NewController(FICCI_DB_APPLICATIONSContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> POST([FromForm] PurchaseInvoice_Request request)
        {
            PurchaseInvoice_New purchaseInvoice_New = new PurchaseInvoice_New();
            try
            {
                if (request != null)
                {
                    if (!request.isupdate)
                    {


                        FicciImpiHeader ficciImpiHeader = new FicciImpiHeader();
                        ficciImpiHeader.ImpiHeaderInvoiceType = request.ImpiHeaderInvoiceType;
                        ficciImpiHeader.ImpiHeaderProjectCode = request.ImpiHeaderProjectCode;
                        ficciImpiHeader.ImpiHeaderDepartment = request.ImpiHeaderDepartment;
                        ficciImpiHeader.ImpiHeaderDivison = request.ImpiHeaderDivison;
                        ficciImpiHeader.ImpiHeaderPanNo = request.ImpiHeaderPanNo;
                        ficciImpiHeader.ImpiHeaderGstNo = request.ImpiHeaderGstNo;
                        ficciImpiHeader.ImpiHeaderCustomerName = request.ImpiHeaderCustomerName;
                        ficciImpiHeader.ImpiHeaderCustomerCode = request.ImpiHeaderCustomerCode;
                        ficciImpiHeader.ImpiHeaderCustomerAddress = request.ImpiHeaderCustomerAddress;
                        ficciImpiHeader.ImpiHeaderCustomerCity = request.ImpiHeaderCustomerCity;
                        ficciImpiHeader.ImpiHeaderCustomerState = request.ImpiHeaderCustomerState;
                        ficciImpiHeader.ImpiHeaderCustomerPinCode = request.ImpiHeaderCustomerPinCode;
                        ficciImpiHeader.ImpiHeaderCustomerGstNo = request.ImpiHeaderCustomerGstNo;
                        ficciImpiHeader.ImpiHeaderCustomerContactPerson = request.ImpiHeaderCustomerContactPerson;
                        ficciImpiHeader.ImpiHeaderCustomerEmailId = request.ImpiHeaderCustomerEmailId;
                        ficciImpiHeader.ImpiHeaderCustomerPhoneNo = request.ImpiHeaderCustomerPhoneNo;
                        ficciImpiHeader.ImpiHeaderCreatedBy = request.ImpiHeaderCreatedBy;
                        ficciImpiHeader.ImpiHeaderCreatedOn = DateTime.Now;
                        ficciImpiHeader.ImpiHeaderActive = true;
                        ficciImpiHeader.ImpiHeaderTotalInvoiceAmount = request.ImpiHeaderTotalInvoiceAmount;
                        if (request.ImpiHeaderAttachment != null)
                        {
                            ficciImpiHeader.ImpiHeaderAttachment = UploadFile(request.ImpiHeaderAttachment);
                        }
                        ficciImpiHeader.ImpiHeaderPaymentTerms = request.ImpiHeaderPaymentTerms;
                        ficciImpiHeader.ImpiHeaderRemarks = request.ImpiHeaderRemarks;
                        ficciImpiHeader.ImpiHeaderStatus = request.IsDraft == true ? "Draft" : "Pending";
                        ficciImpiHeader.IsDraft = request.IsDraft;
                        ficciImpiHeader.ImpiHeaderPiNo = DateTime.Now.ToString("yyyyMMddhhmmss");
                        ficciImpiHeader.ImpiHeaderSubmittedDate = DateTime.Now;
                      

                        _dbContext.Add(ficciImpiHeader);
                        _dbContext.SaveChanges();
                        int returnid = ficciImpiHeader.ImpiHeaderId;
                        if (returnid != 0 && request.lineItem_Requests.Count > 0)
                        {
                            foreach (var k in request.lineItem_Requests)
                            {
                                FicciImpiLine FicciImpiLine = new FicciImpiLine();
                                FicciImpiLine.ImpiLineDescription = k.impiLineDescription;
                                FicciImpiLine.ImpiLineQuantity = k.ImpiLineQuantity;
                                FicciImpiLine.ImpiLineUnitPrice = k.ImpiLineUnitPrice;
                                FicciImpiLine.ImpiLineDiscount = k.ImpiLineDiscount;
                                FicciImpiLine.ImpiLineAmount = k.ImpiLineAmount;
                                FicciImpiLine.ImpiLineActive = true;
                                FicciImpiLine.ImpiLineCreatedBy = request.ImpiHeaderCreatedBy;
                                FicciImpiLine.ImpiLineCreatedOn = DateTime.Now;
                                FicciImpiLine.PiHeaderId = returnid;
                                FicciImpiLine.IsDeleted = false;
                                FicciImpiLine.ImpiLinePiNo = DateTime.Now.ToString("yyyyMMddhhmmss");
                                _dbContext.Add(FicciImpiLine);
                                _dbContext.SaveChanges();


                            }

                        }
                        purchaseInvoice_New.Status = true;
                        purchaseInvoice_New.Message = "Purchase Invoice Submit Successfully";
                        return StatusCode(200, purchaseInvoice_New);
                    }
                    else
                    {
                        var data = await _dbContext.FicciImpiHeaders.Where(m => m.ImpiHeaderId == request.headerid).FirstOrDefaultAsync();
                        if (data != null)
                        {
                            //FicciImpiHeader ficciImpiHeader = new FicciImpiHeader();
                            data.ImpiHeaderInvoiceType = request.ImpiHeaderInvoiceType;
                            data.ImpiHeaderProjectCode = request.ImpiHeaderProjectCode;
                            data.ImpiHeaderDepartment = request.ImpiHeaderDepartment;
                            data.ImpiHeaderDivison = request.ImpiHeaderDivison;
                            data.ImpiHeaderPanNo = request.ImpiHeaderPanNo;
                            data.ImpiHeaderGstNo = request.ImpiHeaderGstNo;
                            data.ImpiHeaderCustomerName = request.ImpiHeaderCustomerName;
                            data.ImpiHeaderCustomerCode = request.ImpiHeaderCustomerCode;
                            data.ImpiHeaderCustomerAddress = request.ImpiHeaderCustomerAddress;
                            data.ImpiHeaderCustomerCity = request.ImpiHeaderCustomerCity;
                            data.ImpiHeaderCustomerState = request.ImpiHeaderCustomerState;
                            data.ImpiHeaderCustomerPinCode = request.ImpiHeaderCustomerPinCode;
                            data.ImpiHeaderCustomerGstNo = request.ImpiHeaderCustomerGstNo;
                            data.ImpiHeaderCustomerContactPerson = request.ImpiHeaderCustomerContactPerson;
                            data.ImpiHeaderCustomerEmailId = request.ImpiHeaderCustomerEmailId;
                            data.ImpiHeaderCustomerPhoneNo = request.ImpiHeaderCustomerPhoneNo;
                            data.ImpiHeaderModifiedBy = request.ImpiHeaderCreatedBy;
                            data.ImpiHeaderModifiedOn = DateTime.Now;
                            //data.ImpiHeaderActive = true;
                            data.ImpiHeaderTotalInvoiceAmount = request.ImpiHeaderTotalInvoiceAmount;
                            data.ImpiHeaderAttachment = UploadFile(request.ImpiHeaderAttachment);
                            data.ImpiHeaderPaymentTerms = request.ImpiHeaderPaymentTerms;
                            data.ImpiHeaderRemarks = request.ImpiHeaderRemarks;
                            data.ImpiHeaderStatus = request.IsDraft == true ? "Draft" : "Pending";
                            data.IsDraft = request.IsDraft;



                            //_dbContext.Add(data);
                            _dbContext.SaveChanges();
                            int returnid = data.ImpiHeaderId;
                            if (returnid != 0 && request.lineItem_Requests.Count > 0)
                            {
                                foreach (var k in request.lineItem_Requests)
                                {
                                    var dataline = _dbContext.FicciImpiLines.ToList();
                                    foreach (var l in dataline)
                                    {
                                        l.IsDeleted = true;
                                        l.ImpiLineActive = false;

                                    }
                                    await _dbContext.SaveChangesAsync();

                                    FicciImpiLine FicciImpiLine = new FicciImpiLine();
                                    FicciImpiLine.ImpiLineDescription = k.impiLineDescription;
                                    FicciImpiLine.ImpiLineQuantity = k.ImpiLineQuantity;
                                    FicciImpiLine.ImpiLineUnitPrice = k.ImpiLineUnitPrice;
                                    FicciImpiLine.ImpiLineDiscount = k.ImpiLineDiscount;
                                    FicciImpiLine.ImpiLineAmount = k.ImpiLineAmount;
                                    FicciImpiLine.ImpiLineActive = true;
                                    FicciImpiLine.ImpiLineCreatedBy = request.ImpiHeaderCreatedBy;
                                    FicciImpiLine.ImpiLineCreatedOn = DateTime.Now;
                                    FicciImpiLine.PiHeaderId = returnid;
                                    FicciImpiLine.IsDeleted = false;
                                    FicciImpiLine.ImpiLinePiNo = DateTime.Now.ToString("yyyyMMddhhmmss");
                                    _dbContext.Add(FicciImpiLine);
                                    _dbContext.SaveChanges();


                                }

                            }
                            purchaseInvoice_New.Status = true;
                            purchaseInvoice_New.Message = "Purchase Invoice Update Successfully";
                            return StatusCode(200, purchaseInvoice_New);
                        }
                        else
                        {
                            purchaseInvoice_New.Status = false;
                            purchaseInvoice_New.Message = "Purchase Invoice record not found";
                            return StatusCode(200, purchaseInvoice_New);
                        }
                    }
                }
                else
                {
                    purchaseInvoice_New.Status = false;
                    purchaseInvoice_New.Message = "Invalid Data";
                    return StatusCode(404, purchaseInvoice_New);
                }
            }
            catch (Exception ex)
            {
                purchaseInvoice_New.Status = false;
                purchaseInvoice_New.Message = "Invalid Data";
                return StatusCode(500, purchaseInvoice_New);
            }
        }

        [HttpGet("{headerid}")]

        public async Task<IActionResult> GET(int headerid = 0)
        {
            PurchaseInvoice_New purchaseInvoice_New = new PurchaseInvoice_New();
            try
            {

                var list = _dbContext.FicciImpiHeaders.Where(m => m.ImpiHeaderActive == true).ToList();
                if (headerid != 0)
                {
                    list = list.Where(m => m.ImpiHeaderId == headerid).ToList();
                }
                if (list.Count > 0)
                {
                    List<PurchaseInvoice_Response> purchaseInvoice_responsel = new List<PurchaseInvoice_Response>();
                    foreach (var k in list)
                    {
                        PurchaseInvoice_Response purchaseInvoice_response = new PurchaseInvoice_Response();
                        purchaseInvoice_response.HeaderId = k.ImpiHeaderId;
                        purchaseInvoice_response.HeaderPiNo = k.ImpiHeaderPiNo ;
                        purchaseInvoice_response.ImpiHeaderInvoiceType = k.ImpiHeaderInvoiceType;
                        purchaseInvoice_response.ImpiHeaderProjectCode = k.ImpiHeaderProjectCode;
                        purchaseInvoice_response.ImpiHeaderDepartment = k.ImpiHeaderDepartment;
                        purchaseInvoice_response.ImpiHeaderDivison = k.ImpiHeaderDivison;
                        purchaseInvoice_response.ImpiHeaderPanNo = k.ImpiHeaderPanNo;
                        purchaseInvoice_response.ImpiHeaderGstNo = k.ImpiHeaderGstNo;
                        purchaseInvoice_response.ImpiHeaderCustomerName = k.ImpiHeaderCustomerName;
                        purchaseInvoice_response.ImpiHeaderCustomerCode = k.ImpiHeaderCustomerCode;
                        purchaseInvoice_response.ImpiHeaderCustomerAddress = k.ImpiHeaderCustomerAddress;
                        purchaseInvoice_response.ImpiHeaderCustomerCity = k.ImpiHeaderCustomerCity;
                        purchaseInvoice_response.ImpiHeaderCustomerState = k.ImpiHeaderCustomerState;
                        purchaseInvoice_response.ImpiHeaderCustomerPinCode = k.ImpiHeaderCustomerPinCode;
                        purchaseInvoice_response.ImpiHeaderCustomerGstNo = k.ImpiHeaderCustomerGstNo;
                        purchaseInvoice_response.ImpiHeaderCustomerContactPerson = k.ImpiHeaderCustomerContactPerson;
                        purchaseInvoice_response.ImpiHeaderCustomerEmailId = k.ImpiHeaderCustomerEmailId;
                        purchaseInvoice_response.ImpiHeaderCustomerPhoneNo = k.ImpiHeaderCustomerPhoneNo;
                        purchaseInvoice_response.ImpiHeaderCreatedBy = k.ImpiHeaderCreatedBy;
                        purchaseInvoice_response.IsDraft = k.IsDraft;
                        purchaseInvoice_response.ImpiHeaderSubmittedDate = k.ImpiHeaderSubmittedDate;
                        purchaseInvoice_response.ImpiHeaderTotalInvoiceAmount = k.ImpiHeaderTotalInvoiceAmount;
                        purchaseInvoice_response.ImpiHeaderPaymentTerms = k.ImpiHeaderPaymentTerms;
                        purchaseInvoice_response.ImpiHeaderRemarks = k.ImpiHeaderRemarks;
                        purchaseInvoice_response.ImpiHeaderModifiedDate = k.ImpiHeaderModifiedOn;
                        var lindata = _dbContext.FicciImpiLines.Where(m => m.ImpiLineActive == true && m.PiHeaderId == k.ImpiHeaderId).ToList();
                        if (lindata.Count > 0)
                        {
                            List<LineItem_request> lineItem_Requestl = new List<LineItem_request>();
                            foreach (var l in lindata)
                            {
                                LineItem_request lineItem_Request = new LineItem_request();
                                lineItem_Request.impiLineDescription = l.ImpiLineDescription;
                                lineItem_Request.ImpiLineUnitPrice = l.ImpiLineUnitPrice;
                                lineItem_Request.ImpiLineQuantity = l.ImpiLineQuantity;
                                lineItem_Request.ImpiLineDiscount = l.ImpiLineDiscount;
                                lineItem_Request.ImpiLineAmount = l.ImpiLineAmount;
                                lineItem_Requestl.Add(lineItem_Request);
                            }

                            purchaseInvoice_response.lineItem_Requests = lineItem_Requestl;


                        }
                        purchaseInvoice_responsel.Add(purchaseInvoice_response);

                    }


                    purchaseInvoice_New.Status = true;
                    purchaseInvoice_New.Data = purchaseInvoice_responsel;
                    purchaseInvoice_New.Message = "Purchase Invoice list successfully";
                    return StatusCode(200, purchaseInvoice_New);
                }
                else
                {
                    purchaseInvoice_New.Status = false;
                    purchaseInvoice_New.Message = "No Data found";
                    return StatusCode(200, purchaseInvoice_New);
                }



            }
            catch (Exception ex)
            {
                purchaseInvoice_New.Status = false;
                purchaseInvoice_New.Message = "Invalid Data";
                return StatusCode(500, purchaseInvoice_New);
            }
        }

        [HttpDelete("{headerid}")]

        public async Task<IActionResult> DELETE(int headerid)
        {
            PO_delete pO_Delete = new PO_delete();


            try
            {

                var list = await _dbContext.FicciImpiHeaders.Where(m => m.ImpiHeaderId == headerid).FirstOrDefaultAsync();

                list.ImpiHeaderActive = false;
              await  _dbContext.SaveChangesAsync();

                pO_Delete.status = true;
                pO_Delete.message = "Delete Successfully";
                return StatusCode(200, pO_Delete);

            }
            catch (Exception ex)
            {
                pO_Delete.status = false;
                pO_Delete.message = "Invalid Data";
                return StatusCode(500, pO_Delete);
            }
        }


        [NonAction]
        public string UploadFile(IFormFile? file)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddhhmmss");
            if (file == null || file.Length == 0)
            {
                return null; // Handle invalid file
            }


            // Generate a unique filename to avoid conflicts
            string uniqueFileName = timestamp;
            var fileExtension = Path.GetExtension(file.FileName);
            string folderpath = Path.Combine("wwwroot", "PurchaseInvoice");
            // Combine the path where you want to store the file with the unique filename
            string filePath = Path.Combine("wwwroot", "PurchaseInvoice", uniqueFileName + fileExtension);
            string savefilePath = Path.Combine("PurchaseInvoice", uniqueFileName + fileExtension);
            if (!Directory.Exists(folderpath))
            {
                // The folder does not exist, so create it
                Directory.CreateDirectory(folderpath);

            }
            // Save the file to the specified path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            // Return the file path
            return savefilePath;
        }

    }
    public class PO_delete
    {
        public Boolean status { get; set; }
        public string message { get; set; }
    }
}
