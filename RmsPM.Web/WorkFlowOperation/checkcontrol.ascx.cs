//===========================================================================
// 此文件是作为 ASP.NET 2.0 Web 项目转换的一部分修改的。
// 类名已更改，且类已修改为从文件“App_Code\Migrated\workflowoperation\Stub_checkcontrol_ascx_cs.cs”的抽象基类 
// 继承。
// 在运行时，此项允许您的 Web 应用程序中的其他类使用该抽象基类绑定和访问 
// 代码隐藏页。
// 关联的内容页“workflowoperation\checkcontrol.ascx”也已修改，以引用新的类名。
// 有关此代码模式的更多信息，请参考 http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
namespace RmsPM.Web.WorkFlowOperation
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using RmsPM.Web.WorkFlowControl;

	/// <summary>
	///		CheckControl 的摘要说明。
	/// </summary>
	public partial class Migrated_CheckControl : CheckControl
	{

		#region --- 私有成员集合 ---
		/// <summary>
		/// 模块状态
		/// </summary>
		private ModuleState _State = ModuleState.Unbeknown;


		#endregion --- 私有成员集合 ---

		#region --- 属性集合 ---

		/// <summary>
		/// 业务代码
		/// </summary>
//		public string Result
//		{
		override public string Result
		{
			get
			{
				if (rdoContrachCheck.SelectedIndex == -1)
				{
					return "Unknow";
				}
				else
				{
					return rdoContrachCheck.SelectedItem.Value;
				}
			}
			set
			{
				foreach ( ListItem item in rdoContrachCheck.Items )
				{
					if ( item.Value == value )
					{
						item.Selected = true;
						break;
					}
				}
			}
		}

		/// <summary>
		/// 模块状态
		/// </summary>
//		public ModuleState State
//		{
		override public ModuleState State
		{
			get
			{
				if ( _State == ModuleState.Unbeknown )
				{
					if(this.ViewState["_State"] != null)
						return (ModuleState)this.ViewState["_State"];
					return ModuleState.Unbeknown;
				}
				return _State;
			}
			set
			{
				_State = value;
				this.ViewState["_State"] = value;
			}
		}
		#endregion --- 属性集合 ---


		#region --- 公共方法 ---

		/// <summary>
		/// 控件初始化
		/// </summary>
//		public void InitControl()
		override public void InitControl()
		{
			try
			{
				this.Visible = true;

				switch ( this.State )
				{
					case ModuleState.Sightless://不可见的
						this.Visible = false;
						break;

					case  ModuleState.Operable://可操作的
						LoadData();
						EyeableDiv.Visible = false;
						OperableDiv.Visible = true;
						break;

					case ModuleState.Eyeable://可见的
						LoadData();
						OperableDiv.Visible = false;
						EyeableDiv.Visible = false;
						break;

					case  ModuleState.Begin://不可见的
						this.Visible = false;
						break;

					case ModuleState.End://不可见的
						this.Visible = false;
						break;

					default:
						this.Visible = false;
						break;
				}


			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		/// <summary>
		/// 装载控件数据
		/// </summary>
//		public void LoadData()
		override public void LoadData()
		{
			try
			{
				if ( this.Result == "Unknow" )
				{
					lblResult.Text = "未知";
				}
				else
				{
					lblResult.Text = rdoContrachCheck.SelectedItem.Text;
				}

			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		#endregion --- 公共方法 ---

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
	}
}
