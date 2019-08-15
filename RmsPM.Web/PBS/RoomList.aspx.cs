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
	/// RoomList 的摘要说明。
	/// </summary>
	public partial class RoomList : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlSelect sltModelCode;
		protected System.Web.UI.HtmlControls.HtmlSelect sltPBSTypeCode;
		protected System.Web.UI.HtmlControls.HtmlSelect sltInvState;
		protected System.Web.UI.HtmlControls.HtmlSelect sltOutState;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
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
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

				//权限
				this.btnModifyArea.Visible = base.user.HasRight("010315");

				//集团管理进入时，只可查询
				if (this.txtProjectCode.Value == "") 
				{
					//集团管理
					this.btnModifyArea.Style["display"] = "none";

					this.dgList.Columns[1].Visible = true;
					this.dgList.Columns[2].Visible = true;

					//分页
					this.dgList.AllowPaging = true;

					((SearchRoom)this.tbSearchRoom).Visible = false;
					((SearchRoomAll)this.tbSearchRoomAll).SetProject(this.txtProjectCode.Value);
				}
				else 
				{
					//项目
					((SearchRoomAll)this.tbSearchRoomAll).Visible = false;
					((SearchRoom)this.tbSearchRoom).SetProject(this.txtProjectCode.Value);

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
				RoomStrategyBuilder sb = new RoomStrategyBuilder("V_ROOM");

				if (isEmpty)
					sb.AddStrategy(new Strategy( RoomStrategyName.False));

				string ProjectCode = this.txtProjectCode.Value;
				if (ProjectCode != "")
					sb.AddStrategy( new Strategy( RoomStrategyName.ProjectCode, ProjectCode));

				if (ProjectCode == "") 
				{
					if (this.tbSearchRoomAll.Visible) 
					{
						((SearchRoomAll)this.tbSearchRoomAll).AddSearch(sb);
					}
				}
				else 
				{
					if (this.tbSearchRoom.Visible) 
					{
						((SearchRoom)this.tbSearchRoom).AddSearch(sb);
					}
				}

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
					sb.AddOrder("ChamberCode", true);
					sb.AddOrder("FloorIndex", true);
					sb.AddOrder("RoomName", true);
				}

				string sql = sb.BuildMainQueryString();

				if (sortsql != "")
				{
					//点列标题排序
					sql = sql + " order by " + sortsql;
				}

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "V_ROOM",sql );
				qa.Dispose();

				string[] arrField = {"BuildArea", "Cost", "TotalPayMoney"};
				decimal[] arrSum = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);

				ViewState["SumCount"] = entity.CurrentTable.Rows.Count.ToString() + "套";
				ViewState["SumBuildArea"] = BLL.StringRule.BuildShowNumberString(arrSum[0]);
				ViewState["SumCost"] = BLL.StringRule.BuildShowNumberString(arrSum[1]);
				ViewState["SumTotalPayMoney"] = BLL.StringRule.BuildShowNumberString(arrSum[2]);

				dgList.DataSource = entity;
				dgList.DataBind();

				if (this.GridPagination1.Visible)
				{
					this.GridPagination1.RowsCount = entity.CurrentTable.Rows.Count.ToString();
				}

				entity.Dispose();

				SetSelectRoomCode();

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

		private void SetSelectRoomCode() 
		{
			//记录列表记录关键字，用“,”分隔
			string[] arr = new string[dgList.DataKeys.Count];
			dgList.DataKeys.CopyTo(arr, 0);
			this.txtSelectRoomCode.Value = BLL.ConvertRule.JoinArray(arr, ",");
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
				((Label)e.Item.FindControl("lblSumBuildArea")).Text = BLL.ConvertRule.ToString(ViewState["SumBuildArea"]);
				((Label)e.Item.FindControl("lblSumCost")).Text = BLL.ConvertRule.ToString(ViewState["SumCost"]);
				((Label)e.Item.FindControl("lblSumTotalPayMoney")).Text = BLL.ConvertRule.ToString(ViewState["SumTotalPayMoney"]);
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
