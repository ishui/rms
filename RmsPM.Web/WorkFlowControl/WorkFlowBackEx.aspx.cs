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
	public partial class WorkFlowBackEx : PageBase
	{
     //


	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			AttachMentAdd1.AttachMentType = "WorkFlowActOpinion";
			AttachMentAdd1.MasterCode = Request["ActCode"]+"";
			if(!IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{
			Session["WorkCaseApplicationTemp"] = null;
		}

		private void LoadData()
		{
			string caseCode = Request["CaseCode"]+"";
			string actCode = Request["ActCode"]+"";
            this.ProjectCode = Request["ProjectCode"] + "";
			WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(caseCode);
			Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode,true);
			Act currentAct = workCase.GetAct(Request["ActCode"]+"");

			//this.ChkShow.Checked = (currentAct.IsSleep == 1);
            this.FlowOpinion.Value = "不同意";
			System.Collections.IDictionaryEnumerator ie = workCase.GetOpinionEnumerator();
			while(ie.MoveNext())
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
                                this.FlowOpinion.Value = "不同意";
                            }
                            break;
                    }
                }
			}

			Task currentTask = procedure.GetTask(currentAct.ToTaskCode);
			
			DataSet ds = Rms.WorkFlow.WorkCaseManager.SaveWorkCaseData(workCase);

			//BLL.ConvertRule.DataTableDistinctRow(ds.Tables["WorkFlowAct"],"FromTaskCode");
			//BLL.ConvertRule.DataTableDistinctRow(ds.Tables["WorkFlowAct"],"FromTaskCode");
			//DataView dv = new DataView( ds.Tables["WorkFlowAct"],String.Format(" FromTaskCode<>'{0}' and ActType <> 1 and  Copy <> 1",currentTask.TaskCode),"",DataViewRowState.CurrentRows);
            //clm修改 日期：2006-11-2
            DataView dv = new DataView(ds.Tables["WorkFlowAct"], String.Format(" FromTaskCode<>'{0}' and  Copy <> 1", currentTask.TaskCode), "", DataViewRowState.CurrentRows);
			dv = BLL.ConvertRule.GetDistinct(dv,"FromTaskCode");
			//BLL.ConvertRule.DataViewDistinctRow(dv,"FromTaskCode");


			if(Request["Debug"]+"" == "1")
			{
				this.DataGrid1.DataSource = dv;
				this.DataGrid1.DataBind();
			}
			
			int iCount = dv.Count;
			for ( int i=0;i<iCount;i++ )
			{
				string FromTaskCode = (String)dv[i].Row["FromTaskCode"];
				string FromTaskName = "退回给"+procedure.GetTask(FromTaskCode).TaskName;
				if(procedure.GetTask(FromTaskCode).TaskType != 1)
                    this.rblSelectRouter.Items.Add(new ListItem(FromTaskName,FromTaskCode));
			}
			if ( this.rblSelectRouter.Items.Count > 0 )
			{
				this.rblSelectRouter.SelectedIndex = 0;
				this.txtSelectRouterCode.Value = this.rblSelectRouter.SelectedItem.Value;
				FillUserSelect( workCase, this.rblSelectRouter.SelectedValue);
			}
			Session["WorkCaseApplicationTemp"] = workCase;

		}

		private void FillUserSelect ( WorkCase workCase, string FromTaskCode )
		{
			string procedureCode = this.txtProcedureCode.Value;
			string currentActCode = this.txtCurrentActCode.Value;
			
			Procedure procedure = Rms.WorkFlow.DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode,true);
			Task FromTask = procedure.GetTask(FromTaskCode);
			
			DataSet ds = Rms.WorkFlow.WorkCaseManager.SaveWorkCaseData(workCase);
			//BLL.ConvertRule.DataTableDistinctRow(ds.Tables["WorkFlowAct"],"ActUserCode");
			//DataView dv = new DataView( ds.Tables["WorkFlowAct"],String.Format(" ToTaskCode='{0}' and ActType <> 1 and status='End' and  Copy <> 1",FromTaskCode),"",DataViewRowState.CurrentRows);
            //clm修改 日期：2006-11-2
            DataView dv = new DataView(ds.Tables["WorkFlowAct"], String.Format(" ToTaskCode='{0}' and status='End' and  Copy <> 1", FromTaskCode), "", DataViewRowState.CurrentRows);
			dv = BLL.ConvertRule.GetDistinct(dv,"ActUserCode");
			//BLL.ConvertRule.DataViewDistinctRow(dv,"ActUserCode");


			if(Request["Debug"]+"" == "1")
			{
				this.DataGrid2.DataSource = dv;
				this.DataGrid2.DataBind();
			}

			string s = "";
			this.txtTaskActorIDs.Value = "";
			int iCount = dv.Count;
            for (int i = 0; i < iCount; i++)
            {
                string userCode = (String)dv[i].Row["ActUserCode"];
                this.txtTaskActorIDs.Value += userCode + ",";
                string UserName = RmsPM.BLL.SystemRule.GetUserNameByProjectCode(userCode,this.ProjectCode, null);
                s += BuildCheckBoxListUsers("", userCode, userCode, true, false, UserName);
            }

			this.tdSelectTaskActors.InnerHtml = s;
		}


		
		private string BuildCheckBoxListUsers ( string taskActorName , string taskActorID, string userCode, bool selectAll,bool disabledFlag,string UserName)
		{
			string disabledString = "";
			if(disabledFlag)
			{
				disabledString = "disabled";
			}
			
			this.tdSelectTaskActors.InnerHtml = "";
			StringBuilder sb = new StringBuilder ();

			sb.Append( "<table border=0 cellpadding=0 cellspace=0>" );
			if ( taskActorName != "" )
			{
				sb.Append( "<tr><td>"+taskActorName+"</td></tr>" );
			}
				long a = 0;
				long b =  System.Math.DivRem(0,2, out a);
				if ( a == 0 )
					sb.Append("<tr>");


				sb.Append( " <td nowrap> <input " );
				if ( selectAll )
					sb.Append( " checked " );
				sb.Append(" type=checkbox "+disabledString+" name=sta"+taskActorID + " id=sta" + taskActorID  + " userName="  + UserName + "  value="+ userCode + " taskActorID=" + taskActorID + " taskActorName=" + taskActorName  +  " >" );
				sb.Append ( UserName );
				sb.Append("</td>");
				if ( a == 0 )
					sb.Append( "</tr>" );
			sb.Append( "</table>" );
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
	}
}
