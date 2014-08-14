using System;
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
      //  List<Area> list = Area.GetData();
    }
    
}
