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
    
    public partial class YesNo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public YesNo()
        {
            this.PartReceiveds = new HashSet<PartReceived>();
        }
    
        public short YesNoId { get; set; }
        public string YesNoDesc { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartReceived> PartReceiveds { get; set; }
    }
}
