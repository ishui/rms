namespace RmsPM.Web.CashFlow
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		RptFinIOChartColumn ��ժҪ˵����
	/// </summary>
	public partial class RptFinIOChartColumn : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				BLL.FileIO.CreateDir(Server.MapPath(this.UltraChart1.ChartImagesPath));
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
		///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
		///		�޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		/// <summary>
		/// ʵ�ʡ��ƻ�
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
                if (MonthType.ToString() == "d")
                {
//                        this.UltraChart1.Axis.X.Labels.ItemFormatString = "yy-MM-dd";
                    this.UltraChart1.Axis.X.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
                }
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		/// <summary>
		/// ��ʾͼ��
		/// </summary>
		/// <param name="DataSource"></param>
		public void BindChart(object DataSource)
		{
			try 
			{
				CashFlow.RptFinIOChartLine.SetChartColor(this.UltraChart1, Source);

				this.UltraChart1.Data.DataSource = DataSource;
				this.UltraChart1.Data.DataBind();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾͼ�����" + ex.Message));
			}
		}

	}
}
