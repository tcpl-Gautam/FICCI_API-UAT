using FICCI_API.DTO;
using FICCI_API.ModelsEF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FICCI_API.Controller.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly FICCI_DB_APPLICATIONSContext _dbContext;
        public CustomerController(FICCI_DB_APPLICATIONSContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("{id}")]
        //List of all customers
        public async Task<IActionResult> Get(int id = 0)
        {
            var result = new CustomerDTO();
            var resu = new List<CustomerList>();
            try
            {
                if (id > 0)
                {
                    result = await _dbContext.FicciErpCustomerDetails.Where(x => x.IsDelete != true && x.IsActive != false)
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
                                SalePersonCode = customer.CustomerSalepersonCode,
                                RegionCode = customer.CustomerCountryRegion,
                                PaymentMethod = customer.CustomerPaymethod,
                                Location = customer.CustomerLocation,
                                VatRegistration = customer.CustomerVatRegistration,
                                GenBusPost = customer.CustomerGenBus,
                                PinCode = customer.CustomerPinCode,
                                Email = customer.CustomerEmailId,
                                Phone = customer.CustomerPhoneNo,
                                TextAreaCode = customer.CustomerTextArea,
                                ResponsibilityCenter = customer.CustomerResponsibility,
                                GstType = customer.GstCustomerTypeNavigation == null ? null : new GSTCustomerTypeInfo
                                {
                                    GstTypeId = customer.GstCustomerTypeNavigation.CustomerTypeId,
                                    GstTypeName = customer.GstCustomerTypeNavigation.CustomerTypeName,
                                },
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
                                }

                            }).FirstOrDefaultAsync(x => x.CustomerId == id);

                    if (result == null)
                    {
                        var response = new
                        {
                            status = false,
                            message = "No customer found for the given Id",
                            data = result
                        };
                        return NotFound(response);
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
                    resu = await _dbContext.FicciErpCustomerDetails.Where(x => x.IsDelete != true && x.IsActive != false)
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
                            GstType = customer.GstCustomerTypeNavigation == null ? null : new GSTCustomerTypeInfo
                            {
                                GstTypeId = customer.GstCustomerTypeNavigation.CustomerTypeId,
                                GstTypeName = customer.GstCustomerTypeNavigation.CustomerTypeName,
                            },
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
                            }

                        }).ToListAsync();
                    if (resu.Count <= 0)
                    {
                        var response = new
                        {
                            status = false,
                            message = "No customer list found",
                            data = resu
                        };
                        return NotFound(response);
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

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> Delete(int customerId)
        {
            try
            {
                if(customerId > 0)
                {
                    var res = await _dbContext.FicciErpCustomerDetails.Where(x => x.CustomerId == customerId).FirstOrDefaultAsync();
                    res.IsActive = false;
                    res.IsDelete = true;
                    await _dbContext.SaveChangesAsync();
                    var response = new
                    {
                        status = true,
                        message = "Delete successfully",
                        data = new object[] { }
                    };
                    return StatusCode(200,response);
                }
                else
                {
                    var response = new
                    {
                        status = false,
                        message = "Invalid Id",
                    };
                    return NotFound(response);
                }

            }catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "An error occurred." });
            }
        }


        [HttpPost]
        //Submit customer not drafted
        public async Task<IActionResult> Post(CustomerRequest data)
        {
            new_Customer request = new new_Customer();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    if(data != null)
                    {
                        if(!data.isupdate)
                        {
                            FicciErpCustomerDetail customer = new FicciErpCustomerDetail();
                            customer.CusotmerNo = data.CustomerCode;
                            customer.CustoemrAddress = data.Address;
                            customer.CustoemrAddress2 = data.Address2;
                            customer.CustomerContact = data.Contact;
                            customer.CustomerName = data.CustomerName;
                            customer.CustomerLastname = data.CustomerLastName;
                            customer.CustomerGstNo = data.GSTNumber;
                            customer.CustomerEmailId = data.Email;
                            customer.CustomerPinCode = data.PinCode;
                            customer.CustomerPanNo = data.PAN;
                            customer.CustomerUpdatedOn = DateTime.Now.ToString("yyyyMMdd");
                            customer.CustomerCity = data.Cityid;
                            customer.CustomerPhoneNo = data.Phone;
                            customer.GstCustomerType = data.GSTCustomerType;
                            customer.IsPending = true;
                            customer.IsDraft = data.IsDraft;

                            _dbContext.Add(customer);
                            _dbContext.SaveChanges();
                            transaction.Commit();

                            request.Status = true;
                            request.Message = "Customer Insert Successfully";
                            return StatusCode(200, request);
                        }
                        else
                        {
                            var result = await _dbContext.FicciErpCustomerDetails.Where(x => x.CustomerId == data.CustomerId).FirstOrDefaultAsync();
                            if(result!= null)
                            {
                                result.CustoemrAddress = data.Address;
                                result.CustoemrAddress2 = data.Address2;
                                result.CusotmerNo = data.CustomerCode;
                                result.CustomerContact = data.Contact;
                                result.CustomerName = data.CustomerName;
                                result.CustomerLastname = data.CustomerLastName;
                                result.CustomerGstNo = data.GSTNumber;
                                result.CustomerEmailId = data.Email;
                                result.CustomerPinCode = data.PinCode;
                                result.CustomerPanNo = data.PAN;
                                result.CustomerUpdatedOn = DateTime.Now.ToString("yyyyMMdd");
                                result.CustomerCity = data.Cityid;
                                result.CustomerPhoneNo = data.Phone;
                                result.GstCustomerType = data.GSTCustomerType;
                                result.IsPending = true;
                                result.IsDraft = data.IsDraft;

                                _dbContext.SaveChanges();
                                transaction.Commit();

                                request.Status = true;
                                request.Message = "Customer Update Successfully";
                                return StatusCode(200, request);
                            }
                            else
                            {
                                request.Status = false;
                                request.Message = "Customer not found";
                                return NotFound(request);
                            }
                        }
                    }
                    else
                    {
                        request.Status = false;
                        request.Message = "Invalid data";
                        return StatusCode(404, request);
                    }
                  
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, new { status = false, message = "An error occurred while fetching the detail of Customers." });
                }
            }
        }

    }
}
