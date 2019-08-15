namespace RmsPM.Web.BiddingManage
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		BiddingPrejudicationList 的摘要说明。
	/// </summary>
	public partial class BiddingPrejudicationList : System.Web.UI.UserControl
	{

		#region --- 私有成员集合 ----------------------------------------------------------------------

		/// <summary>
		/// 
		/// </summary>
		private string _BiddingPrejudicationCode = null;
		/// <summary>
		/// 
		/// </summary>
		private string _BiddingCode = null;
		/// <summary>
		/// 
		/// </summary>
		private string _WorkConfine = null;
		/// <summary>
		/// 
		/// </summary>
		private string _Number = null;
		/// <summary>
		/// 
		/// </summary>
		private string _Remark = null;
		/// <summary>
		/// 
		/// </summary>
		private string _UserCode = null;
		/// <summary>
		/// 
		/// </summary>
		private string _CreateDate = null;
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
		public string BiddingPrejudicationCode
		{
			get{return _BiddingPrejudicationCode;}
			set{_BiddingPrejudicationCode = value;}
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
		public string WorkConfine
		{
			get{return _WorkConfine;}
			set{_WorkConfine = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Number
		{
			get{return _Number;}
			set{_Number = value;}
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
		public string UserCode
		{
			get{return _UserCode;}
			set{_UserCode = value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CreateDate
		{
			get{return _CreateDate;}
			set{_CreateDate = value;}
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
				BLL.BiddingPrejudication cBiddingPrejudication = new BLL.BiddingPrejudication();

				if(_BiddingPrejudicationCode != null)
					cBiddingPrejudication.BiddingPrejudicationCode = _BiddingPrejudicationCode;
				if(_BiddingCode != null)
					cBiddingPrejudication.BiddingCode = _BiddingCode;
				if(_WorkConfine != null)
					cBiddingPrejudication.WorkConfine = _WorkConfine;
				if(_Number != null)
					cBiddingPrejudication.Number = _Number;
				if(_Remark != null)
					cBiddingPrejudication.Remark = _Remark;
				if(_UserCode != null)
					cBiddingPrejudication.UserCode = _UserCode;
				if(_CreateDate != null)
					cBiddingPrejudication.CreateDate = _CreateDate;
				if(_State != null)
					cBiddingPrejudication.State = _State;
				if(_Flag != null)
					cBiddingPrejudication.Flag = _Flag;

				DataTable dt = cBiddingPrejudication.GetBiddingPrejudications();
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

