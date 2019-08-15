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
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// 成本核算 的摘要说明。
	/// </summary>
	public partial class CostCheckReport : PageBase
	{

		DataRow[] m_FirstLevelCostRows = null;		//一级成本节点
		EntityData m_CostEntity = null;				//费用对象
		EntityData m_BuildingEntity = null;			//楼栋
		EntityData m_PBSUnit = null;				//单位工程

		DataTable m_DtApportion = null;				//分摊到楼栋的费用数据
		DataTable m_DtUnitApportion = new DataTable();			//分摊到单位工程的费用数据
		

		decimal m_TotalBuildArea = decimal.Zero;
		decimal m_TotalCost = decimal.Zero;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];

				BLL.PageFacade.LoadBuildingAreaFieldSelect(this.sltBuildingAreaField, BLL.CostRule.GetApportionAreaField(this.txtProjectCode.Value));

//				LoadData();
			}
		}

		private void LoadData()
		{
			if (this.txtBuildingName.Value.Trim() == "")
			{
				this.txtBuildingCode.Value = "";
			}

			string projectCode = this.txtProjectCode.Value;
			string DateBegin = this.dtDateBegin.Value;
			string DateEnd = this.dtDateEnd.Value;
			string BuildingCodes = this.txtBuildingCode.Value.Trim();

			//保存面积字段
			BLL.SystemRule.UpdateProjectConfigValue(projectCode, BLL.SystemRule.m_CostApportionBuildingAreaField, this.sltBuildingAreaField.Value);

			try
			{
				// 临时表，装单位工程的数据
				this.m_DtUnitApportion.Columns.Add( "UnitCode",System.Type.GetType("System.String") );
				this.m_DtUnitApportion.Columns.Add( "BuildingCode",System.Type.GetType("System.String") );
				this.m_DtUnitApportion.Columns.Add( "CostCode",System.Type.GetType("System.String") );
				this.m_DtUnitApportion.Columns.Add( "FullCode",System.Type.GetType("System.String") );
				this.m_DtUnitApportion.Columns.Add( "ApportionMoney",System.Type.GetType("System.Decimal") );

				this.m_CostEntity = DAL.EntityDAO.CBSDAO.GetCBSByProject(projectCode);
				this.m_PBSUnit = DAL.EntityDAO.PBSDAO.GetPBSUnitByProject(projectCode);
				this.m_BuildingEntity = DAL.EntityDAO.ProductDAO.GetBuildingNotAreaByProjectCode(projectCode);

				//一级成本项
				int deep = 2;
				this.m_FirstLevelCostRows = this.m_CostEntity.CurrentTable.Select(String.Format("Deep={0}",deep),"SortID,FullCode");
				this.m_DtApportion = BLL.CostRule.BuildingCostApportionAllPayout(projectCode, DateBegin, DateEnd, BLL.CostRule.GetApportionAreaField(projectCode));

				this.m_TotalCost = BLL.MathRule.SumColumn(this.m_DtApportion,"ApportionMoney");
				this.lblProjectTotalCost.Text = BLL.StringRule.BuildShowNumberString(this.m_TotalCost);

				//面积字段
				string BuildingAreaFieldName = BLL.CostRule.GetApportionAreaField(projectCode);
				string BuildingAreaFieldDesc = BLL.ProductRule.GetBuildingAreaFieldDesc(BuildingAreaFieldName);
				this.lblBuildingAreaFieldDesc.Text = BuildingAreaFieldDesc;

				m_BuildingEntity.CurrentTable.Columns.Add( "ApportionArea",System.Type.GetType("System.Decimal") );

				foreach ( DataRow dr in m_BuildingEntity.CurrentTable.Rows )
				{
					string buildingCode = BLL.ConvertRule.ToString("BuildingCode");
					dr["ApportionArea"] = dr[BuildingAreaFieldName];
				}

				// 计算总面积
				this.m_TotalBuildArea = BLL.MathRule.SumColumn( this.m_BuildingEntity.CurrentTable, "ApportionArea")  ;
				this.lblProjectTotalArea.Text = BLL.StringRule.BuildShowNumberString(this.m_TotalBuildArea);

				// 初始化表头
				IniTableTitle();

//				BuildTableContext( );

				//过滤楼栋
				if (BuildingCodes != "")
				{
					//删除不搜索的楼栋
					string inBuildingCodes = "'" + BuildingCodes.Replace(",", "','") + "'";
					DataRow[] drsDelete = this.m_BuildingEntity.CurrentTable.Select(string.Format("BuildingCode not in ({0})", inBuildingCodes));

					for(int k=drsDelete.Length-1;k>=0;k--)
					{
						this.m_BuildingEntity.CurrentTable.Rows.Remove(drsDelete[k]);
					}

					//删除无楼栋的单位工程
					for(int k=this.m_PBSUnit.CurrentTable.Rows.Count-1;k>=0;k--)
					{
						DataRow drUnit = this.m_PBSUnit.CurrentTable.Rows[k];
						string unitCode  = BLL.ConvertRule.ToString( drUnit["PBSUnitCode"]);
						if (this.m_BuildingEntity.CurrentTable.Select(String.Format( "PBSUnitCode='{0}'" ,unitCode)).Length == 0)
						{
							this.m_PBSUnit.CurrentTable.Rows.Remove(drUnit);
						}
					}
				}

				// 处理有单位工程的楼栋，
				foreach ( DataRow drUnit in this.m_PBSUnit.CurrentTable.Rows)
				{
					string unitCode  = BLL.ConvertRule.ToString( drUnit["PBSUnitCode"]);
					string unitName = BLL.ConvertRule.ToString( drUnit["PBSUnitName"]);
					decimal unitArea = BLL.ConvertRule.ToDecimal(drUnit["BuildingAreaSum"]);
					foreach ( DataRow drBuilding in this.m_BuildingEntity.CurrentTable.Select( String.Format( "PBSUnitCode='{0}'" ,unitCode ) ))
					{
						BuildSingleRow( (string) drBuilding["BuildingCode"],(string)drBuilding["BuildingName"] , (decimal)drBuilding["ApportionArea"] , unitCode,unitArea );
					}

					// 加一个总计
					BuildUnitRow( unitCode,unitName,unitArea );

					// 加一个分行
					HtmlTableRow tr = new HtmlTableRow();
					HtmlTableCell td = new HtmlTableCell();
					tr.Cells.Add(td);
					td.ColSpan = this.m_FirstLevelCostRows.Length * 2 + 1;
					td.Height = "15";
					this.tbReport.Rows.Add(tr);
				}
				
				// 没有单位工程的楼栋
				foreach ( DataRow drBuilding in this.m_BuildingEntity.CurrentTable.Select( " isnull(PBSUnitCode,'')='' " ))
				{
					BuildSingleRow( (string) drBuilding["BuildingCode"],(string)drBuilding["BuildingName"]  , (decimal)drBuilding["ApportionArea"],"",decimal.Zero);
				}

				this.m_CostEntity.Dispose();
				this.m_BuildingEntity.Dispose();
				this.m_PBSUnit.Dispose();
				this.m_DtApportion.Dispose();
				this.m_DtUnitApportion.Dispose();
				

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void BuildSingleRow( string buildingCode , string buildingName,  decimal buildingArea ,string unitCode, decimal unitArea )
		{

			HtmlTableRow row = new HtmlTableRow();
			this.tbReport.Rows.Add(row);

			HtmlTableCell cell0 = new  HtmlTableCell();
			cell0.InnerHtml = buildingName;
			cell0.NoWrap = true;
			cell0.Align="center";
			row.Cells.Add(cell0);

			// 平摊部分 , buildingCode = ""
			int iCount = this.m_FirstLevelCostRows.Length;
			for ( int i= 0;i<iCount; i++)
			{
				string costCode = (string)m_FirstLevelCostRows[i]["CostCode"];
				string fullCode = (string)m_FirstLevelCostRows[i]["FullCode"];

				//decimal partMoney = BLL.MathRule.SumColumn(this.m_PaymentTable,"Money" ,String.Format( "CostCode='{0}' and BuildingCode='{1}' " , costCode,"" ) ) ;
				decimal buildingMoney = GetBuildingMoney( buildingCode,fullCode);
				decimal pbsUnitMoney = decimal.Zero ;
				if ( unitCode != "" && unitArea != decimal.Zero )
					pbsUnitMoney = GetPBSMoney(buildingCode,fullCode) * buildingArea / unitArea;
				decimal projectMoney = decimal.Zero ;
				if ( this.m_TotalBuildArea != decimal.Zero )
					projectMoney =  GetProjectMoney (  fullCode) * buildingArea / this.m_TotalBuildArea;

				decimal partMoney = buildingMoney + pbsUnitMoney + projectMoney;
				HtmlTableCell cell = new HtmlTableCell();
				row.Cells.Add(cell);
				cell.Width = "80";
				cell.NoWrap = true;

				// 防止零除
				if ( ! BLL.MathRule.CheckDecimalEqual( this.m_TotalBuildArea , decimal.Zero) )
					cell.InnerHtml = BLL.StringRule.BuildShowNumberString( partMoney );
				else
					cell.InnerHtml = "----";

				DataRow drUC = this.m_DtUnitApportion.NewRow();
				drUC["UnitCode"]=unitCode;
				drUC["BuildingCode"]=buildingCode;
				drUC["CostCode"]=costCode;
				drUC["FullCode"]=fullCode;
				drUC["ApportionMoney"]=partMoney;
				this.m_DtUnitApportion.Rows.Add(drUC);
			}

//			// 分摊部分 
//			for ( int i= 0;i<iCount; i++)
//			{
//				string costCode = (string)m_FirstLevelCostRows[i]["CostCode"];
//				decimal partMoney =  BLL.MathRule.SumColumn( this.m_PaymentTable, "Money", String.Format( "CostCode='{0}' and BuildingCode='{1}' " , costCode,buildingCode ) ) ;
//				HtmlTableCell cell = new HtmlTableCell();
//				row.Cells.Add(cell);
//				cell.Align="right";
//				cell.Width = "80";
//				cell.NoWrap = true;
//
//				// 防止零除
//				if ( ! BLL.MathRule.CheckDecimalEqual( this.m_TotalBuildArea , decimal.Zero) )
//					cell.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString( partMoney*buildArea/this.m_TotalBuildArea );
//				else
//					cell.InnerHtml = "----";
//			}
		}


		
		private void BuildUnitRow ( string unitCode , string unitName , decimal unitArea )
		{

			HtmlTableRow row = new HtmlTableRow();
			this.tbReport.Rows.Add(row);
			row.BgColor = "Yellow";

			HtmlTableCell cell0 = new  HtmlTableCell();
			cell0.InnerHtml = unitName + " 总计" ;
			cell0.NoWrap = true;
			cell0.Width = "80";
			cell0.Align="center";
			row.Cells.Add(cell0);

			// 平摊部分 , buildingCode = ""
			int iCount = this.m_FirstLevelCostRows.Length;
			for ( int i= 0;i<iCount; i++)
			{
				string costCode = (string)m_FirstLevelCostRows[i]["CostCode"];
				string fullCode = (string)m_FirstLevelCostRows[i]["FullCode"];

				decimal partMoney =  BLL.MathRule.SumColumn(this.m_DtUnitApportion,"ApportionMoney",String.Format( "CostCode='{0}' and unitCode='{1}' " , costCode,unitCode )  ) ;
				HtmlTableCell cell = new HtmlTableCell();
				cell.Width = "80";
				row.Cells.Add(cell);
				cell.Align="right";
				cell.NoWrap = true;

				// 防止零除
				if ( ! BLL.MathRule.CheckDecimalEqual( this.m_TotalBuildArea , decimal.Zero) )
					cell.InnerHtml = BLL.StringRule.BuildShowNumberString( partMoney );
				else
					cell.InnerHtml = "----";

			}

//			// 分摊部分 
//			for ( int i= 0;i<iCount; i++)
//			{
//				string costCode = (string)m_FirstLevelCostRows[i]["CostCode"];
//				decimal partMoney =  BLL.MathRule.SumColumn(this.m_PaymentTable,"Money",String.Format( "CostCode='0' and PBSUnitCode='{1}' " , costCode,unitCode )  ) ;
//				HtmlTableCell cell = new HtmlTableCell();
//				cell.Width = "80";
//				row.Cells.Add(cell);
//				cell.Align="right";
//				cell.NoWrap = true;
//
//				// 防止零除
//				if ( ! BLL.MathRule.CheckDecimalEqual( this.m_TotalBuildArea , decimal.Zero) )
//					cell.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString( partMoney*unitArea/this.m_TotalBuildArea );
//				else
//					cell.InnerHtml = "----";
//			}
		}



		private void IniTableTitle()
		{
			HtmlTableRow row0 = this.tbReport.Rows[0];
			HtmlTableRow row1 = this.tbReport.Rows[1];

			int iCount = this.m_FirstLevelCostRows.Length;
			row0.Cells[1].ColSpan = iCount ;
			row0.Cells[2].ColSpan = iCount ;


			for ( int i=0;i<iCount;i++ )
			{
				HtmlTableCell cell = new HtmlTableCell();
				cell.InnerHtml = (string) this.m_FirstLevelCostRows[i]["CostName"];
				cell.Align = "center";
				cell.NoWrap = true;
				row1.Cells.Add(cell);
			}

//			for ( int i=0;i<iCount;i++ )
//			{
//				HtmlTableCell cell = new HtmlTableCell();
//				cell.Align = "center";
//				cell.NoWrap = true;
//				cell.InnerHtml = (string) this.m_FirstLevelCostRows[i]["CostName"];
//				row1.Cells.Add(cell);
//			}
			
		}

		private decimal GetPBSUnitArea( string buildingCode )
		{
			decimal unitArea = decimal.Zero;
			DataRow[] drs = this.m_BuildingEntity.CurrentTable.Select( String.Format( "BuildingCode='{0}'",buildingCode ) );
			if ( drs.Length>0)
			{
				unitArea = BLL.ConvertRule.ToDecimal(drs[0]["BuildingAreaSum"]);
			}
			return unitArea;
		}

//		private decimal GetBuildArea ( string buildingCode )
//		{
//			decimal buildArea = decimal.Zero;
//			DataRow[] drs = this.m_BuildingEntity.CurrentTable.Select( String.Format( "BuildingCode='{0}'" ,buildingCode ) );
//			if ( drs.Length > 0 )
//				buildArea = (decimal)drs[0]["BuildArea"];
//			return buildArea;
//
//		}


		private decimal GetBuildingMoney( string buildingCode, string fullCode)
		{
			return BLL.MathRule.SumColumn(this.m_DtApportion,"ApportionMoney",String.Format( "BuildingCode='{0}' and FullCode like( '{1}%') and AlloType='B'  "  ,buildingCode,fullCode  ));
		}

		private decimal GetPBSMoney( string pbsUnitCode,string fullCode )
		{
			return BLL.MathRule.SumColumn(this.m_DtApportion,"ApportionMoney",String.Format( "BuildingCode='{0}' and FullCode like( '{1}%') and AlloType='U'  "  ,pbsUnitCode,fullCode  ));
		}

		private decimal GetProjectMoney(  string fullCode)
		{
			return BLL.MathRule.SumColumn(this.m_DtApportion,"ApportionMoney",String.Format( " FullCode like( '{0}%' ) and AlloType='P'  " , fullCode  ));
		}

//		private string GetCostFullCode ( string costCode )
//		{
//			string fullCode = "";
//			DataRow[] drs = this.m_CostEntity.CurrentTable.Select( String.Format( "CostCode='{0}'",costCode ) );
//			if ( drs.Length>0)
//				fullCode = BLL.ConvertRule.ToString(drs[0]["FullCode"]);
//			return fullCode;
//		}



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
			this.btnOk.ServerClick += new System.EventHandler(this.btnOk_ServerClick);

		}
		#endregion

		/// <summary>
		/// 统计
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOk_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				LoadData();
			}
			catch(Exception ex)
			{	
				ApplicationLog.WriteLog(this.ToString(),ex,"统计出错");
				Response.Write(Rms.Web.JavaScript.Alert(true,"统计出错：" + ex.Message));
			}
		}
	}
}
