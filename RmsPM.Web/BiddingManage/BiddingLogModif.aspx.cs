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

public partial class BiddingManage_BiddingLogModif :RmsPM.Web.PageBase
{

    private string _BiddingCode = "";

    /// <summary>
    /// 业务代码
    /// </summary>
    public string BiddingCode
    {
        get
        {
            if (_BiddingCode == "")
            {
                if (this.ViewState["_BiddingCode"] != null)
                    return this.ViewState["_BiddingCode"].ToString();
                return "";
            }
            return _BiddingCode;
        }
        set
        {
            _BiddingCode = value;
            this.ViewState["_BiddingCode"] = value;
        }
    }
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

    public void LoadData()
    {
        string BiddingLogCode=Request["BiddingLogCode"]+"";
        BiddingCode = Request["BiddingCode"] + "";
        string ViewLast=Request["Viewlast"]+"";
        
        RmsPM.BLL.BiddingLog cbiddingLog = new RmsPM.BLL.BiddingLog();
        System.Data.DataTable dtBiddingLog;
        if (ViewLast != "")
        {
            if(this.BiddingCode!="")
                cbiddingLog.BiddingCode = BiddingCode;
            dtBiddingLog = cbiddingLog.GetBiddingLogs();
        }
        else
        {
            if (BiddingLogCode != "")
                cbiddingLog.BiddingLogCode = BiddingLogCode;
            dtBiddingLog = cbiddingLog.GetBiddingLogs();
        }

        if (dtBiddingLog != null)
        {
            if (dtBiddingLog.Rows.Count == 0)
            {
                Response.Write(Rms.Web.JavaScript.ScriptStart);
                Response.Write("window.alert('没有修改项');");
                Response.Write(Rms.Web.JavaScript.WinClose(false));
                Response.Write(Rms.Web.JavaScript.ScriptEnd);
            }
            for (int i = 0; i < dtBiddingLog.Rows.Count; i++)
            {
                this.TdUpdateMan.InnerText = RmsPM.BLL.SystemRule.GetUserName(dtBiddingLog.Rows[i]["UserCode"].ToString());
                this.tdCreateDate.InnerText = System.Convert.ToString(dtBiddingLog.Rows[i]["UpdateTime"]);
                this.tdFormerMoney.InnerText = RmsPM.BLL.MathRule.GetDecimalShowString(dtBiddingLog.Rows[i]["FormerMoney"].ToString());
                this.tdTeamMoney.InnerText = RmsPM.BLL.MathRule.GetDecimalShowString(dtBiddingLog.Rows[i]["TeamMoney"].ToString());
                break;
            }
          
        }
        if (this.BiddingCode == ""&& BiddingLogCode!="")
        {
            this.BiddingCode = cbiddingLog.BiddingCode;
        }
    }




}
