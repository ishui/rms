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
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.Web;
using Rms.ORMap;


namespace RmsPM.Web.SelectBox
{
	/// <summary>
	/// SystemGroupData 的摘要说明。
	/// </summary>
	public partial class SystemGroupData : PageBase
	{

		//树分类
		private string m_TreeType = "";

//		// 所有供应商
//		private DataTable m_DataTableSupplier = null ;

		// 所有供应商类型的数据表
		private DataTable m_SystemGroupData = null;

		// 保存返回数据的数据表
		private DataTable m_Table = null ;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				m_TreeType = Request["TreeType"] + "";

				string classCode = Request["ClassCode"]+"";
				string projectCode = Request["ProjectCode"] + "";
				//String projectCode = this.project.ProjectCode;
				string m_strGetType=Request.QueryString["GetType"]+"";				//数据分类
				string m_strLayer=Request.QueryString["Layer"]+"";					//需要取的层数
				string m_strNodeId=Request.QueryString["NodeId"]+"";				//父节点编号
				string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//定点展开的序列
				string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//定层展开的深度

				m_Table=new DataTable("SystemGroupList");
				m_Table.Columns.Add("GroupCode");
				m_Table.Columns.Add("ParentCode");
				m_Table.Columns.Add("GroupName");
				m_Table.Columns.Add("Layer");
				m_Table.Columns.Add("SortID");
				m_Table.Columns.Add("ChildNodesCount");
				m_Table.Columns.Add("ShowChildNodes");
				m_Table.Columns.Add("DisplayHref");						// 是否显示链接
				m_Table.Columns.Add("DisplaySpan");						// 是否显示文字

