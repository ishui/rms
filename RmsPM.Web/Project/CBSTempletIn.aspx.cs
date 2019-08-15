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
	/// CBSTempletIn ��ժҪ˵����
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


		protected void btnOK_ServerClick(object sender, System.EventArgs e)
		{
			string fileName = "";

			try
			{
				/*
				if (this.txtTempletName.Text.Trim().Length<0)
				{
					Response.Write(Rms.Web.JavaScript.Alert(true,"ģ�����Ʋ���Ϊ�� ��"));
					return;
				}
				*/

				string projectCode = Request["ProjectCode"] + "";

				EntityData cbs = DAL.EntityDAO.CBSDAO.GetCBSByProject(projectCode);
				string vPath = Server.MapPath(ConfigurationSettings.AppSettings["VirtualDirectory"]);
				string path = vPath +  @"\temp\";
				fileName = "�������" + DateTime.Now.ToString("yyyyMMddhhmmss")  + ".csv";

				StreamWriter w = new StreamWriter( path + fileName, false,	System.Text.Encoding.Default);
				w.WriteLine("���������,����������,��Ŀ����,���÷ֽ�˵��,��ע"  );
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
				Response.Write(JavaScript.Alert(true, "��������" + ex.Message));
				return;
			}

			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write("window.open('../Temp/"+ fileName  + @"');");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}
	}
}
