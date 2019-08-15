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

namespace RmsPM.Web.Sal
{
	/// <summary>
	/// ImportSalOldDlg 的摘要说明。
	/// </summary>
	public partial class ImportSubjectDlg : PageBase
	{
		protected System.Web.UI.WebControls.Label Label1;
	
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
				//清空科目
				BLL.SubjectRule.DeleteAllSubjectBySet(this.txtSubjectSetCode.Value);

				StreamReader m_sr = new StreamReader(this.txtFile.PostedFile.InputStream, System.Text.Encoding.Default);

				//第1行是标题
				if (m_sr.Peek() >= 0) 
				{
					m_sr.ReadLine();
				}

				EntityData entity = DAL.EntityDAO.SubjectDAO.GetSubjectBySubjectSet(this.txtSubjectSetCode.Value);

				while (m_sr.Peek() >= 0) 
				{
					string s = m_sr.ReadLine();

					ImportSubjectSingle(s, entity);

				}

				DAL.EntityDAO.SubjectDAO.SubmitAllSubject(entity);

				entity.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "导入出错：" + ex.Message));
				return;
			}

			Response.Write(JavaScript.ScriptStart);
			Response.Write(JavaScript.Alert(false,"导入完成"));
			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(JavaScript.WinClose(false));
			Response.Write(JavaScript.ScriptEnd);
		}

		private void ImportSubjectSingle(string val, EntityData entity) 
		{
			try 
			{
				string s = val;
				s = s.Replace("'", "");
				s = s.Replace("\"","");
				string[] m_strSub = s.Split(","[0]);

				if (m_strSub.Length < 4)
					return;

				//科目代码为空时，不导
				if (m_strSub[2].Trim() == "")
					return;

				string SubjectCode = m_strSub[2].Trim();

				DataRow dr;
				
				DataRow[] drs = entity.CurrentTable.Select("SubjectCode='" + SubjectCode + "'");
				if (drs.Length > 0)
				{
					dr = drs[0];
				}
				else
				{
					dr = entity.CurrentTable.NewRow();
					dr["SubjectCode"] = SubjectCode;
					dr["SubjectSetCode"] = this.txtSubjectSetCode.Value;
					entity.CurrentTable.Rows.Add(dr);
				}

                dr["SubjectType"] = BLL.ConvertRule.ToString(BLL.ConvertRule.GetArrayItem(m_strSub, 0)).Trim();
                dr["Layer"] = BLL.ConvertRule.ToString(BLL.ConvertRule.GetArrayItem(m_strSub, 1)).Trim();
                dr["SubjectName"] = BLL.ConvertRule.ToString(BLL.ConvertRule.GetArrayItem(m_strSub, 3)).Trim();
                dr["OtherCode"] = BLL.ConvertRule.ToString(BLL.ConvertRule.GetArrayItem(m_strSub, 4)).Trim();
                dr["Currentcy"] = BLL.ConvertRule.ToString(BLL.ConvertRule.GetArrayItem(m_strSub, 5)).Trim();
                dr["Unit"] = BLL.ConvertRule.ToString(BLL.ConvertRule.GetArrayItem(m_strSub, 6)).Trim();
                dr["AssistantType"] = BLL.ConvertRule.ToString(BLL.ConvertRule.GetArrayItem(m_strSub, 7)).Trim();
                dr["Format"] = BLL.ConvertRule.ToString(BLL.ConvertRule.GetArrayItem(m_strSub, 8)).Trim();
                dr["Balance"] = BLL.ConvertRule.ToString(BLL.ConvertRule.GetArrayItem(m_strSub, 9)).Trim();
				dr["IsDebit"] = 1;
				dr["IsCrebit"] = 1;

			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

	}

}
