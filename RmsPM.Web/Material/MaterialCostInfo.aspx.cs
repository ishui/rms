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
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using RmsPM.Web;
using Rms.ORMap;


namespace RmsPM.Web.Material
{
	/// <summary>
	/// MaterialCostInfo 的摘要说明。
	/// </summary>
	public partial class MaterialCostInfo :PageBase
	{
		protected System.Web.UI.WebControls.Label lblU8Code;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnNew;

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!IsPostBack)
			{

                this.txtMaterialCostCode.Value = Request["MaterialCostCode"];

                ArrayList ar = user.GetResourceRight(txtMaterialCostCode.Value, "MaterialCost");
				if ( ! ar.Contains("141101"))
				{
					Response.Redirect( "../RejectAccess.aspx" );
					Response.End();
				}

				LoadData();

				// 权限
				if ( !ar.Contains("141103"))
					this.btnModify.Visible = false;

				if ( !ar.Contains("141104"))
					this.btnDelete.Visible = false;

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

		private void LoadData()
		{
			try
			{
				//基本信息
				EntityData entity = DAL.EntityDAO.MaterialDAO.GetMaterialCostByCode(txtMaterialCostCode.Value);
				if ( entity.HasRecord())
				{
					this.lblUnit.Text = entity.GetString("Unit");
                    this.lblPrice.Text = BLL.MathRule.GetDecimalShowString(entity.GetDecimal("Price"), "0.##");
                    this.lblBiddingDate.Text = entity.GetDateTimeOnlyDate("BiddingDate");
                    this.lblProject.Text = entity.GetString("Project");
                    this.lblAreaCode.Text = entity.GetString("AreaCode");

                    this.lblDescription.Text = entity.GetString("Description").Replace("\n", "<br>");
                    this.lblDescriptionEn.Text = entity.GetString("DescriptionEn").Replace("\n", "<br>");

                    this.lblGroupName.Text = BLL.SystemGroupRule.GetSystemGroupFullName(entity.GetString("GroupCode"));
                    this.lblCategory.Text = entity.GetString("Category");

                    if (this.lblGroupName.Text.StartsWith("系数含量"))
                    {
                        this.lblPriceTitle.Text = "含量";
                    }

				}
                entity.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"载入页面出错");
			}
		}

        protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
                BLL.MaterialRule.DeleteMaterialCost(this.txtMaterialCostCode.Value);

				Response.Write(Rms.Web.JavaScript.ScriptStart);
				Response.Write(Rms.Web.JavaScript.OpenerReload(false));
				Response.Write(Rms.Web.JavaScript.WinClose(false));
				Response.Write(Rms.Web.JavaScript.ScriptEnd);

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
                Response.Write(Rms.Web.JavaScript.Alert(true, "删除出错：" + ex.Message));
			}
		}
	}
}
