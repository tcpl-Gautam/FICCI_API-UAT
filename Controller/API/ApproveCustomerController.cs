using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using FICCI_API.ModelsEF;
using FICCI_API.DTO;

namespace FICCI_API.Controller.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApproveCustomerController : BaseController
    {
        private readonly FICCI_DB_APPLICATIONSContext _dbContext;
        public ApproveCustomerController(FICCI_DB_APPLICATIONSContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        //List of Pending customers
        public async Task<IActionResult> Get(int id = 0)
        {
            var result = new CustomerDTO();
            var resu = new List<CustomerList>();
            try
            {
                if (id > 0)
                {
                    result = await _dbContext.FicciErpCustomerDetails.Where(x => x.IsDelete != true && x.IsActive != false && x.IsPending != false)
                            .Include(x => x.CustomerCityNavigation)
                            .ThenInclude(city => city.State).ThenInclude(country => country.Country)
                            .Select(customer => new CustomerDTO
                            {
                                CustomerId = customer.CustomerId,
                                CustomerCode = customer.CusotmerNo,
                                CustomerName = customer.CustomerName,
                                CustomerLastName = customer.CustomerLastname,
                                Address = customer.CustoemrAddress,
                                Address2 = customer.CustoemrAddress2,
                                Contact = customer.CustomerContact,
                                //SalePersonCode = customer.CustomerSalepersonCode,
                                //RegionCode = customer.CustomerCountryRegion,
                                PaymentMethod = customer.CustomerPaymethod,
                                Location = customer.CustomerLocation,
                                VatRegistration = customer.CustomerVatRegistration,
                                GenBusPost = customer.CustomerGenBus,
                                PinCode = customer.CustomerPinCode,
                                Email = customer.CustomerEmailId,
                                Phone = customer.CustomerPhoneNo,
                                //TextAreaCode = customer.CustomerTextArea,
                                //ResponsibilityCenter = customer.CustomerResponsibility,
                                IsDraft = customer.IsDraft,
                                City = customer.CustomerCityNavigation == null ? null : new CityInfo
                                {
                                    CityId = customer.CustomerCityNavigation.CityId,
                                    CityName = customer.CustomerCityNavigation.CityName,
                                    State = customer.CustomerCityNavigation.State == null ? null : new StateInfo
                                    {
                                        StateId = customer.CustomerCityNavigation.StateId,
                                        StateName = customer.CustomerCityNavigation.State.StateName,
                                        Country = customer.CustomerCityNavigation.State.Country == null ? null : new CountryInfo
                                        {
                                            CountryId = customer.CustomerCityNavigation.State.CountryId,
                                            CountryName = customer.CustomerCityNavigation.State.Country.CountryName,
                                        }
                                    }
                                }

                            }).FirstOrDefaultAsync(x => x.CustomerId == id);

                    if (result == null)
                    {
                        var response = new
                        {
                            status = true,
                            message = "No customer found for the given Id",
                            data = result
                        };
                        return Ok(response);
                    }
                    var respons = new
                    {
                        status = true,
                        message = "Customer Detail fetch successfully",
                        data = result
                    };
                    return Ok(respons);
                }
                else if (id == 0)
                {
                    resu = await _dbContext.FicciErpCustomerDetails.Where(x => x.IsDelete != true && x.IsActive != false && x.IsPending != false)
                        .Select(customer => new CustomerList
                        {
                            CustomerId = customer.CustomerId,
                            CustomerCode = customer.CusotmerNo,
                            CustomerName = customer.CustomerName,
                            Address = customer.CustoemrAddress,
                            Email = customer.CustomerEmailId,
                            PhoneNumber = customer.CustomerPhoneNo,
                            Pincode = customer.CustomerPinCode,
                            IsActive = customer.IsActive,
                            CustomerLastName = customer.CustomerLastname,
                            Address2 = customer.CustoemrAddress2,
                            Contact = customer.CustomerContact,
                            IsDraft = customer.IsDraft,
                            PAN = customer.CustomerPanNo,
                            GSTNumber = customer.CustomerGstNo,
                            City = customer.CustomerCityNavigation == null ? null : new CityInfo
                            {
                                CityId = customer.CustomerCityNavigation.CityId,
                                CityName = customer.CustomerCityNavigation.CityName,
                               
                            },
                            State = customer.CustomerCityNavigation.State == null ? null : new StateInfo
                            {
                                StateId = customer.CustomerCityNavigation.StateId,
                                StateName = customer.CustomerCityNavigation.State.StateName,
                                
                            },
                            Country = customer.CustomerCityNavigation.State.Country == null ? null : new CountryInfo
                            {
                                CountryId = customer.CustomerCityNavigation.State.CountryId,
                                CountryName = customer.CustomerCityNavigation.State.Country.CountryName,
                            },
                            GstType = customer.GstCustomerTypeNavigation == null ? null : new GSTCustomerTypeInfo
                            {
                                GstTypeId = customer.GstCustomerTypeNavigation.CustomerTypeId,
                                GstTypeName = customer.GstCustomerTypeNavigation.CustomerTypeName,
                            }

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
                return StatusCode(500, new { status = false, message = "An error occurred while fetching the detail of Customers." });
            }
        }


        [HttpPost("ApproveCustomer")]
        //IsPending column becomes false
        public async Task<IActionResult> ApproveCustomer(ApproveCustomer data)
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var result = await _dbContext.FicciErpCustomerDetails.Where(x => x.CustomerId == data.CustomerId).FirstOrDefaultAsync();
                    if (result != null)
                    {
                        result.IsPending = false;
                        result.ApprovedOn = DateTime.Now;
                        result.IsApproved = data.IsApproved;
                        //result.ApprovedBy
                        _dbContext.SaveChanges();
                        transaction.Commit();
                        var response = new
                        {
                            status = false,
                            message = "Approval status has been changed",
                            data = result
                        };
                        return Ok(response);
                    }
                    else
                    {
                        var response = new
                        {
                            status = false,
                            message = "Could not find the customer",
                            data = result
                        };
                        return Ok(response);
                    }

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, new { status = false, message = "An error occurred." });
                }
            }
        }
    }
}
