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
using RmsPM.DAL.EntityDAO;
using Rms.Web;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// SelectSuplList 的摘要说明。
	/// </summary>
	public partial class SelectSuplList : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnSelectHidden;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				IniPage();
				LoadDataGrid(true);
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
			this.dgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgList_PageIndexChanged);

		}
		#endregion

		private void IniPage() 
		{
			try 
			{
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void LoadDataGrid(bool isEmpty) 
		{
			try 
			{
				DataTable tb;

				if (this.rbType.SelectedValue == "0") 
				{
					SalSuplStrategyBuilder sb = new SalSuplStrategyBuilder();

					if (isEmpty)
						sb.AddStrategy(new Strategy(SalSuplStrategyName.False));

					string SuplName = this.txtSearchSuplName.Value.Trim();
					if (SuplName != "")
						sb.AddStrategy(new Strategy(SalSuplStrategyName.SuplName, SuplName));

					sb.AddOrder("SuplName", true);

					string sql = sb.BuildMainQueryString();

					QueryAgent qa = new QueryAgent();
					EntityData entity = qa.FillEntityData( "SalSupl",sql );
					tb = entity.CurrentTable;
					qa.Dispose();
					entity.Dispose();
				}
				else
				{
					SupplierStrategyBuilder sb = new SupplierStrategyBuilder();

					if (isEmpty)
						sb.AddStrategy(new Strategy(SupplierStrategyName.False));

					string SuplName = this.txtSearchSuplName.Value.Trim();
					if (SuplName != "")
						sb.AddStrategy(new Strategy(SupplierStrategyName.SupplierName, SuplName));

					sb.AddOrder("SupplierName", true);

					string sql = sb.BuildMainQueryString();

					QueryAgent qa = new QueryAgent();
					EntityData entity = qa.FillEntityData( "Supplier",sql );
					tb = entity.CurrentTable;
					qa.Dispose();
					entity.Dispose();
				}

				dgList.DataSource = tb;
				dgList.DataBind();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid(false);
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.dgList.CurrentPageIndex = 0;
			LoadDataGrid(false);
		}

	}
}
