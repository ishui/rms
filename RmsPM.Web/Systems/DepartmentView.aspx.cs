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
using RmsPM.DAL;
using RmsPM.BLL;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// DepartmentView 的摘要说明。
	/// </summary>
	public partial class DepartmentView : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if ( !IsPostBack)
			{
				this.txtUnitCode.Value = Request["UnitCode"] + "";
				this.txtAction.Value = Request.QueryString["Action"] + "";
				this.txtCloseScript.Value = Request["CloseScript"] + "";

				string ButtonClose = Request["ButtonClose"] + "";
				if (ButtonClose == "0") 
				{
					this.trButton.Visible = false;
				}

				LoadData();
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

		private void LoadData()
		{
			string UnitCode = this.txtUnitCode.Value;

			if (UnitCode == "")
				return;

			try
			{
				EntityData entity = DAL.EntityDAO.OBSDAO.GetUnitByCode(UnitCode);
				if ( entity.HasRecord())
				{
					this.lblUnitName.Text = entity.GetString("UnitName");
					this.lblPrincipal.Text = entity.GetString("Principal");
					this.lblRemark.Text = entity.GetString("Remark");
				}
				entity.Dispose();
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"读取部门节点错误");
				Response.Write(Rms.Web.JavaScript.Alert(true, "读取部门节点错误"));
			}
		}

	}
}
