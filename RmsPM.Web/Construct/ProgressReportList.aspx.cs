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

namespace RmsPM.Web.Construct
{
	/// <summary>
	/// ProgressReportList 的摘要说明。
	/// </summary>
	public partial class ProgressReportList : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputText txtSearchPBSUnitName;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanTitle;
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.spanReportPerson.InnerText = this.txtReportPersonName.Value;

			if (!Page.IsPostBack) 
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
			this.dgList.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemCreated);
			this.dgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgList_PageIndexChanged);
			this.dgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgList_SortCommand);

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtPBSUnitCode.Value = Request.QueryString["PBSUnitCode"];

				//权限
				this.btnAdd.Visible = user.HasRight("020202");

				BLL.PageFacade.LoadPBSUnitSelect(sltSearchPBSUnitCode, this.txtPBSUnitCode.Value, this.txtProjectCode.Value);
//				BLL.PageFacade.LoadAllUserSelect(this.sltSearchReportPerson,"");
				BLL.PageFacade.LoadVisualProgressSelect(this.sltSearchVisualProgress, "");

				//只显示某个单位工程的进度报告
				if (this.txtPBSUnitCode.Value.Trim() != "") 
				{
					this.tableSearch.Style["display"] = "none";
					this.trPBSUnitName.Style["display"] = "";

					EntityData entityU = DAL.EntityDAO.PBSDAO.GetPBSUnitByCode(txtPBSUnitCode.Value);
					if (entityU.HasRecord()) 
					{
						this.lblPBSUnitName.Text = entityU.GetString("PBSUnitName");
						this.txtProjectCode.Value = entityU.GetString("ProjectCode");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "单位工程不存在"));
					}
					entityU.Dispose();
				}
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
				string PBSUnitCode = this.sltSearchPBSUnitCode.Value.Trim();

				ConstructProgressStrategyBuilder sb = new ConstructProgressStrategyBuilder();

				sb.AddStrategy(new Strategy(ConstructProgressStrategyName.ProjectCode, this.txtProjectCode.Value));
				if (PBSUnitCode.Length > 0)
					sb.AddStrategy(new Strategy(ConstructProgressStrategyName.PBSUnitCode, PBSUnitCode));

				if ( this.dtSearchReportDateBegin.Value != "" || this.dtSearchReportDateEnd.Value != "" )
				{
					ArrayList ar = new ArrayList();
					ar.Add(this.dtSearchReportDateBegin.Value);
					ar.Add(this.dtSearchReportDateEnd.Value);
					sb.AddStrategy( new Strategy( ConstructProgressStrategyName.ReportDateRange,ar ));
				}

				if ( this.txtReportPerson.Value != "" )
					sb.AddStrategy( new Strategy( ConstructProgressStrategyName.ReportPerson,this.txtReportPerson.Value ));

				if ( this.sltSearchVisualProgress.Value != "" )
					sb.AddStrategy( new Strategy( ConstructProgressStrategyName.VisualProgress,this.sltSearchVisualProgress.Value ));

				if ( this.txtSearchContent.Value != "" )
					sb.AddStrategy( new Strategy( ConstructProgressStrategyName.Content,this.txtSearchContent.Value ));

				if ( this.txtSearchRiskRemark.Value != "" )
					sb.AddStrategy( new Strategy( ConstructProgressStrategyName.RiskRemark,this.txtSearchRiskRemark.Value ));

				//排序
				string sortsql = BLL.GridSort.GetSortSQL(ViewState);
				if (sortsql == "")
				{
					//缺省排序
					sb.AddOrder("ReportDate", false);
					sb.AddOrder("ProgressCode", false);
				}

				string sql = sb.BuildQueryViewString();

				if (sortsql != "")
				{
					//点列标题排序
					sql = sql + " order by " + sortsql;
				}

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "ConstructProgress",sql );
				qa.Dispose();

				//				EntityData entity = DAL.EntityDAO.ConstructDAO.GetConstructProgressByPBSUnit(PBSUnitCode);

				this.dgList.DataSource = entity;
				this.dgList.DataBind();
				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

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

		protected void btnAllowPaging_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.AllowPaging = !(this.dgList.AllowPaging);

			if (this.dgList.AllowPaging) 
			{
				this.btnAllowPaging.Value = "取消分页";
			}
			else 
			{
				this.btnAllowPaging.Value = "分页显示";
			}

			this.dgList.CurrentPageIndex = 0;
			this.LoadDataGrid();
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
