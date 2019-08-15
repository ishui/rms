namespace RmsPM.Web
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using RmsPM.BLL;
	using Rms.Web;
	//using TestDB.

	/// <summary>
	///		Control_CreatStyle 的摘要说明。
	/// </summary>
	public partial class Control_CreatStyle : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				DefaultSet();
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
		private void GetStationList()
		{
		}
		#region 初始信息显示
		private void DefaultSet()
		{
			BindDDL_StyleList();
			BindDDL_StationList();
			GetStyelByID();
		}
		private void GetStyelByID()
		{
			BindDDL_StyleList();
			DataTable Style = StyleOperation.GetStationConfig(this.DDL_StationList.SelectedValue.ToString(),"-1");
			DDL_StyleList.SelectedIndex = DDL_StyleList.Items.IndexOf(DDL_StyleList.Items.FindByValue(Style.Rows[0]["StyleID"].ToString()));
		}
		private void BindDDL_StyleList()
		{
			DataSet ds = StyleOperation.GetStyleList();
			DDL_StyleList.DataSource = ds;
			DDL_StyleList.DataTextField="StyleName";
			DDL_StyleList.DataValueField="StyleID";
			DDL_StyleList.DataBind();
		}
		private void BindDDL_StationList()
		{
			DataSet ds;
			ds = StyleOperation.GetStationList();
			DDL_StationList.DataSource=StyleOperation.GetStationList();
			DDL_StationList.DataTextField="StationName";
			DDL_StationList.DataValueField="StationCode";
			DDL_StationList.DataBind();
		}
		
		#endregion
		#region 加入,删除信息
		protected void Bt_Sumit_Click(object sender, System.EventArgs e)
		{
			try
			{
				StyleOperation.SetStationStyle(this.DDL_StationList.SelectedValue,this.DDL_StyleList.SelectedValue);
				Response.Write(JavaScript.Alert(true,"更新成攻"));				
			}
			catch(Exception ex)
			{
				Response.Write(JavaScript.Alert(true,ex.Message));
			}
		}
		protected void DDL_StyleList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Bt_Sumit.Enabled=true;
			ShowStyleInfo();
		}
	
		#endregion
		#region 显示样式框内容	
		private void ShowStyleInfo()
		{
			//this.DDL_StyleList.SelectedValue
			DataSet Leftds = StyleOperation.GetLeftSytleByID(this.DDL_StyleList.SelectedValue);
			DataSet Rightds = StyleOperation.GetRightSytleByID(this.DDL_StyleList.SelectedValue);		
			LB_LeftBind(Leftds);
			LB_RightBind(Rightds);
		}
		private void LB_LeftBind(DataSet ds)
		{
			LB_Left.DataSource = ds;
			LB_Left.DataTextField = "ControlTitle";
			LB_Left.DataValueField = "ControlID";
			LB_Left.DataBind();
		}
		private void LB_RightBind(DataSet ds)
		{
			LB_Right.DataSource = ds;
			LB_Right.DataTextField = "ControlTitle";
			LB_Right.DataValueField = "ControlID";
			LB_Right.DataBind();
		}
		#endregion	

		protected void DDL_StationList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindDDL_StyleList();
			GetStyelByID();
		}
		//private void 
	}
}