				EntityData entityDataSystemGroup = null;
				if(classCode=="1603")
				{
					// 档案管理
					entityDataSystemGroup = DAL.EntityDAO.OADAO.GetAllOAFileType();
					m_SystemGroupData = entityDataSystemGroup.CurrentTable;

					string filter = "";
					switch (m_strGetType)
					{
						case "":
							filter = " isnull(ParentCode,'') =''";
							break;
						case "ChildNodes":
							filter = "ParentCode='"+m_strNodeId+"'";
							break;
						case "SelectLayer":
							filter = "Deep='1'";
							break;
						case "All":
							filter = "ParentCode=''";
							break;
						case "SingleNode":
							filter = "OAFileTypeCode='"+Request.QueryString["NodeId"]+""+"'";
							break;
					}

					DataView m_DV=new DataView(m_SystemGroupData,filter,"",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();
						this.FillFileRow(ref m_NewRow,m_Row);
						m_Table.Rows.Add(m_NewRow);

						if(m_strGetType=="SelectLayer")
						{
							if(int.Parse(m_strSelectedLayer)>1)
							{
								m_NewRow["ShowChildNodes"]="1";
								this.FillFileSelectedLayerData(m_Row["OAFileTypeCode"].ToString(),2,int.Parse(m_strSelectedLayer));
							}
						}

						if(m_strGetType=="All")
						{
							if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
							{
								m_NewRow["ShowChildNodes"]="1";
								this.FillFileAllData( m_Row["OAFileTypeCode"].ToString(),2);
							}
						}
					}

					entityDataSystemGroup.Dispose();

				}
				else
				{
					string projectName =BLL.ProjectRule.GetProjectName(projectCode);
					entityDataSystemGroup = DAL.EntityDAO.SystemManageDAO.GetSystemGroupByClassCode(classCode,projectName);
					m_SystemGroupData = entityDataSystemGroup.CurrentTable;
				
					//				EntityData entityDataSupplier = DAL.EntityDAO.ProjectDAO.GetAllSupplier();
					//				m_DataTableSupplier = entityDataSupplier.CurrentTable;

					
					string filter = "";
					switch (m_strGetType)
					{
						case "":
							filter = " isnull(ParentCode,'') =''";
							break;
						case "ChildNodes":
							filter = "ParentCode='"+m_strNodeId+"'";
							break;
						case "SelectLayer":
							filter = "Deep='1'";
							break;
						case "All":
							filter = "ParentCode='1'";
							break;
						case "SingleNode":
							filter = "CostCode='"+Request.QueryString["NodeId"]+""+"'";
							break;
					}


					DataView m_DV=new DataView(m_SystemGroupData,filter,"",DataViewRowState.CurrentRows);
                    if (System.Configuration.ConfigurationSettings.AppSettings["PMName"].ToLower() == "shimaopm"&&(classCode=="0501"||classCode=="0601"||classCode=="0602"))//对应合同，请款，付款３个类型。需要按项目名字匹配，只出匹配节点　ｂｙ　ｓｉｍｏｎ
                    {
                        string curProjectSystemCode="";
                        foreach(DataRowView m_Row in m_DV)
                        {
                            if (m_Row["GroupName"].ToString() == projectName) { 
                                curProjectSystemCode = m_Row["GroupCode"].ToString();
                                break;
                            }                            
                         }
                         if (curProjectSystemCode.Length > 0)
                         {
                             for(int i=m_DV.Count-1;i>=0;i--) 
                             {
                                 DataRowView m_Row = m_DV[i];
                                 if (!(m_Row["FullID"].ToString().IndexOf(curProjectSystemCode) >= 0 ||curProjectSystemCode.IndexOf(m_Row["FullID"].ToString()) >= 0))//fullcode 包含 curprojectsystemcode 或被包含
                                 { m_DV.Delete(i); }
                                 
                             }
                           
                         }
                    }
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();
						this.FillRow(ref m_NewRow,m_Row);
						m_Table.Rows.Add(m_NewRow);

						if(m_strGetType=="SelectLayer")
						{
							if(int.Parse(m_strSelectedLayer)>1)
							{
								m_NewRow["ShowChildNodes"]="1";
								this.FillSelectedLayerData(m_Row["GroupCode"].ToString(),2,int.Parse(m_strSelectedLayer));
							}
						}

						if(m_strGetType=="All")
						{
							if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
							{
								m_NewRow["ShowChildNodes"]="1";
								this.FillAllData( m_Row["GroupCode"].ToString(),2);
							}
						}
					}

					entityDataSystemGroup.Dispose();
					//				entityDataSupplier.Dispose();
				}

				Response.Write(RmsPM.WebControls.TreeView.XmlTree.GetDataToXmlString(m_Table));
				Response.End();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void FillRow(ref DataRow m_NewRow,DataRowView m_Row)
		{

			int iColumnCount = m_Row.DataView.Table.Columns.Count;
			for ( int i =0 ; i<iColumnCount; i++)
			{
				string columnName= m_Row.DataView.Table.Columns[i].ColumnName;
				if ( m_NewRow.Table.Columns.Contains(columnName))
					m_NewRow[columnName] = m_Row[columnName];
			}

			m_NewRow["Layer"]=m_Row["Deep"].ToString();
			m_NewRow["ChildNodesCount"]=m_Row["ChildNodesCount"].ToString();
			m_NewRow["ShowChildNodes"]="0";

			int iChildCount = (int)m_Row["ChildNodesCount"];
			if ( iChildCount > 0 )
			{
				m_NewRow["DisplayHref"] = "none";
				m_NewRow["DisplaySpan"] = "";
			}
			else
			{
				m_NewRow["DisplayHref"] = "";
				m_NewRow["DisplaySpan"] = "none";
			}
		}

