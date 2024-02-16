﻿using FICCI_API.Models;

namespace FICCI_API.DTO
{
    public class CustomerDTO
    {
        public bool isupdate { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string? CustomerLastName { get; set; }
        public string? Address { get; set; }
        public string? Address2 { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string SalePersonCode { get; set; }
        public string RegionCode { get; set; }
        public string PaymentMethod { get; set; }
        public string? Location { get; set; }
        public string? VatRegistration { get; set; }
        public string? GenBusPost { get; set; }
        public string? PinCode { get; set; }
        public string Email { get; set; }
        public string? TextAreaCode { get; set; }
        public string? ResponsibilityCenter { get; set; }
        public CityInfo? City { get; set; }

    }
    public class CustomerList
    {
        public int CustomerId { get; set;}
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Pincode { get; set; }
        public string CustomerLastName { get; set; }
        public string Address2 { get; set; }
        public string Contact { get; set; }
        public string? GSTNumber { get; set; }
        //public int GSTCustomerType { get; set; }
        public string? PAN { get; set; }
        public CityInfo? City { get; set; }
        public StateInfo? State { get; set; }
        public CountryInfo? Country { get; set; }
        public GSTCustomerTypeInfo? GstType { get; set; }
    }
    public class CityInfo
    {
        public int CityId {  get; set; }
        public string CityName { get; set; }
        public StateInfo? State { get; set; }
    }
    public class StateInfo
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public CountryInfo? Country { get; set; }

    }
    public class CountryInfo
    {
        public int CountryId {  get; set; }
        public string CountryName { get; set; }

    }

    public class GSTCustomerTypeInfo
    {
        public int GstTypeId { get; set; }
        public string GstTypeName { get; set; }
    }

    public class new_Customer
    {
        public Boolean Status { get; set; }
        public string Message { get; set; }
        public List<CustomerRequest> Data { get; set; }
    }




    public class CustomerRequest
    {
        public bool isupdate { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string? CustomerLastName { get; set; }
        public string? Address { get; set; }
        public string? Address2 { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string? PinCode { get; set; }
        public string Email { get; set; }
        public int Cityid { get; set; }
        public bool? IsDraft { get; set; }
        public string? GSTNumber { get; set; }
        public string? PAN { get; set; }
        public int GSTCustomerType { get; set; }
    }
}