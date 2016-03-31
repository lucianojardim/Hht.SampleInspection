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
        [Required]
        public DateTime IncomingDate;
        [Required]
        [Range(10000, 99999, ErrorMessage = "Must be a 5 digit number")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F0}")]
        public decimal DateCode;
        [Required]
        [Range(100, 999, ErrorMessage = "Must be a 3 digit number")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F0}")]
        public decimal InspectorNum;
        [Required]
        [MaxLength(20, ErrorMessage = "Cannot exceed 20 characters.")]
        [RegularExpression(@"^[dD]?[0-Z\/\\\-\s]+$", ErrorMessage = "Can start with D followed by letter and numbers and space and '-','\','/'")]
        public string SerialNumber;

    }

    public class PassFail03Metadata
    {
        [Display(Name = "Result3 - Visual Insp, damaged screws/debris")]
        public string PassFail03Desc;
    }
    public class PassFail04Metadata
    {
        [Display(Name = "Result4 - Drop/Impact datage check")]
        public string PassFail04Desc;
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
        public decimal Step05mH;

        [Display(Name = "Result6 - Main valve solenoid inductance (mH)")]
        [Range(0.0, 9999.9, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F1}")]
        public decimal Step06mH;

        [Display(Name = "Result10 - Valve control pressure High")]
        [Range(0.00, 99999999.99, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F2}")]
        public decimal Step10High;

        [Display(Name = "Result10 - Valve control pressure Low")]
        [Range(0.00, 99999999.99, ErrorMessage = "Range must be between {1} and {2}.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F2}")]
        public decimal Step10Low;
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