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
	/// GetUnitData ��ժҪ˵����
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
				//����Ų�
				EntityData entity = DAL.EntityDAO.OBSDAO.GetUnitBySortID(Value);
				entity.Dispose();

				//�����Ʋ�
				if (!entity.HasRecord()) 
				{
					entity = DAL.EntityDAO.OBSDAO.GetUnitByUnitName(Value);
					entity.Dispose();
				}

				//������ģ����
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
					Hint = "���Ų����� ��";
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
