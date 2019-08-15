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
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// SystemGroupClassInfo ��ժҪ˵����
	/// </summary>
	public partial class SystemGroupClassInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTextArea txtDescription;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputText txtGroupName;
		protected System.Web.UI.UserControl ucGroupTree;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)	
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage() 
		{
			try 
			{
//				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtClassCode.Value = Request.QueryString["ClassCode"];
				this.txtAct.Value = Request.QueryString["Action"];
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				string ClassCode = this.txtClassCode.Value;

				if (ClassCode == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ����������"));
					return;
				}

				EntityData entity = RmsPM.DAL.EntityDAO.SystemManageDAO.GetFunctionStructureByCode(ClassCode);
				if (entity.HasRecord())
				{
					this.lblClassName.Text = entity.GetString("FunctionStructureName");
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "��𲻴���"));
					return;
				}
				entity.Dispose();

				if (ClassCode == "0401") 
				{
					//������
					this.trToolbar.Style["display"] = "none";
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ����" + ex.Message));
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
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			string FromUrl = this.txtFromUrl.Value.Trim();
			if (FromUrl != "") 
			{
				Response.Write(string.Format("window.location = '{0}';", FromUrl));
			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// <summary>
		/// ճ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPaste_ServerClick(object sender, System.EventArgs e)
		{
			string act = "";

			try 
			{
				if (this.txtIsCut.Value == "1") 
				{
					act = "move";
					BLL.SystemGroupRule.MoveSystemGroup(this.txtSrcGroupCode.Value, "", this.txtClassCode.Value);
				}
				else
				{
					act = "insert";
					BLL.SystemGroupRule.CopySystemGroup(this.txtSrcGroupCode.Value, "", this.txtClassCode.Value);
				}
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "ճ��ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			string s = Rms.Web.JavaScript.ScriptStart
				+ string.Format("Refresh('{0}');", act)
				+ Rms.Web.JavaScript.ScriptEnd;
			this.RegisterStartupScript("start", s);
		}
	}
}
