namespace RmsPM.Web.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		InputSubject 的摘要说明。
	/// </summary>
	public partial class InputSubject : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.divName.InnerText = this.txtName.Value;
			this.divHint.InnerText = this.txtHint.Value;

			if (this.Visible) 
			{
				this.txtCode.Attributes["ClientID"] = this.ClientID;

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

		protected string imagePath = "../";
		public string ImagePath
		{
			set{ this.imagePath=value;}
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
				this.txtSubjectSetCode.Value = BLL.ProjectRule.GetSubjectSetCodeByProject(txtProjectCode.Value);
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
		private string _ProjectControl = "";
		public string ProjectControl
		{
			get {return _ProjectControl;}
			set {_ProjectControl = value;}
		}

        /// <summary>
        /// 帐套编号
        /// </summary>
        public string SubjectSetCode
        {
            get { return this.txtSubjectSetCode.Value; }
            set { this.txtSubjectSetCode.Value = value; }
        }
        
        /// <summary>
		/// 设置代码
		/// </summary>
		/// <param name="code"></param>
		private void SetCode(string code) 
		{
			try 
			{
				if(code != "")
				{
					this.txtCode.Value = code;

					string name = "";
					string fullname = "";
					string hint = BLL.SubjectRule.CheckSubject(code, this.txtSubjectSetCode.Value, "", ref name, ref fullname);

					this.txtName.Value = fullname;
					this.txtHint.Value = hint;

					this.divName.InnerText = this.txtName.Value;
					this.divHint.InnerText = this.txtHint.Value;
				}
			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		public string Value 
		{
			get {return this.txtCode.Value;}
			set {SetCode(value);}
		}

		public string Text 
		{
			get {return this.txtName.Value;}
		}

		public string Hint 
		{
			get {return this.txtHint.Value;}
		}

	}
}
