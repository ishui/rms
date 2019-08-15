namespace RmsPM.Web.DesignChange.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using RmsPM.BLL;

	/// <summary>
	///		DesignChangeControl 的摘要说明。
	/// </summary>
	public partial class DesignChangeControl : Components.ControlBase
	{
		protected AspWebControl.Calendar txtArrangedDate;
		protected Rms.ControlLb.TextBox_Lable TextBox_Lable2;
		protected Rms.ControlLb.TextBox_Lable TextBox_Lable3;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			AttachDefault();
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		private void ExchangeTypes1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}

		#region 私有属性 -----------------------------------------------
		BLL.Design_Message dm=new RmsPM.BLL.Design_Message();
		#endregion


		#region 共公属性 -----------------------------------------------
		#endregion


		#region 私有方法 -----------------------------------------------
		/// <summary>
		/// 附件初始化
		/// </summary>
		private void AttachDefault()
		{
			AttachMentAdd1.AttachMentType="设计变更";
			AttachMentAdd1.MasterCode=this.ApplicationCode;
			AttachMentAdd1.Visible=this.IsEditMode;
			this.AttachMentList1.AttachMentType="设计变更";
			AttachMentList1.MasterCode=ApplicationCode;
			this.AttachMentList1.Visible=!this.IsEditMode;
		}
		/// <summary>
		/// 保存数据
		/// </summary>
		private void SaveData()
		{
			IsVaile();
			dm.DesignCode=this.ApplicationCode;
			dm.DesignName=TB_DesignName.Text;

			dm.DesignID=TB_DesignCode.Text;

			dm.DesignReason=TB_DesignReason.Text;


			dm.DesignRemark=TB_DesignRemark.Text;
			dm.DesignLastTime=Calendar_LB1.Value;
			dm.DesignState="0";
			dm.ProjectCode=Request["ProjectCode"]+"";
			dm.DesignJionTime=System.DateTime.Now.ToShortDateString();
			dm.DesignSupplier=this.InputSupplier1.Value;
			dm.ProjectName=this.InputSupplier1.Text;
			dm.ContractID=this.SelectBox1.Value;
			dm.ContractName=this.SelectBox1.Text;
			dm.ContractCode=this.SelectBox1.Code;
			dm.dao=dao;
			dm.Design_MessageSubmit();
			this.IsEditMode=false;
            ViewState["ApplicationCode"] = dm.DesignCode;
			ControlState();
			
		}
		/// <summary>
		/// 验证
		/// </summary>
		protected void IsVaile()
		{
			if(InputSupplier1.Text=="选择合同")
			{
				throw new Exception("请选择合同");
			}
			//InputSearch1.Text
		}
		/// <summary>
		/// 加载数据
		/// </summary>
		override protected void LoadData()
		{
			TB_DesignName.IsEditMode=this.IsEditMode;
			if(this.ApplicationCode=="")
			{
				GetNewApplicationCode("DesignCode");
				return;
			}
			dm.dao=dao;			
			dm.DesignCode=this.ApplicationCode;
			TB_DesignName.Text=dm.DesignName;
			this.State=dm.DesignState;
            TB_DesignCode.Text = dm.DesignID;
			TB_DesignReason.Text=dm.DesignReason;
			TB_DesignRemark.Text=dm.DesignRemark;
			Calendar_LB1.Text=dm.DesignLastTime;
			SelectBox1.Code=dm.ContractID;
			SelectBox1.Text=dm.ContractName;
			SelectBox1.Value=dm.ContractCode;
			InputSupplier1.Value=dm.DesignSupplier;
			Lb_Supplier.InnerHtml=BLL.ProjectRule.GetSupplierName(dm.DesignSupplier);
			AttachDefault();
			ControlState();
		}
		/// <summary>
		/// 状态控制
		/// </summary>
		public void ControlState()
		{
			TB_DesignName.IsEditMode=this.IsEditMode;

			TB_DesignCode.IsEditMode=this.IsEditMode;

			TB_DesignReason.IsEditMode=this.IsEditMode;


			this.TB_DesignRemark.IsEditMode=this.IsEditMode;
			Calendar_LB1.IsEditMode=this.IsEditMode;
			Lb_Supplier.Visible=!IsEditMode;
			Lb_Contract.Visible=!IsEditMode;
			InputSupplier1.Visible=IsEditMode;
			SelectBox1.IsEditMode=this.IsEditMode;
		}
		private void UpdataState()
		{
			BLL.Design_MessageSystem.UpdateDesignState(this.ApplicationCode,this.State,dao);
		}
		#endregion


		#region 公共方法 -----------------------------------------------
		/// <summary>
		/// 控件初始化
		/// </summary>
		public void InitControl()
		{
			LoadData();
		}
		/// <summary>
		/// 保存信息
		/// </summary>
		override public void SumitData()
		{
			try
			{
				SaveData();
			}
			catch(Exception ex)
			{
				Response.Write(ex.Message);
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
		#endregion
	}
}
