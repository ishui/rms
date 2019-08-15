namespace RmsPM.Web.BiddingManage
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		BiddingList1 的摘要说明。
	/// </summary>
	public partial class BiddingList : System.Web.UI.UserControl
	{

		#region --- 私有成员集合 ----------------------------------------------------------------------
                                                                                                                                                                                                      
		/// <summary>
		/// 代码
		/// </summary>
		private string _BiddingCode = null;
		/// <summary>
		/// 类别
		/// </summary>
		private string _Type = null;
		/// <summary>
		/// 名称
		/// </summary>
		private string _Title = null;
		/// <summary>
		/// 内容摘要
		/// </summary>
		private string _Content = null;
		/// <summary>
		/// 附件
		/// </summary>
		private string _Accessory = null;
		/// <summary>
		/// 仪标图纸
		/// </summary>
		private string _ArrangedDate = null;
		/// <summary>
		/// 规范日期
		/// </summary>
        private string _StandardDate = null;
		/// <summary>
		/// 资格预审日期
		/// </summary>
		private string _PrejudicationDate = null;
		/// <summary>
		/// 发标日期
		/// </summary>
		private string _EmitDate = null;
		/// <summary>
		/// 回标日期
		/// </summary>
		private string _ReturnDate = null;
		/// <summary>
		/// 定标日期
		/// </summary>
		private string _ConfirmDate = null;
		/// <summary>
		/// 状态
		/// </summary>
		private string _State = null;
		/// <summary>
		/// 备注
		/// </summary>
		private string _Remark = null;
		/// <summary>
		/// 项目代码
		/// </summary>
		private string _ProjectCode = null;
		/// <summary>
		/// 费用项编号
		/// </summary>
		private string _CostCode = null;
		/// <summary>
		/// 预算设置表编号
		/// </summary>
		private string _CostBudgetSetCode = null;
		/// <summary>
		/// 单位工程类型
		/// </summary>
		private string _PBSType = null;
		/// <summary>
		/// 单位工程代码
		/// </summary>
		private string _PBSCode = null;
		/// <summary>
		/// 预估费用
		/// </summary>
		private string _Money = null;
        /// <summary>
        /// 
        /// </summary>
        private DataTable _DtBidding = null;

		#endregion -------------------------------------------------------------------------------------

		#region --- 属性集合 ----------------------------------------------------------------------
		
		/// <summary>
		/// 代码
		/// </summary>
		public string BiddingCode
		{
			get{return _BiddingCode;}
			set{_BiddingCode = value;}
		}
                                                                                                                                                                                                                                                                 
		/// <summary>
		/// 类别
		/// </summary>
		public string Type
		{
			get{return _Type;}
			set{ _Type = value;}
		}

		/// <summary>
		/// 标段
		/// </summary>
		public string Title
		{
			get{return _Title;}
			set{ _Title = value;}
		}

		/// <summary>
		/// 内容摘要
		/// </summary>
		public string Content
		{
			get{return _Content;}
			set{ _Content = value;}
		}

		/// <summary>
		/// 附件
		/// </summary>
		public string Accessory
		{
			get{return _Accessory;}
			set{ _Accessory = value;}
		}

        /// <summary>
        /// 议标图纸
        /// </summary>
        public string ArrangedDate
        {
            get { return _ArrangedDate; }
            set { _ArrangedDate = value; }
        }
        /// <summary>
        /// 规范日期
        /// </summary>
        public string StandardDate
        {
            get { return _StandardDate; }
            set { _StandardDate = value; }
        }

		/// <summary>
		/// 资格预审日期
		/// </summary>
		public string PrejudicationDate
		{
			get{return _PrejudicationDate;}
			set{ _PrejudicationDate = value;}
		}

		/// <summary>
		/// 发标日期
		/// </summary>
		public string EmitDate
		{
			get{return _EmitDate;}
			set{ _EmitDate = value;}
		}

		/// <summary>
		/// 回标日期
		/// </summary>
		public string ReturnDate
		{
			get{return _ReturnDate;}
			set{ _ReturnDate = value;}
		}

		/// <summary>
		/// 定标日期
		/// </summary>
		public string ConfirmDate
		{
			get{return _ConfirmDate;}
			set{ _ConfirmDate = value;}
		}

		/// <summary>
		/// 状态
		/// </summary>
		public string State
		{
			get{return _State;}
			set{ _State = value;}
		}

		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			get{return _Remark;}
			set{ _Remark = value;}
		}

		/// <summary>
		/// 项目代码
		/// </summary>
		public string ProjectCode
		{
			get{return _ProjectCode;}
			set{ _ProjectCode = value;}
		}

		/// <summary>
		/// 费用项编号
		/// </summary>
		public string CostCode
		{
			get{return _CostCode;}
			set{ _CostCode = value;}
		}

		/// <summary>
		/// 预算设置标编号
		/// </summary>
		public string CostBudgetSetCode
		{
			get{return _CostBudgetSetCode;}
			set{ _CostBudgetSetCode = value;}
		}

		/// <summary>
		/// 单位工程类型
		/// </summary>
		public string PBSType
		{
			get{return _PBSType;}
			set{ _PBSType = value;}
		}

		/// <summary>
		/// 单位工程代码
		/// </summary>
		public string PBSCode
		{
			get{return _PBSCode;}
			set{ _PBSCode = value;}
		}

		/// <summary>
		/// 预估算费用
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
		/// 组件加载
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void Page_Load(object sender, System.EventArgs e)
		{

		}
		/// ****************************************************************************
		/// <summary>
		/// 数据加载
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
        /// 分页事件
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
        /// 数据加载
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
