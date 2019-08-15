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

namespace RmsPM.Web.Contract
{
	/// <summary>
	/// ������غ�ͬ
	/// </summary>
	public partial class MoreContractList : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				LoadDataGrid();
			}
		}

		private void IniPage()
		{

		}

		private void LoadDataGrid()
		{
			
			try
			{
				string projectCode = Request["ProjectCode"]+"";
				string costCode = Request["CostCode"]+"";
				DataTable dtContract = BLL.CBSRule.GetCostRelationContract( costCode,projectCode, 5);
				this.repeatContract.DataSource = dtContract;
				this.repeatContract.DataBind();
				dtContract.Dispose();

			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"���غ�ͬ�б����");
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
