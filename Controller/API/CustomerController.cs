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


        [HttpGet]
        //List of all customers
        public async Task<IActionResult> Get(string email)
        {
            var result = new CustomerDTO();
            var resu = new List<CustomerList>();
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
                            //IsDraft = customer.IsDraft,
                            CreatedBy = customer.Createdby,
                            CreatedOn = customer.CreatedOn,
                            LastUpdateBy = customer.LastUpdateBy,
                            ModifiedOn = customer.CustomerUpdatedOn,
                            TLApprover = customer.CustomerTlApprover,
                            CLApprover = customer.CustomerClusterApprover,
                           // CustomerStatus = customer.CustomerStatus == 1 ? "Draft":"Pending With TL Approver",
                            CustomerStatus = _dbContext.StatusMasters.Where(x => x.StatusId == customer.CustomerStatus).Select(a => a.StatusName).FirstOrDefault(),
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

                        }).Where(m => m.CreatedBy == email).ToListAsync();

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
                            customer.Createdby = data.LoginId;
                            customer.CustomerStatus = data.IsDraft == true ? 1 : 2;

                            customer.CustomerTlApprover =  _dbContext.FicciImems.Where(x => x.ImemEmail == data.LoginId).Select(x => x.ImemManagerEmail).FirstOrDefault().ToString() == null 
                                ? null : _dbContext.FicciImems.Where(x => x.ImemEmail == data.LoginId).Select(x => x.ImemManagerEmail).FirstOrDefault().ToString();
                           
                            customer.CustomerClusterApprover= _dbContext.FicciImems.Where(x => x.ImemEmail == data.LoginId).Select(x => x.ImemClusterEmail).FirstOrDefault().ToString() == null
                                 ? null : _dbContext.FicciImems.Where(x => x.ImemEmail == data.LoginId).Select(x => x.ImemClusterEmail).FirstOrDefault().ToString();
                           
                            customer.CustomerSgApprover = _dbContext.FicciImems.Where(x => x.ImemEmail == data.LoginId).Select(x => x.ImemDepartmentHeadEmail).FirstOrDefault().ToString() == null
                                 ? null : _dbContext.FicciImems.Where(x => x.ImemEmail == data.LoginId).Select(x => x.ImemDepartmentHeadEmail).FirstOrDefault().ToString();


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
                                result.LastUpdateBy = data.LoginId;
                                result.CustomerUpdatedOn = DateTime.Now.ToString();
                                result.CustomerStatus = data.IsDraft== true?1:2;
                                _dbContext.SaveChanges();
                                transaction.Commit();

                                request.Status = true;
                                request.Message = "Customer Update Successfully";
                                return StatusCode(200, request);
                            }
                            else
                            {
                                request.Status = true;
                                request.Message = "Customer not found";
                                return Ok(request);
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
