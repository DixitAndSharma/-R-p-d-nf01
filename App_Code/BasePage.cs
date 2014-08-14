using System;
using System.Web.UI;
using System.Text;
using System.Data;
using System.Collections.Specialized;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
    public string Action = "";
    public string Data1 = "";
    public string Data2 = "";
    public string Data3 = "";
    public string Data4 = "";
    public string Data5 = "";
    public string Data6 = "";

    public BasePage()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool Page_Load(object sender, EventArgs e)
    {
        Action = RouteData.Values["Action"] != null ? RouteData.Values["Action"].ToString() : "";
        Data1 = RouteData.Values["Data1"] != null ? RouteData.Values["Data1"].ToString() : "";
        Data2 = RouteData.Values["Data2"] != null ? RouteData.Values["Data2"].ToString() : "";
        Data3 = RouteData.Values["Data3"] != null ? RouteData.Values["Data3"].ToString() : "";

        return IsLoggedIn();
    }

    public int GetFormInt(string FieldName)
    {
        NameValueCollection nvc = Request.Form;
        if (nvc[FieldName] != null)
            return Cmn.ToInt(nvc[FieldName]);

        return 0;
    }

    public double GetFormDbl(string FieldName)
    {
        NameValueCollection nvc = Request.Form;
        if (nvc[FieldName] != null)
            return Cmn.ToDbl(nvc[FieldName]);

        return 0;
    }

    public string GetFormString(string FieldName)
    {
        NameValueCollection nvc = Request.Form;
        if (nvc[FieldName] != null)
            return nvc[FieldName];

        return "";
    }

    public DateTime GetFormDate(string FieldName)
    {
        NameValueCollection nvc = Request.Form;
        if (nvc[FieldName] != null)
            return Cmn.ToDate(nvc[FieldName]);

        return Cmn.GetIndiaTime();
    }

    public string RouteString(string Key, string Default = "")
    {
        return RouteData.Values[Key] != null ? RouteData.Values[Key].ToString() : Default;
    }

    public string QueryString(string Key, string Default = "")
    {
        return Request.QueryString[Key] != null ? Request.QueryString[Key].ToString() : Default;
    }

    public int QueryStringInt(string Key, int Default = 0)
    {
        return Request.QueryString[Key] != null ? Cmn.ToInt(Request.QueryString[Key]) : Default;
    }

    public string GetMobile(string Mobile)
    {
        return Mobile.Length == 10 ? "0" + Mobile : Mobile;
    }

    public void WriteClientScript(string Client_Script)
    {
        ClientScriptManager cs = ClientScript;
        string csname1 = "S1";
        if (!cs.IsClientScriptBlockRegistered(GetType(), csname1))
        {
            StringBuilder cstext2 = new StringBuilder();
            cstext2.Append("<script language='javascript' type=text/javascript> \n");
            cstext2.Append(Client_Script);
            cstext2.Append("</script>");
            cs.RegisterClientScriptBlock(GetType(), csname1, cstext2.ToString(), false);
        }
    }

    public Boolean IsLocalHost
    {
        get
        {
            return Request.Url.AbsoluteUri.Contains("localhost");
        }
    }

    public Boolean IsLoggedIn(Boolean DoAdminCheck = false)
    {
        //if (IsLocalHost())
        //{
        //    Login.SetDefaults();
        //    Global.LogInDone = true;
        //}

        if (!Global.IsAdmin && DoAdminCheck)
        {
            Response.Redirect("AdminAccess.aspx");
            return false;
        }

        return true;
    }

    public void WriteClientScript()
    {
        string str = "";
        GetAllClientID(this, ref str);
        WriteClientScript(str);
    }

    public void GetAllClientID(Control parent, ref string strCtl)
    {
        foreach (Control ctl in parent.Controls)
        {
            if (ctl.ID != null)
                strCtl += "var " + ctl.ID + "=\"#" + ctl.ClientID + "\";\n";

            try
            {
                if (ctl.Controls.Count > 0)
                    GetAllClientID(ctl, ref strCtl);
            }
            catch (Exception Ex)
            {
                Cmn.LogError(Ex, "GetAllClientID");
                string str = Ex.Message;
            }
        }
    }

    public static string GetSchema(IDataReader dr)
    {
        string str = "";
        DataTable schemaTable = dr.GetSchemaTable();

        foreach (DataRow myField in schemaTable.Rows)
        {
            string c = myField["ColumnName"].ToString();

            switch (c)
            {
                case "ObjectType":
                case "LUDate":
                case "LUBy":
                case "MachineID":
                case "DirtyFlag":
                case "CRC":
                    break;
                default:
                    str += "\"" + c + "\",";
                    break;
            }
        }
        return str;
    }
    public static string GetRowData(IDataReader dr)
    {
        string str = "";
        DataTable schemaTable = dr.GetSchemaTable();

        foreach (DataRow myField in schemaTable.Rows)
        {
            string c = myField["ColumnName"].ToString();

            switch (c)
            {
                case "ObjectType":
                case "LUDate":
                case "LUBy":
                case "MachineID":
                case "DirtyFlag":
                case "CRC":
                    break;
                default:
                    str += "\"" + dr[c].ToString().Trim() + "\",";
                    break;
            }
        }
        return str;
    }
}