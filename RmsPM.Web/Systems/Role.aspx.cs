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
	/// Rule 的摘要说明。
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
                //按照sortid 排序 2006-12-25日添加
                dv.Sort="sortid asc";

                //过滤
                dv.RowFilter = ViewState["searchpara"].ToString();
                this.dgList.DataSource = dv;
				this.dgList.DataBind();
				entity.Dispose();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示出错"));
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

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            string roleName = RoleName.Text.Trim().Replace("'", "''");
            string sortID = SortID.Text.Trim().Replace("'", "''");

            //模糊搜索
            ViewState["searchpara"] = String.Format("(RoleName like '%{0}%' or '{0}'='' ) and ( SortID like '%{1}%' or '{1}'='')", roleName, sortID);

            IniPage();
     

        }
}
}
