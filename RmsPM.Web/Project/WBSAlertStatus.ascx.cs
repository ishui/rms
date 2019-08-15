namespace RmsPM.Web.Project
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using RmsPM.DAL.EntityDAO;
	using Rms.ORMap;
	using Rms.Web;

	/// <summary>
	/// <description>
	/// 	WBSAlertStatus 的摘要说明。
	///		主要单独封装对任务状态改变的事件和方法.
	///		页面显示时以button事件处理，同时提供状态改变的方法
	/// </description>
	///	<author>unm</author>
	///	<date>2004/11/8</date>
	///	<version>1.0</version>
	///	<modify>
	///		<description></description>
	///		<author></author>	
	///		<date></date>
	///		<version></version>
	///	</modify>
	/// </summary>
	public partial class WBSAlertStatus : System.Web.UI.UserControl
	{
		protected string strStatus = "";
		protected string strTaskCode = "";
		private string strUserType = "";
		/// <summary>
		/// 工作编码
		/// </summary>
		public string TaskCode
		{
			set
			{
				this.strTaskCode = value;
			}
			get
			{
				return this.strTaskCode;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				// 当前用户类型，责任人，监督人，参与人等
				this.strUserType = (string)ViewState["UserType"];
				// 在此处放置用户代码以初始化页面
				EntityData entity = WBSDAO.GetV_TaskByCode(this.strTaskCode);
				if(entity.HasRecord())
					this.strStatus = entity.GetInt("Status").ToString();
				entity.Dispose();
				this.CheckStatus();

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"状态改变失败");
			}
		}

		private void CheckStatus()
		{	
			this.btStart.Visible = false;
			this.btPause.Visible = false;
			this.btCancel.Visible = false;
			this.btFinish.Visible = false;
			switch(this.strStatus)
			{
				// 现在有的操作：开始，暂停，取消，完成
				case "0": // 未开始
					this.btStart.Visible = true;
					this.btCancel.Visible = true;
					this.btFinish.Visible = true;
					break;
				case "1": // 进行中
					this.btPause.Visible = true;
					this.btCancel.Visible = true;
					this.btFinish.Visible = true;
					break;
				case "2": // 暂停
					this.btStart.Value = "继续工作";
					this.btStart.Visible = true;
					this.btCancel.Visible = true;
					this.btFinish.Visible = true;
					break;
				case "3": // 取消
//					this.btStart.Value = "继续工作";
//					this.btStart.Visible = true;
					break;
				case "4": // 已完成

					break;
			}
			// 只有责任人可以暂停任务,取消，完成任务
//			if(this.strUserType!="2")
//			{
//				this.btPause.Visible = false;
//				this.btCancel.Visible = false;
//				this.btFinish.Visible = false;
//			}
		}

		/// <summary>
		/// 状态操作改动到WBSStatus中
		/// </summary>

		private void SetStatus(string strValue)
		{
		}

	
		public void SetStart()
		{
			this.SetStatus("1");
		}

		public void SetPause()
		{
			this.SetStatus("2");
		}

		public void SetCancel()
		{
			this.SetStatus("3");
		}

		public void SetFinish()
		{
			this.SetStatus("4");
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

		private void JSAction()
		{
			// 父节点需要再次载入数据
			Response.Write(JavaScript.ScriptStart);
			Response.Write("window.location.href = '"+Request.Url.PathAndQuery+"';");
			Response.Write(JavaScript.ScriptEnd);
		}

		protected void btStart_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				this.SetStart();
				this.JSAction();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"状态改变失败");
			}
		}

		protected void btPause_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				this.SetPause();
				this.JSAction();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"状态改变失败");
			}
		}

		protected void btCancel_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				this.SetCancel();
				this.JSAction();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"状态改变失败");
			}
		}

		protected void btFinish_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				this.SetFinish();
				this.JSAction();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"状态改变失败");
			}
		}
	}
}
