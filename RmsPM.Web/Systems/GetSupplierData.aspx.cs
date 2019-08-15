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
	/// GetSupplierData 的摘要说明。
	/// </summary>
	public partial class GetSupplierData : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			string Value = Request.QueryString["Value"] + "";
			string Type = Request.QueryString["Type"] + "";

			string SupplierCode = "";
			string SupplierName = "";
			string SupplierFullName = "";
			string SortID = "";
			string Hint = "";
			string IsExists = "";

			if (Value != "")
			{
				EntityData entity = null;

				try
				{
					SupplierStrategyBuilder sb = new SupplierStrategyBuilder();
	
					if ( SupplierName != "")
						sb.AddStrategy( new Strategy( SupplierStrategyName.SupplierName, "%"+SupplierName+"%" ));
					
					string sql = sb.BuildMainQueryString();

					QueryAgent qa = new QueryAgent();
					entity = qa.FillEntityData("Supplier",sql);
					qa.Dispose();

				}
				catch ( Exception ex )
				{
					throw ex;
				}

				switch ( entity.Tables["Supplier"].Rows.Count )
				{
					case 0:
						Hint = "该供应商不存在 ！";
						break;
					case 1:
						SupplierCode = entity.GetString("SupplierCode");
						SupplierFullName = entity.GetString("SupplierName");
						IsExists = "1";
						break;
					default:
						Hint = "请继续输入以确定唯一的供应商 ！";
						break;

				}

			
			}

			string sResult = "<Result>"
				+ "<SupplierCode>" + SupplierCode + "</SupplierCode>"
				+ "<SupplierName>" + SupplierName + "</SupplierName>"
				+ "<SupplierFullName>" + SupplierFullName + "</SupplierFullName>"
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
