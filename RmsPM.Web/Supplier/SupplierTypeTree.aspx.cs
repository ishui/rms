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
	/// SupplierTypeTree ��ժҪ˵����
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
	}
}
