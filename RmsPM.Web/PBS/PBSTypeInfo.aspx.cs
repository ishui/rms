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

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// PBSTypeInfo ��ժҪ˵����
	/// </summary>
	public partial class PBSTypeInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTextArea txtDescription;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputText txtPBSTypeName;
	
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
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtPBSTypeCode.Value = Request.QueryString["PBSTypeCode"];
				this.txtParentCode.Value = Request.QueryString["ParentCode"];
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
				string code = this.txtPBSTypeCode.Value;
				if (code != "") 
				{
					EntityData entity = RmsPM.DAL.EntityDAO.PBSDAO.GetPBSTypeByCode(code);
					if (entity.HasRecord())
					{
						this.lblPBSTypeName.Text = entity.GetString("PBSTypeName");
						this.lblDescription.Text = entity.GetString("Description").Replace("\n", "<br>");
						this.txtParentCode.Value = entity.GetString("ParentCode");
						this.txtProjectCode.Value = entity.GetString("ProjectCode");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "��Ʒ��ϲ�����"));
						return;
					}
					entity.Dispose();
				}

				this.lblParentName.Text = BLL.PBSRule.GetPBSTypeName(this.txtParentCode.Value);

				((PBSTypeTreeCtrl)this.ucPBSTypeTree).SetParam(this.txtProjectCode.Value, this.txtPBSTypeCode.Value, "");
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
		/// ɾ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string code = this.txtPBSTypeCode.Value;
				BLL.PBSRule.DeletePBSType(code);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "ɾ��ʧ�ܣ�" + ex.Message));
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
			string FromUrl = this.txtFromUrl.Value.Trim();
			if (FromUrl != "") 
			{
				Response.Write(string.Format("window.location = '{0}';", FromUrl));
			}
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}
	}
}
