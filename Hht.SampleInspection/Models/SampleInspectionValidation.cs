using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Hht.SampleInspection.Models
{
    public class SampleInspectionValidation
    {
        private static SampleInspectionEntities _dbContext = new SampleInspectionEntities();

        public static ValidationResult ValidateAuditorName(string AuditorName)
        {
            bool exists = _dbContext.Auditors.Any(item => item.AuditorName == AuditorName);
            if (exists)
            {
                return new ValidationResult("Auditor already in the database.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }

        public static ValidationResult ValidateInspectionTypeDesc(string InspectionTypeDesc)
        {
            bool exists = _dbContext.InspectionTypes.Any(item => item.InspectionTypeDesc == InspectionTypeDesc);
            if (exists)
            {
                return new ValidationResult("Inspection Type already in the database.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }

        public static ValidationResult ValidatePartCategoryDesc(string PartCategoryDesc)
        {
            bool exists = _dbContext.PartCategories.Any(item => item.PartCategoryDesc == PartCategoryDesc);
            if (exists)
            {
                return new ValidationResult("Part Category already in the database.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
        public static ValidationResult ValidateValveControlTypeDesc(string ValveControlTypeDesc)
        {
            bool exists = _dbContext.ValveControlTypes.Any(item => item.ValveControlTypeDesc == ValveControlTypeDesc);
            if (exists)
            {
                return new ValidationResult("Valve Control Type already found in the database.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }

        public static ValidationResult ValidateValveFuelTypeDesc(string ValveFuelTypeDesc)
        {
            bool exists = _dbContext.ValveFuelTypes.Any(item => item.ValveFuelTypeDesc == ValveFuelTypeDesc);
            if (exists)
            {
                return new ValidationResult("Valve Fuel Type already in the database.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }

        public static ValidationResult ValidateVendorDesc(string VendorDesc)
        {
            bool exists = _dbContext.Vendors.Any(item => item.VendorDesc == VendorDesc);
            if (exists)
            {
                return new ValidationResult("Vendor already in the database.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
        public static ValidationResult ValidateWhereFoundDesc(string WhereFoundDesc)
        {
            bool exists = _dbContext.WhereFounds.Any(item => item.WhereFoundDesc == WhereFoundDesc);
            if (exists)
            {
                return new ValidationResult("Where Found already in the database.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}