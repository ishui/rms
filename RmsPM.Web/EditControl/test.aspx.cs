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
using RmsPM.BLL;
using RmsPM.Web.Components;
using Rms.Web;
namespace RmsPM.Web.EditControl
{
	/// <summary>
	/// test ��ժҪ˵����
	/// </summary>
	public partial class test : PageBase
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			//WorkFlowInOut gy = new WorkFlowInOut();
			//gy.test(this.GetOldDataPath,this.GetServerPath);
			//Response.Write(gy.test(this.GetOldDataPath,this.GetServerPath));
			//Response.Write(gy.SetNewSysCodeByCode("WorkFlowRole",gy.test(this.GetOldDataPath,this.GetServerPath)));
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
		public string GetServerPath
		{
			get
			{
				return Server.MapPath("../WorkFlowDB/NewData/");
			}		
		}
		public string GetOldDataPath
		{
			get
			{
				return Server.MapPath("../WorkFlowDB/OldData/");
			}		
		}
		public string XmlName
		{
			get
			{
				return "WorkFlowDB.xml";
			}
		}
		private void WriteAllXml()
		{
			
		}

		private void BT_OutAll_Click(object sender, System.EventArgs e)
		{
			//Response.Write(GetServerPath);
			
		}

		private void BT_OutWorkFlow_Click(object sender, System.EventArgs e)
		{
			
		}

		protected void BT_OutAll_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				WorkFlowInOut wio = new WorkFlowInOut();
				wio.BackDataToXml(this.GetServerPath,"WorkFlowDB.xml");
				//WorkFlowInOut.WriteAllWorkFlow(GetServerPath);
				Response.Write(Rms.Web.JavaScript.Alert(true,"�ѳɹ����ݵ�"+GetServerPath+"���ȷ��,�鿴�����ļ�"));
				Response.Write(Rms.Web.JavaScript.WriteJS("window.open(\"../WorkFlowDB/NewData/WorkFlowDB.xml\",\"_blank\")"));				
			}
			catch(Exception ex)
			{
				Response.Write(Rms.Web.JavaScript.Alert(true,ex.Message));
			}
		}

		protected void BT_OutWorkFlow_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				//WorkFlowInOut wio = new WorkFlowInOut();
				//wio.OverWriteAllWorkFlow(this.GetOldDataPath,this.GetServerPath);
				WorkFlowInOut wio = new WorkFlowInOut();
				wio.SetNewDataSet(this.GetServerPath,this.GetOldDataPath,"WorkFlowDB.xml");
				Response.Write(Rms.Web.JavaScript.Alert(true,"�ѳɹ���������"));
			}
			catch(Exception ex)
			{
				Response.Write(Rms.Web.JavaScript.Alert(true,ex.Message));
			}
		}
		protected void BT_Up_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				HttpPostedFile UpFile = this.Request.Files[0];
				WorkFlowInOut.UpXmlDB(this.GetServerPath,UpFile,this.XmlName);
				Response.Write(Rms.Web.JavaScript.Alert(true,"�ϴ��ɹ�"));
			}
			catch(Exception ex)
			{
				Response.Write(Rms.Web.JavaScript.Alert(true,ex.Message));
			}
		}
	}
}
