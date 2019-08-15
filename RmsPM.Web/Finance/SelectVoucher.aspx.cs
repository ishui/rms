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
using Rms.Web;
using Rms.ORMap;
using RmsPM.DAL.QueryStrategy;
using RmsPM.DAL.EntityDAO;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// 
	/// </summary>
	public partial class SelectVoucher : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnOK;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadDataGrid();
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

		private void IniPage()
		{
			try 
			{
				this.txtAct.Value = Request["Act"];
				this.txtRefreshScript.Value = Request["RefreshScript"];
				this.txtProjectCode.Value = Request["ProjectCode"];

				BLL.PageFacade.LoadVoucherTypeSelect(this.sltSearchVoucherType, "");
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		/// <summary>
		/// 显示列表
		/// </summary>
		private void LoadDataGrid() 
		{
			this.lblMessage.Text = "";

			try
			{
				string VoucherID = this.txtSearchVoucherID.Value.Trim();

				VoucherStrategyBuilder sb = new VoucherStrategyBuilder();
				sb.AddStrategy( new Strategy( VoucherStrategyName.ProjectCode,this.txtProjectCode.Value));

				if ( VoucherID.Length > 0 )
					sb.AddStrategy( new Strategy(DAL.QueryStrategy.VoucherStrategyName.VoucherID, VoucherID)  );

				if ( this.sltSearchVoucherType.Value != "" )
					sb.AddStrategy( new Strategy( VoucherStrategyName.VoucherType,this.sltSearchVoucherType.Value ));

				sb.AddOrder( "VoucherID", true);
				string sql = sb.BuildQueryViewString();

				Rms.ORMap.QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData("Voucher",sql);

				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.DataBind();
				entity.Dispose();
				qa.Dispose();
			}
			catch ( Exception ex )
			{
				this.lblMessage.Text = "选择凭证查询出错";
				ApplicationLog.WriteLog(this.ToString(), ex, "选择凭证查询出错");
			}
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			LoadDataGrid();
		}

	}
}
