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
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;
using Rms.Web;

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// PBSUnitPlanList ��ժҪ˵����
	/// </summary>
	public partial class PBSUnitPlanList : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
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
			this.dgPlanList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgPlanList_PageIndexChanged);
			this.dgPlanList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgPlanList_ItemDataBound);

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

				//Ȩ��
				this.btnBatchEditPlan.Visible = user.HasRight("020105");
				this.btnDeleteYearPlan.Visible = user.HasRight("020104");
				this.btnNewYearPlan.Visible = user.HasRight("020102");

				LoadPlan();
				LoadDataGridPlan();

				((PBSUnitHintEmpty)this.ucPBSUnitHintEmpty).SetProject(this.txtProjectCode.Value);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadPlan() 
		{
			try 
			{
				//��ǰ���
				DataTable tbYear = DAL.EntityDAO.ConstructDAO.GetConstructAnnualPlanYearByProject(this.txtProjectCode.Value);
				if (tbYear.Rows.Count > 0) 
				{
					DataRow dr = tbYear.Rows[tbYear.Rows.Count-1];
					this.txtIYear.Value = BLL.ConvertRule.ToString(dr["IYear"]);
				}
				//						this.txtIYear.Value = BLL.ConstructRule.GetConstructPlanCurrYearByProject(this.txtProjectCode.Value);
				this.lblIYear.Text = this.txtIYear.Value;

				if (this.txtIYear.Value == "") 
				{
					this.btnNewYearPlan.Style["display"] = "none";
				}
				else 
				{
					this.btnNewYearPlan.Style["display"] = "";
				}

				//�������������ʱ������ɾ����ǰ���
				if (tbYear.Rows.Count <= 1) 
				{
					this.btnDeleteYearPlan.Style["display"] = "none";
				}
				else 
				{
					this.btnDeleteYearPlan.Style["display"] = "";
				}

				//ȱʡΪ����
				if (this.lblIYear.Text == "") 
				{
					this.lblIYear.Text = DateTime.Today.Year.ToString();
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadDataGridPlan() 
		{
			try 
			{
				ConstructAnnualPlanStrategyBuilder sb = new ConstructAnnualPlanStrategyBuilder("V_ConstructAnnualPlan");

				string ProjectCode = this.txtProjectCode.Value;
				if (ProjectCode != "")
					sb.AddStrategy( new Strategy( ConstructAnnualPlanStrategyName.ProjectCode, ProjectCode));

				int IYear = BLL.ConvertRule.ToInt(this.txtIYear.Value);
				sb.AddStrategy(new Strategy(ConstructAnnualPlanStrategyName.IYear, IYear.ToString()));

				//				string PBSUnitCode = this.txtSearchPBSUnitCode.Value.Trim();
				//				if (PBSUnitCode != "")
				//					sb.AddStrategy(new Strategy(PBSUnitStrategyName.PBSUnitCode, PBSUnitCode));

				string PBSUnitName = this.txtSearchPBSUnitName.Value.Trim();
				if (PBSUnitName != "")
					sb.AddStrategy(new Strategy(ConstructAnnualPlanStrategyName.PBSUnitName, PBSUnitName));

				sb.AddOrder("PBSUnitName", true);

				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "V_ConstructAnnualPlan",sql );
				qa.Dispose();

				string[] arrField = {"TotalBuildArea", "PTotalInvest", "InvestBefore", "PInvest", "LCFArea"};
				decimal[] arrValue = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);
				this.txtSumTotalBuildArea.Value = BLL.MathRule.GetDecimalShowString(arrValue[0]);
				this.txtSumPTotalInvest.Value = BLL.MathRule.GetDecimalShowString(arrValue[1]);
				this.txtSumInvestBefore.Value = BLL.MathRule.GetDecimalShowString(arrValue[2]);
				this.txtSumPInvest.Value = BLL.MathRule.GetDecimalShowString(arrValue[3]);
				this.txtSumLCFArea.Value = BLL.MathRule.GetDecimalShowString(arrValue[4]);

				dgPlanList.DataSource = entity;
				dgPlanList.DataBind();
				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgPlanList.CurrentPageIndex = 0;
			LoadDataGridPlan();
		}

		/// <summary>
		/// ɾ����ȼƻ�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDeleteYearPlan_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				int IYear = BLL.ConvertRule.ToInt(this.txtIYear.Value);
				BLL.ConstructRule.DeleteConstructAnnualPlan(this.txtProjectCode.Value, IYear);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������" + ex.Message));
				return;
			}

			LoadPlan();
			LoadDataGridPlan();
		}

		/// <summary>
		/// ��ת��ȼƻ�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnNewYearPlan_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				int IYear = BLL.ConvertRule.ToInt(this.txtIYear.Value);
				BLL.ConstructRule.NewYearConstructAnnualPlan(this.txtProjectCode.Value, IYear, base.user.UserCode);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������" + ex.Message));
				return;
			}

			LoadPlan();
			LoadDataGridPlan();
		}

		private void dgPlanList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgPlanList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGridPlan();
		}

		private void dgPlanList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Footer) 
			{
				//��ʾ�ϼƽ��
				((Label)e.Item.FindControl("lblSumTotalBuildArea")).Text = this.txtSumTotalBuildArea.Value;
				((Label)e.Item.FindControl("lblSumPTotalInvest")).Text = this.txtSumPTotalInvest.Value;
				((Label)e.Item.FindControl("lblSumInvestBefore")).Text = this.txtSumInvestBefore.Value;
				((Label)e.Item.FindControl("lblSumPInvest")).Text = this.txtSumPInvest.Value;
				((Label)e.Item.FindControl("lblSumLCFArea")).Text = this.txtSumLCFArea.Value;
			}
		}
	}
}
