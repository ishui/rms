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
	/// RptCostList 的摘要说明。
	/// </summary>
	public partial class RptCostList : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				IniPage();
				LoadGrid();
				LoadGridCashFlow();
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

		private void IniPage()
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtYear.Value = Request.QueryString["Year"];
				this.txtMonth.Value = Request.QueryString["Month"];

				this.lblYm.InnerText = this.txtYear.Value + "年" + this.txtMonth.Value + "月";
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadGrid()
		{
			try 
			{
				string ProjectCode = this.txtProjectCode.Value;
				int IYear = BLL.ConvertRule.ToInt(this.txtYear.Value);
				int IMonth = BLL.ConvertRule.ToInt(this.txtMonth.Value);

				DataTable tb = BLL.CashFlowRule.GenerateRptCostList(ProjectCode, IYear, IMonth);

				tb.Columns.Add("TrHtml");
				foreach(DataRow dr in tb.Rows)
				{
					string ItemName = BLL.ConvertRule.ToString(dr["ItemName"]);
					string ItemDesc = BLL.ConvertRule.ToString(dr["ItemDesc"]);
					string FieldName = BLL.ConvertRule.ToString(dr["FieldName"]);
					int Deep = BLL.ConvertRule.ToInt(dr["Deep"]);
					string CostCode = BLL.ConvertRule.ToString(dr["CostCode"]);
					string CostName = BLL.ConvertRule.ToString(dr["CostName"]);
					string ParentCode = BLL.ConvertRule.ToString(dr["ParentCode"]);

					string ClassName = "";

					string id = FieldName + "_" + CostCode;

					string html = "";
					html += "<tr id='mytr" + id + "' CostCode='" + CostCode + "' ParentCode='" + ParentCode + "' Deep='" + Deep + "' FieldName='" + FieldName + "'>";

					if (Deep == 0) //产品类型根结点
					{
						/*
						if (IsAct == 0)  //计划
						{
							//计划的子结点数
							int AllCountPlan = tb.Select(string.Format("ItemName='{0}' and IsAct={1}", ItemName, IsAct)).Length;
							int rowspanPlan = AllCountPlan + 1;

							//实际的子结点数
							int AllCountAct = tb.Select(string.Format("ItemName='{0}' and IsAct=1", ItemName)).Length;
							int rowspanAct = AllCountAct + 1;

							html += "<td align='center' class='list-c' nowrap rowspan='" + (rowspanPlan + rowspanAct).ToString() + "'>" + ItemDesc + "</td>"
								+ "<td align='center' class='list-c' nowrap rowspan='" + rowspanPlan.ToString() + "'>" + IsActName + "</td>"
								;
						}
						else  //实际
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

					//费用项是否有子结点
					int ChildCount;
					if (Deep == 0)
					{
						ChildCount = tb.Select(string.Format("ItemName='{0}' and ParentCode='' and Deep > 0", ItemName)).Length;
					}
					else
					{
						ChildCount = tb.Select(string.Format("ItemName='{0}' and ParentCode='{1}'", ItemName, CostCode)).Length;
					}

					//展开、折叠
					string image = "";
					/*
					string image = "<span style=\"width:15px\">";
					if (ChildCount > 0)
					{
						image += "<img exp=1 id='myimg" + id + "' src=\"../Images/Minus.gif\" CostCode='" + CostCode + "' Deep='" + Deep.ToString() + "' FieldName='" + FieldName + "' IsAct='" + IsAct.ToString() + "' onclick=\"TreeExpand(this);\">";
					}
					else
					{
					}
					image += "</span>";
					*/

					//缩进
					string space = "";
					for(int i=0;i<Deep;i++)
					{
						space += "&nbsp;&nbsp;&nbsp;";
					}

					if (Deep == 0) //费用项根结点
					{
						html += "<td nowrap class='sum-item'>" + space + image + ItemName + "</td>";
						ClassName = "sum";
					}
					else
					{
						html += "<td nowrap class='list-c'>" + space + image + CostName + "</td>";
					}

					//本月实际
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["CurrAct"], FieldName) + "</td>";

					//本月计划
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["CurrPlan"], FieldName) + "</td>";

					//本月对比
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.StringRule.AddUnit(BLL.CashFlowRule.FormatSalListValue(dr["CurrPercent"], "PERCENT"), "%") + "</td>";

					//后期预测
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["AfterPlanM1"], FieldName) + "</td>";
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["AfterPlanM3"], FieldName) + "</td>";

					//当年累计实际
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["CurrYAct"], FieldName) + "</td>";

					//当年累计计划
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["CurrYPlan"], FieldName) + "</td>";

					//当年累计对比
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.StringRule.AddUnit(BLL.CashFlowRule.FormatSalListValue(dr["CurrYPercent"], "PERCENT"), "%") + "</td>";

					//项目累计实际
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["ProjectAct"], FieldName) + "</td>";

					//项目累计计划
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["ProjectPlan"], FieldName) + "</td>";

					//项目累计对比
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.StringRule.AddUnit(BLL.CashFlowRule.FormatSalListValue(dr["ProjectPercent"], "PERCENT"), "%") + "</td>";

					html += "</tr>";

					/*
					if (Deep == 0) //费用项根结点
					{
						//占预计销售总额百分比
						html += "<tr>";

						html += "</tr>";
					}
					*/


					dr["TrHtml"] = html;
				}

				BindGrid(tb);
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示成本出错：" + ex.Message));
			}
		}

		private void LoadGridCashFlow()
		{
			try 
			{
				string ProjectCode = this.txtProjectCode.Value;
				int IYear = BLL.ConvertRule.ToInt(this.txtYear.Value);
				int IMonth = BLL.ConvertRule.ToInt(this.txtMonth.Value);

				DataTable tb = BLL.CashFlowRule.GenerateRptCostListCashFlow(ProjectCode, IYear, IMonth);

				tb.Columns.Add("TrHtml");
				foreach(DataRow dr in tb.Rows)
				{
					string ItemName = BLL.ConvertRule.ToString(dr["ItemName"]);
					string ItemDesc = BLL.ConvertRule.ToString(dr["ItemDesc"]);
					int Deep = BLL.ConvertRule.ToInt(dr["Deep"]);

					string ClassName = "";

					string html = "";
					html += "<tr>";

					if (Deep == 0) //根结点
					{
						int AllCount = tb.Rows.Count;
						int rowspan = AllCount + 1;

						html += "<td align='center' class='list-c' nowrap rowspan='" + rowspan.ToString() + "'>" + "现金流量" + "</td>"
							;
					}
					else
					{
					}

					//缩进
					string space = "";
					for(int i=0;i<Deep;i++)
					{
						space += "&nbsp;&nbsp;&nbsp;";
					}

					if (Deep == 0) //根结点
					{
						html += "<td nowrap class='sum-item'>" + space + ItemDesc + "</td>";
						ClassName = "sum";
					}
					else
					{
						html += "<td nowrap class='list-c'>" + space + ItemName + "</td>";
					}

					//本月实际
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["CurrAct"], "") + "</td>";

					//本月计划
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["CurrPlan"], "") + "</td>";

					//本月对比
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.StringRule.AddUnit(BLL.CashFlowRule.FormatSalListValue(dr["CurrPercent"], "PERCENT"), "%") + "</td>";

					//后期预测
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["AfterPlanM1"], "") + "</td>";
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["AfterPlanM3"], "") + "</td>";

					//当年累计实际
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["CurrYAct"], "") + "</td>";

					//当年累计计划
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["CurrYPlan"], "") + "</td>";

					//当年累计对比
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.StringRule.AddUnit(BLL.CashFlowRule.FormatSalListValue(dr["CurrYPercent"], "PERCENT"), "%") + "</td>";

					//项目累计实际
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["ProjectAct"], "") + "</td>";

					//项目累计计划
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.CashFlowRule.FormatSalListValue(dr["ProjectPlan"], "") + "</td>";

					//项目累计对比
					html += "<td nowrap align='right' class='" + ClassName + "'>" + BLL.StringRule.AddUnit(BLL.CashFlowRule.FormatSalListValue(dr["ProjectPercent"], "PERCENT"), "%") + "</td>";

					html += "</tr>";

					dr["TrHtml"] = html;
				}

				BindGridCashFlow(tb);
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示现金流出错：" + ex.Message));
			}
		}

		private void BindGrid(DataTable tb)
		{
			try 
			{
				DataView dv = new DataView(tb, "FieldName='Money'", "sno, CostFullID", DataViewRowState.CurrentRows);
				this.dgList.DataSource = dv;
				this.dgList.DataBind();

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示成本出错：" + ex.Message));
			}
		}

		private void BindGridCashFlow(DataTable tb)
		{
			try 
			{
				DataView dv = new DataView(tb, "", "sno", DataViewRowState.CurrentRows);
				this.dgListCashFlow.DataSource = dv;
				this.dgListCashFlow.DataBind();

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示现金流出错：" + ex.Message));
			}
		}

	}
}
