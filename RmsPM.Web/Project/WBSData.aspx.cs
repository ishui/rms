using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using RmsPM.DAL.EntityDAO;
using Rms.ORMap;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSData ��ժҪ˵����
	/// </summary>
	public partial class WBSData : PageBase
	{
		private string m_strNodeId = "";

//		private string m_HasRightFullCodes = ""; //��Ȩ�޵Ĺ�����
		private string[] m_arrHasRightFullCode; //��Ȩ�޵Ĺ�����

//		private DataTable m_tbHasRightFullCode = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			string m_strGetType=Request.QueryString["GetType"]+"";				//���ݷ���
			string m_strLayer=Request.QueryString["Layer"]+"";					//��Ҫȡ�Ĳ���
			m_strNodeId=Request.QueryString["NodeId"]+"";				//���ڵ���
			string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//����չ��������
			string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//����չ�������
			ViewState["ProjectCode"] = Request["ProjectCode"].ToString();

			try
			{
				DataTable m_Table=new DataTable("Task");
				m_Table.Columns.Add("WBSCode");
				m_Table.Columns.Add("ParentCode");
				m_Table.Columns.Add("TaskName");
				m_Table.Columns.Add("SortID");
				m_Table.Columns.Add("Layer");
				m_Table.Columns.Add("ChildNodesCount");
				m_Table.Columns.Add("ShowChildNodes");
				m_Table.Columns.Add("WorkStartDate");
				m_Table.Columns.Add("WorkActualStartDate");
				m_Table.Columns.Add("WorkActualFinishDate");
				m_Table.Columns.Add("WorkDays");
				m_Table.Columns.Add("OverDays");
				m_Table.Columns.Add("NodeType");
				m_Table.Columns.Add("Status");
				m_Table.Columns.Add("Remark");
				m_Table.Columns.Add("TaskStatus");
				m_Table.Columns.Add("Exceed");
				m_Table.Columns.Add("ImportantLevel");
				m_Table.Columns.Add("Flag");
				m_Table.Columns.Add("WorkEndDate");
				m_Table.Columns.Add("CompletePercent");
				m_Table.Columns.Add("PauseReason");
				m_Table.Columns.Add("CancelReason");
				m_Table.Columns.Add("FullCode");
				m_Table.Columns.Add("IsRight");
                m_Table.Columns.Add("LastModifyDate");

				WBSStrategyBuilder asb = new WBSStrategyBuilder();
				asb.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.ProjectCode,(string)ViewState["ProjectCode"]));

				switch (m_strGetType)
				{
					case "SingleNode":  //�������
						asb.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.WBSCode,m_strNodeId));
						break;

					default:  //�ӽ��
						asb.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.ParentCode,m_strNodeId));
						break;
				}

