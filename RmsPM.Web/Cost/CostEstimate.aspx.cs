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

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// CostEstimate 的摘要说明。
	/// </summary>
	public partial class CostEstimate : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();

				ContralRight();
			}
		}

		private void ContralRight()
		{
			if ( ! user.HasRight("040203"))
				this.btnCheck.Visible = false;
		}

		private void IniPage()
		{


			string projectCode = Request["ProjectCode"] + "";
			try
			{
				this.lblProjectName.Text = BLL.ProjectRule.GetProjectName( projectCode);

				EntityData entity = RmsPM.DAL.EntityDAO.CBSDAO.GetCostEstimateCheckByCode(projectCode);

//				this.btnCheckView.Visible = false;
				this.btnCheck.Visible = false;

				if ( entity.HasRecord())
				{
					this.lblCheckPersonName.Text = BLL.SystemRule.GetUserName( entity.GetString("CheckPerson"));
					this.lblCheckDate.Text = entity.GetDateTimeOnlyDate("CheckDate");
				}
				else
					this.btnCheck.Visible = true;

				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化页面错误。");
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

		protected void btnCheck_ServerClick(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"] + "" ;

			try
			{
				EntityData entity = DAL.EntityDAO.CBSDAO.GetCostEstimateCheckByCode(projectCode);
				bool isNew = !entity.HasRecord();
				DataRow dr = null;

				if ( isNew )
				{
					dr = entity.GetNewRecord();
					dr["ProjectCode"] = projectCode;
					dr["CheckPerson"] = base.user.UserCode;
					dr["CheckDate"] = DateTime.Now.ToString("yyyy-MM-dd");
				}
				else
					dr = entity.CurrentRow;

				if ( isNew )
				{
					entity.AddNewRecord(dr);
					RmsPM.DAL.EntityDAO.CBSDAO.InsertCostEstimateCheck(entity);
				}
				else
				{
					RmsPM.DAL.EntityDAO.CBSDAO.UpdateCostEstimateCheck(entity);
				}
				entity.Dispose();

				Response.Write(Rms.Web.JavaScript.Reload(true));
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


	}
}
