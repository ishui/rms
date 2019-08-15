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
	/// PayoutApportionAccount ��ժҪ˵����
	/// </summary>
	public partial class PayoutApportionAccount : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				LoadData();
			}
		}

		private void LoadData()
		{
			try
			{
				string projectCode = Request["ProjectCode"]+"";

				BLL.PageFacade.LoadBuildingAreaFieldSelect(this.sltBuildingAreaField, BLL.CostRule.GetApportionAreaField(projectCode));

				//ȡ��Ŀ�ĸ����ܶ�
				this.lblTotalCost.Text = BLL.StringRule.AddUnit(BLL.CostRule.GetTotalCost(projectCode).ToString("N"), "Ԫ");

				/*
				DataTable dt = BLL.CostRule.ApportionAllPayout( projectCode, BLL.CostRule.GetApportionAreaField(projectCode));
				this.dgGridApportion.DataSource = new DataView( dt,"","SortID,BuildingName",DataViewRowState.CurrentRows);
				this.dgGridApportion.Columns[4].FooterText = BLL.MathRule.SumColumn(dt,"ApportionMoney").ToString("N");
				this.dgGridApportion.DataBind();
				dt.Dispose();
				*/

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write( Rms.Web.JavaScript.Alert(true,ex.Message) );
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
			this.btnSave.ServerClick += new System.EventHandler(this.btnSave_ServerClick);
		}
		#endregion



		private void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"]+"";
			try
			{
				//��������ֶ�
				BLL.SystemRule.UpdateProjectConfigValue(projectCode, BLL.SystemRule.m_CostApportionBuildingAreaField, this.sltBuildingAreaField.Value);

				BLL.CostRule.ProjectCostApportion(projectCode, BLL.CostRule.GetApportionAreaField(projectCode));
				CloseWindow();
				//Response.End();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write( Rms.Web.JavaScript.Alert(true,ex.Message) );
			}

		}

		/// <summary>
		/// ����
		/// </summary>
		private void CloseWindow() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write(Rms.Web.JavaScript.OpenerReload(false));
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

	}
}
