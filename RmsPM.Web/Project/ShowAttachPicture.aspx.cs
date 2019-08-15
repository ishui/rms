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
	/// ShowAttachPicture 的摘要说明。
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
					ApplicationLog.WriteLog(this.ToString(), ex, "读取图片失败");
				}
			}
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
