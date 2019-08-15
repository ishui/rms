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
	/// ����Ԥ���ƶ���֮�����ʱ����һ�ݵ���̬���ã���Ϊ��̬���õĵ�ǰ�汾��
	/// ԭ�Ȼ�����һ��Ԥ�����ڵĶ�̬���ñ����ʷ�汾��
	/// 
	/// ÿ�ε�����̬���õĵ�ǰ�汾���������м�����¼������һ��Ԥ��Ļ������ƶ���
	/// 
	/// 
	/// </summary>
	public partial class DynamicCost : PageBase
	{


	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( IsPostBack )
				return;

			this.lblToDay.Text = DateTime.Now.ToString("yyyy-MM-dd");
			string budgetCode = Request["BudgetCode"] + "";

			if ( budgetCode == "" ) 
			{
				//this.btnDynamicApplyList.Visible = false;
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


					}

				}
				catch(Exception ex)
				{
					ApplicationLog.WriteLog(this.ToString(),ex,"");
				}
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
