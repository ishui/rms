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
using RmsPM;
using RmsPM.BLL;
using Rms.ORMap;
using RmsPM.Web;

public partial class BiddingControl_BiddingEmit : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private string _BiddingEmitCode = "";
    private bool _IsEdit = true;
    private string _BiddingCode = "";
    private StandardEntityDAO _dao;

    /// <summary>
    /// 发标代码
    /// </summary>
    public string BiddingEmitCode
    {
        get
        {
            if (_BiddingEmitCode == "")
            {
                if (this.ViewState["_BiddingEmitCode"] != null)
                    return this.ViewState["_BiddingEmitCode"].ToString();
                return "";
            }
            return _BiddingEmitCode;
        }
        set
        {
            _BiddingEmitCode = value;
            this.ViewState["_BiddingEmitCode"] = value;
        }
    }
    /// <summary>
    /// 招投标代码

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
    /// <summary>
	/// 事务对象
	/// </summary>
	public StandardEntityDAO dao
	{
		get
		{
			return this._dao;
		}
		set
		{
			_dao = value;
		}
	}
    /// <summary>
    /// 是否编辑状态

    /// </summary>
    public bool IsEdit
    {
        get
        {
            return _IsEdit;
        }
        set
        {
            _IsEdit = value;
            this.ViewState["_IsEdit"] = value;
        }
    }
    /// <summary>
    /// 当前用户
    /// </summary>
    public string UserCode
    {
        get {
            if ((User)Session["user"] != null)
            {
                return ((User)Session["user"]).UserCode;
            }
            else
            {
                return "";
            }
        }
    }

    public string NowState
    {
        get
        {
            if (Request.QueryString["NowState"] == "5")
            {
                return "6";
            }
            else
            {
                return "2";
            }
        }
    }
    /// ****************************************************************************
    /// <summary>
    /// 组件初始化

    /// </summary>
    /// ****************************************************************************
    public void InitControl()
    {
        if (this.IsEdit)
        {
            //EyeableDiv.Visible = false;
            OperableDiv.Visible = true;
        }
        else
        {
            //EyeableDiv.Visible = true;
            OperableDiv.Visible = false;
        }
        LoadData(this.IsEdit);
    }
    /// ****************************************************************************
    /// <summary>
    /// 数据加载
    /// </summary>
    /// ****************************************************************************
    private void LoadData(bool Flag)
    {
        if (this.BiddingEmitCode != "")
        {
            RmsPM.BLL.BiddingEmit cBiddingEmit = new RmsPM.BLL.BiddingEmit();
            cBiddingEmit.BiddingEmitCode = this.BiddingEmitCode;

            if (Flag)
            {
                this.txtEmitNumber.Value = cBiddingEmit.EmitNumber;
                this.txtEmitDate.Value = cBiddingEmit.EmitDate;
                this.txtEndDate.Value = cBiddingEmit.EndDate;
                this.txtPrejudicationDate.Value = cBiddingEmit.PrejudicationDate;
                this.txtTotalRemark.Text = cBiddingEmit.TotalRemark2;
                this.BiddingCode = cBiddingEmit.BiddingCode;
                Show_ttachMentAdd(cBiddingEmit.BiddingEmitCode);

                this.txtEmitNumber.Disabled = true;
                this.txtEmitDate.ReadOnly = true;
            }
       }
    }

    /// ****************************************************************************
    /// <summary>
    /// 提交数据
    /// </summary>
    /// ****************************************************************************
    public void SubmitData()
    {
        RmsPM.BLL.BiddingEmit cBiddingEmit = new RmsPM.BLL.BiddingEmit();
        cBiddingEmit.BiddingEmitCode = this.BiddingEmitCode;
        cBiddingEmit.BiddingCode = this.BiddingCode;
        cBiddingEmit.EmitNumber = this.txtEmitNumber.Value;
        if (this.txtEmitDate.Value != "")
            cBiddingEmit.EmitDate = this.txtEmitDate.Value;
        if (this.txtEndDate.Value != "")
            cBiddingEmit.EndDate = this.txtEndDate.Value;
        if (this.txtPrejudicationDate.Value != "")
            cBiddingEmit.PrejudicationDate = this.txtPrejudicationDate.Value;
        cBiddingEmit.CreatUser = this.UserCode;
        cBiddingEmit.CreatDate = DateTime.Now.ToString();
        cBiddingEmit.TotalRemark2 = this.txtTotalRemark.Text;
        cBiddingEmit.dao = this.dao;
        cBiddingEmit.BiddingEmitSubmit();
    }
    /// <summary>
    /// 发标编号
    /// </summary>
    /// <param name="EmitCode"></param>
    private void Show_ttachMentAdd(string EmitCode)
    {
        AttachMentAdd1.AttachMentType = "BiddingReturnModify2";
        AttachMentAdd1.MasterCode = EmitCode;
    }
}
