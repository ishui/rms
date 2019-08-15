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
using System.Configuration;

using Rms.Web;
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// CBSTempletIn 的摘要说明。
	/// </summary>
	public partial class CBSTempletIn : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
			}
		}

		private void IniPage()
		{
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


		protected void btnOK_ServerClick(object sender, System.EventArgs e)
		{
			string fileName = "";

			try
			{
				/*
				if (this.txtTempletName.Text.Trim().Length<0)
				{
					Response.Write(Rms.Web.JavaScript.Alert(true,"模板名称不能为空 ！"));
					return;
				}
				*/

				string projectCode = Request["ProjectCode"] + "";

				EntityData cbs = DAL.EntityDAO.CBSDAO.GetCBSByProject(projectCode);
				string vPath = Server.MapPath(ConfigurationSettings.AppSettings["VirtualDirectory"]);
				string path = vPath +  @"\temp\";
				fileName = "费用项导出" + DateTime.Now.ToString("yyyyMMddhhmmss")  + ".csv";

				StreamWriter w = new StreamWriter( path + fileName, false,	System.Text.Encoding.Default);
				w.WriteLine("费用项编码,费用项名称,科目编码,费用分解说明,备注"  );
				int iCount = cbs.CurrentTable.Rows.Count;
				for ( int i=0;i<iCount;i++)
				{
					cbs.SetCurrentRow(i);
					string sTemp = "";
					sTemp += "'" + cbs.GetString("SortID") + "," ;
					sTemp += "'" + cbs.GetString("CostName") + "," ;
					sTemp += "'" + cbs.GetString("SubjectCode") + "," ;
					sTemp += "'" + cbs.GetString("CostAllocationDescription") + "," ;
					sTemp += "'" + cbs.GetString("Description") ;
					w.WriteLine(sTemp);
				}
				w.Flush(); 
				w.Close();
				cbs.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog (this.ToString(),ex,"");
				Response.Write(JavaScript.Alert(true, "导出出错：" + ex.Message));
				return;
			}

			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.open('../Temp/"+ fileName  + @"');");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}
	}
}
