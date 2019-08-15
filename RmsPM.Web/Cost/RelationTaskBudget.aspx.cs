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
	/// RelationTaskBudget ��ժҪ˵����
	/// </summary>
	public partial class RelationTaskBudget : PageBase
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
			string costCode = Request["CostCode"]+"";
			this.lblCostName.Text = BLL.CBSRule.GetCostName(costCode);
		}

		private void LoadData()
		{
			string costCode = Request["CostCode"]+"";
			try
			{

				TaskBudgetStrategyBuilder sb = new TaskBudgetStrategyBuilder();
				ArrayList ar = new ArrayList();
				ar.Add(costCode);
				ar.Add("0");
				sb.AddStrategy( new Strategy( TaskBudgetStrategyName.CostCodeEx,ar ));
				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "TaskBudget", sb.BuildMainQueryString());
				qa.Dispose();
				this.dgTaskBudget.DataSource = entity.CurrentTable;
				this.dgTaskBudget.DataBind();
				entity.Dispose();

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
