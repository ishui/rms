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
	/// BuildingFloorInfo 的摘要说明。
	/// </summary>
	public partial class BuildingFloorInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputText txtSearchPBSUnitName;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanTitle;
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtPBSUnitCode;
		protected System.Web.UI.WebControls.Label lblPBSUnitName;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnShowPBSUnitWindow;
		protected System.Web.UI.HtmlControls.HtmlSelect sltSearchPBSUnitCode;
		protected AspWebControl.Calendar dtSearchReportDateBegin;
		protected AspWebControl.Calendar dtSearchReportDateEnd;
		protected System.Web.UI.HtmlControls.HtmlSelect sltSearchVisualProgress;
		protected System.Web.UI.HtmlControls.HtmlInputText txtSearchContent;
		protected System.Web.UI.HtmlControls.HtmlInputText txtSearchRiskRemark;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanReportPerson;
		protected System.Web.UI.HtmlControls.HtmlAnchor hrefSelectPerson;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtReportPerson;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtReportPersonName;
		protected System.Web.UI.HtmlControls.HtmlTable tableSearch;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPBSUnitName;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
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

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtBuildingCode.Value = Request.QueryString["BuildingCode"];
				this.txtDefaultVisualProgress.Value = Request.QueryString["DefaultVisualProgress"];

				if (this.txtBuildingCode.Value.Trim() != "") 
				{
					EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingByCode(txtBuildingCode.Value);
					if (entity.HasRecord()) 
					{
						this.lblBuildingName.Text = entity.GetString("BuildingName");
						this.txtProjectCode.Value = entity.GetString("ProjectCode");
						this.lblFloorCount.Text = entity.GetIntString("IFloorCount");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "楼栋不存在"));
					}
					entity.Dispose();
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "未传入楼栋代码"));
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
				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingFloorByBuildingCode(this.txtBuildingCode.Value);
				entity.Dispose();

				//工具栏按钮
				if (entity.HasRecord()) 
				{
					this.btnAdd.Style["display"] = "none";
					this.btnModify.Style["display"] = "";
					this.btnDelete.Style["display"] = "";
				}
				else 
				{
					this.btnAdd.Style["display"] = "";
					this.btnModify.Style["display"] = "none";
					this.btnDelete.Style["display"] = "none";
				}

				BindDataGrid(entity.CurrentTable);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void BindDataGrid(DataTable tb) 
		{
			try 
			{
				this.dgList.DataSource = tb;
				this.dgList.DataBind();
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}

		/// <summary>
		/// 删除楼层工程结构
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				BLL.ConstructProgRule.DeleteBuildingFloor(this.txtBuildingCode.Value);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错：" + ex.Message));
				return;
			}

			LoadDataGrid();
		}
	}
}
