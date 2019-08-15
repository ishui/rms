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
using RmsPM.BLL;

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// ������� IsDynamic �� ��ǣ� 0-Ԥ�㣬1 ��̬ �� ȡ����һ����Ч�Ļ���������˵İ汾��s
	/// </summary>
	public partial class BudgetNavigate : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"] + "";
			string isDynamic = Request["IsDynamic"] + "";
			string isDynamicName = ( isDynamic== "0" )? "����Ԥ��":"��̬����";
		
			try
			{
				BudgetStrategyBuilder sb = new BudgetStrategyBuilder();
				sb.AddStrategy( new Strategy( BudgetStrategyName.ProjectCode,projectCode ) );
				sb.AddStrategy( new Strategy( BudgetStrategyName.IsDynamic,isDynamic ) );
				sb.AddStrategy ( new Strategy( BudgetStrategyName.Flag , "0,1"  ) );
				sb.AddOrder( "MakeDate",false );
				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData budget = qa.FillEntityData("Budget",sql);
				qa.Dispose();

				string budgetCode = "";
				// �������˹�����Ч�İ汾��������汾
				// û�оͿ����µİ汾
				DataRow[] drs = budget.CurrentTable.Select( "Flag=0","MakeDate DESC" );
				if (drs.Length>0)
					budgetCode=(string)drs[0]["BudgetCode"];
				else
				{
					if ( budget.HasRecord())
						budgetCode = budget.GetString("BudgetCode");
				}
				budget.Dispose();

				Response.Write(Rms.Web.JavaScript.ScriptStart);
//				if ( budgetCode == "" )
//					Response.Write( Rms.Web.JavaScript.Alert(false,"��û������"+isDynamicName+" ��"   ));

				if ( isDynamic == "0" )
					Response.Write(Rms.Web.JavaScript.PageTo(false,"Budget.aspx?projectCode="+projectCode+"&BudgetCode=" + budgetCode));
				else
					Response.Write(Rms.Web.JavaScript.PageTo(false,"DynamicCost.aspx?projectCode="+projectCode+"&BudgetCode=" + budgetCode));
				Response.Write(Rms.Web.JavaScript.ScriptEnd);

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
