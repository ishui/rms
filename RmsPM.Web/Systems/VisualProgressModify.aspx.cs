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
	/// VisualProgressModify 的摘要说明。
	/// </summary>
	public partial class VisualProgressModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputText txtTypeName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtTypeCode;
		protected System.Web.UI.HtmlControls.HtmlSelect sltRemindInexCode;
	
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
				this.txtSystemID.Value = Request.QueryString["SystemID"];
				this.txtAct.Value = Request.QueryString["Action"];

				if (this.txtSystemID.Value == "") 
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
				string code = this.txtSystemID.Value;

				if (code != "") 
				{
					EntityData entity = RmsPM.DAL.EntityDAO.ConstructDAO.GetVisualProgressByCode(code);
					if (entity.HasRecord())
					{
						this.txtVisualProgress.Value = entity.GetString("VisualProgress");
						this.sltProgressType.Value = entity.GetInt("ProgressType").ToString();
						this.txtSortID.Value = entity.GetInt("SortID").ToString();
						this.txtInvestPercent.Value = BLL.MathRule.GetDecimalShowString(entity.GetDecimal("InvestPercent"));

						this.rdoIsPoint0.Checked = entity.GetInt("IsPoint")==0;
						this.rdoIsPoint1.Checked = entity.GetInt("IsPoint")==1;
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "形象进度不存在"));
					}
					entity.Dispose();
				}
				else 
				{
					//新增时的缺省值
					this.sltProgressType.Value = "0";
					this.rdoIsPoint0.Checked = true;

					//排序号
					int SortID = 0;

					EntityData entityA = DAL.EntityDAO.ConstructDAO.GetLastVisualProgress("");
					if (entityA.HasRecord()) 
					{
						SortID = entityA.GetInt("SortID");
					}
					entityA.Dispose();

					SortID++;
					this.txtSortID.Value = SortID.ToString();
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
				string code = this.txtSystemID.Value;

				EntityData entity = DAL.EntityDAO.ConstructDAO.GetVisualProgressByCode(code);
				DataRow dr;
				bool isNew = !entity.HasRecord();


				if (isNew)
				{
					dr = entity.GetNewRecord();

					code = SystemManageDAO.GetNewSysCode("VisualProgressSystemID");
					this.txtSystemID.Value = code;
					dr["SystemID"] = code;
					entity.CurrentTable.Rows.Add(dr);

				}
				else
				{
					dr = entity.CurrentRow;
				}
				
				dr["VisualProgress"] = this.txtVisualProgress.Value;
				dr["ProgressType"] = this.sltProgressType.Value;
				dr["SortID"] = BLL.ConvertRule.ToInt(this.txtSortID.Value);
				dr["InvestPercent"] = BLL.ConvertRule.ToDecimal(this.txtInvestPercent.Value);

				if (this.rdoIsPoint0.Checked) 
				{
					dr["IsPoint"] = 0;
				}
				else 
				{
					dr["IsPoint"] = 1;
				}
				
				DAL.EntityDAO.ConstructDAO.SubmitAllVisualProgress(entity);
					
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

			if (this.txtVisualProgress.Value.Trim() == "") 
			{
				Hint = "请输入形象进度";
				return false;
			}

			if (this.sltProgressType.Value.Trim() == "") 
			{
				Hint = "请选择进度类型";
				return false;
			}

			if ((this.txtSortID.Value != "") &&(! Rms.Check.StringCheck.IsInt(this.txtSortID.Value))) 
			{
				Hint = "排序号必须是整数";
				return false;
			}

			if (this.txtInvestPercent.Value != "")
			{
				if (! Rms.Check.StringCheck.IsNumber(this.txtInvestPercent.Value))
				{
					Hint = "投资比例必须是数值";
					return false;
				}

				decimal InvestPercent = BLL.ConvertRule.ToDecimal(this.txtInvestPercent.Value);
				if ((InvestPercent < 0) ||(InvestPercent > 100)) 
				{
					Hint = "投资比例必须在“0”到“100”范围内";
					return false;
				}
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
				BLL.ConstructRule.DeleteVisualProgress(this.txtSystemID.Value);
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
