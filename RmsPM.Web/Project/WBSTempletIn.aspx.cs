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
using RmsPM.BLL;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// �����ֽ�������
	/// </summary>
	public partial class WBSTempletIn : PageBase
	{
		protected System.Web.UI.WebControls.Button ButtonSave;
		
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.Button Button1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				if (!IsPostBack)
				{
					LoadData();
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ��ҳ�����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}
		
		private void LoadData()
		{
			try
			{
				PageFacade.LoadTempletSelect(this.sltTemplet,"","WBS");
			}
			catch(Exception ex)
			{
				throw ex;
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

		}
		#endregion

		private void InData(string templetCode)
		{
			ViewState["ProjectCode"] = Request["ProjectCode"].ToString();
			string projectCode = (string)ViewState["ProjectCode"];

			//EntityData del=WBSDAO.GetTaskByProject((string)ViewState["ProjectCode"]);
			//��ȡ��ǰ������ṹ����ɾ��֮
			DAL.QueryStrategy.WBSStrategyBuilder WSB = new RmsPM.DAL.QueryStrategy.WBSStrategyBuilder();
			WSB.AddStrategy(new Strategy(DAL.QueryStrategy.WBSStrategyName.ProjectCode,(string)ViewState["ProjectCode"]));
			string sql = WSB.BuildMainQueryString();
			QueryAgent QA = new QueryAgent();
			EntityData entityProject = QA.FillEntityData("Task",sql + " and Flag = 1");
			EntityData del = QA.FillEntityData("Task",sql + " and Flag = 0 ");
				
			string ProjectWBSCode = "";
			if (entityProject.HasRecord())
			{
				ProjectWBSCode = entityProject.GetString("WBSCode");
			}
			entityProject.Dispose();
			QA.Dispose();

			if (del.HasRecord())
			{
				int iCount = del.CurrentTable.Rows.Count;
				for(int i=0;i<iCount;i++)
				{
					DeleteStandard_WBS(del.CurrentTable.Rows[i]["WBSCode"].ToString());
				}
			}
			del.Dispose();

			EntityData entity=WBSDAO.GetStandard_WBSTempletByCode(templetCode);
			if (entity.HasRecord())
			{
				entity.SetCurrentTable("WBSTemplet");
				entity.CurrentTable.Columns.Add("NewWBSCode");
				entity.CurrentTable.Columns.Add("NewFullCode");

				/*
				// ȡ������������ļƻ���ʼʱ����Ϊ��Ŀ�Ŀ�ʼʱ�䣬 
				// ��������������Ŀ�Ŀ�ʼʱ������ֵ
				DateTime dateBaseStart;
				DataRow[] rowDate = entity.CurrentTable.Select("PlannedStartDate is not null","PlannedStartDate");
				if (rowDate.Length > 0)
				{
					dateBaseStart = (DateTime)rowDate[0]["PlannedStartDate"];
				}

				DateTime dateNewStartDate = DateTime.Parse( this.dtbProjectStartDate.Value);
				TimeSpan bts = dateNewStartDate - dateBaseStart ;
				int iSPDate = bts.Days;							// ������Ŀ��ʱ���������
				*/

				// ����Deep˳�������µ�WBSCode��FullCode
				DataRow[] drs = entity.CurrentTable.Select("","Deep");
				int iCount = entity.CurrentTable.Rows.Count;
				for ( int i=0;i<iCount;i++)
				{
					string newWBSCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("WBS");
					drs[i]["NewWBSCode"] = newWBSCode;
					string parentCode = "";
					if ( !drs[i].IsNull("ParentCode"))
						parentCode = (string) drs[i]["ParentCode"];

					string newParentFullCode = "";
					string newFullCode = "";
					string newParentCode = "";
					if ( parentCode != "" )
					{
						DataRow[] pDrs = entity.CurrentTable.Select( "WBSCode='" + parentCode + "'");
						if ( pDrs.Length>0)
						{
							newParentFullCode = (string) pDrs[0]["NewFullCode"];
							newParentCode = (string )pDrs[0]["NewWBSCode"];
						}

					}
					else
					{
						newParentCode = ProjectWBSCode;
						newParentFullCode = ProjectWBSCode;

					}
					if ( newParentFullCode == "" )
						newFullCode = newWBSCode;
					else
						newFullCode = newParentFullCode + "-" + newWBSCode ;

					drs[i]["NewFullCode"] = newFullCode;
					drs[i]["ParentCode"] = newParentCode;

				}

				// ȡ�ø��ڵ���Ȩ��
				string strUser = "";
				string strStation = "";
				EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(ProjectWBSCode);
				if (entityUser.HasRecord())
				{
					DataTable dtUserNew = entityUser.CurrentTable.Copy();					
					for (int i = 0; i < dtUserNew.Rows.Count; i++)
					{
						if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // ����Ϊ��
						{
							if (dtUserNew.Rows[i]["Type"].ToString() == "2") // ������
							{
								strUser +=(strUser == "")?"":",";
								strUser += dtUserNew.Rows[i]["UserCode"].ToString();
							}							
						}
						if(dtUserNew.Rows[i]["RoleType"].ToString()=="1") // ����Ϊ��λ
						{
							if (dtUserNew.Rows[i]["Type"].ToString() == "2") // ������
							{
								strStation +=(strStation == "")?"":",";
								strStation += dtUserNew.Rows[i]["UserCode"].ToString();
							}							
						}
					}
				}
				entityUser.Dispose();
				
				EntityData task = new EntityData("Task");
				//DataRow newWBSRow = null;
				DataRow newTaskRow = null;
				for(int i=0;i< iCount ;i++)
				{
					newTaskRow = task.GetNewRecord();
					newTaskRow["WBSCode"] = drs[i]["NewWBSCode"];

					newTaskRow["FullCode"] = drs[i]["NewFullCode"];

					newTaskRow["ProjectCode"] = projectCode;
					newTaskRow["TaskName"] = drs[i]["TaskName"];
					newTaskRow["OutLineNumber"] = drs[i]["OutLineNumber"];
					newTaskRow["SortID"] = drs[i]["SortID"];
					newTaskRow["ParentCode"] = drs[i]["ParentCode"];

					newTaskRow["Deep"] = drs[i]["Deep"];

					newTaskRow["TaskCode"] = "";//DAL.EntityDAO.SystemManageDAO.GetNewSysCode("Task");

					newTaskRow["WBSCode"]= drs[i]["NewWBSCode"];
					newTaskRow["ProjectCode"]= projectCode;

					newTaskRow["PlannedStartDate"] = drs[i]["PlannedStartDate"];
					newTaskRow["PlannedFinishDate"] = drs[i]["PlannedFinishDate"];

					/*
					if ( drs[i].IsNull("PlannedStartDate"))
						newTaskRow["PlannedStartDate"]= System.DBNull.Value;
					else
						newTaskRow["PlannedStartDate"]= ((DateTime)drs[i]["PlannedStartDate"]).AddDays( iSPDate) ;

					if ( drs[i].IsNull("PlannedFinishDate"))
						newTaskRow["PlannedFinishDate"]= System.DBNull.Value;
					else
						newTaskRow["PlannedFinishDate"]= ((DateTime) drs[i]["PlannedFinishDate"]).AddDays(iSPDate);

					if ( drs[i].IsNull("ActualStartDate"))
						newTaskRow["ActualStartDate"]= System.DBNull.Value;
					else
						newTaskRow["ActualStartDate"]= ((DateTime)drs[i]["ActualStartDate"]).AddDays( iSPDate) ;

					if ( drs[i].IsNull("ActualFinishDate"))
						newTaskRow["ActualFinishDate"]= System.DBNull.Value;
					else
						newTaskRow["ActualFinishDate"]= ((DateTime) drs[i]["ActualFinishDate"]).AddDays(iSPDate);

					if ( drs[i].IsNull("EarlyFinishDate") )
						newTaskRow["EarlyFinishDate"]= System.DBNull.Value;
					else
						newTaskRow["EarlyFinishDate"]= ((DateTime)drs[i]["EarlyFinishDate"]).AddDays(iSPDate);

					if ( drs[i].IsNull("EarlyStartDate"))
						newTaskRow["EarlyStartDate"] = System.DBNull.Value;
					else
						newTaskRow["EarlyStartDate"]= ((DateTime)drs[i]["EarlyStartDate"]).AddDays(iSPDate);

					if ( drs[i].IsNull("LastFinishDate"))
						newTaskRow["LastFinishDate"]= System.DBNull.Value;
					else
						newTaskRow["LastFinishDate"]= ((DateTime)drs[i]["LastFinishDate"]).AddDays(iSPDate);

					if ( drs[i].IsNull("LastStartDate"))
						newTaskRow["LastStartDate"]= System.DBNull.Value;
					else
						newTaskRow["LastStartDate"]= ((DateTime)drs[i]["LastStartDate"]).AddDays(iSPDate);
					*/

					newTaskRow["Duration"] = drs[i]["Duration"];
					newTaskRow["Remark"] = drs[i]["Remark"];
					newTaskRow["Status"] = 0;
					newTaskRow["Flag"] = 0;
					newTaskRow["ImportantLevel"] = drs[i]["ImportantLevel"];
					newTaskRow["CompletePercent"] = 0;
					newTaskRow["ImageFileName"] = drs[i]["ImageFileName"];

					task.AddNewRecord(newTaskRow);

					// �����ԴȨ��
					// ������Դ
					//this.SaveRS(drs[i]["NewWBSCode"].ToString(),strUser,strStation,"070101,070102,070103,070104,070105,070106,070107,070108,070109,070110");// ��ʼӵ�й�����ȫ��Ȩ��
				}

				DAL.EntityDAO.WBSDAO.InsertTask(task);
				task.Dispose();
			}
			entity.Dispose();

		}

		private void SaveRS(string strWBS,string strUser,string strStation,string strOption)
		{
			
			ArrayList arOperator = new ArrayList();
			if(strUser.Length>0)
			{
				foreach(string strTUser in strUser.Split(','))
				{
					if(strTUser=="") continue;
					AccessRange acRang = new AccessRange();
					acRang.AccessRangeType = 0;
					acRang.RelationCode = strTUser;
					acRang.Operations = strOption;
					arOperator.Add(acRang);
				}
			}			
			if(strStation.Length>0)
			{
				foreach(string strTStation in strStation.Split(','))
				{
					if(strTStation=="") continue;
					AccessRange acRang = new AccessRange();
					acRang.AccessRangeType = 1;
					acRang.RelationCode = strStation;
					acRang.Operations = strOption;
					arOperator.Add(acRang);
				}
			}
			
			if(arOperator.Count>0)
				BLL.ResourceRule.SetResourceAccessRange(strWBS,strOption.Substring(0,4),"",arOperator);
		}


		private void DeleteStandard_WBS( string wbsCode )
		{
			try
			{
				EntityData wbs = DAL.EntityDAO.WBSDAO.GetStandard_WBSByCode(wbsCode);
				DAL.EntityDAO.WBSDAO.DeleteStandard_WBS(wbs);
				wbs.Dispose();
			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			string templetCode=this.sltTemplet.Value;
			//����У��
			if ( templetCode == "" )
			{
				Response.Write( Rms.Web.JavaScript.Alert(true,"����ѡ��ģ�� ��") );
				return;
			}

			if ( this.dtbProjectStartDate.Text == "" )
			{
				Response.Write( Rms.Web.JavaScript.Alert(true,"����ָ����Ŀ������ʱ�� ��") );
				return;
			}

			try
			{
				InData(templetCode);

				Response.Write(JavaScript.ScriptStart);
				Response.Write(JavaScript.OpenerReload(false));
				Response.Write("if (window.opener.opener) window.opener.opener.location = window.opener.opener.location;");
				//				Response.Write(JavaScript.Alert(false,"����ɹ� ��"));
				Response.Write(JavaScript.OpenerReload(false));
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);
			
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog (this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
			}
		}
	}
}
