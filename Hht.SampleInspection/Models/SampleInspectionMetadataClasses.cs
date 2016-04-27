using System;
using System.ComponentModel.DataAnnotations;

namespace Hht.SampleInspection.Models
{ 
    public class AuditorMetadata
    {
        [CustomValidation(typeof(SampleInspectionValidation), "ValidateAuditorName")]
        public string AuditorName;
    }

    public class InspectionTypeMetadata
    {
        [CustomValidation(typeof(SampleInspectionValidation), "ValidateInspectionTypeDesc")]
        public string InspectionTypeDesc;
    }

    public class PartMetadata
    {
        [MaxLength(15, ErrorMessage = "Cannot exceed 15 characters.")]
        [RegularExpression("^[pP]?[0-Z-]+$", ErrorMessage = "Can start with P followed by letter and numbers and '-'")]
        public string PartNumber;
    }

    public class PartCategoryMetadata
    {
        [CustomValidation(typeof(SampleInspectionValidation), "ValidatePartCategoryDesc")]
        public string PartCategoryDesc;
    }

    public class PartReceivedMetadata
    {
        //[Display(Name = "TodayDate")]
        public DateTime SampleInspectionEntryDate;
        [Required(ErrorMessage = "The {0} field is required (MM/DD/YYYY)")]
        public DateTime IncomingDate;
        [Required(ErrorMessage = "The {0} field is required (YYWWD, e.g. 16231)")]
        [Range(10000, 99999, ErrorMessage = "Must be a 5 digit number (YYWWD, e.g. 16231)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F0}")]
        public decimal DateCode;
        [Required(ErrorMessage = "The {0} field is required (3-digit number e.g. 152)")]
        [Range(100, 999, ErrorMessage = "Must be a 3 digit number (e.g. 152)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F0}")]
        public decimal InspectorNum;
        [Range(100, 999, ErrorMessage = "Must be a 3 digit number(e.g. 152)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F0}")]
        public decimal? InspectorNum2;
        [Required]
        [MaxLength(20, ErrorMessage = "Cannot exceed 20 characters.")]
        [RegularExpression(@"^[dD]?[0-z\/\\\-\s]+$", ErrorMessage = "Can start with D followed by letter and numbers and space and '-','\','/'")]
        public string SerialNumber;
    }

    public class PassFail03Metadata
    {
        [Display(Name = "Result3 - Visual Insp, damaged screws/debris/drop/impact damage check")]
        public string PassFail03Desc;
    }
    public class PassFail08Metadata
    {
        [Display(Name = "Result8 - Flame goes out burner and pilot")]
        public string PassFail08Desc;
    }
    public class PassFail11Metadata
    {
        [Display(Name = "Result11 - Flame Height Adjustment Works")]
        public string PassFail11Desc;
    }
    public class PassFail13Metadata
    {
        [Display(Name = "Result13 - Gas leaks at valve and pilot")]
        public string PassFail13Desc;
    }
    public class ValveMetadata
    {
        [Range(0.00, 99999999.99, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F2}")]
        public decimal Step10LowMin;
        [Range(0.00, 99999999.99, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F2}")]
        public decimal Step10LowMax;
        [Range(0.00, 99999999.99, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F2}")]
        public decimal Step10HighMin;
        [Range(0.00, 99999999.99, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F2}")]
        public decimal Step10HighMax;
        [Range(0.0, 9999.9, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        public decimal Step5mHMin;
        [Range(0.0, 9999.9, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        public decimal Step5mHMax;
        [Range(0.0, 9999.9, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        public decimal Step6mHMin;
        [Range(0.0, 9999.9, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        public decimal Step6mHMax;
        [Range(0.0, 9999.9, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        public decimal Step5OhmsMin;
        [Range(0.0, 9999.9, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        public decimal Step5OhmsMax;
        [Range(0.0, 9999.9, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        public decimal Step6OhmsMin;
        [Range(0.0, 9999.9, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        public decimal Step6OhmsMax;
    }

    public class ValveControlTypeMetadata
    {
        [CustomValidation(typeof(SampleInspectionValidation), "ValidateValveControlTypeDesc")]
        public string ValveControlTypeDesc;
    }

    public class ValveFuelTypeMetadata
    {
        [CustomValidation(typeof(SampleInspectionValidation), "ValidateValveFuelTypeDesc")]
        public string ValveFuelTypeDesc;
    }

    public class ValveTestResultMetadata
    {
        [Display(Name ="Result5 - Pilot valve solenoid inductance (mH)")]
        [Range(0.0, 9999.9, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        public Nullable<decimal> Step05mH;

        [Display(Name = "Result6 - Main valve solenoid inductance (mH)")]
        [Range(0.0, 9999.9, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        public Nullable<decimal> Step06mH;

        [Display(Name = "Result10 - Valve control pressure High")]
        [Range(0.00, 99999999.99, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F2}")]
        public Nullable<decimal> Step10High;

        [Display(Name = "Result10 - Valve control pressure Low")]
        [Range(0.00, 99999999.99, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F2}")]
        public Nullable<decimal> Step10Low;

        [Display(Name = "Step5 Result - Pilot Valve Solenoid Resistance (Ohms)")]
        [Range(0.0, 9999.9, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        public Nullable<decimal> Step05Ohms;

        [Display(Name = "Step6 Result - Main Valve Solenoid Resistance (Ohms)")]
        [Range(0.0, 9999.9, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        public Nullable<decimal> Step06Ohms;
    }

    public class VendorMetadata
    {
        [CustomValidation(typeof(SampleInspectionValidation), "ValidateVendorDesc")]
        public string VendorDesc;
    }

    public class WhereFoundMetadata
    {
        [CustomValidation(typeof(SampleInspectionValidation), "ValidateWhereFoundDesc")]
        public string WhereFoundDesc;
    }
}