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
using Rms.Web;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;
using RmsPM.Web;
using RmsPM.DAL.QueryStrategy;
using RmsPM.Web.UserControls;


namespace RmsPM.Web.Project
{
	/// <summary>
	/// ������Ϣ
	/// </summary>
	/// <modify>
	///   <description>����ҳ����Ϣ�޸�</description>
	///   <date>2007/07/01</date>
	///   <author>unm</author>
	///   <version>1.5</version>
	/// </modify>
	public partial class WBSInfo : PageBase
	{
        private string strUsersCode;   
        private string strMasterCode;
        private string strMonitorCode;
        ArrayList alStationCode = new ArrayList();   //���StationCode
        ArrayList alUserCode = new ArrayList();     //���UserCode
		#region ��������
		protected System.Web.UI.WebControls.DropDownList drlstImportant;
		protected System.Web.UI.WebControls.DropDownList drlstStatus;
		protected RmsPM.WebControls.ToolsBar.ToolsButton tdBtnAddPerson;
		protected RmsPM.WebControls.ToolsBar.ToolsButton BtnEdit;
		protected RmsPM.WebControls.ToolsBar.ToolsButton BtnSave;
		protected System.Web.UI.HtmlControls.HtmlTableCell TDBaseSave;
		protected System.Web.UI.HtmlControls.HtmlTableCell TDBaseEdit;
		protected System.Web.UI.HtmlControls.HtmlTableCell TdContractSave;
		protected System.Web.UI.HtmlControls.HtmlTableCell TdProjectSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden ContractView;
		protected System.Web.UI.HtmlControls.HtmlTableCell TdTaskSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden RelatedView;
		protected System.Web.UI.HtmlControls.HtmlTextArea TEXTAREA1;
		protected System.Web.UI.HtmlControls.HtmlTable tbContractButton;
		protected System.Web.UI.WebControls.Label Label2;
		// Ϊҳ����ȡ�õĸ��ڵ���׼��
		protected string strFatherCode = "";

		protected string strProjectCode = "";

		private bool isMaster = false; // �Ƿ�����
		private bool isInputor = false; //�Ƿ�¼����
		private bool isMonitor = false; // �Ƿ�ල��

		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
                // ��ʼ��ҳ��
				InitPage();

				//������Ϣ
				LoadTaskBaseInfo();	
					
				// ���빤�����棬ָʾ��		
				LoadData();		
			
                //��ʱ���͹�ע������Ϣ
                //�˹���Ϊʱ��PM����
                AttentionSendMsg();

				// Ȩ�޴���
				SetRole();	

