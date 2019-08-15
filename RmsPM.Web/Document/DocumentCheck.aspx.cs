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
using Rms.Web;

namespace RmsPM.Web.Document
{
	/// <summary>
	/// DocumentCheck ��ժҪ˵����
	/// </summary>
	public partial class DocumentCheck : PageBase
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{
			try
			{
				this.txtDocumentCode.Value = Request.QueryString["DocumentCode"];
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			string DocumentCode = this.txtDocumentCode.Value;

			try
			{
                if (DocumentCode == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ�����ĵ���"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}

                EntityData entity = DAL.EntityDAO.DocumentDAO.GetDocumentByCode(DocumentCode);
				if ( entity.HasRecord())
				{
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "�ĵ�������"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}

				entity.Dispose();
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ���ݳ���" + ex.Message));
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
		/// ���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string DocumentCode = this.txtDocumentCode.Value;
				BLL.DocumentRule.Instance().CheckDocument(DocumentCode, base.user.UserCode);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "���ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack();
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write("if (window.opener.opener) window.opener.opener.location = window.opener.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

	}
}
