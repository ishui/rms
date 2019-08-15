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
	/// MaterialCostInfo ��ժҪ˵����
	/// </summary>
	public partial class MaterialCostInfo :PageBase
	{
		protected System.Web.UI.WebControls.Label lblU8Code;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnNew;

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
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

				// Ȩ��
				if ( !ar.Contains("141103"))
					this.btnModify.Visible = false;

				if ( !ar.Contains("141104"))
					this.btnDelete.Visible = false;

			}
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		private void LoadData()
		{
			try
			{
				//������Ϣ
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

                    if (this.lblGroupName.Text.StartsWith("ϵ������"))
                    {
                        this.lblPriceTitle.Text = "����";
                    }

				}
                entity.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"����ҳ�����");
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������" + ex.Message));
			}
		}
	}
}
