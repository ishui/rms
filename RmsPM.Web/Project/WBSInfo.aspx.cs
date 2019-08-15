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
	/// 工作信息
	/// </summary>
	/// <modify>
	///   <description>工作页面信息修改</description>
	///   <date>2007/07/01</date>
	///   <author>unm</author>
	///   <version>1.5</version>
	/// </modify>
	public partial class WBSInfo : PageBase
	{
        private string strUsersCode;   
        private string strMasterCode;
        private string strMonitorCode;
        ArrayList alStationCode = new ArrayList();   //添加StationCode
        ArrayList alUserCode = new ArrayList();     //添加UserCode
		#region 属性声明
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
		// 为页面上取得的父节点编号准备
		protected string strFatherCode = "";

		protected string strProjectCode = "";

		private bool isMaster = false; // 是否负责人
		private bool isInputor = false; //是否录入人
		private bool isMonitor = false; // 是否监督人

		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
                // 初始化页面
				InitPage();

				//基本信息
				LoadTaskBaseInfo();	
					
				// 载入工作报告，指示等		
				LoadData();		
			
                //即时发送关注工程消息
                //此功能为时代PM个性
                AttentionSendMsg();

				// 权限处理
				SetRole();	

				// 对应保存文档，合同，关联任务等，如果隐藏控件有数据，则添加数据
				if(this.IsPostBack)				
					SaveData();

			}
			catch (Exception ex)
			{
                ApplicationLog.WriteLog(this.ToString(), ex, "工作明细初始化失败");
                Response.Write(Rms.Web.JavaScript.Alert(true, "工作明细初始化失败：" + ex.Message));
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
                string strMsg = "<b>" + this.lblProjectName.Text + "</b>" + "项目中的工作名称为" + "<b>" + this.lblTaskName.Text + "</b>" + "的工作已被" + "<b>" + strUserName + "</b>" + "所关注！";
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
                LogHelper.Error(this.ToString(), ex);  //按这个写错误日志
                Response.Write(Rms.Web.JavaScript.Alert(true, "工作明细初始化失败：" + ex.Message));
            }
               
            


           
        }

		#region 初始化页面

        /// <summary>
		/// 初始化页面
		/// </summary>
		/// <param name="WBSCode"></param>
		private void InitPage()
		{
            string urlTrue = urlBother(Request.RawUrl);
			ViewState["strWBSCode"] = Request["WBSCode"]+"";
			//ViewState["ProjectCode"] = Request["ProjectCode"].ToString();

            // 检查权限
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

			// 设定状态控制控件
			this.myStatus.TaskCode = ViewState["strWBSCode"].ToString();
			// 设定主动关注
			myUCAttention.Module = "工作信息";
			myUCAttention.Title = this.lblTaskName.Text;	
			myUCAttention.MasterCode = ViewState["strWBSCode"].ToString();
            myUCAttention.Url = urlTrue;
			myUCAttention.CurUser = base.user.UserCode;
			myUCAttention.ProjectCode = ViewState["ProjectCode"].ToString();
		}

        /// <summary>
        /// 判断进来的URL，并处理成原由URL．
        /// </summary>
        /// <param name="url">从不同入口进来的URL</param>
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
		/// 初始化工作信息页面
		/// </summary>
		/// <param name="WBSCode">工作项的WBS编码</param>
		private void LoadData()
		{	
			try
			{
				//子工作项
				LoadChildTask();

				//相关工作项
				LoadRelatedTask();

				//相关合同
				LoadContract();			

				//工作文档
				LoadDocument();

				//工作报告
				LoadExecute();
				
				// 工作指示
				LoadGuid();

				// 工作预算
				LoadTaskBudget();
				
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化工作信息页面出错：" + ex.Message));
			}
		}

		#region 加载各块数据
		/// <summary>
		/// 获取工作项基本信息
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

				// 取得前置任务信息
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

				//显示类型名称
				this.lblRelaName.Text = BLL.TaskRule.GetTaskRelaName(entityTask.GetString("RelaType"), entityTask.GetString("RelaCode"));

				//图标文件
				this.lblImageFileName.Text = entityTask.GetString("ImageFileName");
				this.hrefImageFileName.HRef = "../images/" + entityTask.GetString("ImageFileName");



				// 载入父工作项的相关信息
				LoadParentTask(strFatherCode);

				//相关人员
				LoadUser(entityTask.GetString("FullCode"));

				// 关注人
				LoadAttention();

				// 修改权限,如果拥有某节点的管理权限,就拥有之下所有子节点的权限
				this.IsParentsRight(entityTask.GetString("FullCode"));
				
				entityTask.Dispose();


			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"获取工作项基本信息出错");
				Response.Write(Rms.Web.JavaScript.Alert(true, "获取工作项基本信息出错：" + ex.Message));
			}
		}


		/// <summary>
		/// 获取父工作项
		/// </summary>
		/// <param name="ParentCode">父工作项编码</param>
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
				ApplicationLog.WriteLog(this.ToString(),ex,"获取上级工作项出错");
				Response.Write(Rms.Web.JavaScript.Alert(true, "获取上级工作项出错：" + ex.Message));
			}
		}


		/// <summary>
		/// 获取子工作项列表
		/// </summary>
		/// <param name="WBSCode">工作项编码</param>
		private void LoadChildTask()
		{
			try
			{

				WBSStrategyBuilder asb = new WBSStrategyBuilder();
//				ArrayList arA = new ArrayList();显示需要显示全部，有权的才给链接
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
				ApplicationLog.WriteLog(this.ToString(),ex,"获取子工作项列表出错");
				Response.Write(Rms.Web.JavaScript.Alert(true, "获取子工作项列表出错：" + ex.Message));
			}
		}


		/// <summary>
		/// 获取相关工作项列表
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
				ApplicationLog.WriteLog(this.ToString(),ex,"获取相关工作项列表出错");
				Response.Write(Rms.Web.JavaScript.Alert(true, "获取相关工作项列表出错：" + ex.Message));
			}
		}


		/// <summary>
		/// 获取相关合同列表
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
				ApplicationLog.WriteLog(this.ToString(),ex,"获取合同列表出错");
				Response.Write(Rms.Web.JavaScript.Alert(true, "获取合同列表出错：" + ex.Message));
			}
		}


		/// <summary>
		/// 获取相关人员列表
		/// </summary>
		/// <param name="WBSCode">工作项编码</param>
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
                    string strUsers = "";　　　　　//取监督人Ｃode
                    string strStations = "";　　　//取监督人岗位Ｃode
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

						if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // 类型为人
						{
							if (dtUserNew.Rows[i]["Type"].ToString() == "9") // 负责
							{
								this.lblMaster.Text +=(this.lblMaster.Text == "")?"":",";                             
								this.lblMaster.Text += this.GetMyTask(false,BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString()),dtUserNew.Rows[i]["UserCode"].ToString());
							}

							if (dtUserNew.Rows[i]["Type"].ToString() == "2") // 录入
							{
								this.lblInputor.Text +=(this.lblInputor.Text == "")?"":",";
								this.lblInputor.Text += this.GetMyTask(false,BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString()),dtUserNew.Rows[i]["UserCode"].ToString());
							}

							if (dtUserNew.Rows[i]["Type"].ToString() == "1") // 监督
							{
                               
								strUsers +=","+dtUserNew.Rows[i]["UserCode"].ToString();
								this.lblMonitor.Text +=(this.lblMonitor.Text == "")?"":","; 
								this.lblMonitor.Text += this.GetMyTask(false,BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString()),dtUserNew.Rows[i]["UserCode"].ToString());
                            }

							if (dtUserNew.Rows[i]["Type"].ToString() == "0") // 参与
							{
								this.lblExecuter.Text +=(this.lblExecuter.Text == "")?"":",";
								this.lblExecuter.Text += this.GetMyTask(false,BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString()),dtUserNew.Rows[i]["UserCode"].ToString());
							}
                            
						}

						if(dtUserNew.Rows[i]["RoleType"].ToString()=="1") // 类型为岗位
						{
							if (dtUserNew.Rows[i]["Type"].ToString() == "9") // 负责
							{
								this.lblMaster.Text +=(this.lblMaster.Text == "")?"":",";
								this.lblMaster.Text += this.GetMyTaskByStation(false,BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString()),dtUserNew.Rows[i]["UserCode"].ToString());
                            }

							if (dtUserNew.Rows[i]["Type"].ToString() == "2") // 录入
							{
								this.lblInputor.Text +=(this.lblInputor.Text == "")?"":",";
								this.lblInputor.Text += this.GetMyTaskByStation(false,BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString()),dtUserNew.Rows[i]["UserCode"].ToString());
							}

							if (dtUserNew.Rows[i]["Type"].ToString() == "1") // 监督
							{
								strStations +=","+dtUserNew.Rows[i]["UserCode"].ToString();
								this.lblMonitor.Text +=(this.lblMonitor.Text == "")?"":",";
								this.lblMonitor.Text += this.GetMyTaskByStation(false,BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString()),dtUserNew.Rows[i]["UserCode"].ToString());
                            }
							if (dtUserNew.Rows[i]["Type"].ToString() == "0") // 参与
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
									if(dr["RoleType"].ToString()=="0") // 类型为人
									{
										if (dr["Type"].ToString() == "9") // 负责
										{
                                            alUserCode.Add(dr["UserCode"].ToString());
                                            this.lblMaster.Text +=(this.lblMaster.Text == "")?"":",";
											this.lblMaster.Text += this.GetMyTask(true,BLL.SystemRule.GetUserName(dr["UserCode"].ToString()),dr["UserCode"].ToString());                                       
                                        }
                                        
										if (dr["Type"].ToString() == "2") // 录入
										{
											this.lblInputor.Text +=(this.lblInputor.Text == "")?"":",";
											this.lblInputor.Text += this.GetMyTask(true,BLL.SystemRule.GetUserName(dr["UserCode"].ToString()),dr["UserCode"].ToString());
										}

										if (dr["Type"].ToString() == "1") // 监督
										{
                                            alUserCode.Add(dr["UserCode"].ToString());
											strUsers +=","+dr["UserCode"].ToString();//这里获得了监督人姓名
                                            this.lblMonitor.Text +=(this.lblMonitor.Text == "")?"":",";
											this.lblMonitor.Text += this.GetMyTask(true,BLL.SystemRule.GetUserName(dr["UserCode"].ToString()),dr["UserCode"].ToString());
										　　//上面２句已经通过ＵserCode为监督人赋值了姓名
                                        }
                                       
										if (dr["Type"].ToString() == "0") // 参与
										{
											this.lblExecuter.Text +=(this.lblExecuter.Text == "")?"":",";
											this.lblExecuter.Text += this.GetMyTask(true,BLL.SystemRule.GetUserName(dr["UserCode"].ToString()),dr["UserCode"].ToString());
										}
									}

									if(dr["RoleType"].ToString()=="1") // 类型为岗位
									{
                                        if (dr["Type"].ToString() == "9") // 负责
										{
                                            alStationCode.Add(dr["UserCode"].ToString());      
											this.lblMaster.Text +=(this.lblMaster.Text == "")?"":",";
                                            this.lblMaster.Text += this.GetMyTaskByStation(true, BLL.SystemRule.GetStationName(dr["UserCode"].ToString()), dr["UserCode"].ToString());                                            
                                        }

										if (dr["Type"].ToString() == "2") // 录入
										{
											this.lblInputor.Text +=(this.lblInputor.Text == "")?"":",";
                                            this.lblInputor.Text += this.GetMyTaskByStation(true, BLL.SystemRule.GetStationName(dr["UserCode"].ToString()), dr["UserCode"].ToString());
										}

										if (dr["Type"].ToString() == "1") // 监督
										{
                                            alStationCode.Add(dr["UserCode"].ToString());  
											strStations +=","+dr["UserCode"].ToString();
											this.lblMonitor.Text +=(this.lblMonitor.Text == "")?"":",";
                                            this.lblMonitor.Text += this.GetMyTaskByStation(true, BLL.SystemRule.GetStationName(dr["UserCode"].ToString()), dr["UserCode"].ToString());
                                        }

										if (dr["Type"].ToString() == "0") // 参与
										{
											this.lblExecuter.Text +=(this.lblExecuter.Text == "")?"":",";
                                            this.lblExecuter.Text += this.GetMyTaskByStation(true, BLL.SystemRule.GetStationName(dr["UserCode"].ToString()), dr["UserCode"].ToString());
										}
                                    }
								}
								break; // 此节点有数据
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
				ApplicationLog.WriteLog(this.ToString(),ex,"获取人员列表出错");
				Response.Write(Rms.Web.JavaScript.Alert(true, "获取人员列表出错：" + ex.Message));
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
        /// 加载关注人
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
		/// 获取工作报告列表
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
				ApplicationLog.WriteLog(this.ToString(),ex,"获取工作报告列表出错");
				Response.Write(Rms.Web.JavaScript.Alert(true, "获取工作报告列表出错：" + ex.Message));
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
					//付款条件
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
		/// 获取工作指示列表
		/// </summary>
		private void LoadGuid()
		{
			try
			{				
//				if (ViewState["strUserType"].ToString() == "2"||ViewState["strUserType"].ToString() == "1") // 责任人可以看到指示
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
				ApplicationLog.WriteLog(this.ToString(),ex,"获取工作指示列表出错");
				Response.Write(Rms.Web.JavaScript.Alert(true, "获取工作指示列表出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 加载相关文档
		/// </summary>
		/// <param name="WBSCode">工作项编码</param>
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

		#region 对工作项、文档、报告、合同的预显示处理处理

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

					string strTUser = "";// 取得当前任务负责人					
					for (int j = 0; j < entityUser.CurrentTable.Rows.Count; j++)
					{
						if (entityUser.CurrentTable.Rows[j]["Type"].ToString() == "9") // 负责
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
//					if(strTUsers.IndexOf(base.user.UserCode)<0&&!isIN) // 无权
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

					string strTUser = "";// 取得当前任务负责人	
					EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(dtNew.Rows[i]["TWBSCODE"].ToString());
					for (int j = 0; j < entityUser.CurrentTable.Rows.Count; j++)
					{
						if (entityUser.CurrentTable.Rows[j]["Type"].ToString() == "9") // 负责
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
				// 取得有权限看此报告的人
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
		//		/// 取得某个工作报告的分发范围
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
		//					if (dtUserNew.Rows[i]["Type"].ToString() == "3"&&dtUserNew.Rows[i]["ExecuteCode"].ToString()==strExecuteCode) // 分发对象
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
				DataTable dtNew = dtDocument.Clone(); // 自己发出的文档才可以看到 ???
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

		#region 删除处理


		/// <summary>
		/// 删除自身
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
					this.JSAlert("删除操作成功!");
					this.JSClose();
				}
				else
					this.JSAlert("该工作项存在子项，无法删除!");
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"删除工作失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除工作失败：" + ex.Message));
			}
		}
		/// <summary>
		/// 判断该工作项节点是否有子节点
		/// </summary>
		/// <param name="WBSCode">选择的工作项节点编号</param>
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
				ApplicationLog.WriteLog(this.ToString(),ex,"删除相关工作指示失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除相关工作指示失败：" + ex.Message));
			}
		}

		private void dgExecuteList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string Code = "";
			Code = this.dgExecuteList.DataKeys[e.Item.ItemIndex].ToString();
			try
			{
				// 删除工作报告
				EntityData entityExecute = WBSDAO.GetTaskExecuteByCode(Code);
				for(int i=0;i<entityExecute.CurrentTable.Rows.Count;i++)
				{
					// 删除工作报告附件
					BLL.WBSRule.DeleteAttachByMaster("TaskExecute", entityExecute.CurrentTable.Rows[i]["TaskExecuteCode"].ToString());
				}
				WBSDAO.DeleteTaskExecute(entityExecute);

				this.LoadExecute();
				entityExecute.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"删除相关工作报告失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除相关工作报告失败：" + ex.Message));
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
				ApplicationLog.WriteLog(this.ToString(),ex,"删除相关合同失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除相关合同失败：" + ex.Message));
			}
		}

		private void dgChildTaskList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				string Code = this.dgChildTaskList.DataKeys[e.Item.ItemIndex].ToString();
				if (!this.IsHasChild(Code))
				{		
					// 删除子项所有相关数据
					EntityData entity  = WBSDAO.GetStandard_WBSByCode(Code);
					WBSDAO.DeleteTask(entity);	
					entity.Dispose();

					// 删除工作关联
					EntityData entity1 = WBSDAO.GetDoubleRelatedByCode(Code);
					if (entity1.HasRecord())
					{
						WBSDAO.DeleteTaskRelated(entity1);
					}
					entity1.Dispose();

					this.LoadChildTask();
				}
				else
                    this.JSAlert("该工作项存在子项，无法删除!");
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"删除工作任务失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除工作任务失败：" + ex.Message));
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
				ApplicationLog.WriteLog(this.ToString(),ex,"删除相关文档失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除相关文档失败：" + ex.Message));
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
				ApplicationLog.WriteLog(this.ToString(),ex,"删除相关工作项失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除相关工作项失败：" + ex.Message));
			}
		}

		#endregion	

		#region 公用函数
		/// <summary>
		/// javascript提示信息
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
			
		#endregion

		#region 权限处理
		
		/// <summary>
		/// 设定权限
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

			// 3=负责 2=录入 1=监督 0=参与

			//参与人,只可添加工作报告和相关文档
			if (ViewState["strUserType"].ToString() == "0")
			{
				this.btnAddNewExecute.Visible = true;
				this.btnAddRelatDocument.Visible = true;
			}

