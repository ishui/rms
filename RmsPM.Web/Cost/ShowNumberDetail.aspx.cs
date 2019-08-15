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

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// ShowNumberDetail 的摘要说明。
	/// </summary>
	public partial class ShowNumberDetail : PageBase
	{

		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{

		}

		private void LoadData()
		{
			string startDate = Request["StartDate"]+"";
			string endDate = Request["EndDate"]+"";
			string numberType = Request["NumberType"]+"";
			string costCode = Request["CostCode"]+"";

			this.tableAH.Visible = false;
			this.tableUse.Visible = false;
			this.tableApply.Visible = false;

			switch ( numberType )
			{

					// 显示在时间段以内的付款，以及这些款项已付金额
				case "Use":
					DataSet useDataSet = BLL.CBSRule.GetContractAllocationDataSet(costCode,"","",startDate,endDate,"0,2");
					useDataSet.Tables[0].Columns.Add("PayedMoney",System.Type.GetType("System.Decimal"));
					foreach ( DataRow dr in useDataSet.Tables[0].Rows)
					{
						string allocateCode = (string)dr["AllocateCode"];
						BLL.CBSRule.GetContractAllocationAHMoney(startDate,endDate,allocateCode);
					}

					this.tableUse.Visible = true;
					this.dgUse.DataSource = useDataSet;
					this.dgUse.DataBind();
					useDataSet.Dispose();
					break;

					// 显示合同款项
				case "Apply":

					DataSet applyDataSet = BLL.CBSRule.GetContractAllocationDataSet(costCode,"","",startDate,endDate,"1");
					this.tableApply.Visible = true;
					this.dgApply.DataSource = applyDataSet;
					this.dgApply.DataBind();
					applyDataSet.Dispose();
					break;

					// 显示已发生金额
				case "AH":

					EntityData ahEntity = BLL.CBSRule.GetAHEntity(costCode,startDate,endDate,"","");
					this.tableAH.Visible = true;
					this.dgAH.DataSource = ahEntity;
					this.dgAH.DataBind();
					ahEntity.Dispose();
					break;

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
	}
}
