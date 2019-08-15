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
	/// ShowPicture ��ժҪ˵����
	/// </summary>
	public partial class ShowPicture : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string fileID = "" + Request["FileID"];
			
			if ( fileID == "" )
				return;

			if ( !Page.IsPostBack )
			{

				try
				{
					EntityData entity=RmsPM.DAL.EntityDAO.ProductDAO.GetPhotosByCode(fileID);
					if ( entity.HasRecord() )
					{
						if ( !entity.CurrentRow.IsNull("PicContent") )
							Response.BinaryWrite((byte[]) entity.CurrentRow["PicContent"] );

						//Response.BinaryWrite((byte[])myDataReader["imgdata"]);

					}
					entity.Dispose();

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
