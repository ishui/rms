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

using System.Text;

using Rms.ORMap;
using RmsPM.DAL;
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// CostContrast ��ժҪ˵����
	/// </summary>
	public partial class CostContrast : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				LoadData();
			}
		}

		// costCode == ""ʱ�� ���ܳɱ�
		private void LoadData()
		{
			string costCode = Request["CostCode"] + "";
			string projectCode = Request["ProjectCode"] + "";

			try
			{

				if ( costCode == "")
					this.lblCostName.Text = "�ܳɱ�";
				else
					this.lblCostName.Text = BLL.CBSRule.GetCostName(costCode);


				DateTime tStart = DateTime.Today;		//����ͼ�����ʼʱ��
				DateTime tEnd = DateTime.Today;			//����ͼ��Ľ���ʱ�䣬 ����ȷ��ͼ���ʱ����
				decimal maxValue = decimal.Zero;		//����ͼ���е������ֵ�� ����ȷ��ͼ��ĸ߶�

				EntityData estimateCheck = RmsPM.DAL.EntityDAO.CBSDAO.GetCostEstimateCheckByCode(projectCode);
				
				if ( ! estimateCheck.HasRecord())
				{
					Response.Write(Rms.Web.JavaScript.ScriptStart);
					Response.Write(Rms.Web.JavaScript.Alert(false,"��û�����ù��㣬�������ù��� ��"));
					Response.Write(Rms.Web.JavaScript.WinClose(false));
					Response.Write(Rms.Web.JavaScript.ScriptEnd);
					return;
				}

				tStart = DateTime.Parse(estimateCheck.GetDateTimeOnlyDate("CheckDate"));
				estimateCheck.Dispose();
				this.lblDate.Text = "ʱ�䣺 �� " + tStart.ToString("yyyy-MM-dd") + " �� " + tEnd.ToString("yyyy-MM-dd");

				// �������
				decimal ce = BLL.CBSRule.SumTotalEstimateCost(costCode,projectCode);

				TimeSpan ts = tEnd - tStart ;
				int Length = Math.Abs(ts.Days)+1;

				string [] CEs = new string[ Length ];			//����ɱ�����
				string [] Bs = new string[ Length ];			//Ԥ��ɱ�����
				string [] Ds = new string[ Length ];			//��̬�ɱ�����

				//���ó�ֵ
				for ( int i =0 ; i< Length ; i++)
				{
					CEs[i] = ce.ToString();
					Bs[i]=ce.ToString();
					Ds[i]=ce.ToString();
				}

				string sql = "";
				int iCount = 0;
				TimeSpan tSpan  ;
				int iS = 0;
				decimal dTemp = decimal.Zero;
				DateTime checkDate ;
				QueryAgent qa = new QueryAgent();
				// ��������BudgetCost����
				if ( costCode != "" )
				{
					// Ԥ�� , �Լ���Ȼ���ڵĶ�̬
					V_BudgetCostStrategyBuilder sb = new V_BudgetCostStrategyBuilder();
					sb.AddStrategy( new Strategy(V_BudgetCostStrategyName.ProjectCode,projectCode) );
					sb.AddStrategy( new Strategy(V_BudgetCostStrategyName.IsDynamic,"0") );
					sb.AddStrategy( new Strategy(V_BudgetCostStrategyName.Flag,"0,2") );
					sb.AddStrategy( new Strategy(V_BudgetCostStrategyName.CostCode,costCode) );
					sb.AddOrder("CheckDate",true);
					sql = sb.BuildMainQueryString();
					EntityData yuBudgetCost = qa.FillEntityData("V_BudgetCost",sql);
					iCount = yuBudgetCost.CurrentTable.Rows.Count;
					for ( int i=0;i<iCount;i++)
					{
						yuBudgetCost.SetCurrentRow(i);
						dTemp = yuBudgetCost.GetDecimal("BudgetCost");
						if ( dTemp > maxValue )
							maxValue = dTemp;
						checkDate = DateTime.Parse(yuBudgetCost.GetDateTimeOnlyDate("CheckDate"));
						tSpan = checkDate - tStart;
						iS = tSpan.Days ;
						for ( int j = iS;j<Length;j++)
						{
							Bs[j] = dTemp.ToString();
							Ds[j] = dTemp.ToString();
						}
						
					}

					// ��̬�޸�����
					sb = new V_BudgetCostStrategyBuilder();
					sb.AddStrategy( new Strategy(V_BudgetCostStrategyName.ProjectCode,projectCode) );
					sb.AddStrategy( new Strategy(V_BudgetCostStrategyName.IsDynamic,"2") );
					sb.AddStrategy( new Strategy(V_BudgetCostStrategyName.Flag,"2") );
					sb.AddStrategy( new Strategy(V_BudgetCostStrategyName.CostCode,costCode) );
					sb.AddOrder("CheckDate",true);

					sql = sb.BuildMainQueryString();
					yuBudgetCost = qa.FillEntityData("V_BudgetCost",sql);
					iCount = yuBudgetCost.CurrentTable.Rows.Count;
					for ( int i=0;i<iCount;i++)
					{
						yuBudgetCost.SetCurrentRow(i);
						dTemp = yuBudgetCost.GetDecimal("BudgetCost");
						if ( dTemp > maxValue )
							maxValue = dTemp;
						checkDate = DateTime.Parse(yuBudgetCost.GetDateTimeOnlyDate("CheckDate"));
						tSpan = checkDate - tStart;
						iS = tSpan.Days ;
						for ( int j = iS;j<Length;j++)
						{
							Ds[j] = dTemp.ToString();
						}
					}
					yuBudgetCost.Dispose();

				}
					// �ܷ���
				else
				{
					// Ԥ�� , �Լ���Ȼ���ڵĶ�̬
					BudgetStrategyBuilder sb = new BudgetStrategyBuilder();
					sb.AddStrategy( new Strategy(BudgetStrategyName.ProjectCode,projectCode) );
					sb.AddStrategy( new Strategy(BudgetStrategyName.IsDynamic,"0") );
					sb.AddStrategy( new Strategy(BudgetStrategyName.Flag,"0,2") );
					sb.AddOrder("CheckDate",true);
					sql = sb.BuildMainQueryString();
					EntityData budget = qa.FillEntityData("Budget",sql);
					iCount = budget.CurrentTable.Rows.Count;
					for ( int i=0;i<iCount;i++)
					{
						budget.SetCurrentRow(i);
						dTemp = budget.GetDecimal("TotalMoney");
						if ( dTemp > maxValue )
							maxValue = dTemp;
						checkDate = DateTime.Parse(budget.GetDateTimeOnlyDate("CheckDate"));
						tSpan = checkDate - tStart;
						iS = tSpan.Days ;
						for ( int j = iS;j<Length;j++)
						{
							Bs[j] = dTemp.ToString();
							Ds[j] = dTemp.ToString();
						}
					}

					// ��̬�޸�����
					sb = new BudgetStrategyBuilder();
					sb.AddStrategy( new Strategy(BudgetStrategyName.ProjectCode,projectCode) );
					sb.AddStrategy( new Strategy(BudgetStrategyName.IsDynamic,"2") );
					sb.AddStrategy( new Strategy(BudgetStrategyName.Flag,"2") );
					sb.AddOrder("CheckDate",true);

					sql = sb.BuildMainQueryString();
					budget = qa.FillEntityData("Budget",sql);
					iCount = budget.CurrentTable.Rows.Count;
					for ( int i=0;i<iCount;i++)
					{
						budget.SetCurrentRow(i);
						dTemp = budget.GetDecimal("TotalMoney");
						if ( dTemp > maxValue )
							maxValue = dTemp;
						checkDate = DateTime.Parse(budget.GetDateTimeOnlyDate("CheckDate"));
						tSpan = checkDate - tStart;
						iS = tSpan.Days ;
						for ( int j = iS;j<Length;j++)
						{
							Ds[j] = dTemp.ToString();
						}
					}
					budget.Dispose();
				}

				qa.Dispose();

//
//				//�����ַ���
				string cString ="";
				string bString ="";
				string dString ="";
				for ( int i =0 ; i<Length; i++)
				{
					if ( i != 0 )
					{
						cString += ",";
						bString += ",";
						dString += ",";
					}

					cString += CEs[i];
					bString += Bs[i];
					dString += Ds[i];

				}

				ShowChar( cString,bString,dString,maxValue , this.tdGraphy );

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"���سɱ����ݴ���");
			}

		}


		

		private void ShowChar( string cString , string bString , string dString , decimal maxValue , HtmlTableCell cell )
		{


			string url = Request.Url.AbsoluteUri;
			int i = url.LastIndexOf("/");
			string path = url.Substring(0,i+1);
			decimal dRange = (maxValue*1.2m);
			string sRange = dRange.ToString();


			StringBuilder sb = new StringBuilder();
			
			sb.Append ( @"<APPLET height=300 width=500 archive=""chart.jar"" codebase=""" + path  +  @"""  code=""com.objectplanet.chart.LineChartApplet""   VIEWASTEXT>");

			sb.Append ( @"<param name=seriesCount value=3>"  );
			sb.Append ( @"<param name=sampleValues_0 value=""" + cString +  @""">"  );
			sb.Append ( @"<param name=sampleValues_1 value=""" + bString +  @""">"  );
			sb.Append ( @"<param name=sampleValues_2 value=""" + dString +  @""">"  );
			sb.Append ( @"<param name=sampleColors value=""black, green, red"">"  );
			sb.Append ( @"<param name=range value="""+ sRange +@""">"  );
			sb.Append ( @"</applet> ");
			cell.InnerHtml=sb.ToString();

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
