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
using RmsPM.BLL;

namespace RmsPM.Web.Systems
{
    public partial class Systems_StampDutyInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string StampDutyID = Request["StampDutyID"];
                if (string.IsNullOrEmpty(StampDutyID) || !Rms.Check.StringCheck.IsInt(StampDutyID))
                {
                    Model.ChangeMode(FormViewMode.Insert);
                }
                else
                {
                    ModelData.SelectParameters[0].DefaultValue = StampDutyID;
                    Model.ChangeMode(FormViewMode.ReadOnly);

                }
            }
        }

        private void IniPage()
        {
            this.txtRefreshScript.Value = Request["RefreshScript"] + "";
            string roleCode = Request["RoleCode"] + "";
        }

        protected void btnAdd_ServerClick(object sender, System.EventArgs e)
        {
            string projectCode = Request["ProjectCode"] + "";
            Response.Redirect("StampDutyInfo.aspx?ProjectCode=" + projectCode);
        }

        private void WriteRefreshScript()
        {
            Response.Write(Rms.Web.JavaScript.ScriptStart);
            if (this.txtRefreshScript.Value.Trim() != "")
            {
                Response.Write("window.opener." + this.txtRefreshScript.Value.Trim());
            }
            else
            {
                Response.Write(Rms.Web.JavaScript.OpenerReload(false));
            }
            Response.Write(Rms.Web.JavaScript.WinClose(false));
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }


        protected void Model_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            Response.Write(Rms.Web.JavaScript.ScriptStart);
            Response.Write(Rms.Web.JavaScript.OpenerReload(false));
            Response.Write(Rms.Web.JavaScript.WinClose(false));
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }
        protected void Model_ItemDeleted(object sender, FormViewDeletedEventArgs e)
        {
            Response.Write(Rms.Web.JavaScript.ScriptStart);
            Response.Write(Rms.Web.JavaScript.OpenerReload(false));
            Response.Write(Rms.Web.JavaScript.WinClose(false));
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }
        protected void Model_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            Response.Write(Rms.Web.JavaScript.ScriptStart);
            Response.Write(Rms.Web.JavaScript.OpenerReload(false));
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }
    }
}