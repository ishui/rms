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

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// GetUserData 的摘要说明。
	/// </summary>
	public partial class GetUserData : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string Value = Request.QueryString["Value"] + "";

			string UserCode = "";
			string UserName = "";
			string SortID = "";
			string Hint = "";
			string IsExists = "";

			if (Value != "")
			{
				//按工号查
				EntityData entity = DAL.EntityDAO.SystemManageDAO.GetSystemUserBySortID(Value);
				entity.Dispose();

				//按登录名查
				if (!entity.HasRecord()) 
				{
					entity = DAL.EntityDAO.SystemManageDAO.GetSystemUserByUserID(Value);
					entity.Dispose();
				}

				//按姓名查
				if (!entity.HasRecord()) 
				{
					entity = DAL.EntityDAO.SystemManageDAO.GetSystemUserByUserName(Value);
					entity.Dispose();
				}

				//按姓名模糊查
				if (!entity.HasRecord()) 
				{
					UserStrategyBuilder sb = new UserStrategyBuilder();

					sb.AddStrategy( new Strategy( UserStrategyName.UserName, "%" + Value + "%"));

					string sql = sb.BuildMainQueryString();

					sql = sql + sb.GetDefaultOrder();

					QueryAgent qa = new QueryAgent();
					qa.SetTopNumber(1);
					entity = qa.FillEntityData( "SystemUser",sql );
					qa.Dispose();
				}

				if (entity.HasRecord()) 
				{
					UserCode = entity.GetString("UserCode");
					UserName = entity.GetString("UserName");
					SortID = entity.GetString("SortID");
					IsExists = "1";
				}
				else 
				{
					Hint = "用户不存在 ！";
				}
			}

			string sResult = "<Result>"
				+ "<UserCode>" + UserCode + "</UserCode>"
				+ "<UserName>" + UserName + "</UserName>"
				+ "<SortID>" + SortID + "</SortID>"
				+ "<Hint>" + Hint + "</Hint>"
				+ "<IsExists>" + IsExists + "</IsExists>"
				+ "</Result>";

			Response.Write(sResult);
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
