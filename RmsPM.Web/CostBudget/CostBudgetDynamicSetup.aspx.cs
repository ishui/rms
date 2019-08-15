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
using Rms.Web;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.CostBudget
{
	/// <summary>
	/// CostBudgetDynamicSetup 的摘要说明。
	/// </summary>
	public partial class CostBudgetDynamicSetup : PageBase
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage() 
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				if (this.txtProjectCode.Value == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "未传入项目编号"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
				}

				decimal ValidHours = BLL.CostBudgetRule.GetOfflineValidHours(this.txtProjectCode.Value);
				this.txtValidHours.Value = ValidHours.ToString();

				if (ValidHours > 0)
				{
					this.rdoOfflineType1.Checked = true;
				}
				else
				{
					this.rdoOfflineType0.Checked = true;
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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

		/// <summary>
		/// 保存
		/// </summary>
		private void SavaData()
		{
			try
			{
				if (this.rdoOfflineType0.Checked) 
				{
					this.txtValidHours.Value = "";
				}

				BLL.SystemRule.UpdateProjectConfigValue(this.txtProjectCode.Value, BLL.SystemRule.m_ConfigCostBudgetOffineValidHours, BLL.ConvertRule.ToDecimal(this.txtValidHours.Value));

				if (this.rdoOfflineType0.Checked) //即时
				{
					//删除非即时版本
					BLL.CostBudgetRule.DeleteCostBudgetBackup(BLL.CostBudgetRule.GetOfflineBackupCode(this.txtProjectCode.Value));
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.rdoOfflineType1.Checked) 
			{
				if (this.txtValidHours.Value == "")
				{
					Hint = "请输入非即时状态下的有效期";
					return false;
				}

				if (this.txtValidHours.Value != "")
				{
					if (!Rms.Check.StringCheck.IsNumber(this.txtValidHours.Value))
					{
						Hint = "非即时状态下的有效期必须是数值";
						return false;
					}

					if (BLL.ConvertRule.ToDecimal(this.txtValidHours.Value) <= 0)
					{
						Hint = "非即时状态下的有效期必须大于0";
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);

			string ReturnFunc = "" + Request.QueryString["ReturnFunc"];
			if (ReturnFunc != "")
			{
				Response.Write(string.Format("window.opener.{0}();", ReturnFunc));
			}

//			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				SavaData();

				GoBack();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "保存失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

	}
}