//            Response.Write(Rms.Web.JavaScript.Alert(true, "user:" + base.user.UserCode + ",isInputor：" + BLL.ConvertRule.ToString(isInputor)));

			//监督人,只可添加工作报告和相关文档,工作指示，可修改删除工作项
			if (ViewState["strUserType"].ToString() == "1"||this.isMonitor) // isMonitor当前人员为监督人
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

			//责任人或录入人：可添加子工作项、合同关联、文档关联,新增文档、工作报告、相关工作，修改，工作状态维护，添加关注人
			// 可删除子工作项、合同关联、文档、工作报告、相关工作，工作指示
			if (ViewState["strUserType"].ToString() == "2" || this.isInputor
				|| ViewState["strUserType"].ToString() == "9" || this.isMaster
				) //isMaster当前人员为责任人、isInputor当前人员为录入人
			{
				this.btnAddNewChild.Visible = true;
				this.btnAddNewContract.Visible = true;
				this.btnAddRelatDocument.Visible = true;
				this.btnAddNewDocument.Visible = true;
				this.btnAddNewExecute.Visible = true;
				this.btnAddNewRelatedTask.Visible = true;
				this.btnAddNewGuid.Visible = true; //只有监督人可以新增指示，只给责任人
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
				//this.dgGuidList.Columns[this.dgGuidList.Columns.Count - 1].Visible = true; 责任人无权删除指示

				// 为状态维护保存用户类型：class WBSStatus
				ViewState["UserType"] = ViewState["strUserType"].ToString();

			}	
