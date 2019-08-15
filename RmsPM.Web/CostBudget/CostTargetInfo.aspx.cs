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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;

namespace RmsPM.Web.CostBudget
{
	/// <summary>
	/// CostTargetInfo ��ժҪ˵����
	/// </summary>
	public partial class CostTargetInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtPaymentCode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtContractCode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtIsContract;
		protected System.Web.UI.HtmlControls.HtmlAnchor hrefViewVoucher;
		protected System.Web.UI.HtmlControls.HtmlTableRow trContract;
		protected System.Web.UI.WebControls.Label lblPurpose;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanTitle;
		protected System.Web.UI.WebControls.Label lblPaymentID;
		protected System.Web.UI.WebControls.Label lblRecieptCount;
		protected System.Web.UI.WebControls.Label lblContractID;
		protected System.Web.UI.WebControls.Label lblContractName;
		protected System.Web.UI.WebControls.Label lblPayer;
		protected System.Web.UI.WebControls.Label lblPayDate;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAddDtl;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAddFromCost;
		protected System.Web.UI.WebControls.Label lblMoney;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnPayout;
		protected System.Web.UI.WebControls.Label lblIsContractName;
		protected System.Web.UI.WebControls.DataGrid dgContractAllocation;
		protected System.Web.UI.HtmlControls.HtmlTable trContract2;
		protected System.Web.UI.WebControls.DataGrid dgPayoutItem;
		protected System.Web.UI.HtmlControls.HtmlTable trContract3;
		protected System.Web.UI.WebControls.DataGrid dgContractPaymentPlan;
		protected System.Web.UI.HtmlControls.HtmlTable trContract4;
		protected System.Web.UI.HtmlControls.HtmlTable trContract5;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAccount;
		protected System.Web.UI.WebControls.Label lblRemark;
		protected System.Web.UI.WebControls.Label lblSupplyName;
		protected System.Web.UI.WebControls.Label lblOldMoney;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanOldMoney;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanOldMoney2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSubjectSetCode;
		protected System.Web.UI.HtmlControls.HtmlTable tbOpiniont;
		protected System.Web.UI.HtmlControls.HtmlTable tbOpinionv;
		protected System.Web.UI.WebControls.Label lblOpinion;

		/// <summary>
		/// �Ƿ���ʾ��ʷԤ����
		/// </summary>
		public bool IsShowBudgetMoneyHis
		{
			get {return (this.txtShowTargetHis.Value == "1");}
			set {this.txtShowTargetHis.Value = (value?"1":"");}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.ucCostBudgetSelectMonth.GotoMonthClick += new System.EventHandler(this.btnGotoMonth_ServerClick);

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
				this.txtCostBudgetCode.Value = Request.QueryString["CostBudgetCode"];
				this.txtCostBudgetSetCode.Value = Request.QueryString["CostBudgetSetCode"];
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}


