using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Contract_ContractBill : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string strStatus = Request["Status"] + "";
            if (strStatus == "New")
            {
                FormView1.ChangeMode(FormViewMode.Insert);
            }
        }
    }

    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        if (this.FormView1.CurrentMode == FormViewMode.Insert)
        {
            e.Values["ProjectCode"] = Request["ProjectCode"]+"";
            e.Values["ContractCode"] = Request["ContractCode"]+"";
        }
    }

    protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        this.RegisterStartupScript("", "<script>opener.location.reload();window.close();</script>");
    }
    protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        this.RegisterStartupScript("", "<script>opener.location.reload();window.close();</script>");
    }
    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        this.RegisterStartupScript("", "<script>opener.location.reload();</script>");
        this.FormView1.ChangeMode(FormViewMode.ReadOnly);
    }
}
