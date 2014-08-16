﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using rapidInfoModel;

public partial class GetData : System.Web.UI.Page
{
    string Action, Data1, Data2, Data3, Data4, Data5, Data6, Data7, Data8, Message = "";
    StringBuilder sb = new StringBuilder();
    Database db = null;
    Database db2 = null;
    Boolean AttachError = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Cmn.LogError(null, "test");
        Action = QueryString("Action");
        Data1 = QueryString("Data1");
        Data2 = QueryString("Data2");
        Data3 = QueryString("Data3");
        Data4 = QueryString("Data4");
        Data5 = QueryString("Data5");
        Data6 = QueryString("Data6");
        Data7 = QueryString("Data7");
        Data8 = QueryString("Data8");
        string term = QueryString("term");

        db = new Database();
        db2 = new Database();
        string encode = Cmn.GetEncode(this);
        try
        {
            switch (Action)
            {
                case "GetAreas": GetAreas(); break;
                case "SearchAreas":
                    SearchAreas(term);
                    Cmn.WriteResponse(this, sb.ToString(), encode);
                    return;

                //SearchAreas

            }

            if (!AttachError)
                Cmn.WriteResponse(this, sb.ToString(), encode);
            else
                Cmn.WriteResponse(this, Message + "~" + sb.ToString(), encode);
        }
        catch (Exception ex)
        {
            Cmn.WriteResponse(this, Action + "-" + ex.Message + "~" + sb.ToString(), encode);
            Cmn.LogError(ex, "Data.aspx");
        }
        finally { db.Close(); }
    }

    public string QueryString(string Key, string Default = "")
    {
        return Request.QueryString[Key] != null ? Request.QueryString[Key].ToString() : Default;
    }

    void GetAreas()
    {
        List<rapidInfoModel.Area> list = rapidInfoModel.Area.GetData();

        foreach (rapidInfoModel.Area a in list)
        {
            sb.Append(a.Id + "^" + a.Name + "^" + a.ParentId + "~");
        }

    }

    void SearchAreas(string term)
    {
        if (term == "")
            return;

        List<rapidInfoModel.Area> list = rapidInfoModel.Area.GetData(term);

        StringBuilder Result = new StringBuilder();
        int ctr = 0;
        foreach (rapidInfoModel.Area a in list)
        {
            Result.Append(",{\"id\":\"" + a.Id
                + "\",\"label\":\"" + a.Name
                + "\",\"value\":\"" + a.Name
                + "\",\"Name\":\"" + a.Name
                + "\"}");

            if (ctr++ > 5)
                break;
        }

        if (Result.Length > 0)
            sb.Append("[" + Result.ToString().Substring(1) + "]");

    }

    public List<Area> GetAreaData()
    {
        using (rapidInfoEntities context = new rapidInfoEntities())
        {
            return context.Areas.ToList();
        }
    }

}