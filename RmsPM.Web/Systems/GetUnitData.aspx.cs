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
	/// GetUnitData 的摘要说明。
	/// </summary>
	public partial class GetUnitData : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string Value = Request.QueryString["Value"] + "";

			string UnitCode = "";
			string UnitName = "";
			string SortID = "";
			string Hint = "";
			string IsExists = "";

			if (Value != "")
			{
				//按编号查
				EntityData entity = DAL.EntityDAO.OBSDAO.GetUnitBySortID(Value);
				entity.Dispose();

				//按名称查
				if (!entity.HasRecord()) 
				{
					entity = DAL.EntityDAO.OBSDAO.GetUnitByUnitName(Value);
					entity.Dispose();
				}

				//按名称模糊查
				if (!entity.HasRecord()) 
				{
					UnitStrategyBuilder sb = new UnitStrategyBuilder();

					sb.AddStrategy( new Strategy( UnitStrategyName.UnitName, "%" + Value + "%"));

					string sql = sb.BuildMainQueryString();

					sql = sql + sb.GetDefaultOrder();

					QueryAgent qa = new QueryAgent();
					qa.SetTopNumber(1);
					entity = qa.FillEntityData( "Unit",sql );
					qa.Dispose();
				}

				if (entity.HasRecord()) 
				{
					UnitCode = entity.GetString("UnitCode");
					UnitName = entity.GetString("UnitName");
					SortID = entity.GetString("SortID");
					IsExists = "1";
				}
				else 
				{
					Hint = "部门不存在 ！";
				}
			}

			string sResult = "<Result>"
				+ "<UnitCode>" + UnitCode + "</UnitCode>"
				+ "<UnitName>" + UnitName + "</UnitName>"
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
