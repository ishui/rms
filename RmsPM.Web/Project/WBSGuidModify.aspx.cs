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
	/// WBSGuidModify 的摘要说明。
	/// 新增工作批示
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
				// 在此处放置用户代码以初始化页面
				InitPage();
				LoadData();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"新增工作指示失败");
			}
		}

		private void InitPage()
		{
			this.strAction = Request["Action"]+"";
			this.strWBSCode = Request["WBSCode"]+"";
			if(this.strAction=="Insert")
				this.lblTitle.Text = "新增工作批示";
			else
			{
				this.lblTitle.Text = "修改工作批示";
				// 指示只有新增，不需检查
				//CheckRole();
			}
		}

		


		private void LoadData()
		{
			User user = (User)Session["User"];
			this.lblUser.Text = RmsPM.BLL.SystemRule.GetUserName(user.UserCode);
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
				//保存当前工作参与人的资源

				// 待加入分发范围记录
				string strUser = this.txtUsers.Value.Trim();
				if(strUser.Length>0)
					this.AddUser(this.strWBSCode,strUser,"5",strCode);// UserType为5是工作指示分发范围
				string strStation = this.txtStations.Value.Trim();
				if(strStation.Length>0)
					this.AddStation(this.strWBSCode,strStation,"5",strCode);// UserType为5是工作指示分发范围

				// 取得当前工作参与人
//				string strUsers = "";
//				string strUserNames = "";
//				string strStations = "";
//				string strStationNames = "";
//				BLL.ResourceRule.GetAccessRange(this.strWBSCode,"0701","070107",ref strUsers,ref strUserNames,ref strStations,ref strStationNames);

				// 取得责任人
				strUser += ","+this.GetMaster(strWBSCode)+","+base.user.UserCode;

				// 设定指示人权限加入资源
				this.SaveRS(strCode,CutRepeat(strUser),CutRepeat(this.txtStations.Value),"070402");

				this.JSAction();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"保存工作指示失败");
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
			return strOut.Substring(1);
		}

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

		/// <summary>
		/// javascript的处理
		/// </summary>
		private void JSAction()
		{
			Response.Write(JavaScript.ScriptStart);			
			Response.Write("window.opener.SelectTaskExecute();");	
			Response.Write("window.close();");
			Response.Write(JavaScript.ScriptEnd);
		}

		/// <summary>
		/// 添加权限资源
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
		/// 添加相关人员
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
		/// 添加工作相关岗位
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
				drStation["RoleType"] = "1"; // 1代表角色
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
					if (dtUserNew.Rows[i]["Type"].ToString() == "2") // 分发对象，负责人，责任人只有一个
					{
						if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // 类型为人
						{
							strUsers += (strUsers.Length>0)?",":"";
							strUsers += dtUserNew.Rows[i]["UserCode"].ToString();
//							SelectName += (SelectName.Length>0)?",":"";
//							SelectName += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
						}
//						if(dtUserNew.Rows[i]["RoleType"].ToString()=="1") // 类型为岗位
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
