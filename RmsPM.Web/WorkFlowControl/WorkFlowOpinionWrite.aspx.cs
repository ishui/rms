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
	/// WorkFlowRouterCopy ��ժҪ˵����
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
                    li.Text = "--�������--";
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
                LogHelper.WriteLog("ȡ������Ӧ����");
                throw new Exception("������������ȡ������Ӧ����");
            }
           
          
            Act currentAct = workCase.GetAct(Request["ActCode"] + "");
            if (currentAct == null)
            {
                LogHelper.WriteLog("ȡ������ǰ���̽ڵ�");
                throw new Exception("�Ƿ�����,ȡ������ǰ���̽ڵ�");
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
                                    LogHelper.WriteLog("����ȡ������ʼ�ڵ�");
                                    throw new Exception("���������������鿪ʼ�ڵ������");
                                }
                                if (currentAct.ToTaskCode == firstTask.TaskCode)
                                {
                                    this.FlowOpinion.Value = "";
                                }
                                else
                                {
                                    this.FlowOpinion.Value = "ͬ��";
                                }
                            }
                            break;
                    }
                }
			}
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
	}
}
