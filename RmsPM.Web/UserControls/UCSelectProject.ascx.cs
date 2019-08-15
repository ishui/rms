namespace RmsPM.Web.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.ORMap;

	/// <summary>
	///		UCSelectProject 的摘要说明。
	/// </summary>
	public partial class UCSelectProject : System.Web.UI.UserControl
	{

		/// <summary>
		/// 是否触发服务器端事件
		/// </summary>
		public bool ProjectAutoPostBack
		{
			get
			{
				return this.ddlSelProject.AutoPostBack;
			}
			set
			{
				this.ddlSelProject.AutoPostBack = value;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				this.ddlSelProject.Attributes.Add("onchange",this.ClientID+"ChangeProjectEvent(this.value);");

				if(!this.IsPostBack)
				{
					// 在此处放置用户代码以初始化页面
					EntityData entity = new EntityData("Project");
					DataTable dt = entity.CurrentTable;
			
					if(this.access=="CanAccess")
					{
						if ( Session["User"] != null )
						{
							User user = (User)Session["User"];
							dt = user.m_EntityDataAccessProject.CurrentTable.Copy();
						}					
					}
					else
						dt = DAL.EntityDAO.ProjectDAO.GetAllProject().CurrentTable.Copy();

					DataRow dr = dt.NewRow();
					dr["projectName"] = "--请选择--";
					dr["projectCode"] = "";				
					dt.Rows.Add(dr);
					DataView dv = dt.DefaultView;
					dv.Sort = "projectCode";
			
					this.ddlSelProject.DataSource = dv;
					this.ddlSelProject.DataTextField = "projectName";
					this.ddlSelProject.DataValueField = "projectCode";
					this.ddlSelProject.DataBind();
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		public string Value
		{
			get
			{
				return this.ddlSelProject.SelectedValue;
			}
			set
			{
				this.ddlSelProject.SelectedValue = value;
			}
		}

		private string access = "CanAccess"; // 有权限的项目 all 全部项目
		public string Access
		{
			set
			{
				this.access = value;
			}
		}

		protected string changeTarget="";
		public string ChangeTaget
		{
			set
			{
				string[] artmp = value.Split(',');
				foreach(string tmp in artmp)
				{
					if(tmp.Length<1) continue;
					this.changeTarget += tmp+"ProjectCodeOnChange(objValue);\n";
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

		public event EventHandler SelectProject;
		protected void ddlSelProject_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.SelectProject(this,EventArgs.Empty);		
		}
	}
}
