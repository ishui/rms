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

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// GetSubjectData ��ժҪ˵����
	/// </summary>
	public partial class GetSubjectData : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string SubjectCode = Request.QueryString["SubjectCode"] + "";
			string SubjectSetCode = Request.QueryString["SubjectSetCode"] + "";

			string SubjectName = "";
			string SubjectFullName = "";

			string Hint = BLL.SubjectRule.CheckSubject(SubjectCode, SubjectSetCode, "", ref SubjectName, ref SubjectFullName);

			string sResult = "<Result>"
				+ "<SubjectCode>" + SubjectCode + "</SubjectCode>"
				+ "<SubjectName>" + SubjectName + "</SubjectName>"
				+ "<SubjectFullName>" + SubjectFullName + "</SubjectFullName>"
				+ "<Hint>" + Hint + "</Hint>"
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
