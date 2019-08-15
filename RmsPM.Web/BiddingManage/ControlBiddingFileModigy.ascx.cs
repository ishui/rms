

namespace RmsPM.Web.BiddingManage
{

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
    using RmsPM.Web.WorkFlowControl;

    public partial class ControlBiddingFileModigy : BiddingControlBase
    {
        private string _BiddingFileCode="";
        private string _BiddingCode="";
        private string _Remark="";
        private string _BiddingFileState="";

        /// <summary>
        /// 招标文件编号
        /// </summary>
        public string BiddingFileCode
        {
            get
            {
                if (_BiddingFileCode == "")
                {
                    if (this.ViewState["_BiddingFileCode"] != null)
                        return this.ViewState["_BiddingFileCode"].ToString();
                    return "";
                }
                return _BiddingFileCode;
            }
            set
            {
                _BiddingFileCode = value;
                this.ViewState["_BiddingFileCode"] = value;
            }
        }

        /// <summary>
        /// 招标计划编号
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
        /// 招标文件摘要说明
        /// </summary>
        public string Remark
        {
            get
            {
                if (_Remark == "")
                {
                    if (this.ViewState["_Remark"] != null)
                        return this.ViewState["_Remark"].ToString();
                    return "";
                }
                return _Remark;
            }
            set
            {
                _Remark = value;
                this.ViewState["_Remark"] = value;
            }
        }


        /// <summary>
        /// 招标文件状态

