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
        db2 = new Database(Global.ConnectionStringrapidInfo);
        string encode = Cmn.GetEncode(this);
        try
        {
            switch (Action)
            {
                case "GetAreas": GetAreas(); break;
                case "GetIdentity": GetIdentity(); break;
                case "LinkAreas": LinkAreas(Cmn.ToInt(Data1), Cmn.ToInt(Data2)); break;
                case "UnlinkAreas": UnlinkAreas(Cmn.ToInt(Data1), Cmn.ToInt(Data2)); break;

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

    void LinkAreas(int AreaId, int IdentityId)
    {
        rapidInfoModel.AreaLink a = new rapidInfoModel.AreaLink();

        a.AreaId = AreaId;
        a.IdentityId = IdentityId;
        a.Save();

    }

    void UnlinkAreas(int AreaId, int IdentityId)
    {
        db2.RunQuery("Delete from AreaLink Where AreaId=" + AreaId + " and IdentityId="+ IdentityId);
    }

    void GetAreas()
    {
        List<rapidInfoModel.Area> list = rapidInfoModel.Area.GetData();

        foreach (rapidInfoModel.Area a in list)
        {
            rapidInfoModel.Area parent = rapidInfoModel.Area.GetData(Cmn.ToInt(a.ParentId));

            sb.Append(a.Id + "^" + a.Name + "^" + a.ParentId + "^");

            if (parent != null)
                sb.Append(parent.Name);

            sb.Append("~");
        }

    }

    void GetIdentity()
    {
        List<rapidInfoModel.Identity> list = rapidInfoModel.Identity.GetData();

        foreach (rapidInfoModel.Identity a in list)
        {
            List<rapidInfoModel.AreaLink> Areas = rapidInfoModel.AreaLink.GetData(Cmn.ToInt(a.Id));

            sb.Append(a.Id + "^" + a.Name + "^");

            if (Areas.Count != 0)
            {
                foreach (rapidInfoModel.AreaLink al in Areas)
                {
                    rapidInfoModel.Area area = rapidInfoModel.Area.GetData(al.AreaId);

                    sb.Append("<div class='alert alert-warning alert-dismissible' style='display:inline-block;padding:6px;padding-right:25px;' role='alert'>" +
                        "<button type='button' class='close' data-dismiss='alert' onclick='UnlinkAreas(" + al.AreaId + "," + a.Id + ")' aria-label='Close'><span aria-hidden='true'>&times;</span></button><strong>" +
                        area.Name + "</strong></div> ");
                }
            }

            sb.Append("^");
            sb.Append("~");
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