//				asb.AddOrder(" Deep ",true);
				asb.AddOrder(" PlannedStartDate ",false);
				asb.AddOrder(" SortID ",true);

				string sql = asb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData m_Task1 = qa.FillEntityData("Task",sql);	

				DataTable m_DataTable = m_Task1.CurrentTable;

				m_arrHasRightFullCode = BLL.WBSRule.GetAllHasRightTask(Session, user.UserCode);

				if(m_strGetType=="")
				{
					#region ȡ��һ��
					DataView m_DV=new DataView(m_DataTable,"ParentCode='' ","",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();

						this.FillRow(ref m_NewRow,m_Row);

						m_Table.Rows.Add(m_NewRow);
					}
					#endregion
				}
				else if(m_strGetType=="ChildNodes")
				{
					#region ȡĳ�ڵ���Ŀ¼
					DataView m_DV=new DataView(m_DataTable,"ParentCode='"+m_strNodeId+"'","",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();

						this.FillRow(ref m_NewRow,m_Row);
					
						m_Table.Rows.Add(m_NewRow);
					}
					#endregion
				}
				else if(m_strGetType=="SelectLayer")
				{
					#region ȡ�ƶ��������
					DataView m_DV=new DataView(m_DataTable,"Deep='1'","",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();

						this.FillRow(ref m_NewRow,m_Row);
					
						m_Table.Rows.Add(m_NewRow);
						if(int.Parse(m_strSelectedLayer)>1)
						{
							m_NewRow["ShowChildNodes"]="1";
							//this.FillSelectedLayerData(ref m_Table,m_Row["SortID"].ToString(),2,int.Parse(m_strSelectedLayer),m_DataTable); ԭ��������SortID
							this.FillSelectedLayerData(ref m_Table,m_Row["WBSCode"].ToString(),2,int.Parse(m_strSelectedLayer),m_DataTable);
						}
					}
					#endregion
				}
				else if(m_strGetType=="All")
				{
					#region ȡ���н��
					DataView m_DV=new DataView(m_DataTable,"ParentCode='1'","",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();

						this.FillRow(ref m_NewRow,m_Row);
					
						m_Table.Rows.Add(m_NewRow);

						if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
						{
							m_NewRow["ShowChildNodes"]="1";
							//this.FillAllData(ref m_Table,m_Row["SortID"].ToString(),2,m_DataTable);ԭ��������SortID
							this.FillAllData(ref m_Table,m_Row["WBSCode"].ToString(),2,m_DataTable);
						}
					}
					#endregion
				}
				else if(m_strGetType=="SingleNode")
				{
					#region �����ڵ�
					DataView m_DV=new DataView(m_DataTable,"WBSCode='"+m_strNodeId+"'","",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();

						this.FillRow(ref m_NewRow,m_Row);
					
						m_Table.Rows.Add(m_NewRow);

					}
					#endregion
				}

				string m_strCondition="";
				if(Request.QueryString["TaskName"]+""!="")
				{
					m_strCondition+=(m_strCondition==""?"":" and ");
					m_strCondition += " ((TaskName LIKE '*" + Server.UrlDecode(Request.QueryString["TaskName"]).ToString().Trim() + "*') OR ";
					m_strCondition +="(WBSCode like '%" + Request.QueryString["TaskName"].Trim() + "%')) ";
				}
				if (Request.QueryString["Master"] + "" !="")
				{
					m_strCondition += (m_strCondition == ""?"":" and ");
					m_strCondition += " Master LIKE '*" + Server.UrlDecode(Request.QueryString["Master"]).ToString().Trim() + "*'";
				}
				if(Request.QueryString["Status"]+""!="")
				{
					m_strCondition+=(m_strCondition==""?"":" and ");
					m_strCondition +="TaskStatus='" + Request.QueryString["Status"].Trim() + "'";
				}
				if(Request.QueryString["ImportantLevel"]+""!="")
				{
					m_strCondition+=(m_strCondition==""?"":" and ");
					m_strCondition +="ImportantLevel='" + Request.QueryString["ImportantLevel"].Trim() + "'";
				}
				if(Request.QueryString["Exceed"]+""!="")
				{
					m_strCondition+=(m_strCondition==""?"":" and ");
					m_strCondition +="Exceed='" + Request.QueryString["Exceed"].Trim() + "'";
				}
				if(Request.QueryString["StartDate"]+""!="")
				{
					m_strCondition+=(m_strCondition==""?"":" and ");
					m_strCondition+="CONVERT(WorkStartDate,'System.DateTime')>='"+Request.QueryString["StartDate"]+"'";
				}
				if(Request.QueryString["EndDate"]+""!="")
				{
					m_strCondition+=(m_strCondition==""?"":" and ");
					m_strCondition+="CONVERT(WorkStartDate,'System.DateTime')<'"+Request.QueryString["EndDate"]+"'";
				}

				if (m_strCondition != "")
				{
					m_strCondition = "((" + m_strCondition + ") or ParentCode = '')";
				}
				

				//��������������
				DataView m_DataView=new DataView(m_Table,m_strCondition,"Layer,SortID",DataViewRowState.CurrentRows);
				DataTable tbNew = m_Table.Clone();
				foreach(DataRowView drv in m_DataView)
				{
					DataRow drNew = tbNew.NewRow();

					foreach(DataColumn col in tbNew.Columns) 
					{
						drNew[col.ColumnName] = drv[col.ColumnName];
					}

					tbNew.Rows.Add(drNew);
				}

				m_Task1.Dispose();

				TaskProc(tbNew);

				//ɾ��û��Ȩ�޵Ľڵ�
				BLL.WBSRule.DeleteNoRightTask(tbNew, m_arrHasRightFullCode);

				Response.Write(RmsPM.WebControls.TreeView.XmlTree.GetDataToXmlString(tbNew));
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"WBSData�ļ�");
			}

			Response.End();
		}

		private void FillRow(ref DataRow m_NewRow,DataRowView m_Row)
		{
			string nodeType=int.Parse(m_Row["ChildNodesCount"].ToString())>0?"folder":"item";
			m_NewRow["WBSCode"]=m_Row["WBSCode"].ToString();
			m_NewRow["FullCode"] = m_Row["FullCode"];
			m_NewRow["ParentCode"]=m_Row["ParentCode"].ToString();
			m_NewRow["TaskName"]=m_Row["TaskName"].ToString();
			m_NewRow["SortID"]=m_Row["SortID"].ToString();
			m_NewRow["Layer"]=m_Row["Deep"].ToString();
			//m_NewRow["WorkStartDate"]="";
			m_NewRow["WorkDays"]="0";
			m_NewRow["OverDays"]="0";
			int childNodeCount = (int) m_Row["ChildNodesCount"];
			m_NewRow["ChildNodesCount"]=childNodeCount.ToString();
			m_NewRow["ShowChildNodes"]="0";
			m_NewRow["NodeType"]=nodeType;
			m_NewRow["Exceed"] = m_Row["Exceed"].ToString();
			m_NewRow["Remark"] = m_Row["Remark"].ToString();
			m_NewRow["TaskStatus"] = m_Row["Status"].ToString();
			m_NewRow["ImportantLevel"] = m_Row["ImportantLevel"].ToString();
			m_NewRow["CompletePercent"] = m_Row["CompletePercent"].ToString();
			m_NewRow["Flag"] = m_Row["Flag"].ToString();

			if (m_Row["PlannedStartDate"] != System.DBNull.Value)
			{
				m_NewRow["WorkStartDate"] = DateTime.Parse(m_Row["PlannedStartDate"].ToString()).ToString("yyyy-MM-dd");
			}
			else
			{
				m_NewRow["WorkStartDate"] = "";
			}

			if (m_Row["PlannedFinishDate"] != System.DBNull.Value)
			{
                m_NewRow["WorkEndDate"] = DateTime.Parse(m_Row["PlannedFinishDate"].ToString()).ToString("yyyy-MM-dd");
			}
			else
			{
				m_NewRow["WorkEndDate"] = "";
			}

			if (m_Row["ActualStartDate"] != System.DBNull.Value)
			{
                m_NewRow["WorkActualStartDate"] = DateTime.Parse(m_Row["ActualStartDate"].ToString()).ToString("yyyy-MM-dd");
			}
			else
			{
				m_NewRow["WorkActualStartDate"] = "";
			}

			if (m_Row["ActualFinishDate"] != System.DBNull.Value)
			{
                m_NewRow["WorkActualFinishDate"] = DateTime.Parse(m_Row["ActualFinishDate"].ToString()).ToString("yyyy-MM-dd");
			}
			else
			{
				m_NewRow["WorkActualFinishDate"] = "";
			}

            m_NewRow["LastModifyDate"] = BLL.ConvertRule.ToDateString(m_Row["LastModifyDate"], "yyyy-MM-dd");

			m_NewRow["PauseReason"] = m_Row["PauseReason"].ToString();
			m_NewRow["CancelReason"] = m_Row["CancelReason"].ToString();


		}

		/// <summary>
		/// ������Ȩ�ޡ���ʾ���Ƶ�
		/// </summary>
		/// <param name="dtTask"></param>
		private void TaskProc(System.Data.DataTable dtTask)
		{
			try
			{
				dtTask.Columns.Add("StatusName");
				dtTask.Columns.Add("ImportantName");
				dtTask.Columns.Add("Master");

				string[] strStatusList = {"δ��ʼ","������","��ͣ","ȡ��","�����"};
				string[] strImportantList = {"һ��","��Ҫ"};

				foreach(DataRow dr in dtTask.Rows)
				{
					dr["StatusName"] = (dr["Status"] + ""== "")?"":strStatusList[int.Parse(dr["Status"].ToString())];
					dr["ImportantName"] = (dr["ImportantLevel"] + "" == "")?"":strImportantList[int.Parse(dr["ImportantLevel"].ToString())];
					dr["Master"] = this.GetMaster(dr["WBSCode"].ToString());					

					//dr["TaskName"] = dr["SortID"].ToString() +"  " + BLL.StringRule.TruncText(dr["TaskName"].ToString(),15);
					dr["TaskName"] = BLL.StringRule.TruncText(dr["TaskName"].ToString(),15);
					dr["Master"] = BLL.StringRule.TruncText(dr["Master"].ToString(),7);
				}

				BLL.WBSRule.SetTaskIsRight(dtTask, "IsRight", m_arrHasRightFullCode);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				throw ex;
			}
		}

		private string GetMaster(string strWBSCode)
		{
			string strUsers = "";
			EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(strWBSCode);
			if (entityUser.HasRecord())
			{
				DataTable dtUserNew = entityUser.CurrentTable;				
				for (int i = 0; i < dtUserNew.Rows.Count; i++)
				{
					if (dtUserNew.Rows[i]["Type"].ToString() == "9") // ������
					{
						if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // ����Ϊ��
						{
							strUsers += (strUsers.Length>0)?",":"";
							strUsers += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
						}
						if(dtUserNew.Rows[i]["RoleType"].ToString()=="1") // ����Ϊ��λ
						{
							strUsers += (strUsers.Length>0)?",":"";
							strUsers += BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString());
						}
					}
				}
			}
			entityUser.Dispose();
			return strUsers;
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
					this.FillSelectedLayerData(ref m_Table,m_Row["WBSCode"].ToString(),m_iNowLayer+1,m_iStopLayer,m_DataTable);
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
				if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
				{
					m_NewRow["ShowChildNodes"]="1";
					this.FillAllData(ref m_Table,m_Row["WBSCode"].ToString(),m_iNowLayer+1,m_DataTable);
				}
			}
		}
		#endregion

		#region GetFolderWorkDates  ����û����;
		private ArrayList GetFolderWorkDates(string strWBSCode)
		{
			ArrayList aryDates=new ArrayList();
			aryDates.Add(null);
			aryDates.Add(null);
			SqlConnection conn=new SqlConnection(base.ConnectionString);
			conn.Open();
			SqlCommand cmd=new SqlCommand("select * from wbs w,task t where w.wbscode=t.wbscode and w.projectcode='"+(string)ViewState["ProjectCode"]+"' and w.ParentCode='"+strWBSCode+"'",conn);
			SqlDataReader dr=cmd.ExecuteReader();
			while(dr.Read())
			{
				DateTime startDatex=DateTime.Parse(dr["PlannedStartDate"].ToString());
				DateTime endDatex=DateTime.Now;
				if(double.Parse(dr["Duration"].ToString())!=0.00)
				{
					endDatex=DateTime.Parse(dr["PlannedStartDate"].ToString()).AddDays(double.Parse(dr["Duration"].ToString()));
				}
				else
				{
					endDatex=startDatex.AddDays(1.00);
				}
				ArrayList dates=GetFolderWorkDates(dr["WBSCode"].ToString());

				if(dates[0]!=null)
				{
					startDatex=DateTime.Parse(dates[0].ToString());
				}
				if(dates[1]!=null)
				{
					endDatex=DateTime.Parse(dates[1].ToString());
				}

				if(aryDates[0]==null||DateTime.Parse(aryDates[0].ToString())>startDatex)
				{
					aryDates[0]=startDatex;
				}
				if(aryDates[1]==null||DateTime.Parse(aryDates[1].ToString())<endDatex)
				{
					aryDates[1]=endDatex;
				}
			}
			dr.Close();
			conn.Close();
			return aryDates;
		}
		#endregion

		#region GetTaskOverPercent
		private decimal GetTaskOverPercent(string strWBSCode)
		{
			decimal returnValue=decimal.Zero;
			SqlConnection conn=new SqlConnection(base.ConnectionString);
			conn.Open();
			SqlCommand cmd=new SqlCommand("select completepercent from taskexecute where wbscode='"+strWBSCode+"' order by executeDate desc",conn);
			SqlDataReader dr=cmd.ExecuteReader();
			if(dr.Read())
			{
				returnValue=decimal.Parse(dr[0].ToString());
			}
			dr.Close();
			conn.Close();
			return returnValue;
		}
		#endregion

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

		}
		#endregion
	}
}
