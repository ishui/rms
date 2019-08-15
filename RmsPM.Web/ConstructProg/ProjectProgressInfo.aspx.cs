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
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.ConstructProg
{
	/// <summary>
	/// ProjectProgressInfo 的摘要说明。
	/// </summary>
	public partial class ProjectProgressInfo : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
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
				this.txtFromUrl.Value = Request.QueryString["FromUrl"];
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtWBSCode.Value = Request.QueryString["WBSCode"];

				//项目下的工作根结点
				if (this.txtWBSCode.Value == "") 
				{
					this.txtWBSCode.Value = BLL.ConstructProgRule.GetRootTaskCode(this.txtProjectCode.Value);
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private string GetHref(string WBSCode, string TaskName, bool isCurr) 
		{
			try 
			{
				string s = "";

				if (isCurr) 
				{
					//当前工作项时，也显示链接
					s = string.Format("<a href=\"#\" onclick=\"GotoTask('{0}');\">{1}</a>", WBSCode, TaskName);
//					s = TaskName;
				}
				else 
				{
					s = string.Format("<a href=\"#\" onclick=\"GotoTask('{0}');\">{1}</a>", WBSCode, TaskName);
				}

				return s;
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		private string GetNavigator(string WBSCode) 
		{
			try 
			{
				string Navigator = "";

				//导航条：显示当前位置
				EntityData entity = DAL.EntityDAO.WBSDAO.GetTaskByCode(WBSCode);
				if (entity.HasRecord()) 
				{
					string TaskName = entity.GetString("TaskName");
					string ParentCode = entity.GetString("ParentCode");
					Navigator = GetHref(WBSCode, TaskName, true);

					while (ParentCode != "") 
					{
						EntityData entity1 = DAL.EntityDAO.WBSDAO.GetTaskByCode(ParentCode);
						if (entity1.HasRecord()) 
						{
							string CurrWBSCode = entity1.GetString("WBSCode");
							ParentCode = entity1.GetString("ParentCode");
							TaskName = entity1.GetString("TaskName");

							Navigator = GetHref(CurrWBSCode, TaskName, false) + " -> " + Navigator;
						}
						else 
						{
							ParentCode = "";
						}
						entity1.Dispose();
					}
				}
				entity.Dispose();

				return Navigator;
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		private void LoadData() 
		{
			try 
			{
				string WBSCode = this.txtWBSCode.Value.Trim();

				//this.lblNavigator.Text = GetNavigator(WBSCode);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
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
			Response.End();
		}

	}
}
