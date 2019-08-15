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
using RmsPM.DAL.QueryStrategy;
using RmsPM.DAL.EntityDAO;
using Rms.ORMap;
using Rms.Web;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// PayoutBatchModify ��ժҪ˵����
	/// </summary>
	public partial class PayoutBatchModify : System.Web.UI.Page
	{
		protected string ProjectCode = "";
		protected string SelectCode = "";

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			try
			{
				if(!this.IsPostBack)
				{
					this.InitPage();
					this.LoadData();
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��" + ex.Message));
			}
		}

		private void InitPage()
		{
			ProjectCode = Request["ProjectCode"].ToString();
			SelectCode  = Request["SelectCode"].ToString();
		}

		private void LoadData()
		{	
			PayoutStrategyBuilder sb = new PayoutStrategyBuilder("V_Payout");
			sb.AddStrategy( new Strategy( PayoutStrategyName.ProjectCode,ProjectCode));


			string sql = sb.BuildMainQueryString();

			QueryAgent qa = new QueryAgent();
			EntityData entity = qa.FillEntityData( "V_Payout",sql );
			qa.Dispose();

			this.dgDetailData.DataSource = entity;
			this.dgDetailData.DataBind();


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

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{				
				EntityData entity;

				for(int i=0;i<this.dgDetailData.Items.Count;i++)
				{
					string txtSupplierCode = ((TextBox)this.dgDetailData.Items[i].FindControl("txtSupplierCode")).Text.Trim();
					string txtName = ((TextBox)this.dgDetailData.Items[i].FindControl("txtName")).Text.Trim();
				
					string Code = this.dgDetailData.DataKeys[i].ToString();
					entity = DAL.EntityDAO.PaymentDAO.GetPayoutByCode(Code);
					DataRow dr = entity.CurrentRow;

					dr["Payer"] = txtName;
					dr["SupplyCode"] = txtSupplierCode;

					DAL.EntityDAO.PaymentDAO.UpdatePayout(entity);
					entity.Dispose();
				}

				//ˢ�¸�����
				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.opener.document.all.btnSearch.submit();");
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��" + ex.Message));
			}
		}

	}
}
