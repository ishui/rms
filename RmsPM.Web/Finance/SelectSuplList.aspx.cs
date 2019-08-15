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
	/// SelectSuplList ��ժҪ˵����
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
