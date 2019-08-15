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
	/// GetUserData ��ժҪ˵����
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
				//�����Ų�
				EntityData entity = DAL.EntityDAO.SystemManageDAO.GetSystemUserBySortID(Value);
				entity.Dispose();

				//����¼����
				if (!entity.HasRecord()) 
				{
					entity = DAL.EntityDAO.SystemManageDAO.GetSystemUserByUserID(Value);
					entity.Dispose();
				}

				//��������
				if (!entity.HasRecord()) 
				{
					entity = DAL.EntityDAO.SystemManageDAO.GetSystemUserByUserName(Value);
					entity.Dispose();
				}

				//������ģ����
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
					Hint = "�û������� ��";
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
