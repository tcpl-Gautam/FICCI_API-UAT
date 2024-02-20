﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FICCI_API.ModelsEF;

public partial class FicciErpCustomerDetail
{
    public int CustomerId { get; set; }

    public string CusotmerNo { get; set; }

    public string CustomerName { get; set; }

    public string CustomerLastname { get; set; }

    public string CustoemrAddress { get; set; }

    public string CustoemrAddress2 { get; set; }

    public int CustomerCity { get; set; }

    public string CustomerContact { get; set; }

    public string CustomerPhoneNo { get; set; }

    public string CustomerSalepersonCode { get; set; }

    public string CustomerCountryRegion { get; set; }

    public string CustomerPaymethod { get; set; }

    public string CustomerLocation { get; set; }

    public string CustomerVatRegistration { get; set; }

    public string CustomerGenBus { get; set; }

    public string CustomerPinCode { get; set; }

    public string CustomerEmailId { get; set; }

    public string CustomerTextArea { get; set; }

    public string CustomerResponsibility { get; set; }

    public string CustomerContactPerson { get; set; }

    public string CustomerPanNo { get; set; }

    public string CustomerGstNo { get; set; }

    public string CustomerUpdatedOn { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDelete { get; set; }

    public bool? IsPending { get; set; }

    public string ApprovedBy { get; set; }

    public DateTime? ApprovedOn { get; set; }

    public bool? IsDraft { get; set; }

    public bool IsApproved { get; set; }

    public int? GstCustomerType { get; set; }

    public string CustomerTlApprover { get; set; }

    public DateTime? CustomerTlApproverDate { get; set; }

    public string CustomerTlApproverRemarks { get; set; }

    public string CustomerClusterApprover { get; set; }

    public DateTime? CustomerClusterApproverDate { get; set; }

    public string CustomerClusterApproverRemarks { get; set; }

    public string CustomerSgApprover { get; set; }

    public DateTime? CustomerSgApproverDate { get; set; }

    public string CustomerSgRemaks { get; set; }

    public string Createdby { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string LastUpdateBy { get; set; }

    public virtual City CustomerCityNavigation { get; set; }

    public virtual GstCustomerType GstCustomerTypeNavigation { get; set; }
}