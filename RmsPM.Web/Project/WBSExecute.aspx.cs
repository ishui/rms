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
	/// ���ġ����������ƻ�
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

		/// <summary>
		/// ��ʼ��ҳ��
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
					this.lblTitle.Text = "������������";
				}
				else
				{
                    /*
					// ���Ȩ��
					User user = (User)Session["User"];
					if(BLL.WBSRule.IsTaskExecuteAccess(this.txtTaskExecuteCode.Value,user.UserCode))
					{
						if(!user.HasOperationRight("070203"))// ����Ȩ�޻�����ԭ���Ŀ���
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
					this.lblTitle.Text = "�޸Ĺ�������";
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		
		/// <summary>
		/// ��������
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
                        // ���Ȩ��
                        User user = (User)Session["User"];
                        if (BLL.WBSRule.IsTaskExecuteAccess(this.txtTaskExecuteCode.Value, user.UserCode))
                        {
                            if (!user.HasOperationRight("070203"))// ����Ȩ�޻�����ԭ���Ŀ���
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

				//ȡ������Ϣ
				EntityData entityTask = WBSDAO.GetTaskByCode(txtWBSCode.Value);
				if (entityTask.HasRecord()) 
				{
					this.txtProjectCode.Value = entityTask.GetString("ProjectCode");
//					this.lblTaskName.Text = entityTask.GetString("TaskName");
					this.txtPercent.Text = entityTask.GetInt("CompletePercent").ToString();
				}
				entityTask.Dispose();

				// ��һ�ν���ʱ�ſ���������Ⱥ���Ա
				// ����ַ���Χ
				this.LoadUser();

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}


		/// <summary>
		/// ���湤������
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

			// �����������
			BLL.WBSRule.UpdateTaskCompletePercent(txtWBSCode.Value, BLL.ConvertRule.ToInt(this.txtPercent.Text));

			// ���½���
			this.CheckPercent(this.txtWBSCode.Value,this.txtPercent.Text);
				
			// ������ַ���Χ��¼
			string strUser = this.txtUsers.Value.Trim();
			if(strUser.Length>0)
				this.AddUser(this.txtWBSCode.Value,strUser,"3",txtTaskExecuteCode.Value);// UserTypeΪ3�ǹ�������ַ���Χ
			string strStation = this.txtStations.Value.Trim();
			if(strStation.Length>0)
				this.AddStation(this.txtWBSCode.Value,strStation,"3",txtTaskExecuteCode.Value);// UserTypeΪ3�ǹ�������ַ���Χ

			// �Լ���д�Ĺ���������Կ���


			// ������Դ������Ȩ��	
//			ArrayList arOperator = new ArrayList();
//			this.SaveRS(arOperator,strUser,strStation,"070202");
//			this.SaveRS(arOperator,base.user.UserCode,"","070202,070203");
//
//			if(arOperator.Count>0)  
//				BLL.ResourceRule.SetResourceAccessRange(this.txtTaskExecuteCode.Value,"0702","",arOperator,false);

			if (isNew)
			{
				// ���渽��
				this.myAttachMentAdd.SaveAttachMent(this.txtTaskExecuteCode.Value);
			}
			entity.Dispose();
		}

		/// <summary>
		/// ��������Ա
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
		/// ��ȡ�����Ա�б�
		/// </summary>
		/// <param name="WBSCode">���������</param>
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
							if (dtUserNew.Rows[i]["Type"].ToString() != "3") // �ַ�����
							{
								if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // ����Ϊ��
								{
									strUsers += (strUsers.Length>0)?",":"";
									strUsers += dtUserNew.Rows[i]["UserCode"].ToString();
									this.SelectName.InnerText += (this.SelectName.InnerText.Length>0)?",":"";
									this.SelectName.InnerText += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
								}
								if(dtUserNew.Rows[i]["RoleType"].ToString()=="1") // ����Ϊ��λ
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

				// ȡ����Ȩ�鿴�˱������,һ���Ǵ˱������д�ˣ��ټ��Ϲ����������,��λ
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
				
				// ֻ�и����˲ſ����޸Ľ���
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

				// ȥ���ظ��ִ�
				this.txtUsers.Value = this.CutRepeat(this.txtUsers.Value);
				this.SelectName.InnerText = this.CutRepeat(this.SelectName.InnerText);
				
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ȡ��Ա�б�ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ȡ��Ա�б�ʧ�ܣ�" + ex.Message));
			}
		}

		// ȥ���ظ��ִ�
		private string CutRepeat(string strTmp)
		{
			if(strTmp.Length<1) return strTmp;
			string strOut = "";
			string strTmp1 = "";
			foreach(string str in strTmp.Split(','))
			{
				if(str.Length<1) continue;
				if(strTmp.IndexOf(',')==0) strTmp=strTmp.Substring(1);
				if(strTmp.IndexOf(',')>0) // δ�����
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
		/// ��ӹ�����ظ�λ
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
				drStation["RoleType"] = "1"; // 1�����ɫ
				drStation["Type"] = strUserType;
				drStation["ExecuteCode"] = this.txtTaskExecuteCode.Value;
				entityStation.AddNewRecord(drStation);
				WBSDAO.InsertTaskPerson(entityStation);
			}				
			entityStation.Dispose();
		}

		/// <summary>
		/// ���½��Ⱥ�״̬
		/// </summary>
		/// <param name="strWBSCode"></param>
		/// <param name="strPercent"></param> 
		private void CheckPercent(string strWBSCode,string strPercent)
		{			
			WBSStatus myChangeStatus = new WBSStatus();
			
			if(int.Parse(strPercent)>=100)
			{
				// ��������״̬Ϊ���
				myChangeStatus.FinishProcess(strWBSCode,DateTime.Now.ToString("yyyy-MM-dd"),(string) ViewState["ProjectCode"]);
				return;
			}

			int status = BLL.WBSRule.GetTaskStatus(strWBSCode);

			if(status == 0 && int.Parse(strPercent)>0)
				myChangeStatus.StartProcess(strWBSCode,DateTime.Now.ToString("yyyy-MM-dd"));
		}


		/// <summary>
		/// ���Ȩ����Դ
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
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if(this.ftbDetail.HtmlStrippedText.Length<1)
			{
				Hint = "������ִ�����";
				return false;
			}

			// ��������Ƿ����
			WBSStatus myChangeStatus = new WBSStatus();
			if(this.txtPercent.Enabled==true&&int.Parse(this.txtPercent.Text)>100&&!myChangeStatus.IsAllFinish(this.txtWBSCode.Value,(string)ViewState["ProjectCode"])) 
			{
				Hint = "��ǰ����������û�н��������Խ��Ȳ���Ϊ100����";
				return false;
			}

			return true;
		}

		/// <summary>
		/// ���湤������
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
				ApplicationLog.WriteLog(this.ToString(),ex,"���湤������ʧ��");
				Response.Write(Rms.Web.JavaScript.Alert(true, "���湤������ʧ�ܣ�" + ex.Message));
				return;
			}

			GoBack();
		}

		/// <summary>
		/// ����
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
