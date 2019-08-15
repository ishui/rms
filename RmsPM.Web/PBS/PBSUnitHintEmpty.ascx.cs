namespace RmsPM.Web.PBS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		PBSUnitHintEmpty 的摘要说明。
	/// </summary>
	public partial class PBSUnitHintEmpty : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
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

		public void SetProject(string ProjectCode)
		{
			try 
			{
				DataTable tb = BLL.PBSRule.GetBuildingNoPBSUnit(ProjectCode);
				if (tb.Rows.Count > 0) 
				{
					this.lblHint.Text = BLL.ConvertRule.Concat(tb, "BuildingName", ",");
					this.tbHint.Style["display"] = "";
				}
				else 
				{
					this.tbHint.Style["display"] = "none";
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}
	}
}
