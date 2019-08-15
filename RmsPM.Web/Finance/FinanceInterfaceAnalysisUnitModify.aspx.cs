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

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// FinanceInterfaceAnalysisUnitModify 的摘要说明。
	/// </summary>
	public partial class FinanceInterfaceAnalysisUnitModify : PageBase
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
				this.txtUnitCode.Value = Request.QueryString["UnitCode"];
				this.txtSubjectSetCode.Value = Request["SubjectSetCode"];
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
				if ((this.txtUnitCode.Value == "") || (this.txtSubjectSetCode.Value == ""))
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "未传入部门编号或帐套编号"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}

				this.lblUnitName.Text = BLL.SystemRule.GetUnitFullName(this.txtUnitCode.Value);
				this.lblSubjectSetName.Text = BLL.SubjectRule.GetSubjectSetName(this.txtSubjectSetCode.Value);

				EntityData entity = DAL.EntityDAO.OBSDAO.GetUnitSubjectSetByUnit(this.txtUnitCode.Value, this.txtSubjectSetCode.Value);
				if (entity.HasRecord()) 
				{
					this.txtU8Code.Value = entity.GetString("U8Code");
				}
				entity.Dispose();
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
				EntityData entity = DAL.EntityDAO.OBSDAO.GetUnitSubjectSetByUnit(this.txtUnitCode.Value, this.txtSubjectSetCode.Value);
				DataRow dr = null;
				if (!entity.HasRecord())
				{
					dr = entity.CurrentTable.NewRow();
					dr["UnitSubjectSetCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("UnitSubjectSetCode");
					dr["UnitCode"] = this.txtUnitCode.Value;
					dr["SubjectSetCode"] = this.txtSubjectSetCode.Value;
					entity.CurrentTable.Rows.Add(dr);
				}
				else
				{
					dr = entity.CurrentRow;
				}

				dr["U8Code"] = this.txtU8Code.Value;

				DAL.EntityDAO.OBSDAO.SubmitAllUnitSubjectSet(entity);

				entity.Dispose();

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

			return true;
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
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
