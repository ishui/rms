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
using Rms.Web;

namespace RmsPM.Web.Sal
{
	/// <summary>
	/// SalSuplModify ��ժҪ˵����
	/// </summary>
	public partial class SalSuplModify : PageBase
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
				BLL.PageFacade.LoadProjectSelect(this.sltProject, txtProjectCode.Value);

				this.txtSystemID.Value = Request.QueryString["SystemID"];
				string SystemID = this.txtSystemID.Value.Trim();

				if (SystemID != "") 
				{
					EntityData entity = DAL.EntityDAO.SalDAO.GetSalSuplByCode(SystemID);
					if (entity.HasRecord()) 
					{
						this.txtSuplCode.Value = entity.GetString("SuplCode");
						this.txtSuplName.Value = entity.GetString("SuplName");
						this.sltProject.Value = entity.GetString("ProjectCode");
					}
					entity.Dispose();
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.txtSuplCode.Value.Trim() == "") 
			{
				Hint = "�����빩Ӧ�̴���";
				return false;
			}

			if (this.txtSuplName.Value.Trim() == "") 
			{
				Hint = "�����빩Ӧ������";
				return false;
			}

			return true;
		}

		protected void btnOK_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				string SystemID = this.txtSystemID.Value.Trim();
				EntityData entity = DAL.EntityDAO.SalDAO.GetSalSuplByCode(SystemID);
				DataRow dr;

				if (entity.HasRecord()) 
				{
					dr = entity.CurrentRow;
				}
				else 
				{
					dr = entity.CurrentTable.NewRow();
					dr["SystemID"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("SalSuplSystemID");
				}

				dr["ProjectCode"] = this.sltProject.Value.Trim();
				dr["SuplCode"] = this.txtSuplCode.Value.Trim();
				dr["SuplName"] = this.txtSuplName.Value.Trim();

				if (entity.HasRecord()) 
				{
					DAL.EntityDAO.SalDAO.UpdateSalSupl(entity);
				}
				else 
				{
					entity.CurrentTable.Rows.Add(dr);
					DAL.EntityDAO.SalDAO.InsertSalSupl(entity);
				}

				entity.Dispose();

				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.opener.location = window.opener.location;");
				Response.Write(JavaScript.WinClose(false));
				Response.Write(JavaScript.ScriptEnd);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
			}
		}
	}
}
