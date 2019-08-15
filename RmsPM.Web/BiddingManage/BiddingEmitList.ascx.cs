namespace RmsPM.Web.BiddingManage
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		BiddingEmitList 的摘要说明。
	/// </summary>
	public partial class BiddingEmitList : System.Web.UI.UserControl
	{

		#region --- 私有成员集合 ----------------------------------------------------------------------

		/// <summary>
		/// 
		/// </summary>
		private string _BiddingEmitCode = null;
		/// <summary>
		/// 
		/// </summary>
		private string _BiddingCode = null;
		/// <summary>
		/// 
		/// </summary>
		private string _EmitNumber = null;
		/// <summary>
		/// 
		/// </summary>
		private string _EmitDate = null;
		/// <summary>
		/// 
		/// </summary>
		private string _EndDate = null;
		/// <summary>
		/// 
		/// </summary>
		private string _PrejudicationDate = null;
		/// <summary>
		/// 
		/// </summary>
		private string _CreatUser = null;
		/// <summary>
		/// 
		/// </summary>
		private string _CreatDate = null;

		#endregion -------------------------------------------------------------------------------------

		#region --- 属性集合 ----------------------------------------------------------------------


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
		public string BiddingCode
		{
			get{return _BiddingCode;}
			set{_BiddingCode = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmitNumber
		{
			get{return _EmitNumber;}
			set{_EmitNumber = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmitDate
		{
			get{return _EmitDate;}
			set{_EmitDate = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EndDate
		{
			get{return _EndDate;}
			set{_EndDate = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PrejudicationDate
		{
			get{return _PrejudicationDate;}
			set{_PrejudicationDate = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CreatUser
		{
			get{return _CreatUser;}
			set{_CreatUser = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CreatDate
		{
			get{return _CreatDate;}
			set{_CreatDate = value;}
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
				BLL.BiddingEmit cBiddingEmit = new BLL.BiddingEmit();

				if(_BiddingEmitCode != null)
					cBiddingEmit.BiddingEmitCode = _BiddingEmitCode;
				if(_BiddingCode != null)
					cBiddingEmit.BiddingCode = _BiddingCode;
				if(_EmitNumber != null)
					cBiddingEmit.EmitNumber = _EmitNumber;
				if(_EmitDate != null)
					cBiddingEmit.EmitDate = _EmitDate;
				if(_EndDate != null)
					cBiddingEmit.EndDate = _EndDate;
				if(_PrejudicationDate != null)
					cBiddingEmit.PrejudicationDate = _PrejudicationDate;
				if(_CreatUser != null)
					cBiddingEmit.CreatUser = _CreatUser;
				if(_CreatDate != null)
					cBiddingEmit.CreatDate = _CreatDate;

				DataTable dt = cBiddingEmit.GetBiddingEmits();
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
		private void gpControl_PageIndexChange(object sender, System.EventArgs e)
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

