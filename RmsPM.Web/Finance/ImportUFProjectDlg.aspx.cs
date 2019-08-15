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
	/// ImportUFProjectDlg ��ժҪ˵����
	/// </summary>
	public partial class ImportUFProjectDlg : PageBase
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
				//��պ�����Ŀ
				BLL.PaymentRule.DeleteAllUFProject();

				StreamReader m_sr = new StreamReader(this.txtFile.PostedFile.InputStream, System.Text.Encoding.Default);

				//��1���Ǳ���
				if (m_sr.Peek() >= 0) 
				{
					m_sr.ReadLine();
				}

				SingleEntityDAO dao = new SingleEntityDAO("UFProject");
				EntityData entity = new EntityData("UFProject");

				while (m_sr.Peek() >= 0) 
				{
					string s = m_sr.ReadLine();

					ImportUFProjectSingle(s, dao, entity);

				}
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

		private void ImportUFProjectSingle(string s, SingleEntityDAO dao, EntityData entity) 
		{
			string[] m_strSub = s.Replace("\"","").Split(',');

			if (m_strSub.Length < 2)
				return;

			//Ϊ��ʱ������
			if ((m_strSub[0].Trim() == "") || (m_strSub[1].Trim() == ""))
				return;

			string code = m_strSub[0].Trim();

			DataRow dr = entity.CurrentTable.NewRow();

			dr["UFProjectCode"] = code;
			dr["UFProjectName"] = m_strSub[1].Trim();

			entity.CurrentTable.Rows.Add(dr);
			dao.InsertEntity(entity);
		}

	}

}
