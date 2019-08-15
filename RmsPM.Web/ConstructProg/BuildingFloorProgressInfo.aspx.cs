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
	/// BuildingFloorProgressInfo 的摘要说明。
	/// </summary>
	public partial class BuildingFloorProgressInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputText txtSearchPBSUnitName;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanTitle;
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtPBSUnitCode;
		protected System.Web.UI.WebControls.Label lblPBSUnitName;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnShowPBSUnitWindow;
		protected AspWebControl.Calendar dtSearchReportDateBegin;
		protected AspWebControl.Calendar dtSearchReportDateEnd;
		protected System.Web.UI.HtmlControls.HtmlInputText txtSearchContent;
		protected System.Web.UI.HtmlControls.HtmlInputText txtSearchRiskRemark;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanReportPerson;
		protected System.Web.UI.HtmlControls.HtmlAnchor hrefSelectPerson;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtReportPerson;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtReportPersonName;
		protected System.Web.UI.HtmlControls.HtmlTable tableSearch;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPBSUnitName;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnVisualProgressChange;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
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
				this.txtDefaultVisualProgressName.Value = Request.QueryString["DefaultVisualProgressName"];

				string BuildingCode = this.txtBuildingCode.Value;

				if (this.txtBuildingCode.Value.Trim() != "") 
				{
					string[] arrBuildingCode = BuildingCode.Split(",".ToCharArray());

					if (arrBuildingCode.Length > 1) 
					{
						//多个楼栋
						this.txtMulti.Value = "1";
						this.btnGotoFloor.Visible = false;
						this.btnBatchEdit.Visible = false;

						BLL.PageFacade.LoadBuildingTaskVisualProgressSelect(this.sltVisualProgress, this.txtDefaultVisualProgress.Value, arrBuildingCode, this.txtProjectCode.Value);
					}
					else
					{
						//一个楼栋
						EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingByCode(txtBuildingCode.Value);
						if (entity.HasRecord()) 
						{
							//						this.lblBuildingName.Text = entity.GetString("BuildingName");
							this.txtProjectCode.Value = entity.GetString("ProjectCode");
							//						this.lblFloorCount.Text = entity.GetIntString("IFloorCount");
						}
						else 
						{
							Response.Write(Rms.Web.JavaScript.Alert(true, "楼栋不存在"));
							return;
						}
						entity.Dispose();

						BLL.PageFacade.LoadBuildingTaskVisualProgressSelect(this.sltVisualProgress, this.txtDefaultVisualProgress.Value, this.txtBuildingCode.Value, this.txtProjectCode.Value);
					}
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "未传入楼栋代码"));
					return;
				}

				if ((this.txtDefaultVisualProgressName.Value != "") && (this.sltVisualProgress.Value == "")) 
				{
					int selectIndex = BLL.PageFacade.GetSelectIndexByText(this.sltVisualProgress, this.txtDefaultVisualProgressName.Value);
					if (selectIndex >= 0)
					{
						this.sltVisualProgress.SelectedIndex = selectIndex;
					}
				}

				//权限
				if ( ! user.HasRight("030303")) //修改
				{
					this.btnBatchEdit.Visible = false;
				}

				if ( ! user.HasRight("030304"))	this.btnDelete.Visible = false; //清空
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 重填
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				BLL.ConstructProgRule.DeleteBuildingFloorProgress(this.txtBuildingCode.Value, this.sltVisualProgress.Value);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错：" + ex.Message));
				return;
			}
		}

	}
}
