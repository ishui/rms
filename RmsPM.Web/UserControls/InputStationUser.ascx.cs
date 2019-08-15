namespace RmsPM.Web.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	/// InputStationUser 的摘要说明。
	/// </summary>
	public partial class InputStationUser : System.Web.UI.UserControl
	{
        public Boolean Readonly
        {
            set
            {
                this.chkReadonly.Checked = value;
                this.divSelect.Visible = !value;
            }
            get
            {
                return chkReadonly.Checked;
            }
        }
        
        protected void Page_Load(object sender, System.EventArgs e)
		{
			this.divName.InnerText = this.txtName.Value;
			this.divHint.InnerText = this.txtHint.Value;

			if (this.Visible) 
			{
//				this.txtInput.Attributes["ClientID"] = this.ClientID;

//				string reload = Rms.Web.JavaScript.ScriptStart;
//				reload += @"var ClientID = '" + this.ClientID + "';" + "\n" ;
//				reload += Rms.Web.JavaScript.ScriptEnd;
//				Response.Write(reload);

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
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化页面失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
			}
		}

		/// <summary>
		/// 设置项目代码，初始化
		/// </summary>
		/// <param name="ProjectCode"></param>
		private void SetProject(string ProjectCode)
		{
			try 
			{
				this.txtProjectCode.Value = ProjectCode;
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化页面失败");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
			}
		}

		public string ProjectCode
		{
			get {return this.txtProjectCode.Value;}
			set {SetProject(value);}
		}

		/// <summary>
		/// 设置用户代码
		/// </summary>
		/// <param name="code"></param>
		private void SetUserCodes(string codes) 
		{
			try 
			{
				this.txtUserCodes.Value = codes;

				string names = "";

				if (codes != "") 
				{
					string[] arrCode = codes.Split(",".ToCharArray());

					int i = -1;
					foreach(string code in arrCode) 
					{
						i++;

						if (i > 0) 
						{
							names = names + ",";
						}

						string name = BLL.SystemRule.GetUserName(code);
						names = names + name;
					}
				}

				this.txtUserNames.Value = names;

				ShowName();
			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		/// <summary>
		/// 设置岗位代码
		/// </summary>
		/// <param name="code"></param>
		private void SetStationCodes(string codes) 
		{
			try 
			{
				this.txtStationCodes.Value = codes;

				string names = "";

				if (codes != "") 
				{
					string[] arrCode = codes.Split(",".ToCharArray());

					int i = -1;
					foreach(string code in arrCode) 
					{
						i++;

						if (i > 0) 
						{
							names = names + ",";
						}

						string name = BLL.SystemRule.GetStationName(code);
						names = names + name;
					}
				}

				this.txtStationNames.Value = names;

				ShowName();
			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		private void ShowName()
		{
			try 
			{
				string name = this.txtUserNames.Value;

				if (this.txtStationNames.Value != "") 
				{
					if (name != "")
					{
						name = name + ",";
					}
					name = name + this.txtStationNames.Value;
				}

				this.txtName.Value = name;
				this.divName.InnerText = this.txtName.Value;
			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		public string UserCodes 
		{
			get {return this.txtUserCodes.Value;}
			set {SetUserCodes(value);}
		}

		public string StationCodes 
		{
			get {return this.txtStationCodes.Value;}
			set {SetStationCodes(value);}
		}

		public string UserNames 
		{
			get {return this.txtUserNames.Value;}
		}

		public string StationNames 
		{
			get {return this.txtStationNames.Value;}
		}

		public string Text 
		{
			get {return this.txtName.Value;}
		}

		public string Hint 
		{
			get {return this.txtHint.Value;}
		}
		protected string imagePath = "../images/";
		public string ImagePath
		{
			set
			{
				this.imagePath = value;
			}
		}

        private string m_OnClientPost = "";

        private void SetOnClientPost(string value)
        {
            m_OnClientPost = value;
        }

        public string OnClientPost
        {
            get { return this.m_OnClientPost; }
            set { SetOnClientPost(value); }
        }

        public string MyOnClientPost
        {
            get
            {
                if (m_OnClientPost == "")
                {
                    return "InputStationUser_Null()";
                }
                else
                {
                    return m_OnClientPost;
                }
            }
        }
    }
}
