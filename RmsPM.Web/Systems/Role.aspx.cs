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

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// Rule ��ժҪ˵����
	/// </summary>
	public partial class Role : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
			}
		}

		private void IniPage()
		{
            if (ViewState["searchpara"] == null) { ViewState["searchpara"] = String.Empty; }
			string projectCode = Request["projectCode"] + "";
			try
			{
				EntityData entity  = DAL.EntityDAO.SystemManageDAO.GetAllRole();
                DataView dv =new DataView(entity.CurrentTable);
                //����sortid ���� 2006-12-25�����
                dv.Sort="sortid asc";

                //����
                dv.RowFilter = ViewState["searchpara"].ToString();
                this.dgList.DataSource = dv;
				this.dgList.DataBind();
				entity.Dispose();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ����"));
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

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            string roleName = RoleName.Text.Trim().Replace("'", "''");
            string sortID = SortID.Text.Trim().Replace("'", "''");

            //ģ������
            ViewState["searchpara"] = String.Format("(RoleName like '%{0}%' or '{0}'='' ) and ( SortID like '%{1}%' or '{1}'='')", roleName, sortID);

            IniPage();
     

        }
}
}
