
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace ERP_Hamza_API.Models
{

using System;
    using System.Collections.Generic;
    
public partial class Audit
{

    public int Id { get; set; }

    public Nullable<int> FormOrId { get; set; }

    public string ActionBy { get; set; }

    public Nullable<System.DateTime> ADate { get; set; }

    public string Action { get; set; }

    public string Type { get; set; }

}

}
