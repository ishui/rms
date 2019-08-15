

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
    using Rms.ORMap;
    using Rms.Web;

    public partial class BiddingConditionFileManage : PageBase
    {
        private string _BiddingConditionFileCode = "";

        public string BiddingConditionFileCode
        {
            get
            {
                if (_BiddingConditionFileCode == "")
                {
                    if (this.ViewState["_BiddingConditionFileCode"] != null)
                        return this.ViewState["_BiddingConditionFileCode"].ToString();
                    return "";
                }
                return _BiddingConditionFileCode;
            }
            set
            {
                _BiddingConditionFileCode = value;
                this.ViewState["_BiddingConditionFileCode"] = value;
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

            

            this.BiddingConditionFileCode = this.Request["BiddingConditionFileCode"] + "";
            string BiddingCode = this.Request["BiddingCode"] + "";
            string ProjectCode = this.Request["ProjectCode"] + "";
            if (BiddingConditionFileCode == "")
                BiddingConditionFileCode = this.Request["ApplicationCode"] + "";


            if (BiddingConditionFileCode != "")
            {
                this.btnModify.Visible = true;
                this.btnSave.Visible = false;
                this.btnDel.Visible = true;
                this.BiddingConditionFileModigy1.State = ModuleState.Eyeable;
            }
            else
            {
                this.btnModify.Visible = false;
                this.btnSave.Visible = true;
                this.btnDel.Visible = false;
                this.BiddingConditionFileModigy1.State = ModuleState.Operable;
            }
            this.BiddingConditionFileModigy1.ApplicationCode = BiddingConditionFileCode;
            this.BiddingConditionFileModigy1.BiddingConditionFileCode = BiddingConditionFileCode;
            this.BiddingConditionFileModigy1.BiddingCode = BiddingCode;
            this.BiddingConditionFileModigy1.ProjectCode = ProjectCode;
            this.BiddingConditionFileModigy1.InitControl();

            if (!this.user.HasRight("210801"))//招标技术条件新增

            {
                this.btnSave.Visible = false;
            }
            if (!this.user.HasRight("210803"))//招标技术条件删除

            {
                this.btnDel.Visible = false;
            }
            if (!this.user.HasRight("210804"))//招标技术条件修改

            {
                this.btnModify.Visible = false;
            }

           
        }
        protected void btnModify_ServerClick(object sender, EventArgs e)
        {
            this.btnModify.Visible = false;
            this.btnSave.Visible = true;
            this.BiddingConditionFileModigy1.State = ModuleState.Operable;


            this.BiddingConditionFileModigy1.InitControl();
        }
        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            string ErrMessage = "";

            ErrMessage = this.BiddingConditionFileModigy1.SubmitData();
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
            ErrMessage = this.BiddingConditionFileModigy1.DeleteBiddingConditionFile();
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
