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
	/// NoticeAttachMentView 的摘要说明。
	/// </summary>
	public partial class NoticeAttachMentView : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			InitPage();
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
