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
	/// CostBudgetList 的摘要说明。
	/// </summary>
	public partial class CostBudgetList : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputText txtCostBudgetID;
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAdd;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdSearchStatus;
		protected System.Web.UI.HtmlControls.HtmlInputText txtContractName;
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
//				this.txtStatus.Value = Request.QueryString["Status"];

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

				if (this.txtAdvSearch.Value != "none") 
				{

					if ( this.ucCreatePerson .Value != "" )
						sb.AddStrategy( new Strategy( CostBudgetStrategyName.CreatePerson,this.ucCreatePerson.Value ));
					if ( this.ucModifyPerson .Value != "" )
						sb.AddStrategy( new Strategy( CostBudgetStrategyName.ModifyPerson,this.ucModifyPerson.Value ));

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

				}

				//权限
				ArrayList arA = new ArrayList();
				arA.Add(user.UserCode);
				arA.Add(user.BuildStationCodes());
				sb.AddStrategy( new Strategy( DAL.QueryStrategy.CostBudgetStrategyName.AccessRange,arA));

				sb.AddOrder( "CostBudgetSetName", true);
				string sql = sb.BuildQueryViewCostDynamicListString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "CostBudgetSet",sql );
				qa.Dispose();

				/*
				string[] arrField = {"Money", "TotalPayoutMoney"};
				decimal[] arrSum = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);
				this.txtSumMoney.Value = arrSum[0].ToString("N");
				this.txtSumTotalPayoutMoney.Value = arrSum[1].ToString("N");
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

		private void BindDataGrid(DataTable tb)
		{
			try 
			{
				this.dgList.DataSource = tb;
				this.dgList.DataBind();

//				this.GridPagination1.RowsCount = tb.Rows.Count.ToString();
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
