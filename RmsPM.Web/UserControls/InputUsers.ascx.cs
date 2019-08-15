namespace RmsPM.Web.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		InputUsers ��ժҪ˵����
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
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if ( !IsPostBack )
			{
				LoadData();
			}
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
		///		�޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
