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

namespace RmsPM.Web.Remind
{
	/// <summary>
	/// NoticeAttachMentView ��ժҪ˵����
	/// </summary>
	public partial class NoticeAttachMentView : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			InitPage();
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		private void InitPage()
		{
			string AttachMentCode = Request.QueryString["AttachMentCode"] + "";
            DocumentRule documentRule = DocumentRule.Instance();
            new ViewAttachment().OutputAttachment(Response, AttachMentCode, "", documentRule);
            /*
            EntityData entityAttachMent = RemindDAO.GetNoticeAttachMentByCode(AttachMentCode);
			DataRow dr = entityAttachMent.CurrentRow;
			

			if (dr["Content"].ToString() == "" || dr["Content"] == null)
			{
				return;
			}

			if (dr["Content_Type"].ToString() == "" || dr["Content_Type"] == null)
			{
				return;
			}

			string filename = "";

			if (dr["filename"] != null) 
			{
				filename = dr["filename"].ToString();
			}

			Response.ContentType = dr["Content_Type"].ToString();
			Response.AppendHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(filename));
			Response.BinaryWrite((byte[]) dr["Content"]);
			Response.Write("window.close();");
			Response.End();
             * */
		}
	}
}
