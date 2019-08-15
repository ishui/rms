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

using Rms.ORMap;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// Document 的摘要说明。
	/// </summary>
	public partial class Document : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnConfirmDocument;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!Page.IsPostBack) 
			{
				this.txtAct.Value = Request.QueryString["action"];
                this.txtGroupName.Value = Request.QueryString["GroupName"];
                this.txtGroupCode.Value = Request.QueryString["GroupCode"];
                this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

                //按类别名字取类别代码
                if ((this.txtGroupName.Value != "") && (this.txtGroupCode.Value == ""))
                {
                    string ProjectName = BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value);

                    EntityData entityGroup = DAL.EntityDAO.SystemManageDAO.GetSystemGroupByClassCode("1001");
                    DataRow[] drs = null;

                    if (ProjectName != "") //按项目＋类别名称过滤
                    {
                        //按项目名称取系统类别代码
                        string ProjectGroupCode = "";
                        DataRow[] drsProject = entityGroup.CurrentTable.Select("GroupName like '" + ProjectName + "%'");
                        if (drsProject.Length > 0)
                        {
                            ProjectGroupCode = drsProject[0]["GroupCode"].ToString();
                        }

                        if (ProjectGroupCode != "")  //按项目＋类别名称过滤
                        {
                            drs = entityGroup.CurrentTable.Select(string.Format("ParentCode='{0}' and GroupName='{1}'", ProjectGroupCode, this.txtGroupName.Value));
                        }
                    }
                    else  //按类别名称过滤
                    {
                        drs = entityGroup.CurrentTable.Select(string.Format("GroupName='{0}'", this.txtGroupName.Value));
                    }

                    if ((drs != null) && (drs.Length > 0))
                    {
                        string GroupCode = "";
                        foreach (DataRow dr in drs)
                        {
                            if (GroupCode != "") GroupCode += ",";
                            GroupCode += dr["GroupCode"].ToString();
                        }
                        this.txtGroupCode.Value = GroupCode;
                    }
                    entityGroup.Dispose();
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
