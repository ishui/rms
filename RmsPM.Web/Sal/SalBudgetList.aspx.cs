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
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;
using Rms.Web;

namespace RmsPM.Web.Sal
{
	/// <summary>
	/// SalBudgetList ��ժҪ˵����
	/// </summary>
	public partial class SalBudgetList : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			ViewState["HasLoadGrid"] = 0;

			if (!Page.IsPostBack) 
			{
				IniPage();
				IniYear();
				LoadData();
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

		private void IniPage() 
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtProjectName.Value = BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value);

				//Ȩ��
				this.btnAddBudget.Visible = user.HasRight("020402");
				this.btnModifyBudget.Visible = user.HasRight("020403");
				this.btnModifyAct.Visible = user.HasRight("020403");
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void IniYear() 
		{
			try 
			{
				string CurrYear = this.sltYear.Value.Trim();
				int iCurrYear;

				if (CurrYear == "") 
				{
					iCurrYear = DateTime.Today.Year;
				}
				else 
				{
					iCurrYear = BLL.ConvertRule.ToInt(CurrYear);
				}

				this.sltYear.Items.Clear();

				//ǰ����
				for (int i=-5;i<0;i++) 
				{
					int year = iCurrYear + i;
					this.sltYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
				}

				//���ꡢ����
				for (int i=0;i<6;i++) 
				{
					int year = iCurrYear + i;
					this.sltYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
				}

				this.sltYear.Value = iCurrYear.ToString();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ����ȳ���" + ex.Message));
			}
		}

