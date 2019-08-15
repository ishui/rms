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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// FinanceInterfaceAnalysisUserList ��ժҪ˵����
	/// </summary>
	public partial class FinanceInterfaceAnalysisUserList : PageBase
	{

		protected System.Web.UI.HtmlControls.HtmlInputText txtVoucherID;
		protected System.Web.UI.HtmlControls.HtmlSelect sltAccountant;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAdd;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spanFinanceInterface;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadDataGrid();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtSubjectSetCode.Value = Request.QueryString["SubjectSetCode"];
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadDataGrid()
		{
			try
			{
				SystemUserSubjectSetStrategyBuilder sb = new SystemUserSubjectSetStrategyBuilder();
				sb.AddStrategy( new Strategy( SystemUserSubjectSetStrategyName.SubjectSetCode, txtSubjectSetCode.Value));

//				sb.AddOrder( "SortID", true);
				sb.AddOrder( "UserName", true);
				string sql = sb.BuildQueryViewString();

				QueryAgent qa = new QueryAgent();
				DataTable tb = qa.ExecSqlForDataSet(sql).Tables[0];
				qa.Dispose();

				this.dgList.DataSource = tb;
				this.dgList.DataBind();
				tb.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ����" + ex.Message));
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



	}
}
