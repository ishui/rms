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
	public partial class WorkFlowBackTop : PageBase
	{
   
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
            string code = Request["ApplicationCode"] + "";
            string caseCode = Request["CaseCode"] + "";

            string procedureCode = Request["ProcedureCode"] + "";
            string procedureName = Request["ProcedureName"] + "";
            string userCode = Request["UserCode"] + "";
            string unitCode = Request["UnitCodeCode"] + "";
            this.ProjectCode = Request["ProjectCode"] + "";
            WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(caseCode);
            Act currentAct = workCase.GetAct(Request["ActCode"] + "");

            //this.ChkShow.Checked = (currentAct.IsSleep == 1);

            Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);

            DataSet dsp = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
            DataView dvt = new DataView(dsp.Tables["WorkFlowTask"], String.Format(" TaskType=1 "), "", DataViewRowState.CurrentRows);
            DataView dvr = new DataView(dsp.Tables["WorkFlowRouter"], String.Format(" FromTaskCode='{0}' ", dvt[0].Row["TaskCode"].ToString()), "", DataViewRowState.CurrentRows);
            string FromTaskCode = dvr[0].Row["ToTaskCode"].ToString();
            string FromTaskName = procedure.GetTask(FromTaskCode).TaskName;

            this.rblSelectRouter.Items.Add(new ListItem(FromTaskName, FromTaskCode));

            if (this.rblSelectRouter.Items.Count > 0)
            {
                this.rblSelectRouter.SelectedIndex = 0;
                this.txtSelectRouterCode.Value = this.rblSelectRouter.SelectedItem.Value;
            }

            string userCodes = "";
            System.Collections.IDictionaryEnumerator ieact = workCase.GetActEnumerator();
            while (ieact.MoveNext())
            {
                Act act = (Act)ieact.Value;
                if (act.FromTaskCode == dvt[0].Row["TaskCode"].ToString())
                {
                    userCodes = act.ActUserCode;//+",taskActorName=,,undefined;";
                    this.txtTaskActorIDs.Value += userCodes + ",";
                }
            }

            this.FlowOpinion.Value = "不同意";
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
                                this.FlowOpinion.Value = "不同意";
                            }
                            break;
                    }
                }
            }

            this.txtCurrentActCode.Value = currentAct.ActCode;

            this.tdSelectTaskActors.InnerHtml = "";
            StringBuilder sb = new StringBuilder();

            string UserName = RmsPM.BLL.SystemRule.GetUserNameByProjectCode(userCodes,this.ProjectCode,null);
            
            sb.Append("<table border=0 cellpadding=0 cellspace=0>");
            sb.Append("<tr>");
            sb.Append(" <td nowrap> <input ");
            sb.Append(" checked ");
            sb.Append(" type=checkbox name=sta" + userCodes + " id=sta" + userCodes + " userName=" + UserName + "  value=" + userCodes + " taskActorID=" + userCodes + " taskActorName=" + " >");
            sb.Append(UserName);
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            this.tdSelectTaskActors.InnerHtml = sb.ToString();
            Session["WorkCaseApplicationTemp"] = workCase;


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
