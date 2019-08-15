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
	/// 工作分解树导入
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
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化页面出错");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
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

		private void InData(string templetCode)
		{
			ViewState["ProjectCode"] = Request["ProjectCode"].ToString();
			string projectCode = (string)ViewState["ProjectCode"];

			//EntityData del=WBSDAO.GetTaskByProject((string)ViewState["ProjectCode"]);
			//获取当前工作项结构，并删除之
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
				// 取任务项中最早的计划开始时间作为项目的开始时间， 
				// 用来计算与新项目的开始时间的相对值
				DateTime dateBaseStart;
				DataRow[] rowDate = entity.CurrentTable.Select("PlannedStartDate is not null","PlannedStartDate");
				if (rowDate.Length > 0)
				{
					dateBaseStart = (DateTime)rowDate[0]["PlannedStartDate"];
				}

				DateTime dateNewStartDate = DateTime.Parse( this.dtbProjectStartDate.Value);
				TimeSpan bts = dateNewStartDate - dateBaseStart ;
				int iSPDate = bts.Days;							// 新老项目的时间错误天数
				*/

				// 按照Deep顺序生成新的WBSCode和FullCode
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

				// 取得根节点人权限
				string strUser = "";
				string strStation = "";
				EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(ProjectWBSCode);
				if (entityUser.HasRecord())
				{
					DataTable dtUserNew = entityUser.CurrentTable.Copy();					
					for (int i = 0; i < dtUserNew.Rows.Count; i++)
					{
						if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // 类型为人
						{
							if (dtUserNew.Rows[i]["Type"].ToString() == "2") // 负责人
							{
								strUser +=(strUser == "")?"":",";
								strUser += dtUserNew.Rows[i]["UserCode"].ToString();
							}							
						}
						if(dtUserNew.Rows[i]["RoleType"].ToString()=="1") // 类型为岗位
						{
							if (dtUserNew.Rows[i]["Type"].ToString() == "2") // 负责人
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

					// 添加资源权限
					// 保存资源
					//this.SaveRS(drs[i]["NewWBSCode"].ToString(),strUser,strStation,"070101,070102,070103,070104,070105,070106,070107,070108,070109,070110");// 初始拥有工作的全部权限
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
			//数据校验
			if ( templetCode == "" )
			{
				Response.Write( Rms.Web.JavaScript.Alert(true,"必须选定模板 ！") );
				return;
			}

			if ( this.dtbProjectStartDate.Text == "" )
			{
				Response.Write( Rms.Web.JavaScript.Alert(true,"必须指定项目的启动时间 ！") );
				return;
			}

			try
			{
				InData(templetCode);

				Response.Write(JavaScript.ScriptStart);
				Response.Write(JavaScript.OpenerReload(false));
				Response.Write("if (window.opener.opener) window.opener.opener.location = window.opener.opener.location;");
				//				Response.Write(JavaScript.Alert(false,"导入成功 ！"));
				Response.Write(JavaScript.OpenerReload(false));
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);
			
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog (this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "导入出错：" + ex.Message));
			}
		}
	}
}
