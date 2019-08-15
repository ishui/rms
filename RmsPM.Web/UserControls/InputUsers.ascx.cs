namespace RmsPM.Web.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		InputUsers 的摘要说明。
	/// </summary>
	public partial class InputUsers : System.Web.UI.UserControl
	{


		public enum ControlState
		{
			View,
			Input
		}

		public ControlState State
		{
			get
			{
				if ( btnSelectUsers.Visible == false )
				{
					return ControlState.Input;
				}
				else
				{
					return ControlState.View;
				}
			}

			set
			{
				if ( value == ControlState.View )
				{
					btnSelectUsers.Visible = false;
					this.MustInput = false;
				}
				else
				{
					btnSelectUsers.Visible = false;
				}
			}
		}

		public Boolean MustInput
		{
			get { return td_MustInput.Visible; }
			set { td_MustInput.Visible = value; }
		}
	
		public string UserCodes
		{
			get { return txtUsers.Value; }
			set { txtUsers.Value = BLL.StringRule.CutRepeat(value); }
		}

		public string StationCodes
		{
			get { return txtUserStations.Value; }
			set { txtUserStations.Value = BLL.StringRule.CutRepeat(value); }
		}

		public string Value
		{
			get { return txtUsers.Value + ":" + txtUserStations.Value; }
			set 
			{
				string delimStr = ":";
				char [] delimiter = delimStr.ToCharArray();
				string [] codes = value.Split(delimiter,2);

				txtUsers.Value = BLL.StringRule.CutRepeat(codes[0]);
				if  ( codes.Length < 2)
				{
					txtUserStations.Value = "";
				}
				else
				{
					txtUserStations.Value = BLL.StringRule.CutRepeat(codes[1]);
				}

			}
		}

		public string ButtonName
		{
			get { return btnSelectUsers.Value; }
			set { btnSelectUsers.Value = value; }
		}

		public void InitControl()
		{
			try
			{
				LoadData();
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void LoadData()
		{
			try
			{
				string delimStr = ",";
				char [] delimiter = delimStr.ToCharArray();

				string [] saUserCode = this.UserCodes.Split(delimiter);
				string [] saStationCode = this.StationCodes.Split(delimiter);

				int i;
				this.SelectName.InnerText = "";

				for ( i=0;i<saUserCode.Length;i++ )
				{
					string userName = BLL.SystemRule.GetUserName(saUserCode[i]);
					if ( userName != "" )
					{
					this.SelectName.InnerText +=(this.SelectName.InnerText == "")?"":",";
						this.SelectName.InnerText += userName;
					}
				}

				for ( i=0;i<saStationCode.Length;i++ )
				{
					string stationName = BLL.SystemRule.GetStationName(saStationCode[i]);

					if ( stationName != "" )
					{
						this.SelectName.InnerText +=(this.SelectName.InnerText == "")?"":",";
						this.SelectName.InnerText += stationName;
					}
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}


		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if ( !IsPostBack )
			{
				LoadData();
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
