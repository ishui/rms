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

namespace RmsPM.Web.UserControls
{
	/// <summary>
	/// GetUserUnit ��ժҪ˵����
	/// </summary>
	public partial class GetUserUnit : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			try
			{
				string userCode = Request["Value"]+"";
				string ReturnValue = "<result><values>";
				EntityData entity = DAL.EntityDAO.SystemManageDAO.GetUnitByUserCode(userCode);
				if(entity.HasRecord())
				{
					DataTable dt = entity.CurrentTable;
					for(int i=0;i<dt.Rows.Count;i++)
					{
						ReturnValue+=dt.Rows[i]["UnitCode"].ToString()+":"+dt.Rows[i]["UnitName"].ToString()+";";
					}
				}
				ReturnValue+="</values></result>";
				Response.Write(ReturnValue);
				Response.End();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
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