		private void LoadData()
		{
			string CostBudgetCode = this.txtCostBudgetCode.Value;
			string CostBudgetSetCode = this.txtCostBudgetSetCode.Value;

            switch (this.up_sPMNameLower)
            {
                case "shidaipm":
                    this.btnflowcheck.Visible = true;
                    break;
                default:
                    this.btnflowcheck.Visible = false;
                    break;
            }
            ViewState["_ObjectCostConfirmURL"] = BLL.WorkFlowRule.GetProcedureURLByName("Ŀ��ɱ��������");
			try
			{
				if ( CostBudgetCode != "")
				{
					//Ԥ���
					EntityData entity = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetByCode(CostBudgetCode);
					if ( entity.HasRecord())
					{
						DataRow dr = entity.CurrentRow;

						this.txtCostBudgetSetCode.Value = entity.GetString("CostBudgetSetCode");

						this.lblCostBudgetSetName.Text = entity.GetString("CostBudgetSetName");
						this.lblVerID.Text = entity.GetDecimal("VerID").ToString();
                        this.lblCostBudgetName.Text = entity.GetString("CostBudgetName");
                        this.txtProjectCode.Value = entity.GetString("ProjectCode");
						this.lblGroupName.Text = entity.GetString("GroupName");
						this.lblUnitName.Text = entity.GetString("UnitName");
						this.lblPBSName.Text = entity.GetString("PBSName");

						this.lblCreateDate.Text = entity.GetDateTimeOnlyDate("CreateDate");
						this.lblCreatePersonName.Text = entity.GetString("CreatePersonName");

						this.lblModifyDate.Text = entity.GetDateTimeOnlyDate("ModifyDate");
						this.lblModifyPersonName.Text = entity.GetString("ModifyPersonName");

						this.lblCheckDate.Text = entity.GetDateTimeOnlyDate("CheckDate");
						this.lblCheckPersonName.Text = entity.GetString("CheckPersonName");

						this.txtStatus.Value = entity.GetInt("Status").ToString();
						this.lblStatusName.Text = entity.GetString("StatusName");

						this.txtFirstCostBudgetCode.Value = entity.GetString("FirstCostBudgetCode");

						switch (this.txtStatus.Value)
						{
							case "1"://��ǰ
								this.btnModify.Style["display"] = "";
								this.btnDelete.Style["display"] = "none";
								this.btnCheck.Style["display"] = "none";

								this.btnViewHistory.Style["display"] = "";
                                if (this.up_sPMNameLower == "shidaipm")
                                    this.btnflowcheck.Style["display"] = "none";

								break;

							case "2"://��ʷ
								this.btnModify.Style["display"] = "none";
								this.btnDelete.Style["display"] = "none";
								this.btnCheck.Style["display"] = "none";

								this.btnViewHistory.Style["display"] = "none";
                                if (this.up_sPMNameLower == "shidaipm")
                                this.btnflowcheck.Style["display"] = "none";

								break;

                            case "3"://ʱ���ɱ�������� ������
                                if (this.up_sPMNameLower == "shidaipm")
                                {
                                    this.btnModify.Style["display"] = "none";
                                    this.btnDelete.Style["display"] = "none";
                                    this.btnCheck.Style["display"] = "none";
                                    this.btnflowcheck.Style["display"] = "none";
                                    this.btnViewHistory.Style["display"] = "none";
                                }
                                break;

							default: //δ�󡢵���
								this.btnModify.Style["display"] = "";
								this.btnDelete.Style["display"] = "";
								this.btnCheck.Style["display"] = "";

								this.btnViewHistory.Style["display"] = "none";
                                if (this.up_sPMNameLower == "shidaipm")
                                this.btnflowcheck.Style["display"] = "";
								break;
						}

						BindDataGrid(false);
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "Ԥ�������"));
						return;
					}
					entity.Dispose();
				}
				else //��δ��Ԥ��
				{
					//Ԥ�����ñ�
					EntityData entity = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetSetByCode(CostBudgetSetCode);
					if ( entity.HasRecord())
					{
						DataRow dr = entity.CurrentRow;

						this.lblCostBudgetSetName.Text = entity.GetString("CostBudgetSetName");
						this.txtProjectCode.Value = entity.GetString("ProjectCode");
						this.lblGroupName.Text = entity.GetString("GroupName");
						this.lblUnitName.Text = entity.GetString("UnitName");
						this.lblPBSName.Text = entity.GetString("PBSName");

						this.btnModify.Style["display"] = "";
						this.btnDelete.Style["display"] = "";
						this.btnCheck.Style["display"] = "none";

						this.btnViewHistory.Style["display"] = "none";
                        if (this.up_sPMNameLower == "shidaipm")
                        this.btnflowcheck.Style["display"] = "none";
						BindDataGrid(false);
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "Ԥ�����ñ�����"));
						return;
					}
					entity.Dispose();
				}

				/*
				//Ȩ��
				ArrayList ar = user.GetResourceRight(this.txtCostBudgetSetCode.Value,"CostBudgetSet");
				if ( ! ar.Contains("041201"))  //Ŀ����ò鿴
				{
					Response.Redirect( "../RejectAccess.aspx?OperationCode=041201" );
					Response.End();
				}

				this.btnModify.Visible = ar.Contains("041202");
				this.btnDelete.Visible = ar.Contains("041202");
				this.btnCheck.Visible = ar.Contains("041203");
				this.btnModifySet.Visible = ar.Contains("041103");
				*/
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾԤ������" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾԤ����ϸ
		/// </summary>
		private void BindDataGrid(bool IsScreenToTable) 
		{
			try 
			{
				string StartY = "";
				string EndY = "";

//				BLL.CostBudgetRule.GetCostBudgetStartEnd(this.txtProjectCode.Value, ref StartY, ref EndY);

				if (this.ucCostBudgetSelectMonth.ShowMonth) 
				{
					StartY = this.ucCostBudgetSelectMonth.MonthStart;
					EndY = this.ucCostBudgetSelectMonth.MonthEnd;
				}

                BLL.CostBudgetTargetView target = new RmsPM.BLL.CostBudgetTargetView(this.txtProjectCode.Value, this.txtCostBudgetCode.Value, this.txtCostBudgetSetCode.Value, this.txtFirstCostBudgetCode.Value);
                target.StartY = StartY;
                target.EndY = EndY;
                target.ShowTargetHis = this.IsShowBudgetMoneyHis;

                DataTable tbDtl = target.Generate();

                if (tbDtl == null) return;

                //Ԥ����汾��չ��
				ViewState["TargetHisHead1"] = target.TargetHisHead1;
				ViewState["TargetHisHead2"] = target.TargetHisHead2;

                //Ԥ��ƻ������չ��
				string html_title1 = "";
				string html_title2 = "";
				BLL.CostBudgetPageRule.GenerateCostBudgetPlanTitleHtml(tbDtl, BLL.ConvertRule.ToInt(StartY), BLL.ConvertRule.ToInt(EndY), ref html_title1, ref html_title2);

				ViewState["html_title1"] = html_title1;
				ViewState["html_title2"] = html_title2;

				//�ݴ���ϸ��
				ViewState["tbDtl"] = tbDtl;

				if (IsScreenToTable)
				{
					//��Ļ���ݱ��浽��ʱ��
					tbDtl = ScreenToTable(false);
				}
				else 
				{
					//�۵�ȫ��������
					BLL.CostBudgetPageRule.CollapseAll(tbDtl);

					BLL.CostBudgetPageRule.ExpandRoot(tbDtl);
				}

				this.dgList.DataSource = tbDtl;
				this.dgList.DataBind();

				/*
				string[] arrField = {"ItemMoney", "OldItemMoney", "TotalPayoutMoney", "RemainItemMoney"};
				decimal[] arrSum = BLL.MathRule.SumColumn(tb, arrField);
				this.dgList.Columns[2].FooterText = arrSum[0].ToString("N");
				this.dgList.Columns[3].FooterText = arrSum[1].ToString("N");
				this.dgList.Columns[6].FooterText = arrSum[2].ToString("N");
				this.dgList.Columns[7].FooterText = arrSum[3].ToString("N");

				this.dgList.DataSource = tb;
				this.dgList.DataBind();
				*/
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
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

		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);

			Response.Write("try {window.opener.Refresh();}");
			Response.Write("catch(e){window.opener.location = window.opener.location;}");

			//			Response.Write("window.opener.location = window.opener.location;");
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
				BLL.CostBudgetRule.DeleteCostBudget(this.txtCostBudgetCode.Value);

				GoBack();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ��Ŀ����ó���" + ex.Message));
			}
		}

		/// <summary>
		/// ���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnCheck_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				BLL.CostBudgetRule.CheckCostBudget(this.txtCostBudgetCode.Value, user.UserCode);

				GoBack();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "Ŀ�������˳���" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾĳ����Χ����ȼƻ�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnGotoMonth_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				BindDataGrid(true);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��ȼƻ�����" + ex.Message));
			}
		}

		/// <summary>
		/// ��ʾ��ʷԤ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnShowBudgetMoneyHis_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				this.IsShowBudgetMoneyHis = true;
				BindDataGrid(true);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��ʷԤ�������" + ex.Message));
			}
		}

		/// <summary>
		/// ������ʷԤ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnHideBudgetMoneyHis_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				this.IsShowBudgetMoneyHis = false;
				BindDataGrid(true);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "������ʷԤ�������" + ex.Message));
			}
		}

		/// <summary>
		/// ��Ļ���ݱ��浽��ʱ��
		/// </summary>
		/// <returns></returns>
		private DataTable ScreenToTable(bool isBindGrid) 
		{
			DataTable tb = (DataTable)ViewState["tbDtl"];

			foreach (RepeaterItem item in this.dgList.Items)
			{
				HtmlInputHidden txtCostCode = (HtmlInputHidden)item.FindControl("txtCostCode");
				HtmlInputHidden txtIsExpand = (HtmlInputHidden)item.FindControl("txtIsExpand");

				string CostCode = txtCostCode.Value;

				DataRow dr = null;
				DataRow[] drs = tb.Select("CostCode='" + CostCode + "'");
				if (drs.Length > 0) 
				{
					dr = drs[0];
				}
				else
				{
					throw new Exception("������" + CostCode + "����ʱ���в����ڣ����ܱ���");
				}

				dr["IsExpand"] = BLL.ConvertRule.ToInt(txtIsExpand.Value);
			}

			if (isBindGrid) 
			{
				this.dgList.DataSource = tb;
				this.dgList.DataBind();
			}

			return tb;
		}

	}
}
