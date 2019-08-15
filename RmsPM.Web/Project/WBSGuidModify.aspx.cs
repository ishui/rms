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
using RmsPM.BLL;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSGuidModify ��ժҪ˵����
	/// ����������ʾ
	/// </summary>
	/// <author>unm</author>
	/// <date>2004/11/10</date>
	/// <version>1.0</version>
	public partial class WBSGuidModify : PageBase//System.Web.UI.Page
	{
		private string strAction = "";
		private string strWBSCode = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				// �ڴ˴������û������Գ�ʼ��ҳ��
				InitPage();
				LoadData();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��������ָʾʧ��");
			}
		}

		private void InitPage()
		{
			this.strAction = Request["Action"]+"";
			this.strWBSCode = Request["WBSCode"]+"";
			if(this.strAction=="Insert")
				this.lblTitle.Text = "����������ʾ";
			else
			{
				this.lblTitle.Text = "�޸Ĺ�����ʾ";
				// ָʾֻ��������������
				//CheckRole();
			}
		}

		


		private void LoadData()
		{
			User user = (User)Session["User"];
			this.lblUser.Text = RmsPM.BLL.SystemRule.GetUserName(user.UserCode);
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

		protected void SaveToolsButton_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				EntityData entity = new EntityData("TaskGuid");
				DataRow dr = entity.GetNewRecord();
				string strCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("TaskGuid");
				dr["TaskGuidCode"] = strCode;
				dr["WBSCode"] = this.strWBSCode;
				dr["TaskGuidContent"] = StringRule.FormartInput(this.arDetail.Value.Trim());
				User user = (User)Session["User"];
				dr["TaskGuidPerson"] = user.UserCode;
				dr["CreateDate"] = DateTime.Now;
				entity.AddNewRecord(dr);
				WBSDAO.InsertTaskGuid(entity);
				//���浱ǰ���������˵���Դ

				// ������ַ���Χ��¼
				string strUser = this.txtUsers.Value.Trim();
				if(strUser.Length>0)
					this.AddUser(this.strWBSCode,strUser,"5",strCode);// UserTypeΪ5�ǹ���ָʾ�ַ���Χ
				string strStation = this.txtStations.Value.Trim();
				if(strStation.Length>0)
					this.AddStation(this.strWBSCode,strStation,"5",strCode);// UserTypeΪ5�ǹ���ָʾ�ַ���Χ

				// ȡ�õ�ǰ����������
//				string strUsers = "";
//				string strUserNames = "";
//				string strStations = "";
//				string strStationNames = "";
//				BLL.ResourceRule.GetAccessRange(this.strWBSCode,"0701","070107",ref strUsers,ref strUserNames,ref strStations,ref strStationNames);

				// ȡ��������
				strUser += ","+this.GetMaster(strWBSCode)+","+base.user.UserCode;

				// �趨ָʾ��Ȩ�޼�����Դ
				this.SaveRS(strCode,CutRepeat(strUser),CutRepeat(this.txtStations.Value),"070402");

				this.JSAction();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"���湤��ָʾʧ��");
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
			return strOut.Substring(1);
		}

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

		/// <summary>
		/// javascript�Ĵ���
		/// </summary>
		private void JSAction()
		{
			Response.Write(JavaScript.ScriptStart);			
			Response.Write("window.opener.SelectTaskExecute();");	
			Response.Write("window.close();");
			Response.Write(JavaScript.ScriptEnd);
		}

		/// <summary>
		/// ���Ȩ����Դ
		/// </summary>
		private void SaveRS(string strMasterCode,string strUser,string strStation,string strOption)
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
				BLL.ResourceRule.SetResourceAccessRange(strMasterCode,strOption.Substring(0,4),"",arOperator,false);
		}
		/// <summary>
		/// ��������Ա
		/// </summary>
		/// <param name="strTWBSCode"></param>
		/// <param name="strUser"></param>
		/// <param name="strUserType"></param>
		private void AddUser(string strTWBSCode,string strUser,string strUserType,string strCode)
		{
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
			EntityData entityUser = WBSDAO.GetAllTaskPerson();			
			foreach(string sUser in arUser)
			{
				DataRow drUser = entityUser.GetNewRecord();
				drUser["WBSCode"] = strTWBSCode;
				drUser["TaskPersonCode"] = SystemManageDAO.GetNewSysCode("TaskPerson");
				drUser["UserCode"] = sUser;
				drUser["Type"] = strUserType;
				drUser["RoleType"] = '0';
				drUser["ExecuteCode"] = strCode;
				entityUser.AddNewRecord(drUser);
				WBSDAO.InsertTaskPerson(entityUser);
			}				
			entityUser.Dispose();
			this.txtUsers.Value = "";
		}
		/// <summary>
		/// ��ӹ�����ظ�λ
		/// </summary>
		/// <param name="strTWBSCode"></param>
		/// <param name="strStation"></param>
		/// <param name="strStationType"></param>
		private void AddStation(string strTWBSCode,string strStation,string strUserType,string strCode)
		{
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
			EntityData entityStation = WBSDAO.GetAllTaskPerson();			
			foreach(string sStation in arStation)
			{
				DataRow drStation = entityStation.GetNewRecord();
				drStation["WBSCode"] = strTWBSCode;
				drStation["TaskPersonCode"] = SystemManageDAO.GetNewSysCode("TaskPerson");
				drStation["UserCode"] = sStation;
				drStation["RoleType"] = "1"; // 1�����ɫ
				drStation["Type"] = strUserType;
				drStation["ExecuteCode"] = strCode;
				entityStation.AddNewRecord(drStation);
				WBSDAO.InsertTaskPerson(entityStation);
			}				
			entityStation.Dispose();
		}

		private string GetMaster(string txtWBSCode)
		{
			string strUsers = "";
//			string SelectName = "";
			EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(txtWBSCode);
			if (entityUser.HasRecord())
			{
				DataTable dtUserNew = entityUser.CurrentTable.Copy();					
				
				for (int i = 0; i < dtUserNew.Rows.Count; i++)
				{
					if (dtUserNew.Rows[i]["Type"].ToString() == "2") // �ַ����󣬸����ˣ�������ֻ��һ��
					{
						if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // ����Ϊ��
						{
							strUsers += (strUsers.Length>0)?",":"";
							strUsers += dtUserNew.Rows[i]["UserCode"].ToString();
//							SelectName += (SelectName.Length>0)?",":"";
//							SelectName += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
						}
//						if(dtUserNew.Rows[i]["RoleType"].ToString()=="1") // ����Ϊ��λ
//						{
//							strStations += (strStations.Length>0)?",":"";
//							strStations += dtUserNew.Rows[i]["UserCode"].ToString();
//							this.SelectName.InnerText += (this.SelectName.InnerText.Length>0)?",":"";
//							this.SelectName.InnerText += BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString());
//						}
					}
				}
			}
			entityUser.Dispose();
			return strUsers;
		}
	}
}
