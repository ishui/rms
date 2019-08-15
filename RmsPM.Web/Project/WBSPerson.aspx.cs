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
using RmsPM.DAL;
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using Rms.Web;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// 添加、修改工作参与人员
	/// </summary>
	public partial class WBSPerson : System.Web.UI.Page
	{
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			InitPage();
			if (!IsPostBack)
			{
				LoadData();
			}
			if ((Request["hFlag"] +"" != "") && (Request["hCode"] + "" != ""))
			{
				ModifyUser();
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
			this.dgUserList.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgUserList_ItemCommand);
			this.SaveToolsButton.ServerClick += new System.EventHandler(this.SaveToolsButton_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void InitPage()
		{
			string UserAccess = Request.QueryString["UserAccess"] + "";

			this.trMaster.Visible = false;
			this.trMonitor.Visible = false;
			this.trExecuter.Visible = false;

			//工作项负责人可调度执行人
			if (UserAccess == "2")
			{
				this.trExecuter.Visible = true;
			}
			//上级工作项负责人可调度本工作项负责人和监督人
			else if (UserAccess == "4")
			{
				this.trMaster.Visible = true;
				this.trMonitor.Visible = true;
			}
		}
		/// <summary>
		/// 初始化人员列表
		/// </summary>
		private void LoadData()
		{
			try
			{
				string WBSCode = Request.QueryString["WBSCode"] + "";

				EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(WBSCode);
				if(entityUser.HasRecord())
				{
					DataTable dt = entityUser.CurrentTable.Clone();
					foreach(DataRow dr in entityUser.CurrentTable.Rows)
					{
						switch (dr["Type"].ToString())
						{
							case "2":
								this.lblMaster.Text = BLL.SystemRule.GetUserName(dr["UserCode"].ToString());
								this.hMasterCode.Value = dr["UserCode"].ToString();
								this.txtMasterDetail.Value = dr["MainTask"].ToString();
								break;

							case "1":
								this.lblMonitor.Text = BLL.SystemRule.GetUserName(dr["UserCode"].ToString());
								this.hMonitorCode.Value = dr["UserCode"].ToString();
								this.txtMonitorDetail.Value = dr["MainTask"].ToString();
								break;

							case "0":
								dt.Rows.Add(dr.ItemArray);
								break;
						}
					}
					this.dgUserList.DataSource = dt;
					this.dgUserList.DataBind();
					Session["UserList"] = dt;
				}

				this.btnMaster.Value = (this.lblMaster.Text == "")?"新增负责人":"更换负责人";
				this.btnMonitor.Value = (this.lblMonitor.Text == "")?"新增监督人":"更换监督人";
			}
			catch( Exception ex)
			{
				ApplicationLog.WriteLog ( this.ToString(),ex,"");
			}
		}

		private void ModifyUser()
		{
			string Flag = Request["hFlag"] + "";
			string Code = Request["hCode"] + "";

			//检查是否与原有用户 重复
			if (CheckRepeatUser(Code))
			{
				if (Flag == "2")
				{
					this.lblMaster.Text = BLL.SystemRule.GetUserName(Code);
					this.btnMaster.Value = "更换责任人";
					this.hMasterCode.Value = Code;
				}
				else if (Flag == "1")
				{
					this.lblMonitor.Text = BLL.SystemRule.GetUserName(Code);
					this.btnMonitor.Value = " 更换监督人";
					this.hMonitorCode.Value = Code;
				}
				else if (Flag == "0")
				{
					DataTable dtUser = ((DataTable)Session["UserList"]).Copy();
					DataRow dr;
					string[] strUserList = Code.Split(',');
					for (int i = 0;i <strUserList.Length; i++)
					{
						dr = dtUser.NewRow();
						dr["TaskPersonCode"] =  SystemManageDAO.GetNewSysCode("TaskPersonCode");
						dr["UserName"] = BLL.SystemRule.GetUserName(strUserList[i]);
						dr["UserCode"] = strUserList[i];
						dtUser.Rows.Add(dr);
					}
					this.dgUserList.DataSource  = dtUser;
					this.dgUserList.DataBind();
					Session["UserList"] = dtUser;
				}
			}
			else
			{
				Response.Write(JavaScript.ScriptStart);
				Response.Write("alert('与原有值重复！');");
				Response.Write(JavaScript.ScriptEnd);
			}
			this.hFlag.Value = "";
			this.hCode.Value = "";
		}


		private void SaveToolsButton_ServerClick(object sender, System.EventArgs e)
		{
			string WBSCode = Request.QueryString["WBSCode"];
			EntityData entityUser = WBSDAO.GetTaskPersonByWBSCode(WBSCode);
			WBSDAO.DeleteTaskPerson(entityUser);
			
			try
			{
				DataRow dr;

				//责任人
				dr = entityUser.GetNewRecord();
				dr["TaskPersonCode"] = SystemManageDAO.GetNewSysCode("TaskPersonCode");
				dr["WBSCode"] = WBSCode;
				dr["MainTask"] =this.txtMasterDetail.Value.Trim();
				dr["UserCode"] = this.hMasterCode.Value;
				dr["Type"] = 2;
				entityUser.AddNewRecord(dr);

				//监督人
				dr = entityUser.GetNewRecord();
				dr["TaskPersonCode"] = SystemManageDAO.GetNewSysCode("TaskPersonCode");
				dr["WBSCode"] = WBSCode;
				dr["MainTask"] = this.txtMonitorDetail.Value.Trim();
				dr["UserCode"] = this.hMonitorCode.Value;
				dr["Type"] = 1;
				entityUser.AddNewRecord(dr);

				//执行人
				System.Web.UI.WebControls.TextBox objText = new  TextBox();
				foreach(DataGridItem oDataGridItem in this.dgUserList.Items)
				{
					objText = (TextBox)oDataGridItem.FindControl("txtExecuterDetail");
					dr = entityUser.GetNewRecord();
					dr["TaskPersonCode"] = SystemManageDAO.GetNewSysCode("TaskPersonCode");
					dr["WBSCode"] = WBSCode;
					dr["UserCode"] = this.dgUserList.DataKeys[oDataGridItem.ItemIndex];
					dr["MainTask"] = objText.Text.Trim();
					dr["Type"] = 0;

					entityUser.AddNewRecord(dr);
				}
				WBSDAO.SubmitAllTaskPerson(entityUser);
				entityUser.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"保存相关人员失败");
			}

			Response.Write(JavaScript.ScriptStart);
			Response.Write("window.opener.Select('Base','');");
			Response.Write("window.close();");
			Response.Write(JavaScript.ScriptEnd);
		}


		/// <summary>
		/// 检查添加的用户是否与原有数据重复
		/// </summary>
		/// <returns></returns>
		private bool CheckRepeatUser(string UserCode)
		{	
			if (UserCode == this.hMasterCode.Value || UserCode == this.hMonitorCode.Value)
			{
				return false;
			}

			if (Session["UserList"] == null ) 
			{
				return true;
			}

			DataTable dt = ((DataTable)Session["UserList"]).Copy();
			foreach(DataRow dr in dt.Rows)
			{
				if (dr["UserCode"].ToString() == UserCode)
				{
					return false;
				}
			}
			return true;
		}

		//删除执行人
		private void dgUserList_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName == "Delete")
			{
				DataTable dt = ((DataTable)Session["UserList"]).Copy();
				
				foreach (DataRow dr in dt.Rows)
				{
					if (dr["UserCode"].ToString() == this.dgUserList.DataKeys[e.Item.ItemIndex].ToString())	
					{
						dt.Rows.Remove(dr);
						break;
					}
				}
				Session["UserList"] = dt;
				this.dgUserList.DataSource = dt;
				this.dgUserList.DataBind();
			}
		}


	}
}
