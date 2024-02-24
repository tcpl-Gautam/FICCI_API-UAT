﻿using FICCI_API.DTO;
using FICCI_API.Interface;
using FICCI_API.Models;
using FICCI_API.Models.Services;
using FICCI_API.ModelsEF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Xml.Linq;

namespace FICCI_API.Controller.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavERPController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _erpServer;
        private readonly string _navURL;
        private readonly string _navServiceURL;
        private readonly string _endURL;
        ApiResponseModel _responseModel;
        public NavERPController(IConfiguration configuration)
        {
            _configuration = configuration;            
            _navURL = _configuration["ERP:URL"] ?? "https://api.businesscentral.dynamics.com/v2.0/d3a55687-ec5c-433b-9eaa-9d952c913e94";
            _erpServer = _configuration["ERP:Server"] ?? "FICCI_CRM";
            _endURL = _configuration["ERP:EndURL"] ?? "ODataV4/Company('FICCI')";
            _navServiceURL = $"{_navURL}/{_erpServer}/{_endURL}";
            _responseModel = new ApiResponseModel();
        }

        [HttpGet("GetCountry")]
        public async Task<IActionResult> GetCountry()
        {
            try
            {
                #region Old Code
                //string result = await GetSecurityToken();
                //var httpClient = new HttpClient();
                //httpClient.DefaultRequestHeaders.Add("Authorization", result);
                //string serviceURL = $"{_navServiceURL}/{_configuration["ERP:Services:Country"]}";
                //var response = await httpClient.GetAsync(serviceURL);
                //response.EnsureSuccessStatusCode();
                //var responseContent = response.Content.ReadAsStringAsync().Result;
                //List<CountryMaster> countryList = JsonConvert.DeserializeObject<ODataResponse<CountryMaster>>(responseContent).Value.ToList();
                #endregion


                List<CountryMaster> countryList = await GetList<CountryMaster>("Country");
                var apiResponse = new
                {
                    data = countryList,
                    status = true,
                    message = $"{countryList.Count} records found.",
                };

             //   var country = await _dbContext.Countries.Where(x => x.IsDelete != true && x.IsActive != false).OrderBy(x => x.CountryName).ToListAsync();
                if (countryList.Count > 0)
                {
                    var countryResponse = countryList.Select(c => new CountryInfo
                    {
                        CountryId = c.Code,
                        CountryName = c.Name,
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
                        status = true,
                        message = "No Country list found",
                        data = countryList
                    };
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            //List<CountryMaster> countryList = await GetList<CountryMaster>("Country");
            //    var apiResponse = new
            //    {
            //        data = countryList,
            //        status = true,
            //        message = $"{countryList.Count} records found.",
            //    };
            //    return Ok(apiResponse);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new { status = false, message = ex.Message });
            //}
        }

        [HttpGet("GetState")]
        public async Task<IActionResult> GetState()
        {
            try
            {
                List<StateMaster> stateList = await GetList<StateMaster>("State");

                var apiResponse = new
                {
                    data = stateList,
                    status = true,
                    message = $"{stateList.Count} records found.",
                };

                if (stateList.Count > 0)
                {
                    var stateResponse = stateList.Select(c => new StateInfo
                    {
                        StateId = c.Code,
                        StateName = c.Name,
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
                        status = true,
                        message = "No State list found",
                        data = stateList
                    };
                    return Ok(response);
                }
            
            


            //var apiResponse = new
            //{
            //    data = stateList,
            //    status = true,
            //    message = $"{stateList.Count} records found.",
            //};
            //return Ok(apiResponse);

        }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "An error occurred while fetching the detail." });
            }
        }

        [HttpGet("GetCity")]
        public async Task<IActionResult> GetCity()
        {
            try
            {
                List<CityMaster> cityList = await GetList<CityMaster>("City");


                var apiResponse = new
                {
                    data = cityList,
                    status = true,
                    message = $"{cityList.Count} records found.",
                };
               // return Ok(apiResponse);


                if (cityList.Count > 0)
                {
                    var cityResponse = cityList.Select(c => new CityInfo
                    {
                        CityId = c.Code,
                        CityName = c.Name,
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
                        status = true,
                        message = "No City list found",
                        data = cityList
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            //var apiResponse = new
            //    {
            //        data = cityList,
            //        status = true,
            //        message = $"{cityList.Count} records found.",
            //    };
            //    return Ok(apiResponse);

            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new { status = false, message = "An error occurred while fetching the detail." });
            //}
        }

        [HttpGet("GetPostCode")]
        public async Task<IActionResult> GetPostCode()
        {
            try
            {
                List<PostCodeMaster> cityList = await GetList<PostCodeMaster>("PostCode");

                var apiResponse = new
                {
                    data = cityList,
                    status = true,
                    message = $"{cityList.Count} records found.",
                };
                return Ok(apiResponse);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "An error occurred while fetching the detail." });
            }
        }


        [HttpGet("GetCustomer")]
        public async Task<IActionResult> GetCustomer()
        {
            try
            {
                List<CustomerModel> cityList = await GetList<CustomerModel>("Customer");

                var apiResponse = new
                {
                    data = cityList,
                    status = true,
                    message = $"{cityList.Count} records found.",
                };
                return Ok(apiResponse);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "An error occurred while fetching the detail." });
            }
        }

        [HttpGet("GetEmployee")]
        public async Task<IActionResult> GetEmployee()
        {
            try
            {
                List<EmployeeModel> cityList = await GetList<EmployeeModel>("Employee");

                var apiResponse = new
                {
                    data = cityList,
                    status = true,
                    message = $"{cityList.Count} records found.",
                };
                return Ok(apiResponse);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "An error occurred while fetching the detail." });
            }
        }

        [HttpGet("GetProject")]
        public async Task<IActionResult> GetProject()
        {
            try
            {
                List<ProjectModel> cityList = await GetList<ProjectModel>("Project");

                var apiResponse = new
                {
                    data = cityList,
                    status = true,
                    message = $"{cityList.Count} records found.",
                };
                return Ok(apiResponse);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "An error occurred while fetching the detail." });
            }
        }


        [HttpPost("PostCustomer")]
        //Submit customer not drafted
        public async Task<IActionResult> PostCustomer(CustomerRequest data)
        {
            try
            {
            //    HttpContent content = new StringContent("{'insertPurchInvHeadDetails': '" + insertPurchCreditMemoDetails + "','insertPurchInvLineDetails': '" + insertPurchCreditMemoLineDetails + "' }", System.Text.Encoding.UTF8, "application/json");



            //    //var username = "TEAMCOMPUTERS.CRM";
            //    //var password = "gXAXZPsieceCYWsxzjzzCYayJe53ZtAFEXitC+xgA08=";
            //    //string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            //    //var httpClient = new HttpClient();
            //    //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", svcCredentials);

            //    var response = httpClient.PostAsync(erpURL + "ODataV4/VMSServiceCreditMemo_InsertPurchaseInvoice?company=FICCI", content);

            //    response.Result.EnsureSuccessStatusCode();

            //    var responseContent = response.Result.Content.ReadAsStringAsync().Result;

            //    var odata = JsonConvert.DeserializeObject<OData>(responseContent);

            }
            catch(Exception ex) {
                _responseModel.Data = ex;
                _responseModel.Status = false;
                _responseModel.Message = ex.Message;                
            }
            return Ok(_responseModel);
        }


        #region Private Functions
        private async Task<string> GetSecurityToken()
        {
            string ClientId = "fa7e3cb6-60be-4d54-b515-81173f58d31e";
            string ClientSecret = "1FM8Q~ywhdiC39l2Rwy3yEqmhYVyJwlBWwlhub6e";
            string BearerToken = "";
            string URL = "https://login.microsoftonline.com/d3a55687-ec5c-433b-9eaa-9d952c913e94/oauth2/v2.0/token";

            HttpClient client1 = new HttpClient();

            var content = new StringContent("grant_type= Client_Credentials" +
                "&Scope= https://api.businesscentral.dynamics.com/.default" +
                "&client_id=" + HttpUtility.UrlEncode(ClientId) +
                "&client_secret=" + HttpUtility.UrlEncode(ClientSecret));


            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var response = client1.PostAsync(URL, content).Result;

            if (response.IsSuccessStatusCode)
            {
                JObject Result = JObject.Parse(await response.Content.ReadAsStringAsync());
                BearerToken = Result["access_token"].ToString();


            }
            return "Bearer " + BearerToken;
        }

        public async Task<List<T>> GetList<T>(string serviceKey)
        {
            try
            {
                string result = await GetSecurityToken();
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", result);
                string serviceURL = $"{_navServiceURL}/{_configuration[$"ERP:Services:{serviceKey}"]}";
                var response = await httpClient.GetAsync(serviceURL);
                response.EnsureSuccessStatusCode();
                var responseContent = response.Content.ReadAsStringAsync().Result;
                List<T> list = JsonConvert.DeserializeObject<ODataResponse<T>>(responseContent).Value.ToList();

                return list;
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}