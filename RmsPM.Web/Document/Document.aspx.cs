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
using Rms.Web;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// Document ��ժҪ˵����
	/// </summary>
	public partial class Document : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnConfirmDocument;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if (!Page.IsPostBack) 
			{
				this.txtAct.Value = Request.QueryString["action"];
                this.txtGroupName.Value = Request.QueryString["GroupName"];
                this.txtGroupCode.Value = Request.QueryString["GroupCode"];
                this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

                //���������ȡ������
                if ((this.txtGroupName.Value != "") && (this.txtGroupCode.Value == ""))
                {
                    string ProjectName = BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value);

                    EntityData entityGroup = DAL.EntityDAO.SystemManageDAO.GetSystemGroupByClassCode("1001");
                    DataRow[] drs = null;

                    if (ProjectName != "") //����Ŀ��������ƹ���
                    {
                        //����Ŀ����ȡϵͳ������
                        string ProjectGroupCode = "";
                        DataRow[] drsProject = entityGroup.CurrentTable.Select("GroupName like '" + ProjectName + "%'");
                        if (drsProject.Length > 0)
                        {
                            ProjectGroupCode = drsProject[0]["GroupCode"].ToString();
                        }

                        if (ProjectGroupCode != "")  //����Ŀ��������ƹ���
                        {
                            drs = entityGroup.CurrentTable.Select(string.Format("ParentCode='{0}' and GroupName='{1}'", ProjectGroupCode, this.txtGroupName.Value));
                        }
                    }
                    else  //��������ƹ���
                    {
                        drs = entityGroup.CurrentTable.Select(string.Format("GroupName='{0}'", this.txtGroupName.Value));
                    }

                    if ((drs != null) && (drs.Length > 0))
                    {
                        string GroupCode = "";
                        foreach (DataRow dr in drs)
                        {
                            if (GroupCode != "") GroupCode += ",";
                            GroupCode += dr["GroupCode"].ToString();
                        }
                        this.txtGroupCode.Value = GroupCode;
                    }
                    entityGroup.Dispose();
                }
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

		}
		#endregion

	}
}
