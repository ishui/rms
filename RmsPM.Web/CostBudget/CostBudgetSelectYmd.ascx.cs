namespace RmsPM.Web.CostBudget
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	/// CostBudgetSelectYmd ��ժҪ˵����
	/// </summary>
	public partial class CostBudgetSelectYmd : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!this.IsPostBack)
            {
                if (this.spanShowMonthTitle.InnerText == "")
                {
                    this.spanShowMonthTitle.InnerText = "��ʾ��ȼƻ�";
                }
            }

			this.chkShowMonth.Attributes["ClientID"] = this.ClientID;
			this.btnGotoMonth.Attributes["ClientID"] = this.ClientID;
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

		public bool ShowMonth
		{
			get {return this.chkShowMonth.Checked;}
			set
			{
				this.chkShowMonth.Checked = value;
				this.spanMonth.Style["display"] = this.chkShowMonth.Checked?"":"none";
			}
		}

		public bool ShowMonthVisible
		{
			get
			{
				return this.spanShowMonth.Visible;
			}
			set
			{
				this.spanShowMonth.Visible = value;
			}
		}

        /// <summary>
        /// �Ƿ���ʾ��ť
        /// </summary>
        public bool ShowButton
        {
            get
            {
                return this.btnGotoMonth.Visible;
            }
            set
            {
                this.btnGotoMonth.Visible = value;
            }
        }

        public string MonthStart
		{
			get {return this.dtMonthStart.Text;}
			set {this.dtMonthStart.Value = value;}
		}

		public string MonthEnd
		{
			get {return this.dtMonthEnd.Text;}
			set {this.dtMonthEnd.Value = value;}
		}

        public int iMonthStart
        {
            get
            {
                if (this.dtMonthEnd.Text == "")
                {
                    return 0;
                }
                else
                {
                    return BLL.ConvertRule.ToInt(this.dtMonthStart.Text.Replace("-", ""));
                }
            }
        }

        public int iMonthEnd
        {
            get
            {
                if (this.dtMonthEnd.Text == "")
                {
                    return 0;
                }
                else
                {
                    return BLL.ConvertRule.ToInt(this.dtMonthEnd.Text.Replace("-", ""));
                }
            }
        }

        /// <summary>
		/// ���ȵ�����
		/// </summary>
		public int MaxYearsBetween
		{
			get {return BLL.ConvertRule.ToInt(this.txtMaxYearsBetween.Value);}
			set {this.txtMaxYearsBetween.Value = value.ToString();}
		}

        public string MonthTitle
        {
            get { return this.spanShowMonthTitle.InnerText; }
            set { this.spanShowMonthTitle.InnerText = value; }
        }

        public delegate void GotoMonthClickEventHandler(object sender, System.EventArgs e);

		/// <summary>
		/// ѡ�����º�ȷ���¼�
		/// </summary>
		public event EventHandler GotoMonthClick;

		protected void btnGotoMonth_ServerClick(object sender, System.EventArgs e)
		{
			if ((MonthStart != "") && (MonthEnd == "")) 
			{
				MonthEnd = MonthStart;
			}

			if ((MonthEnd != "") && (MonthStart == "")) 
			{
				MonthStart = MonthEnd;
			}

			if (GotoMonthClick != null)
				GotoMonthClick(this, e);
		}

		private string m_OnClientPost = "";

		private void SetOnClientPost(string value)
		{
			m_OnClientPost = value;
		}

		public string OnClientPost
		{
			get {return this.m_OnClientPost;}
			set {SetOnClientPost(value);}
		}

		public string MyOnClientPost
		{
			get
			{
				if (m_OnClientPost == "") 
				{
					return "CostBudgetSelectYmd_Null()";
				}
				else 
				{
					return m_OnClientPost;
				}
			}
		}

	}
}
