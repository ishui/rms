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

namespace RmsPM.Web.Project
{
	/// <summary>
	/// GetTaskData ��ժҪ˵����
	/// </summary>
	public partial class GetTaskData : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try 
			{
				string Value = Request.QueryString["Value"] + "";
				string ProjectCode = Request.QueryString["ProjectCode"] + "";
				string Type = Request.QueryString["Type"] + "";

				string TaskCode = "";
				string TaskName = "";
				string SortID = "";
				string Hint = "";
				string IsExists = "";

				if (Value != "")
				{
					EntityData entity = null;

					if (Type.ToLower() == "code") 
					{
						//���ؼ��ֲ�
						entity = DAL.EntityDAO.WBSDAO.GetTaskByCode(Value);
						entity.Dispose();
					}
					else 
					{
						//���������Ų�
						entity = DAL.EntityDAO.WBSDAO.GetTaskBySortID(Value, ProjectCode);
						entity.Dispose();
					}

					//				//��������
					//				if (!entity.HasRecord()) 
					//				{
					//					entity = DAL.EntityDAO.SystemManageDAO.GetSystemUserByUserName(Value);
					//					entity.Dispose();
					//				}

					if (entity.HasRecord()) 
					{
						TaskCode = entity.GetString("WBSCode");
						TaskName = entity.GetString("TaskName");
						SortID = entity.GetString("SortID");
						IsExists = "1";
					}
					else 
					{
						Hint = "��������� ��";
					}
				}

				string sResult = "<Result>"
					+ "<TaskCode>" + TaskCode + "</TaskCode>"
					+ "<TaskName>" + TaskName + "</TaskName>"
					+ "<SortID>" + SortID + "</SortID>"
					+ "<Hint>" + Hint + "</Hint>"
					+ "<IsExists>" + IsExists + "</IsExists>"
					+ "</Result>";

				Response.Write(sResult);
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"ȡ��������Ϣ����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ȡ��������Ϣ����" + ex.Message));
			}

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
