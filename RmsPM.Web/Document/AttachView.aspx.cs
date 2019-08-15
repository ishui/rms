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
	/// �鿴�ĵ�����
	/// input��
	///		AttachmentCode	�������
	/// </summary>
	public partial class AttachView : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if (!Page.IsPostBack) 
			{
				LoadData();
			}
		}

		/// <summary>
		/// ��ʾ����
		/// </summary>
		private void LoadData()
		{
			try
			{

				string AttachmentCode = "" + Request.QueryString["AttachmentCode"];
				string from = "" + Request.QueryString["from"];

				if ( AttachmentCode == "" ) 
				{
					this.lblMessage.Text = "�޸������";
					return;
				}

				EntityData entity;
				DataRow dr = null;

				if (from.ToLower() == "session") 
				{
					//ֱ��ȡSession�е�entityDocument;
					if (Session["entityDocument"] == null) 
					{
						this.lblMessage.Text = "��ʱ�������µ�¼";
						return;
					}

					entity = (EntityData)Session["entityDocument"];
					entity.SetCurrentTable("Attachment");
					foreach(DataRow drTemp in entity.CurrentTable.Rows) 
					{
						if (drTemp["AttachmentCode"].ToString() == AttachmentCode) 
						{
							dr = drTemp;
							break;
						}
					}

					if (dr != null) 
					{
						ShowContent(dr);
					}
				}
				else 
				{
                    entity = RmsPM.DAL.EntityDAO.DAOFactory.GetAttachmentDAO().GetAttachMentByCode(AttachmentCode);

					if ( !entity.HasRecord() )
					{
						this.lblMessage.Text = "û���ҵ��ü�¼";
						return;
					}

					ShowContent(entity.CurrentRow);
					entity.Dispose();
				}
			}
			catch( Exception ex )
			{
				this.lblMessage.Text = "��������ʧ��";
				ApplicationLog.WriteLog(this.ToString(),ex,"��������ʧ��");
			}

		}

		private void ShowContent(DataRow dr) 
		{
			if ((dr["Content"] == null) || (dr["Content"].ToString() == ""))
			{
				this.lblMessage.Text = "�ø���������";
				return;
			}

			if ((dr["Content_Type"] == null) || (dr["Content_Type"].ToString() == "")) 
			{
				this.lblMessage.Text = "δ���帽������ʾ��ʽ";
				return;
			}

			string filename = "";

			if (dr["filename"] != null) 
			{
				filename = dr["filename"].ToString();
			}

			Response.ContentType = dr["content_type"].ToString();
			Response.AppendHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(filename));
			Response.BinaryWrite((byte[]) dr["Content"]);
			Response.End();
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
	}
}