/*
			User user = (User)Session["User"];
			User myUser = new User(user.UserCode);	
			// 有修改工作项权限，可以修改工作项，状态维护，关注人维护等
			if(myUser.HasOperationRight("070102"))	
			{
				this.ModifyButton.Visible = true;	
				this.myStatus.Visible = true;
				this.btAttention.Visible = true;
				this.btnAddNewRelatedTask.Visible = true;				
				this.btBatchModify.Visible = true;
			}

			// 有删除工作项权限
			if(myUser.HasOperationRight("070104"))	
			{
				this.btDelete.Visible = true;
				this.dgChildTaskList.Columns[this.dgChildTaskList.Columns.Count - 1].Visible = true;
			}

			// 新增工作子项权限
			if(myUser.HasOperationRight("070101"))
				this.btnAddNewChild.Visible = true;

			// 新增工作报告权限
			if(myUser.HasOperationRight("070201"))
				this.btnAddNewExecute.Visible = true;

			// 新增工作指示权限
			if(myUser.HasOperationRight("070401"))	
				this.btnAddNewGuid.Visible = true;

			// 新增关注范围
			if(myUser.HasOperationRight("070111"))
				this.btAttention.Visible = true;

*/
			// 取消和完成的不可新增子项
			if(ViewState["strStatus"].ToString()=="3"||ViewState["strStatus"].ToString()=="4")
			{
				this.btnAddNewChild.Visible = false;
				this.btnAddNewGuid.Visible = false;
			}

			//按钮权限
			if ( ! user.HasRight("070301"))  //模板导入
				this.btnImportTmpl.Visible = false;
			if ( ! user.HasRight("070302"))  //模板导出
				this.btnExportTmpl.Visible = false;

			// 如果有提醒更新提醒
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
		/// 搜索所有父节点,如果有监督人和负责人就赋予权限
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
							if(dr["RoleType"].ToString()=="0") // 类型为人
							{
								if (dr["Type"].ToString() == "9") // 负责
								{
									// 负责人可以对下属节点有权限
									if(dr["UserCode"].ToString()==base.user.UserCode)
										this.isMaster = true;
								}

								if (dr["Type"].ToString() == "2") // 录入
								{
									// 录入人可以对下属节点有权限
									if(dr["UserCode"].ToString()==base.user.UserCode)
										this.isInputor = true;
								}

								if (dr["Type"].ToString() == "1") // 监督
								{
									// 监督人可以对下属节点有权限
									if(dr["UserCode"].ToString()==base.user.UserCode)
										this.isMonitor = true;
								}
							}
							if(dr["RoleType"].ToString()=="1") // 类型为岗位
							{
								if (dr["Type"].ToString() == "9") // 负责
								{
									//查看当前人员的岗位是否在设定岗位中
                                    if (HasStation(dr["UserCode"].ToString()))
										this.isMaster = true;
								}

								if (dr["Type"].ToString() == "2") // 录入
								{
									//查看当前人员的岗位是否在设定岗位中
                                    if (HasStation(dr["UserCode"].ToString()))
                                        this.isInputor = true;
                                }

								if (dr["Type"].ToString() == "1") // 监督
								{
									//查看当前人员的岗位是否在设定岗位中
                                    if (HasStation(dr["UserCode"].ToString()))
                                        this.isMonitor = true;
								}
							}
							// break 内循环
							if(this.isMaster||this.isInputor||this.isMonitor) break;
						}
						// break外循环
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
			bool isSystemProportion = true;// 取得系统设定
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
        /// 检查当前用户的岗位是否在设定岗位中
        /// </summary>
        /// <returns></returns>
        private bool HasStation(string station)
        {
            try
            {
                bool r = false;

                //取当前用户岗位
                string user_station = BLL.SystemRule.GetStationListByUserCode(base.user.UserCode);

                //前后加上“,”再比较（系统管理员的岗位代码是0）
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

		#region 添加数据 

		private void SaveData()
		{
			// 判断相关工作项，相关合同，相关文档是否有新关联,有则新增
			// 其他如工作报告等都是在各自页面保存的，不需在此处理
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
		/// 添加相关合同 
		/// </summary>
		/// <param name="ContractCode">合同编号</param>
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
		/// 添加相关工作项
		/// </summary>
		/// <param name="TaskCode">工作项编号</param>
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
		/// 添加文档关联
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
					dr["DocumentTypeCode"] = "000006"; //000006为任务的关联文档
					entity.AddNewRecord(dr);
					DocumentDAO.InsertDocumentConfig(entity);
				}
			}		
			entity.Dispose();
			this.LoadDocument();
			this.hDocumentCode.Value = "";
		}

		/// <summary>
		/// 添加关注人
		/// </summary>
		private void AddAttention(string strUser,string strStation)
		{
            string urlTrue = urlBother(Request.RawUrl);
			// 设定我的关注的控件
			UCAttention myAttention = new UCAttention();
			myAttention.Module = "工作信息";
			myAttention.Title = this.lblTaskName.Text;	
			myAttention.MasterCode = ViewState["strWBSCode"].ToString();
            myAttention.Url = urlTrue;
			myAttention.CurUser = base.user.UserCode;
			myAttention.ProjectCode = ViewState["ProjectCode"].ToString();
			myAttention.AttentionProcess(strUser,strStation);
			this.hIsAddAttention.Value = "";	
		}

		/// <summary>
		/// 检查在表中是否已有相同记录
		/// </summary>
		/// <param name="dtOld">原数据表</param>
		/// <param name="Code">记录值</param>
		/// <param name="ColumnName">字段名</param>
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



