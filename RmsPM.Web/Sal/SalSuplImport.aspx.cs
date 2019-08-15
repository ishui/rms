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
	/// SalSuplImport 的摘要说明。
	/// </summary>
	public partial class SalSuplImport : PageBase
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
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				BLL.PageFacade.LoadProjectSelect(this.sltProject, this.txtProjectCode.Value);

				//			if (this.txtProjectCode.Value == "") 
				//			{
				//				Response.Write(Rms.Web.JavaScript.Alert(true, "无项目代码，请先选择项目"));
				//				Response.End();
				//			}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		protected void btnOK_ServerClick(object sender, System.EventArgs e)
		{
			if (this.txtFile.PostedFile.FileName == "") 
			{
				Response.Write(Rms.Web.JavaScript.Alert(true, "请选择文件"));
				return;
			}

			string ProjectCode = this.sltProject.Value.Trim();
//			if (ProjectCode == "") 
//			{
//				Response.Write(Rms.Web.JavaScript.Alert(true, "请选择对应项目"));
//				return;
//			}

			try
			{
				//删除该项目的供应商
				DAL.EntityDAO.SalDAO.DeleteSalSuplByProjectCode(ProjectCode);

				StreamReader m_sr = new StreamReader(this.txtFile.PostedFile.InputStream, System.Text.Encoding.Default);

				SingleEntityDAO dao = new SingleEntityDAO("SalSupl");
				EntityData entity = new EntityData("SalSupl");

				//第1行是标题
				if (m_sr.Peek() >= 0) 
				{
					m_sr.ReadLine();
				}

				while (m_sr.Peek() >= 0) 
				{
					string s = m_sr.ReadLine();
					string[] m_strSub = s.Replace("\"","").Split(',');

					if (m_strSub.Length < 3)
						continue;

					if (m_strSub[0].Trim() == "") 
						continue;

					DataRow dr = entity.CurrentTable.NewRow();

					dr["SystemID"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("SalSuplSystemID");
					dr["ProjectCode"] = ProjectCode;
					dr["SuplCode"] = m_strSub[0].Trim();
					dr["SuplName"] = m_strSub[1].Trim();

					entity.CurrentTable.Rows.Add(dr);
					dao.InsertEntity(entity);
				}
				entity.Dispose();
				dao.Dispose();
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
	}
}
