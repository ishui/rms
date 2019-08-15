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
using RmsPM.DAL;
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSDocument ��ժҪ˵����
	/// </summary>
    public partial class WBSDocument : System.Web.UI.Page
	{
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if ( !IsPostBack)
			{
				LoadData();
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
			this.dgDocumentList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgDocumentList_PageIndexChanged);
			this.SaveToolsButton.Click += new System.EventHandler(this.SaveToolsButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// ��ʼ���ĵ��б�
		/// </summary>
		private void LoadData()
		{
			try
			{
				EntityData entityDocument = DAL.EntityDAO.DocumentDAO.GetAllDocument();
				if ( entityDocument.HasRecord())
				{
					this.dgDocumentList.DataSource = entityDocument.CurrentTable;
					this.dgDocumentList.DataBind();
				}
				else
				{
					this.SaveToolsButton.Visible = false;
					this.CancelToolsButton.Visible = false;
				}
				entityDocument.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}

		}

		/// <summary>
		/// ����ѡ�е��ĵ�Ϊ����������ĵ�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SaveToolsButton_Click(object sender, System.EventArgs e)
		{
		
		}

		/// <summary>
		/// ��ҳ�¼�����
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void dgDocumentList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgDocumentList.CurrentPageIndex = e.NewPageIndex;
			LoadData();
		}
	}
}
