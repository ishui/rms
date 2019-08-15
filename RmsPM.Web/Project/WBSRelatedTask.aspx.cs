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
using Rms.Web;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSRelatedTask 的摘要说明。
	/// </summary>
    public partial class WBSRelatedTask : System.Web.UI.Page
	{
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!this.IsPostBack)
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
			this.dgTaskList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgTaskList_PageIndexChanged);
			this.SaveToolsButton.Click += new System.EventHandler(this.SaveToolsButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		/// <summary>
		/// 初始化合同列表
		/// </summary>
		private void LoadData()
		{
			try
			{
				EntityData entityWBS = WBSDAO.GetAllTask();
				if ( entityWBS.HasRecord())
				{
					this.dgTaskList.DataSource = entityWBS.CurrentTable;
					this.dgTaskList.DataBind();
					this.SaveToolsButton.Visible = true;
					this.CancelToolsButton.Visible = true;
				}
				else
				{
					this.SaveToolsButton.Visible = false;
					this.CancelToolsButton.Visible = false;
				}
				dgTaskList.Dispose();
			}
			catch ( Exception ex)
			{
				ApplicationLog.WriteLog ( this.ToString(),ex,"");
			}
		}

		/// <summary>
		/// 保存选中的合同为工作相关合同
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SaveToolsButton_Click(object sender, System.EventArgs e)
		{
			System.Web.UI.WebControls.CheckBox chkWBS ;
			string WBSCode = Request.QueryString["WBSCode"];

			EntityData entityWBS = WBSDAO.GetTaskRelatedByWBSCode(WBSCode);
			DataRow drWBS;

			try
			{
				foreach(DataGridItem oDataGridItem in this.dgTaskList.Items)
				{
					chkWBS = (CheckBox)oDataGridItem.FindControl("checkContract");
					if (chkWBS.Checked == true)
					{
						drWBS = entityWBS.GetNewRecord();
						drWBS["TaskRelatedCode"] = SystemManageDAO.GetNewSysCode("TaskRelatedCode");
						drWBS["RelatedWBSCode"] = (string)this.dgTaskList.DataKeys[oDataGridItem.ItemIndex];
						drWBS["WBSCode"] = WBSCode;
						entityWBS.AddNewRecord(drWBS);
					}
					
				}
				WBSDAO.InsertTaskRelated(entityWBS);
				entityWBS.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"保存相关合同失败");
			}


			Response.Write(JavaScript.ScriptStart);
			Response.Write("window.opener.Update(2);");
			Response.Write("window.close();");
			Response.Write(JavaScript.ScriptEnd);
		}

		/// <summary>
		/// 翻页事件
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dgTaskList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgTaskList.CurrentPageIndex = e.NewPageIndex;
			LoadData();
		}
	}
}