				// ��Ӧ�����ĵ�����ͬ����������ȣ�������ؿؼ������ݣ����������
				if(this.IsPostBack)				
					SaveData();

			}
			catch (Exception ex)
			{
                ApplicationLog.WriteLog(this.ToString(), ex, "������ϸ��ʼ��ʧ��");
                Response.Write(Rms.Web.JavaScript.Alert(true, "������ϸ��ʼ��ʧ�ܣ�" + ex.Message));
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
			this.dgExecuteList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgExecuteList_DeleteCommand);
			this.dgGuidList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgGuidList_DeleteCommand);
			this.dgChildTaskList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgChildTaskList_DeleteCommand);
			this.dgRelatedTask.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgRelatedTask_DeleteCommand);
			this.dgContractList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgContractList_DeleteCommand);
			this.dgDocumentList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgDocumentList_DeleteCommand);

		}
		#endregion

        private void AttentionSendMsg()
        {
            if (this.up_sPMNameLower == "shidaipm")
            {
                if (Request.QueryString["Type"] != null)
                {
                    sendMessage();
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }

        }
        public void sendMessage()
        {
            try
            {
                string strUserName = BLL.SystemRule.GetUserName(base.user.UserCode);
                string strMsg = "<b>" + this.lblProjectName.Text + "</b>" + "��Ŀ�еĹ�������Ϊ" + "<b>" + this.lblTaskName.Text + "</b>" + "�Ĺ����ѱ�" + "<b>" + strUserName + "</b>" + "����ע��";
                for (int i = 0; i < alStationCode.Count; i++)
                {
                    EntityData ed = BLL.SystemRule.GetUserByStation(alStationCode[i].ToString());
                    foreach (DataRow dr in ed.Tables[0].Rows)
                    {
                        alUserCode.Add(dr["UserCode"]);
                    }
                }
                string userCodes = "";
                if (alUserCode.Count > 0)
                {
                    for (int i = 0; i < alUserCode.Count; i++)
                    {
                        userCodes += ((string)alUserCode[i] + ",");
                    }
                    userCodes = userCodes.Substring(0, userCodes.Length - 1);
                    BLL.SendMsg send = new SendMsg();
                    send.SendMsgCode = "";
                    send.SendUsercode = base.user.UserCode;
                    send.Msg = strMsg;
                    send.Sendtime = DateTime.Now.ToShortDateString();
                    send.State = "1";
                    send.senddel = "0";
                    send.todel = "0";
                    send.SendMsgSubmit(userCodes);
                }
               
            }
            catch(Exception ex)
            {
                LogHelper.Error(this.ToString(), ex);  //�����д������־
                Response.Write(Rms.Web.JavaScript.Alert(true, "������ϸ��ʼ��ʧ�ܣ�" + ex.Message));
            }
               
            


           
        }

		#region ��ʼ��ҳ��

        /// <summary>
		/// ��ʼ��ҳ��
		/// </summary>
		/// <param name="WBSCode"></param>
		private void InitPage()
		{
            string urlTrue = urlBother(Request.RawUrl);
			ViewState["strWBSCode"] = Request["WBSCode"]+"";
			//ViewState["ProjectCode"] = Request["ProjectCode"].ToString();

            // ���Ȩ��
			if(!BLL.WBSRule.IsTaskAccess(ViewState["strWBSCode"].ToString(),user.UserCode))
			{
				Response.Redirect( "../RejectAccess.aspx" );
				Response.End();
			}

			EntityData entity = WBSDAO.GetTaskByCode(ViewState["strWBSCode"].ToString());
			if(entity.HasRecord())
			{
				ViewState["strStatus"] = entity.GetInt("Status").ToString();
				ViewState["ProjectCode"] = entity.GetString("ProjectCode").ToString();
				strProjectCode = entity.GetString("ProjectCode").ToString();
			}
			entity.Dispose();

			ViewState["strUserType"] = "";

			// �趨״̬���ƿؼ�
			this.myStatus.TaskCode = ViewState["strWBSCode"].ToString();
			// �趨������ע
			myUCAttention.Module = "������Ϣ";
			myUCAttention.Title = this.lblTaskName.Text;	
			myUCAttention.MasterCode = ViewState["strWBSCode"].ToString();
            myUCAttention.Url = urlTrue;
			myUCAttention.CurUser = base.user.UserCode;
			myUCAttention.ProjectCode = ViewState["ProjectCode"].ToString();
		}

        /// <summary>
        /// �жϽ�����URL���������ԭ��URL��
        /// </summary>
        /// <param name="url">�Ӳ�ͬ��ڽ�����URL</param>
        /// <returns></returns>
        private string  urlBother(string url)
        {
            string[] split = url.Split('&');
            foreach (string i in split)
            {
                if (i.ToString() == "Type=0")
                {
                    int strlenght = url.LastIndexOf("&");
                    string newUrl = url.Substring(0, strlenght);
                    return newUrl;
                }
            }
            return url;
        }
		/// <summary>
		/// ��ʼ��������Ϣҳ��
		/// </summary>
		/// <param name="WBSCode">�������WBS����</param>
		private void LoadData()
		{	
			try
			{
				//�ӹ�����
				LoadChildTask();

				//��ع�����
				LoadRelatedTask();

				//��غ�ͬ
				LoadContract();			

				//�����ĵ�
				LoadDocument();

				//��������
				LoadExecute();
				
				// ����ָʾ
				LoadGuid();

				// ����Ԥ��
				LoadTaskBudget();
				
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��������Ϣҳ�����" + ex.Message));
			}
		}

		#region ���ظ�������
		/// <summary>
		/// ��ȡ�����������Ϣ
		/// </summary>
		private void LoadTaskBaseInfo()
		{
			try
			{
				EntityData entityTask = WBSDAO.GetV_TaskByCode(ViewState["strWBSCode"].ToString());
				DataTable dtTask = DisposeTask(entityTask.CurrentTable,ViewState["strWBSCode"].ToString());
				strFatherCode = entityTask.CurrentRow["ParentCode"].ToString();
				//intTmpImportantLevel = (entityTask.CurrentRow["ImportantLevel"].ToString()!="1")?0:1;
				this.lblTaskName.Text = entityTask.GetString("TaskName");
                this.lblTaskName.ToolTip = BLL.WBSRule.GetWBSFullName(ViewState["strWBSCode"].ToString());
				this.lblTaskCode.Text = entityTask.GetString("SortID");
				this.txtFlag.Value = entityTask.GetIntString("Flag");
				this.lblImportantLevel.Text = dtTask.Rows[0]["ImportantName"].ToString();
				this.lblCompletePercent.Text = dtTask.Rows[0]["CompletePercent"].ToString();
                this.lblTaskStatus.Text = ComSource.GetTaskStatusName(dtTask.Rows[0]["Status"].ToString()); ;
				this.lblPlannedStartDate.Text = entityTask.GetDateTimeOnlyDate("PlannedStartDate").ToString();
				this.lblPlannedFinishDate.Text = entityTask.GetDateTimeOnlyDate("PlannedFinishDate").ToString();
				this.lblActualStartDate.Text = entityTask.GetDateTimeOnlyDate("ActualStartDate").ToString();
				this.lblActualFinishDate.Text = entityTask.GetDateTimeOnlyDate("ActualFinishDate").ToString();
                this.lblEarlyFinishDate.Text = entityTask.GetDateTimeOnlyDate("EarlyFinishDate").ToString();
                this.tdTaskDetail.InnerHtml = System.Web.HttpUtility.HtmlDecode(entityTask.GetString("Remark"));
				this.tdPauseReason.InnerHtml = System.Web.HttpUtility.HtmlDecode(entityTask.GetString("PauseReason"));
				this.tdCancelReason.InnerHtml = System.Web.HttpUtility.HtmlDecode(entityTask.GetString("CancelReason"));
				this.txtProjectCode.Value = entityTask.GetString("ProjectCode");	
				this.lblDept.Text = BLL.SystemRule.GetUnitName(entityTask.GetString("Unit"));	
				this.lblLastModifyUser.Text = BLL.SystemRule.GetUserName(entityTask.GetString("LastModifyPerson"));
				this.lblLastModifyDate.Text = entityTask.GetDateTimeOnlyDate("LastModifyDate");
				string tProportion = entityTask.GetDouble("Proportion").ToString();
				if(tProportion.IndexOf('.')>0&&tProportion.Substring(tProportion.IndexOf('.')).Length>4)
					this.lblProportion.Text = tProportion.Substring(0,tProportion.IndexOf('.')+4);
				else
					this.lblProportion.Text = tProportion;

                this.lblProjectName.Text = BLL.ProjectRule.GetProjectName(entityTask.GetString("ProjectCode"));

				// ȡ��ǰ��������Ϣ
				if(entityTask.GetString("PreWBSCode").Length>0)
				{
					this.tdPreTask.InnerHtml = "";
					string[] arPreTask =  entityTask.GetString("PreWBSCode").Split(',');					
					foreach(string tmp in arPreTask)
					{
						if(tmp.Length==0) continue;
						if(this.tdPreTask.InnerHtml.Length>0) this.tdPreTask.InnerHtml += "&nbsp;&nbsp;";
						this.tdPreTask.InnerHtml += "<a href=\"#\" onclick=\"OpenTask('"+tmp+"');return false;\">"+BLL.WBSRule.GetFieldName(tmp,"SortID")+"&nbsp;"+BLL.WBSRule.GetWBSName(tmp)+"</a>";
					}
				}

				this.trPauseReason.Visible = (entityTask.GetIntString("Status") != "2")?false:true;
				this.trCancelReason.Visible =(entityTask.GetIntString("Status") != "3")?false:true;

				//��ʾ��������
				this.lblRelaName.Text = BLL.TaskRule.GetTaskRelaName(entityTask.GetString("RelaType"), entityTask.GetString("RelaCode"));

				//ͼ���ļ�
				this.lblImageFileName.Text = entityTask.GetString("ImageFileName");
				this.hrefImageFileName.HRef = "../images/" + entityTask.GetString("ImageFileName");



				// ���븸������������Ϣ
				LoadParentTask(strFatherCode);

				//�����Ա
				LoadUser(entityTask.GetString("FullCode"));

				// ��ע��
				LoadAttention();

				// �޸�Ȩ��,���ӵ��ĳ�ڵ�Ĺ���Ȩ��,��ӵ��֮�������ӽڵ��Ȩ��
				this.IsParentsRight(entityTask.GetString("FullCode"));
				
				entityTask.Dispose();


			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ȡ�����������Ϣ����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ȡ�����������Ϣ����" + ex.Message));
			}
		}


		/// <summary>
		/// ��ȡ��������
		/// </summary>
		/// <param name="ParentCode">�����������</param>
		private void LoadParentTask(string ParentCode)
		{
			try
			{
				EntityData entityParent = WBSDAO.GetV_TaskByCode(ParentCode);
				if (entityParent.HasRecord())
				{
					if (entityParent.GetInt("Deep") != 0)
						this.tdParentName.InnerHtml = entityParent.GetString("SortID")+"&nbsp;&nbsp;<a href='#' onclick='OpenTask(" + entityParent.GetString("WBSCode") + ");return false;'>" + entityParent.GetString("TaskName") + "</a>"	;
					else
						this.tdParentName.InnerHtml = entityParent.GetString("SortID")+"&nbsp;"+entityParent.GetString("TaskName");
				}
				entityParent.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ȡ�ϼ����������");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ȡ�ϼ����������" + ex.Message));
			}
		}


		/// <summary>
		/// ��ȡ�ӹ������б�
		/// </summary>
		/// <param name="WBSCode">���������</param>
		private void LoadChildTask()
		{
			try
			{

				WBSStrategyBuilder asb = new WBSStrategyBuilder();
//				ArrayList arA = new ArrayList();��ʾ��Ҫ��ʾȫ������Ȩ�ĲŸ�����
//				arA.Add("070107");
//				arA.Add(base.user.UserCode);
//				arA.Add(base.user.BuildStationCodes());
//				asb.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.AccessRange,arA));
				asb.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.ProjectCode,ViewState["ProjectCode"].ToString()));
				asb.AddStrategy( new Strategy( DAL.QueryStrategy.WBSStrategyName.ParentCode,ViewState["strWBSCode"].ToString()));
				asb.AddOrder(" PlannedStartDate ",false);
				asb.AddOrder(" SortID ",true);
				string sql = asb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData dsChild = qa.FillEntityData("Task",sql);
				qa.Dispose();
				this.dgChildTaskList.DataSource = (dsChild.Tables[0].Rows.Count > 0 )?DisposeTask(dsChild.CurrentTable,""):null;
				this.dgChildTaskList.DataBind();
				this.divChildTask.Visible = (dsChild.Tables[0].Rows.Count > 0 )?true:false;
				this.tbNoChild.Visible = (dsChild.Tables[0].Rows.Count > 0 )?false:true;
				dsChild.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ȡ�ӹ������б����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ȡ�ӹ������б����" + ex.Message));
			}
		}


		/// <summary>
		/// ��ȡ��ع������б�
		/// </summary>
		private void LoadRelatedTask()
		{
			try
			{
				EntityData entityRelatedTask = WBSDAO.GetTaskRelatedByWBSCode(ViewState["strWBSCode"].ToString());
				this.dgRelatedTask.DataSource = (entityRelatedTask.HasRecord())?DisposeRelatedTask(entityRelatedTask.CurrentTable):null;
				this.dgRelatedTask.DataBind();
				this.divRelateTask.Visible = (entityRelatedTask.HasRecord())?true:false;
				this.tbNoRelatedTask.Visible = (entityRelatedTask.HasRecord())?false:true;
				entityRelatedTask.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ȡ��ع������б����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ȡ��ع������б����" + ex.Message));
			}
		}


		/// <summary>
		/// ��ȡ��غ�ͬ�б�
		/// </summary>
		private void LoadContract()
		{
			try
			{
				EntityData entityContract = WBSDAO.GetTaskContractByWBSCode(ViewState["strWBSCode"].ToString());
				if (entityContract.HasRecord())
				{
					DataTable dtContractNew = DisposeTaskContract(entityContract.CurrentTable).Copy();
					this.dgContractList.DataSource = dtContractNew;
				}
				else
				{
					this.dgContractList.DataSource = null;
				}
				this.dgContractList.DataBind();
				this.divContract.Visible = (entityContract.HasRecord())?true:false;
				this.tbNoContract.Visible = (entityContract.HasRecord())?false:true;
				
				entityContract.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ȡ��ͬ�б����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ȡ��ͬ�б����" + ex.Message));
			}
		}


		/// <summary>
		/// ��ȡ�����Ա�б�
		/// </summary>
		/// <param name="WBSCode">���������</param>
		private void LoadUser(string strFullCode)
		{
			try
			{
//				string WBSCode = ViewState["strWBSCode"].ToString();
//				DataTable tbGroup = BLL.WBSRule.GetTaskPersonNameGroupByType(WBSCode);
//				this.lblMaster.Text = BLL.WBSRule.GetTaskPersonNameMaster(tbGroup);				

				EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(ViewState["strWBSCode"].ToString());
				if (entityUser.HasRecord())
				{
					DataTable dtUserNew = entityUser.CurrentTable.Copy();					
					this.lblMaster.Text = "";
					this.lblInputor.Text = "";
					this.lblMonitor.Text = "";    
					this.lblExecuter.Text = "";
                    string strUsers = "";����������//ȡ�ල�ˣ�ode
                    string strStations = "";������//ȡ�ල�˸�λ��ode
					for (int i = 0; i < dtUserNew.Rows.Count; i++)
					{
						if(dtUserNew.Rows[i]["UserCode"].ToString().Trim().Length<1) continue;

						if(dtUserNew.Rows[i]["UserCode"].ToString()==user.UserCode)
						{
							string strTmp = dtUserNew.Rows[i]["Type"].ToString();

							if(ViewState["strUserType"].ToString()=="")
								ViewState["strUserType"] = strTmp;
							else
							if(BLL.ConvertRule.ToInt(strTmp) > BLL.ConvertRule.ToInt(ViewState["strUserType"]))
								ViewState["strUserType"] = strTmp;
						}

						if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // ����Ϊ��
						{
							if (dtUserNew.Rows[i]["Type"].ToString() == "9") // ����
							{
								this.lblMaster.Text +=(this.lblMaster.Text == "")?"":",";                             
								this.lblMaster.Text += this.GetMyTask(false,BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString()),dtUserNew.Rows[i]["UserCode"].ToString());
							}

							if (dtUserNew.Rows[i]["Type"].ToString() == "2") // ¼��
							{
								this.lblInputor.Text +=(this.lblInputor.Text == "")?"":",";
								this.lblInputor.Text += this.GetMyTask(false,BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString()),dtUserNew.Rows[i]["UserCode"].ToString());
							}

							if (dtUserNew.Rows[i]["Type"].ToString() == "1") // �ල
							{
                               
								strUsers +=","+dtUserNew.Rows[i]["UserCode"].ToString();
								this.lblMonitor.Text +=(this.lblMonitor.Text == "")?"":","; 
								this.lblMonitor.Text += this.GetMyTask(false,BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString()),dtUserNew.Rows[i]["UserCode"].ToString());
                            }

							if (dtUserNew.Rows[i]["Type"].ToString() == "0") // ����
							{
								this.lblExecuter.Text +=(this.lblExecuter.Text == "")?"":",";
								this.lblExecuter.Text += this.GetMyTask(false,BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString()),dtUserNew.Rows[i]["UserCode"].ToString());
							}
                            
						}

						if(dtUserNew.Rows[i]["RoleType"].ToString()=="1") // ����Ϊ��λ
						{
							if (dtUserNew.Rows[i]["Type"].ToString() == "9") // ����
							{
								this.lblMaster.Text +=(this.lblMaster.Text == "")?"":",";
								this.lblMaster.Text += this.GetMyTaskByStation(false,BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString()),dtUserNew.Rows[i]["UserCode"].ToString());
                            }

							if (dtUserNew.Rows[i]["Type"].ToString() == "2") // ¼��
							{
								this.lblInputor.Text +=(this.lblInputor.Text == "")?"":",";
								this.lblInputor.Text += this.GetMyTaskByStation(false,BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString()),dtUserNew.Rows[i]["UserCode"].ToString());
							}

							if (dtUserNew.Rows[i]["Type"].ToString() == "1") // �ල
							{
								strStations +=","+dtUserNew.Rows[i]["UserCode"].ToString();
								this.lblMonitor.Text +=(this.lblMonitor.Text == "")?"":",";
								this.lblMonitor.Text += this.GetMyTaskByStation(false,BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString()),dtUserNew.Rows[i]["UserCode"].ToString());
                            }
							if (dtUserNew.Rows[i]["Type"].ToString() == "0") // ����
							{
								this.lblExecuter.Text +=(this.lblExecuter.Text == "")?"":",";
								this.lblExecuter.Text += this.GetMyTaskByStation(false,BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString()),dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}						
					}
					dtUserNew.Dispose();

				}
				else
				{
					string[] arWBSCode = strFullCode.Split('-');
					for(int i=arWBSCode.Length-1;i>=0;i--)
					{
						entityUser = WBSDAO.GetTaskPersonByWBSCode(arWBSCode[i]);
                        if (entityUser.HasRecord())
						{
							DataTable dtUserNew = entityUser.CurrentTable.Copy();	
							DataRow[] arDR = dtUserNew.Select("Type in (0,1,2,9) ");
							if(arDR.Length>0)
							{
								this.lblMaster.Text = "";
								this.lblInputor.Text = "";
								this.lblMonitor.Text = "";
								this.lblExecuter.Text = "";
								string strUsers = "";
								string strStations = "";
							
								foreach(DataRow dr in arDR)
								{
									if(dr["RoleType"].ToString()=="0") // ����Ϊ��
									{
										if (dr["Type"].ToString() == "9") // ����
										{
                                            alUserCode.Add(dr["UserCode"].ToString());
                                            this.lblMaster.Text +=(this.lblMaster.Text == "")?"":",";
											this.lblMaster.Text += this.GetMyTask(true,BLL.SystemRule.GetUserName(dr["UserCode"].ToString()),dr["UserCode"].ToString());                                       
                                        }
                                        
										if (dr["Type"].ToString() == "2") // ¼��
										{
											this.lblInputor.Text +=(this.lblInputor.Text == "")?"":",";
											this.lblInputor.Text += this.GetMyTask(true,BLL.SystemRule.GetUserName(dr["UserCode"].ToString()),dr["UserCode"].ToString());
										}

										if (dr["Type"].ToString() == "1") // �ල
										{
                                            alUserCode.Add(dr["UserCode"].ToString());
											strUsers +=","+dr["UserCode"].ToString();//�������˼ල������
                                            this.lblMonitor.Text +=(this.lblMonitor.Text == "")?"":",";
											this.lblMonitor.Text += this.GetMyTask(true,BLL.SystemRule.GetUserName(dr["UserCode"].ToString()),dr["UserCode"].ToString());
										����//���棲���Ѿ�ͨ����serCodeΪ�ල�˸�ֵ������
                                        }
                                       
										if (dr["Type"].ToString() == "0") // ����
										{
											this.lblExecuter.Text +=(this.lblExecuter.Text == "")?"":",";
											this.lblExecuter.Text += this.GetMyTask(true,BLL.SystemRule.GetUserName(dr["UserCode"].ToString()),dr["UserCode"].ToString());
										}
									}

									if(dr["RoleType"].ToString()=="1") // ����Ϊ��λ
									{
                                        if (dr["Type"].ToString() == "9") // ����
										{
                                            alStationCode.Add(dr["UserCode"].ToString());      
											this.lblMaster.Text +=(this.lblMaster.Text == "")?"":",";
                                            this.lblMaster.Text += this.GetMyTaskByStation(true, BLL.SystemRule.GetStationName(dr["UserCode"].ToString()), dr["UserCode"].ToString());                                            
                                        }

										if (dr["Type"].ToString() == "2") // ¼��
										{
											this.lblInputor.Text +=(this.lblInputor.Text == "")?"":",";
                                            this.lblInputor.Text += this.GetMyTaskByStation(true, BLL.SystemRule.GetStationName(dr["UserCode"].ToString()), dr["UserCode"].ToString());
										}

										if (dr["Type"].ToString() == "1") // �ල
										{
                                            alStationCode.Add(dr["UserCode"].ToString());  
											strStations +=","+dr["UserCode"].ToString();
											this.lblMonitor.Text +=(this.lblMonitor.Text == "")?"":",";
                                            this.lblMonitor.Text += this.GetMyTaskByStation(true, BLL.SystemRule.GetStationName(dr["UserCode"].ToString()), dr["UserCode"].ToString());
                                        }

										if (dr["Type"].ToString() == "0") // ����
										{
											this.lblExecuter.Text +=(this.lblExecuter.Text == "")?"":",";
                                            this.lblExecuter.Text += this.GetMyTaskByStation(true, BLL.SystemRule.GetStationName(dr["UserCode"].ToString()), dr["UserCode"].ToString());
										}
                                    }
								}
								break; // �˽ڵ�������
							}

							dtUserNew.Dispose();
							break;
						}
					}

				}
				entityUser.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ȡ��Ա�б����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ȡ��Ա�б����" + ex.Message));
			}
		}

		private string GetMyTask(bool isColor,string strName,string strUser)
		{
            if (isColor)
            {
                return "<a href='#' onclick=\"OpenMyTask('" + strUser + "');return false;\"><font color=blue>" + strName + "</font></a>";
            }
            else
            {
                return "<a href='#' onclick=\"OpenMyTask('" + strUser + "');return false;\">" + strName + "</a>";
            }
        }

		private string GetMyTaskByStation(bool isColor,string strName,string strStation)
		{
            if (isColor)
            {
                return "<font color=blue>" + strName + "</font>";
            }
            else
            {
                return strName;
            }
		}

        /// <summary>
        /// ���ع�ע��
        /// </summary>
		private void LoadAttention()
		{
            if (!this.IsPostBack)
            {
                string strUsers = "";
                string strUserNames = "";
                string strStations = "";
                string strStationNames = "";
                BLL.ResourceRule.GetAccessRange(ViewState["strWBSCode"].ToString(), "0701", "070110", ref strUsers, ref strUserNames, ref strStations, ref strStationNames);
                this.hAttention.Value = strUsers;
                this.hAttentionStation.Value = strStations;
            }
		}
		/// <summary>
		/// ��ȡ���������б�
		/// </summary>
		private void LoadExecute()
		{
			try
			{
				TaskExecuteStrategyBuilder asb = new TaskExecuteStrategyBuilder();

				asb.AddStrategy( new Strategy( DAL.QueryStrategy.TaskExecuteStrategyName.WBSCode,ViewState["strWBSCode"].ToString()));

				ArrayList arA = new ArrayList();
				arA.Add("070202");
				arA.Add(user.UserCode);
				arA.Add(user.BuildStationCodes());
				asb.AddStrategy( new Strategy( DAL.QueryStrategy.TaskExecuteStrategyName.AccessRange,arA));

				string sql = asb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				EntityData entityExecute = qa.FillEntityData("TaskExecute",sql);
				qa.Dispose();

				DataTable dtExecuteNew = new DataTable();
				if (entityExecute.HasRecord())
					 dtExecuteNew = DisposeTaskExecute(entityExecute.CurrentTable).Copy();
				else
					this.dgExecuteList.DataSource = null;
				this.dgExecuteList.DataSource = dtExecuteNew;
				this.dgExecuteList.DataBind();
				this.divExecute.Visible = (dtExecuteNew.Rows.Count>0)?true:false;
				this.tbNoExecute.Visible = (dtExecuteNew.Rows.Count>0)?false:true;
				dtExecuteNew.Dispose();
				entityExecute.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ȡ���������б����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ȡ���������б����" + ex.Message));
			}
		}

		private void LoadTaskBudget()
		{
			try
			{
				string wbsCode = ViewState["strWBSCode"].ToString();
				EntityData entity = DAL.EntityDAO.WBSDAO.GetTaskBudgetByWBSCode(wbsCode);
				EntityData entityCondition = DAL.EntityDAO.WBSDAO.GetTaskBudgetConditionByWBSCode(wbsCode);
				entity.CurrentTable.Columns.Add("PayConditionHtml");
				foreach ( DataRow dr in entity.CurrentTable.Rows)
				{
					string taskBudgetCode = (string)dr["TaskBudgetCode"];
					//��������
					dr["PayConditionHtml"] = BLL.WBSRule.GetTaskBudgetPayConditionHtml(taskBudgetCode, entityCondition.CurrentTable, false);
				}

				if ( entity.HasRecord())
				{
					this.dgTaskBudget.DataSource = entity.CurrentTable;
					this.dgTaskBudget.DataBind();
					this.tableTaskBudget.Visible = false;
					this.divTaskBudget.Visible = true;
					this.dgTaskBudget.Visible = true;
				}
				else
				{
					this.dgTaskBudget.DataSource = null;
					this.dgTaskBudget.DataBind();
					this.dgTaskBudget.Visible = false;
					this.divTaskBudget.Visible = false;
					this.tableTaskBudget.Visible = true;
				}

				entity.Dispose();
				entityCondition.Dispose();
			}
			catch (Exception ex)
			{ApplicationLog.WriteLog(this.ToString(),ex,"");}
		}

		/// <summary>
		/// ��ȡ����ָʾ�б�
		/// </summary>
		private void LoadGuid()
		{
			try
			{				
//				if (ViewState["strUserType"].ToString() == "2"||ViewState["strUserType"].ToString() == "1") // �����˿��Կ���ָʾ
//				{
					TaskGuidStrategyBuilder asb = new TaskGuidStrategyBuilder();
					ArrayList arA = new ArrayList();
					arA.Add("070402");
					arA.Add(user.UserCode);
					arA.Add(user.BuildStationCodes());
					asb.AddStrategy( new Strategy( DAL.QueryStrategy.GuidStrategyName.AccessRange,arA));
					asb.AddStrategy( new Strategy( DAL.QueryStrategy.GuidStrategyName.WBSCode,ViewState["strWBSCode"].ToString()));
					string sql = asb.BuildMainQueryString();
					QueryAgent qa = new QueryAgent();
					EntityData entityGuid = qa.FillEntityData("TaskGuid",sql);
					qa.Dispose();
					DataView dv = new DataView();
					if (entityGuid.HasRecord())
					{
						DataTable dtGuidNew = DisposeTaskGuid(entityGuid.CurrentTable).Copy();
						dv = new DataView(dtGuidNew,"","CreateDate desc",System.Data.DataViewRowState.CurrentRows);
					}
					else
						this.dgGuidList.DataSource = null;	
					this.dgGuidList.DataSource = dv;
					this.dgGuidList.DataBind();
					this.divGuid.Visible = (dv.Count>0)?true:false;
					this.tbNoGuid.Visible = (dv.Count>0)?false:true;
					entityGuid.Dispose();
//				}
//				else
//				{
//					this.divGuid.Visible = false;		
//					this.tbNoGuid.Visible = true;	
//				}							
				
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ȡ����ָʾ�б����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ȡ����ָʾ�б����" + ex.Message));
			}
		}

		/// <summary>
		/// ��������ĵ�
		/// </summary>
		/// <param name="WBSCode">���������</param>
		private void LoadDocument()
		{
//			DAL.QueryStrategy.DocumentStrategyBuilder DSB = new DocumentStrategyBuilder();
//			ArrayList ar = new ArrayList();
//			ar.Add("000006");
//			ar.Add(ViewState["strWBSCode"].ToString());
//			DSB.AddStrategy(new Strategy(DAL.QueryStrategy.DocumentStrategyName.Code,ar));
//			string strDocumentSQL = DSB.BuildMainQueryString();
//			QueryAgent qaDocument = new QueryAgent();
//			EntityData entityDocument = qaDocument.FillEntityData("Document",strDocumentSQL);
//			if (entityDocument.HasRecord())
//			{
//				DataTable dtDocumentNew = DisposeTaskDocument(entityDocument.CurrentTable).Copy();
//				this.dgDocumentList.DataSource = dtDocumentNew;
//			}
//			else
//			{
//				this.dgDocumentList.DataSource = null;
//			}

			EntityData entity = DocumentDAO.GetDocumentAllInfoByMainCode("000006",ViewState["strWBSCode"].ToString());	
			this.dgDocumentList.DataSource = entity;
			this.dgDocumentList.DataBind();
			this.divDocument.Visible = (entity.CurrentTable.Rows.Count>0)?true:false;
			this.tbNoDocument.Visible = (entity.CurrentTable.Rows.Count>0)?false:true;
			entity.Dispose();
		}

		#region �Թ�����ĵ������桢��ͬ��Ԥ��ʾ������

		private DataTable DisposeTask(System.Data.DataTable dtTask,string strTmpWBSCode)
		{
			try
			{
				DataTable dtNew = dtTask.Copy();
				dtNew.Columns.Add("StatusName",System.Type.GetType("System.String"));
				dtNew.Columns.Add("ImportantName");
				dtNew.Columns.Add("Master");
				EntityData entityUser = new EntityData("TaskPerson");
				if(strTmpWBSCode!="")
					entityUser = WBSDAO.GetTaskPersonByWBSCode(ViewState["strWBSCode"].ToString());
				for ( int i = 0 ; i < dtNew.Rows.Count ; i++)
				{	
					dtNew.Rows[i]["ImportantName"] = (dtNew.Rows[i]["ImportantLevel"] == System.DBNull.Value)?"":ComSource.GetImportantName(dtNew.Rows[i]["ImportantLevel"].ToString());
					if(strTmpWBSCode=="")
						entityUser = WBSDAO.GetTaskPersonByWBSCode(dtNew.Rows[i]["WBSCode"].ToString());

					string strTUser = "";// ȡ�õ�ǰ��������					
					for (int j = 0; j < entityUser.CurrentTable.Rows.Count; j++)
					{
						if (entityUser.CurrentTable.Rows[j]["Type"].ToString() == "9") // ����
						{
							strTUser +=(strTUser == "")?"":",";
							strTUser = BLL.SystemRule.GetUserName(entityUser.CurrentTable.Rows[j]["UserCode"].ToString());
						}						
					}					
					dtNew.Rows[i]["Master"] = strTUser;

					string strTxt = this.GetStatusImg(dtNew.Rows[i]["Status"].ToString())+"&nbsp;&nbsp;"+dtNew.Rows[i]["SortID"].ToString()+"&nbsp;&nbsp;"+dtNew.Rows[i]["TaskName"].ToString();					
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
						dtNew.Rows[i]["StatusName"] = "<a href=\"#\" onclick=\"OpenTask('"+dtNew.Rows[i]["WBSCode"].ToString()+"');return false;\">"+strTxt+"</a>";

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

		private DataTable DisposeRelatedTask(System.Data.DataTable dtTask)
		{
			try
			{
				DataTable dtNew = dtTask.Copy();
				dtNew.Columns.Add("StatusName",System.Type.GetType("System.String"));
				dtNew.Columns.Add("ImportantName");
				dtNew.Columns.Add("Master");
				dtNew.Columns.Add("CompletePercent");
				dtNew.Columns.Add("PreTask");
				for ( int i = 0 ; i < dtNew.Rows.Count ; i++)
				{
					EntityData entity = WBSDAO.GetV_TaskByCode(dtNew.Rows[i]["RelatedWBSCode"].ToString());
					dtNew.Rows[i]["StatusName"] = this.GetStatusImg(entity.GetInt("Status").ToString())+"&nbsp;&nbsp;"+entity.GetString("SortID")+"&nbsp;&nbsp;"+entity.GetString("TaskName");
					dtNew.Rows[i]["ImportantName"] = BLL.ComSource.GetImportantName(entity.GetInt("ImportantLevel").ToString());
					dtNew.Rows[i]["CompletePercent"] = entity.GetInt("CompletePercent").ToString();

					string strTUser = "";// ȡ�õ�ǰ��������	
					EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(dtNew.Rows[i]["TWBSCODE"].ToString());
					for (int j = 0; j < entityUser.CurrentTable.Rows.Count; j++)
					{
						if (entityUser.CurrentTable.Rows[j]["Type"].ToString() == "9") // ����
						{
							strTUser +=(strTUser == "")?"":",";
							strTUser = BLL.SystemRule.GetUserName(entityUser.CurrentTable.Rows[j]["UserCode"].ToString());
						}						
					}				
					dtNew.Rows[i]["Master"] = strTUser;

//					string[] arPreTask = dtNew.Rows[i]["PreWBSCode"].ToString().Split(',');
//					foreach(string tmp in arPreTask)
//					{
//						dtNew.Rows[i]["PreTask"] += "<a href=\"javascript:OpenTask('"+tmp+"');\">"+BLL.WBSRule.GetFieldName(tmp,"SortID")+"&nbsp;"+BLL.WBSRule.GetWBSName(tmp)+"</a>&nbsp;";
//					}
					entityUser.Dispose();
				}				
				return dtNew;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return null;
			}
		}

		private DataTable DisposeTaskExecute(System.Data.DataTable dtExecute)
		{
			try
			{
				DataTable dtNew = dtExecute.Clone();
				// ȡ����Ȩ�޿��˱������
				DataView dvExecute = new DataView(dtExecute," WBSCode = '"+ViewState["strWBSCode"].ToString()+"'","ExecuteDate DESC ",System.Data.DataViewRowState.CurrentRows);
				for ( int i = 0 ; i < dvExecute.Count; i++ )
				{
					dtNew.ImportRow (dvExecute[i].Row);
					dtNew.Rows[i]["ExecutePerson"] = BLL.SystemRule.GetUserName(dtNew.Rows[i]["ExecutePerson"].ToString());
					string strTmp = dtNew.Rows[i]["Detail"].ToString();
					if(strTmp.Length>8)
						dtNew.Rows[i]["Detail"] = Server.HtmlEncode(dtNew.Rows[i]["Detail"].ToString().Substring(0,8)+"...");
				}
				return dtNew;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return null;
			}
		}

		//		/// <summary>
		//		/// ȡ��ĳ����������ķַ���Χ
		//		/// </summary>
		//		/// <param name="strExecuteCode"></param>
		//		/// <returns></returns>
		//		private string GetExecuteMan(string strExecuteCode)
		//		{
		//			string strUser = "";
		//			EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(ViewState["strWBSCode"].ToString());
		//			if (entityUser.HasRecord())
		//			{
		//				DataTable dtUserNew = entityUser.CurrentTable.Copy();				
		//				for (int i = 0; i < dtUserNew.Rows.Count; i++)
		//				{
		//					if (dtUserNew.Rows[i]["Type"].ToString() == "3"&&dtUserNew.Rows[i]["ExecuteCode"].ToString()==strExecuteCode) // �ַ�����
		//					{
		//						strUser += ","+dtUserNew.Rows[i]["UserCode"].ToString();
		//					}
		//				}
		//			}
		//			entityUser.Dispose();
		//			return strUser;
		//		}

		private DataTable DisposeTaskGuid(System.Data.DataTable dtguid)
		{
			try
			{
				DataTable dtNew = dtguid.Clone();
				DataView dvGuid = new DataView(dtguid," ","CreateDate DESC ",System.Data.DataViewRowState.CurrentRows);
				for ( int i = 0 ; i < dvGuid.Count; i++ )
				{
					dtNew.ImportRow (dvGuid[i].Row);
					dtNew.Rows[i]["TaskGuidPerson"] = BLL.SystemRule.GetUserName(dtNew.Rows[i]["TaskGuidPerson"].ToString());
					string strTmp = dtNew.Rows[i]["TaskGuidContent"].ToString();
					if(strTmp.Length>8)
						dtNew.Rows[i]["TaskGuidContent"] = Server.HtmlEncode(dtNew.Rows[i]["TaskGuidContent"].ToString().Substring(0,8)+"...");
				}
				return dtNew;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return null;
			}
		}

		private DataTable DisposeTaskDocument(System.Data.DataTable dtDocument)
		{
			try
			{
				DataTable dtNew = dtDocument.Clone(); // �Լ��������ĵ��ſ��Կ��� ???
				DataView dvDocument = new DataView(dtDocument," CreatePerson = '" + user.UserCode + "' ","CreateDate DESC ",System.Data.DataViewRowState.CurrentRows);
				for ( int i = 0 ; i < dvDocument.Count; i++ )
				{
					dtNew.ImportRow (dvDocument[i].Row);
				}
				return dtNew;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return null;
			}
		}

		private DataTable DisposeTaskContract(System.Data.DataTable dtContract)
		{
			DataTable dtNew = dtContract.Copy();
			
			try
			{
				dtNew.Columns.Add("TypeName");
				EntityData entityType = new EntityData("ContractType");
				for (int i = 0;i<dtNew.Rows.Count; i++)
				{
					entityType = DAL.EntityDAO.ContractDAO.GetContractTypeByCode(dtNew.Rows[i]["Type"].ToString());
					dtNew.Rows[i]["TypeName"] = (entityType.HasRecord())?entityType.GetString("TypeName"):"";
				}
				entityType.Dispose();
				return dtNew;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return null;
			}
		}



		#endregion

		#endregion
		
		#endregion

		#region ɾ������


		/// <summary>
		/// ɾ������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btDelete_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				//EntityData entityTask = WBSDAO.GetTaskByWBSCode(ViewState["strWBSCode"].ToString());
				//string ParentCode = entityTask.GetString("ParentCode");
				//entityTask.Dispose();

				if (!IsHasChild(ViewState["strWBSCode"].ToString()))
				{
					BLL.WBSRule.DeleteTask(ViewState["strWBSCode"].ToString());
					this.JSAlert("ɾ�������ɹ�!");
					this.JSClose();
				}
				else
					this.JSAlert("�ù������������޷�ɾ��!");
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"ɾ������ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������ʧ�ܣ�" + ex.Message));
			}
		}
		/// <summary>
		/// �жϸù�����ڵ��Ƿ����ӽڵ�
		/// </summary>
		/// <param name="WBSCode">ѡ��Ĺ�����ڵ���</param>
		/// <returns></returns>
		private bool IsHasChild(string strTWBSCode)
		{
			DAL.QueryStrategy.WBSStrategyBuilder WBS = new WBSStrategyBuilder();
			WBS.AddStrategy(new Strategy(DAL.QueryStrategy.WBSStrategyName.ParentCode,strTWBSCode));
			string sql = WBS.BuildMainQueryString();
			QueryAgent QA = new QueryAgent();
			EntityData entityChild = QA.FillEntityData("Task",sql);
			bool m_bFlag = entityChild.HasRecord();
			entityChild.Dispose();
			QA.Dispose();
			return m_bFlag;
		}

		private void dgGuidList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string Code = "";
			Code = this.dgGuidList.DataKeys[e.Item.ItemIndex].ToString();
			try
			{
				EntityData entity = WBSDAO.GetTaskGuidByCode(Code);
				if (entity.HasRecord())
				{
					WBSDAO.DeleteTaskGuid(entity);
				}
				entity.Dispose();
				this.LoadGuid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"ɾ����ع���ָʾʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ����ع���ָʾʧ�ܣ�" + ex.Message));
			}
		}

		private void dgExecuteList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string Code = "";
			Code = this.dgExecuteList.DataKeys[e.Item.ItemIndex].ToString();
			try
			{
				// ɾ����������
				EntityData entityExecute = WBSDAO.GetTaskExecuteByCode(Code);
				for(int i=0;i<entityExecute.CurrentTable.Rows.Count;i++)
				{
					// ɾ���������渽��
					BLL.WBSRule.DeleteAttachByMaster("TaskExecute", entityExecute.CurrentTable.Rows[i]["TaskExecuteCode"].ToString());
				}
				WBSDAO.DeleteTaskExecute(entityExecute);

				this.LoadExecute();
				entityExecute.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"ɾ����ع�������ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ����ع�������ʧ�ܣ�" + ex.Message));
			}
		}

		private void dgContractList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string Code = "";
			Code = this.dgContractList.DataKeys[e.Item.ItemIndex].ToString();
			try
			{
				EntityData entity = WBSDAO.GetTaskContractByCode(Code);
				if (entity.HasRecord())
				{
					WBSDAO.DeleteTaskContract(entity);
				}
				entity.Dispose();

				this.LoadContract();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"ɾ����غ�ͬʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ����غ�ͬʧ�ܣ�" + ex.Message));
			}
		}

		private void dgChildTaskList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				string Code = this.dgChildTaskList.DataKeys[e.Item.ItemIndex].ToString();
				if (!this.IsHasChild(Code))
				{		
					// ɾ�����������������
					EntityData entity  = WBSDAO.GetStandard_WBSByCode(Code);
					WBSDAO.DeleteTask(entity);	
					entity.Dispose();

					// ɾ����������
					EntityData entity1 = WBSDAO.GetDoubleRelatedByCode(Code);
					if (entity1.HasRecord())
					{
						WBSDAO.DeleteTaskRelated(entity1);
					}
					entity1.Dispose();

					this.LoadChildTask();
				}
				else
                    this.JSAlert("�ù������������޷�ɾ��!");
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"ɾ����������ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ����������ʧ�ܣ�" + ex.Message));
			}
		}

		private void dgDocumentList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//				// 
			//				string strDocumentConfigCode = ((DataRowView)e.Item.DataItem).Row["DocumentConfigCode"].ToString();
			//				EntityData entity = DocumentDAO.GetDocumentConfigByCode(strDocumentConfigCode);
			//				DocumentDAO.DeleteDocumentConfig(entity);				
				
			try
			{
				string DocumentCode =  this.dgDocumentList.DataKeys[e.Item.ItemIndex].ToString();
				EntityData entity = DocumentDAO.GetDocumentAllInfoByMainCode("000006",ViewState["strWBSCode"].ToString());	
				for(int i=0;i<entity.CurrentTable.Rows.Count;i++)
				{
					if(entity.CurrentTable.Rows[i]["DocumentCode"].ToString()==DocumentCode)
					{
						EntityData DelEntity = DocumentDAO.GetDocumentConfigByCode(entity.CurrentTable.Rows[i]["DocumentConfigCode"].ToString());
						DocumentDAO.DeleteDocumentConfig(DelEntity);
					}
				}
				entity.Dispose();

				LoadDocument();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"ɾ������ĵ�ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������ĵ�ʧ�ܣ�" + ex.Message));
			}
		}

		private void dgRelatedTask_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string Code = "";
			Code = this.dgRelatedTask.DataKeys[e.Item.ItemIndex].ToString();
			try
			{
				EntityData entity = WBSDAO.GetDoubleRelatedByCode(Code);
				if (entity.HasRecord())
				{
					WBSDAO.DeleteTaskRelated(entity);
				}
				entity.Dispose();

				this.LoadRelatedTask();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"ɾ����ع�����ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ����ع�����ʧ�ܣ�" + ex.Message));
			}
		}

		#endregion	

		#region ���ú���
		/// <summary>
		/// javascript��ʾ��Ϣ
		/// </summary>
		/// <param name="strInfo"></param>
		private void JSAlert(string strInfo)
		{
			Response.Write(JavaScript.ScriptStart);
			Response.Write("alert('"+strInfo+"');");
			Response.Write(JavaScript.ScriptEnd);
		}
		private void JSClose()
		{
			Response.Write(JavaScript.ScriptStart);
			Response.Write("window.close();");
			Response.Write(JavaScript.ScriptEnd);
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
			
		#endregion

		#region Ȩ�޴���
		
		/// <summary>
		/// �趨Ȩ��
		/// </summary>
		private void SetRole()
		{
			this.btnAddNewChild.Visible = false;
			this.btnAddNewContract.Visible = false;
			this.btnAddRelatDocument.Visible = false;
			this.btnAddNewRelatedTask.Visible = false;
			this.btnAddNewExecute.Visible = false;
			this.btnAddNewGuid.Visible = false;
			this.btnAddNewDocument.Visible = false;
			this.btBatchModify.Visible = false;

			this.ModifyButton.Visible = false;
			this.btDelete.Visible = false;
			this.myStatus.Visible = false;
			this.btAttention.Visible = false;

			this.btnExportTmpl.Visible = false;
			this.btnImportTmpl.Visible = false;
			this.btnTaskBudget.Visible = false;

			this.dgChildTaskList.Columns[this.dgChildTaskList.Columns.Count - 1].Visible = false;
			this.dgRelatedTask.Columns[this.dgRelatedTask.Columns.Count - 1].Visible = false;
			this.dgContractList.Columns[this.dgContractList.Columns.Count - 1].Visible = false;
			this.dgDocumentList.Columns[this.dgDocumentList.Columns.Count - 1].Visible = false;
			this.dgExecuteList.Columns[this.dgExecuteList.Columns.Count - 1].Visible = false;
			this.dgGuidList.Columns[this.dgGuidList.Columns.Count - 1].Visible = false;

			// 3=���� 2=¼�� 1=�ල 0=����

			//������,ֻ����ӹ������������ĵ�
			if (ViewState["strUserType"].ToString() == "0")
			{
				this.btnAddNewExecute.Visible = true;
				this.btnAddRelatDocument.Visible = true;
			}

//            Response.Write(Rms.Web.JavaScript.Alert(true, "user:" + base.user.UserCode + ",isInputor��" + BLL.ConvertRule.ToString(isInputor)));

			//�ල��,ֻ����ӹ������������ĵ�,����ָʾ�����޸�ɾ��������
			if (ViewState["strUserType"].ToString() == "1"||this.isMonitor) // isMonitor��ǰ��ԱΪ�ල��
			{
				this.btnAddNewExecute.Visible = true;
				this.btnAddRelatDocument.Visible = true;
				this.btnAddNewGuid.Visible = true;
				this.btBatchModify.Visible = true;
				this.ModifyButton.Visible = true;
				this.btDelete.Visible = true;
				this.dgGuidList.Columns[this.dgGuidList.Columns.Count - 1].Visible = true;
				this.dgChildTaskList.Columns[this.dgChildTaskList.Columns.Count - 1].Visible = true;
				
			}

			//�����˻�¼���ˣ�������ӹ������ͬ�������ĵ�����,�����ĵ����������桢��ع������޸ģ�����״̬ά������ӹ�ע��
			// ��ɾ���ӹ������ͬ�������ĵ����������桢��ع���������ָʾ
			if (ViewState["strUserType"].ToString() == "2" || this.isInputor
				|| ViewState["strUserType"].ToString() == "9" || this.isMaster
				) //isMaster��ǰ��ԱΪ�����ˡ�isInputor��ǰ��ԱΪ¼����
			{
				this.btnAddNewChild.Visible = true;
				this.btnAddNewContract.Visible = true;
				this.btnAddRelatDocument.Visible = true;
				this.btnAddNewDocument.Visible = true;
				this.btnAddNewExecute.Visible = true;
				this.btnAddNewRelatedTask.Visible = true;
				this.btnAddNewGuid.Visible = true; //ֻ�мල�˿�������ָʾ��ֻ��������
				this.ModifyButton.Visible = true;
				this.btDelete.Visible = true;
				this.myStatus.Visible = true;
				this.btAttention.Visible = true;				
				this.btBatchModify.Visible = true;

				this.btnExportTmpl.Visible = true;
				this.btnImportTmpl.Visible = true;
				this.btnTaskBudget.Visible = true;

				this.dgChildTaskList.Columns[this.dgChildTaskList.Columns.Count - 1].Visible = true;
				this.dgRelatedTask.Columns[this.dgRelatedTask.Columns.Count - 1].Visible = true;
				this.dgContractList.Columns[this.dgContractList.Columns.Count - 1].Visible = true;
				this.dgDocumentList.Columns[this.dgDocumentList.Columns.Count - 1].Visible = true;
				this.dgExecuteList.Columns[this.dgExecuteList.Columns.Count - 1].Visible = true;
				//this.dgGuidList.Columns[this.dgGuidList.Columns.Count - 1].Visible = true; ��������Ȩɾ��ָʾ

				// Ϊ״̬ά�������û����ͣ�class WBSStatus
				ViewState["UserType"] = ViewState["strUserType"].ToString();

			}	
/*
			User user = (User)Session["User"];
			User myUser = new User(user.UserCode);	
			// ���޸Ĺ�����Ȩ�ޣ������޸Ĺ����״̬ά������ע��ά����
			if(myUser.HasOperationRight("070102"))	
			{
				this.ModifyButton.Visible = true;	
				this.myStatus.Visible = true;
				this.btAttention.Visible = true;
				this.btnAddNewRelatedTask.Visible = true;				
				this.btBatchModify.Visible = true;
			}

			// ��ɾ��������Ȩ��
			if(myUser.HasOperationRight("070104"))	
			{
				this.btDelete.Visible = true;
				this.dgChildTaskList.Columns[this.dgChildTaskList.Columns.Count - 1].Visible = true;
			}

			// ������������Ȩ��
			if(myUser.HasOperationRight("070101"))
				this.btnAddNewChild.Visible = true;

			// ������������Ȩ��
			if(myUser.HasOperationRight("070201"))
				this.btnAddNewExecute.Visible = true;

			// ��������ָʾȨ��
			if(myUser.HasOperationRight("070401"))	
				this.btnAddNewGuid.Visible = true;

			// ������ע��Χ
			if(myUser.HasOperationRight("070111"))
				this.btAttention.Visible = true;

*/
			// ȡ������ɵĲ�����������
			if(ViewState["strStatus"].ToString()=="3"||ViewState["strStatus"].ToString()=="4")
			{
				this.btnAddNewChild.Visible = false;
				this.btnAddNewGuid.Visible = false;
			}

			//��ťȨ��
			if ( ! user.HasRight("070301"))  //ģ�嵼��
				this.btnImportTmpl.Visible = false;
			if ( ! user.HasRight("070302"))  //ģ�嵼��
				this.btnExportTmpl.Visible = false;

			// ��������Ѹ�������
			if(Request["ViewRemind"]+""=="true")
				this.ViewRemindUpDate();

			if(!IsSystemProportion())
			{
				this.tdProportionText.Visible = false;
				this.tdProportionValue.Visible = false;
				this.tdPreProportion.ColSpan = 3;
			}
		}


		/// <summary>
		/// �������и��ڵ�,����мල�˺͸����˾͸���Ȩ��
		/// </summary>
		/// <param name="fullcode"></param>
		/// <returns></returns>
		private void IsParentsRight(string fullcode)
		{
			string[] arWBSCode = fullcode.Split('-');
			for(int i=arWBSCode.Length-1;i>=0;i--)
			{
				EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(arWBSCode[i]);
                if (entityUser.HasRecord())
				{
					DataTable dtUserNew = entityUser.CurrentTable.Copy();	
					DataRow[] arDR = dtUserNew.Select("Type in (1,2,9) ");
					if(arDR.Length>0)
					{
                        foreach (DataRow dr in arDR)
                        {
							if(dr["RoleType"].ToString()=="0") // ����Ϊ��
							{
								if (dr["Type"].ToString() == "9") // ����
								{
									// �����˿��Զ������ڵ���Ȩ��
									if(dr["UserCode"].ToString()==base.user.UserCode)
										this.isMaster = true;
								}

								if (dr["Type"].ToString() == "2") // ¼��
								{
									// ¼���˿��Զ������ڵ���Ȩ��
									if(dr["UserCode"].ToString()==base.user.UserCode)
										this.isInputor = true;
								}

								if (dr["Type"].ToString() == "1") // �ල
								{
									// �ල�˿��Զ������ڵ���Ȩ��
									if(dr["UserCode"].ToString()==base.user.UserCode)
										this.isMonitor = true;
								}
							}
							if(dr["RoleType"].ToString()=="1") // ����Ϊ��λ
							{
								if (dr["Type"].ToString() == "9") // ����
								{
									//�鿴��ǰ��Ա�ĸ�λ�Ƿ����趨��λ��
                                    if (HasStation(dr["UserCode"].ToString()))
										this.isMaster = true;
								}

								if (dr["Type"].ToString() == "2") // ¼��
								{
									//�鿴��ǰ��Ա�ĸ�λ�Ƿ����趨��λ��
                                    if (HasStation(dr["UserCode"].ToString()))
                                        this.isInputor = true;
                                }

								if (dr["Type"].ToString() == "1") // �ල
								{
									//�鿴��ǰ��Ա�ĸ�λ�Ƿ����趨��λ��
                                    if (HasStation(dr["UserCode"].ToString()))
                                        this.isMonitor = true;
								}
							}
							// break ��ѭ��
							if(this.isMaster||this.isInputor||this.isMonitor) break;
						}
						// break��ѭ��
						if(this.isMaster||this.isInputor||this.isMonitor) break;
					}
					dtUserNew.Dispose();
				}
			}
		}

		private void ViewRemindUpDate()
		{
			EntityData entity = RemindDAO.GetRemindObjectByMasterUser(Request["Type"]+"",ViewState["strWBSCode"].ToString(),base.user.UserCode);
			if(entity.HasRecord())
			{
				entity.CurrentRow["IsDesk"] = "0";
				RemindDAO.UpdateRemindObject(entity);
			}
			entity.Dispose();
		}

		private bool IsSystemProportion()
		{
			bool isSystemProportion = true;// ȡ��ϵͳ�趨
			ProjectConfigStrategyBuilder sb = new ProjectConfigStrategyBuilder();
			sb.AddStrategy( new Strategy( ProjectConfigStrategyName.ProjectCode , (string)ViewState["ProjectCode"] ) );
			string sql = sb.BuildMainQueryString();
			QueryAgent qa = new QueryAgent();
			EntityData projectConfig = qa.FillEntityData( "ProjectConfig",sql );
			qa.Dispose();
				
			DataRow[] drSelects = projectConfig.CurrentTable.Select( String.Format(  " ConfigName='Proportion'" ));
			if ( drSelects.Length>0)
			{
				if ( !drSelects[0].IsNull("ConfigData"))
					if((string)drSelects[0]["ConfigData"]=="1")
						isSystemProportion = false;
			}
			projectConfig.Dispose();

			return isSystemProportion;
		}

        /// <summary>
        /// ��鵱ǰ�û��ĸ�λ�Ƿ����趨��λ��
        /// </summary>
        /// <returns></returns>
        private bool HasStation(string station)
        {
            try
            {
                bool r = false;

                //ȡ��ǰ�û���λ
                string user_station = BLL.SystemRule.GetStationListByUserCode(base.user.UserCode);

                //ǰ����ϡ�,���ٱȽϣ�ϵͳ����Ա�ĸ�λ������0��
                user_station = "," + user_station + ",";

                if (user_station.IndexOf("," + station + ",") > -1)
                    r = true;
                    
                return r;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		#endregion

		#region ������� 

		private void SaveData()
		{
			// �ж���ع������غ�ͬ������ĵ��Ƿ����¹���,��������
			// �����繤������ȶ����ڸ���ҳ�汣��ģ������ڴ˴���
			if(this.hContractCode.Value.Trim().Length>0)
				this.AddContract(this.hContractCode.Value.Trim());
			
			if(this.hDocumentCode.Value.Trim().Length>0)
				this.AddDocument(this.hDocumentCode.Value.Trim());

			if(this.hTaskCode.Value.Trim().Length>0)
				this.AddRelatedTask(this.hTaskCode.Value.Trim());

//			if(this.hIsAddAttention.Value=="1"&&(this.hAttentionStation.Value.Trim().Length>0||this.hAttention.Value.Trim().Length>0))
            if (this.hIsAddAttention.Value == "1")
                this.AddAttention(this.hAttention.Value.Trim(), this.hAttentionStation.Value.Trim());
				
		}


		/// <summary>
		/// �����غ�ͬ 
		/// </summary>
		/// <param name="ContractCode">��ͬ���</param>
		private void AddContract(string ContractCode)
		{
			string[] arContractCode = ContractCode.Split(',');
			EntityData entityOld = WBSDAO.GetTaskContractByWBSCode(ViewState["strWBSCode"].ToString());
			EntityData entityTaskContract = new EntityData("TaskContract");
			DataTable dbOld = entityOld.CurrentTable;			
			for (int i = 0;i< arContractCode.Length;i++)
			{
				if (!IsRepeat(dbOld,arContractCode[i],"ContractCode"))
				{  
					DataRow drContract = entityTaskContract.GetNewRecord();
					drContract["TaskContractCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("TaskContract");
					drContract["WBSCode"] = ViewState["strWBSCode"].ToString();
					drContract["ContractCode"] = arContractCode[i];
					entityTaskContract.AddNewRecord(drContract);
					WBSDAO.InsertTaskContract(entityTaskContract);
				}
			}
			entityOld.Dispose();
			entityTaskContract.Dispose();
			this.LoadContract();
			this.hContractCode.Value = "";
		}


		/// <summary>
		/// �����ع�����
		/// </summary>
		/// <param name="TaskCode">��������</param>
		private void AddRelatedTask(string TaskCode)
		{
			string[] arTaskCode = TaskCode.Split(',');
			EntityData entityOld = WBSDAO.GetTaskRelatedByWBSCode(ViewState["strWBSCode"].ToString());
			EntityData entityRelatedTask = new EntityData("TaskRelated");
			DataTable dbRelatedTask = entityOld.CurrentTable;
			for (int i=0;i<arTaskCode.Length;i++)
			{
				if (!IsRepeat(dbRelatedTask,arTaskCode[i],"RelatedWBSCode"))
				{
					DataRow drRelatedTask = entityRelatedTask.GetNewRecord();
					drRelatedTask["TaskRelatedCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("TaskRelated");
					drRelatedTask["WBSCode"] = ViewState["strWBSCode"].ToString();
					drRelatedTask["RelatedWBSCode"] = arTaskCode[i];
					entityRelatedTask.AddNewRecord(drRelatedTask);
					WBSDAO.InsertTaskRelated(entityRelatedTask);

					drRelatedTask = entityRelatedTask.GetNewRecord();
					drRelatedTask["TaskRelatedCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("TaskRelated");
					drRelatedTask["WBSCode"] = arTaskCode[i];
					drRelatedTask["RelatedWBSCode"] = ViewState["strWBSCode"].ToString();
					entityRelatedTask.AddNewRecord(drRelatedTask);
					WBSDAO.InsertTaskRelated(entityRelatedTask);
				}
			}			
			entityOld.Dispose();
			entityRelatedTask.Dispose();
			this.LoadRelatedTask();
			this.hTaskCode.Value = "";
		}

		/// <summary>
		/// ����ĵ�����
		/// </summary>
		/// <param name="strDocumentCode"></param>
		private void AddDocument(string strDocumentCode)
		{
			string[] arDocument = strDocumentCode.Split(',');
			EntityData entity = DocumentDAO.GetDocumentAllInfoByMainCode("000006",ViewState["strWBSCode"].ToString());	
			DataTable dt = entity.CurrentTable.Copy();
			for (int i=0;i<arDocument.Length;i++)
			{
				if (!IsRepeat(dt,arDocument[i],"DocumentCode"))
				{
					DataRow dr = entity.GetNewRecord();
					dr["DocumentConfigCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("DocumentConfig");
					dr["Code"] = ViewState["strWBSCode"].ToString();
					dr["DocumentCode"] = arDocument[i];
					dr["DocumentTypeCode"] = "000006"; //000006Ϊ����Ĺ����ĵ�
					entity.AddNewRecord(dr);
					DocumentDAO.InsertDocumentConfig(entity);
				}
			}		
			entity.Dispose();
			this.LoadDocument();
			this.hDocumentCode.Value = "";
		}

		/// <summary>
		/// ��ӹ�ע��
		/// </summary>
		private void AddAttention(string strUser,string strStation)
		{
            string urlTrue = urlBother(Request.RawUrl);
			// �趨�ҵĹ�ע�Ŀؼ�
			UCAttention myAttention = new UCAttention();
			myAttention.Module = "������Ϣ";
			myAttention.Title = this.lblTaskName.Text;	
			myAttention.MasterCode = ViewState["strWBSCode"].ToString();
            myAttention.Url = urlTrue;
			myAttention.CurUser = base.user.UserCode;
			myAttention.ProjectCode = ViewState["ProjectCode"].ToString();
			myAttention.AttentionProcess(strUser,strStation);
			this.hIsAddAttention.Value = "";	
		}

		/// <summary>
		/// ����ڱ����Ƿ�������ͬ��¼
		/// </summary>
		/// <param name="dtOld">ԭ���ݱ�</param>
		/// <param name="Code">��¼ֵ</param>
		/// <param name="ColumnName">�ֶ���</param>
		/// <returns></returns>
		private bool IsRepeat(DataTable dtOld,string Code,string ColumnName)
		{
			for (int i = 0; i<dtOld.Rows.Count;i++)
			{
				if (dtOld.Rows[i][ColumnName].ToString() == Code)
				{
					return true;
				}
			}
			return false;
		}
		#endregion

	}
}



