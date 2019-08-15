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
	public partial class WorkFlowOpinionWrite : PageBase
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
			}
		}

		private void IniPage()
		{
			Session["WorkCaseApplicationTemp"] = null;
		}

		private void LoadData()
		{
			string code = Request["ApplicationCode"] + "";
			string caseCode = Request["CaseCode"]+"";
			
			string procedureCode = Request["ProcedureCode"]+"";
			string procedureName = Request["ProcedureName"]+"";
			string userCode = Request["UserCode"] + "";
			string unitCode = Request["UnitCodeCode"] + "";
			
			WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(caseCode);

            Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);
            if (procedure == null)
            {
                LogHelper.WriteLog("取不到对应流程");
                throw new Exception("流程配置有误，取不到对应流程");
            }
           
          
            Act currentAct = workCase.GetAct(Request["ActCode"] + "");
            if (currentAct == null)
            {
                LogHelper.WriteLog("取不到当前流程节点");
                throw new Exception("非法操作,取不到当前流程节点");
            }
			/*if(Request["ActCode"]+"" != "")
			{
				Act act = workCase.GetAct(Request["ActCode"]+"");
				if(act.Copy == 0)
					this.ChkShow.Checked = (act.IsSleep == 1);
				else
					this.ChkShow.Visible = false;
			}*/
            
			System.Collections.IDictionaryEnumerator ie = workCase.GetOpinionEnumerator();
			while(ie.MoveNext())
			{
				Opinion Flowopinion = (Opinion)ie.Value;
                if (Flowopinion.ApplicationCode == Request["ActCode"] + "")
                {
                    this.FlowOpinion.Value = Flowopinion.OpinionText;
                    switch (this.up_sPMName.ToLower())
                    {
                        case "shimaopm":
                            if (Flowopinion.OpinionText.Trim() == "")
                            {

                                Task firstTask = DefinitionManager.GetFirstTask(procedure);
                                if (firstTask == null)
                                {
                                    LogHelper.WriteLog("流程取不到开始节点");
                                    throw new Exception("流程配置有误，请检查开始节点的属性");
                                }
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
