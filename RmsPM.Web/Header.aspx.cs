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
	/// Header ��ժҪ˵����
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
			
			//ѡ����Ŀ�ķ�ʽ��1=�б�����=��ͼ�꣩
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

			// Ĭ�ϲ˵�
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
        /// <summary>
        /// �˳�ϵͳ,���Session
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
