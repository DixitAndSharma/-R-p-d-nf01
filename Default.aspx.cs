using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);

        string str = "";
        if (Action == "")
            Action = "people";

        rapidInfoModel.Identity iden = rapidInfoModel.Identity.GetData(Action);

        if (iden != null)
        {

            leftPanel.InnerHtml = "<img class='profileImg' src='" + ResolveClientUrl(@"~/Images/identity/" + iden.Id + ".jpg") + "' alt='" + iden.Name + "' />";


            str += "<h1>" + iden.Name + "</h1>";


            str += "<p>" + Cmn.GetUnCompressed(iden.Details, (int)iden.DetailsLength) + "</p>";

            rightPanel.InnerHtml = str;

            return;
        }


        rapidInfoModel.Area a = rapidInfoModel.Area.GetDataByName(Action);

        List<rapidInfoModel.AreaLink> alList = rapidInfoModel.AreaLink.GetDataByArea(a.Id);

        if (alList.Count == 0)
        {
            str += "<h1>" + a.Name + "</h1><br/>";
            str += "<ul>";

            List<rapidInfoModel.Area> ChildList = rapidInfoModel.Area.GetDataByParent(a.Id);

            foreach (rapidInfoModel.Area child in ChildList)
            {
                str += "<li><a href='/" + child.Name.Replace(" ", "").ToLower() + "'>" + child.Name + "</a></li>";
            }

            str += "</ul>";

        }
        else
        {

            if (alList.Count > 0)
            {
                str += "<h1>" + a.Name + "</h1><br/>";

                str += "<ul>";

                foreach (rapidInfoModel.AreaLink child in alList)
                {
                    rapidInfoModel.Identity identity = rapidInfoModel.Identity.GetData(child.IdentityId);

                    str += "<li><a href='/" + identity.Name.Replace(" ", "").ToLower() + "'>" + identity.Name + "</a></li>";
                }

                str += "</ul>";
            }

        }

        rightPanel.InnerHtml = str;

    }
}