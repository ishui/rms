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
	/// BudgetData 的摘要说明。
	/// </summary>
	public partial class BudgetData : PageBase
	{
		private string m_CheckBalance = "";
		private string m_TreeType = "";
		private string m_BudgetCode = "";

		private EntityData m_CBSCost = null;
		private EntityData m_BudgetData = null;
		private EntityData m_CanAccessBudget = null;
		private EntityData m_CanAccessCost = null;
		
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
				m_Table.Columns.Add("ParentCode");					// 父Codd
				m_Table.Columns.Add("CostName");					// 费用项名称
				m_Table.Columns.Add("Description");					// 说明
				m_Table.Columns.Add("Layer");						// 层数－－－对应deep深度
				m_Table.Columns.Add("ChildNodesCount");				// 子节点数目
				m_Table.Columns.Add("ShowChildNodes");				// 是否显示子节点

				m_Table.Columns.Add("SubjectCode");					// 科目编号
				m_Table.Columns.Add("SortID");						// 费用项编号
				m_Table.Columns.Add("TotalMoney");					// 估算费用
				m_Table.Columns.Add("BudgetCost");					// 预算费用
				m_Table.Columns.Add("BeforeHappenCost");			// 年前发生
				m_Table.Columns.Add("CurrentPlanCost");				// 当年计划
				m_Table.Columns.Add("AfterPlanCost");				// 剩余预算
				m_Table.Columns.Add("BudgetBalanceSign");			// 剩余预算
				m_Table.Columns.Add("Display");						// （例如）非叶节点不显示 复选框
				m_Table.Columns.Add("CanAccess",System.Type.GetType("System.String"));	// 能查看
				m_Table.Columns.Add("ShowSpan");
				m_Table.Columns.Add("ShowHref");

				// 取费用分解结构和估算费用

				string stationCodes = user.BuildStationCodes();
				m_CBSCost = BLL.CBSRule.GetAccessCostOperation(projectCode,user.UserCode,stationCodes,"040101",true);
				m_CanAccessCost = BLL.CBSRule.GetAccessV_CBSCost(projectCode,user.UserCode,stationCodes,false , fromNodeCode);
				m_CanAccessBudget = BLL.CBSRule.GetAccessCostOperation(projectCode,user.UserCode,stationCodes,"040301",false);


				// 取费用预算
				// 如果没有预算编号，显示空框架	
				m_BudgetCode = Request["BudgetCode"] + "";
				if ( m_BudgetCode != "" )
				{
					m_BudgetData = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(m_BudgetCode);
					this.m_BudgetData.SetCurrentTable("BudgetCost");
				}

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
				if ( m_BudgetCode != "" )
					this.m_BudgetData.Dispose();
				this.m_CanAccessBudget.Dispose();
				this.m_CanAccessCost.Dispose();

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
//					if ( columnName == "TotalMoney"   )
//					{
//						m_NewRow[columnName] = BLL.MathRule.GetWanDecimalShowString(m_Row,columnName);
//					}
//					else
					m_NewRow[columnName] = m_Row[columnName];

				}
			}

			m_NewRow["Layer"]=m_Row["Deep"].ToString();
			m_NewRow["ChildNodesCount"]=m_Row["ChildCount"].ToString();
			m_NewRow["ShowChildNodes"]="0";

			if ( m_Row["ChildCount"] != System.DBNull.Value )
			{
				int iChildCount = (int) m_Row["ChildCount"];
				if ( iChildCount > 0)
				{
					m_NewRow["Display"] = "none";
				}
			}

			string costCode = (string) m_Row["CostCode"];
			string fullCode = (string) m_Row["FullCode"];
			int deep = (int) m_Row["Deep"];
			int nextDeep = deep+1;

			m_NewRow["ShowSpan"]="";
			m_NewRow["ShowHref"]="none";
			if ( this.m_BudgetCode != "" )
			{

				decimal budgetCost = decimal.Zero;

				// 预算数据权限
				DataRow[] drsB = this.m_CanAccessBudget.CurrentTable.Select( String.Format( " CostCode='{0}' " ,costCode ) );
				if ( drsB.Length> 0)
				{

					//取费用预算金额
					DataRow[] drs = this.m_BudgetData.CurrentTable.Select( String.Format( " CostCode='{0}' " ,costCode )  );
					if ( drs.Length>0)
					{
						DataRow dr = drs[0];
						m_NewRow["BudgetCost"] = BLL.MathRule.GetWanDecimalShowString(drs[0],"BudgetCost");
						m_NewRow["BeforeHappenCost"] = BLL.MathRule.GetWanDecimalShowString(drs[0],"BeforeHappenCost");
						m_NewRow["CurrentPlanCost"] = BLL.MathRule.GetWanDecimalShowString(drs[0],"CurrentPlanCost");
						m_NewRow["AfterPlanCost"] = BLL.MathRule.GetWanDecimalShowString(drs[0],"AfterPlanCost");
						if ( !drs[0].IsNull("BudgetCost"))
							budgetCost = (decimal) drs[0]["BudgetCost"];
					}

					m_NewRow["ShowSpan"]="none";
					m_NewRow["ShowHref"]="";

				}
				else
				{
					m_NewRow["BudgetCost"] = "-----";
					m_NewRow["BeforeHappenCost"] = "-----";
					m_NewRow["CurrentPlanCost"] = "-----";
					m_NewRow["AfterPlanCost"] = "-----";
				}
			}

			//  估算数据权限
			m_NewRow["TotalMoney"] = "-----";
			DataRow[] drsVCost = this.m_CanAccessCost.CurrentTable.Select(String.Format("CostCode='{0}'",costCode));
			if ( drsVCost.Length > 0 )
			{
				m_NewRow["TotalMoney"] = BLL.StringRule.BuildMoneyWanFormatString(drsVCost[0]["TotalMoney"]);
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
