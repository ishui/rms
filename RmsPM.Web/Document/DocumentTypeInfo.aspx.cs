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
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.Web;
using Rms.Web;

namespace RmsPM.Web.Document
{
	/// <summary>
	/// DocumentTypeInfo ��ժҪ˵����
	/// </summary>
	public partial class DocumentTypeInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTableCell tdTitle;
//		private bool iIsFolder;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				IniPage();
				LoadData();
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
				this.txtDocumentTypeCode.Value = Request.QueryString["DocumentTypeCode"];
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];

				if (this.txtFromUrl.Value.Trim() == "") 
				{
					this.txtFromUrl.Value = "../Document/DocumentType.aspx";
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ��ҳ�����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����"));
			}
		}

		private void LoadData()
		{
			try 
			{
				if ( this.txtDocumentTypeCode.Value != "" ) 
				{
					EntityData entity = DocumentDAO.GetDocumentTypeByCode(this.txtDocumentTypeCode.Value);

					if(entity.HasRecord())
					{
						this.lblTypeName.Text = entity.GetString("TypeName");
						this.lblDescription.Text = entity.GetString("Description").Replace("\n", "<br>");
						this.txtParentCode.Value = entity.GetString("ParentCode");
					}
					else 
					{
						Response.Write(Rms.Web.JavaScript.Alert(true, "�ĵ����Ͳ�����"));
						return;
					}
					entity.Dispose();
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ�����ĵ����ʹ���"));
					return;
				}

				this.lblParentName.Text = BLL.DocumentRule.Instance().GetDocumentTypeName(this.txtParentCode.Value);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʾ�ĵ����ʹ���");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�ĵ����ʹ���"));
			}
		}

		/// <summary>
		/// ɾ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				BLL.DocumentRule.Instance().DeleteDocumentType(this.txtDocumentTypeCode.Value);
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
