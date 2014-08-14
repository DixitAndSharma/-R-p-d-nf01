using System;

public partial class eCab_DBUpdate : BasePage
{
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
    }

    protected void btnUpdateDatabase_Click(object sender, EventArgs e)
    {
        Response.Write(DBCheck.DBUpdate());
    }
}