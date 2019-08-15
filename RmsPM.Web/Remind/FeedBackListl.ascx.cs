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
using RmsPM.DAL.EntityDAO;
using Rms.ORMap;
using RmsPM.BLL;

public partial class Remind_FeedBackListl : System.Web.UI.UserControl
{
    protected System.Web.UI.WebControls.TextBox txtCode;

    public string FeedBackType
    {
        set
        {
            //this.strType = value;
            this.ViewState["FeedBackType"] = value;
        }
        get
        {
            //return this.strType;
            if (null != this.ViewState["FeedBackType"])
            {
                return this.ViewState["FeedBackType"].ToString();
            }
            return "";
        }
    }

    //private string strMasterCode = "";

    public string MasterCode
    {
        set
        {
            //this.strMasterCode = value;
            this.ViewState["MasterCode"] = value;
        }
        get
        {
            //return this.strMasterCode;
            if (null != this.ViewState["MasterCode"])
            {
                return this.ViewState["MasterCode"].ToString();
            }
            return "";
        }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            // 在此处放置用户代码以初始化页面

            InitPage();
            LoadData();
        }
        catch (Exception ex)
        {
           // ApplicationLog.WriteLog(this.ToString(), ex, "反馈载入失败");
            //Response.Write(Rms.Web.JavaScript.Alert(true, "反馈载入失败：" + ex.Message));
        }

    }

    private void InitPage()
    { }

    private void LoadData()
    {
        //EntityData	entity = WBSDAO.GetFeedBackByMasterCode(this.strMasterCode);
        EntityData entity = WBSDAO.GetFeedBackByMasterCode(this.ViewState["MasterCode"].ToString());
        if (entity.HasRecord())
        {
            DataView dv = new DataView(entity.CurrentTable, "", "CreateDate desc", System.Data.DataViewRowState.CurrentRows);
            foreach (DataRowView drv in dv)
            {
                drv["Content"] = drv["Content"].ToString().Replace("\n", "<br>");
            }
            this.rpFeedBack.DataSource = dv;
            this.rpFeedBack.DataBind();
        }
    }
}
