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

namespace RmsPM.Web.ConstructProg
{
	/// <summary>
	/// ProgressPic ��ժҪ˵����
	/// </summary>
	public partial class ProgressPic : PageBase
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
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

                this.UCPicGroup1.RootPath = "../";
                this.UCPicGroup1.MasterType = "ProjectProgress";
                this.UCPicGroup1.MasterCode = this.txtProjectCode.Value;

                if (!base.user.HasRight("030502"))
                {
                    this.UCPicGroup1.SetReadonly(true);
                }
            }
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

	}
}