        /// </summary>
        public string BiddingFileState
        {
            get
            {
                if (_BiddingFileState == "")
                {
                    if (this.ViewState["_BiddingFileState"] != null)
                        return this.ViewState["_BiddingFileState"].ToString();
                    return "";
                }
                return _BiddingFileState;
            }
            set
            {
                _BiddingFileState = value;
                this.ViewState["_BiddingFileState"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AttachMentAdd1.AttachMentType = "BiddingFile1";
            AttachMentList1.AttachMentType = "BiddingFile1";

            if (this.BiddingFileCode!=null && this.BiddingFileCode != "")
            {
                AttachMentAdd1.MasterCode = this.BiddingFileCode;
                AttachMentList1.MasterCode = this.BiddingFileCode;
            }
        }

        /// ****************************************************************************
        /// <summary>
        /// 组件初始化

        /// </summary>
        /// ****************************************************************************
        public void InitControl()
        {
            if (this.State == ModuleState.Sightless)//不可见的
            {
                this.Visible = false;
            }
            else if (this.State == ModuleState.Operable)//可操作的
            {
                LoadData(true);
                EyeableDiv.Visible = false;
                OperableDiv.Visible = true;
            }
            else if (this.State == ModuleState.Eyeable)//可见的

            {
                LoadData(false);
                OperableDiv.Visible = false;
                EyeableDiv.Visible = true;
            }
            else if (this.State == ModuleState.Begin)//可见的

            {
                LoadData(false);
                OperableDiv.Visible = false;
                EyeableDiv.Visible = true;
            }
            else if (this.State == ModuleState.End)//可见的

            {
                LoadData(false);
                OperableDiv.Visible = false;
                EyeableDiv.Visible = true;
            }
            else
            {
                this.Visible = false;
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// 数据加载
        /// </summary>
        /// ****************************************************************************
        private void LoadData(bool Flag)
        {

            RmsPM.BLL.BiddingFile cBiddingFile = new RmsPM.BLL.BiddingFile();
            if (this.ApplicationCode != "")
            {
                this.BiddingFileCode = this.ApplicationCode;
            }
            else if (this.BiddingFileCode != "")
            {
                this.ApplicationCode = this.BiddingFileCode;
            }
        
            //System.Data.DataTable dtBiddingFile = cBiddingFile.GetBiddings();
            //if (dtBiddingFile.Rows.Count != 0)
            //{
            //    this.BiddingFileCode = dtBiddingFile.Rows[0]["BiddingFileCode"].ToString();
            //    cBiddingFile.BiddingFileCode = this.BiddingFileCode;
            if (this.ApplicationCode != "")
            {
                EntityData entitydata=RmsPM.BLL.BiddingFile.GetBiddingFileByCode(this.ApplicationCode);

                if (entitydata.HasRecord())
                {
                    this.BiddingFileState = entitydata.GetString("state");
                    if (Flag)
                    {
                        RmsPM.BLL.Bidding cBidding = new RmsPM.BLL.Bidding();
                        cBidding.BiddingCode = this.BiddingCode;
                        string LinkUrl = "<a onclick=OpenLargeWindow('../BiddingManage/biddingmodify.aspx?BiddingCode=" + this.BiddingCode + "&State=edit&ProjectCode=" + cBidding.ProjectCode + "','ControlBiddingFileModigy3')>" + cBidding.Title + "</a>";
                        //this.tdBiddingTitle.InnerHtml = cBidding.Title;
                        this.txtBiddingTitle.InnerHtml = LinkUrl;
                        this.txtProjectName.InnerHtml = RmsPM.BLL.ProjectRule.GetProjectName(cBidding.ProjectCode);

                        
                        this.TxtBiddingFileName.Value = entitydata.GetString("Remark");
                        this.TxtNumber.Value = entitydata.GetString("BiddingFileNumber");
                        this.tdBiddingFileState1.InnerHtml = RmsPM.BLL.BiddingFile.GetBiddingFileStatusName(BiddingFileState);
                   
                    
                    }
                    else
                    {
                        RmsPM.BLL.Bidding cBidding = new RmsPM.BLL.Bidding();
                        cBidding.BiddingCode = this.BiddingCode;
                        string LinkUrl = "<a onclick=OpenLargeWindow('../BiddingManage/biddingmodify.aspx?BiddingCode=" + this.BiddingCode + "&State=edit&ProjectCode=" + cBidding.ProjectCode + "','ControlBiddingFileModigy1')>" + cBidding.Title + "</a>";
                        //this.tdBiddingTitle.InnerHtml = cBidding.Title;
                        this.tdBiddingTitle.InnerHtml = LinkUrl;
                        this.tdProjectName.InnerHtml = RmsPM.BLL.ProjectRule.GetProjectName(cBidding.ProjectCode);


                        this.TdBiddingFileName.InnerHtml = entitydata.GetString("Remark");
                        this.TdNumber.InnerHtml = entitydata.GetString("BiddingFileNumber");
                        this.tdBiddingFileState2.InnerHtml = RmsPM.BLL.BiddingFile.GetBiddingFileStatusName(BiddingFileState);
                    }
                }
                entitydata.Dispose();
            }
            else
            {
                RmsPM.BLL.Bidding cBidding = new RmsPM.BLL.Bidding();
                cBidding.BiddingCode = this.BiddingCode;
                string LinkUrl = "<a onclick=OpenLargeWindow('../BiddingManage/biddingmodify.aspx?BiddingCode=" + this.BiddingCode + "&State=edit&ProjectCode=" + cBidding.ProjectCode + "','ControlBiddingFileModigy2')> " + cBidding.Title + "</a>";
                //this.tdBiddingTitle.InnerHtml = cBidding.Title;
                this.txtBiddingTitle.InnerHtml = LinkUrl;
                this.txtProjectName.InnerHtml = RmsPM.BLL.ProjectRule.GetProjectName(cBidding.ProjectCode);
            }
           

        }

        /// ****************************************************************************
        /// <summary>
        /// 提交数据
        /// </summary>
        /// ****************************************************************************
        public override string SubmitData()
        {
            string Errmsg = "";
            try
            {

                if (this.TxtBiddingFileName.Value == "")
                {
                    Errmsg = "招标文件名称不允许为空";
                    return Errmsg;
                }
                else if (this.TxtNumber.Value == "")
                {
                    Errmsg = "招标文件编号不允许为空";
                    return Errmsg;
                }

                if (this.ApplicationCode != "")
                {
                    this.BiddingFileCode = this.ApplicationCode;
                }
                else if (this.BiddingFileCode != "")
                {
                    this.ApplicationCode = this.BiddingFileCode;
                }

                RmsPM.BLL.BiddingFile cBiddingFile = new RmsPM.BLL.BiddingFile();

                cBiddingFile.BiddingFileCode = this.BiddingFileCode + "";

                cBiddingFile.BiddingCode = this.BiddingCode + "";
                cBiddingFile.BiddingFileNumber = this.TxtNumber.Value;
                cBiddingFile.Remark = this.TxtBiddingFileName.Value;
                cBiddingFile.State = "1";


                cBiddingFile.BiddingFileAdd();
                this.BiddingFileCode = cBiddingFile.BiddingFileCode;

                this.AttachMentAdd1.SaveAttachMent(this.BiddingFileCode);
                return Errmsg;

            }
            catch (System.Exception ec)
            {
                Errmsg = ec.Message;
                return Errmsg;
            }
        }

        public string DeleteBiddingFile()
        {
            string Errmsg = "";
            try
            {
                RmsPM.BLL.BiddingFile cBiddingFile = new RmsPM.BLL.BiddingFile();

                cBiddingFile.BiddingFileCode = this.BiddingFileCode;
                //RmsPM.BLL.BiddingFile.DeleteBiddingFile
                cBiddingFile.BiddingFileDelete();
                return Errmsg;
            }
            catch (System.Exception ec)
            {
                Errmsg = ec.Message;
                return Errmsg;
            }

        }


    }
}
