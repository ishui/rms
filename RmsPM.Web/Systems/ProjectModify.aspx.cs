using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

using Rms.ORMap;
using RmsPM.DAL;
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;

namespace RmsPM.Web.Systems
{
    public partial class ProjectModify : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    EntityData entity = DAL.EntityDAO.OBSDAO.GetStandard_UnitByCode(Request.QueryString["UnitCode"]);
                    if (entity.HasRecord())
                    {
                        this.parentUnit.Value = entity.GetString("ParentUnitCode");
                        EntityData entityParent = DAL.EntityDAO.OBSDAO.GetStandard_UnitByCode(this.parentUnit.Value);
                        if (entityParent.HasRecord())
                        {
                            this.parentUnitName.Value = entityParent.GetString("UnitName");
                        }
                        entityParent.Dispose();
                    }
                    entity.Dispose();

                    EntityData projectds = ProjectDAO.GetProjectByCode(Request.QueryString["ProjectCode"].ToString());
                    RmsPM.BLL.PageFacade.LoadSubjectSetSelect(this.sltSubjectSet, "");
                    this.sltSubjectSet.Value = projectds.GetString("SubjectSetCode");

                    projectds.Dispose();
                }
                catch (Exception ex)
                {
                    ApplicationLog.WriteLog(this.ToString(), ex, "读取项目信息错误");
                    Response.Write(Rms.Web.JavaScript.Alert(true, "读取项目信息错误"));
                }
            }


        }



        protected void btnSave_ServerClick(object sender, System.EventArgs e)
        {

            try
            {
                EntityData projectds = ProjectDAO.GetProjectByCode(Request.QueryString["ProjectCode"].ToString());

                projectds.CurrentRow["SubjectSetCode"] = this.sltSubjectSet.Value;
                DAL.EntityDAO.ProjectDAO.UpdateProject(projectds);
                //this.lblSubjectSet.Text = BLL.SubjectRule.GetSubjectSetName(projectds.GetString("SubjectSetCode"));

                EntityData entity = null;
                DataRow dr = null;
                entity = DAL.EntityDAO.OBSDAO.GetStandard_UnitByCode(Request.QueryString["UnitCode"]);
                dr = entity.CurrentRow;

                if (this.parentUnit.Value == dr["UnitCode"].ToString())
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "不能将部门设置为自身的子部门 ！"));
                    entity.Dispose();
                    return;
                }
                EntityData parent = DAL.EntityDAO.OBSDAO.GetUnitByCode(this.parentUnit.Value);
                int parentDeep = parent.GetInt("Deep");
                dr["ParentUnitCode"] = this.parentUnit.Value;
                dr["Deep"] = parentDeep + 1;
                dr["FullCode"] = parent.GetString("FullCode") + "-" + dr["UnitCode"].ToString();

                DAL.EntityDAO.OBSDAO.SubmitAllStandard_Unit(entity);
                entity.Dispose();
                projectds.Dispose();

                Response.Write(Rms.Web.JavaScript.ScriptStart);
                if (this.txtRefreshScript.Value.Trim() != "")
                {
                    Response.Write("window.opener." + this.txtRefreshScript.Value.Trim());
                }
                else
                {
                    Response.Write(Rms.Web.JavaScript.OpenerReload(false));
                    Response.Write("window.opener.parent.frames['Left'].location.reload();");

                }

                Response.Write(Rms.Web.JavaScript.WinClose(false));
                Response.Write(Rms.Web.JavaScript.ScriptEnd);


            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "保存项目信息错误");
                Response.Write(Rms.Web.JavaScript.Alert(true, "保存项目信息错误"));
            }
        }
    }
}
