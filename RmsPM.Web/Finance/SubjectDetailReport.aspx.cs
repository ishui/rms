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

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// PaymentList 的摘要说明。
	/// </summary>
	public partial class SubjectDetailReport : PageBase
	{
		private EntityData m_Subject = null;
		private EntityData m_VoucherDetail = null;
		private int m_iYear ;
		private int m_Month ;
		private string m_CurrentMonthLastDate;
		private string m_BeforeMonthLastDate;

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			LoadData();
		}


		private void LoadData()
		{
			string sYear = Request["iYear"] + "";
			string projectCode = Request["ProjectCode"]+"";
			if ( ! Rms.Check.StringCheck.IsInt(sYear))
				this.m_iYear = DateTime.Now.Year;
			else
				this.m_iYear = int.Parse(sYear);

			string sMonth = Request["iMonth"] + "";
			if ( !Rms.Check.StringCheck.IsInt(sMonth))
				this.m_Month = DateTime.Now.Month;
			else
				this.m_Month = DateTime.Now.Month;

			// 计算本月最后一天和前一个月份最后一天
			this.m_CurrentMonthLastDate = (new DateTime(this.m_iYear,this.m_Month,1)).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
			this.m_BeforeMonthLastDate = (new DateTime(this.m_iYear,this.m_Month,1)).AddDays(-1).ToString("yyyy-MM-dd");

			this.lblProjectName.Text = BLL.ProjectRule.GetProjectName( projectCode );
			this.lblYear.Text = this.m_iYear.ToString();
			this.lblMonth.Text = this.m_Month.ToString();

			try
			{
				this.m_Subject = DAL.EntityDAO.SubjectDAO.GetAllSubject();
				VoucherDetailStrategyBuilder sb = new VoucherDetailStrategyBuilder();
				sb.AddStrategy( new Strategy (VoucherDetailStrategyName.ProjectCode ,projectCode ));
				string sql = sb.BuildQueryViewString();
				QueryAgent qa = new QueryAgent();
				this.m_VoucherDetail = qa.FillEntityData( "VoucherDetail",sql);
				qa.Dispose(); 


				DataRow[] drsFirst = this.m_Subject.CurrentTable.Select( "layer=1" );
				int iCount = drsFirst.Length;
				for (int i=0; i<iCount;i++)
				{
					DataRow dr = drsFirst[i];
					BuildRow( dr);
				}

				this.m_Subject.Dispose();
				this.m_VoucherDetail.Dispose();

			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void BuildChildRows( string subjectCode , int layer )
		{
			DataRow[] childDrs = this.m_Subject.CurrentTable.Select( String.Format( " SubjectCode like '{0}%' and layer={1} " ,subjectCode, (layer+1)) ,"SubjectCode" );

			int iCount = childDrs.Length;
			for ( int i=0; i<iCount; i++)
			{
				DataRow dr = childDrs[i] ;
				BuildRow(dr);
			}
		}

		private void BuildRow ( DataRow dr  )
		{
			string subjectCode = (string)dr["SubjectCode"];
			string subjectName = (string)dr["SubjectName"];
			int layer = (int) dr["Layer"];

			HtmlTableRow htmlRow = new HtmlTableRow();
			this.tbReport.Rows.Add(htmlRow);

			HtmlTableCell cellCode = new HtmlTableCell();
			htmlRow.Cells.Add(cellCode);
			cellCode.InnerHtml = subjectCode;

			HtmlTableCell cellName = new HtmlTableCell();
			string sTemp = "";
			for ( int i =0; i<layer-1; i++ )
				sTemp += @"";
			cellName.InnerHtml = sTemp + subjectName;
			htmlRow.Cells.Add(cellName);

			HtmlTableCell cell1 = new HtmlTableCell();
			htmlRow.Cells.Add(cell1);
			cell1.Align = "right";

			HtmlTableCell cell2 = new HtmlTableCell();
			htmlRow.Cells.Add(cell2);
			cell2.Align = "right";

			HtmlTableCell cell3 = new HtmlTableCell();
			htmlRow.Cells.Add(cell3);
			cell3.Align = "right";

			HtmlTableCell cell4 = new HtmlTableCell();
			htmlRow.Cells.Add(cell4);
			cell4.Align = "right";

			decimal currentDebit = BLL.MathRule.SumColumn( this.m_VoucherDetail.CurrentTable,"DebitMoney",
					String.Format(" SubjectCode like '{0}%' and MakeDate <= '{1}' ",subjectCode,this.m_CurrentMonthLastDate ) );
			
			decimal currentCrebit = BLL.MathRule.SumColumn( this.m_VoucherDetail.CurrentTable,"CrebitMoney",
				String.Format(" SubjectCode like '{0}%' and MakeDate <= '{1}' ",subjectCode,this.m_CurrentMonthLastDate ) );

			decimal beforeDebit = BLL.MathRule.SumColumn( this.m_VoucherDetail.CurrentTable,"DebitMoney",
				String.Format(" SubjectCode like '{0}%' and MakeDate <= '{1}' ",subjectCode,this.m_BeforeMonthLastDate ) );
			
			decimal beforeCrebit = BLL.MathRule.SumColumn( this.m_VoucherDetail.CurrentTable,"CrebitMoney",
				String.Format(" SubjectCode like '{0}%' and MakeDate <= '{1}' ",subjectCode,this.m_BeforeMonthLastDate ) );

			cell1.InnerHtml = BLL.StringRule.BuildShowNumberString( beforeDebit);
			cell2.InnerHtml = BLL.StringRule.BuildShowNumberString( beforeCrebit);
			cell3.InnerHtml = BLL.StringRule.BuildShowNumberString( currentDebit);
			cell4.InnerHtml = BLL.StringRule.BuildShowNumberString( currentCrebit);

			BuildChildRows(subjectCode,layer);

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
