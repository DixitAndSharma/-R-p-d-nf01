using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using rapidInfoModel;

public partial class Edit_AreaEdit : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int ID = Cmn.ToInt(Request.QueryString["id"]);
        ShowData(ID);
    }

    void ShowData(int id)
    {
        rapidInfoModel.Identity a = rapidInfoModel.Identity.GetData(id);

        if (a != null)
        {
            lblId.Text = a.Id.ToString();
            txtName.Text = a.Name;

            txtDetails.InnerHtml = Cmn.GetUnCompressed(a.Details, (int)a.DetailsLength);
        }

    }

    protected void Unnamed1_Click1(object sender, EventArgs e)
    {
        rapidInfoModel.Identity a = rapidInfoModel.Identity.GetData(Cmn.ToInt(lblId.Text));
        if (a == null)
            a = new rapidInfoModel.Identity();

        a.Name = txtName.Text;
        a.Details = Cmn.GetCompressed(txtDetails.InnerText);
        a.DetailsLength = txtDetails.InnerText.Length;
        a.Save();

        WriteClientScript("parent.GetIdentity();");
    }
}