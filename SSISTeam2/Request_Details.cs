//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SSISTeam2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Request_Details
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Request_Details()
        {
            this.Request_Event = new HashSet<Request_Event>();
        }
    
        public int request_detail_id { get; set; }
        public int request_id { get; set; }
        public string item_code { get; set; }
        public string deleted { get; set; }
        public Nullable<int> orig_quantity { get; set; }
    
        public virtual Request Request { get; set; }
        public virtual Stock_Inventory Stock_Inventory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Request_Event> Request_Event { get; set; }
    }
}
