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
using Rms.Web;
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// DepartmentData 的摘要说明。
	/// </summary>
	public partial class OBSData : PageBase
	{
		private int m_intCurrLayer = 0;
		private string m_strParentCode = "";

		private int ToInt(string s) 
		{
			try 
			{
				return int.Parse(s);
			}
			catch 
			{
				return 0;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try 
			{
				string m_strGetType=Request.QueryString["GetType"]+"";				//数据分类
				string m_strLayer=Request.QueryString["Layer"]+"";					//需要取的层数
				string m_strNodeId=Request.QueryString["NodeId"]+"";				//父节点编号
				string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//定点展开的序列
				string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//定层展开的深度

				string m_strCurrLayer=Request.QueryString["CurrLayer"]+"";					//当前节点的层数
				string m_strParentLayer=Request.QueryString["ParentLayer"]+"";					//父节点的层数

				string m_strNotDisplayNull = Request.QueryString["NotDisplayNull"] + "";  //当人数为空时不显示该部门节点

				if (m_strCurrLayer != "") 
				{
					m_intCurrLayer = ToInt(m_strCurrLayer);
				}
				else 
				{
					m_intCurrLayer = ToInt(m_strParentLayer) + 1;
//					m_intCurrLayer = ToInt(m_strParentLayer) + 2;
				}

				m_strNodeId = DecodeId(m_strNodeId);

				DataTable m_Table=new DataTable("OBS");
				m_Table.Columns.Add("Code");
				m_Table.Columns.Add("SelfCode");
				m_Table.Columns.Add("Name");
				m_Table.Columns.Add("ParentCode");
				m_Table.Columns.Add("Description");
				m_Table.Columns.Add("Layer");
				m_Table.Columns.Add("ChildNodesCount");
				m_Table.Columns.Add("ShowChildNodes");
				m_Table.Columns.Add("NodeType");
				m_Table.Columns.Add("SortID");
				m_Table.Columns.Add("UserCount");
				m_Table.Columns.Add("ImageName");

				if(m_strGetType=="")
				{
					#region 取第一层

					m_strParentCode = "";
					EntityData entity;

					if (m_strNotDisplayNull == "1") 
					{
						entity = OBSDAO.GetOBSUnitOnlyHasUserByParent(m_strNodeId);
					}
					else 
					{
						entity = OBSDAO.GetOBSUnitByParent(m_strNodeId);
					}
					DataTable m_DataTable=entity.CurrentTable;

					foreach(DataRow dr in m_DataTable.Rows)
					{
						//用户数为0时，该节点不显示
//						int UserCount = 0;
//						if (m_DataTable.Columns.Contains("UserCount"))
//						{
//							UserCount = BLL.ConvertRule.ToInt(dr["UserCount"]);
//						}
//
//						if ((UserCount > 0) || (m_strNotDisplayNull != "1")) 
//						{
							DataRow m_NewRow = m_Table.NewRow();

							this.FillRow(ref m_NewRow,dr);
					
							m_Table.Rows.Add(m_NewRow);
//						}
					}

					entity.Dispose();

					#endregion
				}
				else if(m_strGetType=="ChildNodesOnlyUnit")
				{
					#region 取部门节点下的子部门（ChildCount只统计子部门数，不统计岗位数）

					m_strParentCode = m_strNodeId;
					EntityData entity;

					if (m_strNotDisplayNull == "1") 
					{
						entity = OBSDAO.GetUnitOnlyHasUserByParent(m_strNodeId);
					}
					else 
					{
						entity = OBSDAO.GetUnitByParent(m_strNodeId);
					}
					DataTable m_DataTable=entity.CurrentTable;

					foreach(DataRow dr in m_DataTable.Rows)
					{
						//用户数为0时，该节点不显示
//						int UserCount = 0;
//						if (m_DataTable.Columns.Contains("UserCount"))
//						{
//							UserCount = BLL.ConvertRule.ToInt(dr["UserCount"]);
//						}
//
//						if ((UserCount > 0) || (m_strNotDisplayNull != "1")) 
//						{
							DataRow m_NewRow = m_Table.NewRow();

							this.FillRow(ref m_NewRow,dr);
					
							m_Table.Rows.Add(m_NewRow);
//						}
					}

					entity.Dispose();

					#endregion
				}
				else if(m_strGetType=="ChildNodes")
				{
					#region 取部门节点下的子部门（ChildCount统计子部门数+岗位数）

					m_strParentCode = m_strNodeId;
					EntityData entity;

					if (m_strNotDisplayNull == "1") 
					{
						entity = OBSDAO.GetOBSUnitOnlyHasUserByParent(m_strNodeId);
					}
					else 
					{
						entity = OBSDAO.GetOBSUnitByParent(m_strNodeId);
					}
					DataTable m_DataTable=entity.CurrentTable;

					foreach(DataRow dr in m_DataTable.Rows)
					{
						//用户数为0时，该节点不显示
//						int UserCount = 0;
//						if (m_DataTable.Columns.Contains("UserCount"))
//						{
//							UserCount = BLL.ConvertRule.ToInt(dr["UserCount"]);
//						}
//
//						if ((UserCount > 0) || (m_strNotDisplayNull != "1")) 
//						{
							DataRow m_NewRow = m_Table.NewRow();

							this.FillRow(ref m_NewRow,dr);
					
							m_Table.Rows.Add(m_NewRow);
//						}
					}

					entity.Dispose();

					#endregion
				}
				else if(m_strGetType=="ChildNodesRoleOfUnit")
				{
					#region 取部门节点下的岗位

					m_strParentCode = m_strNodeId;

					EntityData entity = OBSDAO.GetStationByUnitCode(m_strNodeId);
					DataTable m_DataTable=entity.CurrentTable;

					foreach(DataRow dr in m_DataTable.Rows)
					{
						DataRow m_NewRow = m_Table.NewRow();

						this.FillRoleRow(ref m_NewRow,dr);
					
						m_Table.Rows.Add(m_NewRow);
					}

					entity.Dispose();

					#endregion
				}
					//				else if(m_strGetType=="ChildNodesUserOfRole")
					//				{
					//					#region 取角色节点下的子用户
					//
					//					m_strParentCode = m_strNodeId;
					//
					//					EntityData entity = SystemManageDAO.GetSystemUserByRoleCode(m_strNodeId);
					//					DataTable m_DataTable=entity.CurrentTable;
					//
					//					foreach(DataRow dr in m_DataTable.Rows)
					//					{
					//						DataRow m_NewRow = m_Table.NewRow();
					//
					//						this.FillUserRow(ref m_NewRow,dr);
					//					
					//						m_Table.Rows.Add(m_NewRow);
					//					}
					//
					//					entity.Dispose();
					//
					//					#endregion
					//				}
				else if(m_strGetType=="SingleNode")
				{
					#region 单个部门节点

					EntityData entity = OBSDAO.GetOBSUnitByCode(m_strNodeId);
					DataTable m_DataTable=entity.CurrentTable;
					if (m_DataTable.Rows.Count > 0)
					{
						DataRow dr = m_DataTable.Rows[0];
						DataRow m_NewRow = m_Table.NewRow();

						this.FillRow(ref m_NewRow,dr);
					
						m_Table.Rows.Add(m_NewRow);
					}

					entity.Dispose();

					#endregion
				}
				else if(m_strGetType=="SingleNodeRole")
				{
					#region 单个岗位节点

					EntityData entity = OBSDAO.GetStationByCode(m_strNodeId);
					DataTable m_DataTable=entity.CurrentTable;
					if (m_DataTable.Rows.Count > 0)
					{
						DataRow dr = m_DataTable.Rows[0];
						DataRow m_NewRow = m_Table.NewRow();

						this.FillRoleRow(ref m_NewRow,dr);
					
						m_Table.Rows.Add(m_NewRow);
					}

					entity.Dispose();

					#endregion
				}
				else if(m_strGetType=="NoStationUser")
				{
					#region 未定岗人员

					int UserCount = 0;
					EntityData entity = BLL.SystemRule.GetUsersNoStation();
					UserCount = entity.CurrentTable.Rows.Count;
					entity.Dispose();

					if ((UserCount > 0) || (m_strNotDisplayNull != "1")) 
					{
						DataRow m_NewRow = m_Table.NewRow();

						m_NewRow["Code"] = "D_NoStationUser";
						m_NewRow["SelfCode"] = m_NewRow["Code"];
						m_NewRow["Name"] = "未定岗人员";
						m_NewRow["Layer"] = m_intCurrLayer;
						m_NewRow["ParentCode"] = "";

						m_NewRow["UserCount"] = UserCount;

						m_NewRow["ChildNodesCount"] = 0;
						m_NewRow["ShowChildNodes"]="0";

						m_NewRow["SortID"] = "";

						m_NewRow["ImageName"] = "user.gif";
						m_NewRow["NodeType"] = "";

						m_Table.Rows.Add(m_NewRow);
					}

					#endregion
				}
//				else if(m_strGetType=="SingleNodeUser")
//				{
//					#region 单个用户节点
//
//					EntityData entity = SystemManageDAO.GetSystemUserByCode(m_strNodeId);
//					DataTable m_DataTable=entity.CurrentTable;
//					if (m_DataTable.Rows.Count > 0)
//					{
//						DataRow dr = m_DataTable.Rows[0];
//						DataRow m_NewRow = m_Table.NewRow();
//
//						this.FillUserRow(ref m_NewRow,dr);
//					
//						m_Table.Rows.Add(m_NewRow);
//					}
//
//					entity.Dispose();
//
//					#endregion
//				}

				Response.Write(RmsPM.WebControls.TreeView.XmlTree.GetDataToXmlString(m_Table));

/*  注释掉  2004.5.26  begin---------------------------------------------------------------------------------
				string m_strUnitCode=(Request.QueryString["UnitCode"]==null?"":Request.QueryString["UnitCode"]);
				string m_strLayers=(Request.QueryString["Layers"]==null?"*":Request.QueryString["Layers"]);
				string m_strLayer=(Request.QueryString["Layer"]==null?"0":Request.QueryString["Layer"]);
				string[] m_Layers=m_strLayers.Split('.');

				DataTable m_Table=new DataTable("Unit");
				m_Table.Columns.Add("UnitCode");
				m_Table.Columns.Add("UnitName");
				m_Table.Columns.Add("Remark");
				m_Table.Columns.Add("Layer");
				m_Table.Columns.Add("ChildNodesCount");
				m_Table.Columns.Add("ShowChildNodes");
				m_Table.Columns.Add("PrincipalName");

				//			EntityData m_Unit=OBSDAO.GetUnitByParent(base.ProjectCode,m_strUnitCode);
				EntityData m_Unit=OBSDAO.GetUnitByParent(m_strUnitCode);

				foreach(DataRow m_Row in m_Unit.Tables["Unit"].Rows)
				{
					DataRow m_NewRow = m_Table.NewRow();
					m_NewRow["UnitCode"]=m_Row["UnitCode"].ToString();
					m_NewRow["UnitName"]=m_Row["UnitName"].ToString();
					m_NewRow["PrincipalName"]=m_Row["PrincipalName"].ToString();
					m_NewRow["Remark"]=m_Row["Remark"].ToString();
					m_NewRow["Layer"]=m_Row["Deep"].ToString();
					m_NewRow["ChildNodesCount"]=m_Row["ChildCount"].ToString();
					m_NewRow["ShowChildNodes"]=0;
					m_Table.Rows.Add(m_NewRow);
				
				}
				m_Unit.Dispose();

				Response.Write(RmsPM.WebControls.TreeView.XmlTree.GetDataToXmlString(m_Table));
				Response.End();
注释掉  2004.5.26  end---------------------------------------------------------------------------------*/
			}
			catch (Exception ex) 
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载组织结构错误。" + ex.Message);
			}

			Response.End();
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

		private void FillRow(ref DataRow m_NewRow,DataRow m_Row)
		{
			m_NewRow["Code"]=EncodeId(m_Row["UnitCode"].ToString(), "D_");
			m_NewRow["SelfCode"]=m_Row["UnitCode"].ToString();
			m_NewRow["Name"]=m_Row["UnitName"].ToString();
			m_NewRow["Layer"]=m_intCurrLayer;//int.Parse(m_Row["Deep"].ToString()) + 1;
			m_NewRow["ParentCode"] = EncodeId(m_Row["ParentUnitCode"].ToString(), "D_");
			m_NewRow["ChildNodesCount"]=m_Row["ChildCount"].ToString();
			m_NewRow["ShowChildNodes"]="0";

			m_NewRow["SortID"]=m_Row["SortID"].ToString();
			m_NewRow["UserCount"] = m_Row["UserCount"].ToString();

			string UnitType = "";
			if (m_Row["UnitType"] != null) 
			{
				UnitType = m_Row["UnitType"].ToString();
			}

			switch (UnitType) 
			{
				case "项目":
					m_NewRow["ImageName"] = "deptproj.gif";
					m_NewRow["Name"] = m_NewRow["Name"].ToString();
//					m_NewRow["Name"] = "项目 " + m_NewRow["Name"].ToString();
					break;

				default:
					m_NewRow["ImageName"] = "dept.gif";
					break;
			}

			m_NewRow["NodeType"] = UnitType;
		}

		
		private void FillRoleRow(ref DataRow m_NewRow,DataRow m_Row)
		{
			m_NewRow["Code"]=EncodeId(m_Row["StationCode"].ToString(), "R_");
			m_NewRow["SelfCode"]=m_Row["StationCode"].ToString();
			m_NewRow["Name"]=m_Row["StationName"].ToString();
			m_NewRow["Layer"]=m_intCurrLayer;
			m_NewRow["ParentCode"] = EncodeId(m_strParentCode, "D_");
			m_NewRow["ChildNodesCount"]=0;
			m_NewRow["ShowChildNodes"]="0";
			m_NewRow["NodeType"] = "";
			//m_NewRow["SortID"]=m_Row["SortID"].ToString();
			m_NewRow["UserCount"]=m_Row["UserCount"].ToString();
		}

//		private void FillUserRow(ref DataRow m_NewRow,DataRow m_Row)
//		{
//			m_NewRow["Code"]=EncodeId(m_Row["UserCode"].ToString(), "U" + m_strParentCode + "_");
//			m_NewRow["SelfCode"]=m_Row["UserCode"].ToString();
//			m_NewRow["Name"]=m_Row["UserName"].ToString();
//			m_NewRow["Layer"]=m_intCurrLayer;
//			m_NewRow["ParentCode"] = EncodeId(m_strParentCode, "R_");
//			m_NewRow["ChildNodesCount"]="0";
//			m_NewRow["ShowChildNodes"]="0";
//			m_NewRow["NodeType"] = "";
//
//		}

		private string EncodeId(string id, string type) 
		{
			if (id == "")
				return "";

			return type + id;
		}

		private string DecodeId(string id) 
		{
			if (id == "")
				return "";

			int i = id.IndexOf("_");
			if (i < 0)
				return id;

			return id.Substring(i + 1, id.Length-i-1);
		}
	}
}
