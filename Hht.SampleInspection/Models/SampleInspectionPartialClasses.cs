using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hht.SampleInspection.Models
{
    [MetadataType(typeof(AuditorMetadata))]
    public partial class Auditor
    {

    }

    [MetadataType(typeof(InspectionTypeMetadata))]
    public partial class InspectionType
    {

    }

    [MetadataType(typeof(PartMetadata))]
    public partial class Part
    {

    }

    [MetadataType(typeof(PartCategoryMetadata))]
    public partial class PartCategory
    {

    }

    [MetadataType(typeof(PartReceivedMetadata))]
    public partial class PartReceived
    {
    }

    [MetadataType(typeof(PassFail03Metadata))]
    public partial class PassFail03
    {
    }
    [MetadataType(typeof(PassFail04Metadata))]
    public partial class PassFail04
    {
    }
    [MetadataType(typeof(PassFail08Metadata))]
    public partial class PassFail08
    {
    }
    [MetadataType(typeof(PassFail11Metadata))]
    public partial class PassFail11
    {
    }

    [MetadataType(typeof(PassFail13Metadata))]
    public partial class PassFail13
    {
    }


    [MetadataType(typeof(ValveMetadata))]
    public partial class Valve : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Step10LowMin > Step10LowMax)
            {
                yield return new ValidationResult
                 ("Min value of the range is greater than Max", new[] { "Step10LowMin", "Step10LowMax" });
            }

            if (Step10HighMin > Step10HighMax)
            {
                yield return new ValidationResult
                    ("Min value of the range is greater than Max", new[] { "Step10HighMin", "Step10HighMax" });
            }

            if (Step5mHMin > Step5mHMax)
            {
                yield return new ValidationResult
                    ("Min value of the range is greater than Max", new[] { "Step5mHMin", "Step5mHMax" });
            }

            if (Step6mHMin > Step6mHMax)
            {
                yield return new ValidationResult
                    ("Min value of the range is greater than Max", new[] { "Step6mHMin", "Step6mHMax" });
            }
        }
    }

    [MetadataType(typeof(ValveControlTypeMetadata))]
    public partial class ValveControlType
    {

    }

    [MetadataType(typeof(ValveFuelTypeMetadata))]
    public partial class ValveFuelType
    {

    }

    [MetadataType(typeof(ValveTestResultMetadata))]
    public partial class ValveTestResult
    {

    }

    [MetadataType(typeof(VendorMetadata))]
    public partial class Vendor
    {

    }

    [MetadataType(typeof(WhereFoundMetadata))]
    public partial class WhereFound
    {

    }
}