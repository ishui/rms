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

namespace RmsPM.Web.CashFlow
{
	/// <summary>
	/// RptConstructProgList ��ժҪ˵����
	/// </summary>
	public partial class RptConstructProgList : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				IniPage();
//				LoadGrid();
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
				this.txtYear.Value = Request.QueryString["Year"];
				this.txtMonth.Value = Request.QueryString["Month"];

				this.lblYm.InnerText = this.txtYear.Value + "��" + this.txtMonth.Value + "��";
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadGrid()
		{
			try 
			{
				string ProjectCode = this.txtProjectCode.Value;
				int IYear = BLL.ConvertRule.ToInt(this.txtYear.Value);
				int IMonth = BLL.ConvertRule.ToInt(this.txtMonth.Value);

				DataTable tb = BLL.CashFlowRule.GenerateRptSalList(ProjectCode, IYear, IMonth);

				tb.Columns.Add("TrHtml");
				foreach(DataRow dr in tb.Rows)
				{
					string ItemName = BLL.ConvertRule.ToString(dr["ItemName"]);
					string ItemDesc = BLL.ConvertRule.ToString(dr["ItemDesc"]);
					string FieldName = BLL.ConvertRule.ToString(dr["FieldName"]);
					int Deep = BLL.ConvertRule.ToInt(dr["Deep"]);
					string PBSTypeCode = BLL.ConvertRule.ToString(dr["PBSTypeCode"]);
					string PBSTypeName = BLL.ConvertRule.ToString(dr["PBSTypeName"]);
					string ParentCode = BLL.ConvertRule.ToString(dr["ParentCode"]);

					string ClassName = "";
//					decimal val;

					string id = FieldName + "_" + PBSTypeCode;

					string html = "";
					html += "<tr id='mytr" + id + "' PBSTypeCode='" + PBSTypeCode + "' ParentCode='" + ParentCode + "' Deep='" + Deep + "' FieldName='" + FieldName + "'>";

					if (Deep == 0) //��Ʒ���͸����
					{
						/*
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
						*/

						int AllCount = tb.Select(string.Format("ItemName='{0}'", ItemName)).Length;
						int rowspan = AllCount + 1;

						html += "<td align='center' class='list-c' nowrap rowspan='" + rowspan.ToString() + "'>" + ItemDesc + "</td>"
							;
					}
					else
					{
					}

					//��Ʒ�����Ƿ����ӽ��
					int ChildCount;
					if (Deep == 0)
					{
						ChildCount = tb.Select(string.Format("ItemName='{0}' and ParentCode='' and Deep > 0", ItemName)).Length;
					}
					else
					{
						ChildCount = tb.Select(string.Format("ItemName='{0}' and ParentCode='{1}'", ItemName, PBSTypeCode)).Length;
					}

					//չ�����۵�
					string image = "";
					/*
					string image = "<span style=\"width:15px\">";
					if (ChildCount > 0)
					{
						image += "<img exp=1 id='myimg" + id + "' src=\"../Images/Minus.gif\" PBSTypeCode='" + PBSTypeCode + "' Deep='" + Deep.ToString() + "' FieldName='" + FieldName + "' IsAct='" + IsAct.ToString() + "' onclick=\"TreeExpand(this);\">";
					}
					else
					{
					}
					image += "</span>";
					*/

					//����
					string space = "";
					for(int i=0;i<Deep;i++)
					{
						space += "&nbsp;&nbsp;&nbsp;";
					}

					if (Deep == 0) //��Ʒ���͸����
					{
						html += "<td nowrap class='sum-item'>" + space + image + ItemName + "</td>";
						ClassName = "sum";
					}
					else
					{
						html += "<td nowrap class='list-c'>" + space + image + PBSTypeName + "</td>";
					}

					//����ʵ��
					html += "<td align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["CurrAct"], FieldName) + "</td>";

					//���¼ƻ�
					html += "<td align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["CurrPlan"], FieldName) + "</td>";

					//���¶Ա�
					html += "<td align='right' class='" + ClassName + "'>" + BLL.StringRule.AddUnit(BLL.CashFlowRule.FormatSalListValue(dr["CurrPercent"], "PERCENT"), "%") + "</td>";

					//����Ԥ��
					html += "<td align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["AfterPlanM1"], FieldName) + "</td>";
					html += "<td align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["AfterPlanM3"], FieldName) + "</td>";

					//�����ۼ�ʵ��
					html += "<td align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["CurrYAct"], FieldName) + "</td>";

					//�����ۼƼƻ�
					html += "<td align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["CurrYPlan"], FieldName) + "</td>";

					//�����ۼƶԱ�
					html += "<td align='right' class='" + ClassName + "'>" + BLL.StringRule.AddUnit(BLL.CashFlowRule.FormatSalListValue(dr["CurrYPercent"], "PERCENT"), "%") + "</td>";

					//��Ŀ�ۼ�ʵ��
					html += "<td align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["ProjectAct"], FieldName) + "</td>";

					//��Ŀ�ۼƼƻ�
					html += "<td align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["ProjectPlan"], FieldName) + "</td>";

					//��Ŀ�ۼƶԱ�
					html += "<td align='right' class='" + ClassName + "'>" + BLL.StringRule.AddUnit(BLL.CashFlowRule.FormatSalListValue(dr["ProjectPercent"], "PERCENT"), "%") + "</td>";

					html += "</tr>";

					/*
					if (Deep == 0) //��Ʒ���͸����
					{
						//ռԤ�������ܶ�ٷֱ�
						html += "<tr>";

						html += "</tr>";
					}
					*/


					dr["TrHtml"] = html;
				}

				BindDataGrid(tb);
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�������" + ex.Message));
			}
		}

		private void BindDataGrid(DataTable tb)
		{
			try 
			{
				/*
				DataView dvArea = new DataView(tb, "FieldName='Money'", "sno, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListMoney.DataSource = dvArea;
				this.dgListMoney.DataBind();

				DataView dvHouseArea = new DataView(tb, "FieldName='HouseArea'", "sno, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListHouseArea.DataSource = dvHouseArea;
				this.dgListHouseArea.DataBind();

				DataView dvPrice = new DataView(tb, "FieldName='Price'", "sno, PBSTypeFullID", DataViewRowState.CurrentRows);
				this.dgListPrice.DataSource = dvPrice;
				this.dgListPrice.DataBind();
				*/

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ��ȼƻ���ϸ����" + ex.Message));
			}
		}

	}
}
