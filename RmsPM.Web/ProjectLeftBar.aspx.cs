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
using System.Xml;

using Rms.ORMap;

namespace RmsPM.Web
{
	/// <summary>
	/// ProjectLeftBar ��ժҪ˵����
	/// </summary>
	public partial class ProjectLeftBar : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				this.DropDownProject.DataSource = new DataView( user.m_EntityDataAccessProject.CurrentTable ,"","ProjectName",DataViewRowState.CurrentRows) ;
				this.DropDownProject.DataTextField = "ProjectShortName";
				this.DropDownProject.DataValueField = "ProjectCode";
				this.DropDownProject.DataBind();

				string LastProjectCode = BLL.SystemRule.GetLastProjectCode(user.UserCode);
				if(LastProjectCode != "")
				{
					this.DropDownProject.SelectedIndex = this.DropDownProject.Items.IndexOf(this.DropDownProject.Items.FindByValue(LastProjectCode));
				}

				SelectedProject();
			}
	
			//this.divProjectName.InnerHtml = BLL.ProjectRule.GetProjectShortName(projectCode, true);
		}

		private void SelectedProject()
		{
			string projectCode = this.DropDownProject.SelectedItem.Value;

			project.Reset( projectCode);
			Session["ProjectCode"] = projectCode;
			Session["Project"] = base.project;

			EntityData entity = DAL.EntityDAO.SystemManageDAO.GetSystemUserByCode( base.user.UserCode);
			entity.CurrentRow["LastProjectCode"] = projectCode;
			DAL.EntityDAO.SystemManageDAO.UpdateSystemUser(entity);
			entity.Dispose();

			XMLTreeViewManager vm = new XMLTreeViewManager( Server.MapPath(System.Configuration.ConfigurationSettings.AppSettings["VirtualDirectory"]) + @"\ProjectLeftBar.xml");
			string s = vm.GetLeftBarString( base.user , projectCode ,"" );
			this.tdBar.InnerHtml = s;
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void DropDownProject_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SelectedProject();
            Response.Write(string.Format("<script>window.parent.document.all.main.src='ProjectChangeMes.aspx?ProjectName={0}';</script>", DropDownProject.SelectedItem.Text));
		}
	}
}