		private void ClearAll() 
		{
			try 
			{
				this.lblPeriodMonthDesc.Text = "";
				this.lblAfterPeriodDesc.Text = "";
				this.lblModiPersonName.Text = "";
				this.lblModiDate.Text = "";

				DataTable tb = BLL.SalBudgetRule.GenerateSalBudgetDtlTableInfra("", 0);
				tb.Columns.Add("TrHtml");

				BindDataGrid(tb);
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		private void LoadData() 
		{
			try 
			{
				ClearAll();

				string ProjectCode = this.txtProjectCode.Value;
				int IYear = BLL.ConvertRule.ToInt(this.sltYear.Value);

				this.lblBudgetName.Text = string.Format("{1}{0}������ۼƻ�", IYear, this.txtProjectName.Value);

				EntityData entity = DAL.EntityDAO.SalDAO.GetSalBudgetByProjectYear(ProjectCode, IYear);
				bool isExists = entity.HasRecord();

				if (entity.HasRecord()) 
				{
					DataRow dr = entity.CurrentRow;
					this.lblPeriodMonthDesc.Text = BLL.SalRule.GetSalBudgetPeriodMonthDesc(dr);
					this.lblAfterPeriodDesc.Text = BLL.SalRule.GetSalBudgetAfterPeriodDesc(dr);

					this.lblModiPersonName.Text = BLL.SystemRule.GetUserName( entity.GetString("ModiPerson"));
					this.lblModiDate.Text = entity.GetDateTimeOnlyDate("ModiDate");
				}

				if (isExists) 
				{
					this.btnAddBudget.Style["display"] = "none";
					this.btnModifyBudget.Style["display"] = "";
				}
				else 
				{
					this.btnAddBudget.Style["display"] = "";
					this.btnModifyBudget.Style["display"] = "none";
				}

				entity.Dispose();

				if (isExists) 
				{
					LoadDataGrid();
				}

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��ȼƻ�����" + ex.Message));
			}
		}

		private void LoadDataGrid() 
		{
			try 
			{
				string ProjectCode = this.txtProjectCode.Value;
				int IYear = BLL.ConvertRule.ToInt(this.sltYear.Value);

				DataTable tb = BLL.SalBudgetRule.GenerateSalBudgetDtlTableInfra(ProjectCode, IYear);

				tb.Columns.Add("TrHtml");
				foreach(DataRow dr in tb.Rows)
				{
					string ItemName = BLL.ConvertRule.ToString(dr["ItemName"]);
					string ItemDesc = BLL.ConvertRule.ToString(dr["ItemDesc"]);
					string FieldName = BLL.ConvertRule.ToString(dr["FieldName"]);
					int IsAct = BLL.ConvertRule.ToInt(dr["IsAct"]);
					string IsActName = BLL.ConvertRule.ToString(dr["IsActName"]);
					int Deep = BLL.ConvertRule.ToInt(dr["Deep"]);
					string PBSTypeCode = BLL.ConvertRule.ToString(dr["PBSTypeCode"]);
					string PBSTypeName = BLL.ConvertRule.ToString(dr["PBSTypeName"]);
					string ParentCode = BLL.ConvertRule.ToString(dr["ParentCode"]);
					decimal val;

					string id = FieldName + "_" + IsAct.ToString() + "_" + PBSTypeCode;

					string html = "";
					html += "<tr id='mytr" + id + "' PBSTypeCode='" + PBSTypeCode + "' ParentCode='" + ParentCode + "' Deep='" + Deep + "' FieldName='" + FieldName + "', IsAct='" + IsAct.ToString() + "'>";

					if (Deep == 0) //��Ʒ���͸����
					{
						if (IsAct == 0)  //�ƻ�
						{
							//�ƻ����ӽ����
							int AllCountPlan = tb.Select(string.Format("ItemName='{0}' and IsAct={1}", ItemName, IsAct)).Length;
							int rowspanPlan = AllCountPlan + 1;

							//ʵ�ʵ��ӽ����
							int AllCountAct = tb.Select(string.Format("ItemName='{0}' and IsAct=1", ItemName)).Length;
							int rowspanAct = AllCountAct + 1;

							html += "<td align='center' class='list-c' nowrap rowspan='" + (rowspanPlan + rowspanAct).ToString() + "'>" + ItemDesc + "</td>"
								+ "<td align='center' class='list-c' nowrap rowspan='" + rowspanPlan.ToString() + "'>" + IsActName + "</td>"
								;
						}
						else  //ʵ��
						{
							int AllCount = tb.Select(string.Format("ItemName='{0}' and IsAct={1}", ItemName, IsAct)).Length;
							int rowspan = AllCount + 1;

							html += "<td align='center' class='list-c' nowrap rowspan='" + rowspan.ToString() + "'>" + IsActName + "</td>"
								;
						}


						/*
						int AllCount = tb.Select(string.Format("ItemName='{0}' and IsAct={1}", ItemName, IsAct)).Length;
						int rowspan = AllCount + 1;

						html += "<td align='center' class='list-c' nowrap rowspan='" + rowspan.ToString() + "'>" + ItemDesc + "</td>"
							+ "<td align='center' class='list-c' nowrap rowspan='" + rowspan.ToString() + "'>�ƻ�</td>"
							;
						*/
					}
					else
					{
					}

					//��Ʒ�����Ƿ����ӽ��
					int ChildCount;
					if (Deep == 0)
					{
						ChildCount = tb.Select(string.Format("ItemName='{0}' and IsAct={1} and ParentCode='' and Deep > 0", ItemName, IsAct)).Length;
					}
					else
					{
						ChildCount = tb.Select(string.Format("ItemName='{0}' and IsAct={1} and ParentCode='{2}'", ItemName, IsAct, PBSTypeCode)).Length;
					}

					//չ�����۵�
					string image = "<span style=\"width:15px\">";
					if (ChildCount > 0)
					{
						image += "<img exp=1 id='myimg" + id + "' src=\"../Images/Minus.gif\" PBSTypeCode='" + PBSTypeCode + "' Deep='" + Deep.ToString() + "' FieldName='" + FieldName + "' IsAct='" + IsAct.ToString() + "' onclick=\"TreeExpand(this);\">";
					}
					else
					{
					}
					image += "</span>";

					//����
					string space = "";
					for(int i=0;i<Deep;i++)
					{
						space += "&nbsp;&nbsp;";
					}

					html += "<td nowrap>" + space + image + PBSTypeName + "</td>";

					//��ǰ�ۼ�
					val = BLL.ConvertRule.ToDecimal(dr["y0"]);
					html += "<td align='right'>" + BLL.SalBudgetRule.FormatSalBudgetFieldValue(val, FieldName) + "</td>";

					//����12����
					for(int i=1;i<=12;i++)
					{
						val = BLL.ConvertRule.ToDecimal(dr["m" + i.ToString()]);
						html += "<td align='right'>" + BLL.SalBudgetRule.FormatSalBudgetFieldValue(val, FieldName) + "</td>";
					}

					//������ȼƻ�
					val = BLL.ConvertRule.ToDecimal(dr["m0"]);
					html += "<td align='right'>" + BLL.SalBudgetRule.FormatSalBudgetFieldValue(val, FieldName) + "</td>";

					//������ȼƻ�
					for(int i=1;i<=2;i++)
					{
						val = BLL.ConvertRule.ToDecimal(dr["y" + i.ToString()]);
						html += "<td align='right'>" + BLL.SalBudgetRule.FormatSalBudgetFieldValue(val, FieldName) + "</td>";
					}

					html += "</tr>";

					dr["TrHtml"] = html;
				}

				BindDataGrid(tb);

				ViewState["HasLoadGrid"] = 1;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��ȼƻ���ϸ����" + ex.Message));
			}
		}

		private void BindDataGrid(DataTable tb)
		{
			try 
			{
//				Session["tbSalBudgetList"] = tb;

//				DataView dv = new DataView(tb, "Deep=0", "sno, IsAct, PBSTypeSortID", DataViewRowState.CurrentRows);
				
				//�ƻ�
				DataView dv = new DataView(tb, "IsAct=0 and FieldName='HouseCount'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgList.DataSource = dv;
				this.dgList.DataBind();

				DataView dvArea = new DataView(tb, "IsAct=0 and FieldName='HouseArea'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListArea.DataSource = dvArea;
				this.dgListArea.DataBind();

				DataView dvPrice = new DataView(tb, "IsAct=0 and FieldName='Price'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListPrice.DataSource = dvPrice;
				this.dgListPrice.DataBind();

				DataView dvMoney = new DataView(tb, "IsAct=0 and FieldName='Money'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListMoney.DataSource = dvMoney;
				this.dgListMoney.DataBind();

				DataView dvRcvMoney = new DataView(tb, "IsAct=0 and FieldName='RcvMoney'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListRcvMoney.DataSource = dvRcvMoney;
				this.dgListRcvMoney.DataBind();

				//ʵ��
				DataView dvAct = new DataView(tb, "IsAct=1 and FieldName='HouseCount'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListAct.DataSource = dvAct;
				this.dgListAct.DataBind();

				DataView dvAreaAct = new DataView(tb, "IsAct=1 and FieldName='HouseArea'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListAreaAct.DataSource = dvAreaAct;
				this.dgListAreaAct.DataBind();

				DataView dvPriceAct = new DataView(tb, "IsAct=1 and FieldName='Price'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListPriceAct.DataSource = dvPriceAct;
				this.dgListPriceAct.DataBind();

				DataView dvMoneyAct = new DataView(tb, "IsAct=1 and FieldName='Money'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListMoneyAct.DataSource = dvMoneyAct;
				this.dgListMoneyAct.DataBind();

				DataView dvRcvMoneyAct = new DataView(tb, "IsAct=1 and FieldName='RcvMoney'", "sno, IsAct, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListRcvMoneyAct.DataSource = dvRcvMoneyAct;
				this.dgListRcvMoneyAct.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��ȼƻ���ϸ����" + ex.Message));
			}
		}

		protected void btnHiddenYear_ServerClick(object sender, System.EventArgs e)
		{
			IniYear();
			LoadData();
		}

	}
}
