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

namespace RmsPM.Web.Supplier
{
	/// <summary>
	/// SupplierTypeTree 的摘要说明。
	/// </summary>
	public partial class SupplierTypeTree : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtTreeType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtCheckBalance;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtShowItems;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//IniPage();
			try
			{
//				EntityData entity = DAL.EntityDAO.SystemManageDAO.GetSystemGroupByClassCode("1103");
//				if(entity.HasRecord())
//				{
//					this.dgType.DataSource = entity;
//					this.dgType.DataBind();
//				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void IniPage()
		{
//			string treeType = Request["treeType"] + "";
//			string checkBalance = Request["CheckBalance"] + "";
//			string showItems = "";
//			showItems = "SupplierTypeCode,ParentCode,TypeName,Description,FullCode,SortID";
//			
//			this.txtShowItems.Value = showItems;
//			this.txtTreeType.Value = treeType;
//			this.txtCheckBalance.Value = checkBalance;
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
