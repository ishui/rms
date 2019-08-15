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
using System.Text;
using System.Collections.Generic;

using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using RmsPM.Web;
using Rms.ORMap;
using Rms.WorkFlow;

namespace RmsPM.Web.WorkFlowControl
{
	/// <summary>
	/// WorkFlowRouterCopy 的摘要说明。
	/// </summary>
	public partial class WorkFlowRouterCopy : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack)
            {
              
               
                ///////////////////////////////////////////////////
                string code = Request["ApplicationCode"] + "";
                string caseCode = Request["CaseCode"] + "";
                string procedureCode = Request["ProcedureCode"] + "";
                string procedureName = Request["ProcedureName"] + "";
                string userCode = Request["UserCode"] + "";
                string unitCode = Request["UnitCodeCode"] + "";
                this.ProjectCode = Request["ProjectCode"] + "";
                //if (procedureCode == "")
                //    procedureCode = BLL.WorkFlowRule.GetProcedureCodeByName(procedureName);
                bool isNew = (caseCode == "");
                Procedure procedure = DefinitionManager.GetProcedureDifinition(procedureCode, true);
                string currentActCode = Request["ActCode"] + "";
                Act currentAct = null;
                string actCode = "";
                WorkCase workCase = null;
                if (isNew)
                {
                    workCase = Rms.WorkFlow.WorkCaseManager.StartNewWorkCase(code, procedureCode, userCode, unitCode, ref actCode, "", "");
                    currentAct = (Act)(WorkCaseManager.GetActivityAct(workCase)[0]);
                }
                else
                {
                    workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(caseCode);
                    currentAct = workCase.GetAct(Request["ActCode"] + "");
                }
                this.txtCurrentActCode.Value = currentAct.ActCode;
                Task currentTask = procedure.GetTask(currentAct.ToTaskCode);
                ///////////////////////////////////////////////////

                if (currentTask.CanManual == 1)
                {
                    this.SendTable.Visible = false;
                    this.SendTitle.Visible = false;
                    this.CopyTable.Visible = false;
                    this.CopyTitle.Visible = false;
                    this.OpinionTitle.Visible = false;
                    this.OpinionTable.Visible = false;
                    this.MessageTitle.Visible = false;
                    this.MessageTable.Visible = false;
                    this.ButtonTable.Visible = false;
                    this.pwdcheckdiv.Visible = true;
                }
                else
                {
                    InitPageState();
                }
                BLL.TemplateOpinion to = new BLL.TemplateOpinion();
                to.UserCode = ((User)Session["User"]).UserCode;
                DataTable dt = to.GetTemplateOpinions();
                if (dt.Rows.Count == 0)
                {
                    this.sltTemplateOpinion.Visible = false;
                }
                else
                {
                    this.sltTemplateOpinion.Visible = true;
                    this.sltTemplateOpinion.DataSource = dt;
                    this.sltTemplateOpinion.DataTextField = "Name";
                    this.sltTemplateOpinion.DataValueField = "Center";
                    this.sltTemplateOpinion.DataBind();
                    ListItem li = new ListItem();
                    li.Text = "--常用意见--";
                    li.Value = "";
                    li.Selected = true;
                    this.sltTemplateOpinion.Items.Add(li);
                    this.sltTemplateOpinion.Attributes["onchange"] = "javascript:document.all('FlowOpinion').value = this.value;";
                }
                this.rdoCheck.Visible = (Request["IsAudit"] + ""=="1");
            }
        }

        private void InitPageState()
        {
            this.SendTable.Visible = true;
            this.SendTitle.Visible = true;
            this.CopyTable.Visible = true;
            this.CopyTitle.Visible = true;
            this.OpinionTitle.Visible = true;
            this.OpinionTable.Visible = true;
            this.MessageTitle.Visible = true;
            this.MessageTable.Visible = true;
            this.ButtonTable.Visible = true;
            this.pwdcheckdiv.Visible = false;

            AttachMentAdd1.AttachMentType = "WorkFlowActOpinion";
            AttachMentAdd1.MasterCode = Request["ActCode"] + "";
            if (System.Configuration.ConfigurationSettings.AppSettings["MailServer"] == null)
            {
                this.ChkMail.Style.Add("display", "none");
                this.ChkMailCopy.Style.Add("display", "none");
                this.EmailSpan.Visible = false;
                this.EmailSpanCopy.Visible = false;
            }
            if (System.Configuration.ConfigurationSettings.AppSettings["FlowEmail"] == "1")
            {
                this.ChkMail.Checked = true;
                this.ChkMailCopy.Checked = true;
            }
            if (Request["Work"].ToString() == "Send")
            {
                this.SendTable.Visible = true;
                this.SendTitle.Visible = true;
                this.CopyTable.Visible = false;
                this.CopyTitle.Visible = false;
                IniPage();
                LoadData();
                this.WaitForFlag.Disabled = true;
            }
            else
            {
                this.SendTable.Visible = false;
                this.SendTitle.Visible = false;
                this.CopyTable.Visible = true;
                this.CopyTitle.Visible = true;
                this.WaitForFlag.Disabled = false;
                IniPage();
                LoadData();
            }

        }

		private void IniPage()
		{
			Session["WorkCaseApplicationTemp"] = null;
			if ( Request["Debug"]+"" == "1" )
			{
				this.DataGrid1.Visible = true;
				this.DataGrid2.Visible = true;
			}
			else
			{
				this.DataGrid1.Visible = false;
				this.DataGrid2.Visible = false;
			}
		}

		private void LoadData()
		{
			string code = Request["ApplicationCode"] + "";
			string caseCode = Request["CaseCode"]+"";
			
			string procedureCode = Request["ProcedureCode"]+"";
			string procedureName = Request["ProcedureName"]+"";
			//if ( procedureCode == "" )
			//	procedureCode = BLL.WorkFlowRule.GetProcedureCodeByName(procedureName);

			bool isNew = (caseCode == "" ) ;
			string userCode = Request["UserCode"] + "";
			string unitCode = Request["UnitCodeCode"] + "";
			
			
			Procedure procedure = DefinitionManager.GetProcedureDifinition(procedureCode,true);
			string currentActCode = Request["ActCode"]+"";
			Act currentAct = null;
			string actCode = "";
			WorkCase workCase = null;
			if ( isNew )
			{
				workCase = Rms.WorkFlow.WorkCaseManager.StartNewWorkCase(code,procedureCode,userCode,unitCode, ref actCode,"","");
				currentAct = (Act)(WorkCaseManager.GetActivityAct(workCase)[0]);
                if (currentAct.ApplicationSubject != null)
                    this.rdoCheck.SelectedValue = currentAct.ApplicationSubject.ToString();
			}
			else
			{
				workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(caseCode);
				currentAct = workCase.GetAct(Request["ActCode"]+"");
                if (currentAct.ApplicationSubject!=null)
                    this.rdoCheck.SelectedValue = currentAct.ApplicationSubject.ToString();
			}
            

            //this.ChkShow.Checked = (currentAct.IsSleep == 1);

           
            System.Collections.IDictionaryEnumerator ie = workCase.GetOpinionEnumerator();
            while (ie.MoveNext())
            {
                Opinion Flowopinion = (Opinion)ie.Value;
                if (Flowopinion.ApplicationCode == currentAct.ActCode)
                {
                    this.FlowOpinion.Value = Flowopinion.OpinionText;
                    switch (this.up_sPMName.ToLower())
                    {
                        case "shimaopm":
                            if (Flowopinion.OpinionText.Trim() == "")
                            {

                                Task firstTask = DefinitionManager.GetFirstTask(procedure);
                                if (currentAct.ToTaskCode == firstTask.TaskCode)
                                {
                                    this.FlowOpinion.Value = "";
                                }
                                else
                                {
                                    this.FlowOpinion.Value = "同意";
                                }
                            }
                            break;
                    }
                }

            }

            
			this.txtCurrentActCode.Value = currentAct.ActCode;

			Task currentTask = procedure.GetTask(currentAct.ToTaskCode);
            //是否需要等待
            this.WaitForFlag.Checked = (currentTask.CanEdit == 1);
			this.CopyName.InnerHtml = currentTask.TaskActorType;
			FillCopyUserSelect(workCase,procedure,currentTask);
            if (Request["Work"] + "" == "Send")
            {
                FillSendCopyUserSelect(workCase, procedure, currentTask);
            }

			this.txtProcedureCode.Value = procedureCode;
			this.txtCurrentTaskCode.Value = currentTask.TaskCode;

			if (currentTask.Copy != 1)
			{
				this.CopyTable.Visible = false;
				this.CopyTitle.Visible = false;
			}

			// 填充路由rbl
			DataSet ds = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
			DataView dv = new DataView( ds.Tables["WorkFlowRouter"],String.Format(" FromTaskCode='{0}' ",currentTask.TaskCode),"SortID",DataViewRowState.CurrentRows);

			/////////////////创建属性表///////////////////
			DataTable PropertyTable = BLL.WorkFlowRule.GetPropertyTable(workCase,procedure);
			
			if ( Request["Debug"]+"" == "1" )
			{
				this.DataGrid1.DataSource = PropertyTable;
				this.DataGrid1.DataBind();
			}
			//PropertyTable.Rows.Add(PropertyRow);

			for (int i=0;i<dv.Table.Rows.Count;i++)
			{
				System.Collections.IDictionaryEnumerator iecondition = procedure.GetRouter((string)dv.Table.Rows[i]["RouterCode"]).GetConditionEnumerator();
				if(iecondition.MoveNext())
				{
					Condition condition = (Condition)iecondition.Value;
					if(PropertyTable.Select(condition.Description).Length == 0)
					{
						dv.Table.Rows.Remove(dv.Table.Rows[i]);
						i--;
					}
				}
			}
			if ( Request["Debug"]+"" == "1" )
			{
				this.DataGrid2.DataSource = dv.Table;
				this.DataGrid2.DataBind();
			}
			///////////////////////////////////
			
			int iCount = dv.Count;
			for ( int i=0;i<iCount;i++ )
			{
				string routerCode = (String)dv[i].Row["RouterCode"];
				string description = (String)dv[i].Row["Description"];
				//Router router = (Router)ar[i];
				if(procedure.GetTask(procedure.GetRouter(routerCode).ToTaskCode).TaskType != 2)
				{
					this.rblSelectRouter.Items.Add(new ListItem(description,routerCode));
				}
			}
			if ( this.rblSelectRouter.Items.Count > 0 )
			{
				this.rblSelectRouter.SelectedIndex = 0;
				this.txtSelectRouterCode.Value = this.rblSelectRouter.SelectedItem.Value;
				FillUserSelect( workCase, this.rblSelectRouter.SelectedValue);
			}
			Session["WorkCaseApplicationTemp"] = workCase;

		}

		private void FillUserSelect ( WorkCase workCase, string routerCode )
		{
           
			string procedureCode = this.txtProcedureCode.Value;
			string currentActCode = this.txtCurrentActCode.Value;
			
			Procedure procedure = Rms.WorkFlow.DefinitionManager.GetProcedureDifinition(procedureCode,true);
			Router router = procedure.GetRouter(routerCode);
			Task nextTask = procedure.GetTask(router.ToTaskCode);

			this.ViewState.Add("WayOfSelectPerson",nextTask.WayOfSelectPerson);
			this.ViewState.Add("TaskType",nextTask.TaskType.ToString());
			
			this.txtTaskType.Value = nextTask.TaskType.ToString();
			this.txtTaskActorIDs.Value="";

            if (nextTask.TaskType == 5)
            {
                DataTable TaskActorTable = new DataTable();
                TaskActorTable.Columns.Add("Order", System.Type.GetType("System.Int32"));
                TaskActorTable.Columns.Add("TaskActorCode");

                System.Collections.IDictionaryEnumerator ieactor = nextTask.GetTaskActorEnumerator();

                while (ieactor.MoveNext())
                {
                    TaskActor taskActor = (TaskActor)ieactor.Value;
                    DataRow dr = TaskActorTable.NewRow();
                    dr["Order"] = taskActor.IOrder;
                    dr["TaskActorCode"] = taskActor.TaskActorCode;
                    TaskActorTable.Rows.Add(dr);
                }
                DataRow[] drw = TaskActorTable.Select("", "Order asc");

                string s = "";
                foreach (DataRow dr in drw)
                {

                    TaskActor ta = nextTask.GetTaskActor(dr["TaskActorCode"].ToString());
                    //ta.ActorCode
                    if (ta.TaskActorID == "0")
                    {
                        Role role = procedure.GetRole(ta.ActorCode);
                        this.txtTaskActorIDs.Value += ta.TaskActorCode + ",";
                        EntityData ed = BLL.WorkFlowRule.GetRoleUser(workCase, nextTask, ta);
                        bool ActorNeedValue = false;
                        if (ta.ActorNeed == "1")
                        {
                            ActorNeedValue = true;
                        }
                        if (ta.TaskActorID == "0")
                        {
                            switch (nextTask.WayOfSelectPerson)
                            {
                                // 不用选，合适的人都发
                                case "NoSelect":
                                    s += BuildCheckBoxListUsers(role.RoleName, ta.TaskActorCode, ed, true, ActorNeedValue);
                                    break;
                                //从中选出一个人
                                case "SinglePerson":
                                    s += BuildRadioBoxListUsers(role.RoleName, ta.TaskActorCode, ed);
                                    break;
                                //从中选出多人
                                case "MultiPerson":
                                    s += BuildCheckBoxListUsers(role.RoleName, ta.TaskActorCode, ed, false, false);
                                    break;
                            }
                        }
                        ed.Dispose();
                    }
                }
                this.tdSelectTaskActors.InnerHtml = s;
                ////////////////////////////

            }
            else
            {
                Role role = procedure.GetRole(nextTask.TaskRole);
                EntityData ed = BLL.WorkFlowRule.GetRoleUser(workCase, nextTask);
                
                switch (nextTask.WayOfSelectPerson)
                {
                    // 不用选，合适的人都发
                    case "NoSelect":
                        this.tdSelectTaskActors.InnerHtml = BuildCheckBoxListUsers(role.RoleName, "", ed, true, false);//,currentActCode),true);
                        break;
                    //从中选出一个人
                    case "SinglePerson":
                        this.tdSelectTaskActors.InnerHtml = BuildRadioBoxListUsers(role.RoleName, "", ed);
                        break;
                    //从中选出多人
                    case "MultiPerson":
                        this.tdSelectTaskActors.InnerHtml = BuildCheckBoxListUsers(role.RoleName, "", ed, false, false);
                        break;
                }
            }
		}
        private void FillSendCopyUserSelect(WorkCase workCase, Procedure procedure, Task CurrentTask)
        {
            string sCodes = "";
            string sNames = "";
            System.Collections.IDictionaryEnumerator ie = CurrentTask.GetTaskActorEnumerator();
            while (ie.MoveNext())
            {
                TaskActor ta = (TaskActor)ie.Value;
                if (ta.TaskActorID == "1"&&ta.ActorType == 1)
                {
                    if (!(ta.TaskActorType == "All" || (workCase.GetAct(this.txtCurrentActCode.Value).Copy == 1 && CurrentTask.GetTaskActor(workCase.GetAct(this.txtCurrentActCode.Value).TaskActorID).TaskActorName == "1")))
                    {
                        Role role = procedure.GetRole(ta.ActorCode);
                        EntityData ed = BLL.WorkFlowRule.GetRoleUser(workCase, CurrentTask, ta);
                        int iCount = ed.CurrentTable.Rows.Count;
                        for (int i = 0; i < iCount; i++)
                        {
                            ed.SetCurrentRow(i);
                            string UserName = RmsPM.BLL.SystemRule.GetUserNameByProjectCode(this.ProjectCode, ed.GetString("UserName"), ed.GetString("ShortUserName"), null);
                            
                            sCodes += ed.GetString("UserCode") + "," + ta.TaskActorCode + "," + UserName + "," + role.RoleName + ";";
                            sNames += UserName + ",";
                        }
                    }
                }
            }
            this.txtSendCopyUserCodes.Value = sCodes;
            if (sNames.Length > 0)
                this.SendCopyTd.InnerHtml = "<font color='red'>系统将自动为以下人员抄送一份。</font><br/>  " + sNames.Remove(sNames.Length - 1, 1);
        }
        


		private void FillCopyUserSelect (WorkCase workCase, Procedure procedure,Task CurrentTask )
		{
			/////////////////////////
			System.Collections.IDictionaryEnumerator ie = CurrentTask.GetTaskActorEnumerator();
			string s = "";
			bool IsSelectUser = false;
			while ( ie.MoveNext())
			{
				TaskActor ta = (TaskActor)ie.Value;
				if(ta.TaskActorID == "1")
				{
					if(ta.TaskActorType == "All" || (workCase.GetAct(this.txtCurrentActCode.Value).Copy == 1 && CurrentTask.GetTaskActor(workCase.GetAct(this.txtCurrentActCode.Value).TaskActorID).TaskActorName == "1"))
					{
						IsSelectUser = true;
						this.SelectCopyUserNames.Visible = true;
						this.btnAddCopyUser.Visible = true;
						Role role = procedure.GetRole(ta.ActorCode);
						this.txtCopyTaskActorCode.Value = ta.TaskActorCode;
						this.txtCopyTaskActorName.Value = role.RoleName;
					}
					else
					{
						bool ActorNeedValue = false;
						if(ta.ActorNeed == "1")
						{
							ActorNeedValue = true;
						}
						Role role = procedure.GetRole(ta.ActorCode);
						EntityData ed = BLL.WorkFlowRule.GetRoleUser(workCase,CurrentTask,ta);
						this.txtTaskCopyActorIDs.Value += ta.TaskActorCode + "," ;
						if(ta.TaskActorID == "1")
						{
							switch ( CurrentTask.WayOfSelectPerson )
							{
									// 不用选，合适的人都发
								case "NoSelect":
									s+=BuildCheckBoxListUsers( role.RoleName,ta.TaskActorCode, ed,true,ActorNeedValue);
									break;
									//从中选出一个人
								case "SinglePerson":
									//s+=BuildRadioBoxListUsers( role.RoleName,ta.TaskActorCode, ed);
                                    //以上为人员单选，应客户需求改为了人员多选
                                    s += BuildCheckBoxListUsers(role.RoleName, ta.TaskActorCode, ed, false, false);
									break;
									//从中选出多人
								case "MultiPerson":
									s+=BuildCheckBoxListUsers( role.RoleName,ta.TaskActorCode, ed,false,false);
									break;
							}
						}
					}
				}
			}

			if(!IsSelectUser)
			{
				this.SelectCopyUserNames.Visible = false;
				this.btnAddCopyUser.Visible = false;
			}

            this.tdSelectCopyTaskActors.InnerHtml = s;
			////////////////////////////
		}

        private string BuildRadioBoxListUsers(string taskActorName, string taskActorID, EntityData users)
        {

            this.tdSelectTaskActors.InnerHtml = "";
            StringBuilder sb = new StringBuilder();

            sb.Append("<table border=0 cellpadding=0 cellspace=0 id=\"TreeTable\">");
            if (taskActorName != "")
            {
                sb.Append("<tr id=\"Tr" + taskActorName + "\" status=\"block\" style=\"display:block;\"><td><a href=\"#\" onclick=\"javascript:NodeClick('Tr" + taskActorName + "');return false;\">" + taskActorName + "</a></td></tr>");
            }
            int iCount = users.CurrentTable.Rows.Count;
            List<string> UserList = new List<string>();
            for (int i = 0; i < iCount; i++)
            {
                long a = 0;
                long b = System.Math.DivRem(i, 2, out a);
                users.SetCurrentRow(i);
                if (!UserList.Contains(users.GetString("UserCode")))
                {
                    sb.Append("<tr id=\"Tr" + taskActorName + "node\" status=\"block\" style=\"display:block;\">");

                    sb.Append(" <td nowrap> <input ");
                    if (i == 0)
                        sb.Append(" checked ");

                    string UserName = RmsPM.BLL.SystemRule.GetUserNameByProjectCode(this.ProjectCode, users.GetString("UserName"), users.GetString("ShortUserName"), null);

                    sb.Append(" type=radio  name=sta" + taskActorID + " id=sta" + taskActorID + " userName=" + UserName + "  value=" + users.GetString("UserCode") + " taskActorID=" + taskActorID + " taskActorName=" + taskActorName + " >");
                    sb.Append(UserName);

                    sb.Append("</td>");

                    sb.Append("</tr>");
                    UserList.Add(users.GetString("UserCode"));
                }
            }
            sb.Append("</table>");
            return sb.ToString();

        }

        private string BuildCheckBoxListUsers(string taskActorName, string taskActorID, EntityData users, bool selectAll, bool disabledFlag)
        {
            string disabledString = "";
            if (disabledFlag)
            {
                disabledString = "disabled";
            }

            this.tdSelectTaskActors.InnerHtml = "";
            StringBuilder sb = new StringBuilder();

            sb.Append("<table border=0 cellpadding=0 cellspace=0  id=\"TreeTable\">");
            if (taskActorName != "")
            {
                sb.Append("<tr id=\"Tr" + taskActorName + "\" status=\"block\" style=\"display:block;\"><td><a href=\"#\" onclick=\"javascript:NodeClick('Tr" + taskActorName + "');return false;\">" + taskActorName + "</a></td></tr>");
            }
            int iCount = users.CurrentTable.Rows.Count;
            List<string> UserList = new List<string>();
            for (int i = 0; i < iCount; i++)
            {
                long a = 0;
                long b = System.Math.DivRem(i, 2, out a);
                users.SetCurrentRow(i);
                if (!UserList.Contains(users.GetString("UserCode")))
                {
                    sb.Append("<tr id=\"Tr" + taskActorName + "node\"  status=\"block\" style=\"display:block;\">");

                    sb.Append(" <td nowrap> <input ");
                    if (selectAll)
                        sb.Append(" checked ");

                    string UserName = RmsPM.BLL.SystemRule.GetUserNameByProjectCode(this.ProjectCode, users.GetString("UserName"), users.GetString("ShortUserName"), null);

                    sb.Append(" type=checkbox " + disabledString + " name=sta" + taskActorID + " id=sta" + taskActorID + " userName=" + UserName + "  value=" + users.GetString("UserCode") + " taskActorID=" + taskActorID + " taskActorName=" + taskActorName + " >");
                    sb.Append(UserName);

                    sb.Append("</td>");

                    sb.Append("</tr>");
                    UserList.Add(users.GetString("UserCode"));
                }
            }
            sb.Append("</table>");
            return sb.ToString();
        }

		protected void rblSelectRouter_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string routerCode = this.rblSelectRouter.SelectedValue;
			string routerName = this.rblSelectRouter.SelectedItem.Text;
			this.txtSelectRouterCode.Value = routerCode;
			this.txtSelectRouterName.Value = routerName;
			WorkCase workCase = (WorkCase)Session["WorkCaseApplicationTemp"];
			FillUserSelect( workCase,routerCode);
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            //判断密码验证逻辑
            if (user.ConfirmUserOwnName(this.txtpwd.Value))
            {
                InitPageState();
            }
            else 
            {
                this.msgspan.InnerHtml = "<font color=\"red\">您输入的密码无法通过验证！</font>";
            }
        }
}
}
