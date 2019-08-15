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
using RmsPM.DAL.QueryStrategy;
using RmsPM.DAL.EntityDAO;
using Rms.Web;
using RmsReport;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// CostCheckList 的摘要说明。
	/// </summary>
	public partial class CostCheckList : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadDataGrid();
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
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);
			this.btnApportionExcel.ServerClick += new System.EventHandler(this.btnApportionExcel_ServerClick);
		}
		#endregion

		private void IniPage()
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
			}
			catch (Exception ex) 
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示数据出错：" + ex.Message));
			}
		}

		private void LoadDataGrid() 
		{
			string projectCode = Request["ProjectCode"]+"";
			try 
			{
				//面积字段
				string BuildingAreaFieldName = BLL.CostRule.GetApportionAreaField(projectCode);
				string BuildingAreaFieldDesc = BLL.ProductRule.GetBuildingAreaFieldDesc(BuildingAreaFieldName);
				((System.Web.UI.WebControls.TemplateColumn)dgList.Columns[4]).HeaderText = BuildingAreaFieldDesc + "(平米)";

				BuildingStrategyBuilder sb = new BuildingStrategyBuilder();

				sb.AddStrategy( new Strategy( BuildingStrategyName.ProjectCode, projectCode));
				sb.AddStrategy( new Strategy( BuildingStrategyName.IsArea, "2"));

				sb.AddOrder("BuildingName", true);

				string sql = sb.BuildQueryViewString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "Building",sql );
				qa.Dispose();

				string[] arrField = {"RoomArea", "TotalCost"};
				decimal[] arrSum = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);

				ViewState["SumArea"] = BLL.StringRule.BuildShowNumberString(arrSum[0]);
				ViewState["SumCost"] = BLL.StringRule.BuildShowNumberString(arrSum[1]);

				DataTable tb = entity.CurrentTable;
				BLL.PaymentRule.AddBuildingCBVoucherCode(tb);

				//填面积
				foreach(DataRow dr in tb.Rows) 
				{
					dr["Area"] = dr[BuildingAreaFieldName];
				}

				dgList.DataSource = tb;
				dgList.DataBind();
				entity.Dispose();
			}
			catch (Exception ex) 
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示数据出错：" + ex.Message));
			}
		}

//		private void btnCalc_ServerClick(object sender, System.EventArgs e)
//		{
//			try 
//			{
//				BLL.CostRule.CostCheck(this.txtProjectCode.Value);
//			}
//			catch (Exception ex) 
//			{
//				ApplicationLog.WriteLog(this.ToString(),ex,"");
//				Response.Write(Rms.Web.JavaScript.Alert(true, "成本核算出错：" + ex.Message));
//				return;
//			}
//
//			LoadDataGrid();
//		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Footer) 
			{
				//显示合计
				((Label)e.Item.FindControl("lblSumArea")).Text = BLL.ConvertRule.ToString(ViewState["SumArea"]);
				((Label)e.Item.FindControl("lblSumCost")).Text = BLL.ConvertRule.ToString(ViewState["SumCost"]);
			}
		}

		/// <summary>
		/// 分摊表Excel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnApportionExcel_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				string ProjectCode = Request["ProjectCode"] + "";

				DataTable tb = BLL.CostRule.CostApportionExcel(ProjectCode, BLL.CostRule.GetApportionAreaField(ProjectCode));

				if (tb.Rows.Count == 0) 
				{
					Response.Write(JavaScript.Alert(true, "无数据"));
					return;
				}

				DataView dv = new DataView(tb, "", "BuildingName", DataViewRowState.CurrentRows);

				//导Excel
				TExcel excel = new TExcel(Response, Request, Server, Session);
				try 
				{
					excel.StartRow = 6;
					excel.StartCol = 1;
					excel.ColumnHeadRow = 5;
					//				excel.StartFieldIndex = 3;
					excel.DataSource = dv;

					//新建工作簿
					excel.TemplateFileName = "成本分摊表.xls";
					excel.TemplateSheetName = "Sheet1";
					excel.AddWorkbook();
				

					//表头表尾数据
					excel.SetCellValue("C2", BLL.ProjectRule.GetProjectName(ProjectCode));

					excel.DataToSheet();

					//保存
					excel.SaveWorkbook();
					excel.ShowClient();
				}
				finally 
				{
					excel.Dispose();
				}
			}
			catch (Exception ex) 
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "分摊表Excel出错：" + ex.Message));
			}
		}

	}
}
