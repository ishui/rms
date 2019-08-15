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
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using RmsPM.Web;
using Rms.ORMap;
using Rms.WorkFlow;


namespace RmsPM.Web.WorkFlowContral
{
	/// <summary>
	/// 
	/// </summary>
	public partial class WorkFlowStart :PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden ProjectCode;

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
//			this.Contact.Visible = user.HasOperationRight("190101");//    工作联系单
			this.VehicleApply.Visible = user.HasOperationRight("190102");//用车申请
			//this.ComputerMaintenance.Visible = user.HasOperationRight("190103");//计算机维护
			//this.Equipment.Visible = user.HasOperationRight("190104");//计算机设备申请
//			this.SignFile.Visible = user.HasOperationRight("190105");//签报
//			this.archives.Visible = user.HasOperationRight("190106");//发文
//			this.OverTime.Visible = user.HasOperationRight("190107");//加班
//			this.leave.Visible = user.HasOperationRight("190108");//请假单
//			this.RetainExamine.Visible = user.HasOperationRight("190109");//员工试用期届满考核
//			this.StaffApply.Visible = user.HasOperationRight("190110");//人员需求
//			this.SceneVisa.Visible = user.HasOperationRight("190111");//现场签证单
//			this.cachet.Visible = user.HasOperationRight("190112");//公章(介绍信)申请
//			this.TechnologyCheck.Visible = user.HasOperationRight("190113");//技术核定单
//
//			this.DesignRework.Visible = user.HasOperationRight("190114");//设计修改补充通知单
//			this.MidPayCertificate.Visible = user.HasOperationRight("190115");//中期付款申请单
//			this.ConstructDrawingAuditFlow.Visible = user.HasOperationRight("190116");//施工图纸优化审批流转单
//			this.ConstructPlanAuditFlow.Visible = user.HasOperationRight("190117");//施工方案审批流转单
//			this.InvitBidAuditFlow.Visible = user.HasOperationRight("190118");//招投标审批流转单
//			this.OpinionReferAuditFlow.Visible = user.HasOperationRight("190119");//意见咨询审批流转单
//

			this.Purchase.Visible = user.HasOperationRight("190120");//物资采购申请
			this.ChequeDrow.Visible = user.HasOperationRight("190121");//支票领用（借款）单 
			this.Contract.Visible = user.HasOperationRight("190122");//合同申请审核
			
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
