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


namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSAttachMentView 的摘要说明。
	/// </summary>
	public partial class WBSAttachMentView : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				// 在此处放置用户代码以初始化页面
				if (!this.IsPostBack)
				{
					InitPage();
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"处理附件失败");
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

		private void InitPage()
		{
			string AttachMentCode = Request.QueryString["AttachMentCode"] + "";
			string Action = Request.QueryString["Action"] + "";

            string AttachMent = Request.QueryString["AttachMent"] + "";
            
            DocumentRule documentRule = DocumentRule.Instance();
            if (Action.ToLower() == "view")
            {
                new ViewAttachment().OutputAttachment(Response,AttachMentCode, AttachMent, documentRule);

            }
            if(Action.ToLower()=="del")
			{
                try
                {
                    documentRule.DeleteAttachment(AttachMentCode);

                }
                catch (Exception ex)
                {
                    Rms.LogHelper.LogHelper.Error("删除附件时异常", ex);
                }
                Response.Write(JavaScript.ScriptStart);
				  Response.Write("window.opener.AttachMentRefresh();");                
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);
			}
            /*
            EntityData entityAttachMent = DAL.EntityDAO.AttachmentDAO.GetAttachMentByCode(AttachMentCode);
			if(Action=="View")
			{
				DataRow dr = entityAttachMent.CurrentRow;					

				if (dr["Content"].ToString() == "" || dr["Content"] == null)
				{
					this.lblMessage.Text = "该附件无内容";
					return;
				}

				if (dr["Content_Type"].ToString() == "" || dr["Content_Type"] == null)
				{
					this.lblMessage.Text = "未定义附件的显示方式";
					return;
				}

				string filename = "";

				if (dr["filename"] != null) 
				{
					filename = dr["filename"].ToString();
				}
				entityAttachMent.Dispose();
				Response.ContentType = dr["Content_Type"].ToString();
                switch (AttachMent)
                {
                    case "0":
                        break;
                    case "1":
                        Response.AppendHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(filename));
                        break;
                    default:
                        //Response.AppendHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(filename));
                        Response.AppendHeader("Content-Disposition", "filename=" + Server.UrlEncode(filename));
                        break;

                }
				Response.BinaryWrite((byte[]) dr["Content"]);
				Response.End();
			}*/
			
		}

        private void OutputAttachment(string AttachMentCode, string AttachMent, DocumentRule documentRule)
        {
            documentRule.GetAttachmentByCode(AttachMentCode);
            Response.Clear();
            Response.ContentType = documentRule.ContentType;
            switch (AttachMent)
            {
                case "0":
                    break;
                case "1":
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(documentRule.FileName));
                    break;
                default:
                    //Response.AppendHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(filename));
                    Response.AppendHeader("Content-Disposition", "filename=" + Server.UrlEncode(documentRule.FileName));
                    break;

            }
            Response.BinaryWrite(documentRule.Content);
            Response.Flush();
            Response.End();
        }
	}
}
