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
	/// 查看文档附件
	/// input：
	///		AttachmentCode	附件序号
	/// </summary>
	public partial class AttachView : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!Page.IsPostBack) 
			{
				LoadData();
			}
		}

		/// <summary>
		/// 显示附件
		/// </summary>
		private void LoadData()
		{
			try
			{

				string AttachmentCode = "" + Request.QueryString["AttachmentCode"];
				string from = "" + Request.QueryString["from"];

				if ( AttachmentCode == "" ) 
				{
					this.lblMessage.Text = "无附件序号";
					return;
				}

				EntityData entity;
				DataRow dr = null;

				if (from.ToLower() == "session") 
				{
					//直接取Session中的entityDocument;
					if (Session["entityDocument"] == null) 
					{
						this.lblMessage.Text = "超时，请重新登录";
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
						this.lblMessage.Text = "没有找到该记录";
						return;
					}

					ShowContent(entity.CurrentRow);
					entity.Dispose();
				}
			}
			catch( Exception ex )
			{
				this.lblMessage.Text = "加载数据失败";
				ApplicationLog.WriteLog(this.ToString(),ex,"加载数据失败");
			}

		}

		private void ShowContent(DataRow dr) 
		{
			if ((dr["Content"] == null) || (dr["Content"].ToString() == ""))
			{
				this.lblMessage.Text = "该附件无内容";
				return;
			}

			if ((dr["Content_Type"] == null) || (dr["Content_Type"].ToString() == "")) 
			{
				this.lblMessage.Text = "未定义附件的显示方式";
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
