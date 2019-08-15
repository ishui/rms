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

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// DynamicCost 的摘要说明。
	/// </summary>
	public partial class DynamicCostData : PageBase
	{

		private string m_CheckBalance = "";
		private string m_TreeType = "";
		private string m_BudgetCode = "";
		private string m_CurrentBudgetPlanCode = "";
		private string m_EndDate = "";
		private string m_StartDate = "";


		private EntityData m_CBSCost = null;
		private EntityData m_BudgetData = null;				// 参照预算（就是前一次有效的预算）
		private EntityData m_DynamicData = null;
		private EntityData m_CanAccessDynamic = null;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"] + "";
			try
			{
				m_TreeType = Request["TreeType"] + "";
				m_CheckBalance = Request["CheckBalance"] + "";

				string m_strGetType=Request.QueryString["GetType"]+"";				//数据分类
				string m_strLayer=Request.QueryString["Layer"]+"";					//需要取的层数
				string m_strNodeId=Request.QueryString["NodeId"]+"";				//父节点编号
				string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//定点展开的序列
				string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//定层展开的深度
				string fromNodeCode = Request["FromNodeCode"]+"";

				DataTable m_Table=new DataTable("Budget");
				m_Table.Columns.Add("CostCode");					// 费用编号
				m_Table.Columns.Add("ParentCode");					// 父Code
				m_Table.Columns.Add("CostName");					// 费用项名称
				m_Table.Columns.Add("Description");					// 说明
				m_Table.Columns.Add("Layer");						// 层数－－－对应deep深度
				m_Table.Columns.Add("ChildNodesCount");				// 子节点数目
				m_Table.Columns.Add("ShowChildNodes");				// 是否显示子节点

				m_Table.Columns.Add("SubjectCode");					// 科目编号
				m_Table.Columns.Add("SortID");						// 费用项编号

				m_Table.Columns.Add("TotalMoney");					// 估算
				m_Table.Columns.Add("DynamicCost");					// 动态费用
				m_Table.Columns.Add("CurrentPlanCost");				// 本期预算

				m_Table.Columns.Add("CurrentMonthBudget");				// 本月预算
				m_Table.Columns.Add("CurrentMonthAH");				// 本月实际发生
				m_Table.Columns.Add("CurrentMonthContract");		// 本月合同应付

				m_Table.Columns.Add("CanAccess",System.Type.GetType("System.String"));	// 能查看
				m_Table.Columns.Add("ShowSpan");
				m_Table.Columns.Add("ShowHref");

				m_BudgetCode = Request["BudgetCode"] + "";
				
				QueryAgent qa = new QueryAgent();
				// 动态费用对象
				if ( m_BudgetCode != "" )
				{
					m_DynamicData = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(m_BudgetCode);
					int iYear = m_DynamicData.GetInt("IYear");
					int iMonth = m_DynamicData.GetInt("IMonth");

					this.m_StartDate = iYear.ToString() + "-" + iMonth.ToString() + "-1";
					string checkDate = m_DynamicData.GetDateTimeOnlyDate("CheckDate");
					if ( checkDate == "" )
						this.m_EndDate = DateTime.Now.ToString("yyyy-MM-dd");
					else
						this.m_EndDate = checkDate;
					this.m_CurrentBudgetPlanCode = this.m_DynamicData.GetString("ReferBudgetCode");
					this.m_DynamicData.SetCurrentTable("BudgetCost");
					this.m_BudgetData = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(this.m_CurrentBudgetPlanCode);
					this.m_BudgetData.SetCurrentTable("BudgetCost");
				}
				
				m_CBSCost = BLL.CBSRule.GetAccessCostOperation(projectCode,user.UserCode,user.BuildStationCodes(),"040101",true);
				m_CanAccessDynamic = BLL.CBSRule.GetAccessCostOperation(projectCode,user.UserCode,user.BuildStationCodes(),"040401",false);

				DataTable m_DataTable=m_CBSCost.CurrentTable;
				string filter = "";
				if(m_strGetType=="")
				{
					#region 取第一层

					filter = " isnull(ParentCode,'') =''";
					DataView m_DV=new DataView(m_DataTable,filter," SortID ",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();
					
						this.FillRow(ref m_NewRow,m_Row,m_DataTable);

						m_Table.Rows.Add(m_NewRow);
					}
					#endregion
				}
				else if(m_strGetType=="ChildNodes")
				{
					#region 取某节点子目录

					filter = "ParentCode='"+m_strNodeId+"'";
					DataView m_DV=new DataView(m_DataTable,filter,"SortID",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();

						this.FillRow(ref m_NewRow,m_Row,m_DataTable);
					
						m_Table.Rows.Add(m_NewRow);
					}
					#endregion
				}
				else if(m_strGetType=="SelectLayer")
				{
					#region 取指定层数结果

					filter = "Deep='1'";
					DataView m_DV=new DataView(m_DataTable,filter,"SortID",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();

						this.FillRow(ref m_NewRow,m_Row,m_DataTable);
					
						m_Table.Rows.Add(m_NewRow);
						if(int.Parse(m_strSelectedLayer)>1)
						{
							m_NewRow["ShowChildNodes"]="1";
							this.FillSelectedLayerData(ref m_Table,m_Row["CostCode"].ToString(),2,int.Parse(m_strSelectedLayer),m_DataTable);
						}
					}
					#endregion
				}
				else if(m_strGetType=="All")
				{
					#region 取所有结果

					filter = "ParentCode='1'";
					DataView m_DV=new DataView(m_DataTable,filter," SortID ",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();

						this.FillRow(ref m_NewRow,m_Row,m_DataTable);
					
						m_Table.Rows.Add(m_NewRow);

						if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
						{
							m_NewRow["ShowChildNodes"]="1";
							this.FillAllData(ref m_Table,m_Row["CostCode"].ToString(),2,m_DataTable);
						}
					}
					#endregion
				}
				else if(m_strGetType=="SingleNode")
				{
					#region 单个节点
					filter = "CostCode='"+Request.QueryString["NodeId"]+""+"'";
					DataView m_DV=new DataView(m_DataTable,filter," SortID ",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();
						this.FillRow(ref m_NewRow,m_Row,m_DataTable);
					
						m_Table.Rows.Add(m_NewRow);

					}
					#endregion
				}
				m_CBSCost.Dispose();
				this.m_CanAccessDynamic.Dispose();
				if (this.m_BudgetData != null)
					this.m_BudgetData.Dispose();
				if ( this.m_DynamicData != null )
					this.m_DynamicData.Dispose();

				Response.Write(RmsPM.WebControls.TreeView.XmlTree.GetDataToXmlString(m_Table));
				Response.End();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		private void FillRow(ref DataRow m_NewRow,DataRowView m_Row , DataTable m_DataTable )
		{


			//当前记录是 flag ＝ －1 的预算版本； 
			int iColumnCount = m_Row.DataView.Table.Columns.Count;
			for ( int i =0 ; i<iColumnCount; i++)
			{
				string columnName= m_Row.DataView.Table.Columns[i].ColumnName;
				if ( m_NewRow.Table.Columns.Contains(columnName))
				{
					if ( columnName == "TotalMoney"   )
					{
						m_NewRow[columnName] = BLL.MathRule.GetWanDecimalShowString(m_Row,columnName);
					}
					else
						m_NewRow[columnName] = m_Row[columnName];

				}
			}

			m_NewRow["Layer"]=m_Row["Deep"].ToString();
			m_NewRow["ChildNodesCount"]=m_Row["ChildCount"].ToString();
			m_NewRow["ShowChildNodes"]="0";

			string costCode = (string) m_Row["CostCode"];
			string fullCode = (string) m_Row["FullCode"];
			int deep = (int) m_Row["Deep"];
			int nextDeep = deep+1;


			m_NewRow["ShowSpan"]="";
			m_NewRow["ShowHref"]="none";
			// 取动态
			if ( this.m_BudgetCode != "" )
			{
				// 判断权限
				DataRow[] drsD = this.m_CanAccessDynamic.CurrentTable.Select ( String.Format(" CostCode='{0}' " ,costCode ));
				if ( drsD.Length > 0 )
				{
					DataRow[] drs = this.m_DynamicData.CurrentTable.Select( String.Format( " CostCode='{0}' " ,costCode )  );
					if ( drs.Length > 0 )
					{
						m_NewRow["DynamicCost"] =BLL.MathRule.GetWanDecimalShowString(drs[0],"BudgetCost");
						m_NewRow["CurrentPlanCost"] =BLL.MathRule.GetWanDecimalShowString(drs[0],"CurrentPlanCost");
					}

					int iYear = (int) this.m_DynamicData.Tables["Budget"].Rows[0]["IYear"];
					int iMonth = (int) this.m_DynamicData.Tables["Budget"].Rows[0]["IMonth"];
					int months = (DateTime.Now.Year - iYear)*12 + ( DateTime.Now.Month - iMonth ) + 1;

					DateTime periodEndDate = (DateTime)(this.m_DynamicData.Tables["Budget"].Rows[0]["EndDate"]);
					DateTime periodStartDate = (DateTime)(this.m_DynamicData.Tables["Budget"].Rows[0]["StartDate"]);

					decimal currentMonthBudget = BLL.MathRule.SumColumn( this.m_DynamicData.Tables["BudgetMonth"],"Money",String.Format( "IMonth={0} and CostCode='{1}' " ,months,costCode ));


					// 取本月的月末
					string currentMonthEndDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-1")).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
					// 本月的实际发生数
					decimal currentMonthAH = BLL.CBSRule.GetAHMoney(costCode,DateTime.Now.ToString("yyyy-MM-1"),currentMonthEndDate);

					// 到本月月末为止，该成本项总的应付款
					decimal currentMonthContract = BLL.CBSRule.GetContractAllocationCost( costCode,"","",periodStartDate.ToString("yyyy-MM-dd"),currentMonthEndDate)
						- BLL.CBSRule.GetContractAllocationHappenedCost( costCode,"","",periodStartDate.ToString("yyyy-MM-dd"),currentMonthEndDate) ;


					m_NewRow["CurrentMonthBudget"] = BLL.StringRule.BuildMoneyWanFormatString(currentMonthBudget);
					m_NewRow["CurrentMonthAH"] = BLL.StringRule.BuildMoneyWanFormatString(currentMonthAH);
					m_NewRow["CurrentMonthContract"] = BLL.StringRule.BuildMoneyWanFormatString(currentMonthContract);

					m_NewRow["ShowSpan"]="none";
					m_NewRow["ShowHref"]="";

				}
				else
				{
					m_NewRow["DynamicCost"] ="-----";
					m_NewRow["CurrentPlanCost"] ="-----";
					m_NewRow["CurrentMonthBudget"] = "-----";
					m_NewRow["CurrentMonthAH"] = "-----";
					m_NewRow["CurrentMonthContract"] = "-----";
				}

			}
		}


		private void FillSelectedLayerData(ref DataTable m_Table,string m_strCostCode,int m_iNowLayer,int m_iStopLayer,DataTable m_DataTable)
		{
			DataView m_DV=new DataView(m_DataTable,"ParentCode like '"+m_strCostCode+"'","",DataViewRowState.CurrentRows);
			foreach(DataRowView m_Row in m_DV)
			{
				DataRow m_NewRow=m_Table.NewRow();

				this.FillRow(ref m_NewRow,m_Row, m_DataTable);

				m_Table.Rows.Add(m_NewRow);
				if(m_iStopLayer>m_iNowLayer)
				{
					m_NewRow["ShowChildNodes"]="1";
					this.FillSelectedLayerData(ref m_Table,m_Row["CostCode"].ToString(),m_iNowLayer+1,m_iStopLayer,m_DataTable);
				}
			}
		}

		private void FillAllData(ref DataTable m_Table,string m_strCostCode,int m_iNowLayer,DataTable m_DataTable)
		{
			DataView m_DV=new DataView(m_DataTable,"ParentCode='"+m_strCostCode+"'","",DataViewRowState.CurrentRows);
			foreach(DataRowView m_Row in m_DV)
			{
				DataRow m_NewRow=m_Table.NewRow();

				this.FillRow(ref m_NewRow,m_Row, m_DataTable);

				m_Table.Rows.Add(m_NewRow);
				if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
				{
					m_NewRow["ShowChildNodes"]="1";
					this.FillAllData(ref m_Table,m_Row["CostCode"].ToString(),m_iNowLayer+1,m_DataTable);
				}
			}
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
		}
		#endregion
	}
}
