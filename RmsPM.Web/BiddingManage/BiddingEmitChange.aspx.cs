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
using Rms.Web;

public partial class BiddingManage_BiddingEmitChange : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // this.BiddingEmit1.IsEdit = false;
        if (!IsPostBack)
        {
            this.BiddingEmit1.BiddingEmitCode = Request.QueryString["EmitCode"] + "";
            this.BiddingEmit1.InitControl();
        }
    }
    protected void btnSave_ServerClick(object sender, EventArgs e)
    {
        this.BiddingEmit1.SubmitData();
        Response.Write (JavaScript.OpenerReload (true));
        Response.Write(JavaScript.WinClose(true));
    }
}
