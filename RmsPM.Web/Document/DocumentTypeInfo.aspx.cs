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
	/// DocumentTypeInfo 的摘要说明。
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
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化页面错误");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面错误"));
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
						Response.Write(Rms.Web.JavaScript.Alert(true, "文档类型不存在"));
						return;
					}
					entity.Dispose();
				}
				else 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "未传入文档类型代码"));
					return;
				}

				this.lblParentName.Text = BLL.DocumentRule.Instance().GetDocumentTypeName(this.txtParentCode.Value);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"显示文档类型错误");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示文档类型错误"));
			}
		}

		/// <summary>
		/// 删除
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
				Response.Write(JavaScript.Alert(true, "删除失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack();
		}

		/// <summary>
		/// 返回
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
