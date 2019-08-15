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
	/// ContractPayConditionModify ��ժҪ˵����
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
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ��������Ż��ͬ���"));
					return;
				}
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ��ҳ�����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
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

					//����ȱʡֵ
					this.txtCompletePercent.Value = "100";
				}
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ��ҳ�����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
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

		/// <summary>
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.txtWBSCode.Value == "")
			{
				Hint = "��ѡ������";
				return false;
			}

			if ( this.txtCompletePercent.Value != "" )
			{
				if ( ! Rms.Check.StringCheck.IsInt(txtCompletePercent.Value))
				{
					Hint = "��ɰٷֱȱ��������� �� ";
					return false;
				}

				int CompletePercent = BLL.ConvertRule.ToInt(this.txtCompletePercent.Value);
				if ((CompletePercent < 0) || (CompletePercent > 100))
				{
					Hint = "��ɰٷֱȱ���λ�� 0 �� 100 ֮�� �� ";
					return false;
				}
			}

			if (this.sltDelayType.Value != "0") 
			{
				if (this.txtDelayDays.Value == "")
				{
					Hint = "��������ǰ���Ӻ������";
					return false;
				}

				if ( this.txtDelayDays.Value != "" )
				{
					if ( ! Rms.Check.StringCheck.IsInt(txtDelayDays.Value))
					{
						Hint = "��ǰ���Ӻ���������������� �� ";
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// �����ͬ��ع���
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
		/// ����
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

				//���¿���ĸ���������ʾ
				DataTable dtAllo = entity.Tables["ContractAllocation"];
				DataRow drAllo = dtAllo.Select("AllocateCode='" + AllocateCode + "'")[0];
				drAllo["PayConditionHtml"] = BLL.ContractRule.GetContractPayConditionHtml(AllocateCode, dt, true);

				//���㸶��ʱ��
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
				ApplicationLog.WriteLog(this.ToString(),ex,"�������");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
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
		/// ɾ��
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

				//���¿���ĸ���������ʾ
				DataTable dtAllo = entity.Tables["ContractAllocation"];
				DataRow drAllo = dtAllo.Select("AllocateCode='" + AllocateCode + "'")[0];
				drAllo["PayConditionHtml"] = BLL.ContractRule.GetContractPayConditionHtml(AllocateCode, dt, true);

			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"ɾ������");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������" + ex.Message));
				return;
			}

			GoBack("");
		}

	}
}
