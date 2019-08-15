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
	/// WBSStatus ��ժҪ˵����
	/// </summary>
	public partial class WBSStatus : PageBase
	{
		private string strStatus = "";
		private string strWBSCode = "";

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			try
			{
				InitPage();
				LoadData();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"ά��״̬ʧ��");
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
			this.lblTitle.Text = "��ʼ����";
			this.lblDate.Text = "��ʼʱ��";
			this.trReason.Visible = false;
			this.trTask.Visible = false;
		}
		private void Pause()
		{
			this.lblTitle.Text = "��ͣ����";
			this.lblDate.Text = "��ͣʱ��";
			this.lblReason.Text = "��ͣԭ��";
			this.trTask.Visible = false;
		}
		private void Restart()
		{
			this.lblTitle.Text = "��������";
			this.lblTask.Text = "������";
			this.trReason.Visible = false;
			this.trTime.Visible = false;
		}
		private void Cancel()
		{
			this.lblTitle.Text = "ȡ������";
			this.lblDate.Text = "ȡ��ʱ��";
			this.lblReason.Text = "ȡ��ԭ��";
			this.trTask.Visible = false;
		}
		private void Finish()
		{
			this.lblTitle.Text = "��ɹ���";
			this.lblDate.Text = "���ʱ��";
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
				asb.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.PreStatusNot,"5"));// ȡ�õ�����ͣǰδ��ʼ�Ϳ�ʼ��
				asb.AddOrder(" PlannedStartDate ",false);
				string sql = asb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData dsTask = qa.FillEntityData("Task",sql);
				qa.Dispose();
				this.dgTaskList.DataSource = (dsTask.Tables[0].Rows.Count > 0 )?DisposeTask(dsTask.CurrentTable):null;
				this.dgTaskList.DataBind();

				if(dsTask.CurrentTable.Rows.Count > 0)
				{
					this.lblTask.Text = "������";
					this.lblNoTask.Visible = false;				
				}
				else
				{
					this.divTask.Visible = false;
					this.lblNoTask.Text = "û����Ҫ�����������,���Լ��������";
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
					string strTUser = "";// ȡ�õ�ǰ��������					
					for (int j = 0; j < entityUser.CurrentTable.Rows.Count; j++)
					{
						if (entityUser.CurrentTable.Rows[j]["Type"].ToString() == "2") // ����
						{
							strTUser +=(strTUser == "")?"":",";
							strTUser = BLL.SystemRule.GetUserName(entityUser.CurrentTable.Rows[j]["UserCode"].ToString());
						}						
					}					
					dtNew.Rows[i]["Master"] = strTUser;

					string strTxt = this.GetStatusImg(dtNew.Rows[i]["Status"].ToString())+"&nbsp;&nbsp;"+dtNew.Rows[i]["SortID"].ToString()+"&nbsp;&nbsp;"+dtNew.Rows[i]["TaskName"].ToString();					
					// �˴�ֻ�и����˿��Ը���״̬�����Դ˴�������ӵ�������Ȩ�ޣ������ٴ��ж�Ȩ��
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
//					if(strTUsers.IndexOf(base.user.UserCode)<0&&!isIN) // ��Ȩ
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

		public void StartProcess(string strWBSCode,string strActualStartDate)
		{
			// ���¿�ʼʱ��	// ֱϵ���ʼ���������û�п�ʼ�������Ϊ��ʼ���Ƿ����Ѹ�������ˣ�����
			EntityData entity = WBSDAO.GetV_TaskByCode(strWBSCode);			
			DataRow dr = entity.CurrentTable.Rows[0];
			string strFullCode = dr["FullCode"].ToString();
			entity.Dispose();
			string[] arCode = strFullCode.Split('-');
			foreach(string strCode in arCode)
			{
				EntityData entityUpDate = WBSDAO.GetV_TaskByCode(strCode);
				// δ��ʼ,��ͣ�Ľڵ����ʱ��
				string strTmp = entityUpDate.CurrentTable.Rows[0]["Status"].ToString();
				if(strTmp=="0"||strTmp=="2") 	
					entityUpDate.CurrentTable.Rows[0]["ActualStartDate"] = DateTime.Parse(strActualStartDate);
				if(entityUpDate.CurrentTable.Rows[0]["Status"].ToString()!="1")
				// ״̬������������Ա
					this.Remind(entityUpDate.CurrentTable.Rows[0]["TaskName"].ToString(),strCode);				
				entityUpDate.CurrentTable.Rows[0]["Status"] = "1";
				WBSDAO.UpdateTask(entityUpDate);
				entityUpDate.Dispose();				
			}			
		}
		public void PauseProcess(string strWBSCode,string strProjectCode,string strReason,string strPauseDate)
		{
			// ����״̬
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
					// ����δ��ʼ�ͽ��е�����
					string strTmp = entityUpDate.CurrentTable.Rows[0]["Status"].ToString();
					if(strTmp=="0"||strTmp=="1")
					{
						this.Remind(entityUpDate.CurrentTable.Rows[0]["TaskName"].ToString(),entityUpDate.CurrentTable.Rows[0]["WBSCode"].ToString());
						entityUpDate.CurrentTable.Rows[0]["PreStatus"] = entityUpDate.CurrentTable.Rows[0]["Status"].ToString();
						entityUpDate.CurrentTable.Rows[0]["Status"] = "2";
					}	
					else
						entityUpDate.CurrentTable.Rows[0]["PreStatus"] = "5";// �˴�5����û�б����ǰһ��״̬
					WBSDAO.UpdateTask(entityUpDate);
					entityUpDate.Dispose();
				}
			}
			entityAll.Dispose();
		}

		/// <summary>
		/// ���÷����������¿�ʼ�ķ���
		/// </summary>
		/// <param name="strWBSCode"></param>
		/// <param name="strProjectCode"></param>
		public void RestartPrcecss(string strWBSCode,string strProjectCode)
		{
			// ֱϵ����ָ�Ϊ������
			EntityData entity = WBSDAO.GetTaskByCode(strWBSCode);			
			string strFullCode = entity.CurrentTable.Rows[0]["FullCode"].ToString();
			entity.Dispose();
			string[] arCode = strFullCode.Split('-');
			foreach(string strCode in arCode)
			{
				EntityData entityUpDate = WBSDAO.GetTaskByCode(strCode);
				if(entityUpDate.CurrentTable.Rows[0]["Status"].ToString()!="1")
					// ״̬������������Ա
					this.Remind(entityUpDate.CurrentTable.Rows[0]["TaskName"].ToString(),strCode);				
				entityUpDate.CurrentTable.Rows[0]["Status"] = "1";
				WBSDAO.UpdateTask(entityUpDate);
				entityUpDate.Dispose();				
			}	
			// �����״̬�ָ�
			EntityData entity1 = WBSDAO.GetTaskByCode(strWBSCode);
			strFullCode = entity1.CurrentTable.Rows[0]["FullCode"].ToString();
			entity1.Dispose();
			EntityData entityAll1 = WBSDAO.GetTaskByProject(strProjectCode);
			for(int i=0 ;i<entityAll1.CurrentTable.Rows.Count;i++)
			{
				if(entityAll1.CurrentTable.Rows[i]["FullCode"].ToString().IndexOf(strFullCode)!=-1)
				{
					EntityData entityUpDate1 = WBSDAO.GetTaskByCode(entityAll1.CurrentTable.Rows[i]["WBSCode"].ToString());
					// ����δ��ʼ�ͽ��е�����
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
		/// ��ҳ�浱�û�ѡ����Լ����������ʱ��Ĵ�����
		/// </summary>
		/// <param name="strWBSCode"></param>
		/// <param name="strProjectCode"></param>
		public void RestartPrcecssClick(string strWBSCode,string strProjectCode)
		{
			// ֱϵ����ָ�Ϊ������
			EntityData entity = WBSDAO.GetTaskByCode(strWBSCode);			
			string strFullCode = entity.CurrentTable.Rows[0]["FullCode"].ToString();
			entity.Dispose();

			string[] arCode = strFullCode.Split('-');
			foreach(string strCode in arCode)
			{
				EntityData entityUpDate = WBSDAO.GetTaskByCode(strCode);
				if(entityUpDate.CurrentTable.Rows[0]["Status"].ToString()!="1")
					// ״̬������������Ա
					this.Remind(entityUpDate.CurrentTable.Rows[0]["TaskName"].ToString(),strCode);				
				entityUpDate.CurrentTable.Rows[0]["Status"] = "1";
				WBSDAO.UpdateTask(entityUpDate);
				entityUpDate.Dispose();				
			}	

			// �����״̬�ָ�
			EntityData entity1 = WBSDAO.GetTaskByCode(strWBSCode);
			strFullCode = entity1.CurrentTable.Rows[0]["FullCode"].ToString();
			entity1.Dispose();

			// ��ȡ�û�ѡ������¿�ʼ��������
			string strRestartTask = GetRestartTask();
			if(strRestartTask.Length>0)
			{
				EntityData entityAll1 = WBSDAO.GetTaskByProject(strProjectCode);
				for(int i=0 ;i<entityAll1.CurrentTable.Rows.Count;i++)
				{
					if(entityAll1.CurrentTable.Rows[i]["FullCode"].ToString().IndexOf(strFullCode)!=-1)
					{
						EntityData entityUpDate1 = WBSDAO.GetTaskByCode(entityAll1.CurrentTable.Rows[i]["WBSCode"].ToString());
						// ����δ��ʼ�ͽ��е�����
						string strTmp = entityUpDate1.CurrentTable.Rows[0]["PreStatus"].ToString();
						if(strTmp!="5")
						{
							// �����ǰTask��ѡ�����Ҫ������Task�У���������
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
				this.JSAlert("����һЩ����û�н��������Ե�ǰ��Ŀ����ȡ��");
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
				this.JSAlert("����һЩ����û�н��������Ե�ǰ��Ŀ���ܽ���");
				return ;
			}
			EntityData entity = WBSDAO.GetTaskByCode(strWBSCode);
			string strStatus = entity.CurrentTable.Rows[0]["Status"].ToString();

//			if(strStatus=="0") // �����δ��ʼ��������ʼʱ��Ҳ�ǵ�ǰʱ��
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
		/// ����״̬�������
		/// </summary>
		public void Remind(string strTaskName,string strWBSCode)
		{
			// ����Ƿ��Ѿ�����������
			EntityData entityRemind =DAL.EntityDAO.RemindDAO.GetAllRemindStrategy();
			DataRow[] ardr = entityRemind.CurrentTable.Select("Type='3' and IsActive='1' and ProjectCode='"+(string)ViewState["ProjectCode"]+"'");
			for(int m=0;m<ardr.Length;m++)
			{
				// ȡ�ö����������Ա����
				string strRemindType = ardr[m]["ObjectCode"].ToString();
				double strRemindDay = double.Parse(ardr[m]["RemindDay"].ToString());

				EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(strWBSCode);
				if (entityUser.HasRecord())
				{
					DataTable dtUserNew = entityUser.CurrentTable;
					for (int i = 0; i < dtUserNew.Rows.Count; i++)
					{
						if(strRemindType.IndexOf('0')>-1&&dtUserNew.Rows[i]["Type"].ToString()=="0")//0���룬1�ල��2����3��������ַ���Χ
						{
							if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // 0�����ˣ�1�����λ
								// 3������������ѵĶ���
								RmsPM.Web.Remind.RemindModify.SaveNewRemind("3",strWBSCode,dtUserNew.Rows[i]["UserCode"].ToString(),strTaskName,DateTime.Now,DateTime.Now.AddDays(strRemindDay)); 
							if(dtUserNew.Rows[i]["RoleType"].ToString()=="1")
							{
								// ȡ��ĳ����λ�µ�������
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
						if(strRemindType.IndexOf('1')>-1&&dtUserNew.Rows[i]["Type"].ToString()=="1")//0���룬1�ල��2����3��������ַ���Χ
						{
							if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // 0�����ˣ�1�����λ
								// 3������������ѵĶ���
								RmsPM.Web.Remind.RemindModify.SaveNewRemind("3",strWBSCode,dtUserNew.Rows[i]["UserCode"].ToString(),strTaskName,DateTime.Now,DateTime.Now.AddDays(strRemindDay)); 
							if(dtUserNew.Rows[i]["RoleType"].ToString()=="1")
							{
								// ȡ��ĳ����λ�µ�������
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
						if(strRemindType.IndexOf('2')>-1&&dtUserNew.Rows[i]["Type"].ToString()=="2")//0���룬1�ල��2����3��������ַ���Χ
						{
							if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // 0�����ˣ�1�����λ
								// 3������������ѵĶ���
								RmsPM.Web.Remind.RemindModify.SaveNewRemind("3",strWBSCode,dtUserNew.Rows[i]["UserCode"].ToString(),strTaskName,DateTime.Now,DateTime.Now.AddDays(strRemindDay)); 
							if(dtUserNew.Rows[i]["RoleType"].ToString()=="1")
							{
								// ȡ��ĳ����λ�µ�������
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
//					// dtUserNew.Rows[i]["Type"].ToString() Ϊ0���룬1�ල��2����3��������ַ���Χ
//					if(dtUserNew.Rows[i]["Type"].ToString()!="3")
//					{
//						// �Ե�ǰ�û��������ڵĸ�λ����������
//						double dblDurDays = 0.0;
//						RmsPM.Web.Desktop myRemind = new Desktop();
//						// ȡ�ø�λ
//						string strRole = myRemind.GetUserStation(dtUserNew.Rows[i]["UserCode"].ToString());
//						// ���ݸ�λȡ���趨����
//						EntityData entityRemind =DAL.EntityDAO.RemindDAO.GetRemindStrategyByRoleCode(strRole);
//						if (entityRemind.HasRecord())
//						{
//							dblDurDays = double.Parse(entityRemind.GetInt("RemindDay").ToString());
//						}
//						myRemind.SaveNewRemind("3",strWBSCode,dtUserNew.Rows[i]["UserCode"].ToString(),strTaskName,DateTime.Now,DateTime.Now.AddDays(dblDurDays)); // 3������������ѵĶ���
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
						if(strTmp=="1"||strTmp=="2")// 1�����У�2����ͣ
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
				ApplicationLog.WriteLog(this.ToString(),ex,"״̬����ʧ��");
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
					return "<img src=\"../Images/icon_unbegin.gif\" title=\"δ��ʼ����\" border=0>";
				case "1":
					return "<img src=\"../Images/icon_going.gif\" title=\"�����й���\" border=0>";
				case "2":
					return "<img src=\"../Images/icon_pause.gif\" title=\"����ͣ����\" border=0>";
				case "3":
					return "<img src=\"../Images/icon_cancel.gif\" title=\"��ȡ������\" border=0>";
				case "4":
					return "<img src=\"../Images/icon_over.gif\" title=\"����ɹ���\" border=0>";
				default:
					return "<img src=\"../Images/icon_unbegin.gif\" title=\"δ��ʼ����\" border=0>";
			}
		}
	}
}
