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
using Rms.Web;

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// PBSUnitModify 的摘要说明。
	/// </summary>
	public partial class PBSUnitInfo : PageBase
	{
	
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
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtPBSUnitCode.Value = Request.QueryString["PBSUnitCode"];
				string PBSUnitCode = this.txtPBSUnitCode.Value.Trim();
				this.txtAct.Value = Request.QueryString["Action"];

				//查看模式时，不可“修改”、“删除”
				switch (this.txtAct.Value.ToLower()) 
				{
					case "view":
						this.trToolBar.Style["display"] = "none";
						break;

					default:
						break;
				}

				//权限
				this.btnModify.Visible = base.user.HasRight("010403");
				this.btnDelete.Visible = base.user.HasRight("010404");

				if (PBSUnitCode != "") 
				{
					EntityData entity = DAL.EntityDAO.PBSDAO.GetPBSUnitByCode(PBSUnitCode);
					if (entity.HasRecord()) 
					{
						DataRow dr = entity.CurrentRow;

						this.txtProjectCode.Value = entity.GetString("ProjectCode");

						this.lblPBSUnitName.Text = entity.GetString("PBSUnitName");
						this.lblPInvest.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(dr["PInvest"]), "万元");
						this.lblInvest.Text = BLL.StringRule.AddUnit(BLL.MathRule.GetDecimalShowString(dr["Invest"]), "万元");
						this.lblPStartDate.Text = entity.GetDateTimeOnlyDate("PStartDate");
						this.lblPEndDate.Text = entity.GetDateTimeOnlyDate("PEndDate");
						this.lblStartDate.Text = entity.GetDateTimeOnlyDate("StartDate");
						this.lblEndDate.Text = entity.GetDateTimeOnlyDate("EndDate");
						this.lblVisualProgress.Text = BLL.ConstructRule.GetVisualProgressName(entity.GetString("VisualProgress"));
						this.lblRemark.Text = entity.GetString("Remark").Replace("\n", "<br>");
						this.lblUFCode.Text = entity.GetString("UFCode");
						this.lblManagerName.Text = BLL.SystemRule.GetUserName(entity.GetString("Manager"));

						this.lblConstructUnit.Text = entity.GetString("ConstructUnit");
//						this.lblDevelopUnit.Text = entity.GetString("DevelopUnit");

					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "单位工程不存在"));
						return;
//						Response.End();
					}
					entity.Dispose();

					//取楼栋列表
					EntityData entityBuilding = DAL.EntityDAO.ProductDAO.GetBuildingByPBSUnitCode(PBSUnitCode);
					this.dgList.Columns[1].FooterText = BLL.StringRule.BuildShowNumberString(BLL.MathRule.SumColumn(entityBuilding.CurrentTable,"Area"));
					this.dgList.DataSource = entityBuilding.CurrentTable;
					this.dgList.DataBind();
					entityBuilding.Dispose();

				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "单位工程不存在"));
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			string FromUrl = this.txtFromUrl.Value.Trim();
			if (FromUrl != "") 
			{
				Response.Write(string.Format("window.location = '{0}';", FromUrl));
			}
			else 
			{
				Response.Write("window.location = window.location;");
			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				string code = this.txtPBSUnitCode.Value.Trim();
				BLL.PBSRule.DeletePBSUnit(code);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错：" + ex.Message));
				return;
			}

			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write(string.Format("parent.location = 'PBSUnitFrame.aspx?ProjectCode={0}';", this.txtProjectCode.Value));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

	}
}
