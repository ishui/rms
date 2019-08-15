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
using RmsPM.DAL;
using RmsPM.BLL;
using Rms.ORMap;
using Rms.Web;

namespace RmsPM.Web.SelectBox
{
	/// <summary>
	/// SelectProjectCanAccess 的摘要说明。
	/// </summary>
	public partial class SelectProjectCanAccess : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton Button2;
		protected System.Web.UI.HtmlControls.HtmlInputText txtKGYear;
		protected System.Web.UI.HtmlControls.HtmlSelect SelectStatus;
		protected System.Web.UI.HtmlControls.HtmlInputText txtJGYear;
		protected System.Web.UI.HtmlControls.HtmlInputText txtProjectName;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadDataGrid();
			}
		}

		private void IniPage() 
		{

		}

		private void LoadDataGrid()
		{
			try
			{
				try
				{
					this.dlProject.DataSource = new DataView( user.m_EntityDataAccessProject.CurrentTable ,"","ProjectName",DataViewRowState.CurrentRows) ;
					this.dlProject.DataBind();
				}
				catch( Exception ex )
				{
					ApplicationLog.WriteLog(this.ToString(),ex,"加载公司列表错误。");
				}

			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载项目列表错误。");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载项目列表错误。"));
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
