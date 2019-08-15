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
using RmsPM.DAL.QueryStrategy;
using RmsPM.DAL.EntityDAO;
using RmsPM.Web;
using Rms.ORMap;


namespace RmsPM.Web.Material
{
	/// <summary>
	/// MaterialCostListFrame 的摘要说明。
	/// </summary>
	public partial class MaterialCostListFrame : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!IsPostBack)
			{
				IniPage();
				BuildSearchString();
				LoadDataGrid();
			}
		}

		private void IniPage()
		{
            //只显示某个枝条
            this.txtRootGroupCode.Value = Request.QueryString["RootGroupCode"];

            string RootGroupName = BLL.SystemGroupRule.GetSystemGroupName(this.txtRootGroupCode.Value);
            if (RootGroupName == "系数含量")
            {
                ((TemplateColumn)this.dgList.Columns[3]).HeaderText = "含量<br>ratio";
            }
        }

		private void LoadDataGrid() 
		{
			try 
			{
				string sql = (string)this.ViewState["SqlString"];

//                Response.Write(sql);

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData("MaterialCost",sql);
				qa.Dispose();
				dgList.DataSource = entity.CurrentTable;
				dgList.DataBind();

				int RecordCount = entity.CurrentTable.Rows.Count;
				this.GridPagination1.RowsCount = RecordCount.ToString();
				this.lblRecordCount.Text = RecordCount.ToString();

				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
                Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
			}
		}

		private void BuildSearchString()
		{
			string IsEmpty = Request["IsEmpty"] + "";

			string MaterialTypeCode = Request["MaterialTypeCode"] +"";
            string Unit = Request["Unit"] + "";

            string Price0 = Request["Price0"] + "";
            string Price1 = Request["Price1"] + "";

            string Project = Request["Project"] + "";

            string BiddingDate0 = Request["BiddingDate0"] + "";
            string BiddingDate1 = Request["BiddingDate1"] + "";

            string Description = Request["Description"] + "";
            string DescriptionEn = Request["DescriptionEn"] + "";
            string Category = Request["Category"] + "";
            string AreaCode = Request["AreaCode"] + "";

			string chkSearch = Request["ChkSearch"]+"";

            string RootGroupCode = this.txtRootGroupCode.Value;

			MaterialCostStrategyBuilder sb = new MaterialCostStrategyBuilder();
			
			if (IsEmpty == "1")
				sb.AddStrategy( new Strategy( MaterialCostStrategyName.False));

            if (Unit != "")
                sb.AddStrategy(new Strategy(MaterialCostStrategyName.Unit, "%" + Unit + "%"));

            if (Price0.Trim() != "" || Price1.Trim() != "")
            {
                ArrayList ar = new ArrayList();
                ar.Add(Price0.Trim());
                ar.Add(Price1.Trim());
                sb.AddStrategy(new Strategy(MaterialCostStrategyName.PriceRange, ar));
            }

            if (Project != "")
                sb.AddStrategy(new Strategy(MaterialCostStrategyName.Project, "%" + Project + "%"));

            if (BiddingDate0.Trim() != "" || BiddingDate1.Trim() != "")
            {
                ArrayList ar = new ArrayList();
                ar.Add(BiddingDate0.Trim());
                ar.Add(BiddingDate1.Trim());
                sb.AddStrategy(new Strategy(MaterialCostStrategyName.BiddingDateRange, ar));
            }

            if (Description != "")
                sb.AddStrategy(new Strategy(MaterialCostStrategyName.Description, "%" + Description + "%"));

            if (DescriptionEn != "")
                sb.AddStrategy(new Strategy(MaterialCostStrategyName.DescriptionEn, "%" + DescriptionEn + "%"));

            if (Category != "")
                sb.AddStrategy(new Strategy(MaterialCostStrategyName.Category, "%" + Category + "%"));

            if (AreaCode != "")
				sb.AddStrategy( new Strategy( MaterialCostStrategyName.AreaCode, "%"+AreaCode+"%"));

			if ( chkSearch == "1"  && (MaterialTypeCode != "" ))
			{
				ArrayList arS = new ArrayList();
				arS.Add(MaterialTypeCode);
				arS.Add("0");
				sb.AddStrategy( new Strategy( MaterialCostStrategyName.GroupCodeEx, arS ));
			}

            //只显示某个枝条
            if (RootGroupCode != "")
            {
                ArrayList arS = new ArrayList();
                arS.Add(RootGroupCode);
                arS.Add("0");
                sb.AddStrategy(new Strategy(MaterialCostStrategyName.GroupCodeEx, arS));
            }

			ArrayList arA = new ArrayList();
			arA.Add(user.UserCode);
			arA.Add(user.BuildStationCodes());
			sb.AddStrategy( new Strategy( MaterialCostStrategyName.AccessRange,arA));

//			sb.AddStrategy( new Strategy( MaterialCostStrategyName.SubjectSetCode,subjectSetCode ));

			//排序
			string sortsql = BLL.GridSort.GetSortSQL(ViewState);

			string sql = sb.BuildMainQueryString();

			if (sortsql != "")
			{
				//点列标题排序
				sql = sql + " order by " + sortsql;
			}

			this.ViewState.Add("SqlString",sql);
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
			this.dgList.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemCreated);
			this.dgList.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgList_PageIndexChanged);
			this.dgList.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgList_SortCommand);

		}
		#endregion

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			LoadDataGrid();
		}

		protected void GridPagination1_PageIndexChange(object sender, System.EventArgs e)
		{
			try
			{
				this.LoadDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				BLL.GridSort.ItemCreate((DataGrid)sender, ViewState, sender, e);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void dgList_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			try
			{
				BLL.GridSort.SortCommand((DataGrid)source, ViewState, source, e);
				((DataGrid)source).CurrentPageIndex = 0;
				BuildSearchString();
				LoadDataGrid();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}



	}
}
