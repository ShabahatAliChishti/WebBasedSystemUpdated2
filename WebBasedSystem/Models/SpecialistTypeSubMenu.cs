
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace WebBasedSystem.Models
{

using System;
    using System.Collections.Generic;
    
public partial class SpecialistTypeSubMenu
{

    public int SubMenuId { get; set; }

    public string SubMenuName { get; set; }

    public int SpecialistTypeId { get; set; }



    public virtual SpecialistType SpecialistType { get; set; }

}

}
