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
using RmsPM.DAL.QueryStrategy;



namespace RmsPM.Web.Cost
{
	/// <summary>
	/// DynamicCostModifyList ��ժҪ˵����
	/// </summary>
	public partial class DynamicCostModifyList : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{

			string projectCode = Request["ProjectCode"] + "";
			string inputCostCode = Request["CostCode"] + "";
			try
			{
				EntityData cbs = DAL.EntityDAO.CBSDAO.GetCBSByProject(projectCode);
				foreach ( DataRow dr in cbs.CurrentTable.Select( "ChildCount=0","SortID"  ))
				{
					string costCode = (string)dr["CostCode"];
					string costName = (string)dr["CostName"];
					this.sltCost.Items.Add( new ListItem( costName,costCode));
				}

				cbs.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}

			this.dtCheckDate0.Value = "";
			this.dtCheckDate1.Value = "";
			this.dtMakeDate0.Value = "";
			this.dtMakeDate1.Value = "";

			if ( inputCostCode != "" )
			{
				this.sltCost.Value = inputCostCode;
				this.sltFlag.Value = "";
			}

		}




		private void LoadData()
		{
			string projectCode = Request["ProjectCode"] + "";
			try
			{
				V_BudgetCostStrategyBuilder sb = new V_BudgetCostStrategyBuilder();
				sb.AddStrategy( new Strategy( V_BudgetCostStrategyName.ProjectCode,projectCode) );
				sb.AddStrategy( new Strategy( V_BudgetCostStrategyName.IsDynamic,"2") );					//��̬����
				if ( this.sltFlag.Value != "" )
					sb.AddStrategy( new Strategy( V_BudgetCostStrategyName.Flag,this.sltFlag.Value) );		//�������״̬

				ArrayList ar = new ArrayList();
				if ( this.dtMakeDate0.Value != "" || this.dtMakeDate1.Value !="" )
				{
					ar.Add(this.dtMakeDate0.Value);
					ar.Add(this.dtMakeDate1.Value);
					sb.AddStrategy( new Strategy( V_BudgetCostStrategyName.MakeDate,ar) );
				}

				ArrayList ar1 = new ArrayList();
				if ( this.dtCheckDate0.Value !="" || this.dtCheckDate1.Value != "" )
				{
					ar1.Add(this.dtCheckDate0.Value);
					ar1.Add(this.dtCheckDate1.Value);
					sb.AddStrategy( new Strategy( V_BudgetCostStrategyName.CheckDate,ar1) );
				}

				if ( this.sltCost.Value !="")
					sb.AddStrategy( new Strategy( V_BudgetCostStrategyName.CostCode,this.sltCost.Value) );

				sb.AddOrder("MakeDate",false);
				
				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData budgets =qa.FillEntityData( "V_BudgetCost",sql);
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

		}
		#endregion

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			LoadData();
		}


	}
}
