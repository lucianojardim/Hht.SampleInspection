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
    
    public partial class PassFail13
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PassFail13()
        {
            this.ValveTestResults = new HashSet<ValveTestResult>();
        }
    
        public short PassFail13Id { get; set; }
        public string PassFail13Desc { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ValveTestResult> ValveTestResults { get; set; }
    }
}
