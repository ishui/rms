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
	/// MapSalSupl ��ժҪ˵����
	/// </summary>
	public partial class MapSalSupl : PageBase
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
			this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
			this.txtRefreshScript.Value = Request.QueryString["RefreshScript"];
		}

		protected void btnOK_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				int type = int.Parse(this.rdoType.SelectedValue);
				BLL.DtsPayRule.MapSalSupl(txtProjectCode.Value, type);
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				throw ex;
			}

			Response.Write(JavaScript.ScriptStart);
//			Response.Write(JavaScript.Alert(false,"��Ӧ���"));

			if (this.txtRefreshScript.Value.Trim() != "")
			{
				Response.Write(string.Format("window.opener.{0}", this.txtRefreshScript.Value));
			}
			else 
			{
				Response.Write("window.opener.location = window.opener.location;");
			}
			Response.Write(JavaScript.WinClose(false));
			Response.Write(JavaScript.ScriptEnd);
			Response.End();
		}
	}

}
