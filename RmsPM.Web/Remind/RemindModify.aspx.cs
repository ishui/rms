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
using RmsPM.DAL;
using RmsPM.BLL;
using RmsPM.Web;
using RmsPM.DAL.EntityDAO;

namespace RmsPM.Web.Remind
{
	/// <summary>
	/// 新增、修改提醒
	/// </summary>
	public partial class RemindModify : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				// 在此处放置用户代码以初始化页面
				if (!this.IsPostBack)
				{
					InitPage();
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"载入提醒信息失败！");
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

		private void InitPage()
		{
			string Action = Request.QueryString["Action"] + "";	
			string strObject = "";
			string strType = "";
			switch (Action.ToUpper())
			{
				case "INSERT":
					this.lblTitle.Text = "新增事件提醒";
					break;

				case "MODIFY":
					this.lblTitle.Text = "修改事件提醒";
					string RemindCode = Request.QueryString["Code"] + "";

					EntityData entityRemind =DAL.EntityDAO.RemindDAO.GetRemindStrategyByCode(RemindCode);
					if (entityRemind.HasRecord())
					{
						this.txtRemindDay.Text = entityRemind.GetIntString("RemindDay");
						this.rblActive.SelectedIndex = entityRemind.GetInt("IsActive");					
						strObject = entityRemind.GetString("ObjectCode");						
						strType = entityRemind.GetString("Type");
						if(strType.Substring(0,1)=="0"||strType.Substring(0,1)=="3")
						{
							if(strObject.IndexOf('0')>-1) this.chkExecuter.Checked = true;
							if(strObject.IndexOf('1')>-1) this.chkMonitor.Checked = true;
							if(strObject.IndexOf('2')>-1) this.chkMaster.Checked = true;
						}
						this.rblRemindType.SelectedIndex = (entityRemind.GetInt("RemindDay")>0)?0:1;
						this.taRemark.Value = entityRemind.GetString("Remark").Replace("\n","<br>");
					}
					entityRemind.Dispose();
					break;
			}
			// 取得事件定义
			this.lstRemindType.Items.Clear();
			for(int i=0;i<ComSource.arRemind.Length;i++)
			{
				this.lstRemindType.Items.Add(new ListItem(ComSource.arRemind[i][1],ComSource.arRemind[i][0]));
				if(ComSource.arRemind[i][0]==strType) this.lstRemindType.SelectedIndex = i;
			}

			this.lstRemindObject.Items.Clear();
			// 取得系统定义的某个项目下岗位
			EntityData entityObject = DAL.EntityDAO.OBSDAO.GetAllStation();
			for(int i=0;i<entityObject.CurrentTable.Rows.Count;i++)
			{
				DataRow dr = entityObject.CurrentTable.Rows[i];
				this.lstRemindObject.Items.Add(new ListItem(dr["StationName"].ToString(),dr["StationCode"].ToString()));
				if(dr["StationCode"].ToString()==strObject) this.lstRemindObject.SelectedIndex = i;
			}
			entityObject.Dispose();
		}

		protected void SaveToolsButton_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string Action = Request.QueryString["Action"] + "";
				string strRemindType = "";
				switch (Action.ToUpper())
				{
						//新增提醒
					case "INSERT":
						EntityData entityInsert = DAL.EntityDAO.RemindDAO.GetAllRemindStrategy();
						DataRow drInsert = entityInsert.GetNewRecord();
						drInsert["RemindStrategyCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("RemindStrategyCode");
						if(this.rblRemindType.SelectedValue=="1") 
							drInsert["RemindDay"] = int.Parse(this.txtRemindDay.Text.Trim());
						else 
							drInsert["RemindDay"] = int.Parse("-"+this.txtRemindDay.Text.Trim());
						
						if (this.rblActive.SelectedValue=="1")
						{
							drInsert["IsActive"] = 1;
						}
						else
						{
							drInsert["IsActive"] = 0;
						}
						strRemindType = this.lstRemindType.Value;
						if(strRemindType=="0"||strRemindType=="3")
						{
							string strObject = "";
							if(this.chkMaster.Checked) strObject+="2";
							if(this.chkMonitor.Checked) strObject+="1";
							if(this.chkExecuter.Checked) strObject+="0";
							drInsert["ObjectCode"] = strObject;
						}
						else
							drInsert["ObjectCode"] = this.lstRemindObject.Value;
					
						drInsert["Type"] = strRemindType;
						drInsert["ProjectCode"] = (string)Session["ProjectCode"];
						drInsert["Remark"] = this.taRemark.Value.Trim();
						entityInsert.AddNewRecord(drInsert);
						RemindDAO.InsertRemindStrategy(entityInsert);
						entityInsert.Dispose();
						break;

						//修改
					case "MODIFY":
						string RemindCode = Request.QueryString["Code"] + "";
						EntityData entityModify =DAL.EntityDAO.RemindDAO.GetRemindStrategyByCode(RemindCode);
						DataRow drModify = entityModify.CurrentRow;
						if(this.rblRemindType.SelectedValue=="1") 
							drModify["RemindDay"] = int.Parse(this.txtRemindDay.Text.Trim());
						else 
							drModify["RemindDay"] = int.Parse("-"+this.txtRemindDay.Text.Trim());
				
						if (this.rblActive.SelectedValue=="1")
						{
							drModify["IsActive"] = 1;
						}
						else
						{
							drModify["IsActive"] = 0;
						}
						strRemindType = this.lstRemindType.Value;
						if(strRemindType=="0"||strRemindType=="3")
						{
							string strObject = "";
							if(this.chkMaster.Checked) strObject+="2";
							if(this.chkMonitor.Checked) strObject+="1";
							if(this.chkExecuter.Checked) strObject+="0";
							drModify["ObjectCode"] = strObject;
						}
						else
							drModify["ObjectCode"] = this.lstRemindObject.Value;
					
						drModify["Type"] = strRemindType;
						drModify["Remark"] = StringRule.FormartInput(this.taRemark.Value.Trim());
						RemindDAO.UpdateRemindStrategy(entityModify);
						entityModify.Dispose();
						break;
				}

				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.opener.location.reload();");
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"保存提醒信息失败！");
			}

		}

		/// <summary>
		/// 增加新的提醒到提醒表
		/// </summary>
		public static void SaveNewRemind(string strType,string strMasterCode,string strUser,string strMessage,DateTime  StartDate,DateTime EndDate)
		{
			// 检查当前MasterCode当前用户是否有重复，如果有则更新
			EntityData entity = RemindDAO.GetRemindObjectByMasterUser(strType,strMasterCode,strUser);
			if(entity.HasRecord())
			{
				DataTable dt = entity.CurrentTable;
				for(int i=0;i<dt.Rows.Count;i++)
				{
					dt.Rows[i]["CreateDate"] = StartDate;
					dt.Rows[i]["EndDate"] = EndDate;
				}
				RemindDAO.UpdateRemindObject(entity);
			}
			else
			{
				EntityData entityRemind = new EntityData("RemindObject");
				DataRow drRemind = entityRemind.GetNewRecord();
				drRemind["RemindObjectCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("RemindObject");
				drRemind["Type"] = strType;
				drRemind["User"] = strUser;
				drRemind["MasterCode"] = strMasterCode;
				drRemind["Message"] = strMessage;
				drRemind["CreateDate"] = StartDate;
				drRemind["EndDate"] = EndDate;
				drRemind["IsDesk"] = "1"; // 默认桌面显示
				entityRemind.AddNewRecord(drRemind);
				RemindDAO.InsertRemindObject(entityRemind);
				entityRemind.Dispose();
			}
			entity.Dispose();
		}
	}
}
