namespace RmsPM.Web.PBS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using Rms.ORMap;
	using RmsPM.DAL.QueryStrategy;
	using RmsPM.BLL;
	using Rms.Web;

	/// <summary>
	/// SearchRoomAll 查询
	/// </summary>
	public partial class SearchRoomAll : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DropDownList lstSearchSubmit_unit;
		protected System.Web.UI.WebControls.TextBox txtSearchFacetName;
		protected System.Web.UI.WebControls.TextBox txtSearchSpecialName;
		protected System.Web.UI.HtmlControls.HtmlInputText txtSearchTitle;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSearchSpecial;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSearchFacet;
		protected System.Web.UI.WebControls.TextBox txtSearchSubmitter_name;
		protected System.Web.UI.WebControls.TextBox txtSearchEntrepreneur_name;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSearchSubmitter;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSearchEntrepreneur;
		protected System.Web.UI.WebControls.TextBox txtSearchRelation_code;
		protected System.Web.UI.WebControls.TextBox txtSearchXingzhiName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtSearchXingzhi;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtInfoType;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.divProjectName.InnerText = this.txtProjectName.Value;

			if (this.Visible) 
			{
				string reload = Rms.Web.JavaScript.ScriptStart;
				reload += @"var SearchRoomAllClientID = '" + this.ClientID + "';" + "\n" ;
				reload += Rms.Web.JavaScript.ScriptEnd;
				Response.Write(reload);

				if (!Page.IsPostBack) 
				{
					IniPage();
				}
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

		private void IniPage() 
		{
			try
			{
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载查询条件失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载查询条件失败：" + ex.Message));
			}
		}

		/// <summary>
		/// 设置项目代码，初始化
		/// </summary>
		/// <param name="ProjectCode"></param>
		public void SetProject(string ProjectCode)
		{
			try 
			{
				this.txtProjectCode.Value = ProjectCode;

				PageFacade.LoadPBSTypeSelectAll(sltSearchPBSTypeCode,"","0");
//				PageFacade.LoadModelSelect(this.sltSearchModelCode,"",this.txtProjectCode.Value);
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"加载查询条件失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "加载查询条件失败：" + ex.Message));
			}
		}

		/// <summary>
		/// 添加查询条件
		/// </summary>
		/// <param name="ssb"></param>
		public void AddSearch(Rms.ORMap.StandardQueryStringBuilder sb) 
		{
			string ProjectCode = this.txtProjectCode.Value;
			if (ProjectCode != "")
				sb.AddStrategy(new Strategy(RoomStrategyName.InProjectCode, ProjectCode));

			string JgYear = this.dtJgYear.Text;
			if (JgYear != "")
				sb.AddStrategy(new Strategy(RoomStrategyName.JgYear, JgYear));

			string BuildingName = this.txtSearchBuildingName.Value;
			if (BuildingName != "")
				sb.AddStrategy(new Strategy(RoomStrategyName.InBuildingName, BuildingName, "F"));

			string ChamberName = this.txtSearchChamberName.Value;
			if (ChamberName != "")
				sb.AddStrategy(new Strategy(RoomStrategyName.InChamberName, ChamberName, "F"));

			string RoomName = this.txtSearchRoomName.Value;
			if (RoomName != "")
				sb.AddStrategy(new Strategy(RoomStrategyName.InRoomName, RoomName));

			string PBSTypeCode = this.sltSearchPBSTypeCode.Value;
			if (PBSTypeCode != "")
				sb.AddStrategy(new Strategy(RoomStrategyName.PBSTypeCodeAllChild, PBSTypeCode));

//			string ModelCode = this.sltSearchModelCode.Value;
//			if (ModelCode != "")
//				sb.AddStrategy(new Strategy(RoomStrategyName.ModelCode, ModelCode));

			string OutAspect = this.txtSearchOutAspect.Value;
			if (OutAspect != "")
				sb.AddStrategy(new Strategy(RoomStrategyName.InOutAspect, OutAspect, "F"));

			string InvestType = this.txtSearchInvestType.Value;
			if (InvestType != "")
				sb.AddStrategy(new Strategy(RoomStrategyName.InInvestType, InvestType, "F"));

			string UseType = this.txtSearchUseType.Value;
			if (UseType != "")
				sb.AddStrategy(new Strategy(RoomStrategyName.InUseType, UseType, "F"));

			if ( this.txtIFloorCountBegin.Text != "" || this.txtIFloorCountEnd.Text != "" )
			{
				ArrayList ar = new ArrayList();
				ar.Add((this.txtIFloorCountBegin.Text=="")?"":this.txtIFloorCountBegin.ValueDecimal.ToString());
				ar.Add((this.txtIFloorCountEnd.Text=="")?"":this.txtIFloorCountEnd.ValueDecimal.ToString());
				sb.AddStrategy( new Strategy( RoomStrategyName.IFloorCount, ar));
			}

			string InvState = this.sltSearchInvState.Value;
			if (InvState != "")
				sb.AddStrategy(new Strategy(RoomStrategyName.InInvState, InvState));

			string OutState = this.sltSearchOutState.Value;
			if (OutState != "")
				sb.AddStrategy(new Strategy(RoomStrategyName.OutState, OutState));

			if ( this.dtSearchInDateBegin.Value != "" || this.dtSearchInDateEnd.Value != "" )
			{
				ArrayList ar = new ArrayList();
				ar.Add(this.dtSearchInDateBegin.Value);
				ar.Add(this.dtSearchInDateEnd.Value);
				sb.AddStrategy( new Strategy( RoomStrategyName.InDateRange,ar ));
			}

			if ( this.dtSearchOutDateBegin.Value != "" || this.dtSearchOutDateEnd.Value != "" )
			{
				ArrayList ar = new ArrayList();
				ar.Add(this.dtSearchOutDateBegin.Value);
				ar.Add(this.dtSearchOutDateEnd.Value);
				sb.AddStrategy( new Strategy( RoomStrategyName.OutDateRange,ar ));
			}

			string SalState = this.sltSearchSalState.Value;
			if (SalState != "")
				sb.AddStrategy(new Strategy(RoomStrategyName.SalState, SalState));

			if (this.sltSearchBofangType.Value.Trim() != "")
				sb.AddStrategy(new Strategy(RoomStrategyName.BofangType, this.sltSearchBofangType.Value.Trim()));

			if (this.dtSearchBofangYear.Text.Trim() != "")
				sb.AddStrategy(new Strategy(RoomStrategyName.BofangYear, this.dtSearchBofangYear.Text.Trim()));

			if ( this.txtSearchBofangSnoBegin.Text != "" || this.txtSearchBofangSnoEnd.Text != "" )
			{
				ArrayList ar = new ArrayList();
				ar.Add((this.txtSearchBofangSnoBegin.Text=="")?"":this.txtSearchBofangSnoBegin.ValueDecimal.ToString());
				ar.Add((this.txtSearchBofangSnoEnd.Text=="")?"":this.txtSearchBofangSnoEnd.ValueDecimal.ToString());
				sb.AddStrategy( new Strategy( RoomStrategyName.BofangSnoRange, ar));
			}

		}
	}
}
