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

namespace RmsPM.Web.Project
{
	/// <summary>
	/// 更改、新增工作计划
	/// </summary>
	/// <author>unm</author>
	/// <date>2004/11/10</date>
	/// <version>1.0</version>
	public partial class WBSExecute : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadData();
			}

			this.myAttachMentAdd.AttachMentType = "TaskExecute";
			this.myAttachMentAdd.MasterCode = this.txtTaskExecuteCode.Value;
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

		/// <summary>
		/// 初始化页面
		/// </summary>
		private void IniPage()
		{
			try 
			{
				this.txtActionState.Value = Request.QueryString["ActionState"]+"";
				this.txtTaskExecuteCode.Value = Request.QueryString["TaskExecuteCode"]+"";
				this.txtWBSCode.Value = Request.QueryString["WBSCode"]+"";
				this.txtRefreshScript.Value = Request.QueryString["RefreshScript"]+"";
				ViewState["ProjectCode"] = Request["ProjectCode"].ToString();

				this.txtPercent.Enabled = !BLL.WBSRule.HasChilTask(this.txtWBSCode.Value.Trim());
			
				if(this.txtActionState.Value == "Insert")
				{
					this.lblTitle.Text = "新增工作报告";
				}
				else
				{
                    /*
					// 检查权限
					User user = (User)Session["User"];
					if(BLL.WBSRule.IsTaskExecuteAccess(this.txtTaskExecuteCode.Value,user.UserCode))
					{
						if(!user.HasOperationRight("070203"))// 操作权限还是由原来的控制
						{
                            if (entityExecute.GetString("ExecutePerson") != objUser.UserCode)
                            {
                                Response.Redirect("WBSExecuteInfo.aspx?TaskExecuteCode=" + this.txtTaskExecuteCode.Value);
                                Response.End();
                            }
						}
					}
					else
					{
						Response.Redirect( "../RejectAccess.aspx" );
						Response.End();
					}
                     */
					this.lblTitle.Text = "修改工作报告";
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		
		/// <summary>
		/// 载入数据
		/// </summary>
		private void LoadData()
		{
			try
			{
                if (this.txtActionState.Value == "Modify")
                {
                    User objUser = (User)Session["User"];
                    EntityData entityExecute = WBSDAO.GetTaskExecuteByCode(txtTaskExecuteCode.Value);
                    if (entityExecute.HasRecord())
                    {
                        // 检查权限
                        User user = (User)Session["User"];
                        if (BLL.WBSRule.IsTaskExecuteAccess(this.txtTaskExecuteCode.Value, user.UserCode))
                        {
                            if (!user.HasOperationRight("070203"))// 操作权限还是由原来的控制
                            {
                                if (entityExecute.GetString("ExecutePerson") != objUser.UserCode)
                                {
                                    Response.Redirect("WBSExecuteInfo.aspx?TaskExecuteCode=" + this.txtTaskExecuteCode.Value);
                                    Response.End();
                                }
                            }
                        }
                        else
                        {
                            Response.Redirect("../RejectAccess.aspx");
                            Response.End();
                        }

                        this.txtWBSCode.Value = entityExecute.GetString("WBSCode");
                        this.txtProjectCode.Value = entityExecute.GetString("ProjectCode");

                        this.ftbDetail.Text = entityExecute.GetString("HtmlDetail");

                        this.dtbExecuteDate.Value = entityExecute.GetDateTimeOnlyDate("ExecuteDate");
                    }
                    entityExecute.Dispose();

                }

				//取工作信息
				EntityData entityTask = WBSDAO.GetTaskByCode(txtWBSCode.Value);
				if (entityTask.HasRecord()) 
				{
					this.txtProjectCode.Value = entityTask.GetString("ProjectCode");
//					this.lblTaskName.Text = entityTask.GetString("TaskName");
					this.txtPercent.Text = entityTask.GetInt("CompletePercent").ToString();
				}
				entityTask.Dispose();

				// 第一次进入时才可以载入进度和人员
				// 载入分发范围
				this.LoadUser();

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}


		/// <summary>
		/// 保存工作报告
		/// </summary>
		private void SaveExecute()
		{
			EntityData entity = DAL.EntityDAO.WBSDAO.GetStandard_TaskExecuteByCode(txtTaskExecuteCode.Value);
			bool isNew = ( !entity.HasRecord() );
				
			DataRow dr = null;
			if ( isNew )
			{
				dr = entity.GetNewRecord();

				txtTaskExecuteCode.Value = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("TaskExecute");

				dr["TaskExecuteCode"] = txtTaskExecuteCode.Value;
				dr["WBSCode"] = txtWBSCode.Value;
				dr["ProjectCode"] = this.txtProjectCode.Value;

				entity.AddNewRecord(dr);
			}
			else
			{
				dr = entity.CurrentRow; 
			}

			dr["InputDate"] = DateTime.Now;
            dr["ExecuteDate"] = dtbExecuteDate.Value;
			dr["ExecutePerson"] = base.user.UserCode;

			dr["Detail"] = this.ftbDetail.HtmlStrippedText;
			dr["HtmlDetail"] = this.ftbDetail.HtmlEncodedText;


			DAL.EntityDAO.WBSDAO.SubmitAllStandard_TaskExecute(entity);
			entity.Dispose();

			// 更新任务进度
			BLL.WBSRule.UpdateTaskCompletePercent(txtWBSCode.Value, BLL.ConvertRule.ToInt(this.txtPercent.Text));

			// 更新进度
			this.CheckPercent(this.txtWBSCode.Value,this.txtPercent.Text);
				
			// 待加入分发范围记录
			string strUser = this.txtUsers.Value.Trim();
			if(strUser.Length>0)
				this.AddUser(this.txtWBSCode.Value,strUser,"3",txtTaskExecuteCode.Value);// UserType为3是工作报告分发范围
			string strStation = this.txtStations.Value.Trim();
			if(strStation.Length>0)
				this.AddStation(this.txtWBSCode.Value,strStation,"3",txtTaskExecuteCode.Value);// UserType为3是工作报告分发范围

			// 自己填写的工作报告可以看到


			// 保存资源，保存权限	
//			ArrayList arOperator = new ArrayList();
//			this.SaveRS(arOperator,strUser,strStation,"070202");
//			this.SaveRS(arOperator,base.user.UserCode,"","070202,070203");
//
//			if(arOperator.Count>0)  
//				BLL.ResourceRule.SetResourceAccessRange(this.txtTaskExecuteCode.Value,"0702","",arOperator,false);

			if (isNew)
			{
				// 保存附件
				this.myAttachMentAdd.SaveAttachMent(this.txtTaskExecuteCode.Value);
			}
			entity.Dispose();
		}

		/// <summary>
		/// 添加相关人员
		/// </summary>
		/// <param name="strTWBSCode"></param>
		/// <param name="strUser"></param>
		/// <param name="strUserType"></param>
		private void AddUser(string strTWBSCode,string strUser,string strUserType,string strCode)
		{
			strUser = CutRepeat(strUser);
			EntityData entityDelUser = WBSDAO.GetTaskPersonByWBSCode(strTWBSCode);
			DataView dv = entityDelUser.CurrentTable.DefaultView;
			dv.RowFilter = " Type = '"+strUserType+"' and RoleType='0' and ExecuteCode='"+strCode+"'";
			foreach(DataRowView drv in dv)
			{
				EntityData entityMyUser = WBSDAO.GetTaskPersonByCode(drv["TaskPersonCode"].ToString());
				WBSDAO.DeleteTaskPerson(entityMyUser);
				entityMyUser.Dispose();
			}

			string[] arUser = strUser.Split(',');
			EntityData entityUser = new EntityData("TaskPerson");		
			foreach(string sUser in arUser)
			{
				DataRow drUser = entityUser.GetNewRecord();
				User objUser = (User)Session["User"];
				drUser["WBSCode"] = strTWBSCode;
				drUser["TaskPersonCode"] = SystemManageDAO.GetNewSysCode("TaskPerson");
				drUser["UserCode"] = sUser;
				drUser["Type"] = strUserType;
				drUser["RoleType"] = '0';
				drUser["ExecuteCode"] = this.txtTaskExecuteCode.Value;
				entityUser.AddNewRecord(drUser);
				WBSDAO.InsertTaskPerson(entityUser);
			}				
			entityUser.Dispose();
			this.txtUsers.Value = "";
		}

		/// <summary>
		/// 获取相关人员列表
		/// </summary>
		/// <param name="WBSCode">工作项编码</param>
		private void LoadUser()
		{
			try
			{
				string strUsers = "";
				string strStations = "";
				if(this.txtActionState.Value == "Insert")
				{
					EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(this.txtWBSCode.Value);
					if (entityUser.HasRecord())
					{
						DataTable dtUserNew = entityUser.CurrentTable.Copy();					
					
						for (int i = 0; i < dtUserNew.Rows.Count; i++)
						{
							if (dtUserNew.Rows[i]["Type"].ToString() != "3") // 分发对象
							{
								if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // 类型为人
								{
									strUsers += (strUsers.Length>0)?",":"";
									strUsers += dtUserNew.Rows[i]["UserCode"].ToString();
									this.SelectName.InnerText += (this.SelectName.InnerText.Length>0)?",":"";
									this.SelectName.InnerText += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
								}
								if(dtUserNew.Rows[i]["RoleType"].ToString()=="1") // 类型为岗位
								{
									strStations += (strStations.Length>0)?",":"";
									strStations += dtUserNew.Rows[i]["UserCode"].ToString();
									this.SelectName.InnerText += (this.SelectName.InnerText.Length>0)?",":"";
									this.SelectName.InnerText += BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString());
								}
							}
						}
					}
					entityUser.Dispose();
				}

				// 取得有权查看此报告的人,一般是此报告的填写人，再加上工作项相关人,岗位
				string strUser = "";
				string strUserName = "";
				string strStation = "";
				string strStationName = "";
				//BLL.ResourceRule.GetAccessRange(this.txtTaskExecuteCode.Value,"0702","070202",ref strUser,ref strUserName,ref strStation,ref strStationName);				
				
				string strAllUser = "";
				string strAllStation = "";
				if(strUsers.Length>0&&strUser.Length>0)
					strAllUser = strUser+","+strUsers;				
				else
					strAllUser = strUser+strUsers;
				this.txtUsers.Value = strAllUser;
				if(strStations.Length>0&&strStation.Length>0)
					strAllStation = strStation+","+strStations;
				else
					strAllStation = strStation+strStations;
				this.txtStations.Value += strStation;
				if(strUserName.Length>0&&strStationName.Length>0)
					this.SelectName.InnerText += strUserName + "," + strStationName;
				else
					this.SelectName.InnerText += strUserName+strStationName;
				
				// 只有负责人才可以修改进度
//				this.txtPercent.Enabled = false;
//				bool isIN = false;
//				string strBulidStation = base.user.BuildStationCodes();
//				foreach(string str in strBulidStation.Split(','))
//				{
//					if(str=="") continue;
//					if(strStations.IndexOf(str)>-1)
//					{
//						isIN = true;break;
//					}
//				}
//				if(strUsers.IndexOf(base.user.UserCode)>-1||isIN) 
//					this.txtPercent.Enabled = true;

				// 去除重复字串
				this.txtUsers.Value = this.CutRepeat(this.txtUsers.Value);
				this.SelectName.InnerText = this.CutRepeat(this.SelectName.InnerText);
				
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"获取人员列表失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "获取人员列表失败：" + ex.Message));
			}
		}

		// 去除重复字串
		private string CutRepeat(string strTmp)
		{
			if(strTmp.Length<1) return strTmp;
			string strOut = "";
			string strTmp1 = "";
			foreach(string str in strTmp.Split(','))
			{
				if(str.Length<1) continue;
				if(strTmp.IndexOf(',')==0) strTmp=strTmp.Substring(1);
				if(strTmp.IndexOf(',')>0) // 未到最后
				{
					strTmp1 = strTmp.Substring(0,strTmp.IndexOf(','));
					strTmp = strTmp.Substring(strTmp.IndexOf(',')+1);
					if(strTmp.IndexOf(strTmp1)<0)
						strOut+=","+strTmp1;
				}
				else
				{
					if(str==strTmp) strOut+=","+str;
				}

			}
			if(strOut.Length<1) return "";
			return strOut.Substring(1);
		}

		/// <summary>
		/// 添加工作相关岗位
		/// </summary>
		/// <param name="strTWBSCode"></param>
		/// <param name="strStation"></param>
		/// <param name="strStationType"></param>
		private void AddStation(string strTWBSCode,string strStation,string strUserType,string strCode)
		{
			strStation = CutRepeat(strStation);
			EntityData entityDelStation = WBSDAO.GetTaskPersonByWBSCode(strTWBSCode);
			DataView dv = entityDelStation.CurrentTable.DefaultView;
			dv.RowFilter = " Type = '"+strUserType+"' and RoleType='1' and ExecuteCode='"+strCode+"'";
			foreach(DataRowView drv in dv)
			{
				EntityData entityMyStation = WBSDAO.GetTaskPersonByCode(drv["TaskPersonCode"].ToString());
				WBSDAO.DeleteTaskPerson(entityMyStation);
				entityMyStation.Dispose();
			}
			entityDelStation.Dispose();
			string[] arStation = strStation.Split(',');
			EntityData entityStation = new EntityData("TaskPerson");			
			foreach(string sStation in arStation)
			{
				DataRow drStation = entityStation.GetNewRecord();
				drStation["WBSCode"] = strTWBSCode;
				drStation["TaskPersonCode"] = SystemManageDAO.GetNewSysCode("TaskPerson");
				drStation["UserCode"] = sStation;
				drStation["RoleType"] = "1"; // 1代表角色
				drStation["Type"] = strUserType;
				drStation["ExecuteCode"] = this.txtTaskExecuteCode.Value;
				entityStation.AddNewRecord(drStation);
				WBSDAO.InsertTaskPerson(entityStation);
			}				
			entityStation.Dispose();
		}

		/// <summary>
		/// 更新进度和状态
		/// </summary>
		/// <param name="strWBSCode"></param>
		/// <param name="strPercent"></param> 
		private void CheckPercent(string strWBSCode,string strPercent)
		{			
			WBSStatus myChangeStatus = new WBSStatus();
			
			if(int.Parse(strPercent)>=100)
			{
				// 更新任务状态为完成
				myChangeStatus.FinishProcess(strWBSCode,DateTime.Now.ToString("yyyy-MM-dd"),(string) ViewState["ProjectCode"]);
				return;
			}

			int status = BLL.WBSRule.GetTaskStatus(strWBSCode);

			if(status == 0 && int.Parse(strPercent)>0)
				myChangeStatus.StartProcess(strWBSCode,DateTime.Now.ToString("yyyy-MM-dd"));
		}


		/// <summary>
		/// 添加权限资源
		/// </summary>
		private void SaveRS(ArrayList arOperator,string strUser,string strStation,string strOption)
		{		

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
					acRang.RelationCode = strTStation;
					acRang.Operations = strOption;
					arOperator.Add(acRang);
				}
			}
		}

		/// <summary>
		/// 有效性检查
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if(this.ftbDetail.HtmlStrippedText.Length<1)
			{
				Hint = "请输入执行情况";
				return false;
			}

			// 检测子项是否完成
			WBSStatus myChangeStatus = new WBSStatus();
			if(this.txtPercent.Enabled==true&&int.Parse(this.txtPercent.Text)>100&&!myChangeStatus.IsAllFinish(this.txtWBSCode.Value,(string)ViewState["ProjectCode"])) 
			{
				Hint = "当前任务还有子项没有结束，所以进度不能为100％！";
				return false;
			}

			return true;
		}

		/// <summary>
		/// 保存工作报告
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				SaveExecute();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"保存工作报告失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "保存工作报告失败：" + ex.Message));
				return;
			}

			GoBack();
		}

		/// <summary>
		/// 返回
		/// </summary>
		private void GoBack() 
		{
			Response.Write(JavaScript.ScriptStart);
			if (this.txtRefreshScript.Value.Trim() == "") 
			{
				Response.Write(JavaScript.OpenerReload(false));
			}
			else 
			{
				Response.Write(string.Format("window.opener.{0};", this.txtRefreshScript.Value));
			}
			Response.Write("window.close();");
			Response.Write(JavaScript.ScriptEnd);
		}


	}
}
