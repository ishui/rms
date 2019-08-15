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

namespace RmsPM.Web.CostBudget
{
	/// <summary>
	/// CostBudgetBackupList ��ժҪ˵����
	/// </summary>
	public partial class CostBudgetBackupList : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputText txtCostBudgetID;
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdSearchStatus;
		protected System.Web.UI.HtmlControls.HtmlInputText txtContractName;
		protected System.Web.UI.HtmlControls.HtmlTable divAdvSearch1;
		protected RmsPM.Web.UserControls.InputUser ucCreatePerson;
		protected RmsPM.Web.UserControls.InputUser ucModifyPerson;
		protected RmsPM.Web.UserControls.InputUnit ucUnit;
		protected RmsPM.Web.UserControls.InputSystemGroup ucInputSystemGroup;
		protected RmsPM.Web.UserControls.InputPBS ucPBS;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadDataGrid();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtCostBudgetSetCode.Value = Request.QueryString["CostBudgetSetCode"];
				this.txtGroupCode.Value = Request.QueryString["GroupCode"];

				//Ȩ��
				if (!base.user.HasRight("041305"))
					this.dgList.Columns[this.dgList.Columns.Count-1].Visible = false;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadDataGrid()
		{
			try
			{
				CostBudgetBackupStrategyBuilder sb = new CostBudgetBackupStrategyBuilder();
				sb.AddStrategy( new Strategy( CostBudgetBackupStrategyName.ProjectCode, this.txtProjectCode.Value));
				sb.AddStrategy( new Strategy( CostBudgetBackupStrategyName.OnlyBackup));

				if ( this.dtBackupDateBegin.Value != "" || this.dtBackupDateEnd.Value != "" )
				{
					ArrayList ar = new ArrayList();
					ar.Add(this.dtBackupDateBegin.Value);
					ar.Add(this.dtBackupDateEnd.Value);
					sb.AddStrategy( new Strategy( CostBudgetBackupStrategyName.BackupDateRange,ar ));
				}

				//����
				string sortsql = BLL.GridSort.GetSortSQL(ViewState);
				if (sortsql == "")
				{
					sb.AddOrder( "BackupDate", false);
				}

				string sql = sb.BuildQueryViewString();

				if (sortsql != "")
				{
					//���б�������
					sql = sql + " order by " + sortsql;
				}

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "CostBudgetBackup",sql );
				qa.Dispose();

				/*
				string[] arrField = {"Money", "TotalPayoutMoney"};
				decimal[] arrSum = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);
				this.txtSumMoney.Value = arrSum[0].ToString("N");
				this.txtSumTotalPayoutMoney.Value = arrSum[1].ToString("N");
				*/


				BindDataGrid(entity.CurrentTable);
				entity.Dispose();
				

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
			}
		}

		private void BindDataGrid(DataTable tb)
		{
			try 
			{
				this.dgList.DataSource = tb;
				this.dgList.DataBind();

//				this.GridPagination1.RowsCount = tb.Rows.Count.ToString();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
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
			this.dgList.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemCreated);
			this.dgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgList_SortCommand);
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		private void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
				((DataGrid)source).CurrentPageIndex = 0;
				LoadDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				BLL.GridSort.ItemCreate((DataGrid)sender, ViewState, sender, e);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}	

		private void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
		{
			try
			{
				this.LoadDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid();
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			/*
			if (e.Item.ItemType == ListItemType.Footer) 
			{
				//��ʾ�ϼƽ��
				((Label)e.Item.FindControl("lblSumMoney")).Text = this.txtSumMoney.Value;
				((Label)e.Item.FindControl("lblSumTotalPayoutMoney")).Text = this.txtSumTotalPayoutMoney.Value;
			}
			*/
		}

		/// <summary>
		/// ɾ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string code = this.txtDeleteCode.Value;
				if (code == "") return;

				BLL.CostBudgetRule.DeleteCostBudgetBackup(code);

				this.LoadDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������" + ex.Message));
				return;
			}

			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.RefreshBackup();");
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}
	}
}
