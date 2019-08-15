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
	/// GetCostData ��ժҪ˵����
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
					//���ؼ��ֲ�
					entity = DAL.EntityDAO.CBSDAO.GetCBSByCode(Value);
					entity.Dispose();
				}
				else 
				{
					//����Ų�
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
						//������ĩ��������
						if (!BLL.CBSRule.CheckCBSLeafNode(CostCode))
						{
							Hint = "����ĩ�������� ��";
						}
					}
				}
				else 
				{
					Hint = "��������� ��";
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
