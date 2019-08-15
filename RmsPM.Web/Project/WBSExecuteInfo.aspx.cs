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

namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSExecuteInfo 的摘要说明。
	/// </summary>
	public partial class WBSExecuteInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblPercent;
		protected System.Web.UI.HtmlControls.HtmlTableCell Td4;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadData();
			}

			myAttachMentList.AttachMentType = "TaskExecute";
			myAttachMentList.MasterCode = this.txtTaskExecuteCode.Value;
		}

		private void IniPage()
		{
			try 
			{
				this.btnDelete.Visible = false;
				this.btnModify.Visible = false;
				this.txtActionState.Value = Request.QueryString["ActionState"]+"";
				this.txtTaskExecuteCode.Value = Request.QueryString["TaskExecuteCode"]+"";
				this.txtRefreshScript.Value = Request.QueryString["RefreshScript"]+"";

				// 检查权限
				User user = (User)Session["User"];
				if(user.HasOperationRight("070203"))// 操作权限还是由原来的控制
					this.btnModify.Visible = true;

				if(user.HasOperationRight("070204"))// 操作权限还是由原来的控制
					this.btnDelete.Visible = true;

				FeedBack1.FeedBackType = "Execute";
				FeedBack1.MasterCode = this.txtTaskExecuteCode.Value;
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadData()
		{
			try 
			{
				

				User objUser = (User)Session["User"];
				EntityData entityExecute = WBSDAO.GetTaskExecuteByCode(txtTaskExecuteCode.Value);
				if (entityExecute.HasRecord())
				{
					this.txtWBSCode.Value = entityExecute.GetString("WBSCode");
					this.lblExecutePerson.Text = RmsPM.BLL.SystemRule.GetUserName(entityExecute.GetString("ExecutePerson"));
					this.lblExecuteDate.Text = entityExecute.GetDateTimeOnlyDate("ExecuteDate");
                    this.lblInputDate.Text = entityExecute.GetDateTimeOnlyDate("InputDate");

					this.tdDetail.InnerHtml = HttpUtility.HtmlDecode(entityExecute.GetString("HtmlDetail"));

					if(entityExecute.GetString("ExecutePerson")==objUser.UserCode)
					{
						this.btnDelete.Visible = true;
						this.btnModify.Visible = true;
					}
				}
				entityExecute.Dispose();

				//取工作信息
				EntityData entityTask = WBSDAO.GetTaskByCode(txtWBSCode.Value);
				if (entityTask.HasRecord()) 
				{
					this.lblTaskName.Text = entityTask.GetString("TaskName");
					this.lblCompletePercent.Text = entityTask.GetInt("CompletePercent").ToString();
				}
				entityTask.Dispose();
                LoadUser();

				// 待加入用户反馈的数据
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 获取相关人员列表
		/// </summary>
		/// <param name="WBSCode">工作项编码</param>
		private void LoadUser()
		{
			try
			{
				EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(this.txtWBSCode.Value);
				if (entityUser.HasRecord())
				{
					DataTable dtUserNew = entityUser.CurrentTable.Copy();					
					this.lblUser.Text = "";
					for (int i = 0; i < dtUserNew.Rows.Count; i++)
					{
						if (dtUserNew.Rows[i]["Type"].ToString() == "3"&&dtUserNew.Rows[i]["ExecuteCode"].ToString()==this.txtTaskExecuteCode.Value) // 分发对象
						{
							if(dtUserNew.Rows[i]["RoleType"].ToString()=="0") // 类型为人
							{
								this.lblUser.Text +=(this.lblUser.Text == "")?"":",";
								this.lblUser.Text += BLL.SystemRule.GetUserName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
							if(dtUserNew.Rows[i]["RoleType"].ToString()=="1") // 类型为岗位
							{
								this.lblUser.Text +=(this.lblUser.Text == "")?"":",";
								this.lblUser.Text += BLL.SystemRule.GetStationName(dtUserNew.Rows[i]["UserCode"].ToString());
							}
						}
					}
				}
				entityUser.Dispose();
//				string strUsers = "";
//				string strUserNames = "";
//				string strStations = "";
//				string strStationNames = "";
//				BLL.ResourceRule.GetAccessRange(this.txtTaskExecuteCode.Value,"0702","070202",ref strUsers,ref strUserNames,ref strStations,ref strStationNames);
//				this.lblUser.Text = strUserNames + "&nbsp;&nbsp;"+strStationNames;

				this.lblUser.Text = CutRepeat(this.lblUser.Text);
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
			return strOut.Substring(1);
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

		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				BLL.WBSRule.DeleteTaskExecute(this.txtTaskExecuteCode.Value);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"删除工作报告失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "删除工作报告失败：" + ex.Message));
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
