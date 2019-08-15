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
	/// ConstructProgressModify ��ժҪ˵����
	/// </summary>
	public partial class ConstructProgressModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnDelete;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				IniPage();
			}

			this.myAttachMentAdd.AttachMentType = "ConstructProgress";
			this.myAttachMentAdd.MasterCode = this.txtProgressCode.Value;
		}

		private void IniPage()
		{
			try 
			{
				this.txtProgressCode.Value = Request.QueryString["ProgressCode"];
				this.txtAct.Value = Request.QueryString["action"];
				this.txtPBSUnitCode.Value = Request.QueryString["PBSUnitCode"];

				PageFacade.LoadVisualProgressSelect(sltVisualProgress,"");

				//����ָ���ֵ�
				EntityData entityRiskIndex = DAL.EntityDAO.ConstructDAO.GetAllRiskIndex();
				ViewState["tbRiskIndex"] = entityRiskIndex.CurrentTable;
				entityRiskIndex.Dispose();

				switch (this.txtAct.Value.ToLower()) 
				{
					case "insert":  //����
						LoadPBSUnit();

						//����ʱ��ȱʡֵ
						this.txtReportDate.Value = DateTime.Today.ToString("yyyy-MM-dd");
						this.txtReportPersonCode.Value = base.user.UserCode;
						this.lblReportPersonName.Text = base.user.UserName;

						//ȱʡ��������һ��������Ϣ
						EntityData entityLast = BLL.ConstructRule.GetLastConstructProgressReport(this.txtPBSUnitCode.Value);
						if (entityLast.HasRecord()) 
						{
							this.sltVisualProgress.Value = entityLast.GetString("VisualProgress");

							this.txtCurrentLayer.Value = BLL.MathRule.GetIntShowString(entityLast.GetInt("CurrentLayer"));
						}
						else 
						{
							//��1����ʱ��ȱʡ�������Ϊ��δ������������1��������ȣ�
							if (this.sltVisualProgress.Items.Count >= 2) 
							{
								this.sltVisualProgress.Value = this.sltVisualProgress.Items[1].Value;
							}
						}
						entityLast.Dispose();

						LoadDataGrid();
						LoadDataGridRisk();

						//����ʱ�����ɱ��
						this.txtProgressCode.Value = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ProgressCode");

						break;

					case "modify":  //�޸�
						LoadData();
						LoadPBSUnit();
						LoadDataGrid();
						LoadDataGridRisk();
						break;

					default:
						Response.Write(Rms.Web.JavaScript.Alert(true, "δ����Ĳ�������"));
						return;
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		public DataTable GetRiskIndexDataSource() 
		{
			return (DataTable)ViewState["tbRiskIndex"];
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
						this.lblPBSUnitName.Text = entity.GetString("PBSUnitName");
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
			string code =  this.txtProgressCode.Value;

			try
			{
				if (code != "") 
				{
					EntityData entity = DAL.EntityDAO.ConstructDAO.GetConstructProgressByCode(code);

					if ( entity.HasRecord())
					{
						this.txtPBSUnitCode.Value = entity.GetString("PBSUnitCode");
						this.txtProjectCode.Value = entity.GetString("ProjectCode");

						this.txtReportDate.Value = entity.GetDateTimeOnlyDate("ReportDate");
						this.txtReportPersonCode.Value = entity.GetString("ReportPerson");
						this.lblReportPersonName.Text = RmsPM.BLL.SystemRule.GetUserName(entity.GetString("ReportPerson"));
						this.sltVisualProgress.Value = entity.GetString("VisualProgress").ToString();
						this.txtContent.Value = entity.GetString("Content");
						this.txtRiskRemark.Value = entity.GetString("RiskRemark");

						this.txtCurrentLayer.Value = BLL.MathRule.GetIntShowString(entity.GetInt("CurrentLayer"));
					}
					else
					{
						this.btnDelete.Visible = false;

						Response.Write(Rms.Web.JavaScript.Alert(true, "���Ȳ�����"));
						Response.End();
					}

					entity.Dispose();
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "���ȱ�Ų���Ϊ��"));
					Response.End();
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"���ؽ���ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "���ؽ���ʧ�ܣ�" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾ������ϸ
		/// </summary>
		private void LoadDataGrid()
		{
			string PBSUnitCode =  this.txtPBSUnitCode.Value;

			try
			{
				int year = BLL.ConvertRule.ToInt(BLL.ConstructRule.GetConstructPlanCurrYearByProject(this.txtProjectCode.Value));

				DataTable tb = BLL.ConstructRule.GenerateConstructPlanProgressTable(PBSUnitCode, year);

				this.dgList.DataSource = tb;
				this.dgList.DataBind();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"���ؽ���ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "���ؽ���ʧ�ܣ�" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾ����������ϸ
		/// </summary>
		private void LoadDataGridRisk()
		{
			try
			{
				string ProgressCode = this.txtProgressCode.Value;
				DataTable tb = BLL.ConstructRule.GenerateConstructProgressRiskTable(ProgressCode, true);

				this.dgRisk.DataSource = tb;
				this.dgRisk.DataBind();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"���ط���������ϸʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "���ط���������ϸʧ�ܣ�" + ex.Message));
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
			this.dgRisk.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgRisk_ItemDataBound);
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

			if(txtReportDate.Value=="")
			{
				Hint = "�����뱨������ �� ";
				return false;
			}

			if(sltVisualProgress.Value=="")
			{
				Hint = "��ѡ��������� �� ";
				return false;
			}

			string val = this.txtCurrentLayer.Value;
			if ( val != "" )
			{
				if ( ! Rms.Check.StringCheck.IsInt(val))
				{
					Hint = "Ŀǰʩ����������Ϊ���� �� ";
					return false;
				}
			}

			string tempHint = BLL.PBSRule.CheckPBSUnitFloorCount(this.txtPBSUnitCode.Value, txtCurrentLayer.Value, "Ŀǰʩ������");
			if (tempHint != "") 
			{
				Hint = tempHint;
				return false;
			}

			//���������ȿ�ʼ����������
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

				string StartDate = txtStartDate.Value.Trim();
				string EndDate = txtEndDate.Value.Trim();
				if ((StartDate != "") && (EndDate != ""))
				{
					if (!BLL.ConvertRule.IsDateStartLessThanEnd(StartDate, EndDate)) 
					{
						Hint = string.Format("�������ڣ�{1}������С�ڿ�ʼ���ڣ�{0}��", StartDate, EndDate);
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write("if (window.opener.opener) window.opener.opener.location = window.opener.opener.location;");
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

				string code = this.txtProgressCode.Value;
				EntityData entity = DAL.EntityDAO.ConstructDAO.GetStandard_ConstructProgressByCode(code);
				bool isNew = ( !entity.HasRecord() );
				
				DataRow dr = null;
				if ( isNew )
				{
					dr = entity.GetNewRecord();

//					code = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ProgressCode");
//					this.txtProgressCode.Value = code;

					dr["ProgressCode"] = this.txtProgressCode.Value;

					dr["PBSUnitCode"] = this.txtPBSUnitCode.Value;
					dr["ProjectCode"] = this.txtProjectCode.Value;

					entity.AddNewRecord(dr);

//					this.myAttachMentAdd.SaveAttachMent(code);
				}
				else
				{
					dr = entity.CurrentRow; 
				}

				dr["ProgressDate"] = DateTime.Now;
				dr["ReportPerson"] = base.user.UserCode;

				dr["VisualProgress"] = sltVisualProgress.Value;
				dr["ReportDate"] = BLL.ConvertRule.ToDate(this.txtReportDate.Value);
				dr["Content"] = this.txtContent.Value;
				dr["RiskRemark"] = this.txtRiskRemark.Value;

				dr["CurrentLayer"] = BLL.ConvertRule.ToIntObj(txtCurrentLayer.Value);

				//������ձ�����ϸ
				SaveRisk(entity);

				DAL.EntityDAO.ConstructDAO.SubmitAllStandard_ConstructProgress (entity);

				entity.Dispose();

				SaveDtl();

				//���µ�λ���̵�������ȡ�ʵ�ʿ���������
				BLL.ConstructRule.UpdatePBSUnitByConstructProgressReport(this.txtPBSUnitCode.Value);

				GoBack();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
			}
		}

		/// <summary>
		/// ������ձ�����ϸ
		/// </summary>
		private void SaveRisk(EntityData entity) 
		{
			try 
			{
				entity.SetCurrentTable("ConstructProgressRisk");

				int iCount = this.dgRisk.Items.Count;
				for(int i=0;i<iCount;i++) 
				{
					string code = this.dgRisk.DataKeys[i].ToString();
					HtmlInputHidden txtRiskTypeName = (HtmlInputHidden)this.dgRisk.Items[i].FindControl("txtRiskTypeName");
					RadioButtonList rdoRiskIndexCode = (RadioButtonList)this.dgRisk.Items[i].FindControl("rdoRiskIndexCode");

					DataRow[] drs = entity.CurrentTable.Select("ProgressRiskCode='" + code + "'");
					bool isNew = (drs.Length == 0);
					DataRow dr;

					if (isNew) 
					{
						dr = entity.CurrentTable.NewRow();
						dr["ProgressRiskCode"] = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ProgressRiskCode");
						dr["ProgressCode"] = this.txtProgressCode.Value;
						dr["ProjectCode"] = this.txtProjectCode.Value;
						entity.CurrentTable.Rows.Add(dr);
					}
					else 
					{
						dr = drs[0];
					}

					dr["RiskTypeName"] = txtRiskTypeName.Value;
					dr["RiskIndexCode"] = rdoRiskIndexCode.SelectedValue;

				}
			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		/// <summary>
		/// ���������ϸ
		/// </summary>
		private void SaveDtl() 
		{
			try 
			{
				string PBSUnitCode = this.txtPBSUnitCode.Value;

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

					EntityData entity = DAL.EntityDAO.ConstructDAO.GetConstructProgressStepByCode(code);
					DataRow dr;
					bool isNew = !entity.HasRecord();

					if (isNew) 
					{
						dr = entity.CurrentTable.NewRow();
						dr["ProgressStepCode"] = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ProgressStepCode");
						entity.CurrentTable.Rows.Add(dr);
					}
					else 
					{
						dr = entity.CurrentRow;
					}

					dr["PBSUnitCode"] = PBSUnitCode;
					dr["ProjectCode"] = this.txtProjectCode.Value;
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

					bool isEmpty = false;
					//���ڶ�Ϊ��ʱ��ɾ����¼
					if ((dr["StartDate"] == DBNull.Value) && (dr["EndDate"] == DBNull.Value))
						isEmpty = true;

					if (isNew) 
					{
						if (!isEmpty) 
						{
							DAL.EntityDAO.ConstructDAO.InsertConstructProgressStep(entity);
						}
					}
					else 
					{
						if (!isEmpty) 
						{
							DAL.EntityDAO.ConstructDAO.UpdateConstructProgressStep(entity);
						}
						else 
						{
							DAL.EntityDAO.ConstructDAO.DeleteConstructProgressStep(entity);
						}
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

		private void dgRisk_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) 
			{
				RadioButtonList rdoRiskIndexCode = (RadioButtonList)e.Item.FindControl("rdoRiskIndexCode");
				HtmlInputHidden txtRiskIndexCode = (HtmlInputHidden)e.Item.FindControl("txtRiskIndexCode");
				foreach(ListItem item in rdoRiskIndexCode.Items) 
				{
					item.Selected = (item.Value == txtRiskIndexCode.Value);
				}
			}
		}
	}
}
