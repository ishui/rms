namespace RmsPM.Web.CashFlow
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		RptFinIOChartLine 的摘要说明。
	/// </summary>
	public partial class RptFinIOChartLine : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				BLL.FileIO.CreateDir(Server.MapPath(this.UltraChart1.ChartImagesPath));
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		/// <summary>
		/// 来源（计划、实际）
		/// </summary>
		private string m_Source = "";
		public string Source
		{
			get {return m_Source;}
			set
			{
				m_Source = value;
			}
		}

		private int m_Type = 0;
		public int Type
		{
			get {return m_Type;}
			set
			{
				m_Type = value;
				SetType();
			}
		}

        private string m_MonthType = "";
        public string MonthType
        {
            get { return m_MonthType; }
            set
            {
                m_MonthType = value;
                SetType();
            }
        }

        private void SetType()
		{
			try 
			{
                if (Type == 2)
                {
                    this.UltraChart1.Axis.Y.Labels.ItemFormatString = "<DATA_VALUE:0>%";
                    this.UltraChart1.Tooltips.FormatString = "<DATA_VALUE:0>%";

                    this.UltraChart1.Axis.Y.RangeMax = 100;
                    this.UltraChart1.Axis.Y.RangeType = Infragistics.UltraChart.Shared.Styles.AxisRangeType.Custom;

                    this.UltraChart1.SplineChart.NullHandling = Infragistics.UltraChart.Shared.Styles.NullHandling.DontPlot;
                }
                else
                {
                    if (MonthType.ToString() == "d")
                    {
//                        this.UltraChart1.Axis.X.Labels.ItemFormatString = "yy-MM-dd";
                        this.UltraChart1.Axis.X.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
                    }
                }
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		/// <summary>
		/// 设置图表的曲线颜色
		/// </summary>
		public static void SetChartColor(Infragistics.WebUI.UltraWebChart.UltraChart UltraChart1, string Source)
		{
			try 
			{
				//				Infragistics.UltraChart.Resources.Appearance.PaintElement pe = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
				//				this.UltraChart1.ColorModel.Skin.PEs.Add(pe);

				string[] arrSource = Source.Split(",".ToCharArray());
				UltraChart1.ColorModel.Skin.PEs.Clear();

				foreach(string val in arrSource) 
				{
					Infragistics.UltraChart.Resources.Appearance.PaintElement pe = new Infragistics.UltraChart.Resources.Appearance.PaintElement();

					BLL.CashFlowSource source = BLL.CashFlowRule.GetCashFlowSourceById(val);
					if (source != null)
					{
							pe.Fill = source.Color;
					}

					UltraChart1.ColorModel.Skin.PEs.Add( pe);
				}
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		/// <summary>
		/// 显示图表
		/// </summary>
		/// <param name="DataSource"></param>
		public void BindChart(object DataSource)
		{
			try 
			{
				SetChartColor(this.UltraChart1, Source);

				this.UltraChart1.Data.DataSource = DataSource;
				this.UltraChart1.Data.DataBind();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示图表出错：" + ex.Message));
			}
		}

	}
}
