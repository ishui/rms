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
	/// SupplierMaterialInfo 的摘要说明。
	/// </summary>
	public partial class SupplierMaterialInfo :PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnNew;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!IsPostBack)
			{

                this.txtSupplierMaterialCode.Value = Request["SupplierMaterialCode"];

                ArrayList ar = user.GetResourceRight(txtSupplierMaterialCode.Value, "SupplierMaterial");
				if ( ! ar.Contains("141301"))
				{
					Response.Redirect( "../RejectAccess.aspx" );
					Response.End();
				}

				LoadData();

				// 权限
				if ( !ar.Contains("141303"))
					this.btnModify.Visible = false;

				if ( !ar.Contains("141304"))
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
				EntityData entity = DAL.EntityDAO.MaterialDAO.GetV_SupplierMaterialByCode(txtSupplierMaterialCode.Value);
				if ( entity.HasRecord())
				{
                    this.lblSupplierName.Text = entity.GetString("SupplierName");
                    this.lblBrand.Text = entity.GetString("Brand");
                    this.lblModel.Text = entity.GetString("Model");
                    this.lblSpec.Text = entity.GetString("Spec");
                    this.lblNation.Text = entity.GetString("Nation");
                    this.lblAreaCode.Text = entity.GetString("AreaCode");
                    this.lblSampleID.Text = entity.GetString("SampleID");

                    this.lblUnit.Text = entity.GetString("Unit");
                    this.lblPrice.Text = BLL.MathRule.GetDecimalShowString(entity.GetDecimal("Price"), "0.##");

                    this.lblGroupName.Text = BLL.SystemGroupRule.GetSystemGroupFullName(entity.GetString("GroupCode"));

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
                BLL.MaterialRule.DeleteSupplierMaterial(this.txtSupplierMaterialCode.Value);

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
