//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SSISTeam2.App_Code
{
    using System;
    using System.Collections.Generic;
    
    public partial class Alert
    {
        public int alert_id { get; set; }
        public string item_code { get; set; }
        public string reason { get; set; }
        public System.DateTime date { get; set; }
        public string handled { get; set; }
        public string deleted { get; set; }
    
        public virtual Stock_Inventory Stock_Inventory { get; set; }
    }
}
