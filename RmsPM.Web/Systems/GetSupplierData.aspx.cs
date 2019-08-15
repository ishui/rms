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
	/// GetSupplierData ��ժҪ˵����
	/// </summary>
	public partial class GetSupplierData : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
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
						Hint = "�ù�Ӧ�̲����� ��";
						break;
					case 1:
						SupplierCode = entity.GetString("SupplierCode");
						SupplierFullName = entity.GetString("SupplierName");
						IsExists = "1";
						break;
					default:
						Hint = "�����������ȷ��Ψһ�Ĺ�Ӧ�� ��";
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
