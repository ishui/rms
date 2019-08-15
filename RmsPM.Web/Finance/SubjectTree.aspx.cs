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

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// SubjectTree ��ժҪ˵����
	/// </summary>
	public partial class SubjectTree : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try 
			{
				string unitCode = Request["CorpCode"]+"";
				int isSelfAccount = 0;
				string subjectSetCode = Request["SubjectSetCode"]+"";
				if ( subjectSetCode == "" && unitCode != "" )
					subjectSetCode = BLL.SubjectRule.GetUnitSubjectSet(unitCode, ref isSelfAccount);

				this.txtSubjectSetCode.Value = subjectSetCode;
//				this.txtSelfAccount.Value = isSelfAccount.ToString();
//				if ( isSelfAccount == 0 )
//				{
//					this.btnAdd.Visible = false;
//					this.btnImport.Visible = false;
//				}

				Rms.ORMap.EntityData m_SubjectSet=SubjectDAO.GetSubjectSetByCode(this.txtSubjectSetCode.Value);
				this.LabelSubjectSetName.Text = m_SubjectSet.GetString("SubjectSetName");
				m_SubjectSet.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
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
