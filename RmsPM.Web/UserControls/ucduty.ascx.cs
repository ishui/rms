//===========================================================================
// 此文件是作为 ASP.NET 2.0 Web 项目转换的一部分修改的。
// 类名已更改，且类已修改为从文件“App_Code\Migrated\usercontrols\Stub_ucduty_ascx_cs.cs”的抽象基类 
// 继承。
// 在运行时，此项允许您的 Web 应用程序中的其他类使用该抽象基类绑定和访问 
// 代码隐藏页。
// 关联的内容页“usercontrols\ucduty.ascx”也已修改，以引用新的类名。
// 有关此代码模式的更多信息，请参考 http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
namespace RmsPM.Web.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using Rms.ORMap;
	using RmsPM.DAL.EntityDAO;

	/// <summary>
	///		UCDuty 的摘要说明。
	/// </summary>
	public partial class Migrated_UCDuty : UCDuty
	{
		protected string hClientID;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			try
			{
				hClientID = this.ID;				

				if(!this.IsPostBack)
				{					
					User user = (User)Session["User"];
					EntityData entity = DAL.EntityDAO.SystemManageDAO.GetUnitByUserCode(user.UserCode);
					if(entity.HasRecord())
					{
						DataTable dt = entity.CurrentTable;
						for(int i=0;i<dt.Rows.Count;i++)
						{
							ListItem li = new ListItem(dt.Rows[i]["UnitName"].ToString(),dt.Rows[i]["UnitCode"].ToString());
							this.SelectDuty.Items.Add(li);
						}
					}					
				}
				else
				{
					// 取得ddl中的数据
					this.SelectDuty.Items.Clear();
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
		protected string ctrlPath = "../UserControl/";
//		public string CtrlPath
//		{
		override public string CtrlPath
		{
			set
			{
				this.ctrlPath = value;
			}
		}

//		public string Value
//		{
		override public string Value
		{
			get
			{
				return Request.Form[this.SelectDuty.UniqueID];
			}
			set
			{
				try
				{
					this.SelectDuty.Items.Clear();

					User user = (User)Session["User"];
					EntityData entity = DAL.EntityDAO.SystemManageDAO.GetUnitByUserCode(user.UserCode);
					if(entity.HasRecord())
					{
						DataTable dt = entity.CurrentTable;
						for(int i=0;i<dt.Rows.Count;i++)
						{
							this.SelectDuty.SelectedIndex = -1;
							ListItem li = new ListItem(dt.Rows[i]["UnitName"].ToString(),dt.Rows[i]["UnitCode"].ToString());
							if(dt.Rows[i]["UnitCode"].ToString()==value) li.Selected = true;
							this.SelectDuty.Items.Add(li);														
						}
					}
				}
				catch(Exception ex)
				{
					ApplicationLog.WriteLog(this.ToString(),ex,"");
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
	}
}
