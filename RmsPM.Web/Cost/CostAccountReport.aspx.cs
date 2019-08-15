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
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// �ɱ����öԱȱ� ��ժҪ˵����
	/// </summary>
	public partial class CostAccountReport : PageBase
	{
		EntityData cbs = null;							// �ɱ��ṹ
		string m_EndDate = "";							// �������ʱ��
		decimal m_TotalArea = decimal.Zero;				// ��Ŀ�����

		protected System.Web.UI.HtmlControls.HtmlTable tbReport;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			LoadData();
		}

		private void LoadData()
		{
			string projectCode = Request["ProjectCode"] + "";

			m_EndDate = Request["EndDate"] + "";
			if ( m_EndDate == "" )
				m_EndDate = DateTime.Now.Date.ToString("yyyy-MM-dd");

			string unitText = Request["UnitText"] + "";
			string reportDate = Request["ReportDate"] + "";

			try
			{
				this.tdTitle.InnerHtml = RmsPM.BLL.ProjectRule.GetProjectName(projectCode) +"�ɱ����öԱȱ�";
				this.m_TotalArea = BLL.ProductRule.GetBuildingArea("",projectCode);

				DataTable dt = new DataTable();
				dt.Columns.Add("CostCode");
				dt.Columns.Add("CostName");
				dt.Columns.Add("AHMoney");
				dt.Columns.Add("APMoney");
				dt.Columns.Add("AverageAHMoney");
				dt.Columns.Add("AverageAPMoney");

				cbs = DAL.EntityDAO.CBSDAO.GetCBSByProject( projectCode);

				DataRow[] drs = cbs.CurrentTable.Select( " Deep<=2 "," FullCode");
				int iCount = drs.Length;
				for ( int i=0;i<iCount; i++)
				{
					DataRow dr = drs[i];
					DataRow drNew = dt.NewRow();

					int deep = (int)dr["Deep"];
					string costCode = (string)dr["CostCode"];
					string costName = (string)dr["CostName"];

					string sTemp = "";
					for ( int j =0; j<deep-1; j++ )
						sTemp += @"��";
					drNew["CostName"] = sTemp;

					decimal ahMoney = BLL.CBSRule.GetContractAllocationCost(costCode,"","","","");
					decimal apMoney = BLL.CBSRule.GetAPCost(costCode,"",m_EndDate);

					drNew["AHMoney"] = BLL.StringRule.BuildShowNumberString(ahMoney);
					drNew["APMoney"] = BLL.StringRule.BuildShowNumberString(apMoney);

					if ( ! BLL.MathRule.CheckDecimalEqual(m_TotalArea,decimal.Zero))
					{
						drNew["AverageAHMoney"] = BLL.StringRule.BuildShowNumberString(ahMoney/m_TotalArea);
						drNew["AverageAPMoney"] = BLL.StringRule.BuildShowNumberString(apMoney/m_TotalArea);
					}

					dt.Rows.Add(drNew);
				}

				this.repeatList.DataSource = dt;
				this.repeatList.DataBind();

				dt.Dispose();

				cbs.Dispose();
			}
			catch ( Exception ex )
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
	}
}
