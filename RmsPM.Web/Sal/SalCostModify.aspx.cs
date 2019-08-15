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
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;

namespace RmsPM.Web.Sal
{
	/// <summary>
	/// SalCostModify 的摘要说明。
	/// </summary>
	public partial class SalCostModify : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
				BindDataGrid();
			}
		}

		private void IniPage()
		{
			this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
			this.txtRefreshScript.Value = Request.QueryString["RefreshScript"];
		}

		private void LoadData()
		{
			try
			{
				string ProjectCode = txtProjectCode.Value;

				EntityData entity = DAL.EntityDAO.SalDAO.GetSalCostByCode("");

				DataSet ds = DAL.EntityDAO.SalDAO.GetSalBuildingByProjectCode(ProjectCode);
				DataTable tb = ds.Tables[0];
				int i = 0;
				foreach(DataRow drBuilding in tb.Rows) 
				{
					i = i + 1;
					string BuildingName = drBuilding["BuildingName"].ToString();

					DataRow dr = entity.CurrentTable.NewRow();

					dr["SystemID"] = i.ToString();
					dr["ProjectCode"] = ProjectCode;
					dr["BuildingName"] = BuildingName;
					dr["TotalCost"] = BLL.SalRule.GetSalTotalCostByProjectBuilding(ProjectCode, BuildingName);

					entity.CurrentTable.Rows.Add(dr);
				}

				Session["SalCostTable"] = entity.CurrentTable;
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void BindDataGrid()
		{
			if ( Session["SalCostTable"]  == null )
				return;

			DataTable dt = (DataTable)Session["SalCostTable"];
			this.dgList.DataSource = dt;
			this.dgList.DataBind();
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



		private void btnReload_ServerClick(object sender, System.EventArgs e)
		{
			BindDataGrid();
		}

		private bool SaveToTempTable() 
		{
			DataTable dt = (DataTable)Session["SalCostTable"];

			foreach (DataGridItem item in this.dgList.Items)
			{
				if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem) 
				{
					TextBox txtTotalCost = (TextBox)item.Cells[2].FindControl("txtTotalCost");

					string BuildingName = item.Cells[1].Text;

					DataRow dr = dt.Select("BuildingName='" + BuildingName + "'")[0];

					string TotalCost = txtTotalCost.Text.Trim();

					if ( TotalCost != "")
						dr["TotalCost"] = decimal.Parse(TotalCost);
					else
						dr["TotalCost"] = DBNull.Value;
				}
			}

			Session["SalCostTable"] = dt;
			return true;
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				if (!SaveToTempTable())
					return;

				string ProjectCode = txtProjectCode.Value;

				DataTable tb = (DataTable)Session["SalCostTable"];
				foreach(DataRow drTemp in tb.Rows) 
				{
					DataRow dr;
					string BuildingName = drTemp["BuildingName"].ToString();

					EntityData entity = DAL.EntityDAO.SalDAO.GetSalCostByProjectBuilding(ProjectCode, BuildingName);

					if (entity.HasRecord()) 
					{
						dr = entity.CurrentRow;
					}
					else 
					{
						dr = entity.CurrentTable.NewRow();
						dr["SystemID"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("SalCostSystemID");
						dr["ProjectCode"] = ProjectCode;
						dr["BuildingName"] = BuildingName;
					}

					dr["TotalCost"] = drTemp["TotalCost"];

					if (entity.HasRecord()) 
					{
						DAL.EntityDAO.SalDAO.UpdateSalCost(entity);
					}
					else
					{
						entity.CurrentTable.Rows.Add(dr);
						DAL.EntityDAO.SalDAO.InsertSalCost(entity);
					}

					entity.Dispose();
				}

				Response.Write(Rms.Web.JavaScript.ScriptStart);
				if (this.txtRefreshScript.Value.Trim() != "")
				{
					Response.Write(string.Format("window.opener.{0}", this.txtRefreshScript.Value));
				}
				else 
				{
					Response.Write("window.opener.location = window.opener.location;");
				}
				Response.Write(Rms.Web.JavaScript.WinClose(false));
				Response.Write(Rms.Web.JavaScript.ScriptEnd);
				Response.End();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
	}
}
