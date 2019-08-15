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
using RmsPM.DAL.EntityDAO;

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// Budget ��ժҪ˵����
	/// </summary>
	public partial class Budget : PageBase
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( IsPostBack )
				return;

			string budgetCode = Request["BudgetCode"] + "";
			string projectCode = Request["ProjectCode"] + "";

			if ( budgetCode == "" ) 
			{
//				this.lblYear.Text = " ( û���ƶ�Ԥ�� ��) ";
				this.btnBudgetCheck.Visible = false;
				this.btnNewestBudget.Visible = false;
				return;
			}
			else
			{
				try
				{
					EntityData budget = DAL.EntityDAO.CBSDAO.GetBudgetByCode(budgetCode);
					if ( budget.HasRecord())
					{
						int iYear = budget.GetInt("IYear");
						int iMonth =  budget.GetInt("IMonth");
						int afterPeriod =  budget.GetInt("AfterPeriod");
//						this.lblYear.Text = iYear.ToString();
//						this.lblMonth.Text = iMonth.ToString();

						int flag = budget.GetInt("Flag");
						string flagName = "";
						if ( flag == 0 )
							flagName = "����ǰԤ�㣬��Ч�У�";
						else if ( flag == 1)
							flagName = "���ƶ��У�δ��ˣ�";
						else if ( flag == 2 )
							flagName = "������Ԥ�㣩";

						this.lblBudgetName.Text = budget.GetString("BudgetName");
						this.lblRemark.Text = budget.GetString("Remark");
						this.lblStatus.Text	= flagName ;
						this.lblMakePersonName.Text = BLL.SystemRule.GetUserName( budget.GetString("MakePerson"));

						this.lblCheckDate.Text = budget.GetDateTimeOnlyDate("CheckDate");
						this.lblCheckPersonName.Text = BLL.SystemRule.GetUserName( budget.GetString("CheckPerson"));

						this.lblAfterPeriod.Text = afterPeriod.ToString();
						
						int pMonth = budget.GetInt("PeriodMonth");


						int endMonth = iMonth + pMonth - 1;
						int endYear = iYear;
						if ( endMonth > 12 )
						{
							endYear = endYear + 1;
							endMonth = endMonth - 12;
						}

						int startAfterPeriod = iYear+1;
						int endAfterPerod = iYear + afterPeriod;
						int endPeriod = iYear + afterPeriod;

						this.lblPeriodMonth.Text = iYear.ToString() + "��" + iMonth.ToString() + "�� �� "  + endYear.ToString() + "��" + endMonth.ToString() + "��" ;
						this.lblAfterPeriod.Text = startAfterPeriod.ToString() + "�� �� " + endPeriod.ToString() + "��";


						if ( flag == 1 )
						{
							this.btnNewestBudget.Visible = false;
							this.btnNewBudget.Visible = false;
						}
						else
						{
							this.btnBudgetCheck.Visible = false;

							// ����Ƿ�����������Ԥ��
							BudgetStrategyBuilder sb = new BudgetStrategyBuilder();
							sb.AddStrategy( new Strategy( BudgetStrategyName.ProjectCode,projectCode ) );
							sb.AddStrategy( new Strategy( BudgetStrategyName.IsDynamic,"0" ) );
							sb.AddStrategy ( new Strategy( BudgetStrategyName.Flag , "1"  ) );
							string sql = sb.BuildMainQueryString();
							QueryAgent qa = new QueryAgent();
							EntityData budgets = qa.FillEntityData("Budget",sql);
							qa.Dispose();
							if ( budgets.HasRecord() )
							{
								this.btnNewBudget.Visible = false;
								string budgetNewestCode = budgets.GetString("BudgetCode");
								this.btnNewestBudget.Attributes.Add("onclick",@"gotoBudget('" + budgetNewestCode + @"'); return false;");
							}
							else
								this.btnNewestBudget.Visible = false;
							budgets.Dispose();

						}
					}

				}
				catch(Exception ex)
				{
					ApplicationLog.WriteLog(this.ToString(),ex,"");
				}
			}

			if ( !user.HasRight("040302"))
				this.btnNewBudget.Visible = false;

			if ( !user.HasRight("040303"))
				this.btnBudgetCheck.Visible = false;


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
