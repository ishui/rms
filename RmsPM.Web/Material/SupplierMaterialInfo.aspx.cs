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
	/// SupplierMaterialInfo ��ժҪ˵����
	/// </summary>
	public partial class SupplierMaterialInfo :PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnNew;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
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

				// Ȩ��
				if ( !ar.Contains("141303"))
					this.btnModify.Visible = false;

				if ( !ar.Contains("141304"))
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
				ApplicationLog.WriteLog(this.ToString(),ex,"����ҳ�����");
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������" + ex.Message));
			}
		}
	}
}
