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
	/// ExchangeRate ��ժҪ˵����
	/// </summary>
	public partial class ExchangeRate : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��

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

				// Ȩ��
				if ( ! user.HasRight("2302")) //��������
				{
					this.btnNew.Visible = false;
				}

				if ( ! user.HasRight("2303")) //���ʱ༭
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
				BLL.PageFacade.LoadDictionarySelect(this.sltMoneyType,"����","");

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ��ҳ��ʧ�ܡ�");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ��ʧ�ܣ�" + ex.Message));
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
