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
	/// Ԥ����Ϣ ��ժҪ˵����
	/// </summary>
	public partial class DynamicCostApplyList : PageBase
	{

		private const int IMaxMonth = 12;
		private const int IMaxPeriod = 10;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				BuildSearchString();
				LoadData();
			}
		}

		private void IniPage()
		{
			this.dtCheckDate0.Value = "";
			this.dtCheckDate1.Value = "";
			this.dtMakeDate0.Value = "";
			this.dtMakeDate1.Value = "";
		}


		private void BuildSearchString()
		{
			string projectCode = Request["ProjectCode"] + "";
			BudgetStrategyBuilder sb = new BudgetStrategyBuilder();
			sb.AddStrategy( new Strategy( BudgetStrategyName.ProjectCode,projectCode) );
			sb.AddStrategy( new Strategy( BudgetStrategyName.IsDynamic,"2") );					//��̬����
			if ( this.sltFlag.Value != "" )
				sb.AddStrategy( new Strategy( BudgetStrategyName.Flag,this.sltFlag.Value) );		//�������״̬


			ArrayList ar = new ArrayList();
				
			if ( this.dtMakeDate0.Value != "" || this.dtMakeDate1.Value !="" )
			{
				ar.Add(this.dtMakeDate0.Value);
				ar.Add(this.dtMakeDate1.Value);
				sb.AddStrategy( new Strategy( BudgetStrategyName.MakeDate,ar) );
			}

			ar.Clear();
			if ( this.dtCheckDate0.Value !="" || this.dtCheckDate1.Value != "" )
			{
				ar.Add(this.dtCheckDate0.Value);
				ar.Add(this.dtCheckDate1.Value);
				sb.AddStrategy( new Strategy( BudgetStrategyName.CheckDate,ar) );
			}

			sb.AddOrder("MakeDate",false);
				
			string sql = sb.BuildMainQueryString();
			this.ViewState.Add("SearchString",sql);
		}



		private void LoadData()
		{
			
			try
			{
				string sql = (string)this.ViewState["SearchString"];
				QueryAgent qa = new QueryAgent();
				EntityData budgets =qa.FillEntityData( "Budget",sql);
				qa.Dispose();

				budgets.CurrentTable.Columns.Add("FlagName");
				foreach ( DataRow dr in budgets.CurrentTable.Rows)
				{
					int flag = (int)dr["Flag"];
					switch (flag)
					{
						case 0:		//û�����״̬
							dr["FlagName"] = "��Ч";
							break;
						case 1:
							dr["FlagName"] = "δ���";
							break;
						case 2:
							dr["FlagName"] = "���ͨ��";
							break;
						case 3:
							dr["FlagName"] = "����";
							break;
					}
				}

				this.dgList.DataSource = budgets.CurrentTable;
				this.dgList.DataBind();
				budgets.Dispose();

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
			this.dgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgList_PageIndexChanged);

		}
		#endregion

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			BuildSearchString();
			LoadData();
		}

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			int index = e.NewPageIndex;
			this.dgList.CurrentPageIndex = index;
			LoadData();
		}


	}
}
