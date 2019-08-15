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

namespace RmsPM.Web.Construct
{
	/// <summary>
	/// RiskTypeModify 的摘要说明。
	/// </summary>
	public partial class RiskTypeModify : PageBase
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
//				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtTypeCode.Value = Request.QueryString["TypeCode"];
				this.txtAct.Value = Request.QueryString["Action"];

				BLL.PageFacade.LoadRiskIndexSelect(this.sltRemindInexCode, "");

				if (this.txtTypeCode.Value == "") 
				{
					this.btnDelete.Visible = false;
				}
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
				string code = this.txtTypeCode.Value;
				if (code != "") 
				{
					EntityData entity = RmsPM.DAL.EntityDAO.ConstructDAO.GetRiskTypeByCode(code);
					if (entity.HasRecord())
					{
						this.txtTypeName.Value = entity.GetString("TypeName");
						this.sltRemindInexCode.Value = entity.GetString("RemindIndexCode");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "风险类型不存在"));
					}
					entity.Dispose();
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错：" + ex.Message));
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
		/// <param name="code"></param>
		/// <param name="parentCode"></param>
		private void SavaData()
		{
			try
			{				
				string TypeCode = this.txtTypeCode.Value;

				EntityData entity = DAL.EntityDAO.ConstructDAO.GetRiskTypeByCode(TypeCode);
				DataRow dr;
				bool isNew = !entity.HasRecord();


				if (isNew)
				{
					dr = entity.GetNewRecord();

					TypeCode = SystemManageDAO.GetNewSysCode("RiskTypeCode");
					this.txtTypeCode.Value = TypeCode;
					dr["TypeCode"] = TypeCode;
					entity.CurrentTable.Rows.Add(dr);
				}
				else
				{
					dr=entity.CurrentRow;
				}
				
				dr["TypeName"] = this.txtTypeName.Value;
				dr["RemindIndexCode"] = this.sltRemindInexCode.Value;
				
				DAL.EntityDAO.ConstructDAO.SubmitAllRiskType(entity);
					
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

			if (this.txtTypeName.Value.Trim() == "") 
			{
				Hint = "请输入风险类型";
				return false;
			}

			return true;
		}

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
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "保存失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack();
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
//			string FromUrl = this.txtFromUrl.Value.Trim();
//			if (FromUrl != "") 
//			{
//				Response.Write(string.Format("window.location = '{0}';", FromUrl));
//			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				BLL.ConstructRule.DeleteRiskType(this.txtTypeCode.Value);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "删除失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack();
		}

	}
}
