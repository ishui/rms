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

using RmsPM.DAL.EntityDAO;
using Rms.ORMap;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// SystemGroupData 的摘要说明。
	/// </summary>
	public partial class SystemGroupData : PageBase
	{
		private string m_strAct = "";
		private int m_iNodeLayer = 0;
		private string m_ClassCode = "";
		private string m_ProjectCode = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try 
			{
				string m_strGetType=Request.QueryString["GetType"]+"";				//数据分类
				string m_strNodeLayer=Request.QueryString["NodeLayer"]+"";				//父节点的层数
				string m_strNodeId=Request.QueryString["NodeId"]+"";				//父节点编号
				string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//定点展开的序列
				string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//定层展开的深度

				m_ClassCode = Request.QueryString["ClassCode"] + "";
				m_ProjectCode = Request.QueryString["ProjectCode"] + "";
				m_strAct = Request.QueryString["Act"] + "";

				m_iNodeLayer = BLL.ConvertRule.ToInt(m_strNodeLayer);

				DataTable m_Table=new DataTable("SystemGroup");
				m_Table.Columns.Add("GroupCode");
				m_Table.Columns.Add("ParentCode");
				m_Table.Columns.Add("GroupName");
				m_Table.Columns.Add("SortID");
				m_Table.Columns.Add("DisplayGroupName");
				m_Table.Columns.Add("Layer");
				m_Table.Columns.Add("ChildNodesCount");
				m_Table.Columns.Add("ShowChildNodes");			
				m_Table.Columns.Add("NodeType");
				m_Table.Columns.Add("ClassCode");
				m_Table.Columns.Add("ItemType");
				m_Table.Columns.Add("ImageFileName");

				EntityData entity = null;
				DataTable m_DataTable = null;

				if(m_strGetType=="AllClass")
				{
					#region 取第一层

					FunctionStructureStrategyBuilder sb = new FunctionStructureStrategyBuilder();
					sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.IsSystemClass, "1" )  );

					//只列出某个模块的类别
					string FunctionStructureCode = Request.QueryString["FunctionStructureCode"] + "";
					if (FunctionStructureCode != "")
						sb.AddStrategy ( new Strategy( FunctionStructureStrategyName.FunctionStructureCode, FunctionStructureCode )  );

					sb.AddOrder("FunctionStructureCode", true);
					string sql = sb.BuildMainQueryString();

					QueryAgent qa = new QueryAgent();
					entity = qa.FillEntityData("FunctionStructure", sql);
					qa.Dispose();

					m_DataTable = entity.CurrentTable;

					DataView m_DV = new DataView(m_DataTable,"","",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();
					
						this.FillClassRow(ref m_NewRow,m_Row);

						m_Table.Rows.Add(m_NewRow);
					}
					#endregion
				}
				else if(m_strGetType=="Class")
				{
					#region 取指定大类
					entity = DAL.EntityDAO.SystemManageDAO.GetFunctionStructureByCode(m_strNodeId);
					m_DataTable = entity.CurrentTable;

					DataView m_DV = new DataView(m_DataTable,"","",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();
					
						this.FillClassRow(ref m_NewRow,m_Row);

						m_Table.Rows.Add(m_NewRow);
					}
					#endregion
				}
				else if(m_strGetType=="ChildNodesOfClass")
				{
					#region 取某节点子目录

					m_iNodeLayer++;
					DataView m_DV;

					switch (m_ClassCode) 
					{
						case "0401":
							//费用项
							entity = DAL.EntityDAO.ProjectDAO.GetAllProject();
							m_DataTable = entity.CurrentTable;

							m_DV=new DataView(m_DataTable,"","",DataViewRowState.CurrentRows);
							foreach(DataRowView m_Row in m_DV)
							{
								DataRow m_NewRow=m_Table.NewRow();

								this.FillRowProject(ref m_NewRow,m_Row);
					
								m_Table.Rows.Add(m_NewRow);
							}

							break;

						case "0701":
							//工作项
							entity = DAL.EntityDAO.WBSDAO.GetAllRootTask();
							m_DataTable = entity.CurrentTable;

							m_DV=new DataView(m_DataTable,"","",DataViewRowState.CurrentRows);
							foreach(DataRowView m_Row in m_DV)
							{
								DataRow m_NewRow=m_Table.NewRow();

								this.FillRowTask(ref m_NewRow,m_Row);
					
								m_Table.Rows.Add(m_NewRow);
							}

							break;

						case "1603":
							// 档案管理
							entity = DAL.EntityDAO.OADAO.GetAllOAFileType();
							m_DataTable = entity.CurrentTable;

							m_DV=new DataView(m_DataTable,"","",DataViewRowState.CurrentRows);
							foreach(DataRowView m_Row in m_DV)
							{
								DataRow m_NewRow=m_Table.NewRow();

								this.FillRowDocument(ref m_NewRow,m_Row);
					
								m_Table.Rows.Add(m_NewRow);
							}

							break;

						default:
							entity = RmsPM.DAL.EntityDAO.SystemManageDAO.GetSystemGroupByClassCode(m_ClassCode);
							m_DataTable = entity.CurrentTable;

							m_DV=new DataView(m_DataTable,"ParentCode=''","",DataViewRowState.CurrentRows);
							foreach(DataRowView m_Row in m_DV)
							{
								DataRow m_NewRow=m_Table.NewRow();

								this.FillRow(ref m_NewRow,m_Row);
					
								m_Table.Rows.Add(m_NewRow);
							}

							break;
					}
					#endregion
				}
				else if(m_strGetType=="ChildNodes")
				{
					#region 取某节点子目录

					m_iNodeLayer++;
					DataView m_DV;

					switch (m_ClassCode) 
					{
						case "0401":
							//费用项
							if (m_strNodeId == "") 
							{
								entity = RmsPM.DAL.EntityDAO.CBSDAO.GetCBSRootByProject(m_ProjectCode);
							}
							else 
							{
								entity = RmsPM.DAL.EntityDAO.CBSDAO.GetCBSByParentCode(m_strNodeId);
							}

							m_DataTable = entity.CurrentTable;

							m_DV=new DataView(m_DataTable,"","",DataViewRowState.CurrentRows);
							foreach(DataRowView m_Row in m_DV)
							{
								DataRow m_NewRow=m_Table.NewRow();

								this.FillRowCBS(ref m_NewRow,m_Row);
					
								m_Table.Rows.Add(m_NewRow);
							}

							break;

						case "0701":
							//工作项
							entity = RmsPM.DAL.EntityDAO.WBSDAO.GetChildTask(m_strNodeId);
							m_DataTable = entity.CurrentTable;

							m_DV=new DataView(m_DataTable,"","",DataViewRowState.CurrentRows);
							foreach(DataRowView m_Row in m_DV)
							{
								DataRow m_NewRow=m_Table.NewRow();

								this.FillRowTask(ref m_NewRow,m_Row);
					
								m_Table.Rows.Add(m_NewRow);
							}

							break;

						case "1603":
							// 档案管理
							entity = DAL.EntityDAO.OADAO.GetRuleOAFileType(m_strNodeId);
							m_DataTable = entity.CurrentTable;

							m_DV=new DataView(m_DataTable,"","",DataViewRowState.CurrentRows);
							foreach(DataRowView m_Row in m_DV)
							{
								DataRow m_NewRow=m_Table.NewRow();

								this.FillRowDocument(ref m_NewRow,m_Row);
					
								m_Table.Rows.Add(m_NewRow);
							}

							break;


						default:
							entity = RmsPM.DAL.EntityDAO.SystemManageDAO.GetSystemGroupByParentCode(m_strNodeId);
							m_DataTable = entity.CurrentTable;

							m_DV=new DataView(m_DataTable,"","",DataViewRowState.CurrentRows);
							foreach(DataRowView m_Row in m_DV)
							{
								DataRow m_NewRow=m_Table.NewRow();

								this.FillRow(ref m_NewRow,m_Row);
					
								m_Table.Rows.Add(m_NewRow);
							}

							break;
					}
					#endregion
				}
				else if(m_strGetType=="SingleNodeOfClass")
				{
					#region 取某节点
					entity = RmsPM.DAL.EntityDAO.SystemManageDAO.GetFunctionStructureByCode(m_strNodeId);
					m_DataTable = entity.CurrentTable;

					DataView m_DV=new DataView(m_DataTable,"","",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();

						this.FillClassRow(ref m_NewRow,m_Row);
					
						m_Table.Rows.Add(m_NewRow);
					}
					#endregion
				}
				else if(m_strGetType=="SingleNode")
				{
					#region 取某节点
					entity = RmsPM.DAL.EntityDAO.SystemManageDAO.GetV_SystemGroupByCode(m_strNodeId);
					m_DataTable = entity.CurrentTable;

					DataView m_DV=new DataView(m_DataTable,"","",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();

						this.FillRow(ref m_NewRow,m_Row);
					
						m_Table.Rows.Add(m_NewRow);
					}
					#endregion
				}
				entity.Dispose();

				Response.Write(RmsPM.WebControls.TreeView.XmlTree.GetDataToXmlString(m_Table));
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}

			Response.End();
		}

		private void FillRow(ref DataRow m_NewRow,DataRowView m_Row)
		{
			string nodeType=BLL.ConvertRule.ToInt(m_Row["ChildNodesCount"])>0?"folder":"item";
			m_NewRow["GroupCode"]=m_Row["GroupCode"].ToString();
			m_NewRow["ParentCode"]=m_Row["ParentCode"].ToString();
			m_NewRow["GroupName"] = m_Row["GroupName"].ToString();
			m_NewRow["SortID"] = m_Row["SortID"].ToString();
			m_NewRow["DisplayGroupName"] = BLL.ConvertRule.ToString(m_Row["SortID"]) + " " + m_Row["GroupName"].ToString();
			m_NewRow["Layer"] = m_iNodeLayer;
            m_NewRow["ChildNodesCount"] = BLL.ConvertRule.ToInt(m_Row["ChildNodesCount"]);
			m_NewRow["ShowChildNodes"]="0";
			m_NewRow["NodeType"]=nodeType;
			m_NewRow["ItemType"] = "G";
			m_NewRow["ClassCode"] = m_Row["ClassCode"].ToString();
			m_NewRow["ImageFileName"] = "Folder.gif";
		}

		private void FillClassRow(ref DataRow m_NewRow,DataRowView m_Row)
		{
			m_NewRow["GroupCode"] = m_Row["FunctionStructureCode"].ToString();
			m_NewRow["ParentCode"] = "";
			m_NewRow["GroupName"] = m_Row["FunctionStructureName"].ToString();
			m_NewRow["SortID"] = "";
			m_NewRow["DisplayGroupName"] = m_Row["FunctionStructureName"].ToString();
			m_NewRow["Layer"] = m_iNodeLayer;
			m_NewRow["ChildNodesCount"] = 1;
			m_NewRow["ShowChildNodes"] = "0";
			m_NewRow["ItemType"] = "C";
			m_NewRow["ClassCode"] = m_Row["FunctionStructureCode"].ToString();
			m_NewRow["ImageFileName"] = "dept.gif";

            string nodeType = BLL.ConvertRule.ToInt(m_NewRow["ChildNodesCount"]) > 0 ? "folder" : "item";
			m_NewRow["NodeType"] = nodeType;
		}

		/// <summary>
		/// 填充项目名称
		/// </summary>
		/// <param name="m_NewRow"></param>
		/// <param name="m_Row"></param>
		private void FillRowProject(ref DataRow m_NewRow,DataRowView m_Row)
		{
			m_NewRow["GroupCode"] = m_Row["ProjectCode"].ToString();
			m_NewRow["ParentCode"] = "";
			m_NewRow["GroupName"] = m_Row["ProjectName"];
			m_NewRow["SortID"] = "";
			m_NewRow["DisplayGroupName"] = m_Row["ProjectName"];
			m_NewRow["Layer"] = m_iNodeLayer;
			m_NewRow["ChildNodesCount"] = 1;
			m_NewRow["ShowChildNodes"] = "0";
			m_NewRow["ItemType"] = "P";
			m_NewRow["ClassCode"] = m_ClassCode;
			m_NewRow["ImageFileName"] = "Folder.gif";

            string nodeType = BLL.ConvertRule.ToInt(m_NewRow["ChildNodesCount"]) > 0 ? "folder" : "item";
			m_NewRow["NodeType"] = nodeType;
		}

		/// <summary>
		/// 填充费用项
		/// </summary>
		/// <param name="m_NewRow"></param>
		/// <param name="m_Row"></param>
		private void FillRowCBS(ref DataRow m_NewRow,DataRowView m_Row)
		{
			m_NewRow["GroupCode"] = m_Row["CostCode"].ToString();
			m_NewRow["ParentCode"] = m_Row["ParentCode"].ToString();
			m_NewRow["GroupName"] = m_Row["CostName"];
			m_NewRow["SortID"] = m_Row["SortID"];
			m_NewRow["DisplayGroupName"] = BLL.ConvertRule.ToString(m_Row["SortID"]) + " " + m_Row["CostName"].ToString();
			m_NewRow["Layer"] = m_iNodeLayer;
            m_NewRow["ChildNodesCount"] = BLL.ConvertRule.ToInt(m_Row["ChildCount"]);
			m_NewRow["ShowChildNodes"] = "0";
			m_NewRow["ItemType"] = "G";
			m_NewRow["ClassCode"] = "0401";
			m_NewRow["ImageFileName"] = "Folder.gif";

            string nodeType = BLL.ConvertRule.ToInt(m_NewRow["ChildNodesCount"]) > 0 ? "folder" : "item";
			m_NewRow["NodeType"] = nodeType;
		}

		/// <summary>
		/// 填充工作项
		/// </summary>
		/// <param name="m_NewRow"></param>
		/// <param name="m_Row"></param>
		private void FillRowTask(ref DataRow m_NewRow,DataRowView m_Row)
		{
			m_NewRow["GroupCode"] = m_Row["WBSCode"].ToString();
			m_NewRow["ParentCode"] = m_Row["ParentCode"].ToString();
			m_NewRow["GroupName"] = m_Row["TaskName"];
			m_NewRow["SortID"] = m_Row["SortID"];
			m_NewRow["DisplayGroupName"] = BLL.ConvertRule.ToString(m_Row["SortID"]) + " " + m_Row["TaskName"].ToString();
			m_NewRow["Layer"] = m_iNodeLayer;
            m_NewRow["ChildNodesCount"] = BLL.ConvertRule.ToInt(m_Row["ChildNodesCount"]);
			m_NewRow["ShowChildNodes"] = "0";
			m_NewRow["ItemType"] = "T";
			m_NewRow["ClassCode"] = "0701";
			m_NewRow["ImageFileName"] = "Folder.gif";

            string nodeType = BLL.ConvertRule.ToInt(m_NewRow["ChildNodesCount"]) > 0 ? "folder" : "item";
			m_NewRow["NodeType"] = nodeType;
		}

		/// <summary>
		/// 填充档案管理
		/// </summary>
		/// <param name="m_NewRow"></param>
		/// <param name="m_Row"></param>
		private void FillRowDocument(ref DataRow m_NewRow,DataRowView m_Row)
		{
			// 加入档案信息
			m_NewRow["GroupCode"] = m_Row["OAFileTypeCode"].ToString();
			m_NewRow["ParentCode"] = m_Row["ParentCode"].ToString();
			m_NewRow["GroupName"] = m_Row["TypeName"].ToString();
			m_NewRow["SortID"] = "";
			m_NewRow["DisplayGroupName"] = m_Row["TypeName"].ToString();
			m_NewRow["Layer"] = m_iNodeLayer;
            m_NewRow["ChildNodesCount"] = BLL.ConvertRule.ToInt(m_Row["ChildNodesCount"]);
			m_NewRow["ShowChildNodes"] = "0";
			m_NewRow["ItemType"] = "D";
			m_NewRow["ClassCode"] = "1603";
			m_NewRow["ImageFileName"] = "Folder.gif";

			string nodeType = BLL.ConvertRule.ToInt(m_NewRow["ChildNodesCount"])>0?"folder":"item";
			m_NewRow["NodeType"] = nodeType;
		}

		#region FillSelectedLayerData
		private void FillSelectedLayerData(ref DataTable m_Table,string m_strWBSCode,int m_iNowLayer,int m_iStopLayer,DataTable m_DataTable)
		{
			DataView m_DV=new DataView(m_DataTable,"ParentCode like '"+m_strWBSCode+"'","",DataViewRowState.CurrentRows);
			foreach(DataRowView m_Row in m_DV)
			{
				DataRow m_NewRow=m_Table.NewRow();

				this.FillRow(ref m_NewRow,m_Row);

				m_Table.Rows.Add(m_NewRow);
				if(m_iStopLayer>m_iNowLayer)
				{
					m_NewRow["ShowChildNodes"]="1";
					this.FillSelectedLayerData(ref m_Table,m_Row["PBSTypeCode"].ToString(),m_iNowLayer+1,m_iStopLayer,m_DataTable);
				}
			}
		}
		#endregion

		#region FillAllData
		private void FillAllData(ref DataTable m_Table,string m_strWBSCode,int m_iNowLayer,DataTable m_DataTable)
		{
			DataView m_DV=new DataView(m_DataTable,"ParentCode='"+m_strWBSCode+"'","",DataViewRowState.CurrentRows);
			foreach(DataRowView m_Row in m_DV)
			{
				DataRow m_NewRow=m_Table.NewRow();

				this.FillRow(ref m_NewRow,m_Row);

				m_Table.Rows.Add(m_NewRow);
                if (BLL.ConvertRule.ToInt(m_NewRow["ChildNodesCount"]) > 0)
				{
					m_NewRow["ShowChildNodes"]="1";
					this.FillAllData(ref m_Table,m_Row["PBSTypeCode"].ToString(),m_iNowLayer+1,m_DataTable);
				}
			}
		}
		#endregion


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
