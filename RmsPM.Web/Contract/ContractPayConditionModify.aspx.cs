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
using RmsPM.DAL;
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Contract
{
	/// <summary>
	/// ContractPayConditionModify 的摘要说明。
	/// </summary>
	public partial class ContractPayConditionModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtProjectCode;
		
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.spanTaskName.InnerText = this.txtTaskName.Value;

			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtAllocateCode.Value = Request.QueryString["AllocateCode"];
				this.txtContractCode.Value = Request.QueryString["ContractCode"];
				this.txtConditionCode.Value = Request.QueryString["ConditionCode"];

				if ((this.txtAllocateCode.Value == "") || (this.txtContractCode.Value == ""))
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "未传入款项编号或合同编号"));
					return;
				}
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化页面出错");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadData()
		{
			try 
			{
				string ConditionCode = this.txtConditionCode.Value;

				if (ConditionCode != "") 
				{
					EntityData entity = (EntityData)Session["ContractEntity"];
					DataTable dt = entity.Tables["ContractPayCondition"];

					DataRow dr = dt.Select("ConditionCode='" + ConditionCode + "'")[0];

					this.txtWBSCode.Value = BLL.ConvertRule.ToString(dr["WBSCode"]);
					this.txtTaskName.Value = BLL.ConvertRule.ToString(dr["TaskName"]);
					this.spanTaskName.InnerText = this.txtTaskName.Value;
					this.txtCompletePercent.Value = BLL.ConvertRule.ToInt(dr["CompletePercent"]).ToString();
					this.sltDelayType.Value = BLL.ConvertRule.ToInt(dr["DelayType"]).ToString();
					this.txtDelayDays.Value = BLL.ConvertRule.ToInt(dr["DelayDays"]).ToString();
				}
				else 
				{
					this.btnDelete.Style["display"] = "none";

					//新增缺省值
					this.txtCompletePercent.Value = "100";
				}
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化页面出错");
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
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.txtWBSCode.Value == "")
			{
				Hint = "请选择工作项";
				return false;
			}

			if ( this.txtCompletePercent.Value != "" )
			{
				if ( ! Rms.Check.StringCheck.IsInt(txtCompletePercent.Value))
				{
					Hint = "完成百分比必须是整数 ！ ";
					return false;
				}

				int CompletePercent = BLL.ConvertRule.ToInt(this.txtCompletePercent.Value);
				if ((CompletePercent < 0) || (CompletePercent > 100))
				{
					Hint = "完成百分比必须位于 0 到 100 之间 ！ ";
					return false;
				}
			}

			if (this.sltDelayType.Value != "0") 
			{
				if (this.txtDelayDays.Value == "")
				{
					Hint = "请输入提前或延后的天数";
					return false;
				}

				if ( this.txtDelayDays.Value != "" )
				{
					if ( ! Rms.Check.StringCheck.IsInt(txtDelayDays.Value))
					{
						Hint = "提前或延后的天数必须是整数 ！ ";
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// 保存合同相关工作
		/// </summary>
		private void SaveTaskContract() 
		{
			try 
			{
				string contractCode = this.txtContractCode.Value;

				EntityData entity = (EntityData)Session["ContractEntity"];
				entity.SetCurrentTable("TaskContract");

				DataTable dtCondition = entity.Tables["ContractPayCondition"];

//				int icount = entity.CurrentTable.Rows.Count;
//				for(int i=icount-1;i>=0;i--)
//				{
//					DataRow dr = entity.CurrentTable.Rows[i];
//
//					string WBSCode = BLL.ConvertRule.ToString(dr["WBSCode"]);
//					if (dtCondition.Select("WBSCode='" + WBSCode + "'").Length == 0) 
//					{
//						dr.Delete();
//					}
//				}

				DataView dv = new DataView(dtCondition, "", "", DataViewRowState.CurrentRows);

				foreach(DataRowView drCondition in dv) 
				{
					string WBSCode = BLL.ConvertRule.ToString(drCondition["WBSCode"]);

					if (entity.CurrentTable.Select("WBSCode='" + WBSCode + "'").Length == 0) 
					{
						DataRow drNew = entity.CurrentTable.NewRow();

						drNew["TaskContractCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("TaskContractCode");
						drNew["ContractCode"] = contractCode;
						drNew["WBSCode"] = WBSCode;

						drNew["TaskName"] = BLL.WBSRule.GetWBSName(WBSCode);

						DataTable tbGroup = BLL.WBSRule.GetTaskPersonNameGroupByType(WBSCode);
						drNew["UserNames"] = BLL.WBSRule.GetTaskPersonNameMaster(tbGroup);

						entity.CurrentTable.Rows.Add(drNew);
					}
				}

				Session["ContractEntity"]=entity;
			}
			catch( Exception ex )
			{
				throw ex;
			}
		}

		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			try 
			{
				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				EntityData entity = (EntityData)Session["ContractEntity"];
				DataTable dt = entity.Tables["ContractPayCondition"];

				string ContractCode = this.txtContractCode.Value;
				string AllocateCode = this.txtAllocateCode.Value;
				string ConditionCode = this.txtConditionCode.Value;

				DataRow dr;

				if (ConditionCode == "") 
				{
					dr = dt.NewRow();
					dr["ConditionCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractPayConditionCode");
					dr["AllocateCode"] = AllocateCode;
					dr["ContractCode"] = ContractCode;

					dt.Rows.Add(dr);
				}
				else 
				{
					dr = dt.Select("ConditionCode='" + ConditionCode + "'")[0];
				}

				dr["WBSCode"] = this.txtWBSCode.Value;
				dr["TaskName"] = this.txtTaskName.Value;
				dr["CompletePercent"] = BLL.ConvertRule.ToInt(this.txtCompletePercent.Value);
				dr["DelayType"] = this.sltDelayType.Value;

				if (BLL.ConvertRule.ToInt(dr["DelayType"]) == 0) 
				{
					dr["DelayDays"] = DBNull.Value;
					dr["DelayTypeName"] = "";
				}
				else 
				{
					dr["DelayDays"] = BLL.ConvertRule.ToInt(this.txtDelayDays.Value);
					dr["DelayTypeName"] = this.sltDelayType.Items[this.sltDelayType.SelectedIndex].Text;
				}

				//更新款项的付款条件显示
				DataTable dtAllo = entity.Tables["ContractAllocation"];
				DataRow drAllo = dtAllo.Select("AllocateCode='" + AllocateCode + "'")[0];
				drAllo["PayConditionHtml"] = BLL.ContractRule.GetContractPayConditionHtml(AllocateCode, dt, true);

				//计算付款时间
				string sPayDate = "";
				object PayDate = BLL.ContractRule.CalcContractPayDateByTask(AllocateCode, dt);
				if (PayDate != null) 
				{
					sPayDate = ((DateTime)PayDate).ToString("yyyy-MM-dd");
//					drAllo["PlanningPayDate"] = PayDate;
				}

				SaveTaskContract();

				GoBack(sPayDate);
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"保存出错");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
				return;
			}

		}

		private void GoBack(string sPayDate)
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write( string.Format("window.opener.PayConditionReturn('{0}');", sPayDate));
			Response.Write(Rms.Web.JavaScript.WinClose(false));
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
				EntityData entity = (EntityData)Session["ContractEntity"];
				DataTable dt = entity.Tables["ContractPayCondition"];

				string ContractCode = this.txtContractCode.Value;
				string AllocateCode = this.txtAllocateCode.Value;
				string ConditionCode = this.txtConditionCode.Value;

				DataRow[] drs = dt.Select("ConditionCode='" + ConditionCode + "'");
				if (drs.Length > 0) 
				{
					drs[0].Delete();
				}

				//更新款项的付款条件显示
				DataTable dtAllo = entity.Tables["ContractAllocation"];
				DataRow drAllo = dtAllo.Select("AllocateCode='" + AllocateCode + "'")[0];
				drAllo["PayConditionHtml"] = BLL.ContractRule.GetContractPayConditionHtml(AllocateCode, dt, true);

			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"删除出错");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错：" + ex.Message));
				return;
			}

			GoBack("");
		}

	}
}
