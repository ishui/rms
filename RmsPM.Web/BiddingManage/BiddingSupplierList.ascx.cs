using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RmsPM.BLL;
using RmsPM.DAL;
using RmsPM;
using Rms.DBUtility;
using RmsPM.Web;

public partial class BiddingManage_BiddingSupplierList : System.Web.UI.UserControl
{
    private string _BiddingCode;
    protected User user = null;
    /// <summary>
    /// 在初始化时设定


    /// </summary>
    public string BiddingCode
    {
        get { return _BiddingCode; }
        set { _BiddingCode = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataSource1.ConnectionString = SqlHelper.DBConnString;
        SqlDataSource1.SelectParameters["BiddingCode"].DefaultValue = this.BiddingCode;
        SqlDataSource1.DataBind();
        if (!this.IsPostBack)
        {
            if ((Session["User"] == null) && (ConfigurationSettings.AppSettings["IsDebug"] == "1") && (ConfigurationSettings.AppSettings["DebugUser"] != ""))
            {
                Session["User"] = new RmsPM.Web.User(ConfigurationSettings.AppSettings["DebugUser"]);
            }

            if (Session["User"] != null)
            {
                this.user = (User)Session["User"];
                this.btnAdd.Visible = this.user.HasRight("210201");
                this.btnApprove.Visible = this.user.HasRight("210202");
                this.btnCancelApprove.Visible = this.user.HasRight("210203");
                this.btnRemove.Visible = this.user.HasRight("210204");
            }
        }


    }
    /// <summary>
    /// 批量审核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox isSelected = (CheckBox)row.FindControl("isSelected");
            HiddenField hfBiddingSupplierCode = (HiddenField)row.FindControl("BiddingSupplierCode");
            if (isSelected != null&&isSelected.Checked)
            {
                string BiddingSupplierCode = hfBiddingSupplierCode.Value;
                string sql = string.Format("update BiddingSupplier set flag=1 where BiddingSupplierCode='{0}'",BiddingSupplierCode);
                SqlHelper.ExecuteNonQuery(SqlHelper.DBConnString, CommandType.Text, sql, null);
                string BiddingCode = this.BiddingCode;
                sql = string.Format("update Bidding set state=1 where BiddingCode='{0}' and state=0", BiddingCode);
                SqlHelper.ExecuteNonQuery(SqlHelper.DBConnString, CommandType.Text, sql, null);
            }
        }
        SqlDataSource1.SelectParameters["BiddingCode"].DefaultValue = "-1";
        SqlDataSource1.DataBind();
        SqlDataSource1.SelectParameters["BiddingCode"].DefaultValue = this.BiddingCode;
        SqlDataSource1.DataBind();
    }
    /// <summary>
    /// 撤销审核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelApprove_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox isSelected = (CheckBox)row.FindControl("isSelected");
            HiddenField hfBiddingSupplierCode = (HiddenField)row.FindControl("BiddingSupplierCode");
            if (isSelected != null && isSelected.Checked)
            {
                string BiddingSupplierCode = hfBiddingSupplierCode.Value;
                string sql = string.Format("update BiddingSupplier set flag='' where BiddingSupplierCode='{0}'", BiddingSupplierCode);
                SqlHelper.ExecuteNonQuery(SqlHelper.DBConnString, CommandType.Text, sql, null);
            }
        }
        SqlDataSource1.SelectParameters["BiddingCode"].DefaultValue = "-1";
        SqlDataSource1.DataBind();
        SqlDataSource1.SelectParameters["BiddingCode"].DefaultValue = this.BiddingCode;
        SqlDataSource1.DataBind();
         
    }
    /// <summary>
    /// 添加投标单位。按供应商选择框的
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (this.HideSupplierCode.Value != "")
        {
            SaveBiddingSupplier();
            SqlDataSource1.SelectParameters["BiddingCode"].DefaultValue = "-1";
            SqlDataSource1.DataBind();
            SqlDataSource1.SelectParameters["BiddingCode"].DefaultValue = this.BiddingCode;
            SqlDataSource1.DataBind();
        }
    }

    /// <summary>
    /// 保存数据，产生一条


    /// </summary>
    private void SaveBiddingSupplier()
    {
        try
        {
            string BiddingPrejudicationCode = "";

            BiddingPrejudication prejud = new BiddingPrejudication();
            BiddingPrejudicationCode = prejud.GetLastPrejudicationCodeByBiddingCode(this.BiddingCode);
            if (BiddingPrejudicationCode == "" || BiddingPrejudicationCode == null)
            {
                prejud.BiddingCode = this.BiddingCode;
                prejud.CreateDate = DateTime.Today.ToString();
                prejud.Flag = "";
                prejud.Remark = "";
                prejud.UserCode = ((User)Session["User"]).UserCode;
                prejud.BiddingPrejudicationAdd();
                BiddingPrejudicationCode = prejud.BiddingPrejudicationCode;
            }

            BiddingSupplier cBiddingSupplier = new BiddingSupplier();

            string strOrderCode = "1";

            cBiddingSupplier.BiddingPrejudicationCode = BiddingPrejudicationCode;
            cBiddingSupplier.SupplierCode = this.HideSupplierCode.Value;
            cBiddingSupplier.NominateUser = "";
            cBiddingSupplier.NominateDate = DateTime.Today.ToString();
            cBiddingSupplier.UserCode = ((User)Session["User"]).UserCode;
            cBiddingSupplier.OrderCode = strOrderCode;
            cBiddingSupplier.State = "";
            cBiddingSupplier.Flag = "";
            cBiddingSupplier.BiddingSupplierAdd();
            HideSupplierCode.Value = "";
        }
        catch (Exception ex)
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, ex.Message));
            ApplicationLog.WriteLog(this.ToString(), ex, "");
        }
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox isSelected = (CheckBox)row.FindControl("isSelected");
            HiddenField hfBiddingSupplierCode = (HiddenField)row.FindControl("BiddingSupplierCode");
            if (isSelected != null && isSelected.Checked)
            {
                string BiddingSupplierCode = hfBiddingSupplierCode.Value;
                BiddingSupplier cBiddingSupplier = new BiddingSupplier();
                cBiddingSupplier.BiddingSupplierCode = BiddingSupplierCode;
                cBiddingSupplier.BiddingSupplierDelete();

                //string sql = string.Format("update BiddingSupplier set flag='' where BiddingSupplierCode='{0}'", BiddingSupplierCode);
                //SqlHelper.ExecuteNonQuery(SqlHelper.DBConnString, CommandType.Text, sql, null);
            }
        }
        SqlDataSource1.SelectParameters["BiddingCode"].DefaultValue = "-1";
        SqlDataSource1.DataBind();
        SqlDataSource1.SelectParameters["BiddingCode"].DefaultValue = this.BiddingCode;
        SqlDataSource1.DataBind();
    }
}
