namespace RmsPM.Web.DesignChange.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using RmsPM.BLL;

	/// <summary>
	///		DesignListControl 的摘要说明。
	/// </summary>
	public partial class DesignListControl : Components.ControlBase
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				OnitControl();
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

		#region 私有属性 -----------------------------------------------
		#endregion


		#region 共公属性 -----------------------------------------------
		#endregion


		#region 私有方法 -----------------------------------------------
		protected override void LoadData()
		{
			BLL.Design_Message dm = new RmsPM.BLL.Design_Message();
			if(TB_Name.Text!="")
			{
				dm.DesignName="%"+TB_Name.Text+"%";
			}
			if(this.TB_Code.Text!="")
			{
				dm.DesignID="%"+TB_Code.Text+"%";
			}
			if(this.SelectBox1.Value!="")
			{
				
				dm.ContractID = "%"+SelectBox1.Value+"%";
			}
			if(this.InputStationUser1.UserCodes!="")
			{
				dm.DesignPerson = this.InputStationUser1.UserCodes;
			}
			dm.ProjectCode=Request["ProjectCode"];
			dm.DesignState=this.State;
			BindDataGrid(dm.GetDesign_Messages());
		}
		protected void BindDataGrid(DataTable dt)
		{
			this.DataGrid1.DataSource=dt;
			this.DataGrid1.DataBind();
			this.gpControl.RowsCount = dt.Rows.Count.ToString();
		}
		/// <summary>
		/// 得到合同值
		/// </summary>
		private void GetValue()
		{
			
		}
		#endregion


		#region 公共方法 -----------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		override public void OnitControl()
		{
			trSearch.Visible=false;
			State=Request["State"]+"";
			LoadData();
		}
		#endregion


		#region 事件方法 -----------------------------------------------
		protected void btnSearch_ServerClick(object sender, System.EventArgs e)
		{
			GetValue();			
			OnitControl();
		}
		#endregion

		protected void gpControl_PageIndexChange(object sender, System.EventArgs e)
		{
			LoadData();		
		}


	}
}
