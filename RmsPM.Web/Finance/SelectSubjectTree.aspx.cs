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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// SelectSubjectTree ��ժҪ˵����
	/// </summary>
	public partial class SelectSubjectTree : PageBase
	{
		protected string ReturnFunc = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
                this.txtSubjectSetCode.Value = Request.QueryString["SubjectSetCode"];
                this.txtSubjectCode.Value = Request.QueryString["SubjectCode"];
				this.txtAct.Value = Request.QueryString["Act"];
				this.txtReturnFunc.Value = Request["ReturnFunc"];
				this.txtDefine1.Value = Request["Define1"];
				this.txtSearchSubjectCode.Value = this.txtSubjectCode.Value;

				if (this.txtReturnFunc.Value == "") 
				{
					this.txtReturnFunc.Value = "SelectSubjectReturn";
				}

				this.ReturnFunc = this.txtReturnFunc.Value;

                if (this.txtSubjectSetCode.Value == "")
                {
                    this.txtSubjectSetCode.Value = BLL.ProjectRule.GetSubjectSetCodeByProject(txtProjectCode.Value);
                }

                this.LabelSubjectSetName.Text = BLL.SubjectRule.GetSubjectSetName(this.txtSubjectSetCode.Value);

				switch (this.txtAct.Value.ToLower()) 
				{
					case "auto":
						AutoLoad();
						break;
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

		/// <summary>
		/// �������ѯ��ֱ�ӷ�������
		/// </summary>
		private void AutoLoad() 
		{
			try 
			{
				string SubjectCode = this.txtSearchSubjectCode.Value;
				string SubjectName = BLL.SubjectRule.GetSubjectFullName(SubjectCode, this.txtSubjectSetCode.Value);

				Response.Write(Rms.Web.JavaScript.ScriptStart);
				Response.Write(string.Format("window.opener.{0}('{1}', '{2}', '{3}');", this.txtReturnFunc.Value, SubjectCode, SubjectName, this.txtDefine1.Value));
				Response.Write("window.close();");
				Response.Write(Rms.Web.JavaScript.ScriptEnd);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ѯ����" + ex.Message));
			}
		}
	}
}
