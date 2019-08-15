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
	/// ImportUFProjectDlg 的摘要说明。
	/// </summary>
	public partial class ImportUFProjectDlg : PageBase
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
				//清空核算项目
				BLL.PaymentRule.DeleteAllUFProject();

				StreamReader m_sr = new StreamReader(this.txtFile.PostedFile.InputStream, System.Text.Encoding.Default);

				//第1行是标题
				if (m_sr.Peek() >= 0) 
				{
					m_sr.ReadLine();
				}

				SingleEntityDAO dao = new SingleEntityDAO("UFProject");
				EntityData entity = new EntityData("UFProject");

				while (m_sr.Peek() >= 0) 
				{
					string s = m_sr.ReadLine();

					ImportUFProjectSingle(s, dao, entity);

				}
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

		private void ImportUFProjectSingle(string s, SingleEntityDAO dao, EntityData entity) 
		{
			string[] m_strSub = s.Replace("\"","").Split(',');

			if (m_strSub.Length < 2)
				return;

			//为空时，不导
			if ((m_strSub[0].Trim() == "") || (m_strSub[1].Trim() == ""))
				return;

			string code = m_strSub[0].Trim();

			DataRow dr = entity.CurrentTable.NewRow();

			dr["UFProjectCode"] = code;
			dr["UFProjectName"] = m_strSub[1].Trim();

			entity.CurrentTable.Rows.Add(dr);
			dao.InsertEntity(entity);
		}

	}

}
