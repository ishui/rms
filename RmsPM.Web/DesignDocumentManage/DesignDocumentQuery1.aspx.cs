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

public partial class DesignDocumentManage_DesignDocumentQuery1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }
    protected void btnSearch_ServerClick(object sender, EventArgs e)
    {
        LoadData();
    }

    private void LoadData()
    {
        if (this.txtTitle.Value != "")
            this.DesignDocumentList1.Title = this.txtTitle.Value;
        if (this.Unit.Value != "")
            this.DesignDocumentList1.UnitCode = this.Unit.Value;
        if (this.QueryDate.Value != "")
            this.DesignDocumentList1.CreateDate = this.QueryDate.Value;
        this.DesignDocumentList1.Type = "s";
        this.DesignDocumentList1.State = "s" + Request["State"];
        this.DesignDocumentList1.PageTitle = "施工设计";
        this.DesignDocumentList1.DataBound();

    }
}
