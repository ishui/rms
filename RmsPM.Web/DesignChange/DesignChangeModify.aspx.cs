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

namespace RmsPM.Web.DesignChange
{
	/// <summary>
	/// DesignChangeModify 的摘要说明。
	/// </summary>
	public partial class DesignChangeModify : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				PageOnit();
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

		#region 私有属性 -----------------------------------------------
		#endregion


		#region 共公属性 -----------------------------------------------
		/// <summary>
		/// 中标单位评审页面
		/// </summary>
		public string DesignAuditingUrl
		{
			get
			{
				return BLL.WorkFlowRule.GetProcedureURLByName("设计变更评审");
			}
		}
		#endregion


		#region 私有方法 -----------------------------------------------
		private void PageState()
		{
			if(DesignChangeControl1.State=="0")
			{
				btn_ChangeAuditing.Visible=true;
				Edit_ViewState();
			}
			else
			{
				Edit_ViewState();
				btn_Modify.Visible=false;
				btn_ChangeAuditing.Visible=false;
			}
		}
		private void Edit_ViewState()
		{
			if(DesignChangeControl1.IsEditMode)
			{
				btn_Modify.Visible=false;
				btn_Save.Visible=true;
				Bt_Cancel.Visible=true;
				btn_ChangeAuditing.Visible=false;
			}
			else
			{
				btn_Modify.Visible=true;
				btn_Save.Visible=false;
				Bt_Cancel.Visible=false;
				btn_ChangeAuditing.Visible=true;
			}
		}
		private void OutState()
		{
			string thisState=Request["State"]+"";
			if(thisState=="Edit")
			{
				this.DesignChangeControl1.IsEditMode=true;
			}
			else
			{
				this.DesignChangeControl1.IsEditMode=false;
			}
		}
		private void PageOnit()
		{
			OutState();			
			DesignChangeControl1.InitControl();
			PageState();
		}
		#endregion


		#region 公共方法 -----------------------------------------------
		#endregion


		#region 事件方法 -----------------------------------------------
		protected void btn_Save_ServerClick(object sender, System.EventArgs e)
		{
			DesignChangeControl1.SumitData();
			DesignChangeControl1.InitControl();
            DesignChangeControl1.State = "0";
			PageState();
		}

		protected void btn_Modify_ServerClick(object sender, System.EventArgs e)
		{
			DesignChangeControl1.IsEditMode=true;
			DesignChangeControl1.InitControl();
			PageState();
		}

		protected void Bt_Cancel_ServerClick(object sender, System.EventArgs e)
		{
			DesignChangeControl1.IsEditMode=false;
			DesignChangeControl1.InitControl();
			PageState();
		}
		#endregion

		
	}
}
