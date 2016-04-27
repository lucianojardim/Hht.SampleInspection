//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hht.SampleInspection.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PartReceived
    {
        public int PartReceivedId { get; set; }
        public int VendorId { get; set; }
        public System.DateTime SampleInspectionEntryDate { get; set; }
        public int AuditorId { get; set; }
        public int PartId { get; set; }
        public int WhereFoundId { get; set; }
        public int InspectionTypeId { get; set; }
        public System.DateTime IncomingDate { get; set; }
        public decimal DateCode { get; set; }
        public decimal InspectorNum { get; set; }
        public string SerialNumber { get; set; }
        public string IndividualPartComments { get; set; }
        public string RedTagNum { get; set; }
        public short WasTestedId { get; set; }
        public Nullable<decimal> InspectorNum2 { get; set; }
    
        public virtual Auditor Auditor { get; set; }
        public virtual InspectionType InspectionType { get; set; }
        public virtual Part Part { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual WhereFound WhereFound { get; set; }
        public virtual ValveTestResult ValveTestResult { get; set; }
        public virtual WasTested WasTested { get; set; }
    }
}
