using FICCI_API.DTO;
using FICCI_API.Models;
using FICCI_API.ModelsEF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FICCI_API.Controller.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly FICCI_DB_APPLICATIONSContext _dbContext;
        public AccountController(FICCI_DB_APPLICATIONSContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new CustomerDTO();
            var resu = new List<CustomerList>();
            try
            {
                

                resu = await _dbContext.FicciErpCustomerDetails.Where(x => x.IsDelete != true && x.IsActive != false && x.CustomerStatus == 5)
                        .Select(customer => new CustomerList
                        {
                            CustomerId = customer.CustomerId,
                            CustomerCode = customer.CusotmerNo,
                            CustomerName = customer.CustomerName,
                            CustomerLastName = customer.CustomerLastname,
                            Address = customer.CustoemrAddress,
                            Address2 = customer.CustoemrAddress2,
                            Contact = customer.CustomerContact,
                            Email = customer.CustomerEmailId,
                            PhoneNumber = customer.CustomerPhoneNo,
                            Pincode = customer.CustomerPinCode,
                            PAN = customer.CustomerPanNo,
                            GSTNumber = customer.CustomerGstNo,
                            IsActive = customer.IsActive,
                            CreatedBy = customer.Createdby,
                            CreatedOn = customer.CreatedOn,
                            LastUpdateBy = customer.LastUpdateBy,
                            ModifiedOn = Convert.ToDateTime(customer.CustomerUpdatedOn),
                            TLApprover = customer.CustomerTlApprover,
                            CLApprover = customer.CustomerClusterApprover,
                            CustomerStatus = _dbContext.StatusMasters.Where(x => x.StatusId == customer.CustomerStatus).Select(a => a.StatusName).FirstOrDefault(),
                            CustomerStatusId = customer.CustomerStatus,
                            GstType = customer.GstCustomerTypeNavigation == null ? null : new GSTCustomerTypeInfo
                            {
                                GstTypeId = customer.GstCustomerTypeNavigation.CustomerTypeId,
                                GstTypeName = customer.GstCustomerTypeNavigation.CustomerTypeName,
                            },
                            CityCode = customer.CityCode,
                            StateCode = customer.StateCode,
                            CountryCode = customer.CountryCode,
                            //City = customer.CustomerCityNavigation == null ? null : new CityInfo
                            //{
                            //    CityId = customer.CustomerCityNavigation.CityId,
                            //    CityName = customer.CustomerCityNavigation.CityName,

                            //},
                            //State = customer.CustomerCityNavigation.State == null ? null : new StateInfo
                            //{
                            //    StateId = customer.CustomerCityNavigation.StateId,
                            //    StateName = customer.CustomerCityNavigation.State.StateName,

                            //},
                            //Country = customer.CustomerCityNavigation.State.Country == null ? null : new CountryInfo
                            //{
                            //    CountryId = customer.CustomerCityNavigation.State.CountryId,
                            //    CountryName = customer.CustomerCityNavigation.State.Country.CountryName,
                            //}

                        }).ToListAsync();

                if (resu.Count <= 0)
                {
                    var response = new
                    {
                        status = true,
                        message = "No customer list found",
                        data = resu
                    };
                    return Ok(response);
                }
                var respons = new
                {
                    status = true,
                    message = "Customer List fetch successfully",
                    data = resu
                };
                return Ok(respons);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "An error occurred while fetching the detail of Customers." });
            }
        }



        [HttpGet("GetInvoice")]
        public async Task<IActionResult> GetInvoice()
        {
            var result = new PurchaseInvoice_Response();
            var resu = new List<InvoiceList>();
            try
            {


                resu = await _dbContext.FicciImpiHeaders.Where(x =>   x.ImpiHeaderActive != false && x.HeaderStatusId == 5)
                        .Select(invoice => new InvoiceList
                        {
                            HeaderId = invoice.ImpiHeaderId,
                            HeaderPiNo = invoice.ImpiHeaderPiNo,
                            ImpiHeaderInvoiceType = invoice.ImpiHeaderInvoiceType,
                            ImpiHeaderProjectCode = invoice.ImpiHeaderProjectCode,
                            ImpiHeaderProjectDepartmentName = invoice.ImpiHeaderProjectDepartmentName,
                            ImpiHeaderProjectDivisionName = invoice.ImpiHeaderProjectDivisionName,
                            ImpiHeaderProjectDepartmentCode = invoice.ImpiHeaderProjectDepartmentCode,
                            ImpiHeaderPanNo = invoice.ImpiHeaderPanNo,
                            ImpiHeaderGstNo = invoice.ImpiHeaderGstNo,
                            ImpiHeaderCustomerName = invoice.ImpiHeaderCustomerName,
                            ImpiHeaderCustomerCity = invoice.ImpiHeaderCustomerCity,
                            ImpiHeaderCustomerAddress = invoice.ImpiHeaderCustomerAddress,
                            ImpiHeaderCustomerCode = invoice.ImpiHeaderCustomerCode,
                            ImpiHeaderCustomerState = invoice.ImpiHeaderCustomerState,
                            ImpiHeaderCustomerPinCode = invoice.ImpiHeaderCustomerPinCode,
                            ImpiHeaderCustomerGstNo = invoice.ImpiHeaderCustomerGstNo,
                            ImpiHeaderCustomerContactPerson = invoice.ImpiHeaderCustomerContactPerson,
                            ImpiHeaderCustomerEmailId = invoice.ImpiHeaderCustomerEmailId,
                            ImpiHeaderCustomerPhoneNo = invoice.ImpiHeaderCustomerPhoneNo,
                            ImpiHeaderTlApprover = invoice.ImpiHeaderTlApprover,
                            ImpiHeaderClusterApprover = invoice.ImpiHeaderClusterApprover,
                            ImpiHeaderFinanceApprover = invoice.ImpiHeaderFinanceApprover,
                            ImpiHeaderProjectName = invoice.ImpiHeaderProjectName,
                            ImpiHeaderProjectDivisionCode = invoice.ImpiHeaderProjectDivisionCode,
                            ImpiHeaderCreatedBy = invoice.ImpiHeaderCreatedBy,
                            IsDraft = invoice.IsDraft,
                            HeaderStatus = _dbContext.StatusMasters.Where(x => x.StatusId == invoice.HeaderStatusId).Select(a => a.StatusName).FirstOrDefault(),


            }).ToListAsync();

                if (resu.Count <= 0)
                {
                    var response = new
                    {
                        status = true,
                        message = "No Invoice list found",
                        data = resu
                    };
                    return Ok(response);
                }
                var respons = new
                {
                    status = true,
                    message = "Invoice List fetch successfully",
                    data = resu
                };
                return Ok(respons);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "An error occurred while fetching the detail of Invoice." });
            }
        }

    }
}
