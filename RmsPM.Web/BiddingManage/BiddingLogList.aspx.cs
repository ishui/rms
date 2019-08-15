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

public partial class BiddingManage_BiddingLogList : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (!this.user.HasRight("2111"))
            {
                Response.Redirect("../RejectAccess.aspx");
                Response.End();
            }
            this.LoadData();
        }
    }

    private void LoadData()
    {
        string BiddingCode = Request["BiddingCode"] + "";
        RmsPM.BLL.BiddingLog cbiddingLog = new RmsPM.BLL.BiddingLog();
        cbiddingLog.BiddingCode = BiddingCode;
        System.Data.DataTable dtBiddingLog = cbiddingLog.GetBiddingLogs();
        this.dgList.DataSource = dtBiddingLog;
        this.dgList.DataBind();
    }


    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gpControl_PageIndexChange(object sender, System.EventArgs e)
    {
        try
        {
            LoadData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    
}
