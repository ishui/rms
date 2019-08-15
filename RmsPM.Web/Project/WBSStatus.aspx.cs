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
using Rms.Web;
using System.Text;
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSStatus 的摘要说明。
	/// </summary>
	public partial class WBSStatus : PageBase
	{
		private string strStatus = "";
		private string strWBSCode = "";

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			try
			{
				InitPage();
				LoadData();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"维护状态失败");
			}
		}

		private void InitPage()
		{
			this.strStatus = Request["Status"]+"";
			this.strWBSCode = Request["WBSCode"]+"";
			ViewState["ProjectCode"] = Request["ProjectCode"].ToString();
			
			switch(this.strStatus)
			{
				case "Start":
					Start();
					break;
				case "Pause":
					Pause();
					break;
				case "Retart":
					Restart();
					break;
				case "Cancel":
					Cancel();
					break;
				case "Finish":
					Finish();
					break;
				default:
					break;
			}

		}

		private void Start()
		{
			this.lblTitle.Text = "开始工作";
			this.lblDate.Text = "开始时间";
			this.trReason.Visible = false;
			this.trTask.Visible = false;
		}
		private void Pause()
		{
			this.lblTitle.Text = "暂停工作";
			this.lblDate.Text = "暂停时间";
			this.lblReason.Text = "暂停原因";
			this.trTask.Visible = false;
		}
		private void Restart()
		{
			this.lblTitle.Text = "继续工作";
			this.lblTask.Text = "任务项";
			this.trReason.Visible = false;
			this.trTime.Visible = false;
		}
		private void Cancel()
		{
			this.lblTitle.Text = "取消工作";
			this.lblDate.Text = "取消时间";
			this.lblReason.Text = "取消原因";
			this.trTask.Visible = false;
		}
		private void Finish()
		{
			this.lblTitle.Text = "完成工作";
			this.lblDate.Text = "完成时间";
			this.trReason.Visible = false;
			this.trTask.Visible = false;
		}

		private void LoadData()
		{
			if(this.strStatus=="Retart"&&!this.IsPostBack)
			{
				WBSStrategyBuilder asb = new WBSStrategyBuilder();
				ArrayList arA = new ArrayList();
				arA.Add("070107");
				arA.Add(base.user.UserCode);
				asb.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.AccessRange,arA));
				asb.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.ProjectCode,(string)ViewState["ProjectCode"]));
				asb.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.ParentCode,this.strWBSCode));
				asb.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.PreStatusNot,"5"));// 取得的是暂停前未开始和开始项
				asb.AddOrder(" PlannedStartDate ",false);
				string sql = asb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData dsTask = qa.FillEntityData("Task",sql);
				qa.Dispose();
				this.dgTaskList.DataSource = (dsTask.Tables[0].Rows.Count > 0 )?DisposeTask(dsTask.CurrentTable):null;
				this.dgTaskList.DataBind();

				if(dsTask.CurrentTable.Rows.Count > 0)
				{
					this.lblTask.Text = "工作项";
					this.lblNoTask.Visible = false;				
				}
				else
				{
					this.divTask.Visible = false;
					this.lblNoTask.Text = "没有需要继续的子项工作,可以继续本项工作";
				}
			}
		}

		private DataTable DisposeTask(System.Data.DataTable dtTask)
		{
			try
			{
				DataTable dtNew = dtTask.Copy();
				dtNew.Columns.Add("StatusName",System.Type.GetType("System.String"));
				dtNew.Columns.Add("ImportantName");
				dtNew.Columns.Add("Master");
				EntityData entityUser = new EntityData("TaskPerson");
				for ( int i = 0 ; i < dtNew.Rows.Count ; i++)
				{	
					dtNew.Rows[i]["ImportantName"] = (dtNew.Rows[i]["ImportantLevel"] == System.DBNull.Value)?"":ComSource.GetImportantName(dtNew.Rows[i]["ImportantLevel"].ToString());
					entityUser = WBSDAO.GetTaskPersonByWBSCode(dtNew.Rows[i]["WBSCode"].ToString());
					string strTUser = "";// 取得当前任务负责人					
					for (int j = 0; j < entityUser.CurrentTable.Rows.Count; j++)
					{
						if (entityUser.CurrentTable.Rows[j]["Type"].ToString() == "2") // 负责
						{
							strTUser +=(strTUser == "")?"":",";
							strTUser = BLL.SystemRule.GetUserName(entityUser.CurrentTable.Rows[j]["UserCode"].ToString());
						}						
					}					
					dtNew.Rows[i]["Master"] = strTUser;

					string strTxt = this.GetStatusImg(dtNew.Rows[i]["Status"].ToString())+"&nbsp;&nbsp;"+dtNew.Rows[i]["SortID"].ToString()+"&nbsp;&nbsp;"+dtNew.Rows[i]["TaskName"].ToString();					
					// 此处只有负责人可以更改状态，所以此处负责人拥有子项的权限，不必再次判断权限
//					string strTUsers = "";
//					string strTUserNames = "";
//					string strTStations = "";
//					string strTStationNames = "";
//					BLL.ResourceRule.GetAccessRange(dtNew.Rows[i]["WBSCode"].ToString(),"0701","070107",ref strTUsers,ref strTUserNames,ref strTStations,ref strTStationNames);
//					bool isIN = false;
//					string strBuildStations = base.user.BuildStationCodes();
//					foreach(string str in strBuildStations.Split(','))
//					{
//						if(strTStations.IndexOf(str)>-1)
//						{
//							isIN = true;break;
//						}
//					}
//					if(strTUsers.IndexOf(base.user.UserCode)<0&&!isIN) // 无权
//						dtNew.Rows[i]["StatusName"] = strTxt;
//					else
						dtNew.Rows[i]["StatusName"] = "<a href=\"javascript:OpenTask('"+dtNew.Rows[i]["WBSCode"].ToString()+"');\">"+strTxt+"</a>";

				}
				entityUser.Dispose();
				return dtNew;
				
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return null;
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

		public void StartProcess(string strWBSCode,string strActualStartDate)
		{
			// 更新开始时间	// 直系父项开始，如果父项没有开始，则更新为开始，是否提醒父项相关人？？？
			EntityData entity = WBSDAO.GetV_TaskByCode(strWBSCode);			
			DataRow dr = entity.CurrentTable.Rows[0];
			string strFullCode = dr["FullCode"].ToString();
			entity.Dispose();
			string[] arCode = strFullCode.Split('-');
			foreach(string strCode in arCode)
			{
				EntityData entityUpDate = WBSDAO.GetV_TaskByCode(strCode);
				// 未开始,暂停的节点更新时间
				string strTmp = entityUpDate.CurrentTable.Rows[0]["Status"].ToString();
				if(strTmp=="0"||strTmp=="2") 	
					entityUpDate.CurrentTable.Rows[0]["ActualStartDate"] = DateTime.Parse(strActualStartDate);
				if(entityUpDate.CurrentTable.Rows[0]["Status"].ToString()!="1")
				// 状态变更提醒相关人员
					this.Remind(entityUpDate.CurrentTable.Rows[0]["TaskName"].ToString(),strCode);				
				entityUpDate.CurrentTable.Rows[0]["Status"] = "1";
				WBSDAO.UpdateTask(entityUpDate);
				entityUpDate.Dispose();				
			}			
		}
		public void PauseProcess(string strWBSCode,string strProjectCode,string strReason,string strPauseDate)
		{
			// 更新状态
			EntityData entity = WBSDAO.GetTaskByCode(strWBSCode);
			string strFullCode = entity.CurrentTable.Rows[0]["FullCode"].ToString();
			entity.CurrentTable.Rows[0]["PauseDate"] = DateTime.Parse(strPauseDate);
			entity.CurrentTable.Rows[0]["PauseReason"] = strReason;
			WBSDAO.UpdateTask(entity);
			entity.Dispose();
			EntityData entityAll = WBSDAO.GetTaskByProject(strProjectCode);
			for(int i=0 ;i<entityAll.CurrentTable.Rows.Count;i++)
			{
				if(entityAll.CurrentTable.Rows[i]["FullCode"].ToString().IndexOf(strFullCode)!=-1)
				{
					EntityData entityUpDate = WBSDAO.GetTaskByCode(entityAll.CurrentTable.Rows[i]["WBSCode"].ToString());
					// 更新未开始和进行的子项
					string strTmp = entityUpDate.CurrentTable.Rows[0]["Status"].ToString();
					if(strTmp=="0"||strTmp=="1")
					{
						this.Remind(entityUpDate.CurrentTable.Rows[0]["TaskName"].ToString(),entityUpDate.CurrentTable.Rows[0]["WBSCode"].ToString());
						entityUpDate.CurrentTable.Rows[0]["PreStatus"] = entityUpDate.CurrentTable.Rows[0]["Status"].ToString();
						entityUpDate.CurrentTable.Rows[0]["Status"] = "2";
					}	
					else
						entityUpDate.CurrentTable.Rows[0]["PreStatus"] = "5";// 此处5代表没有保存过前一种状态
					WBSDAO.UpdateTask(entityUpDate);
					entityUpDate.Dispose();
				}
			}
			entityAll.Dispose();
		}

		/// <summary>
		/// 公用方法处理重新开始的方法
		/// </summary>
		/// <param name="strWBSCode"></param>
		/// <param name="strProjectCode"></param>
		public void RestartPrcecss(string strWBSCode,string strProjectCode)
		{
			// 直系父项恢复为进行中
			EntityData entity = WBSDAO.GetTaskByCode(strWBSCode);			
			string strFullCode = entity.CurrentTable.Rows[0]["FullCode"].ToString();
			entity.Dispose();
			string[] arCode = strFullCode.Split('-');
			foreach(string strCode in arCode)
			{
				EntityData entityUpDate = WBSDAO.GetTaskByCode(strCode);
				if(entityUpDate.CurrentTable.Rows[0]["Status"].ToString()!="1")
					// 状态变更提醒相关人员
					this.Remind(entityUpDate.CurrentTable.Rows[0]["TaskName"].ToString(),strCode);				
				entityUpDate.CurrentTable.Rows[0]["Status"] = "1";
				WBSDAO.UpdateTask(entityUpDate);
				entityUpDate.Dispose();				
			}	
			// 子项的状态恢复
			EntityData entity1 = WBSDAO.GetTaskByCode(strWBSCode);
			strFullCode = entity1.CurrentTable.Rows[0]["FullCode"].ToString();
			entity1.Dispose();
			EntityData entityAll1 = WBSDAO.GetTaskByProject(strProjectCode);
			for(int i=0 ;i<entityAll1.CurrentTable.Rows.Count;i++)
			{
				if(entityAll1.CurrentTable.Rows[i]["FullCode"].ToString().IndexOf(strFullCode)!=-1)
				{
					EntityData entityUpDate1 = WBSDAO.GetTaskByCode(entityAll1.CurrentTable.Rows[i]["WBSCode"].ToString());
					// 更新未开始和进行的子项
					string strTmp = entityUpDate1.CurrentTable.Rows[0]["PreStatus"].ToString();
					if(strTmp!="5")
					{
						this.Remind(entityUpDate1.CurrentTable.Rows[0]["TaskName"].ToString(),entityUpDate1.CurrentTable.Rows[0]["WBSCode"].ToString());
						entityUpDate1.CurrentTable.Rows[0]["Status"] = entityUpDate1.CurrentTable.Rows[0]["PreStatus"].ToString();
						entityUpDate1.CurrentTable.Rows[0]["PreStatus"] = "5";
						
					}					
					WBSDAO.UpdateTask(entityUpDate1);
					entityUpDate1.Dispose();
				}
			}
			entityAll1.Dispose();
		}

		/// <summary>
		/// 本页面当用户选择可以继续的子项的时候的处理方法
		/// </summary>
		/// <param name="strWBSCode"></param>
		/// <param name="strProjectCode"></param>
		public void RestartPrcecssClick(string strWBSCode,string strProjectCode)
		{
			// 直系父项恢复为进行中
			EntityData entity = WBSDAO.GetTaskByCode(strWBSCode);			
			string strFullCode = entity.CurrentTable.Rows[0]["FullCode"].ToString();
			entity.Dispose();

			string[] arCode = strFullCode.Split('-');
			foreach(string strCode in arCode)
			{
				EntityData entityUpDate = WBSDAO.GetTaskByCode(strCode);
				if(entityUpDate.CurrentTable.Rows[0]["Status"].ToString()!="1")
					// 状态变更提醒相关人员
					this.Remind(entityUpDate.CurrentTable.Rows[0]["TaskName"].ToString(),strCode);				
				entityUpDate.CurrentTable.Rows[0]["Status"] = "1";
				WBSDAO.UpdateTask(entityUpDate);
				entityUpDate.Dispose();				
			}	

			// 子项的状态恢复
			EntityData entity1 = WBSDAO.GetTaskByCode(strWBSCode);
			strFullCode = entity1.CurrentTable.Rows[0]["FullCode"].ToString();
			entity1.Dispose();

			// 获取用户选择的重新开始的子任务
			string strRestartTask = GetRestartTask();
			if(strRestartTask.Length>0)
			{
				EntityData entityAll1 = WBSDAO.GetTaskByProject(strProjectCode);
				for(int i=0 ;i<entityAll1.CurrentTable.Rows.Count;i++)
				{
					if(entityAll1.CurrentTable.Rows[i]["FullCode"].ToString().IndexOf(strFullCode)!=-1)
					{
						EntityData entityUpDate1 = WBSDAO.GetTaskByCode(entityAll1.CurrentTable.Rows[i]["WBSCode"].ToString());
						// 更新未开始和进行的子项
						string strTmp = entityUpDate1.CurrentTable.Rows[0]["PreStatus"].ToString();
						if(strTmp!="5")
						{
							// 如果当前Task在选择的需要继续的Task中，则继续本项，
							if(strRestartTask.IndexOf(entityUpDate1.CurrentTable.Rows[0]["WBSCode"].ToString())>-1)
							{
								this.Remind(entityUpDate1.CurrentTable.Rows[0]["TaskName"].ToString(),entityUpDate1.CurrentTable.Rows[0]["WBSCode"].ToString());
								entityUpDate1.CurrentTable.Rows[0]["Status"] = entityUpDate1.CurrentTable.Rows[0]["PreStatus"].ToString();
								entityUpDate1.CurrentTable.Rows[0]["PreStatus"] = "5";
							}						
						}					
						WBSDAO.UpdateTask(entityUpDate1);
						entityUpDate1.Dispose();
					}
				}
				entityAll1.Dispose();
			}
		}

		public void CancelProcess(string strWBSCode,string strReason,string strCancelDate,string strProjectCode)
		{
			if(!this.IsAllFinish(strWBSCode,strProjectCode))
			{
				this.JSAlert("还有一些子项没有结束，所以当前项目不能取消");
				return ;
			}
			EntityData entity = WBSDAO.GetTaskByCode(strWBSCode);
			entity.CurrentTable.Rows[0]["ActualFinishDate"] = strCancelDate;	
			entity.CurrentTable.Rows[0]["CancelDate"] = strCancelDate;
			entity.CurrentTable.Rows[0]["CancelReason"] = strReason;
			entity.CurrentTable.Rows[0]["Status"] = "3";	
			this.Remind(entity.CurrentTable.Rows[0]["TaskName"].ToString(),strWBSCode);	
			WBSDAO.UpdateTask(entity);
			entity.Dispose();
		}

		public void FinishProcess(string strWBSCode,string strFinishDate,string strProjectCode)
		{
			if(!this.IsAllFinish(strWBSCode,strProjectCode))
			{
				this.JSAlert("还有一些子项没有结束，所以当前项目不能结束");
				return ;
			}
			EntityData entity = WBSDAO.GetTaskByCode(strWBSCode);
			string strStatus = entity.CurrentTable.Rows[0]["Status"].ToString();

//			if(strStatus=="0") // 如果从未开始结束，则开始时间也是当前时间
			if (entity.CurrentTable.Rows[0]["ActualStartDate"] == DBNull.Value)
				entity.CurrentTable.Rows[0]["ActualStartDate"] = DateTime.Parse(strFinishDate);	

			entity.CurrentTable.Rows[0]["Status"] = "4";
			entity.CurrentTable.Rows[0]["CompletePercent"] = 100;	

			if (entity.CurrentTable.Rows[0]["ActualFinishDate"] == DBNull.Value)
				entity.CurrentTable.Rows[0]["ActualFinishDate"] = DateTime.Parse(strFinishDate);

			this.Remind(entity.CurrentTable.Rows[0]["TaskName"].ToString(),strWBSCode);	
			WBSDAO.UpdateTask(entity);
			entity.Dispose();				
		}

		/// <summary>
		/// 工作状态变更提醒
		/// </summary>
		public void Remind(string strTaskName,string strWBSCode)
		{
			// 检测是否已经定义了提醒
			EntityData entityRemind =DAL.EntityDAO.RemindDAO.GetAllRemindStrategy();
			DataRow[] ardr = entityRemind.CurrentTable.Select("Type='3' and IsActive='1' and ProjectCode='"+(string)ViewState["ProjectCode"]+"'");
			for(int m=0;m<ardr.Length;m++)
			{
				// 取得定义的提醒人员类型
				string strRemindType = ardr[m]["ObjectCode"].ToString();
				double strRemindDay = double.Parse(ardr[m]["RemindDay"].ToString());

				EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(strWBSCode);
				if (entityUser.HasRecord())
				{
					DataTable dtUserNew = entityUser.CurrentTable;
					for (int i = 0; i < dtUserNew.Rows.Count; i++)
					{
						if(strRemindType.IndexOf('0')>-1&&dtUserNew.Rows[i]["Type"].ToString()=="0")//0参与，1监督，2负责，3工作报告分发范围
						{
							if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // 0代表人，1代表岗位
								// 3代表工作变更提醒的定义
								RmsPM.Web.Remind.RemindModify.SaveNewRemind("3",strWBSCode,dtUserNew.Rows[i]["UserCode"].ToString(),strTaskName,DateTime.Now,DateTime.Now.AddDays(strRemindDay)); 
							if(dtUserNew.Rows[i]["RoleType"].ToString()=="1")
							{
								// 取得某个岗位下的所有人
								EntityData entityRemindUser = BLL.SystemRule.GetUserByStation(dtUserNew.Rows[i]["UserCode"].ToString());
								if(entityRemindUser.HasRecord())
								{
									DataTable dt = entityRemindUser.CurrentTable;
									for(int j=0;j<dt.Rows.Count;j++)
									{
										RmsPM.Web.Remind.RemindModify.SaveNewRemind("3",strWBSCode,dt.Rows[j]["UserCode"].ToString(),strTaskName,DateTime.Now,DateTime.Now.AddDays(strRemindDay)); 
									}									
								}
							}
						}
						if(strRemindType.IndexOf('1')>-1&&dtUserNew.Rows[i]["Type"].ToString()=="1")//0参与，1监督，2负责，3工作报告分发范围
						{
							if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // 0代表人，1代表岗位
								// 3代表工作变更提醒的定义
								RmsPM.Web.Remind.RemindModify.SaveNewRemind("3",strWBSCode,dtUserNew.Rows[i]["UserCode"].ToString(),strTaskName,DateTime.Now,DateTime.Now.AddDays(strRemindDay)); 
							if(dtUserNew.Rows[i]["RoleType"].ToString()=="1")
							{
								// 取得某个岗位下的所有人
								EntityData entityRemindUser = BLL.SystemRule.GetUserByStation(dtUserNew.Rows[i]["UserCode"].ToString());
								if(entityRemindUser.HasRecord())
								{
									DataTable dt = entityRemindUser.CurrentTable;
									for(int j=0;j<dt.Rows.Count;j++)
									{
										RmsPM.Web.Remind.RemindModify.SaveNewRemind("3",strWBSCode,dt.Rows[j]["UserCode"].ToString(),strTaskName,DateTime.Now,DateTime.Now.AddDays(strRemindDay)); 
									}									
								}
							}
						}
						if(strRemindType.IndexOf('2')>-1&&dtUserNew.Rows[i]["Type"].ToString()=="2")//0参与，1监督，2负责，3工作报告分发范围
						{
							if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // 0代表人，1代表岗位
								// 3代表工作变更提醒的定义
								RmsPM.Web.Remind.RemindModify.SaveNewRemind("3",strWBSCode,dtUserNew.Rows[i]["UserCode"].ToString(),strTaskName,DateTime.Now,DateTime.Now.AddDays(strRemindDay)); 
							if(dtUserNew.Rows[i]["RoleType"].ToString()=="1")
							{
								// 取得某个岗位下的所有人
								EntityData entityRemindUser = BLL.SystemRule.GetUserByStation(dtUserNew.Rows[i]["UserCode"].ToString());
								if(entityRemindUser.HasRecord())
								{
									DataTable dt = entityRemindUser.CurrentTable;
									for(int j=0;j<dt.Rows.Count;j++)
									{
										RmsPM.Web.Remind.RemindModify.SaveNewRemind("3",strWBSCode,dt.Rows[j]["UserCode"].ToString(),strTaskName,DateTime.Now,DateTime.Now.AddDays(strRemindDay)); 
									}									
								}
							}
						}
					}
				}
				entityUser.Dispose();
			}
			entityRemind.Dispose();
//			EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(strWBSCode);
//			if (entityUser.HasRecord())
//			{
//				DataTable dtUserNew = entityUser.CurrentTable;
//				for (int i = 0; i < dtUserNew.Rows.Count; i++)
//				{
//					// dtUserNew.Rows[i]["Type"].ToString() 为0参与，1监督，2负责，3工作报告分发范围
//					if(dtUserNew.Rows[i]["Type"].ToString()!="3")
//					{
//						// 对当前用户类型所在的岗位的提醒天数
//						double dblDurDays = 0.0;
//						RmsPM.Web.Desktop myRemind = new Desktop();
//						// 取得岗位
//						string strRole = myRemind.GetUserStation(dtUserNew.Rows[i]["UserCode"].ToString());
//						// 根据岗位取得设定天数
//						EntityData entityRemind =DAL.EntityDAO.RemindDAO.GetRemindStrategyByRoleCode(strRole);
//						if (entityRemind.HasRecord())
//						{
//							dblDurDays = double.Parse(entityRemind.GetInt("RemindDay").ToString());
//						}
//						myRemind.SaveNewRemind("3",strWBSCode,dtUserNew.Rows[i]["UserCode"].ToString(),strTaskName,DateTime.Now,DateTime.Now.AddDays(dblDurDays)); // 3代表工作变更提醒的定义
//					}
//				}
//			}
//			entityUser.Dispose();	
		}

		public bool IsAllFinish(string strWBSCode,string strProjectCode)
		{
			bool bleFinish = true;
			EntityData entity = WBSDAO.GetTaskByCode(strWBSCode);
			if (entity.HasRecord()) 
			{
				string strFullCode = entity.CurrentTable.Rows[0]["FullCode"].ToString();
				EntityData entityAll = WBSDAO.GetTaskByProject(strProjectCode);			
				for(int i=0 ;i<entityAll.CurrentTable.Rows.Count;i++)
				{
					if(entityAll.CurrentTable.Rows[i]["FullCode"].ToString().IndexOf(strFullCode)!=-1&&strFullCode!=entityAll.CurrentTable.Rows[i]["FullCode"].ToString())
					{
						EntityData entityUpDate = WBSDAO.GetTaskByCode(entityAll.CurrentTable.Rows[i]["WBSCode"].ToString());
						string strTmp = entityUpDate.CurrentTable.Rows[0]["Status"].ToString();
						if(strTmp=="1"||strTmp=="2")// 1进行中，2，暂停
						{
							bleFinish = false;	
							break;
						}
					}
				}
			}
			entity.Dispose();
			return bleFinish;
		}
		private void JSAlert(string strInfo)
		{
			Response.Write(JavaScript.ScriptStart);
			Response.Write("alert('"+strInfo+"');");
			Response.Write(JavaScript.ScriptEnd);
		}

		protected void btConfirm_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				switch(this.strStatus)
				{
					case "Start":
						StartProcess(this.strWBSCode,this.crDate.Value);
						break;
					case "Pause":
						PauseProcess(this.strWBSCode,(string)ViewState["ProjectCode"],this.txtReason.Text,this.crDate.Value);
						break;
					case "Retart":
						RestartPrcecssClick(this.strWBSCode,(string)ViewState["ProjectCode"]);
						break;
					case "Cancel":
						CancelProcess(this.strWBSCode,this.txtReason.Text,this.crDate.Value,(string) ViewState["ProjectCode"]);
						break;
					case "Finish":
						FinishProcess(this.strWBSCode,this.crDate.Value,(string) ViewState["ProjectCode"]);
						break;
					default:
						break;
				}
				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.opener.SelectTask();");
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"状态更改失败");
			}
		}
		private string GetRestartTask()
		{
			System.Web.UI.WebControls.CheckBox chkTask = new CheckBox();
			StringBuilder strBuilder = new StringBuilder();
			foreach(DataGridItem oItem in this.dgTaskList.Items)
			{
				chkTask = (CheckBox)oItem.FindControl("chkTask");
				if (chkTask.Checked == true)
				{
					strBuilder.Append(this.dgTaskList.DataKeys[oItem.ItemIndex].ToString());
					strBuilder.Append(",");
				}
			}
			return strBuilder.ToString();
		}

		private string GetStatusImg(string strVal)
		{
			switch(strVal)
			{
				case "0":
					return "<img src=\"../Images/icon_unbegin.gif\" title=\"未开始工作\" border=0>";
				case "1":
					return "<img src=\"../Images/icon_going.gif\" title=\"进行中工作\" border=0>";
				case "2":
					return "<img src=\"../Images/icon_pause.gif\" title=\"已暂停工作\" border=0>";
				case "3":
					return "<img src=\"../Images/icon_cancel.gif\" title=\"已取消工作\" border=0>";
				case "4":
					return "<img src=\"../Images/icon_over.gif\" title=\"已完成工作\" border=0>";
				default:
					return "<img src=\"../Images/icon_unbegin.gif\" title=\"未开始工作\" border=0>";
			}
		}
	}
}
