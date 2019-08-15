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
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.ExchangeRate
{
	/// <summary>
	/// ExchangeRate 的摘要说明。
	/// </summary>
	public partial class ExchangeRate : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面

			if ( ! user.HasRight("2301"))
			{
				Response.Redirect( "../RejectAccess.aspx" );
				Response.End();
			}

			if ( !IsPostBack)
			{


				InitPage();
				BuildSearchString();
				LoadData();

				// 权限
				if ( ! user.HasRight("2302")) //汇率新增
				{
					this.btnNew.Visible = false;
				}

				if ( ! user.HasRight("2303")) //汇率编辑
				{
					this.dgExchangeRateList.Columns[0].Visible = true;
					this.dgExchangeRateList.Columns[1].Visible = false;
				}
				else
				{
					this.dgExchangeRateList.Columns[0].Visible = false;
					this.dgExchangeRateList.Columns[1].Visible = true;
				}

				this.dgExchangeRateList.Columns[0].Visible = true;
				this.dgExchangeRateList.Columns[1].Visible = false;
			}
		}


		private void InitPage()
		{
			try
			{
				BLL.PageFacade.LoadDictionarySelect(this.sltMoneyType,"币种","");

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化页面失败。");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
			}
		}

		private void LoadData()
		{
			string sql = (string)ViewState["_SearchSQL"];
			EntityData entity = DAL.EntityDAO.ExchangeRateDAO.GetExchangeRateList(sql);

			if ( rblShow.SelectedValue == "Now" )
			{
				DataTable dt = entity.CurrentTable.Clone();

				foreach( DataRow dr in entity.CurrentTable.Rows )
				{
					string ud_sFilter = "MoneyType = '" + dr["MoneyType"].ToString() + "'";

					if ( dt.Select(ud_sFilter).Length > 0 )
					{
						continue;
					}
					else
					{
						dt.ImportRow(dr);
					}
				}
				this.dgExchangeRateList.DataSource = dt;
				this.dgExchangeRateList.DataBind();
				this.GridPagination1.RowsCount = dt.Rows.Count.ToString();

			}
			else
			{
				this.dgExchangeRateList.DataSource = entity.CurrentTable;
				this.dgExchangeRateList.DataBind();
				this.GridPagination1.RowsCount = entity.CurrentTable.Rows.Count.ToString();
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

		private void BuildSearchString()
		{
			ExchangeRateStrategyBuilder ERSB=new ExchangeRateStrategyBuilder();

			string sql = "";

			string ud_sMoneyType = sltMoneyType.Items[sltMoneyType.SelectedIndex].Value;

			if ( ud_sMoneyType != "" )
			{
				ERSB.AddStrategy( new Strategy( ExchangeRateStrategyName.MoneyType,ud_sMoneyType));
			}

			ERSB.AddStrategy( new Strategy( ExchangeRateStrategyName.Status,"0"));


			if ( rblShow.SelectedValue != "Now" )
			{

				if (this.txtAdvSearch.Value != "none") 
				{

					if ( this.dtDate0.Value != "" || this.dtDate1.Value != "" )
					{
						ArrayList ar = new ArrayList();
						ar.Add(this.dtDate0.Value);
						ar.Add(this.dtDate1.Value);
						ERSB.AddStrategy( new Strategy( ExchangeRateStrategyName.CreateDate, ar));
					}
				}

	

			}

			ERSB.AddOrder("CreateDate",false);

			sql = ERSB.BuildMainQueryString();

			ViewState["_SearchSQL"] = sql;
		}

		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			this.BuildSearchString();
			this.LoadData();
		}
	}
}
