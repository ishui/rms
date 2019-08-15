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
using System.IO;
using Rms.ORMap;
using Rms.Web;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// FinanceInterfaceAnalysisUserImport 的摘要说明。
	/// </summary>
	public partial class FinanceInterfaceAnalysisUserImport : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
			}
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

		private void IniPage() 
		{
			try 
			{
				this.txtSubjectSetCode.Value = Request.QueryString["SubjectSetCode"];

				if (this.txtSubjectSetCode.Value == "") 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "未传入帐套代码"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
				}

				this.lblSubjectSetName.Text = BLL.SubjectRule.GetSubjectSetName(this.txtSubjectSetCode.Value);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		protected void btnOK_ServerClick(object sender, System.EventArgs e)
		{
			if (this.txtFile.PostedFile.FileName == "") 
			{
				Response.Write(Rms.Web.JavaScript.Alert(true, "请选择文件"));
				return;
			}

			try
			{
				string SubjectSetCode = this.txtSubjectSetCode.Value;
				if (SubjectSetCode == "")
					throw new Exception("未传入帐套编号");

				StreamReader m_sr = new StreamReader(this.txtFile.PostedFile.InputStream, System.Text.Encoding.Default);

				//第1行是标题
				if (m_sr.Peek() >= 0) 
				{
					m_sr.ReadLine();
				}

				EntityData entityUser = DAL.EntityDAO.SystemManageDAO.GetAllSystemUser();
				EntityData entity = DAL.EntityDAO.SystemManageDAO.GetSystemUserSubjectSetBySubjectSet(SubjectSetCode);

				while (m_sr.Peek() >= 0) 
				{
					string s = m_sr.ReadLine();

					ImportSingle(s, SubjectSetCode, entity, entityUser);

				}

				DAL.EntityDAO.SystemManageDAO.SubmitAllSystemUserSubjectSet(entity);
				entity.Dispose();
				entityUser.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				throw ex;
			}

			Response.Write(JavaScript.ScriptStart);
			Response.Write(JavaScript.Alert(false,"导入完成"));
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(JavaScript.WinClose(false));
			Response.Write(JavaScript.ScriptEnd);
			Response.End();
		}

		private void ImportSingle(string s, string SubjectSetCode, EntityData entity, EntityData entityUser) 
		{
			string[] arr = BLL.ImportRule.SplitCsvLine(s);

			if (arr.Length < 1)
				return;

			//为空时，不导
			if (arr[0].Trim() == "")
				return;

			string UserName = arr[0].Trim();

			//按人员姓名查
			DataRow[] drsUser = entityUser.CurrentTable.Select("UserName = '" + UserName + "'");
			foreach(DataRow drUser in drsUser) 
			{
				string UserCode = BLL.ConvertRule.ToString(drUser["UserCode"]);

				DataRow dr;
				DataRow[] drs = entity.CurrentTable.Select("UserCode = '" + UserCode + "'");
				if (drs.Length > 0) 
				{
					dr = drs[0];
				}
				else 
				{
					dr = entity.CurrentTable.NewRow();
					dr["SystemUserSubjectSetCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("SystemUserSubjectSetCode");
					dr["SubjectSetCode"] = SubjectSetCode;
					dr["UserCode"] = UserCode;
					entity.CurrentTable.Rows.Add(dr);
				}

				dr["U8Code"] = arr[1].Trim();
			}
		}

	}

}
