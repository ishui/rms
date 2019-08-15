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
using RmsPM.DAL;
using RmsPM.BLL;
using Rms.ORMap;
using Rms.Web;

namespace RmsPM.Web.SelectBox
{
	/// <summary>
	/// 选择供应商。
	/// </summary>
	public partial class SelectSupplier : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				//LoadDataGrid();
			}
		}

		private void IniPage()
		{
			if(!user.HasOperationRight("140102"))
				this.btnAddNewSupplier.Visible = false;
			this.txtSupplierName.Value = Request["SupplierName"]+"";
		}

//		private void LoadDataGrid()
//		{
//			try
//			{
//				string supplierName=this.txtSupplierName.Value.Trim();
//				
//				RmsPM.DAL.QueryStrategy.SupplierStrategyBuilder ssb= new RmsPM.DAL.QueryStrategy.SupplierStrategyBuilder();
//				if ( supplierName.Length > 0 )
//					ssb.AddStrategy( new Strategy(RmsPM.DAL.QueryStrategy.SupplierStrategyName.SupplierName,"%"+supplierName+"%"));
//				string sql = ssb.BuildMainQueryString();
//
//				Rms.ORMap.QueryAgent qa = new QueryAgent();
//				EntityData entity = qa.FillEntityData("Supplier",sql);
//				qa.Dispose();
//				this.dgList.DataSource = entity.CurrentTable ;
//				this.dgList.DataBind();
//				entity.Dispose();
//
//			}
//			catch( Exception ex )
//			{
//				ApplicationLog.WriteLog(this.ToString(),ex,"加载项目列表错误。");
//			}
//		}

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

//		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
//		{
//			
//			this.dgList.CurrentPageIndex = e.NewPageIndex;
//			LoadDataGrid();
//		}
//
//		private void btnSearch_ServerClick(object sender, System.EventArgs e)
//		{
//			this.dgList.CurrentPageIndex = 0;
//			LoadDataGrid();
//		}

		
	}
}
