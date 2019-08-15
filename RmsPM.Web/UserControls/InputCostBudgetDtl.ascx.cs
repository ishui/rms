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
	/// InputCostBudgetDtl 的摘要说明。
	/// </summary>
	public partial class InputCostBudgetDtl : System.Web.UI.UserControl
	{

		public Boolean Enable
		{
			set 
			{ 
				hid_Enable.Checked = value; 
				div_SearchButton.Visible = hid_Enable.Checked;
			}

			get
			{
				return hid_Enable.Checked;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			SetDiv();

			if (this.Visible) 
			{
				this.txtInput.Attributes["ClientID"] = this.ClientID;

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
            IniSelectAllLeaf();

		}
		#endregion

		private void SetDiv()
		{
			try 
			{
//				this.divName.InnerText = this.txtName.Value;
				this.divName.InnerText = this.txtFullName.Value;
				this.divHint.InnerText = this.txtHint.Value;
				this.divDesc.InnerText = this.txtDesc.Value;
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

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
        /// 初始化是否可选择所有节点
        /// </summary>
        private void IniSelectAllLeaf()
        {
            try
            {
                if (this.txtSelectAllLeaf.Value == "")
                {
                    if (BLL.ConvertRule.ToString(System.Configuration.ConfigurationSettings.AppSettings["SelectCBSAllNode"]) == "1")
                    {
                        this.txtSelectAllLeaf.Value = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
		/// 设置代码
		/// </summary>
		/// <param name="code"></param>
		private void SetCode(string CostBudgetSetCode, string CostCode) 
		{
			try 
			{
				this.txtCode.Value = CostCode;
				this.txtCostBudgetSetCode.Value = CostBudgetSetCode;
				this.txtName.Value = "";
				this.txtSortID.Value = "";
				this.txtPBSType.Value = "";
				this.txtPBSCode.Value = "";
				this.txtPBSName.Value = "";
				this.txtHint.Value = "";
				this.txtDesc.Value = "";

				if ((CostCode != "") && (CostBudgetSetCode != ""))
				{
					//预算设置表
					EntityData entity = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetSetByCode(CostBudgetSetCode);
					if (entity.HasRecord()) 
					{
						this.txtCostBudgetSetCode.Value = entity.GetString("CostBudgetSetCode");
						this.txtPBSType.Value = entity.GetString("PBSType");
						this.txtPBSCode.Value = entity.GetString("PBSCode");
						this.txtPBSName.Value = entity.GetString("PBSName");

						//费用项
						EntityData entityCBS = DAL.EntityDAO.CBSDAO.GetCBSByCode(CostCode);
						if (entityCBS.HasRecord()) 
						{
							this.txtCode.Value = entityCBS.GetString("CostCode");
							this.txtName.Value = entityCBS.GetString("CostName");
							this.txtSortID.Value = entityCBS.GetString("SortID");
							this.txtFullName.Value = BLL.CBSRule.GetCostFullName(CostCode);

							if (!SelectAllLeaf) 
							{
								if (!BLL.CBSRule.CheckCBSLeafNode(this.txtCode.Value))
								{
									this.txtHint.Value = "不是末级费用项 ！";
								}
							}

							if (this.txtPBSName.Value != "")
							{
								this.txtDesc.Value = "单位工程：" + this.txtPBSName.Value;
							}
						}
					}
					entity.Dispose();
				}

				this.txtInput.Value = this.txtSortID.Value;
				SetDiv();
			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

		public string CostName 
		{
			get {return this.txtName.Value;}
		}

		public string Hint 
		{
			get {return this.txtHint.Value;}
		}

		public string SortID 
		{
			get {return this.txtSortID.Value;}
		}

		public string CostCode 
		{
			get {return this.txtCode.Value;}
			set {SetCode(CostBudgetSetCode, value);}
		}

		public string CostBudgetSetCode 
		{
			get {return this.txtCostBudgetSetCode.Value;}
			set {SetCode(value, CostCode);}
		}

		public string PBSType 
		{
			get {return this.txtPBSType.Value;}
            set { }
		}

		public string PBSCode 
		{
			get {return this.txtPBSCode.Value;}
            set { }
		}

		public string PBSName 
		{
			get {return this.txtPBSName.Value;}
		}

		/// <summary>
		/// 是否可任意选择费用项
		/// </summary>
		public bool SelectAllLeaf
		{
			get {return (this.txtSelectAllLeaf.Value == "1");}
			set {this.txtSelectAllLeaf.Value = (value?"1":"0");}
		}

	}
}
