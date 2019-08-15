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

namespace RmsPM.Web.Project
{
	/// <summary>
	/// CBSData 的摘要说明。
	/// </summary>
	public partial class CBSData : PageBase
	{
		private string m_SubjectSetCode = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"] + "";
			m_SubjectSetCode = BLL.ProjectRule.GetSubjectSetCodeByProject(projectCode);
			try
			{

				string m_strGetType=Request.QueryString["GetType"]+"";				//数据分类
				string m_strLayer=Request.QueryString["Layer"]+"";					//需要取的层数
				string m_strNodeId=Request.QueryString["NodeId"]+"";				//父节点编号
				string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//定点展开的序列
				string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//定层展开的深度
				string fromNodeCode = Request["FromNodeCode"]+"";

				DataTable m_Table=new DataTable("CBS");
				m_Table.Columns.Add("CostCode");					// 费用编号
				m_Table.Columns.Add("ParentCode");					// 父Code
				m_Table.Columns.Add("CostName");					// 费用项名称
				m_Table.Columns.Add("Description");					// 说明
				m_Table.Columns.Add("Layer");						// 层数－－－对应deep深度
				m_Table.Columns.Add("ChildNodesCount");				// 子节点数目
				m_Table.Columns.Add("ShowChildNodes");				// 是否显示子节点

				m_Table.Columns.Add("SubjectCode");					// 科目编号
				m_Table.Columns.Add("SubjectName");					// 科目名称

				m_Table.Columns.Add("SortID");						// 费用项编号
				m_Table.Columns.Add("CostAllocationDescription");	// 费用分解说明
				m_Table.Columns.Add("BudgetType");  //预算类别
				m_Table.Columns.Add("BudgetTName");  //预算类别名称

				m_Table.Columns.Add("CanAccess",System.Type.GetType("System.String"));	// 能查看
				m_Table.Columns.Add("ShowSpan");
				m_Table.Columns.Add("ShowHref");


//				m_Table.Columns.Add("IsFixed");						// 锁定



				EntityData m_Cost = BLL.CBSRule.GetAccessCBS(projectCode,user.UserCode,user.BuildStationCodes());

				DataTable m_DataTable=m_Cost.CurrentTable;

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
				m_Cost.Dispose();

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

					// 这几个数值特别处理
					if ( columnName == "TotalMoney" || columnName == "UnitPrice" || columnName == "ProjectQuantity"  )
					{
						if( m_Row[columnName] != System.DBNull.Value )
						{
							decimal m = (decimal)m_Row[columnName];
							if ( BLL.MathRule.CheckDecimalEqual(m,decimal.Zero))
								m_NewRow[columnName] = "";
							else
							{
								if ( columnName == "TotalMoney" )
									m_NewRow[columnName] = BLL.StringRule.BuildMoneyWanFormatString(m);
								else
									m_NewRow[columnName] = BLL.StringRule.BuildShowNumberString(m);

							}
						}
					}
					else
						m_NewRow[columnName] = m_Row[columnName];
				}
			}
			
			string costCode = (string) m_Row["CostCode"];
			m_NewRow["Layer"]=m_Row["Deep"].ToString();
			m_NewRow["ChildNodesCount"]=m_Row["ChildCount"].ToString();
			m_NewRow["ShowChildNodes"]="0";
			m_NewRow["SubjectName"] = BLL.SubjectRule.GetSubjectName(m_NewRow["SubjectCode"].ToString(),m_SubjectSetCode);

			m_NewRow["BudgetTName"] = BLL.SystemGroupRule.GetSystemGroupName(BLL.ConvertRule.ToString(m_NewRow["BudgetType"]));


			string canAccess="0";
			if ( !m_NewRow.IsNull("CanAccess"))
				canAccess = (string)m_NewRow["CanAccess"];

			if ( canAccess == "1" )
			{
				m_NewRow["ShowSpan"]="none";
				m_NewRow["ShowHref"]="";
			}
			else
			{
				m_NewRow["ShowSpan"]="";
				m_NewRow["ShowHref"]="none";
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
