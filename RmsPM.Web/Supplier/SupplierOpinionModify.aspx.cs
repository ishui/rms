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
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.Web;

namespace RmsPM.Web.Supplier
{
	/// <summary>
	/// SupplierOpinionModify ��ժҪ˵����
	/// </summary>
	public partial class SupplierOpinionModify : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
			if (!IsPostBack)
			{
				LoadData();
				

			}
		}

		private void LoadData()
		{
			string supplierOpinionCode = Request.QueryString["SupplierOpinionCode"] + "";
			string supplierCode = Request["SupplierCode"] + "";
			this.SupplierName.Text = BLL.ProjectRule.GetSupplierName(supplierCode);

			if ( supplierOpinionCode == "" )
			{
				this.btnDelete.Visible=false;
				return;
			}

			try
			{
				EntityData entity=ProjectDAO.GetSupplierOpinionByCode(supplierOpinionCode);
				if (entity.HasRecord())
				{
					OpinionPerson.Text=entity.GetString("OpinionPerson");
					Event.Text=entity.GetString("Event");
					Opinion.Text=entity.GetString("Opinion");
					dtOpinionDate.Value=entity.GetDateTimeOnlyDate("OpinionDate");
			
				}
				entity.Dispose();
			}
			catch ( Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
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


		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			string supplierOpinionCode = Request.QueryString["SupplierOpinionCode"] + "";
			string supplierCode = Request["SupplierCode"] + "";

			bool isNew = ( supplierOpinionCode == "" );

			try
			{
				EntityData entity = null;
				DataRow dr = null;

				if ( isNew)
				{
					entity = new EntityData("SupplierOpinion");
					supplierOpinionCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("supplierOpinionCode");
					dr = entity.GetNewRecord();
					dr["supplierOpinionCode"]=supplierOpinionCode;
					dr["supplierCode"]=supplierCode;
				}
				else
				{
					entity=ProjectDAO.GetSupplierOpinionByCode(supplierOpinionCode);
					dr = entity.CurrentRow;
				}
				dr["OpinionPerson"]=OpinionPerson.Text;
				dr["Event"]=Event.Text;
				dr["Opinion"]=Opinion.Text;
				if ( dtOpinionDate.Value != "" )
					dr["OpinionDate"]=this.dtOpinionDate.Value;
				else
					dr["OpinionDate"]=System.DBNull.Value;

				if ( isNew )
				{
					entity.AddNewRecord(dr);
					DAL.EntityDAO.ProjectDAO.InsertSupplierOpinion(entity);
				}
				else
				{
					DAL.EntityDAO.ProjectDAO.UpdateSupplierOpinion(entity);
              
				}

				entity.Dispose();

				CloseWindow();

			}
			catch ( Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		
		}

		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			string supplierOpinionCode = Request.QueryString["SupplierOpinionCode"] + "";
			if ( supplierOpinionCode == "" )
				return;

			try
			{
				EntityData entity = ProjectDAO.GetSupplierOpinionByCode(supplierOpinionCode);
				DAL.EntityDAO.ProjectDAO.DeleteSupplierOpinion(entity);
				entity.Dispose();
				CloseWindow();
			}
			catch ( Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		
		}

		private void CloseWindow()
		{
			string supplierCode = Request["SupplierCode"]+"";
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write( "window.opener.navigate('SupplierInfo.aspx?Page=1&SupplierCode="+supplierCode +"');"  );
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

	}
}
