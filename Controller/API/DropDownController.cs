using FICCI_API.DTO;
using FICCI_API.Interface;
using FICCI_API.Models;
using FICCI_API.ModelsEF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace FICCI_API.Controller.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropDownController : BaseController
    {
        private readonly FICCI_DB_APPLICATIONSContext _dbContext;
        public DropDownController(FICCI_DB_APPLICATIONSContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetRole")]

        public async Task<IActionResult> GetRole(int id)
        {
            try
            {
                var roles = await _dbContext.GetProcedures().prc_Role_listAsync(id);
                if (roles.Count > 0)
                {
                    var roleResponses = roles.Select(c => new Role
                    {
                        Role_id = c.Role_Id,
                        RoleName = c.Role_name,
                        IsActive = c.IsActive
                    }).ToList();

                    var response = new GetRoleResponse
                    {
                        status = true,
                        message = "Roles fetched successfully",
                        data = roleResponses
                    };

                    return Ok(response);
                }
                else
                {
                    var response = new GetRoleResponse
                    {
                        status = true,
                        message = "No roles found",
                        data = new List<Role>()
                    };

                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("GetEmployeeList")]
        public async Task<IActionResult> GetEmployeeList(int id)
        {
            try
            {
                var employees = await _dbContext.GetProcedures().prc_EmployeeMaster_listAsync(id);
                if(employees.Count > 0)
                {
                    var employeeResponse = employees.Select(c => new Employee_Master{
                        IMEM_ID = c.IMEM_ID,
                        IMEM_Email =c.IMEM_EMAIL,
                        IMEM_Name =c.IMEM_NAME,
                        IMEM_EmpId = c.IMEM_EMPID,
                        IMEM_Username =c.IMEM_USERNAME,
                        IsActive = c.IMEM_ACTIVE

                    }).ToList();

                    var response = new GetEmployee_MasterResponse
                    {
                        status = true,
                        message = "Employee List Fetch",
                        data = employeeResponse
                    };

                    return Ok(response);
                }
                else
                {
                    var response = new GetEmployee_MasterResponse
                    {
                        status = false,
                        message = "No List Found",
                        data = new List<Employee_Master>()
                    };
                    return Ok(response);
                }

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCityByStateId")]
        public async Task<IActionResult> GetCityByStateId(int stateId)
        {
            try
            {
                var city = await _dbContext.Cities.Where(x => x.IsDelete != true && x.IsActive != false && x.StateId == stateId).OrderBy(x => x.CityName).ToListAsync();
                if(city.Count > 0)
                {
                    var cityResponse = city.Select(c => new CityInfo
                    {
                        CityId = c.CityId,
                        CityName = c.CityName,
                    }).ToList();

                    var response = new
                    {
                        status = true,
                        message = "City List fetch successfully",
                        data = cityResponse
                    };
                    return Ok(response);

                }
                else
                {
                    var response = new
                    {
                        status = false,
                        message = "No City list found",
                        data = city
                    };
                    return Ok(response);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("GetStateByCountryId")]
        public async Task<IActionResult> GetStateByCountryId(int counrtyId)
        {
            try
            {
                var state = await _dbContext.States.Where(x => x.IsDelete != true && x.IsActive != false && x.CountryId == counrtyId).OrderBy(x => x.StateName).ToListAsync();
                if (state.Count > 0)
                {
                    var stateResponse = state.Select(c => new StateInfo
                    {
                        StateId = c.StateId,
                        StateName = c.StateName,
                    }).ToList();

                    var response = new
                    {
                        status = true,
                        message = "State List fetch successfully",
                        data = stateResponse
                    };
                    return Ok(response);

                }
                else
                {
                    var response = new
                    {
                        status = false,
                        message = "No State list found",
                        data = state
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("GetCountryList")]
        public async Task<IActionResult> GetCountryList()
        {
            try
            {
                var country = await _dbContext.Countries.Where(x => x.IsDelete != true && x.IsActive != false).OrderBy(x => x.CountryName).ToListAsync();
                if (country.Count > 0)
                {
                    var countryResponse = country.Select(c => new CountryInfo
                    {
                        CountryId = c.CountryId,
                        CountryName = c.CountryName,
                    }).ToList();

                    var response = new
                    {
                        status = true,
                        message = "Country List fetch successfully",
                        data = countryResponse
                    };
                    return Ok(response);

                }
                else
                {
                    var response = new
                    {
                        status = false,
                        message = "No Country list found",
                        data = country
                    };
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GstCustomerType")]
        public async Task<IActionResult> GstCustomerType()
        {
            try
            {
                var custType = await _dbContext.GstCustomerTypes.Where(x => x.IsDelete != true && x.IsActive != false).OrderBy(x => x.CustomerTypeName).ToListAsync();
                if(custType.Count > 0)
                {
                    var custResponse = custType.Select(c => new GSTCustomerTypeInfo
                    {
                        GstTypeId = c.CustomerTypeId,
                        GstTypeName = c.CustomerTypeName,
                    }).ToList();
                    var response = new
                    {
                        status = true,
                        message = "GstCustomerType List fetch successfully",
                        data = custResponse
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new
                    {
                        status = false,
                        message = "No Country list found",
                        data = custType
                    };
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
