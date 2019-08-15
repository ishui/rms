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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;
using RmsPM.BLL;

namespace RmsPM.Web.Construct
{
	/// <summary>
	/// AnnualPlanModify ��ժҪ˵����
	/// </summary>
	public partial class AnnualPlanModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlTableRow trCurrentFloor;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				IniPage();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtAnnualPlanCode.Value = Request.QueryString["AnnualPlanCode"];
				this.txtAct.Value = Request.QueryString["action"];
				this.txtPBSUnitCode.Value = Request.QueryString["PBSUnitCode"];
				this.txtIYear.Value = Request.QueryString["IYear"];
				int year;

				PageFacade.LoadVisualProgressSelect(sltVisualProgress,"");

				switch (this.txtAct.Value.ToLower()) 
				{
					case "insert":  //����
						LoadPBSUnit();

						//����ʱ�������µ���ȼƻ�����
						EntityData entity = DAL.EntityDAO.ConstructDAO.GetCurrConstructAnnualPlanByPBSUnit(this.txtPBSUnitCode.Value);
						if (entity.HasRecord()) 
						{
							DataRow dr = entity.CurrentRow;
							year = entity.GetInt("IYear") + 1;

							//���������ת���
							this.txtLCFArea.Value = BLL.MathRule.GetDecimalShowString(dr["LCFArea"]);

//							this.txtInvestBefore.Value = BLL.MathRule.GetDecimalShowString(dr["InvestBefore"]);

							this.txtPInvest.Value = BLL.MathRule.GetDecimalShowString(dr["PInvest"]);
							this.sltVisualProgress.Value = entity.GetString("VisualProgress");

							LoadDataGrid(entity.GetInt("IYear"), true);
						}
						else 
						{
							//����ȼƻ�ʱ�����Ϊ��ǰ��� 
							year = DateTime.Today.Year;
							LoadDataGrid(year, false);
						}
						entity.Dispose();

						//����λ���̵ĵ�ǰ������ȣ���������ĩ���Ͷ��
						decimal InvestBefore = BLL.ConstructRule.CalcPBSUnitCompleteInvest(txtPBSUnitCode.Value);
						this.txtInvestBefore.Value = BLL.MathRule.GetDecimalShowString(InvestBefore);

						this.txtIYear.Value = year.ToString();
						this.lblYear.Text  = this.txtIYear.Value;

						break;

					case "modify":  //�޸�
						LoadData();
						LoadPBSUnit();
						LoadDataGrid(int.Parse(this.txtIYear.Value), false);
						break;

					case "delete":  //ɾ��
						DoDelete();
						break;

					default:
						Response.Write(Rms.Web.JavaScript.Alert(true, "δ����Ĳ�������"));
						Response.End();
						break;
				}

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		/// <summary>
		/// ɾ��
		/// </summary>
		private void DoDelete()
		{
			try
			{
				string code = this.txtAnnualPlanCode.Value;
				BLL.ConstructRule.DeleteConstructAnnualPlan(code);

				GoBack();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"ɾ����ȼƻ�����");
			}
		}

		private void LoadPBSUnit()
		{
			string code =  this.txtPBSUnitCode.Value;

			try
			{
				if (code != "") 
				{
					EntityData entity = DAL.EntityDAO.PBSDAO.GetPBSUnitByCode(code);

					if ( entity.HasRecord())
					{
						this.txtPBSUnitCode.Value = entity.GetString("PBSUnitCode");
						this.txtProjectCode.Value = entity.GetString("ProjectCode");
					}
					else
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "��λ���̲�����"));
						return;
					}

					entity.Dispose();
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "��λ���̱�Ų���Ϊ��"));
					return;
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"���ص�λ����ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "���ص�λ����ʧ�ܣ�" + ex.Message));
			}
		}

		private void LoadData()
		{
			string PBSUnitCode =  this.txtPBSUnitCode.Value;
			int IYear = BLL.ConvertRule.ToInt(this.txtIYear.Value);

			this.lblYear.Text  = this.txtIYear.Value;

			try
			{
				if ((PBSUnitCode != "") && (IYear != 0))
				{
					EntityData entity = DAL.EntityDAO.ConstructDAO.GetConstructAnnualPlanByPBSUnitYear(PBSUnitCode, IYear);

					if ( entity.HasRecord())
					{
						DataRow dr = entity.CurrentRow;

						this.txtAnnualPlanCode.Value = entity.GetString("AnnualPlanCode");
						this.txtPBSUnitCode.Value = entity.GetString("PBSUnitCode");
						this.txtProjectCode.Value = entity.GetString("ProjectCode");
						this.txtIYear.Value = entity.GetInt("IYear").ToString();
						this.lblYear.Text  = this.txtIYear.Value;

						this.txtLCFArea.Value = BLL.MathRule.GetDecimalShowString(dr["LCFArea"]);
						this.txtPInvest.Value = BLL.MathRule.GetDecimalShowString(dr["PInvest"]);
						this.txtInvestBefore.Value = BLL.MathRule.GetDecimalShowString(dr["InvestBefore"]);
						this.sltVisualProgress.Value = entity.GetString("VisualProgress").ToString();

						this.txtCurrentFloor.Value = BLL.MathRule.GetIntShowString(dr["CurrentFloor"]);
					}

					entity.Dispose();
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "��λ���̱�ź���Ȳ���Ϊ��"));
					return;
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"������ȼƻ�ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "������ȼƻ�ʧ�ܣ�" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾ�ƻ���ϸ
		/// </summary>
		private void LoadDataGrid(int year, bool isCopy)
		{
			string PBSUnitCode =  this.txtPBSUnitCode.Value;

			try
			{
				DataTable tb = BLL.ConstructRule.GenerateConstructPlanTable(PBSUnitCode, year, isCopy);

				this.dgList.DataSource = tb;
				this.dgList.DataBind();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"������ȼƻ�ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "������ȼƻ�ʧ�ܣ�" + ex.Message));
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
			this.dgList.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemCreated);

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

			if(sltVisualProgress.Value=="")
			{
				Hint = "��ѡ��ƻ�������� �� ";
				return false;
			}

			if ( txtInvestBefore.Value != "" )
			{
				if ( ! Rms.Check.StringCheck.IsNumber(txtInvestBefore.Value))
				{
					Hint = "����ĩ���Ͷ�ʱ�������ֵ �� ";
					return false;
				}
			}

			if ( txtLCFArea.Value != "" )
			{
				if ( ! Rms.Check.StringCheck.IsNumber(txtLCFArea.Value))
				{
					Hint = "�����ת�����������ֵ �� ";
					return false;
				}
			}

			if ( txtPInvest.Value != "" )
			{
				if ( ! Rms.Check.StringCheck.IsNumber(txtPInvest.Value))
				{
					Hint = "����ƻ�Ͷ�ʱ�������ֵ �� ";
					return false;
				}
			}

//			if (sltVisualProgress.Value == "�ṹ")
//			{
//				if (this.txtCurrentFloor.Value == "") 
//				{
//					Hint = "������ƻ�ʩ������ �� ";
//					return false;
//				}

			if ( txtCurrentFloor.Value != "" )
			{
				if ( ! Rms.Check.StringCheck.IsInt(txtCurrentFloor.Value))
				{
					Hint = "�ƻ�ʩ���������������� �� ";
					return false;
				}
			}

			string tempHint = BLL.PBSRule.CheckPBSUnitFloorCount(this.txtPBSUnitCode.Value, txtCurrentFloor.Value, "�ƻ�ʩ������");
			if (tempHint != "") 
			{
				Hint = tempHint;
				return false;
			}

//			}

			return true;
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
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

				string PBSUnitCode = this.txtPBSUnitCode.Value;
				string code = this.txtAnnualPlanCode.Value;
				EntityData entity = DAL.EntityDAO.ConstructDAO.GetConstructAnnualPlanByCode(code);
				bool isNew = ( !entity.HasRecord() );
				
				DataRow dr = null;
				if ( isNew )
				{
					dr = entity.GetNewRecord();
					code = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("AnnualPlanCode");
					dr["AnnualPlanCode"] = code;
					dr["PBSUnitCode"] = PBSUnitCode;
					dr["ProjectCode"] = this.txtProjectCode.Value;
					dr["IYear"] = this.txtIYear.Value;
				}
				else
				{
					dr = entity.CurrentRow; 
				}

				dr["PlanDate"] = DateTime.Now;
				dr["PlanPerson"] = base.user.UserCode;

				string VisualProgress = sltVisualProgress.Value;
				dr["VisualProgress"] = VisualProgress;

				int CurrentFloor = BLL.ConvertRule.ToInt(this.txtCurrentFloor.Value);
				dr["CurrentFloor"] = CurrentFloor;

				decimal InvestBefore = BLL.ConvertRule.ToDecimal(this.txtInvestBefore.Value);
				dr["InvestBefore"] = InvestBefore;

				dr["LCFArea"] = BLL.ConvertRule.ToDecimalObj(this.txtLCFArea.Value);

				//����ƻ�Ͷ���Զ�����
				EntityData entityU = DAL.EntityDAO.PBSDAO.GetV_PBSUnitByCode(PBSUnitCode);
				if (entityU.HasRecord()) 
				{
					decimal TotalInvest = entityU.GetDecimal("PInvest");
					int TotalFloorCount = BLL.PBSRule.GetPBSUnitFloorCount(PBSUnitCode);

					//����ĩ��Ͷ��
					decimal CurrTotalInvest = BLL.ConstructRule.CalcInvestByVisualProgress(TotalInvest, VisualProgress, TotalFloorCount, CurrentFloor);

					//����Ͷ�� = ����ĩ��Ͷ�� - ����ĩͶ��
					decimal CurrYearInvest = CurrTotalInvest - InvestBefore;
					dr["PInvest"] = CurrYearInvest;
				}
				entityU.Dispose();

//				dr["PInvest"] = BLL.ConvertRule.ToDecimalObj(this.txtPInvest.Value);

				if ( isNew )
				{
					entity.AddNewRecord(dr);
					DAL.EntityDAO.ConstructDAO.InsertConstructAnnualPlan(entity);
				}
				else
				{
					DAL.EntityDAO.ConstructDAO.UpdateConstructAnnualPlan(entity);
				}

				entity.Dispose();

				SaveDtl();

				GoBack();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
			}
		}

		/// <summary>
		/// ����ƻ���ϸ
		/// </summary>
		private void SaveDtl() 
		{
			try 
			{
				string PBSUnitCode = this.txtPBSUnitCode.Value;
				int year = int.Parse(this.txtIYear.Value);

				int iCount = this.dgList.Items.Count;
				for(int i=0;i<iCount;i++) 
				{
					string code = this.dgList.DataKeys[i].ToString();
					HtmlInputHidden txtVisualProgress = (HtmlInputHidden)this.dgList.Items[i].FindControl("txtVisualProgress");
					HtmlInputHidden txtVisualProgressName = (HtmlInputHidden)this.dgList.Items[i].FindControl("txtVisualProgressName");
					HtmlInputHidden txtProgressType = (HtmlInputHidden)this.dgList.Items[i].FindControl("txtProgressType");
					HtmlInputHidden txtIsPoint = (HtmlInputHidden)this.dgList.Items[i].FindControl("txtIsPoint");
					AspWebControl.Calendar txtStartDate = (AspWebControl.Calendar)this.dgList.Items[i].FindControl("txtStartDate");
					AspWebControl.Calendar txtEndDate = (AspWebControl.Calendar)this.dgList.Items[i].FindControl("txtEndDate");

					EntityData entity = DAL.EntityDAO.ConstructDAO.GetConstructPlanStepByCode(code);
					DataRow dr;
					bool isNew = !entity.HasRecord();

					if (isNew) 
					{
						dr = entity.CurrentTable.NewRow();
						dr["ConstructPlanStepCode"] = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ConstructPlanStepCode");
					}
					else 
					{
						dr = entity.CurrentRow;
					}

					dr["PBSUnitCode"] = PBSUnitCode;
					dr["IYear"] = year;
					dr["VisualProgress"] = txtVisualProgress.Value;
					dr["StartDate"] = BLL.ConvertRule.ToDate(txtStartDate.Value);

					//ֻ�ʼ����ʱ�����������Զ��ʼ����
					if (txtIsPoint.Value == "1") 
					{
						dr["EndDate"] = dr["StartDate"];
					}
					else 
					{
						dr["EndDate"] = BLL.ConvertRule.ToDate(txtEndDate.Value);
					}

					if (isNew) 
					{
						entity.CurrentTable.Rows.Add(dr);
						DAL.EntityDAO.ConstructDAO.InsertConstructPlanStep(entity);
					}
					else 
					{
						DAL.EntityDAO.ConstructDAO.UpdateConstructPlanStep(entity);
					}

					entity.Dispose();
				}
			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
//			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) 
//			{
//				AspWebControl.Calendar txt = (AspWebControl.Calendar)e.Item.FindControl("txtStartDate");
//				txt.ID = "txtStartDate" + e.Item.ItemIndex.ToString();
//			}
		}
	}
}
