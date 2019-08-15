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
	/// �ɱ����� ��ժҪ˵����
	/// </summary>
	public partial class CostCheckReport : PageBase
	{

		DataRow[] m_FirstLevelCostRows = null;		//һ���ɱ��ڵ�
		EntityData m_CostEntity = null;				//���ö���
		EntityData m_BuildingEntity = null;			//¥��
		EntityData m_PBSUnit = null;				//��λ����

		DataTable m_DtApportion = null;				//��̯��¥���ķ�������
		DataTable m_DtUnitApportion = new DataTable();			//��̯����λ���̵ķ�������
		

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

			//��������ֶ�
			BLL.SystemRule.UpdateProjectConfigValue(projectCode, BLL.SystemRule.m_CostApportionBuildingAreaField, this.sltBuildingAreaField.Value);

			try
			{
				// ��ʱ��װ��λ���̵�����
				this.m_DtUnitApportion.Columns.Add( "UnitCode",System.Type.GetType("System.String") );
				this.m_DtUnitApportion.Columns.Add( "BuildingCode",System.Type.GetType("System.String") );
				this.m_DtUnitApportion.Columns.Add( "CostCode",System.Type.GetType("System.String") );
				this.m_DtUnitApportion.Columns.Add( "FullCode",System.Type.GetType("System.String") );
				this.m_DtUnitApportion.Columns.Add( "ApportionMoney",System.Type.GetType("System.Decimal") );

				this.m_CostEntity = DAL.EntityDAO.CBSDAO.GetCBSByProject(projectCode);
				this.m_PBSUnit = DAL.EntityDAO.PBSDAO.GetPBSUnitByProject(projectCode);
				this.m_BuildingEntity = DAL.EntityDAO.ProductDAO.GetBuildingNotAreaByProjectCode(projectCode);

				//һ���ɱ���
				int deep = 2;
				this.m_FirstLevelCostRows = this.m_CostEntity.CurrentTable.Select(String.Format("Deep={0}",deep),"SortID,FullCode");
				this.m_DtApportion = BLL.CostRule.BuildingCostApportionAllPayout(projectCode, DateBegin, DateEnd, BLL.CostRule.GetApportionAreaField(projectCode));

				this.m_TotalCost = BLL.MathRule.SumColumn(this.m_DtApportion,"ApportionMoney");
				this.lblProjectTotalCost.Text = BLL.StringRule.BuildShowNumberString(this.m_TotalCost);

				//����ֶ�
				string BuildingAreaFieldName = BLL.CostRule.GetApportionAreaField(projectCode);
				string BuildingAreaFieldDesc = BLL.ProductRule.GetBuildingAreaFieldDesc(BuildingAreaFieldName);
				this.lblBuildingAreaFieldDesc.Text = BuildingAreaFieldDesc;

				m_BuildingEntity.CurrentTable.Columns.Add( "ApportionArea",System.Type.GetType("System.Decimal") );

				foreach ( DataRow dr in m_BuildingEntity.CurrentTable.Rows )
				{
					string buildingCode = BLL.ConvertRule.ToString("BuildingCode");
					dr["ApportionArea"] = dr[BuildingAreaFieldName];
				}

				// ���������
				this.m_TotalBuildArea = BLL.MathRule.SumColumn( this.m_BuildingEntity.CurrentTable, "ApportionArea")  ;
				this.lblProjectTotalArea.Text = BLL.StringRule.BuildShowNumberString(this.m_TotalBuildArea);

				// ��ʼ����ͷ
				IniTableTitle();

//				BuildTableContext( );

				//����¥��
				if (BuildingCodes != "")
				{
					//ɾ����������¥��
					string inBuildingCodes = "'" + BuildingCodes.Replace(",", "','") + "'";
					DataRow[] drsDelete = this.m_BuildingEntity.CurrentTable.Select(string.Format("BuildingCode not in ({0})", inBuildingCodes));

					for(int k=drsDelete.Length-1;k>=0;k--)
					{
						this.m_BuildingEntity.CurrentTable.Rows.Remove(drsDelete[k]);
					}

					//ɾ����¥���ĵ�λ����
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

				// �����е�λ���̵�¥����
				foreach ( DataRow drUnit in this.m_PBSUnit.CurrentTable.Rows)
				{
					string unitCode  = BLL.ConvertRule.ToString( drUnit["PBSUnitCode"]);
					string unitName = BLL.ConvertRule.ToString( drUnit["PBSUnitName"]);
					decimal unitArea = BLL.ConvertRule.ToDecimal(drUnit["BuildingAreaSum"]);
					foreach ( DataRow drBuilding in this.m_BuildingEntity.CurrentTable.Select( String.Format( "PBSUnitCode='{0}'" ,unitCode ) ))
					{
						BuildSingleRow( (string) drBuilding["BuildingCode"],(string)drBuilding["BuildingName"] , (decimal)drBuilding["ApportionArea"] , unitCode,unitArea );
					}

					// ��һ���ܼ�
					BuildUnitRow( unitCode,unitName,unitArea );

					// ��һ������
					HtmlTableRow tr = new HtmlTableRow();
					HtmlTableCell td = new HtmlTableCell();
					tr.Cells.Add(td);
					td.ColSpan = this.m_FirstLevelCostRows.Length * 2 + 1;
					td.Height = "15";
					this.tbReport.Rows.Add(tr);
				}
				
				// û�е�λ���̵�¥��
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

			// ƽ̯���� , buildingCode = ""
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

				// ��ֹ���
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

//			// ��̯���� 
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
//				// ��ֹ���
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
			cell0.InnerHtml = unitName + " �ܼ�" ;
			cell0.NoWrap = true;
			cell0.Width = "80";
			cell0.Align="center";
			row.Cells.Add(cell0);

			// ƽ̯���� , buildingCode = ""
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

				// ��ֹ���
				if ( ! BLL.MathRule.CheckDecimalEqual( this.m_TotalBuildArea , decimal.Zero) )
					cell.InnerHtml = BLL.StringRule.BuildShowNumberString( partMoney );
				else
					cell.InnerHtml = "----";

			}

//			// ��̯���� 
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
//				// ��ֹ���
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
			this.btnOk.ServerClick += new System.EventHandler(this.btnOk_ServerClick);

		}
		#endregion

		/// <summary>
		/// ͳ��
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
				ApplicationLog.WriteLog(this.ToString(),ex,"ͳ�Ƴ���");
				Response.Write(Rms.Web.JavaScript.Alert(true,"ͳ�Ƴ���" + ex.Message));
			}
		}
	}
}
