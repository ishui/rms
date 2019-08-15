namespace RmsPM.Web.PBS
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.ORMap;
	using Rms.Web;
	using RmsPM.DAL.QueryStrategy;

	/// <summary>
	/// SelectBuildingCtrl 的摘要说明。
	/// </summary>
	public partial class SelectBuildingCtrl : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (this.Visible) 
			{
				string reload = Rms.Web.JavaScript.ScriptStart;
				reload += @"var BuildingCtrlClientID = '" + this.ClientID + "';" + "\n" ;
				reload += Rms.Web.JavaScript.ScriptEnd;
				Response.Write(reload);
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

		private string m_Multi = "";

		public string Multi 
		{
			get{return m_Multi;}
			set
			{
				m_Multi = value;
				this.txtMulti.Value = value;
			}
		}

		public void SetProject(string ProjectCode)
		{
			try 
			{
				if (this.txtProjectCode.Value != ProjectCode) 
				{
					this.txtProjectCode.Value = ProjectCode;
					LoadDataGrid();
				}
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化页面失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
			}
		}

		private void LoadDataGrid() 
		{
			try 
			{
				BuildingStrategyBuilder sb = new BuildingStrategyBuilder();

				string ProjectCode = this.txtProjectCode.Value;
				if (ProjectCode != "")
					sb.AddStrategy( new Strategy( BuildingStrategyName.ProjectCode, ProjectCode));
				sb.AddStrategy( new Strategy( BuildingStrategyName.IsArea, "2"));

				sb.AddOrder("BuildingName", true);

				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "Building",sql );
				qa.Dispose();

				//选择多个
				if (this.m_Multi == "1") 
				{
					DataRow dr = entity.CurrentTable.NewRow();
					dr["BuildingCode"] = -1;
					dr["BuildingName"] = "多选楼栋...";
					entity.CurrentTable.Rows.InsertAt(dr, 0);
				}

				dgList.DataSource = entity;
				dgList.DataBind();
				entity.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

	}
}
