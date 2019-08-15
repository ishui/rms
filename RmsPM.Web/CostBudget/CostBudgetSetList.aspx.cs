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
	/// CostBudgetSetList ��ժҪ˵����
	/// </summary>
	public partial class CostBudgetSetList : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputText txtCostBudgetID;
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdSearchStatus;
		protected System.Web.UI.HtmlControls.HtmlInputText txtContractName;
		protected System.Web.UI.HtmlControls.HtmlTable divAdvSearch1;
	
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
				this.txtAct.Value = Request.QueryString["Act"];
//				this.txtStatus.Value = Request.QueryString["Status"];

				//Ȩ��
				this.btnAdd.Visible = base.user.HasRight("041102");

				this.ucPBS.ProjectCode = this.txtProjectCode.Value;

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
				CostBudgetSetStrategyBuilder sb = new CostBudgetSetStrategyBuilder();
				sb.AddStrategy( new Strategy( CostBudgetSetStrategyName.ProjectCode,txtProjectCode.Value));

				if ( this.ucInputSystemGroup.Value != "" )
				{
					ArrayList arGroup = new ArrayList();
					arGroup.Add(this.ucInputSystemGroup.Value);
					arGroup.Add("0");
					sb.AddStrategy( new Strategy( CostBudgetSetStrategyName.GroupCodeEx,arGroup ));
				}

				if ( this.ucUnit.Value != "" )
					sb.AddStrategy( new Strategy( CostBudgetSetStrategyName.UnitCode,this.ucUnit.Value ));

				if ( this.ucPBS.Value != "" )
				{
					sb.AddStrategy( new Strategy( CostBudgetSetStrategyName.PBSType,this.ucPBS.PBSType ));
					sb.AddStrategy( new Strategy( CostBudgetSetStrategyName.PBSCode,this.ucPBS.Value ));
				}

				if (this.txtAdvSearch.Value != "none") 
				{

					if ( this.ucCreatePerson .Value != "" )
						sb.AddStrategy( new Strategy( CostBudgetSetStrategyName.CreatePerson,this.ucCreatePerson.Value ));
					if ( this.ucModifyPerson .Value != "" )
						sb.AddStrategy( new Strategy( CostBudgetSetStrategyName.ModifyPerson,this.ucModifyPerson.Value ));

					if ( this.dtCreateDateBegin.Value != "" || this.dtCreateDateEnd.Value != "" )
					{
						ArrayList ar = new ArrayList();
						ar.Add(this.dtCreateDateBegin.Value);
						ar.Add(this.dtCreateDateEnd.Value);
						sb.AddStrategy( new Strategy( CostBudgetSetStrategyName.CreateDateRange,ar ));
					}

					if ( this.dtModifyDateBegin.Value != "" || this.dtModifyDateEnd.Value != "" )
					{
						ArrayList ar = new ArrayList();
						ar.Add(this.dtModifyDateBegin.Value);
						ar.Add(this.dtModifyDateEnd.Value);
						sb.AddStrategy( new Strategy( CostBudgetSetStrategyName.ModifyDateRange,ar ));
					}

				}

				//Ȩ��
				ArrayList arA = new ArrayList();
				arA.Add(user.UserCode);
				arA.Add(user.BuildStationCodes());
				sb.AddStrategy( new Strategy( DAL.QueryStrategy.CostBudgetSetStrategyName.AccessRange,arA));

				//����
				string sortsql = BLL.GridSort.GetSortSQL(ViewState);
				if (sortsql == "")
				{
					//ȱʡ������Ŀ��������������
					sb.AddOrder( "GroupSortID", true);
					sb.AddOrder( "PBSType", true);
					sb.AddOrder( "CostBudgetSetName", true);
				}

				string sql = sb.BuildQueryViewString();

				if (sortsql != "")
				{
					//���б�������
					sql = sql + " order by " + sortsql;
				}

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "CostBudgetSet",sql );
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

				this.GridPagination1.RowsCount = tb.Rows.Count.ToString();
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

		protected void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
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
	}
}
