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

namespace RmsPM.Web.Sal
{
	/// <summary>
	/// SalSuplImport ��ժҪ˵����
	/// </summary>
	public partial class SalSuplImport : PageBase
	{
		protected System.Web.UI.WebControls.Label Label1;
	
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
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				BLL.PageFacade.LoadProjectSelect(this.sltProject, this.txtProjectCode.Value);

				//			if (this.txtProjectCode.Value == "") 
				//			{
				//				Response.Write(Rms.Web.JavaScript.Alert(true, "����Ŀ���룬����ѡ����Ŀ"));
				//				Response.End();
				//			}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		protected void btnOK_ServerClick(object sender, System.EventArgs e)
		{
			if (this.txtFile.PostedFile.FileName == "") 
			{
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ѡ���ļ�"));
				return;
			}

			string ProjectCode = this.sltProject.Value.Trim();
//			if (ProjectCode == "") 
//			{
//				Response.Write(Rms.Web.JavaScript.Alert(true, "��ѡ���Ӧ��Ŀ"));
//				return;
//			}

			try
			{
				//ɾ������Ŀ�Ĺ�Ӧ��
				DAL.EntityDAO.SalDAO.DeleteSalSuplByProjectCode(ProjectCode);

				StreamReader m_sr = new StreamReader(this.txtFile.PostedFile.InputStream, System.Text.Encoding.Default);

				SingleEntityDAO dao = new SingleEntityDAO("SalSupl");
				EntityData entity = new EntityData("SalSupl");

				//��1���Ǳ���
				if (m_sr.Peek() >= 0) 
				{
					m_sr.ReadLine();
				}

				while (m_sr.Peek() >= 0) 
				{
					string s = m_sr.ReadLine();
					string[] m_strSub = s.Replace("\"","").Split(',');

					if (m_strSub.Length < 3)
						continue;

					if (m_strSub[0].Trim() == "") 
						continue;

					DataRow dr = entity.CurrentTable.NewRow();

					dr["SystemID"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("SalSuplSystemID");
					dr["ProjectCode"] = ProjectCode;
					dr["SuplCode"] = m_strSub[0].Trim();
					dr["SuplName"] = m_strSub[1].Trim();

					entity.CurrentTable.Rows.Add(dr);
					dao.InsertEntity(entity);
				}
				entity.Dispose();
				dao.Dispose();
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
	}
}
