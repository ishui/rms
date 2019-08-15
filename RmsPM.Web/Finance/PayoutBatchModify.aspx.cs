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
using RmsPM.DAL.QueryStrategy;
using RmsPM.DAL.EntityDAO;
using Rms.ORMap;
using Rms.Web;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// PayoutBatchModify 的摘要说明。
	/// </summary>
	public partial class PayoutBatchModify : System.Web.UI.Page
	{
		protected string ProjectCode = "";
		protected string SelectCode = "";

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			try
			{
				if(!this.IsPostBack)
				{
					this.InitPage();
					this.LoadData();
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "：" + ex.Message));
			}
		}

		private void InitPage()
		{
			ProjectCode = Request["ProjectCode"].ToString();
			SelectCode  = Request["SelectCode"].ToString();
		}

		private void LoadData()
		{	
			PayoutStrategyBuilder sb = new PayoutStrategyBuilder("V_Payout");
			sb.AddStrategy( new Strategy( PayoutStrategyName.ProjectCode,ProjectCode));


			string sql = sb.BuildMainQueryString();

			QueryAgent qa = new QueryAgent();
			EntityData entity = qa.FillEntityData( "V_Payout",sql );
			qa.Dispose();

			this.dgDetailData.DataSource = entity;
			this.dgDetailData.DataBind();


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

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{				
				EntityData entity;

				for(int i=0;i<this.dgDetailData.Items.Count;i++)
				{
					string txtSupplierCode = ((TextBox)this.dgDetailData.Items[i].FindControl("txtSupplierCode")).Text.Trim();
					string txtName = ((TextBox)this.dgDetailData.Items[i].FindControl("txtName")).Text.Trim();
				
					string Code = this.dgDetailData.DataKeys[i].ToString();
					entity = DAL.EntityDAO.PaymentDAO.GetPayoutByCode(Code);
					DataRow dr = entity.CurrentRow;

					dr["Payer"] = txtName;
					dr["SupplyCode"] = txtSupplierCode;

					DAL.EntityDAO.PaymentDAO.UpdatePayout(entity);
					entity.Dispose();
				}

				//刷新父窗口
				Response.Write(JavaScript.ScriptStart);
				Response.Write("window.opener.document.all.btnSearch.submit();");
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "：" + ex.Message));
			}
		}

	}
}
