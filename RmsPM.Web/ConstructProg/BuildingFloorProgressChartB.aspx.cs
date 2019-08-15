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

namespace RmsPM.Web.ConstructProg
{
	/// <summary>
	/// BuildingFloorProgressChartB 的摘要说明。
	/// </summary>
	public partial class BuildingFloorProgressChartB : PageBase
	{
		protected System.Web.UI.WebControls.Repeater dgList;
		protected System.Web.UI.WebControls.Label lblProjectName;

		private DataTable m_tb;
		private DataTable m_tbBuilding;
		private DataTable m_tbLegend;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadChart();
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
			this.dgBuilding.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.dgBuilding_ItemDataBound);

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				this.txtBuildingCode.Value = Request.QueryString["BuildingCode"];
				this.txtVisualProgress.Value = Request.QueryString["VisualProgress"];
				this.txtMulti.Value = Request.QueryString["Multi"];
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

//				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingByCode(txtBuildingCode.Value);
//				if (entity.HasRecord()) 
//				{
//					this.lblBuildingName.Text = entity.GetString("BuildingName");
//				}
//				else 
//				{
//					Response.Write(Rms.Web.JavaScript.Alert(true, "楼栋不存在"));
//				}
//				entity.Dispose();

				if (this.txtMulti.Value == "1") 
				{
					//多个楼栋，传入形象进度名称
					this.lblVisualProgressName.Text = this.txtVisualProgress.Value;
					this.lblVisualProgressName.Attributes["hint"] = "";
				}
				else 
				{
					//单个楼栋，传入形象进度代码
					this.lblVisualProgressName.Text = BLL.WBSRule.GetWBSName(this.txtVisualProgress.Value);
					this.lblVisualProgressName.Attributes["hint"] = BLL.ConstructProgRule.GetTaskHintHtml(this.txtVisualProgress.Value);
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void GetDataTable(string BuildingCode, string VisualProgress) 
		{
			//楼栋名称（多个）
			EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingByCode(BuildingCode);
			if (entity.HasRecord()) 
			{
				DataRow drB = m_tbBuilding.NewRow();
				drB["BuildingCode"] = BuildingCode;
				drB["BuildingName"] = entity.CurrentRow["BuildingName"];

				m_tbBuilding.Rows.Add(drB);
			}
			entity.Dispose();

			//主数据
			DataTable tbLegend = null;
			DataTable tb = BLL.ConstructProgRule.GenerateBuildingFloorProgressChartTableB(BuildingCode, VisualProgress, ref tbLegend);

			if (m_tb == null) 
			{
				m_tb = tb;
			}
			else 
			{
				BLL.ConvertRule.DataTableCopyRow(tb, m_tb);
			}

			//图例
			if (m_tbLegend == null) 
			{
				m_tbLegend = tbLegend;
			}
			else 
			{
				foreach(DataRow dr in tbLegend.Rows) 
				{
					string TaskName = BLL.ConvertRule.ToString(dr["TaskName"]);

					//按图例的工作项名字distinct
					if (m_tbLegend.Select("TaskName='" + TaskName + "'").Length == 0) 
					{
						DataRow drNew = m_tbLegend.NewRow();
						BLL.ConvertRule.DataRowCopy(dr, drNew, tbLegend, m_tbLegend);
						m_tbLegend.Rows.Add(drNew);
					}
				}
			}
		}

		private void LoadChart() 
		{
			try 
			{
				string BuildingCode = this.txtBuildingCode.Value;
				string VisualProgress = this.txtVisualProgress.Value;
				string Multi = this.txtMulti.Value;
				string ProjectCode = this.txtProjectCode.Value;

				m_tb = null;
				m_tbLegend = null;

				//楼栋名称（多个）
				m_tbBuilding = new DataTable();
				m_tbBuilding.Columns.Add("BuildingCode");
				m_tbBuilding.Columns.Add("BuildingName");

				if (Multi == "1") 
				{
					//多个楼栋
					string[] arrBuildingCode = BuildingCode.Split(",".ToCharArray());
					foreach(string code in arrBuildingCode) 
					{
						if (code != "") 
						{
							string VGCode = "";

							//按形象进度名称取代码
							EntityData entityV = BLL.ConstructProgRule.GetBuildingTaskVisualProgress(code, ProjectCode);
							DataRow[] drs = entityV.CurrentTable.Select("TaskName='" + VisualProgress + "'");
							if (drs.Length > 0) 
							{
								VGCode = drs[0]["WBSCode"].ToString();
							}
							entityV.Dispose();

							GetDataTable(code, VGCode);
						}
					}

					//清空图例的TaskCode、Hint
					foreach(DataRow dr in m_tbLegend.Rows)
					{
						dr["WBSCode"] = "";
						dr["Hint"] = "";
					}
				}
				else 
				{
					//单个楼栋
					GetDataTable(BuildingCode, VisualProgress);
				}

				this.dgBuilding.DataSource = m_tbBuilding;
				this.dgBuilding.DataBind();

				this.dgLegend.DataSource = m_tbLegend;
				this.dgLegend.DataBind();

//				if ((BuildingCode != "") && (VisualProgress != ""))
//				{
//					DataTable tbLegend = null;
//					DataTable tb = BLL.ConstructProgRule.GenerateBuildingFloorProgressChartTableB(BuildingCode, VisualProgress, ref tbLegend);
//
//					this.dgList.DataSource = tb;
//					this.dgList.DataBind();
//
//					this.dgLegend.DataSource = tbLegend;
//					this.dgLegend.DataBind();
//				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"显示图表失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示图表失败：" + ex.Message));
			}
		}

		private void dgBuilding_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			try 
			{
				string BuildingCode = ((HtmlInputHidden)e.Item.FindControl("txtBuildingCodeDtl")).Value;

				//				DataTable tb = (DataTable)ViewState["tb"];
				DataView dv = new DataView(m_tb, string.Format("BuildingCode='{0}'", BuildingCode), "FloorIndex desc", DataViewRowState.CurrentRows);
				Repeater dgList = (Repeater)e.Item.FindControl("dgList");
				dgList.DataSource = dv;
				dgList.DataBind();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"显示图表失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示图表失败：" + ex.Message));
			}		
		}

	}
}
