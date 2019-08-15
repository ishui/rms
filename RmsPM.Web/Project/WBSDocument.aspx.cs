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
using RmsPM.DAL;
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSDocument 的摘要说明。
	/// </summary>
    public partial class WBSDocument : System.Web.UI.Page
	{
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if ( !IsPostBack)
			{
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
			this.dgDocumentList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgDocumentList_PageIndexChanged);
			this.SaveToolsButton.Click += new System.EventHandler(this.SaveToolsButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// 初始化文档列表
		/// </summary>
		private void LoadData()
		{
			try
			{
				EntityData entityDocument = DAL.EntityDAO.DocumentDAO.GetAllDocument();
				if ( entityDocument.HasRecord())
				{
					this.dgDocumentList.DataSource = entityDocument.CurrentTable;
					this.dgDocumentList.DataBind();
				}
				else
				{
					this.SaveToolsButton.Visible = false;
					this.CancelToolsButton.Visible = false;
				}
				entityDocument.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}

		}

		/// <summary>
		/// 保存选中的文档为工作项相关文档
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SaveToolsButton_Click(object sender, System.EventArgs e)
		{
		
		}

		/// <summary>
		/// 翻页事件处理
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dgDocumentList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgDocumentList.CurrentPageIndex = e.NewPageIndex;
			LoadData();
		}
	}
}
