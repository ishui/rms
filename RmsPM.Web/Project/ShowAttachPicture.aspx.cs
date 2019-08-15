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

namespace RmsPM.Web.Project
{
	/// <summary>
	/// ShowAttachPicture ��ժҪ˵����
	/// </summary>
	public partial class ShowAttachPicture : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string AttachMentCode = "" + Request["AttachMentCode"];
			
			if ( AttachMentCode == "" )
				return;

			if ( !Page.IsPostBack )
			{

				try
				{
                    //modi by simon
                    DocumentRule documentRule = DocumentRule.Instance();
                    documentRule.GetAttachmentByCode(AttachMentCode);
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "image/jpeg";
                    Response.BinaryWrite(documentRule.Content);
                    Response.Flush();
                    Response.End();
                    /*
                    EntityData entity = DAL.EntityDAO.WBSDAO.GetAttachMentByCode(AttachMentCode);
					if (entity.HasRecord()) 
					{
						if ( !entity.CurrentRow.IsNull("Content") )
							Response.BinaryWrite((byte[]) entity.CurrentRow["Content"] );
					}
					entity.Dispose();
                     * */
				}
				catch ( Exception ex )
				{
					ApplicationLog.WriteLog(this.ToString(), ex, "��ȡͼƬʧ��");
				}
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
	}
}
