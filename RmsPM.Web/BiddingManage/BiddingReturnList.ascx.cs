namespace RmsPM.Web.BiddingManage
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		BiddingReturnList 的摘要说明。
	/// </summary>
	public partial class BiddingReturnList : System.Web.UI.UserControl
	{

		#region --- 私有成员集合 ----------------------------------------------------------------------

		/// <summary>
		/// 
		/// </summary>
		private string _BiddingReturnCode = null;
		/// <summary>
		/// 
		/// </summary>
		private string _BiddingEmitCode = null;
		/// <summary>
		/// 
		/// </summary>
		private string _SupplierCode = null;
		/// <summary>
		/// 
		/// </summary>
		private string _Money = null;
		/// <summary>
		/// 
		/// </summary>
		private string _Remark = null;
		/// <summary>
		/// 
		/// </summary>
		private string _Design = null;
		/// <summary>
		/// 
		/// </summary>
		private string _Project = null;
		/// <summary>
		/// 
		/// </summary>
		private string _Consultant = null;
		/// <summary>
		/// 
		/// </summary>
		private string _OrderCode = null;
		/// <summary>
		/// 
		/// </summary>
		private string _ReturnDate = null;
		/// <summary>
		/// 
		/// </summary>
		private string _State = null;
		/// <summary>
		/// 
		/// </summary>
		private string _Flag = null;

		#endregion -------------------------------------------------------------------------------------

		#region --- 属性集合 ----------------------------------------------------------------------


		/// <summary>
		/// 
		/// </summary>
		public string BiddingReturnCode
		{
			get{return _BiddingReturnCode;}
			set{_BiddingReturnCode = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BiddingEmitCode
		{
			get{return _BiddingEmitCode;}
			set{_BiddingEmitCode = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SupplierCode
		{
			get{return _SupplierCode;}
			set{_SupplierCode = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Money
		{
			get{return _Money;}
			set{_Money = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			get{return _Remark;}
			set{_Remark = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Design
		{
			get{return _Design;}
			set{_Design = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Project
		{
			get{return _Project;}
			set{_Project = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Consultant
		{
			get{return _Consultant;}
			set{_Consultant = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrderCode
		{
			get{return _OrderCode;}
			set{_OrderCode = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReturnDate
		{
			get{return _ReturnDate;}
			set{_ReturnDate = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string State
		{
			get{return _State;}
			set{_State = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Flag
		{
			get{return _Flag;}
			set{_Flag = value;}
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
				BLL.BiddingReturn cBiddingReturn = new BLL.BiddingReturn();

				if(_BiddingReturnCode != null)
					cBiddingReturn.BiddingReturnCode = _BiddingReturnCode;
				if(_BiddingEmitCode != null)
					cBiddingReturn.BiddingEmitCode = _BiddingEmitCode;
				if(_SupplierCode != null)
					cBiddingReturn.SupplierCode = _SupplierCode;
				if(_Money != null)
					cBiddingReturn.Money = _Money;
				if(_Remark != null)
					cBiddingReturn.Remark = _Remark;
				if(_Design != null)
					cBiddingReturn.Design = _Design;
				if(_Project != null)
					cBiddingReturn.Project = _Project;
				if(_Consultant != null)
					cBiddingReturn.Consultant = _Consultant;
				if(_OrderCode != null)
					cBiddingReturn.OrderCode = _OrderCode;
				if(_ReturnDate != null)
					cBiddingReturn.ReturnDate = _ReturnDate;
				if(_State != null)
					cBiddingReturn.State = _State;
				if(_Flag != null)
					cBiddingReturn.Flag = _Flag;

				DataTable dt = cBiddingReturn.GetBiddingReturns();
				this.dgList.DataSource = dt;
				this.dgList.DataBind();
				this.gpControl.RowsCount = dt.Rows.Count.ToString();
				dt.Dispose();
			}
			catch( Exception ex )
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		/// ****************************************************************************
		/// <summary>
		/// 分页事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void gpControl_PageIndexChange(object sender, System.EventArgs e)
		{
			LoadData();
		}
		/// ****************************************************************************
		/// <summary>
		/// 数据帮定显示
		/// </summary>
		/// ****************************************************************************
		public void DataBound()
		{
			LoadData();
		}
	}
}

