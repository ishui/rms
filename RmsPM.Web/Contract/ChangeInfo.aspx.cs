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
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;

namespace RmsPM.Web.Contract
{
	/// <summary>
	/// ChangeInfo 的摘要说明。
	/// </summary>
	public partial class ChangeInfo : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable tableButton;
		protected System.Web.UI.HtmlControls.HtmlTableRow trViewCheckOpinion;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!IsPostBack)
			{
				IniPage();
				LoadData();
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
		private void LoadData()
		{
			try
			{
				string contractCode=Request["ContractCode"] + "" ;

				EntityData entity=RmsPM.DAL.EntityDAO.ContractDAO.GetContractByCode(contractCode); 
				if(entity.HasRecord())
				{

					this.lblContractID.Text=entity.GetString("ContractID");
					this.lblContractName.Text=entity.GetString("ContractName");
					this.lblChangePersonName.Text=BLL.SystemRule.GetUserName(entity.GetString("ChangePerson"));
					this.lblChangeOpinionDate.Text=entity.GetDateTimeOnlyDate("ChangeOpinionDate");
					this.lblChangeReason.Text = BLL.StringRule.FormartInput(entity.GetString("ChangeReason"));
					this.lblChangeRemark.Text = BLL.StringRule.FormartInput(entity.GetString("ChangeRemark"));
				}
				entity.Dispose();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载数据出错！");
			}
		}




	}
}
