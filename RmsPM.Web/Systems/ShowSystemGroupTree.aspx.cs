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

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// ShowSystemGroupTree 的摘要说明。
	/// </summary>
	public partial class ShowSystemGroupTree : PageBase
	{
		protected System.Web.UI.UserControl ucPBSTypeTree;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)	
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage() 
		{
			try 
			{
//				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtAct.Value = Request.QueryString["Action"];
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];

				//调用函数名
				string MainFunc = Request.QueryString["MainFunc"] + "";
				if (MainFunc == "")
				{
					MainFunc = "GotoList";
				}
				ViewState["MainFunc"] = MainFunc;

                this.txtClassCode.Value = Request.QueryString["ClassCode"];

                //只显示某个枝条
                this.txtRootGroupCode.Value = Request.QueryString["RootGroupCode"];
                this.txtRootGroupName.Value = Request.QueryString["RootGroupName"];
                if ((this.txtRootGroupCode.Value == "") && (this.txtRootGroupName.Value != ""))
                {
                    this.txtRootGroupCode.Value = BLL.SystemGroupRule.GetSystemGroupCodeByGroupName(this.txtRootGroupName.Value, this.txtClassCode.Value);
                }
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错：" + ex.Message));
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

		private void btnUpdateProject_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "更新出错：" + ex.Message));
			}
		}

	}
}
