namespace RmsPM.Web.BiddingManage
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		BiddingList1 ��ժҪ˵����
	/// </summary>
	public partial class BiddingList : System.Web.UI.UserControl
	{

		#region --- ˽�г�Ա���� ----------------------------------------------------------------------
                                                                                                                                                                                                      
		/// <summary>
		/// ����
		/// </summary>
		private string _BiddingCode = null;
		/// <summary>
		/// ���
		/// </summary>
		private string _Type = null;
		/// <summary>
		/// ����
		/// </summary>
		private string _Title = null;
		/// <summary>
		/// ����ժҪ
		/// </summary>
		private string _Content = null;
		/// <summary>
		/// ����
		/// </summary>
		private string _Accessory = null;
		/// <summary>
		/// �Ǳ�ͼֽ
		/// </summary>
		private string _ArrangedDate = null;
		/// <summary>
		/// �淶����
		/// </summary>
        private string _StandardDate = null;
		/// <summary>
		/// �ʸ�Ԥ������
		/// </summary>
		private string _PrejudicationDate = null;
		/// <summary>
		/// ��������
		/// </summary>
		private string _EmitDate = null;
		/// <summary>
		/// �ر�����
		/// </summary>
		private string _ReturnDate = null;
		/// <summary>
		/// ��������
		/// </summary>
		private string _ConfirmDate = null;
		/// <summary>
		/// ״̬
		/// </summary>
		private string _State = null;
		/// <summary>
		/// ��ע
		/// </summary>
		private string _Remark = null;
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		private string _ProjectCode = null;
		/// <summary>
		/// ��������
		/// </summary>
		private string _CostCode = null;
		/// <summary>
		/// Ԥ�����ñ���
		/// </summary>
		private string _CostBudgetSetCode = null;
		/// <summary>
		/// ��λ��������
		/// </summary>
		private string _PBSType = null;
		/// <summary>
		/// ��λ���̴���
		/// </summary>
		private string _PBSCode = null;
		/// <summary>
		/// Ԥ������
		/// </summary>
		private string _Money = null;
        /// <summary>
        /// 
        /// </summary>
        private DataTable _DtBidding = null;

		#endregion -------------------------------------------------------------------------------------

		#region --- ���Լ��� ----------------------------------------------------------------------
		
		/// <summary>
		/// ����
		/// </summary>
		public string BiddingCode
		{
			get{return _BiddingCode;}
			set{_BiddingCode = value;}
		}
                                                                                                                                                                                                                                                                 
		/// <summary>
		/// ���
		/// </summary>
		public string Type
		{
			get{return _Type;}
			set{ _Type = value;}
		}

		/// <summary>
		/// ���
		/// </summary>
		public string Title
		{
			get{return _Title;}
			set{ _Title = value;}
		}

		/// <summary>
		/// ����ժҪ
		/// </summary>
		public string Content
		{
			get{return _Content;}
			set{ _Content = value;}
		}

		/// <summary>
		/// ����
		/// </summary>
		public string Accessory
		{
			get{return _Accessory;}
			set{ _Accessory = value;}
		}

        /// <summary>
        /// ���ͼֽ
        /// </summary>
        public string ArrangedDate
        {
            get { return _ArrangedDate; }
            set { _ArrangedDate = value; }
        }
        /// <summary>
        /// �淶����
        /// </summary>
        public string StandardDate
        {
            get { return _StandardDate; }
            set { _StandardDate = value; }
        }

		/// <summary>
		/// �ʸ�Ԥ������
		/// </summary>
		public string PrejudicationDate
		{
			get{return _PrejudicationDate;}
			set{ _PrejudicationDate = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public string EmitDate
		{
			get{return _EmitDate;}
			set{ _EmitDate = value;}
		}

		/// <summary>
		/// �ر�����
		/// </summary>
		public string ReturnDate
		{
			get{return _ReturnDate;}
			set{ _ReturnDate = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public string ConfirmDate
		{
			get{return _ConfirmDate;}
			set{ _ConfirmDate = value;}
		}

		/// <summary>
		/// ״̬
		/// </summary>
		public string State
		{
			get{return _State;}
			set{ _State = value;}
		}

		/// <summary>
		/// ��ע
		/// </summary>
		public string Remark
		{
			get{return _Remark;}
			set{ _Remark = value;}
		}

		/// <summary>
		/// ��Ŀ����
		/// </summary>
		public string ProjectCode
		{
			get{return _ProjectCode;}
			set{ _ProjectCode = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public string CostCode
		{
			get{return _CostCode;}
			set{ _CostCode = value;}
		}

		/// <summary>
		/// Ԥ�����ñ���
		/// </summary>
		public string CostBudgetSetCode
		{
			get{return _CostBudgetSetCode;}
			set{ _CostBudgetSetCode = value;}
		}

		/// <summary>
		/// ��λ��������
		/// </summary>
		public string PBSType
		{
			get{return _PBSType;}
			set{ _PBSType = value;}
		}

		/// <summary>
		/// ��λ���̴���
		/// </summary>
		public string PBSCode
		{
			get{return _PBSCode;}
			set{ _PBSCode = value;}
		}

		/// <summary>
		/// Ԥ�������
		/// </summary>
		public string Money
		{
			get{return _Money;}
			set{ _Money = value;}
		}
        /// <summary>
        /// 
        /// </summary>
        public DataTable DtBidding
        {
            get
            {
                return _DtBidding;
            }
            set
            {
                this._DtBidding = value;
            }
        }

		#endregion -------------------------------------------------------------------------------------

		/// ****************************************************************************
		/// <summary>
		/// �������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void Page_Load(object sender, System.EventArgs e)
		{

		}
		/// ****************************************************************************
		/// <summary>
		/// ���ݼ���
		/// </summary>
		/// ****************************************************************************
		private void LoadData()
		{
			try
			{

                if (this.DtBidding != null)
                {
                    this.dgList.DataSource = DtBidding;
                    this.dgList.DataBind();
                    this.gpControl.RowsCount = DtBidding.Rows.Count.ToString();
                   
                }
                else
                {
                    BLL.BiddingManage bm = new BLL.BiddingManage();

                    bm.BiddingCode = _BiddingCode;
                    bm.Type = _Type;
                    bm.Title = _Title;
                    bm.Content = _Content;
                    bm.Accessory = _Accessory;
                    bm.ArrangedDate = _ArrangedDate;
                    bm.StandardDate = _StandardDate;
                    bm.PrejudicationDate = _PrejudicationDate;
                    bm.EmitDate = _EmitDate;
                    bm.ReturnDate = _ReturnDate;
                    bm.ConfirmDate = _ConfirmDate;
                    bm.State = _State;
                    bm.Remark = _Remark;
                    bm.ProjectCode = _ProjectCode;
                    bm.CostCode = _CostCode;
                    bm.CostBudgetSetCode = _CostBudgetSetCode;
                    bm.PBSType = _PBSType;
                    bm.PBSCode = _PBSCode;
                    bm.Money = _Money;

                    DataTable dt = bm.GetBiddings();
                    this.dgList.DataSource = dt;
                    this.dgList.DataBind();
                    this.gpControl.RowsCount = dt.Rows.Count.ToString();
                    dt.Dispose();
                }
			}
			catch( Exception ex )
			{
                throw ex;
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
        /// ��ҳ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		protected void gpControl_PageIndexChange(object sender, System.EventArgs e)
		{
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
		}
        /// <summary>
        /// ���ݼ���
        /// </summary>
		public void DataBound()
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
		}
	}
}
