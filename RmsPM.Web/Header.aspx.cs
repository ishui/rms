using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Xml;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Rms.ORMap;

namespace RmsPM.Web.WorkPlan
{
	/// <summary>
	/// Header 的摘要说明。
	/// </summary>
	public partial class Header : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtUnitName;
		protected System.Web.UI.HtmlControls.HtmlInputButton Button1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
			}
		}

		private void IniPage()
		{
            string ConfigFileName = Server.MapPath("~/Configuration/SystemConfig.xml");
            SystemMenuManager menu = new SystemMenuManager(ConfigFileName);
            this.MenuDiv.InnerHtml = menu.SystemMenuHtml(this.user);

            #region -  delete by mi ------------------------------------------------------------
            /*
            this.txtUserName.Value = base.user.UserName;
			if (! user.HasRight("11"))
				this.tdSystem.InnerText="";

			if ( !user.m_IsGroupUser )
				this.tdGroup.InnerText="";
			
			//选择项目的方式（1=列表；否则=大图标）
			this.txtSelectProjectList.Value = System.Configuration.ConfigurationSettings.AppSettings["SelectProjectList"];

			DataView dv = new DataView( user.m_EntityDataAccessProject.CurrentTable ,"","ProjectName",DataViewRowState.CurrentRows);
			if(dv.Count == 0)
			{
				this.tdProject.Visible = false;
			}
			else
			{
				this.tdProject.Visible = true;
			}

			// 默认菜单
			if(user.m_IsGroupUser)
			{
				this.txtGroupUser.Value = "true";
			}
			else
			{
				if(dv.Count == 1)
				{
					string projectCode = dv.Table.Rows[0]["ProjectCode"].ToString();
					project.Reset( projectCode);
					Session["ProjectCode"] = projectCode;
					Session["Project"] = base.project;
					EntityData entity = DAL.EntityDAO.SystemManageDAO.GetSystemUserByCode( base.user.UserCode);
					entity.CurrentRow["LastProjectCode"] = projectCode;
					DAL.EntityDAO.SystemManageDAO.UpdateSystemUser(entity);
					entity.Dispose();
				}
			}
            */
            #endregion
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
        /// <summary>
        /// 退出系统,清楚Session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuit_Click(object sender, System.EventArgs e)
		{
			Session.Abandon();
			this.spanscript.InnerHtml = "<script language=\"javascript\">window.parent.navigate('Default.aspx');</script>";
		}

	}
}
