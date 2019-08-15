

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
    using RmsPM.Web.WorkFlowControl;
    using Rms.Web;

    public partial class BiddingFileManage : PageBase
    {
        private string _BiddingFileCode = "";
      
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


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                this.InitPage();
            }
        }

        protected void InitPage()
        {

            
            
            this.BiddingFileCode = this.Request["BiddingFileCode"] + "";
            string BiddingCode = this.Request["BiddingCode"] + "";
            string ProjectCode = this.Request["ProjectCode"] + "";
            if (BiddingFileCode == "")
                BiddingFileCode = this.Request["ApplicationCode"] + "";


            if (BiddingFileCode != "")
            {
                this.btnModify.Visible = true;
                this.btnSave.Visible = false;
                this.btnDel.Visible = true;
                this.BiddingFileModigy1.State = ModuleState.Eyeable;


            }
            else
            {
                this.btnModify.Visible = false;
                this.btnSave.Visible = true;
                this.btnDel.Visible = false;
                this.BiddingFileModigy1.State = ModuleState.Operable;
            }
            this.BiddingFileModigy1.ApplicationCode = BiddingFileCode;
            this.BiddingFileModigy1.BiddingFileCode = BiddingFileCode;
            this.BiddingFileModigy1.BiddingCode = BiddingCode;
            this.BiddingFileModigy1.ProjectCode = ProjectCode;
            this.BiddingFileModigy1.InitControl();


            if (!this.user.HasRight("210901"))//招标文件新增
            {
                this.btnSave.Visible = false;
            }
            if (!this.user.HasRight("210902"))//招标文件修改
            {
                this.btnModify.Visible = false;
            }
            if (!this.user.HasRight("210903"))//招标文件删除
            {
                this.btnDel.Visible = false;
            }
            switch (this.BiddingFileModigy1.BiddingFileState)
            {
                case "7":
                case "9":
                case "0":
                    this.btnModify.Visible=false;
                    this.btnDel.Visible = false;
                    this.btnSave.Visible = false;
                    break;


                default:
                  
                    break;
            }
           
        }
        protected void btnModify_ServerClick(object sender, EventArgs e)
        {
            this.btnModify.Visible = false;
            this.btnSave.Visible = this.user.HasRight("210901");
            this.BiddingFileModigy1.State = ModuleState.Operable;


            this.BiddingFileModigy1.InitControl();
        }
        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            string ErrMessage = "";

            ErrMessage = this.BiddingFileModigy1.SubmitData();
            Response.Write(JavaScript.ScriptStart);
            if (ErrMessage != "")
            {

                Response.Write(Rms.Web.JavaScript.Alert(false, ErrMessage));
            }
            else
            {
                Response.Write(Rms.Web.JavaScript.OpenerReload(false));
                Response.Write(Rms.Web.JavaScript.WinClose(false));
            }
            Response.Write(JavaScript.ScriptEnd);
        }
        protected void btnDel_ServerClick(object sender, EventArgs e)
        {
            string ErrMessage = "";
            ErrMessage = this.BiddingFileModigy1.DeleteBiddingFile();
            Response.Write(JavaScript.ScriptStart);
            if (ErrMessage != "")
            {

                Response.Write(Rms.Web.JavaScript.Alert(false, ErrMessage));
            }
            else
            {
                Response.Write(Rms.Web.JavaScript.OpenerReload(false));
                Response.Write(Rms.Web.JavaScript.WinClose(false));
            }
            Response.Write(JavaScript.ScriptEnd);
        }
    }
}