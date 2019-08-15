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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// GetTaskData 的摘要说明。
	/// </summary>
	public partial class GetTaskData : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try 
			{
				string Value = Request.QueryString["Value"] + "";
				string ProjectCode = Request.QueryString["ProjectCode"] + "";
				string Type = Request.QueryString["Type"] + "";

				string TaskCode = "";
				string TaskName = "";
				string SortID = "";
				string Hint = "";
				string IsExists = "";

				if (Value != "")
				{
					EntityData entity = null;

					if (Type.ToLower() == "code") 
					{
						//按关键字查
						entity = DAL.EntityDAO.WBSDAO.GetTaskByCode(Value);
						entity.Dispose();
					}
					else 
					{
						//按工作项编号查
						entity = DAL.EntityDAO.WBSDAO.GetTaskBySortID(Value, ProjectCode);
						entity.Dispose();
					}

					//				//按姓名查
					//				if (!entity.HasRecord()) 
					//				{
					//					entity = DAL.EntityDAO.SystemManageDAO.GetSystemUserByUserName(Value);
					//					entity.Dispose();
					//				}

					if (entity.HasRecord()) 
					{
						TaskCode = entity.GetString("WBSCode");
						TaskName = entity.GetString("TaskName");
						SortID = entity.GetString("SortID");
						IsExists = "1";
					}
					else 
					{
						Hint = "工作项不存在 ！";
					}
				}

				string sResult = "<Result>"
					+ "<TaskCode>" + TaskCode + "</TaskCode>"
					+ "<TaskName>" + TaskName + "</TaskName>"
					+ "<SortID>" + SortID + "</SortID>"
					+ "<Hint>" + Hint + "</Hint>"
					+ "<IsExists>" + IsExists + "</IsExists>"
					+ "</Result>";

				Response.Write(sResult);
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"取工作项信息出错");
				Response.Write(Rms.Web.JavaScript.Alert(true, "取工作项信息出错：" + ex.Message));
			}

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
