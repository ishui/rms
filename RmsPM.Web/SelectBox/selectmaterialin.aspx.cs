using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
using Rms.Web;
using RmsPM.DAL;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.SelectBox
{
    /// <summary>
    /// SelectPerson 的摘要说明。
    /// </summary>
    public partial class selectmaterialin : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //返回函数名
                string ReturnFunc = Request.QueryString["ReturnFunc"] + "";
                if (ReturnFunc == "")
                {
                    ReturnFunc = "DoSelectMaterialin";
                }
                ViewState["ReturnFunc"] = ReturnFunc;

                string ProjectCode = Request["ProjectCode"] + "";
                if (ProjectCode != "")
                {
                    this.txtRootUnitCode.Value = "-1";
                    this.txtProjectCode.Value = ProjectCode;

                    EntityData entity = DAL.EntityDAO.ProjectDAO.GetProjectByCode(ProjectCode);
                    if (entity.CurrentTable.Rows.Count > 0)
                    {
                        this.txtRootUnitCode.Value = entity.GetString("UnitCode");
                    }
                    entity.Dispose();
                }
            }

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
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}
