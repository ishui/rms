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
using Rms.ORMap;

public partial class UserControls_manycurrencycostInfo : System.Web.UI.UserControl
{
    protected StandardEntityDAO _dao = null;

    protected void Page_Load(object sender, System.EventArgs e)
    {
    }

    #region Web 窗体设计器生成的代码
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。

        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    ///		设计器支持所需的方法 - 不要使用代码编辑器

    ///		修改此方法的内容。

    /// </summary>
    private void InitializeComponent()
    {

    }
    private void OnInitEvent()
    {
        this.Load += new System.EventHandler(this.Page_Load);
    }
    #endregion

    /// <summary>
    /// 是否处于可编辑状态  true 编辑状态 false 显示状态

    /// </summary>
    public bool IsEditMode
    {
        get
        {
            if (ViewState["IsEditMode"] == null)
                return true;
            return (bool)ViewState["IsEditMode"];
        }
        set
        {
            ViewState["IsEditMode"] = value;
        }
    }
    public bool IsAllowAdd
    {
        get
        {

            if (ViewState["IsAllowAdd"] == null)
                return true;
            return (bool)ViewState["IsAllowAdd"];
        }
        set
        {
            ViewState["IsAllowAdd"] = value;
        }
    }

    /// <summary>
    /// 数据表

    /// </summary>
    public DataTable CashTable
    {
        get
        {
            if (ViewState["CashTable"] == null)
                return null;
            return (DataTable)ViewState["CashTable"];
        }
        set
        {
            ViewState["CashTable"] = value;
        }
    }

    /// <summary>
    /// 代码
    /// </summary>
    public string CashMessageTypeCode
    {
        get
        {
            if (this.ViewState["CashMessageTypeCode"] == null)
                return "";
            return this.ViewState["CashMessageTypeCode"].ToString();
        }
        set
        {
            this.ViewState["CashMessageTypeCode"] = value;
        }
    }
    /// <summary>
    /// 事务对象
    /// </summary>
    public StandardEntityDAO dao
    {
        get
        {
            if (_dao == null)
            {
                _dao = new StandardEntityDAO("BiddingReturnCost");
            }
            return this._dao;
        }
        set
        {
            _dao = value;
        }
    }

    /// <summary>
    /// 读取信息
    /// </summary>
    /// <returns></returns>
    private void GetCostDetail()
    {
        RmsPM.BLL.Cash_Message cashMessage = new RmsPM.BLL.Cash_Message();
        cashMessage.CashMessageTypeCode = this.CashMessageTypeCode;
        cashMessage.CashMessageType = "回标";
        DataTable dt = cashMessage.GetCash_Messages();
        RmsPM.BLL.Cash_Detail cd = new RmsPM.BLL.Cash_Detail();
        if (dt.Rows.Count != 0)
        {
            cd.Cash_MessageCode = dt.Rows[dt.Rows.Count - 1]["CashMessageCode"].ToString();
            this.CashTable = cd.GetCash_Details();
        }
        else
        {
            this.CashTable = cd.GetCash_Details();
            this.CashTable.Clear();
        }
    }

    private void BindData()
    {
        foreach (DataRow dr in this.CashTable.Select())
        {
            this.lblCashDetail.Text = dr["Cash"].ToString() + dr["MoneyType"].ToString();
        }
    }

    public void LoadData()
    {
        GetCostDetail();
        BindData();
    }
}