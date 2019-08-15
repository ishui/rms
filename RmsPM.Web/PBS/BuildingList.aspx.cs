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

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// BuildingList 的摘要说明。
	/// </summary>
	public partial class BuildingList : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlSelect sltModelCode;
		protected System.Web.UI.HtmlControls.HtmlSelect sltInvState;
		protected System.Web.UI.HtmlControls.HtmlSelect sltOutState;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnModifyArea;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSelectCode;
		protected System.Web.UI.HtmlControls.HtmlGenericControl divProjectName;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.divSearchProjectName.InnerText = this.txtSearchProjectName.Value;

			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadDataGrid(true);
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
			this.dgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgList_PageIndexChanged);
			this.dgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgList_SortCommand);
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				BLL.PageFacade.LoadProjectStatusSelect(this.sltSearchProjectStatus, false);
				PageFacade.LoadPBSTypeSelectAll(sltSearchPBSTypeCode,"","0");

				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

				//集团管理进入时，只可查询
				if (this.txtProjectCode.Value == "") 
				{
					//集团管理
					this.dgList.Columns[1].Visible = true;
					this.dgList.Columns[2].Visible = true;

					//分页
					this.dgList.AllowPaging = true;
				}
				else 
				{
					//项目

					//不分页
					this.GridPagination1.DataGridId = "";
					this.GridPagination1.Visible = false;
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadDataGrid(bool isEmpty) 
		{
			try 
			{
				BuildingStrategyBuilder sb = new BuildingStrategyBuilder("V_Building");

				if (isEmpty)
					sb.AddStrategy(new Strategy( BuildingStrategyName.False));

				string ProjectCode = this.txtProjectCode.Value;
				if (ProjectCode != "")
					sb.AddStrategy( new Strategy( BuildingStrategyName.ProjectCode, ProjectCode));

				if (this.txtSearchProjectCode.Value != "")
					sb.AddStrategy(new Strategy(BuildingStrategyName.InProjectCode, this.txtSearchProjectCode.Value));

				if (this.sltSearchProjectStatus.Value != "")
					sb.AddStrategy(new Strategy(BuildingStrategyName.ProjectStatus, this.sltSearchProjectStatus.Value));

				if (this.txtSearchBuildingName.Value != "")
					sb.AddStrategy(new Strategy(BuildingStrategyName.InBuildingName, this.txtSearchBuildingName.Value, "F"));

				if (this.sltSearchPBSTypeCode.Value != "")
					sb.AddStrategy(new Strategy(BuildingStrategyName.PBSTypeCodeAllChild, this.sltSearchPBSTypeCode.Value));

				if (this.txtSearchInvestType.Value != "")
					sb.AddStrategy(new Strategy(BuildingStrategyName.InInvestType, this.txtSearchInvestType.Value, "F"));


				//排序
				string sortsql = BLL.GridSort.GetSortSQL(ViewState);
				if (sortsql == "")
				{
					//缺省排序
					if (ProjectCode == "") 
					{
						sb.AddOrder("ProjectName", true);
					}

					sb.AddOrder("BuildingName", true);
				}

				string sql = sb.BuildMainQueryString();

				if (sortsql != "")
				{
					//点列标题排序
					sql = sql + " order by " + sortsql;
				}

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "V_Building",sql );
				qa.Dispose();

				string[] arrField = {"HouseArea", "RoomArea", "TotalCost"};
				decimal[] arrSum = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);

				ViewState["SumCount"] = entity.CurrentTable.Rows.Count.ToString() + "幢";
				ViewState["SumHouseArea"] = BLL.StringRule.BuildShowNumberString(arrSum[0]);
				ViewState["SumRoomArea"] = BLL.StringRule.BuildShowNumberString(arrSum[1]);
				ViewState["SumTotalCost"] = BLL.StringRule.BuildShowNumberString(arrSum[2]);

				dgList.DataSource = entity;
				dgList.DataBind();

				if (this.GridPagination1.Visible)
				{
					this.GridPagination1.RowsCount = entity.CurrentTable.Rows.Count.ToString();
				}

				entity.Dispose();

				//显示工具栏
				if (entity.HasRecord())
					this.trToolBar.Style["display"] = "";
				else
					this.trToolBar.Style["display"] = "none";
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			//集团管理时，列表分页
			if (this.txtProjectCode.Value == "") 
			{
				this.dgList.AllowPaging = true;
				this.btnPrint.Value = "打印本页";
				this.btnPrintAll.Style["display"] = "";
				this.btnAllowPaging.Style["display"] = "";

				this.GridPagination1.DataGridId = "dgList";
				this.GridPagination1.Visible = true;
			}

			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid(false);
		}

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid(false);
		}

		protected void btnAllowPaging_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.AllowPaging = false;
			this.btnPrint.Value = "打 印";
			this.btnPrintAll.Style["display"] = "none";
			this.btnAllowPaging.Style["display"] = "none";

			this.GridPagination1.DataGridId = "";
			this.GridPagination1.Visible = false;

			this.dgList.CurrentPageIndex = 0;
			this.LoadDataGrid(false);
		}

		private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			BLL.GridSort.ItemCreate((DataGrid)sender, ViewState, sender, e);
		}

		private void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
			((DataGrid)source).CurrentPageIndex = 0;
			LoadDataGrid(false);
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Footer) 
			{
				//显示合计
				((Label)e.Item.FindControl("lblSumCount")).Text = BLL.ConvertRule.ToString(ViewState["SumCount"]);
				((Label)e.Item.FindControl("lblSumHouseArea")).Text = BLL.ConvertRule.ToString(ViewState["SumHouseArea"]);
				((Label)e.Item.FindControl("lblSumRoomArea")).Text = BLL.ConvertRule.ToString(ViewState["SumRoomArea"]);
				((Label)e.Item.FindControl("lblSumTotalCost")).Text = BLL.ConvertRule.ToString(ViewState["SumTotalCost"]);
			}
		}

		protected void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
		{
			try
			{
				this.LoadDataGrid(false);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 打印全部
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPrintAll_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				btnAllowPaging_ServerClick(sender, e);
				this.txtIsLoadPrint.Value = "1";
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "打印出错：" + ex.Message));
			}
		}
	}
}
