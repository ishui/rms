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


namespace RmsPM.Web.Project
{
	/// <summary>
	/// ProjectList 的摘要说明。
	/// </summary>
	public partial class ProjectList : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
	
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
				//权限
				this.btnAdd.Visible = base.user.HasRight("010102");

				BLL.PageFacade.LoadProjectStatusSelect(this.sltSearchStatus, true);
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载项目列表页面错误。");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载项目列表页面错误。"));
			}
		}

		private void LoadDataGrid()
		{
			try
			{
				int kgYear = 0;
				int jgYear = 0;
				
				if (this.txtSearchKgYear.Value.Trim().Length>0)
				{
					kgYear = BLL.ConvertRule.ToInt(this.txtSearchKgYear.Value);
				}

				if (this.txtSearchJgYear.Value.Trim().Length>0)
				{
					jgYear = BLL.ConvertRule.ToInt(this.txtSearchJgYear.Value);
				}
				
				RmsPM.DAL.QueryStrategy.ProjectStrategyBuilder ssb= new RmsPM.DAL.QueryStrategy.ProjectStrategyBuilder();
				if ( this.txtSearchProjectName.Value.Length > 0 )
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.ProjectNameLike,this.txtSearchProjectName.Value));
				if ( kgYear != 0 )
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.kgYear,kgYear.ToString()));
				if ( jgYear != 0 )
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.jgYear,jgYear.ToString()));

				if (this.sltSearchStatus.Value != "")
					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.ProjectStrategyName.Status,this.sltSearchStatus.Value));
				
				//排序
				string sortsql = BLL.GridSort.GetSortSQL(ViewState);
				if (sortsql == "")
				{
					//缺省排序
					ssb.AddOrder("kgDate",false);
					ssb.AddOrder("ProjectName",true);
					//ssb.AddOrder("Status",true);
				}

				string sql = ssb.BuildMainQueryString();

				if (sortsql != "")
				{
					//点列标题排序
					sql = sql + " order by " + sortsql;
				}

				Rms.ORMap.QueryAgent qa = new QueryAgent();
				DataTable tb = qa.ExecSqlForDataSet(sql).Tables[0];
				qa.Dispose();
				
				this.dgList.DataSource = new DataView(tb,"","",DataViewRowState.CurrentRows);
				this.dgList.DataBind();

			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载项目列表错误。");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载项目列表错误。"));
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

		}
		#endregion

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid();
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid();
		}

		private void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
			((DataGrid)source).CurrentPageIndex = 0;
			LoadDataGrid();
		}

		private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			BLL.GridSort.ItemCreate((DataGrid)sender, ViewState, sender, e);
		}
	}
}
