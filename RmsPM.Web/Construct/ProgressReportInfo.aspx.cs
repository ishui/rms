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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;
using RmsPM.BLL;

namespace RmsPM.Web.Construct
{
	/// <summary>
	/// ProgressReportInfo 的摘要说明。
	/// </summary>
	public partial class ProgressReportInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable tbToolbar;
		protected System.Web.UI.HtmlControls.HtmlTableRow trA3;
		protected System.Web.UI.HtmlControls.HtmlTableRow trA4;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				IniPage();
			}

			this.myAttachMentList.AttachMentType = "ConstructProgress";
			this.myAttachMentList.MasterCode = this.txtProgressCode.Value;
		}

		private void IniPage()
		{
			try 
			{
				this.txtProgressCode.Value = Request.QueryString["ProgressCode"];
				this.txtAct.Value = Request.QueryString["action"];
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];

				//权限
				this.btnModify.Visible = user.HasRight("020203");
				this.btnDelete.Visible = user.HasRight("020204");

				//风险指数字典
				EntityData entityRiskIndex = DAL.EntityDAO.ConstructDAO.GetAllRiskIndex();
				ViewState["tbRiskIndex"] = entityRiskIndex.CurrentTable;
				entityRiskIndex.Dispose();

				LoadData();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		public DataTable GetRiskIndexDataSource() 
		{
			return (DataTable)ViewState["tbRiskIndex"];
		}

		/// <summary>
		/// 删除
		/// </summary>
		private void DoDelete()
		{
			try
			{
				BLL.ConstructRule.DeleteConstructProgressReport(this.txtProgressCode.Value);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"删除进度报告出错");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除进度报告出错：" + ex.Message));
				return;
			}

			GoBack();
		}

		private void LoadData()
		{
			string code =  this.txtProgressCode.Value;

			try
			{
				if (code != "") 
				{
					EntityData entity = DAL.EntityDAO.ConstructDAO.GetConstructProgressByCode(code);

					if ( entity.HasRecord())
					{
						this.txtPBSUnitCode.Value = entity.GetString("PBSUnitCode");
						this.txtProjectCode.Value = entity.GetString("ProjectCode");

						this.lblReportDate.Text = entity.GetDateTimeOnlyDate("ReportDate");
						this.txtReportPersonCode.Value = entity.GetString("ReportPerson");
						this.lblReportPersonName.Text = RmsPM.BLL.SystemRule.GetUserName(entity.GetString("ReportPerson"));
						this.lblVisualProgress.Text = BLL.ConstructRule.GetVisualProgressName(entity.GetString("VisualProgress"));
						this.lblContent.Text = entity.GetString("Content").Replace("\n", "<br>");
						this.lblRiskRemark.Text = entity.GetString("RiskRemark").Replace("\n", "<br>");

						this.lblCurrentLayer.Text = BLL.MathRule.GetIntShowString(entity.GetInt("CurrentLayer"));
					}
					else
					{
						this.btnDelete.Visible = false;

						Response.Write(Rms.Web.JavaScript.Alert(true, "进度报告不存在"));
						return;
//						Response.End();
					}

					entity.Dispose();

					EntityData entityU = DAL.EntityDAO.PBSDAO.GetPBSUnitByCode(txtPBSUnitCode.Value);
					if (entityU.HasRecord()) 
					{
						this.lblPBSUnitName.Text = entityU.GetString("PBSUnitName");
					}
					entityU.Dispose();

					LoadDataGridRisk();

				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "进度报告编号不能为空"));
					return;
//					Response.End();
				}

				if (this.txtFromUrl.Value.Trim() == "") 
				{
					this.txtFromUrl.Value = "ConstructPlanInfo.aspx?ProjectCode=" + this.txtProjectCode.Value + "&PBSUnitCode=" + this.txtPBSUnitCode.Value;
				}

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载进度失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载进度失败：" + ex.Message));
			}
		}

		/// <summary>
		/// 显示风险类型明细
		/// </summary>
		private void LoadDataGridRisk()
		{
			try
			{
				string ProgressCode = this.txtProgressCode.Value;
				DataTable tb = BLL.ConstructRule.GenerateConstructProgressRiskTable(ProgressCode, false);
				this.dgRisk.DataSource = tb;
				this.dgRisk.DataBind();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载风险类型明细失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载风险类型明细失败：" + ex.Message));
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
			this.dgRisk.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgRisk_ItemDataBound);

		}
		#endregion

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
//			Response.Write(string.Format("window.location.href = '{0}';", this.txtFromUrl.Value));
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write("if (window.opener.opener) window.opener.opener.location = window.opener.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			DoDelete();
		}

		private void dgRisk_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) 
			{
				RadioButtonList rdoRiskIndexCode = (RadioButtonList)e.Item.FindControl("rdoRiskIndexCode");
				HtmlInputHidden txtRiskIndexCode = (HtmlInputHidden)e.Item.FindControl("txtRiskIndexCode");
				foreach(ListItem item in rdoRiskIndexCode.Items) 
				{
					item.Selected = (item.Value == txtRiskIndexCode.Value);
				}
			}
		}

	}
}
