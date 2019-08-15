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

public partial class BiddingManage_BiddingTypeModify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.LoadData();
        }
    }

    private void LoadData()
    {
        string BiddingCode = Request["BiddingCode"] + "";

        if (BiddingCode == "")
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "这个招投标计划不存在！"));
            return;
        }
        else
        {
           
            RmsPM.BLL.BiddingSystem.Set_BiddingTypeDictionary(this.selBiddingType);
            RmsPM.BLL.Bidding cbidding = new RmsPM.BLL.Bidding();
            cbidding.BiddingCode = BiddingCode;
            string BiddingType = cbidding.BiddingType;
            if (BiddingType == null || BiddingType == "")
            {
                this.selBiddingType.SelectedIndex = 0;
            }
            else
            {
                this.selBiddingType.Value = BiddingType;
            }
        }

        
    }
    protected void btnSave_ServerClick(object sender, EventArgs e)
    {
        
        string BiddingCode = Request["BiddingCode"] + "";
        RmsPM.BLL.Bidding cbidding = new RmsPM.BLL.Bidding();
        cbidding.BiddingCode = BiddingCode;
        
        DataTable dtBidding = cbidding.GetBiddings();

        if (dtBidding==null || dtBidding.Rows.Count == 0)
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "这个招投标计划不存在！"));
            return;
        }
        else
        {
            if (this.selBiddingType.Items[this.selBiddingType.SelectedIndex].Value == "")
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "请选择类型！"));
                return;
            }
            cbidding.BiddingType = this.selBiddingType.Items[this.selBiddingType.SelectedIndex].Value;
            cbidding.Status = "0";
            cbidding.BiddingUpdate();

            Response.Write(Rms.Web.JavaScript.ScriptStart);
            Response.Write("window.opener.location = window.opener.location;");
            Response.Write(Rms.Web.JavaScript.WinClose(false));
           
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }
    }


}
