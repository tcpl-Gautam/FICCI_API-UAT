﻿namespace FICCI_API.Models
{
    public class PurchaseInvoice_New
    {
        public Boolean Status { get; set; }
        public string Message { get; set; }
        public List<PurchaseInvoice_Response> Data { get; set; }
        //public PurchaseInvoice_New()
        //{
        //    Data = new List<PurchaseInvoice_Response>();
        //}

    }
    public class PurchaseInvoice_Response
    {
        public string ImpiHeaderInvoiceType { get; set; }
        public string ImpiHeaderProjectCode { get; set; }
        public string ImpiHeaderDepartment { get; set; }
        public string ImpiHeaderDivison { get; set; }
        public string ImpiHeaderPanNo { get; set; }
        public string ImpiHeaderGstNo { get; set; }
        public string ImpiHeaderCustomerName { get; set; }
        public string ImpiHeaderCustomerCode { get; set; }
        public string ImpiHeaderCustomerAddress { get; set; }
        public string ImpiHeaderCustomerCity { get; set; }
        public string ImpiHeaderCustomerState { get; set; }
        public string ImpiHeaderCustomerPinCode { get; set; }
        public string ImpiHeaderCustomerGstNo { get; set; }
        public string ImpiHeaderCustomerContactPerson { get; set; }
        public string ImpiHeaderCustomerEmailId { get; set; }
        public string ImpiHeaderCustomerPhoneNo { get; set; }
        public string ImpiHeaderCreatedBy { get; set; }
        public decimal? ImpiHeaderTotalInvoiceAmount { get; set; }
        public IFormFile ImpiHeaderAttachment { get; set; }
        public string ImpiHeaderPaymentTerms { get; set; }
        public string ImpiHeaderRemarks { get; set; }
        public bool? IsDraft { get; set; }
        public List<LineItem_request> lineItem_Requests { get; set; }
    }
    

    public class PurchaseInvoice_Request
    {

        public bool isupdate { get; set; }
        public int? headerid { get; set; }
        public string ImpiHeaderInvoiceType { get; set; }
        public string ImpiHeaderProjectCode { get; set; }
        public string ImpiHeaderDepartment { get; set; }
        public string ImpiHeaderDivison { get; set; }
        public string ImpiHeaderPanNo { get; set; }
        public string ImpiHeaderGstNo { get; set; }
        public string ImpiHeaderCustomerName { get; set; }
        public string ImpiHeaderCustomerCode { get; set; }
        public string ImpiHeaderCustomerAddress { get; set; }
        public string ImpiHeaderCustomerCity { get; set; }
        public string ImpiHeaderCustomerState { get; set; }
        public string ImpiHeaderCustomerPinCode { get; set; }
        public string ImpiHeaderCustomerGstNo { get; set; }
        public string ImpiHeaderCustomerContactPerson { get; set; }
        public string ImpiHeaderCustomerEmailId { get; set; }
        public string ImpiHeaderCustomerPhoneNo { get; set; }
        public string ImpiHeaderCreatedBy { get; set; }
        public decimal? ImpiHeaderTotalInvoiceAmount { get; set; }
        public IFormFile ImpiHeaderAttachment { get; set; }
        public string ImpiHeaderPaymentTerms { get; set; }
        public string ImpiHeaderRemarks { get; set; }
        public bool? IsDraft { get; set; }
        public List<LineItem_request> lineItem_Requests { get; set; }


    }
    public class LineItem_request
    {

       
        public string impiLineDescription { get; set; }
        public decimal ImpiLineQuantity { get; set; }
        public decimal ImpiLineDiscount { get; set; }
        public decimal ImpiLineUnitPrice { get; set; }
        public decimal ImpiLineAmount { get; set; }



    }
}