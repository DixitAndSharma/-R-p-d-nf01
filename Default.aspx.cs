using System;
using System.Collections.Generic;
using System.IO;
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

            //leftPanel.InnerHtml = "<img class='profileImg' src='" + ResolveClientUrl(@"~/Images/identity/" + iden.Id + ".jpg") + "' alt='" + iden.Name + "' />";


            str += "<h1>" + iden.Name + "</h1>";


            str += "<p>" + "<img class='profileImg' src='" + ResolveClientUrl(@"~/Images/identity/" + iden.Id + ".jpg") + "' alt='" + iden.Name + "' />" + Cmn.GetUnCompressed(iden.Details, (int)iden.DetailsLength) + "</p>";

            rightPanel.InnerHtml = str;

            return;
        }


        rapidInfoModel.Area a = rapidInfoModel.Area.GetDataByName(Action);

        List<rapidInfoModel.AreaLink> alList = rapidInfoModel.AreaLink.GetDataByArea(a.Id);

        if (alList.Count == 0)
        {
            str += "<h1>" + a.Name + "</h1><br/>";
            str += "<div class='row'>";

            List<rapidInfoModel.Area> ChildList = rapidInfoModel.Area.GetDataByParent(a.Id);
            var ctr=0;

            foreach (rapidInfoModel.Area child in ChildList)
            {
                if(ctr%4==0)
                    str += "</div><br><br><div class='row'>";

                string image = @"\image\" + child.Name.Replace(" ", "-").ToLower() + ".jpg";

                if(!File.Exists(Server.MapPath( image)))
                    image = @"\image\notavailable.jpg";

                str += "<div class='col-md-3 ' ><a class='newProductMenuBlock' href='/" + child.Name.Replace(" ", "").ToLower() +
                "'><img class='img-responsive' style='width:100%;height:147px' src='"+image+"'><span class='ProductMenuCaption'><i class='fa fa-caret-right'></i>" + child.Name + "</span></div>";

                ctr++;

                //str += "<li><a href='/" + child.Name.Replace(" ", "").ToLower() + "'>" + child.Name + "</a></li>";
            }

            str += "</div>";

        }
        else
        {


            //str += "<h1>";


            str += "<h1>" + a.Name + "</h1><br/>";

            str += "<ul>";

            foreach (rapidInfoModel.AreaLink child in alList)
            {
                rapidInfoModel.Identity identity = rapidInfoModel.Identity.GetData(child.IdentityId);

                str += "<li><a href='/" + identity.Name.Replace(" ", "").ToLower() + "'>" + identity.Name + "</a></li>";
            }

            str += "</ul>";



        }

        rightPanel.InnerHtml = str;

    }
}