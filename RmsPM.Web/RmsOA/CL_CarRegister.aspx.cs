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

namespace RmsPM.Web.Contract
{
	/// <summary>
	/// Contract 的摘要说明。
	/// </summary>
	public partial class CL_CarRegister : PageBase
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();

				BuildSearchString();
				LoadDataGrid();
			}
		}

		private void IniPage()
		{
			string projectCode = Request["ProjectCode"]+"";
			string status = Request["Status"] + "";
			BLL.PageFacade.SetListGroupSelectedValues(this.cblStatus,status.Split(new char[]{';'}));

            string ud_sChangeStatus = Request["ChangeStatus"] + "";
            BLL.PageFacade.SetListGroupSelectedValues(this.cblChangeStatus, ud_sChangeStatus.Split(new char[] { ';' }));

//			BLL.PageFacade.LoadUnitSelect(this.sltUnit,"",projectCode);

			this.inputSystemGroup.ClassCode="0501";

			ViewState["ImagePath"] = "../Images/";

            //ArrayList ar = user.GetClassRight("Contract");
            //if ( ! ar.Contains("050102"))
            if ( !user.HasRight("050102")) 
				this.btnNew.Visible = false;


			//绑定费用项的ProjectCode
			ucCostBudgetDtl.ProjectCode = projectCode;
			ucCostBudgetDtl.SelectAllLeaf = true;

            switch (this.up_sPMName.ToUpper())
            {
                case "SHIMAOPM":
                    //世茂：只显示本部门的预算表 xyq 2007.7.25
                    BLL.PageFacade.LoadCostBudgetSetSelect(this.sltCostBudgetSet, "", projectCode, user.m_EntityDataAccessUnit);
                    break;

                default:
                    BLL.PageFacade.LoadCostBudgetSetSelect(this.sltCostBudgetSet, "", projectCode);
                    break;

            }

		}

		private void LoadDataGrid()
		{
			
			try
			{
				string sql = (string)this.ViewState["SearchString"];
				QueryAgent qa = new QueryAgent();
               //
               // EntityData entity = qa.GetCachetManageList(sql); //孙权2007-9-28下午注视
                EntityData entity = null;
				qa.Dispose();

                //string[] arrField = {"TotalMoney","AHMoney","APMoney"};
                //decimal[] arrSum = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);
                //ViewState["SumTotalMoney"] = arrSum[0];
                //ViewState["SumPayment"] = arrSum[1];
                //ViewState["SumAPMoney"] = arrSum[2];
                //ViewState["SumUPMoney"] = arrSum[0] - arrSum[2];

                DataView ud_dvContract = new DataView(entity.CurrentTable);

                //ud_dvContract.Sort = "Car_Type";

                this.dgList.DataSource = ud_dvContract;
				this.dgList.DataBind();
				this.GridPagination1.RowsCount = entity.CurrentTable.Rows.Count.ToString();

				entity.Dispose();

			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载合同列表错误。");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载合同列表错误：" + ex.Message));
			}
		}

		private void dgList_Bind(DataTable dt)
		{
			DataView dv = new DataView(dt);
			dv.Sort = "CreateDate desc";

			this.dgList.DataSource = dv;
			this.dgList.DataBind();
		}

		private void BuildSearchString()
		{
            //string ud_sProjectCode= Request["ProjectCode"] + "";
            //RmsPM.DAL.QueryStrategy.ContractStrategyBuilder CSB=new RmsPM.DAL.QueryStrategy.ContractStrategyBuilder();
			
            //string status = BLL.PageFacade.GetListGroupSelectedValues(this.cblStatus);
            //if (status != "")
            //{
            //    CSB.AddStrategy(new Strategy(ContractStrategyName.Status, status));
            //}

            //string ud_sChangeStatus = BLL.PageFacade.GetListGroupSelectedValues(this.cblChangeStatus);
            //if (ud_sChangeStatus != "")
            //{
            //    CSB.AddStrategy(new Strategy(ContractStrategyName.ChangeStatus, ud_sChangeStatus));
            //}

            //if (ud_sProjectCode != "")
            //{
            //    CSB.AddStrategy(new Strategy(ContractStrategyName.ProjectCode, ud_sProjectCode));
            //}
            //string sltCostBudgetSetCode = this.sltCostBudgetSet.Items[this.sltCostBudgetSet.SelectedIndex].Value;

            //if ( sltCostBudgetSetCode != "" )
            //{
            //    CSB.AddStrategy( new Strategy( ContractStrategyName.CostBudgetSetCode,sltCostBudgetSetCode));
            //}

            //string costBudgetSetCode = ucCostBudgetDtl.CostBudgetSetCode;
            //string fullCode = ucCostBudgetDtl.CostCode;

            //if ( costBudgetSetCode != "" )
            //{
            //    CSB.AddStrategy( new Strategy( ContractStrategyName.CostBudgetSetCode,costBudgetSetCode));
            //}

            //if ( fullCode != "" )
            //{
            //    CSB.AddStrategy( new Strategy( ContractStrategyName.FullCode,"%"+fullCode+"%"));
            //}


            //if ( this.txtContractName.Value.Trim() != "")
            //    CSB.AddStrategy( new Strategy( ContractStrategyName.ContractName, "%" + this.txtContractName.Value.Trim() +"%" ));

            //if ( this.txtContractID.Value.Trim() != "")
            //    CSB.AddStrategy( new Strategy( ContractStrategyName.ContractID, "%" + this.txtContractID.Value.Trim() + "%" ));

            //ArrayList arA = new ArrayList();
            //arA.Add("050101");
            //arA.Add(user.UserCode);
            //arA.Add(user.BuildStationCodes());
            //CSB.AddStrategy( new Strategy( ContractStrategyName.AccessRange,arA));

            //if ( this.txtSupplierCode.Value != "" )
            //    CSB.AddStrategy( new Strategy( ContractStrategyName.SupplierCode, this.txtSupplierCode.Value ));

            //if (this.txtSupplierName.Value.Trim() != "")
            //{
            //    CSB.AddStrategy(new Strategy(ContractStrategyName.SupplierName, this.txtSupplierName.Value.Trim()));
            //}

            //if (  this.inputSystemGroup.Value != "" )
            //{
            //    ArrayList arS = new ArrayList();
            //    arS.Add( this.inputSystemGroup.Value );
            //    arS.Add("0");
            //    CSB.AddStrategy( new Strategy( ContractStrategyName.TypeEx,arS));
            //}

            //if (this.txtAdvSearch.Value != "none") 
            //{
            //    if ( this.ucUnit.Value != "" )
            //        CSB.AddStrategy( new Strategy( ContractStrategyName.UnitCode,this.ucUnit.Value ));

            //    if ( this.ucContractPerson.Value != "" )
            //        CSB.AddStrategy( new Strategy( ContractStrategyName.ContractPerson,this.ucContractPerson.Value ));

            //    if ( this.dtContractDate0.Value != "" || this.dtContractDate1.Value != "" )
            //    {
            //        ArrayList ar = new ArrayList();
            //        ar.Add(this.dtContractDate0.Value);
            //        ar.Add(this.dtContractDate1.Value);
            //        CSB.AddStrategy( new Strategy( ContractStrategyName.ContractDate, ar));
            //    }

            //    if ( this.txtTotalMoney0.Text != "" || this.txtTotalMoney1.Text != "" )
            //    {
            //        ArrayList ar = new ArrayList();
            //        ar.Add((this.txtTotalMoney0.Text=="")?"":this.txtTotalMoney0.ValueDecimal.ToString());
            //        ar.Add((this.txtTotalMoney1.Text=="")?"":this.txtTotalMoney1.ValueDecimal.ToString());
            //        CSB.AddStrategy( new Strategy( ContractStrategyName.TotalMoney, ar));
            //    }
            //}

			//排序
			string sortsql = BLL.GridSort.GetSortSQL(ViewState, "Car_Id,Car_Type");

			string sql ="select * from GK_OA_Car where 1=1";

			if (sortsql != "")
			{
				//点列标题排序
				sql = sql + " order by " + sortsql;
			}

			this.ViewState.Add("SearchString",sql);
		}


		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		private void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
				((DataGrid)source).CurrentPageIndex = 0;
				BuildSearchString();
				LoadDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}	

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			BuildSearchString();
			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid();
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{

			switch ( e.Item.ItemType )
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					break;
				case ListItemType.Footer:
					((Label)e.Item.FindControl("lblSumTotalMoney")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumTotalMoney"]);
					((Label)e.Item.FindControl("lblSumPayment")).Text = BLL.MathRule.GetDecimalShowString( ViewState["SumPayment"] );
					((Label)e.Item.FindControl("lblSumAPMoney")).Text = BLL.MathRule.GetDecimalShowString( ViewState["SumAPMoney"] );
					((Label)e.Item.FindControl("lblSumUPMoney")).Text = BLL.MathRule.GetDecimalShowString( ViewState["SumUPMoney"] );
					break;
				default:
					break;
			}
		}

//		private void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
//		{
//			string sortColumn = e.SortExpression;
//			if ( sortColumn == "" )
//				return;
//			string oldSortColumnName = (string)this.ViewState["SortColumnName"];
//			string sort = (string)this.ViewState["Sort"];
//			if ( sortColumn == oldSortColumnName)
//			{
//				if ( sort == "ASC")
//					sort = "DESC";
//				else
//					sort = "ASC";
//			}
//			else
//				sort = "ASC";
//
//			this.ViewState.Add("SortColumnName",sortColumn);
//			this.ViewState.Add("Sort",sort);
//			this.dgList.CurrentPageIndex = 0;
//			BuildSearchString();
//			LoadDataGrid();
//		}

	}
}
