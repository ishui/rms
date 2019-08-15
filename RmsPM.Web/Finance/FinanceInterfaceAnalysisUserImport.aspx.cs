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
using System.IO;
using Rms.ORMap;
using Rms.Web;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// FinanceInterfaceAnalysisUserImport ��ժҪ˵����
	/// </summary>
	public partial class FinanceInterfaceAnalysisUserImport : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
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

		private void IniPage() 
		{
			try 
			{
				this.txtSubjectSetCode.Value = Request.QueryString["SubjectSetCode"];

				if (this.txtSubjectSetCode.Value == "") 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ�������״���"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
				}

				this.lblSubjectSetName.Text = BLL.SubjectRule.GetSubjectSetName(this.txtSubjectSetCode.Value);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		protected void btnOK_ServerClick(object sender, System.EventArgs e)
		{
			if (this.txtFile.PostedFile.FileName == "") 
			{
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ѡ���ļ�"));
				return;
			}

			try
			{
				string SubjectSetCode = this.txtSubjectSetCode.Value;
				if (SubjectSetCode == "")
					throw new Exception("δ�������ױ��");

				StreamReader m_sr = new StreamReader(this.txtFile.PostedFile.InputStream, System.Text.Encoding.Default);

				//��1���Ǳ���
				if (m_sr.Peek() >= 0) 
				{
					m_sr.ReadLine();
				}

				EntityData entityUser = DAL.EntityDAO.SystemManageDAO.GetAllSystemUser();
				EntityData entity = DAL.EntityDAO.SystemManageDAO.GetSystemUserSubjectSetBySubjectSet(SubjectSetCode);

				while (m_sr.Peek() >= 0) 
				{
					string s = m_sr.ReadLine();

					ImportSingle(s, SubjectSetCode, entity, entityUser);

				}

				DAL.EntityDAO.SystemManageDAO.SubmitAllSystemUserSubjectSet(entity);
				entity.Dispose();
				entityUser.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				throw ex;
			}

			Response.Write(JavaScript.ScriptStart);
			Response.Write(JavaScript.Alert(false,"�������"));
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(JavaScript.WinClose(false));
			Response.Write(JavaScript.ScriptEnd);
			Response.End();
		}

		private void ImportSingle(string s, string SubjectSetCode, EntityData entity, EntityData entityUser) 
		{
			string[] arr = BLL.ImportRule.SplitCsvLine(s);

			if (arr.Length < 1)
				return;

			//Ϊ��ʱ������
			if (arr[0].Trim() == "")
				return;

			string UserName = arr[0].Trim();

			//����Ա������
			DataRow[] drsUser = entityUser.CurrentTable.Select("UserName = '" + UserName + "'");
			foreach(DataRow drUser in drsUser) 
			{
				string UserCode = BLL.ConvertRule.ToString(drUser["UserCode"]);

				DataRow dr;
				DataRow[] drs = entity.CurrentTable.Select("UserCode = '" + UserCode + "'");
				if (drs.Length > 0) 
				{
					dr = drs[0];
				}
				else 
				{
					dr = entity.CurrentTable.NewRow();
					dr["SystemUserSubjectSetCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("SystemUserSubjectSetCode");
					dr["SubjectSetCode"] = SubjectSetCode;
					dr["UserCode"] = UserCode;
					entity.CurrentTable.Rows.Add(dr);
				}

				dr["U8Code"] = arr[1].Trim();
			}
		}

	}

}
