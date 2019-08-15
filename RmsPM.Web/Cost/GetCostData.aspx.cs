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

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// GetCostData 的摘要说明。
	/// </summary>
	public partial class GetCostData : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string Value = Request.QueryString["Value"] + "";
			string ProjectCode = Request.QueryString["ProjectCode"] + "";
			string Type = Request.QueryString["Type"] + "";
			string SelectAllLeaf = Request.QueryString["SelectAllLeaf"] + "";

			string CostCode = "";
			string CostName = "";
			string SortID = "";
			string Hint = "";
			string IsExists = "";
			string FullName = "";

			if (Value != "")
			{
				EntityData entity = null;

				if (Type.ToLower() == "code") 
				{
					//按关键字查
					entity = DAL.EntityDAO.CBSDAO.GetCBSByCode(Value);
					entity.Dispose();
				}
				else 
				{
					//按编号查
					entity = DAL.EntityDAO.CBSDAO.GetCBSBySortID(Value, ProjectCode);
					entity.Dispose();
				}

				if (entity.HasRecord()) 
				{
					CostCode = entity.GetString("CostCode");
					CostName = entity.GetString("CostName");
					SortID = entity.GetString("SortID");
					FullName = BLL.CBSRule.GetCostFullName(CostCode);
					IsExists = "1";

					if (SelectAllLeaf != "1") 
					{
						//必须是末级费用项
						if (!BLL.CBSRule.CheckCBSLeafNode(CostCode))
						{
							Hint = "不是末级费用项 ！";
						}
					}
				}
				else 
				{
					Hint = "费用项不存在 ！";
				}
			}

			string sResult = "<Result>"
				+ "<CostCode>" + CostCode + "</CostCode>"
				+ "<CostName>" + CostName + "</CostName>"
				+ "<SortID>" + SortID + "</SortID>"
				+ "<Hint>" + Hint + "</Hint>"
				+ "<IsExists>" + IsExists + "</IsExists>"
				+ "<FullName>" + FullName + "</FullName>"
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
