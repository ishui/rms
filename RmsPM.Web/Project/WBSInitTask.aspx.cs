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

namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSInitTask ��ժҪ˵����
	/// </summary>
    public partial class WBSInitTask : System.Web.UI.Page
	{
		protected string strState = "";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			InitPage();
			LoadDate();
		}

		private void InitPage()
		{
			
		}

		private DataView GetRoot()
		{
			string strProjectCode = (string)Session["ProjectCode"];
			EntityData entity = DAL.EntityDAO.WBSDAO.GetAllTask();
			DataView dv = entity.Tables[0].DefaultView;
			dv.RowFilter = " ParentCode = '' and Deep=0 "; // ���ڵ�Ϊ�������Ϊ0��
			return dv;
		}

		private void LoadDate()
		{
			// ����Ƿ��Ѿ���ʼ����
			DataView dv = this.GetRoot();
			if(dv.Table.Rows.Count>0)
			{
				DataRow dr = dv.Table.Rows[0];
				this.tdTaskName.Controls.Clear();
				this.tdTaskName.InnerText = dr["TaskName"].ToString();
				this.tdTaskCode.Controls.Clear();
				this.tdTaskCode.InnerText = dr["TaskCode"].ToString();
				this.tdTaskStatus.Controls.Clear();
				this.tdTaskStatus.InnerText = BLL.ComSource.GetTaskStatusName(dr["Status"].ToString());
				this.tdImportantLevel.Controls.Clear();
				this.tdImportantLevel.InnerText = dr["TaskCode"].ToString();
				this.tdPlanStartDate.Controls.Clear();
				this.tdPlanStartDate.InnerText = dr["TaskCode"].ToString();
				this.tdPlanFinishDate.Controls.Clear();
				this.tdPlanFinishDate.InnerText = dr["TaskCode"].ToString();
				this.tdTaskDesc.Controls.Clear();
				this.tdTaskDesc.InnerText = dr["TaskCode"].ToString();

				this.btSubmit.Visible = false;
				this.btCancel.Visible = false;
				this.strState = "List";
			}
			else
			{
				this.trTemplate.Visible = false;
				this.strState = "Insert";
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
			this.btSubmit.ServerClick += new System.EventHandler(this.SaveToolsButton_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void SaveToolsButton_ServerClick(object sender, System.EventArgs e)
		{

//			EntityData entity = new EntityData("Task");
//			DataRow drTask = entity.GetNewRecord();
//			drTask["TaskCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("TaskCode");
//			drTask["TaskName"] = this.txtTaskName.Text;


		}
	}
}
