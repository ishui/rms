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
using RmsPM.Web.WorkFlowControl;
using Rms.ORMap;

public partial class WorkFlowControl_WorkFlowFormSign : WorkFlowFormOpinion
{
    private string _OpinionType = null;
    private string _ControlType = null;
    private string _OpinionUserCode = null;
    private ModuleState _StateConfirm = ModuleState.Unbeknown;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public override ModuleState StateConfirm
    {
        get
        {
            if (_StateConfirm == ModuleState.Unbeknown)
            {
                if (this.ViewState["_StateConfirm"] != null)
                    return (ModuleState)this.ViewState["_StateConfirm"];
                return ModuleState.Unbeknown;
            }
            return _StateConfirm;
        }
        set
        {
            _StateConfirm = value;
            this.ViewState["_StateConfirm"] = value;
        }
    }

    /// <summary>
    /// 意见类型
    /// </summary>
    //		public string OpinionType
    //		{
    override public string OpinionType
    {
        get
        {
            if (_OpinionType == null)
            {
                if (this.ViewState["_OpinionType"] != null)
                    return this.ViewState["_OpinionType"].ToString();
                return "";
            }
            return _OpinionType;
        }
        set
        {
            _OpinionType = value;
            this.ViewState["_OpinionType"] = value;
        }
    }

		/// <summary>
		/// 关联用户代码
		/// </summary>
    override public string OpinionUserCode
    {
        get
        {
            if (_OpinionUserCode == null)
            {
                if (this.ViewState["_OpinionUserCode"] != null)
                    return this.ViewState["_OpinionUserCode"].ToString();
                return "";
            }
            return _OpinionUserCode;
        }
        set
        {
            _OpinionUserCode = value;
            this.ViewState["_OpinionUserCode"] = value;
        }
    }

    public override void InitControl()
    {
        this.imgSign.Visible = false;

        switch ( this.State )
        {
            case ModuleState.Sightless :    //不可见的
                this.Visible = false;
                break;
            case  ModuleState.Operable :    //可操作的
                LoadSign();
                break;
            case ModuleState.Eyeable:       //可见的
                LoadSign();
                break;
            case ModuleState.Begin:         //可见的
                LoadSign();
                break;
            case ModuleState.End:           //可见的
                LoadSign();
                break;
            default:
                this.Visible = false;
                break;
        }
    }

    private void LoadSign()
    {
        EntityData entity = this.GetData();
        if (entity.HasRecord())
        {
            string ud_sUserCode = RmsPM.BLL.SystemRule.GetUserCodeByUserName( entity.CurrentRow["OpinionUser"].ToString() );

            string ud_sAttachMentCode = RmsPM.BLL.WBSRule.GetAttachMentCodeByUserCode(ud_sUserCode);

            if (ud_sAttachMentCode != "")
            {
                this.imgSign.Visible = true;
                this.imgSign.ImageUrl = "../Project/WBSAttachMentView.aspx?Action=View&AttachMent=0&AttachMentCode=" + ud_sAttachMentCode;
            }
            else
            {
                this.imgSign.Visible = false;
            }
        }

        entity.Dispose();

    }

    private EntityData GetData()
    {
        RmsPM.DAL.QueryStrategy.PurchaseFlowOpinionStrategyBuilder sb = new RmsPM.DAL.QueryStrategy.PurchaseFlowOpinionStrategyBuilder();
        //sb.AddStrategy(new Strategy(RmsPM.DAL.QueryStrategy.PurchaseFlowOpinionStrategyName.ObjectCode, this.ApplicationCode));
        sb.AddStrategy(new Strategy(RmsPM.DAL.QueryStrategy.PurchaseFlowOpinionStrategyName.OpinionType, this.OpinionType));
        sb.AddStrategy(new Strategy(RmsPM.DAL.QueryStrategy.PurchaseFlowOpinionStrategyName.CaseCode, this.CaseCode));
        if (this.OpinionUserCode != "")
            sb.AddStrategy(new Strategy(RmsPM.DAL.QueryStrategy.PurchaseFlowOpinionStrategyName.OpinionUserCode, this.OpinionUserCode));

        sb.AddOrder("OpinionDate", false);

        string sql = sb.BuildMainQueryString();
        QueryAgent QA = new QueryAgent();
        EntityData entity = QA.FillEntityData("PurchaseFlowOpinion", sql);
        QA.Dispose();
        return entity;
    }

    public override string CaseCode
    {
        get
        {
            if(this.ViewState["_CaseCode"] == null)
                return "";
            return this.ViewState["_CaseCode"].ToString();

        }
        set
        {
            this.ViewState["_CaseCode"] = value;
        }
    }
    public override string Title
    {
        get
        {
            throw new Exception("The method or operation is not implemented.");
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public override string OpinionConfirm
    {
        get
        {
            throw new Exception("The method or operation is not implemented.");
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public override void SubmitData()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    public override void DeleteData()
    {
        throw new Exception("The method or operation is not implemented.");
    }


    public override string ControlType
    {
        get
        {
            throw new Exception("The method or operation is not implemented.");
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
