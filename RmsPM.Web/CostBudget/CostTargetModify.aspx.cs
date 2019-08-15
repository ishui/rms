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
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;
using Infragistics.WebUI.WebDataInput;

namespace RmsPM.Web.CostBudget
{
	/// <summary>
	/// CostTargetModify ��ժҪ˵����
	/// </summary>
	public partial class CostTargetModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl spantest;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.ucCostBudgetSelectMonth.GotoMonthClick += new System.EventHandler(this.btnGotoMonth_ServerClick);

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
				this.txtCostBudgetSetCode.Value = Request["CostBudgetSetCode"];
				this.txtShowCostCode.Value = Request["CostCode"];
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				//ȡ���������е�Ŀ����ñ�
				EntityData entityChange = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetByStatus(this.txtCostBudgetSetCode.Value, 1, "0,3", false);
				if (entityChange.HasRecord()) 
				{
					this.txtCostBudgetCode.Value = entityChange.GetString("CostBudgetCode");
				}
				entityChange.Dispose();

				//ȡ��ǰ��Ч��Ŀ�����
				EntityData entityValid = BLL.CostBudgetRule.GetValidCostBudget(this.txtCostBudgetSetCode.Value, 1);
				if (entityValid.HasRecord())
				{
					this.txtValidCostBudgetCode.Value = entityValid.GetString("CostBudgetCode");
				}
				entityValid.Dispose();

				string CostBudgetCode = this.txtCostBudgetCode.Value;
				string CostBudgetSetCode = this.txtCostBudgetSetCode.Value;
				string ValidCostBudgetCode = this.txtValidCostBudgetCode.Value;

