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
	/// SalJieZhuan 的摘要说明。
	/// </summary>
	public partial class SalJieZhuan : PageBase
	{
		protected RmsPM.WebControls.ToolsBar.ToolsButton ButtonNew;
		protected RmsPM.WebControls.ToolsBar.ToolsButton Toolsbutton1;
		protected System.Web.UI.HtmlControls.HtmlTable TableToolbar;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadDataGrid(0, true);
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
			this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);

		}
		#endregion

		private void IniPage()
		{
			this.txtParam.Value = "0";
			this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
			BLL.PageFacade.LoadSalBuildingSelect(this.sltSearchBuildingName, "", this.txtProjectCode.Value);

			/*
			this.txtParam.Value = Request.QueryString["param"];

			switch (this.txtParam.Value.Trim()) 
			{
					//销售补差
				case "1":
					this.tdTitle.InnerText = "销售补差";
					this.chkNotBalance.Visible = false;
					break;

				default:
					break;
			}
			*/
		}

		private void LoadDataGrid(int PageIndex, bool isEmpty)
		{
			try
			{
				EntityData entity;

				SalContractStrategyBuilder sb = new SalContractStrategyBuilder();
				sb.AddStrategy( new Strategy( SalContractStrategyName.ProjectCode,txtProjectCode.Value));

				string ContractID = this.txtSearchContractID.Value.Trim();
				if (ContractID != "")
					sb.AddStrategy(new Strategy(SalContractStrategyName.ContractID, ContractID));

				string BuildingName = this.sltSearchBuildingName.Value.Trim();
				if (BuildingName != "")
					sb.AddStrategy(new Strategy(SalContractStrategyName.BuildingName, BuildingName));

				string Status = this.sltSearchStatus.Value.Trim();;
				if (Status != "")
					sb.AddStrategy(new Strategy(SalContractStrategyName.Status, Status));

				if (isEmpty) 
					sb.AddStrategy(new Strategy(SalContractStrategyName.False));

				sb.AddOrder("ContractID", true);

				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				entity = qa.FillEntityData( "SalContract",sql );
				qa.Dispose();

				this.dgList.CurrentPageIndex = PageIndex;
				this.dgList.DataSource = entity.CurrentTable;
				this.dgList.Columns[2].FooterText = "笔数：" + entity.CurrentTable.Rows.Count.ToString();

				string[] arrField = {"BuildDim", "TotalPrice", "FactPrice", "TotalPayMoney"};
				decimal[] arrSum = BLL.MathRule.SumColumn(entity.CurrentTable, arrField);
				this.dgList.Columns[8].FooterText = arrSum[0].ToString("N");
				this.dgList.Columns[9].FooterText = arrSum[1].ToString("N");
				this.dgList.Columns[10].FooterText = arrSum[2].ToString("N");
				this.dgList.Columns[11].FooterText = arrSum[3].ToString("N");

				this.dgList.DataBind();

				entity.Dispose();
				

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
			}
		}

		private void dgList_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			LoadDataGrid(e.NewPageIndex, false);
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			LoadDataGrid(0, false);
		}

		private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item) 
				|| (e.Item.ItemType == ListItemType.AlternatingItem ))
			{
				//分页时序号连续
//				int i = e.Item.ItemIndex + 1;
//				if (dgList.AllowPaging)
//				{
//					i = dgList.PageSize * dgList.CurrentPageIndex + i;
//				}
//				e.Item.Cells[1].Text = i.ToString();
			}
		}

		protected void btnMakeVoucherHidden_ServerClick(object sender, System.EventArgs e)
		{
			Session["RelaCode"] = this.txtSelect.Value;

			string s = JavaScript.ScriptStart;
			s = s + String.Format("MakeVoucher('{0}', '{1}');", this.txtVoucherCode.Value, this.txtParam.Value);
			s = s + JavaScript.ScriptEnd;
			Page.RegisterStartupScript("MakeVoucher", s);
		}
	}
}
