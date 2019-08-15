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
	/// GetSystemGroupData ��ժҪ˵����
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
						//���ؼ��ֲ�
						entity = DAL.EntityDAO.SystemManageDAO.GetV_SystemGroupByCode(Value);
						entity.Dispose();
					}
				}
				else 
				{
					//����Ų�
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
								Hint = "����ĩ����� ��";
							}
						}
					}
				}
				else 
				{
					Hint = "��𲻴��� ��";
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

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