		private void FillFileRow(ref DataRow m_NewRow,DataRowView m_Row)
		{

			int iColumnCount = m_Row.DataView.Table.Columns.Count;
			for ( int i =0 ; i<iColumnCount; i++)
			{
				string columnName= m_Row.DataView.Table.Columns[i].ColumnName;
				if ( m_NewRow.Table.Columns.Contains(columnName))
					m_NewRow[columnName] = m_Row[columnName];
				else
				{
					m_NewRow.Table.Columns.Add(columnName);
					m_NewRow[columnName] = m_Row[columnName];
				}
			}

			// 把filetype表中对应的字段值更新到systemgroup对应的字段
			m_NewRow["GroupCode"] = m_NewRow["OAFileTypeCode"].ToString();
			m_NewRow["GroupName"] = m_NewRow["TypeName"].ToString();
			
			m_NewRow["Layer"]=m_Row["Deep"].ToString();
			m_NewRow["ChildNodesCount"]=m_Row["ChildNodesCount"].ToString();
			m_NewRow["ShowChildNodes"]="0";

			int iChildCount = (int)m_Row["ChildNodesCount"];
			if ( iChildCount > 0 )
			{
				m_NewRow["DisplayHref"] = "none";
				m_NewRow["DisplaySpan"] = "";
			}
			else
			{
				m_NewRow["DisplayHref"] = "";
				m_NewRow["DisplaySpan"] = "none";
			}
		}


		private void FillSelectedLayerData( string supplierTypeCode,int m_iNowLayer,int m_iStopLayer)
		{
			DataView m_DV=new DataView(m_SystemGroupData,"ParentCode like '"+supplierTypeCode+"'","",DataViewRowState.CurrentRows);
			foreach(DataRowView m_Row in m_DV)
			{
				DataRow m_NewRow=m_Table.NewRow();
				this.FillRow(ref m_NewRow,m_Row);
				m_Table.Rows.Add(m_NewRow);
				if(m_iStopLayer>m_iNowLayer)
				{
					m_NewRow["ShowChildNodes"]="1";
					this.FillSelectedLayerData(m_Row["GroupCode"].ToString(),m_iNowLayer+1,m_iStopLayer);
				}
			}
		}

		private void FillFileSelectedLayerData( string FileTypeCode,int m_iNowLayer,int m_iStopLayer)
		{
			DataView m_DV=new DataView(m_SystemGroupData,"ParentCode like '"+FileTypeCode+"'","",DataViewRowState.CurrentRows);
			foreach(DataRowView m_Row in m_DV)
			{
				DataRow m_NewRow=m_Table.NewRow();
				//this.FillRow(ref m_NewRow,m_Row);
				m_Table.Rows.Add(m_NewRow);				
				if(m_iStopLayer>m_iNowLayer)
				{
					m_NewRow["ShowChildNodes"]="1";
					this.FillFileSelectedLayerData(m_Row["OAFileTypeCode"].ToString(),m_iNowLayer+1,m_iStopLayer);
				}
			}
		}


		private void FillAllData(string supplierTypeCode,int m_iNowLayer)
		{
			DataView m_DV=new DataView(m_SystemGroupData,"ParentCode='"+supplierTypeCode+"'","",DataViewRowState.CurrentRows);
			foreach(DataRowView m_Row in m_DV)
			{
				DataRow m_NewRow=m_Table.NewRow();
				this.FillRow(ref m_NewRow,m_Row);
				m_Table.Rows.Add(m_NewRow);
				if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
				{
					m_NewRow["ShowChildNodes"]="1";
					this.FillAllData(m_Row["GroupCode"].ToString(),m_iNowLayer+1);
				}
			}
		}

		private void FillFileAllData(string FileTypeCode,int m_iNowLayer)
		{
			DataView m_DV=new DataView(m_SystemGroupData,"ParentCode='"+FileTypeCode+"'","",DataViewRowState.CurrentRows);
			foreach(DataRowView m_Row in m_DV)
			{
				DataRow m_NewRow=m_Table.NewRow();
				m_Table.Rows.Add(m_NewRow);
				if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
				{
					m_NewRow["ShowChildNodes"]="1";
					this.FillFileAllData(m_Row["OAFileTypeCode"].ToString(),m_iNowLayer+1);
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
