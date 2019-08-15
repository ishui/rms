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
using RmsPM.BLL;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// PAList 的摘要说明。
	/// </summary>
	public partial class PAList : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadDataGrid();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

				//权限
				this.btnAdd.Visible = base.user.HasRight("060302");
				this.btnDownload.Visible = base.user.HasRight("060306");

				BLL.PageFacade.LoadVoucherTypeSelect(this.sltVoucherType, "");
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadDataGrid()
		{
			try
			{
				VoucherStrategyBuilder sb = new VoucherStrategyBuilder();
				sb.AddStrategy( new Strategy( VoucherStrategyName.ProjectCode,txtProjectCode.Value));

				if ( this.ucAccountant.Value != "" )
					sb.AddStrategy( new Strategy( VoucherStrategyName.Accountant,this.ucAccountant.Value ));

				if ( this.dtbMakeDate0.Value != "" || this.dtbMakeDate1.Value != "" )
				{
					ArrayList ar = new ArrayList();
					ar.Add(this.dtbMakeDate0.Value);
					ar.Add(this.dtbMakeDate1.Value);
					sb.AddStrategy( new Strategy( VoucherStrategyName.MakeDate,ar ));
				}

				if ( this.sltVoucherType.Value != "" )
					sb.AddStrategy( new Strategy( VoucherStrategyName.VoucherType,this.sltVoucherType.Value ));

				if ( this.txtVoucherID.Value != "" )
					sb.AddStrategy( new Strategy( VoucherStrategyName.VoucherID,this.txtVoucherID.Value ));

				ArrayList arStatus = new ArrayList();
				if ( this.chkStatus0.Checked )
					arStatus.Add("0");
				if ( this.chkStatus1.Checked )
					arStatus.Add("1");
				if ( this.chkStatus2.Checked )
					arStatus.Add("2");
				string status = BLL.ConvertRule.GetArrayLinkString(arStatus);
				if ( status != "" )
					sb.AddStrategy( new Strategy( VoucherStrategyName.Status, status ));

//				if ( this.sltIsExported.Value != "" )
//					sb.AddStrategy( new Strategy( VoucherStrategyName.IsExported,this.sltIsExported.Value ));

				sb.AddOrder( "MakeDate" ,false);
				sb.AddOrder( "VoucherCode" ,false);
				string sql = sb.BuildQueryViewString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "Voucher",sql );
				qa.Dispose();

				string[] arrField = {"TotalMoney"};
				decimal[] arrSum = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);
				this.dgList.Columns[3].FooterText = arrSum[0].ToString("N");

				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.DataBind();

				entity.Dispose();
				

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
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
			this.dgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgList_PageIndexChanged_1);

		}
		#endregion

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			int index = e.NewPageIndex;
			this.dgList.CurrentPageIndex = index;
			LoadDataGrid();
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			LoadDataGrid();
		}

		/*
		/// <summary>
		/// 导出一条或多条凭证
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDownloadHidden_ServerClick(object sender, System.EventArgs e)
		{
			string codes = this.txtSelect.Value;
			string SaveFileNameHttp = BLL.VoucherRule.MakeVoucherFile(codes, Server);
			Response.Write(Rms.Web.JavaScript.WinOpen(true, SaveFileNameHttp,"","","","","",true,true,false,true,true,true,false,false));

			//刷新导出标志
			LoadDataGrid();
		}
*/

		protected void btnAllowPaging_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.AllowPaging = !(this.dgList.AllowPaging);

			if (this.dgList.AllowPaging) 
			{
				this.btnAllowPaging.Value = "取消分页";
			}
			else 
			{
				this.btnAllowPaging.Value = "分页显示";
			}

			this.LoadDataGrid();
		}

		private void dgList_PageIndexChanged_1(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid();
		}
	}
}
