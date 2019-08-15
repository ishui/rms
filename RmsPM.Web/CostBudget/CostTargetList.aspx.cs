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
	/// CostTargetList 的摘要说明。
	/// </summary>
	public partial class CostTargetList : PageBase
	{

		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkIsContract;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chkIsNotContract;
		protected System.Web.UI.HtmlControls.HtmlInputText txtContractID;
		protected System.Web.UI.HtmlControls.HtmlInputText txtCostBudgetID;
		protected System.Web.UI.HtmlControls.HtmlInputText txtVoucherID;
		protected System.Web.UI.HtmlControls.HtmlSelect sltAccountant;
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
		protected System.Web.UI.HtmlControls.HtmlInputText txtPayer;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdSearchStatus;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnPayout;
		protected System.Web.UI.HtmlControls.HtmlInputText txtContractName;
		protected System.Web.UI.HtmlControls.HtmlInputText txtSupplyName;
		protected System.Web.UI.HtmlControls.HtmlTable divAdvSearch1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadDataGrid();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtAct.Value = Request.QueryString["Act"];
				this.txtTargetFlag.Value = BLL.ConvertRule.ToInt(Request.QueryString["TargetFlag"]).ToString();
				//				this.txtStatus.Value = Request.QueryString["Status"];

				//权限
				this.btnAddSet.Visible = base.user.HasRight("041102");
				this.btnAddTarget.Visible = base.user.HasRight("041202");
				this.btnAddBudget.Visible = base.user.HasRight("041302");

				switch (this.txtTargetFlag.Value) 
				{
					case "1":  //目标费用
						foreach(DataGridColumn col in this.dgList.Columns)
						{
							if (col.SortExpression.ToUpper() == "TotalBudgetMoney".ToUpper())
							{
								col.HeaderText = "预算费用(元)";
							}
						}

                        this.spanTitle.InnerText = "预算费用";
						this.btnAddBudget.Visible = false;
						this.btnGotoCostBudgetMain.Visible = false;

						break;

					default:  //动态费用
						foreach(DataGridColumn col in this.dgList.Columns)
						{
							if (col.SortExpression.ToUpper() == "TotalBudgetMoney".ToUpper())
							{
								col.HeaderText = "项目费用(元)";
							}
						}

						this.spanTitle.InnerText = "动态调整";
						this.btnAddTarget.Visible = false;
						this.btnGotoCostTargetMain.Visible = false;

						break;
				}

				switch (this.txtAct.Value) 
				{
						/*
					case "1"://应付款
						this.spanTitle.InnerText = "应付款";

						this.tdSearchStatus.Style["display"] = "none";

						this.chkStatus0.Checked = false;
						this.chkStatus1.Checked = true;
						this.chkStatus2.Checked = false;

						this.btnAdd.Style["display"] = "none";
						this.btnPayout.Style["display"] = "";

						this.dgList.Columns[0].Visible = true;

						break;
						*/

					default:
						/*
						this.chkStatus0.Checked = this.txtStatus.Value.IndexOf("0") >= 0;
						this.chkStatus1.Checked = this.txtStatus.Value.IndexOf("1") >= 0;
						this.chkStatus2.Checked = this.txtStatus.Value.IndexOf("2") >= 0;
						*/

						break;
				}

				this.ucPBS.ProjectCode = this.txtProjectCode.Value;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadDataGrid()
		{
			try
			{
				CostBudgetStrategyBuilder sb = new CostBudgetStrategyBuilder();
				sb.AddStrategy( new Strategy( CostBudgetStrategyName.ProjectCode,txtProjectCode.Value));

				//只显示目标费用或动态预算
				int TargetFlag = BLL.ConvertRule.ToInt(this.txtTargetFlag.Value);
				sb.AddStrategy( new Strategy( CostBudgetStrategyName.TargetFlag, TargetFlag.ToString()));

				ArrayList arStatus = new ArrayList();
				if ( this.chkStatus0.Checked ) arStatus.Add("0");
				if ( this.chkStatus1.Checked ) arStatus.Add("1");
				if ( this.chkStatus2.Checked ) arStatus.Add("2");
				if ( this.chkStatus3.Checked ) arStatus.Add("3");
				string status = BLL.ConvertRule.GetArrayLinkString(arStatus);
				if ( status != "" )
					sb.AddStrategy( new Strategy( CostBudgetStrategyName.Status, status ));

				if (this.txtAdvSearch.Value != "none") 
				{
					if ( this.ucInputSystemGroup.Value != "" )
					{
						ArrayList arGroup = new ArrayList();
						arGroup.Add(this.ucInputSystemGroup.Value);
						arGroup.Add("0");
						sb.AddStrategy( new Strategy( CostBudgetStrategyName.GroupCodeEx,arGroup ));
					}

					if ( this.ucUnit.Value != "" )
						sb.AddStrategy( new Strategy( CostBudgetStrategyName.UnitCode,this.ucUnit.Value ));

					if ( this.ucPBS.Value != "" )
					{
						sb.AddStrategy( new Strategy( CostBudgetStrategyName.PBSType,this.ucPBS.PBSType ));
						sb.AddStrategy( new Strategy( CostBudgetStrategyName.PBSCode,this.ucPBS.Value ));
					}

					if ( this.ucCreatePerson .Value != "" )
						sb.AddStrategy( new Strategy( CostBudgetStrategyName.CreatePerson,this.ucCreatePerson.Value ));
					if ( this.ucModifyPerson .Value != "" )
						sb.AddStrategy( new Strategy( CostBudgetStrategyName.ModifyPerson,this.ucModifyPerson.Value ));
					if ( this.ucCheckPerson.Value != "" )
						sb.AddStrategy( new Strategy( CostBudgetStrategyName.CheckPerson,this.ucCheckPerson.Value ));

					if ( this.dtCreateDateBegin.Value != "" || this.dtCreateDateEnd.Value != "" )
					{
						ArrayList ar = new ArrayList();
						ar.Add(this.dtCreateDateBegin.Value);
						ar.Add(this.dtCreateDateEnd.Value);
						sb.AddStrategy( new Strategy( CostBudgetStrategyName.CreateDateRange,ar ));
					}

					if ( this.dtModifyDateBegin.Value != "" || this.dtModifyDateEnd.Value != "" )
					{
						ArrayList ar = new ArrayList();
						ar.Add(this.dtModifyDateBegin.Value);
						ar.Add(this.dtModifyDateEnd.Value);
						sb.AddStrategy( new Strategy( CostBudgetStrategyName.ModifyDateRange,ar ));
					}

					if ( this.dtCheckDateBegin.Value != "" || this.dtCheckDateEnd.Value != "" )
					{
						ArrayList ar = new ArrayList();
						ar.Add(this.dtCheckDateBegin.Value);
						ar.Add(this.dtCheckDateEnd.Value);
						sb.AddStrategy( new Strategy( CostBudgetStrategyName.CheckDateRange,ar ));
					}

				}

				//权限
				ArrayList arA = new ArrayList();
				arA.Add(user.UserCode);
				arA.Add(user.BuildStationCodes());
				sb.AddStrategy( new Strategy( DAL.QueryStrategy.CostBudgetStrategyName.AccessRange,arA));

				//缺省排序（项目总体费用排在最后）
				sb.AddOrder( "GroupSortID", true);
				sb.AddOrder( "PBSType", true);
				sb.AddOrder( "CostBudgetSetName", true);
				sb.AddOrder("VerID", true);
				string sql = sb.BuildQueryViewString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "CostBudget",sql );
				qa.Dispose();

				/*
				string[] arrField = {"Money", "TotalPayoutMoney"};
				decimal[] arrSum = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);
				this.txtSumMoney.Value = arrSum[0].ToString("N");
				this.txtSumTotalPayoutMoney.Value = arrSum[1].ToString("N");
				*/


				/*
				//显示未做预算的预算设置表
				if (this.chkStatus1.Checked) 
				{
					EntityData entitySet = GetEmptyCostBudgetSet(this.txtProjectCode.Value);
					foreach(DataRow drSet in entitySet.CurrentTable.Rows) 
					{
						DataRow drNew = entity.CurrentTable.NewRow();

						BLL.ConvertRule.DataRowCopy(drSet, drNew, entitySet.CurrentTable, entity.CurrentTable, new string[] {"ModifyPerson", "ModifyPersonName", "ModifyDate"});
						drNew["CostBudgetCode"] = "NULL_" + drSet["CostBudgetSetCode"].ToString();

						entity.CurrentTable.Rows.Add(drNew);
					}
					entitySet.Dispose();
				}
				*/

				BindDataGrid(entity.CurrentTable);
				entity.Dispose();
				

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
			}
		}

		/*
		/// <summary>
		/// 显示未做预算的预算设置表
		/// </summary>
		/// <returns></returns>
		private EntityData GetEmptyCostBudgetSet(string ProjectCode)
		{
			try 
			{
				CostBudgetSetStrategyBuilder sb = new CostBudgetSetStrategyBuilder();
				sb.AddStrategy( new Strategy( CostBudgetSetStrategyName.ProjectCode, ProjectCode));

				sb.AddOrder( "CostBudgetSetName", true);
				string sql = sb.BuildQueryViewString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "CostBudgetSet",sql );
				qa.Dispose();

				return entity;
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}
		*/

		private void BindDataGrid(DataTable tb)
		{
			try 
			{
				this.dgList.DataSource = tb;
				this.dgList.DataBind();

				this.GridPagination1.RowsCount = tb.Rows.Count.ToString();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
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
			this.dgList.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemCreated);
			this.dgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgList_SortCommand);
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		private void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
				((DataGrid)source).CurrentPageIndex = 0;
				LoadDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				BLL.GridSort.ItemCreate((DataGrid)sender, ViewState, sender, e);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}	

		protected void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
		{
			try
			{
				this.LoadDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid();
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			/*
			if (e.Item.ItemType == ListItemType.Footer) 
			{
				//显示合计金额
				((Label)e.Item.FindControl("lblSumMoney")).Text = this.txtSumMoney.Value;
				((Label)e.Item.FindControl("lblSumTotalPayoutMoney")).Text = this.txtSumTotalPayoutMoney.Value;
			}
			*/
		}
	}
}
