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
	/// BatchCostEstimate 的摘要说明。
	/// </summary>
	public partial class BatchCostEstimate : PageBase
	{
		private int IWan = 10000;

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( ! this.IsPostBack )
			{
				this.ViewState.Add("RemovedCode",new ArrayList());
				LoadData();

				string costCode = Request["CostCode"] + "";
				ArrayList ar = user.GetCBSResourceRight(costCode);
				if ( !ar.Contains("040202"))
					this.btnSave.Visible = false;
			}
		}


		private void LoadData()
		{
			string projectCode = Request["ProjectCode"] + "";
			string costCode = Request["CostCode"] + "";
			this.lblProjectName.Text = BLL.ProjectRule.GetProjectName(projectCode);
			
			try
			{
				string flag = "-1";
				V_CBSCostStrategyBuilder sb = new V_CBSCostStrategyBuilder();
				sb.AddStrategy( new Strategy( V_CBSCostStrategyName.ProjectCode,projectCode ));
				sb.AddStrategy( new Strategy( V_CBSCostStrategyName.Flag,flag ) );
				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData allCost = qa.FillEntityData("V_CBSCost" , sql);
//				allCost.CurrentTable.Columns.Add("IsEnd",System.Type.GetType("System.Int32"));
//				allCost.CurrentTable.Columns.Add("AccountPoint",System.Type.GetType("System.Int32"));
				qa.Dispose();


				
				EntityData cbs = DAL.EntityDAO.CBSDAO.GetCBSByProject(projectCode);
				DataRow[] drs0 = allCost.CurrentTable.Select("CostCode='" +  costCode + "'" );
				string fullCode = (string) drs0[0]["FullCode"];
				string codes = "";
				foreach ( DataRow dr in allCost.CurrentTable.Select( String.Format( "FullCode like '{0}%' " , fullCode),"FullCode" ) )
				{
					codes += (string)dr["CostCode"] + "," ;
				}
				this.txtAllCode.Value = codes;
				
				this.repeat1.DataSource = new DataView( allCost.CurrentTable,String.Format( "FullCode like '{0}%' " , fullCode),"SortID" ,DataViewRowState.CurrentRows );
				this.repeat1.DataBind();
				allCost.Dispose();


				// 已经审核过的不能再做估算
				EntityData entity = RmsPM.DAL.EntityDAO.CBSDAO.GetCostEstimateCheckByCode(projectCode);
				if ( entity.HasRecord())
					this.btnSave.Visible = true;
				entity.Dispose();


			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
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




		private void AdCostEstimate( EntityData allCost , EntityData cbs )
		{
			DataRow[] drs0 = cbs.CurrentTable.Select( "ChildCount>0","Deep DESC" );
			int iCount = drs0.Length;
			for ( int i=0;i<iCount;i++)
			{
				string costCode = (string) drs0[i]["CostCode"];
				SumChild( allCost, cbs, costCode );
			}
		}


		private void SumChild ( EntityData allCost , EntityData cbs, string costCode  )
		{
			DataRow[] drs0 = cbs.CurrentTable.Select(  String.Format("CostCode='{0}'",costCode) );
			string fullCode = (string) drs0[0]["fullCode"];
			
			decimal sumMoney = decimal.Zero;

			DataRow[] childCBSRows = cbs.CurrentTable.Select( String.Format( "FullCode like '{0}%' and parentCode ='{1}' " , fullCode , costCode ),"FullCode" ) ;
			foreach ( DataRow childCBSRow in childCBSRows )
			{
				string childCode = (string) childCBSRow["CostCode"];
				DataRow[] childRows = allCost.CurrentTable.Select(String.Format("CostCode='{0}'",childCode));
				if ( childRows.Length>0)
				{
					if ( !childRows[0].IsNull("TotalMoney"))
					{
						sumMoney += (decimal)childRows[0]["TotalMoney"];
					}
				}
			}

			DataRow[] drsCostRow = allCost.CurrentTable.Select(String.Format("CostCode='{0}'",costCode));
			if ( drsCostRow.Length>0)
			{
				if ( ! BLL.MathRule.CheckDecimalEqual(decimal.Zero,sumMoney))
				{
					drsCostRow[0]["AccountPoint"] = 2;
					drsCostRow[0]["TotalMoney"] = sumMoney;
				}
				else
					drsCostRow[0]["AccountPoint"] = 0;

			}
		}


//		private RepeaterItem GetRepeaterItem( Repeater rp , string costCode )
//		{
//			foreach( RepeaterItem li in rp.Items )
//			{
//				string tempCode = ((HtmlInputHidden)li.FindControl("txtCostCode")).Value;
//				if ( tempCode == costCode )
//					return li;
//			}
//			return null;
//		}
//
//		private decimal GetInputNumber ( RepeaterItem li, string controlName)
//		{
//			decimal re = decimal.Zero;
//			string inputText = ((HtmlInputText)li.FindControl(controlName)).Value;
//			if ( Rms.Check.StringCheck.IsNumber(inputText))
//				re = decimal.Parse(inputText);
//
//			return re;
//		}

		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"] + "";
			try
			{
				string costCode = Request["CostCode"];
				string flag = "-1";
				CostStrategyBuilder sb = new CostStrategyBuilder();
				sb.AddStrategy( new Strategy( CostStrategyName.ProjectCode,projectCode ) );
				sb.AddStrategy( new Strategy( CostStrategyName.Flag,flag ) );
				string sql = sb.BuildMainQueryString();
				QueryAgent qa = new QueryAgent();
				EntityData allCost = qa.FillEntityData( "Cost",sql );
				qa.Dispose();

				EntityData cbs = DAL.EntityDAO.CBSDAO.GetCBSByProject(projectCode);

				string[] codesTemp = this.txtResult.Value.Trim().Split( new char[]{';'} );
				for ( int i=0;i<codesTemp.Length;i++)
				{
					if ( codesTemp[i] != "" )
					{
						string[] va = codesTemp[i].Split( new char[]{','} );
						string costCodeTemp = va[0];
						string v1 = va[1];

						DataRow[] drs = allCost.CurrentTable.Select( String.Format( "CostCode='{0}'" ,costCodeTemp) );
						if ( drs.Length>0)
						{
							if ( v1 == "F" )
							{
								drs[0]["projectQuantity"] = System.DBNull.Value;
								drs[0]["totalMoney"] = System.DBNull.Value;
								drs[0]["AccountPoint"] = 0;
							}
							else
							{

								if (va[4] != "" )
									drs[0]["projectQuantity"] = decimal.Parse(va[4]);
								else
									drs[0]["projectQuantity"] = System.DBNull.Value;
	
								if (va[5] != "" )
									drs[0]["totalMoney"] = decimal.Parse(va[5])*IWan;
								else
									drs[0]["totalMoney"] = System.DBNull.Value;
	
								drs[0]["AccountPoint"] = 1;
							}
						}
	
						drs = cbs.CurrentTable.Select( String.Format( "CostCode='{0}'" ,costCodeTemp) );
						if ( drs.Length > 0 )
						{
							if ( v1 == "T" )
							{
								if ( va[2] != "" )
									drs[0]["UnitPrice"] = decimal.Parse(va[2]);
								else
									drs[0]["UnitPrice"] = System.DBNull.Value;
		
								drs[0]["MeasurementUnit"] =va[3];
							}
						}
					}
				}

				AdCostEstimate(allCost,cbs);

				DAL.EntityDAO.CBSDAO.UpdateCost(allCost);
				DAL.EntityDAO.CBSDAO.UpdateCBS(cbs);

				allCost.Dispose();
				Response.Write(JavaScript.ScriptStart);
				Response.Write(JavaScript.OpenerReload(false));
				Response.Write("window.close();");
				Response.Write(JavaScript.ScriptEnd);
				Response.End();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog( this.ToString(),ex,"");
			}
		}


	}
}
