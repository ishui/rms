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

namespace RmsPM.Web.Sal
{
	/// <summary>
	/// SalContractView 的摘要说明。
	/// </summary>
	public partial class SalContractView : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
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
			this.txtContractCode.Value = Request["ContractCode"] + "";
		}

		private void LoadData()
		{
			string ContractCode = this.txtContractCode.Value;

			try
			{
				if ( ContractCode != "")
				{
					EntityData entity = DAL.EntityDAO.SalDAO.GetStandard_SalContractByCode(ContractCode);

					if ( entity.HasRecord())
					{
 
						this.lbContractID.Text = entity.GetString("ContractID");
						this.lbContractDate.Text = entity.GetDateTime("ContractDate", "yyyy-MM-dd");
						this.lbClientName.Text = entity.GetString("ClientName");
						this.lbChamberName.Text = entity.GetString("ChamberName");
						this.lbRoom.Text = entity.GetString("Room");
						this.lbBofangCode.Text = entity.GetString("BofangCode");
						this.lbTotalPrice.Text = entity.GetDecimal("TotalPrice").ToString("n");
						this.lbFactPrice.Text = entity.GetDecimal("FactPrice").ToString("n");
						this.lbUnitPrice.Text = entity.GetDecimal("UnitPrice").ToString("n");
						this.lbBuildDim.Text = entity.GetDecimal("BuildDim").ToString("n");
						this.lbRoomDim.Text = entity.GetDecimal("RoomDim").ToString("n");
						
					}

					entity.SetCurrentTable("SalPayPlan");
					this.dgSalPayPlan.DataSource = entity.CurrentTable;
					this.dgSalPayPlan.Columns[2].FooterText = Total(entity.CurrentTable, "PlanMoney").ToString("n");//BLL.MathRule.SumColumn(entity.CurrentTable, "PlanMoney").ToString("f");
                    this.dgSalPayPlan.Columns[3].FooterText = Total(entity.CurrentTable, "PayMoney").ToString("n");//BLL.MathRule.SumColumn(entity.CurrentTable, "PayMoney").ToString("f");
					this.dgSalPayPlan.DataBind();

					entity.SetCurrentTable("SalPay");
					this.dgSalPay.DataSource = entity.CurrentTable;
                    this.dgSalPay.Columns[2].FooterText = Total(entity.CurrentTable, "PayMoney").ToString("n");//BLL.MathRule.SumColumn(entity.CurrentTable, "PayMoney").ToString("f");
					this.dgSalPay.DataBind();

					entity.Dispose();
				}
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}

		}

		private decimal Total(DataTable m_Table,string m_strField)
		{
			decimal m_dcReturn = 0M;
			foreach(DataRow m_Row in m_Table.Rows)
			{
				try 
				{
					m_dcReturn+=decimal.Parse(m_Row[m_strField].ToString());
				}
				catch 
				{
				}
			}
			return m_dcReturn;
		}

	}
}
