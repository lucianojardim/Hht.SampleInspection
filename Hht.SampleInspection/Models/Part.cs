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
    
    public partial class Part
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Part()
        {
            this.PartReceiveds = new HashSet<PartReceived>();
        }
    
        public int PartId { get; set; }
        public string PartNumber { get; set; }
        public int PartCategoryId { get; set; }
    
        public virtual PartCategory PartCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartReceived> PartReceiveds { get; set; }
        public virtual Valve Valve { get; set; }
    }
}