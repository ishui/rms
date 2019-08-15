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
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.Web;
using Rms.ORMap;
using RmsPM.DAL.QueryStrategy;


namespace RmsPM.Web.Supplier
{
	/// <summary>
	/// Supplier 的摘要说明。
	/// </summary>
	public partial class Supplier : PageBase
	{
		protected RmsPM.WebControls.ToolsBar.ToolsButton ToolsButtonContractEdit;
		protected System.Web.UI.WebControls.DataGrid dgList;
		protected RmsPM.WebControls.ToolsBar.ToolsButton ToolsbuttonImportSupl;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!IsPostBack)
			{
				IniPage();
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
			string corpCode = Request["CorpCode"] + "";
			string projectCode = Request["ProjectCode"]+"";
			string subjectSetCode = "";
			int isSelfAccount = 0;
			if ( corpCode !="")
				subjectSetCode = BLL.SubjectRule.GetUnitSubjectSet( corpCode, ref isSelfAccount);
			if ( projectCode !="")
				subjectSetCode = BLL.SubjectRule.GetProjectSubjectSet( projectCode, ref isSelfAccount);

			this.txtSubjectSetCode.Value = subjectSetCode;

//			if(!user.HasOperationRight("140102"))
//				this.btnNew.Visible = false;

			ArrayList ar = user.GetClassRight("Supplier");
			if ( ! ar.Contains("140102"))
				this.btnNew.Visible = false;

			if(!user.HasOperationRight("140105"))
				this.btnInputSupplier.Visible = false;
            switch (this.up_sPMNameLower)
            {
                case "shidaipm":
                    this.TrIsAuditted.Visible = true;
                    break;
                default:
                    this.TrIsAuditted.Visible = false;
                    break;
            }
		}

//		private void LoadDataGrid() 
//		{
//			try 
//			{
//				string sql = (string)this.ViewState["SqlString"];
//				QueryAgent qa = new QueryAgent();
//				EntityData entity = qa.FillEntityData("Supplier",sql);
//				qa.Dispose();
//				dgList.DataSource = entity.CurrentTable;
//				dgList.DataBind();
//				entity.Dispose();
//			}
//			catch (Exception ex)
//			{
//				ApplicationLog.WriteLog(this.ToString(),ex,"");
//			}
//		}
//
//
//		private void BuildSearchString()
//		{
//			string subjectSetCode = this.txtSubjectSetCode.Value;
//			string supplierName = this.txtSupplierName.Value.Trim();
//			SupplierStrategyBuilder sb = new SupplierStrategyBuilder();
//			if ( supplierName != "")
//				sb.AddStrategy( new Strategy( SupplierStrategyName.SupplierName, "%"+supplierName+"%" ));
//
//			string supplierTypeCode = this.txtSubjectSetCode.Value;
//			if ( (this.chkSearch.Checked) && (supplierTypeCode == "" ))
//				sb.AddStrategy( new Strategy( SupplierStrategyName.SupplierTypeCodeEx, supplierTypeCode ));
//
//			sb.AddStrategy( new Strategy( SupplierStrategyName.SubjectSetCode,subjectSetCode ));
//			string sql = sb.BuildMainQueryString();
//			this.ViewState.Add("SqlString",sql);
//		}
//
//
//		private void BuildSearchString0()
//		{
//			string subjectSetCode = this.txtSubjectSetCode.Value;
//			string supplierTypeCode = this.txtSelectSupplierTypeCode.Value;
//			SupplierStrategyBuilder sb = new SupplierStrategyBuilder();
//			sb.AddStrategy( new Strategy( SupplierStrategyName.SupplierTypeCodeEx, supplierTypeCode ));
//			sb.AddStrategy( new Strategy( SupplierStrategyName.SubjectSetCode,subjectSetCode ));
//			string sql = sb.BuildMainQueryString();
//			this.ViewState.Add("SqlString",sql);
//		}
//
//		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
//		{
//			this.dgList.CurrentPageIndex = e.NewPageIndex;
//			LoadDataGrid();
//		}
//
//		private void btnSearch_ServerClick(object sender, System.EventArgs e)
//		{
//			BuildSearchString();
//			LoadDataGrid();
//		}
//
//		private void btnRefreshSelectType_ServerClick(object sender, System.EventArgs e)
//		{
//			BuildSearchString0();
//			LoadDataGrid();
//		}
	}
}
