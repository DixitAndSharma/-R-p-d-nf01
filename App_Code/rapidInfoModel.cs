﻿

//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;


public partial class Area
{

    public int Id { get; set; }

    public string Name { get; set; }

    public Nullable<int> ParentId { get; set; }

    public Nullable<System.DateTime> LUDate { get; set; }

    public string LUBy { get; set; }

    public string CRC { get; set; }

    public string MachineID { get; set; }

    public Nullable<int> DirtyFlag { get; set; }

}


public partial class AreaLink
{

    public int IdentityId { get; set; }

    public int AreaId { get; set; }

    public Nullable<System.DateTime> LUDate { get; set; }

    public string LUBy { get; set; }

    public string CRC { get; set; }

    public string MachineID { get; set; }

    public Nullable<int> DirtyFlag { get; set; }

}


public partial class Identity
{

    public int Id { get; set; }

    public Nullable<int> DetailsLength { get; set; }

    public byte[] Details { get; set; }

    public Nullable<System.DateTime> LastUpdate { get; set; }

    public Nullable<System.DateTime> LUDate { get; set; }

    public string LUBy { get; set; }

    public string CRC { get; set; }

    public string MachineID { get; set; }

    public Nullable<int> DirtyFlag { get; set; }

}