				//Ԥ�����ñ���Ϣ
				EntityData entitySet = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetSetByCode(CostBudgetSetCode);
				if ( entitySet.HasRecord())
				{
					/*
							//Ȩ��
							ArrayList ar = user.GetResourceRight(paymentCode,"Payment");
							if ( ! ar.Contains("060101"))
							{
								Response.Redirect( "../RejectAccess.aspx?OperationCode=060101" );
								Response.End();
							}

							this.btnModify.Visible = ar.Contains("060103");
							this.btnModifyEx.Visible = ar.Contains("060106");
							this.btnDelete.Visible = ar.Contains("060104");
							this.btnCheck.Visible = ar.Contains("060105");
							this.btnAccount.Visible = ar.Contains("060107");
							this.btnPayout.Visible = base.user.HasRight("060202");
							*/

					this.lblCostBudgetSetName.Text = entitySet.GetString("CostBudgetSetName");
					this.txtProjectCode.Value = entitySet.GetString("ProjectCode");

					this.txtGroupCode.Value = entitySet.GetString("GroupCode");
					this.lblGroupName.Text = entitySet.GetString("GroupName");
						
					this.lblUnitName.Text = entitySet.GetString("UnitName");
					this.lblPBSName.Text = entitySet.GetString("PBSName");
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "Ԥ�����ñ�����"));
					return;
				}
				entitySet.Dispose();

				if (CostBudgetCode == "")
				{
					if  (ValidCostBudgetCode == "")  //--------------------------�½�����---------------------------
					{
						this.lblTitle.Text = "����";
						this.txtStatus.Value = "0";

						this.lblVerID.Text = "(����ʱ����)";
						BindDataGrid(false);
					}
					else  //--------------------------�½�����---------------------------
					{
						this.lblTitle.Text = "����";
						this.txtStatus.Value = "3";

						this.lblVerID.Text = "(����ʱ����)";
						this.txtFirstCostBudgetCode.Value = entityValid.GetString("FirstCostBudgetCode");
                        this.txtCostBudgetName.Value = entityValid.GetString("CostBudgetName");

						BindDataGrid(false);
					}
				}
				else //--------------------------�޸�(���롢����)---------------------------
				{
					//Ԥ���
					EntityData entity = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetByCode(CostBudgetCode);
					if ( entity.HasRecord())
					{
						DataRow dr = entity.CurrentRow;

						this.lblVerID.Text = entity.GetDecimal("VerID").ToString();
                        this.txtCostBudgetName.Value = entity.GetString("CostBudgetName");
						this.txtStatus.Value = entity.GetInt("Status").ToString();
						this.txtFirstCostBudgetCode.Value = entity.GetString("FirstCostBudgetCode");

						BindDataGrid(false);
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "���������е�Ԥ�������"));
						return;
					}
					entity.Dispose();

					switch (this.txtStatus.Value) 
					{
						case "0":
							this.lblTitle.Text = "����";
							break;

						case "3":
							this.lblTitle.Text = "����";
							break;

					}
				}

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾԤ������" + ex.Message));
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
			this.dgList.ItemCreated += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.dgList_ItemCreated);
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		/// <summary>
		/// ��ʾԤ����ϸ
		/// </summary>
		private void BindDataGrid(bool IsScreenToTable) 
		{
			try 
			{
				string CostBudgetCode = this.txtCostBudgetCode.Value;
				if (CostBudgetCode == "") //�½�����ʱ�����ݴӵ�ǰ��Ч����
				{
					CostBudgetCode = this.txtValidCostBudgetCode.Value;
				}

				string StartY = "";
				string EndY = "";

				if (this.ucCostBudgetSelectMonth.ShowMonth) 
				{
					StartY = this.ucCostBudgetSelectMonth.MonthStart;
					EndY = this.ucCostBudgetSelectMonth.MonthEnd;
				}

				ViewState["StartY"] = StartY;
				ViewState["EndY"] = EndY;

				BLL.CostBudgetTarget target = new RmsPM.BLL.CostBudgetTarget(this.txtProjectCode.Value, CostBudgetCode, this.txtCostBudgetSetCode.Value);
				target.ShowCostCode = txtShowCostCode.Value;
				target.StartY = StartY;
				target.EndY = EndY;
				target.IsModify = true;

				DataTable tbDtl = target.GenerateCurrent();

				if (tbDtl == null) return;

				//Ԥ��ƻ������չ��
				string html_title1 = "";
				string html_title2 = "";
				BLL.CostBudgetPageRule.GenerateCostBudgetPlanTitleHtml(tbDtl, target.iStartY, target.iEndY, ref html_title1, ref html_title2);

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

					//չ�������
					if (this.txtShowCostCode.Value == "") 
					{
						BLL.CostBudgetPageRule.ExpandRoot(tbDtl);
					}
				}

				this.dgList.DataSource = tbDtl;
				this.dgList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}

		/// <summary>
		/// ����
		/// </summary>
		private void SaveData(DataTable tbDtl, ref bool IsNeedCheck)
		{
			try
			{
				string CostBudgetCode = this.txtCostBudgetCode.Value;
				IsNeedCheck = true;
				string ChangingCostBudgetCode = "";

				//ȡ���������е�Ŀ����ñ�
				EntityData entity = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetByStatus(this.txtCostBudgetSetCode.Value, 1, "0,3", false);
				if (entity.HasRecord()) 
				{
					ChangingCostBudgetCode = entity.GetString("CostBudgetCode");
				}
				entity.Dispose();

				//ȡ��ǰ��Ч��Ŀ�����
				EntityData entityValid = BLL.CostBudgetRule.GetValidCostBudget(this.txtCostBudgetSetCode.Value, 1);

				if (this.txtStatus.Value == "3")  //����
				{
					IsNeedCheck = BLL.CostBudgetRule.IsCostTargetNeedCheck(this.txtCostBudgetSetCode.Value, tbDtl);
				}

				if (!IsNeedCheck)  //�������ʱ��ֱ�Ӹ��µ�ǰ��Ч��Ŀ�����
				{
					this.txtCostBudgetCode.Value = "";
					CostBudgetCode = entityValid.GetString("CostBudgetCode");
				}

				//Ҫ�����Ŀ�����
				entity = RmsPM.DAL.EntityDAO.CostBudgetDAO.GetStandard_CostBudgetByCode(CostBudgetCode);

				//����Ԥ������
				BLL.CostBudgetRule.SaveTempTarget(entity, entityValid, this.txtProjectCode.Value, this.txtCostBudgetSetCode.Value, BLL.ConvertRule.ToInt(this.txtStatus.Value), base.user.UserCode, this.txtCostBudgetName.Value);
				this.txtCostBudgetCode.Value = entity.GetString("CostBudgetCode");

				//����Ԥ����ϸ
				BLL.CostBudgetRule.SaveCostBudgetDtl(entity, tbDtl, BLL.ConvertRule.ToString(ViewState["StartY"]), BLL.ConvertRule.ToString(ViewState["EndY"]));

				//���������Ԥ���ܶ�
				BLL.CostBudgetRule.SaveCostBudgetTotalBudgetMoney(entity.Tables["CostBudget"], entity.Tables["CostBudgetDtl"]);

				//�ύ
				using(StandardEntityDAO dao=new StandardEntityDAO("CostBudget"))
				{
					dao.BeginTrans();
					try
					{
						dao.SubmitEntity(entity);

						//ɾ�������е�Ŀ�����
						if (!IsNeedCheck)
						{
							BLL.CostBudgetRule.DeleteChangingTarget(ChangingCostBudgetCode, dao);
						}

						dao.CommitTrans();
					}
					catch(Exception ex)
					{
						try 
						{
							//RollBackTrans�ᱨ���� SqlTransaction ����ɣ�����Ҳ�޷�ʹ��
							dao.RollBackTrans();
						}
						catch 
						{
						}

						throw ex;
					}
				}

//				DAL.EntityDAO.CostBudgetDAO.SubmitAllStandard_CostBudget(entity);

				entity.Dispose();
				entityValid.Dispose();

			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			/*
			if (this.ucCost.Value.Trim() == "") 
			{
				Hint = "�����������";
				return false;
			}
			*/

			return true;
		}

		/// <summary>
		/// ����
		/// </summary>
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
		/// ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				//��Ļ���ݱ��浽��ʱ��
				DataTable tbDtl = ScreenToTable(false);
				if (tbDtl == null) return;

				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				bool IsNeedCheck = true;
				SaveData(tbDtl, ref IsNeedCheck);

				if (IsNeedCheck)
				{
					Response.Write(JavaScript.Alert(true, "Ŀ������ܶ��ѱ䣬��Ҫ���"));
				}
				else 
				{
					Response.Write(JavaScript.Alert(true, "Ŀ������ܶ�δ�䣬������ˣ�������Ч"));
				}

				GoBack();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "����ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
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
//				HtmlInputText txtPrice = (HtmlInputText)item.FindControl("txtPrice");
//				HtmlInputText txtQty = (HtmlInputText)item.FindControl("txtQty");

				HtmlInputText txtBudgetMoney = (HtmlInputText)item.FindControl("txtBudgetMoney");
				HtmlInputHidden txtCostCode = (HtmlInputHidden)item.FindControl("txtCostCode");
				HtmlInputHidden txtIsExpand = (HtmlInputHidden)item.FindControl("txtIsExpand");
				HtmlInputText txtDescription = (HtmlInputText)item.FindControl("txtDescription");

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

//				dr["Price"] = BLL.ConvertRule.ToDecimal(txtPrice.Value);
//				dr["Qty"] = BLL.ConvertRule.ToDecimal(txtQty.Value);

				dr["BudgetMoney"] = BLL.ConvertRule.ToDecimal(txtBudgetMoney.Value);
				dr["Description"] = txtDescription.Value;

				dr["IsExpand"] = BLL.ConvertRule.ToInt(txtIsExpand.Value);

				//Ԥ��ƻ�
				foreach(DataColumn col in tb.Columns) 
				{
					if (col.ColumnName.StartsWith("BudgetMoney_"))
					{
						string ym = col.ColumnName.Replace("BudgetMoney_", "");
						string y = ym.Substring(0, 4);
						string m = ym.Substring(4, 2);

						HtmlInputText txtM = (HtmlInputText)item.FindControl("txtBudgetMoney_" + ym);
						if (txtM != null) 
						{
							dr[col.ColumnName] = BLL.ConvertRule.ToDecimal(txtM.Value);
						}
					}
				}
			}

			if (isBindGrid) 
			{
				this.dgList.DataSource = tb;
				this.dgList.DataBind();
			}

			return tb;
		}

		private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
			{
				PlaceHolder phPlan = (PlaceHolder)e.Item.FindControl("phPlan");

				DataTable tbDtl = (DataTable)ViewState["tbDtl"];

				//ȱʡչ������
//				int curr_year = DateTime.Today.Year;

				foreach(DataColumn col in tbDtl.Columns) 
				{
					if (col.ColumnName.StartsWith("BudgetMoney_"))
					{
						string ym = col.ColumnName.Replace("BudgetMoney_", "");
						string y = ym.Substring(0, 4);
						string m = ym.Substring(4, 2);

						//ֻչ����1��
						int expand = BLL.CostBudgetPageRule.GetExpandByYear(y.ToString(), BLL.ConvertRule.ToString(ViewState["StartY"]));

						string month_display;

						if (expand == 0)  //�۵�״̬
						{
							month_display = "none";
						}
						else  //չ��״̬
						{
							month_display = "";
						}

						//������
						HtmlInputText txt = new HtmlInputText();
						txt.ID = "txtBudgetMoney_" + ym;
						txt.Attributes["class"] = "input-nember";
						txt.Size = 8;
						txt.Attributes["onblur"] = "MoneyBlur(this, true);";
						txt.Attributes["onfocus"] = "MoneyFocus(this);";

						/*
						WebNumericEdit txt = new WebNumericEdit();
						txt.ID = "txtBudgetMoney_" + ym;
						txt.CssClass = "infra-input-nember";
						txt.ImageDirectory = "../images/infragistics/images/";
						txt.JavaScriptFileName = "../images/infragistics/20051/scripts/ig_edit.js";
						txt.JavaScriptFileNameCommon = "../images/infragistics/20051/scripts/ig_shared.js";
						txt.Width = Unit.Pixel(50);
						*/

						//��ʾ���
						HtmlGenericControl span = new HtmlGenericControl();
						span.ID = "spanBudgetMoney_" + ym;
						span.Style["display"] = "none";

						//��Ԫ��
						HtmlTableCell cell = new HtmlTableCell();
						cell.Controls.Add(txt);
						cell.Controls.Add(span);
						cell.Align = "right";
						cell.NoWrap = true;
						cell.ID = "YearData_" + ym;

						if (m == "00")  //���
						{
						}
						else  //�¶�
						{
							cell.Style["display"] = month_display;
						}

						phPlan.Controls.Add(cell);

					}
				}
			}
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
			{
				int iStartY = BLL.ConvertRule.ToInt(ViewState["StartY"]);
				int iEndY = BLL.ConvertRule.ToInt(ViewState["EndY"]);

				DataTable tbDtl = (DataTable)ViewState["tbDtl"];

				string CostBudgetDtlCode = ((HtmlInputHidden)e.Item.FindControl("txtCostBudgetDtlCode")).Value;
				PlaceHolder phPlan = (PlaceHolder)e.Item.FindControl("phPlan");

				//Ԥ����ϸ��¼
				DataRow[] drsDtl = tbDtl.Select("CostBudgetDtlCode = '" + CostBudgetDtlCode + "'");
				DataRow drDtl = null;
				if (drsDtl.Length > 0) 
				{
					drDtl = drsDtl[0];
				}

				//Ԥ��ƻ�
				if (iStartY > 0)
				{
					for(int iy=iStartY;iy<=iEndY;iy++)
					{
						for(int im=0;im<=12;im++)
						{
							string ym = BLL.ConvertRule.FormatYYYYMM(iy, im);

							//�ƻ����
							if (drDtl != null) 
							{
								string sMoney = BLL.CostBudgetPageRule.GetMoneyShowString(drDtl["BudgetMoney_" + ym]);

								HtmlInputText txt = (HtmlInputText)e.Item.FindControl("txtBudgetMoney_" + ym);
								HtmlGenericControl span = (HtmlGenericControl)e.Item.FindControl("spanBudgetMoney_" + ym);

								txt.Value = sMoney;
								txt.Attributes["RowIndex"] = e.Item.ItemIndex.ToString();
								txt.Attributes["ParentCode"] = BLL.ConvertRule.ToString(drDtl["ParentCode"]);

								span.InnerText = sMoney;
								span.Attributes["RowIndex"] = e.Item.ItemIndex.ToString();

								//							((HtmlInputText)phPlan.Controls[1].Controls[0]).Value = BLL.CostBudgetPageRule.GetMoneyShowString(drsM[0]["BudgetMoney_" + ym]);
								//							((HtmlInputText)phPlan.Controls[1].FindControl("txtBudgetMoney_" + ym)).Value = BLL.CostBudgetPageRule.GetMoneyShowString(drsM[0]["BudgetMoney_" + ym]);
							}
						}
					}
				}
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

	}
}
