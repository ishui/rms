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
	/// GetSystemGroupData 的摘要说明。
	/// </summary>
	public partial class GetSystemGroupData : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string Value = Request.QueryString["Value"] + "";
			string ClassCode = Request.QueryString["ClassCode"] + "";
			string Type = Request.QueryString["Type"] + "";
			string SelectAllLeaf = Request.QueryString["SelectAllLeaf"] + "";

			string GroupCode = "";
			string GroupName = "";
			string GroupFullName = "";
			string SortID = "";
			string FullID = "";
			string Hint = "";
			string IsExists = "";

			if (Value != "")
			{
				EntityData entity = null;

				if (Type.ToLower() == "code") 
				{
					if(ClassCode == "1603")
					{
						try
						{
//							OADocumentClassStrategyBuilder sb = new OADocumentClassStrategyBuilder();
//
//							sb.AddStrategy( new Strategy( OADocumentClassStrategyName.OAFileTypeCode, ClassCode));
//							string sql = sb.BuildQueryViewString() + sb.GetDefaultOrder();
//
//							QueryAgent qa = new QueryAgent();
//							EntityData entity = qa.FillEntityData( "Document",sql );
//							qa.Dispose();
							


							entity = DAL.EntityDAO.OADAO.GetOAFileTypeByCode(Value);
							entity.Dispose();
						}
						catch ( Exception ex )
						{
							throw ex;
						}

					}
					else
					{
						//按关键字查
						entity = DAL.EntityDAO.SystemManageDAO.GetV_SystemGroupByCode(Value);
						entity.Dispose();
					}
				}
				else 
				{
					//按编号查
					entity = DAL.EntityDAO.SystemManageDAO.GetSystemGroupBySortID(Value, ClassCode);
					entity.Dispose();
				}

				if (entity.HasRecord()) 
				{
					if(ClassCode == "1603")
					{
						GroupCode = entity.GetString("OAFileTypeCode");
						GroupName = entity.GetString("TypeName");
						GroupFullName = entity.GetString("TypeName");
						SortID = entity.GetString("OAFileTypeCode");
						FullID = "";
						IsExists = "1";
					}
					else
					{
						GroupCode = entity.GetString("GroupCode");
						GroupName = entity.GetString("GroupName");
						GroupFullName = entity.GetString("FullName");
						SortID = entity.GetString("SortID");
						FullID = entity.GetString("FullID");
						IsExists = "1";

						if (SelectAllLeaf != "1") 
						{
							if (!BLL.SystemGroupRule.IsSystemGroupLeafNode(GroupCode))
							{
								Hint = "不是末级类别 ！";
							}
						}
					}
				}
				else 
				{
					Hint = "类别不存在 ！";
				}
			}

			string sResult = "<Result>"
				+ "<GroupCode>" + GroupCode + "</GroupCode>"
				+ "<GroupName>" + GroupName + "</GroupName>"
				+ "<GroupFullName>" + GroupFullName + "</GroupFullName>"
				+ "<SortID>" + SortID + "</SortID>"
				+ "<FullID>" + FullID + "</FullID>"
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
