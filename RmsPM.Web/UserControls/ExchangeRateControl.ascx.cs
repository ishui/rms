namespace RmsPM.Web.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.ORMap;
	using System.Configuration;

	/// <summary>
	///		ExchangeRate 的摘要说明。
	/// </summary>
	public partial class ExchangeRateControl : System.Web.UI.UserControl
	{
		protected static string IsManyCurrency = ConfigurationSettings.AppSettings["IsManyCurrency"];

		#region --- 私有成员集合 ---

		protected DataView _MoneyTypeDataSource = null;
		protected string _MoneyTypeDataTextField = "";
		protected string _MoneyTypeDataValueField = "";

		#endregion --- 私有成员集合 ---

		#region --- 属性集合 ---

		#region 公共属性

		public string Amount
		{
			get { return amount.Text; }
			set { amount.Text = value; }
		}

		public string UnitPrise
		{
			get { return unitprise.Text; }
			set { unitprise.Text = value; }
		}

		/// <summary>
		/// 币种数据源
		/// </summary>
		public DataView MoneyTypeDataSource
		{
			set
			{
				_MoneyTypeDataSource = value;
			}
			get
			{
				return _MoneyTypeDataSource;
			}
		}
		/// <summary>
		/// 下拉框文本列
		/// </summary>
		public string MoneyTypeDataTextField
		{
			set
			{
				_MoneyTypeDataTextField = value;
			}
			get
			{
				return _MoneyTypeDataTextField;
			}
		}
		/// <summary>
		/// 下拉框值列
		/// </summary>
		public string MoneyTypeDataValueField
		{
			set
			{
				_MoneyTypeDataValueField = value;
			}
			get
			{
				return _MoneyTypeDataValueField;
			}
		}

		/// <summary>
		/// 币种文本
		/// </summary>
		public string MoneyType
		{
			get
			{
				if (EditMode)
				{
					if (ExchangeRateControl_M.Items.Count > 0)
					{
						return ExchangeRateControl_M.SelectedItem.Text;
					}
					else
					{
						if (ViewState["MoneyType"] != null)
						{
							return ViewState["MoneyType"].ToString();
						}
						else
						{
							return null;
						}
					}
				}
				else
				{
					if (ViewState["MoneyType"] != null)
					{
						return ViewState["MoneyType"].ToString();
					}
					else
					{
						return null;
					}
				}
			}
			set
			{
				//ViewState["MoneyType"]=value;
				if (ExchangeRateControl_M.Items.Count <= 0)
				{
					ViewState["MoneyType"] = value;
				}
				else
				{
					ViewState["MoneyType"] = value;
					ExchangeRateControl_M.SelectedIndex = ExchangeRateControl_M.Items.IndexOf(ExchangeRateControl_M.Items.FindByText(value));
					this.ExchangeRateControl_H.Value = ExchangeRateControl_M.SelectedItem.Value;
				}
			}
		}
		/// <summary>
		/// 币种值
		/// </summary>
		public string MoneyTypeValue
		{
			get
			{
				if (EditMode)
				{
					if (ExchangeRateControl_M.Items.Count > 0)
					{
						return ExchangeRateControl_M.SelectedItem.Value;
					}
					else
					{
						if (ViewState["MoneyTypeValue"] != null)
						{
							return ViewState["MoneyTypeValue"].ToString();
						}
						else
						{
							return null;
						}
					}
				}
				else
				{

					if (ViewState["MoneyTypeValue"] != null)
					{
						return ViewState["MoneyTypeValue"].ToString();
					}
					else
					{
						return null;
					}

				}


			}
			set
			{
				//ViewState["MoneyType"]=value;
				if (ExchangeRateControl_M.Items.Count <= 0)
				{
					ViewState["MoneyTypeValue"] = value;
				}
				else
				{
					ViewState["MoneyTypeValue"] = value;
					ExchangeRateControl_M.SelectedIndex = ExchangeRateControl_M.Items.IndexOf(ExchangeRateControl_M.Items.FindByValue(value));
					this.ExchangeRateControl_H.Value = this.ExchangeRateControl_M.SelectedItem.Value;
				}
			}
		}
		//public string MoneyType
		/// <summary>
		/// 汇率
		/// </summary>
		public decimal ExchangeRate
		{
			get
			{
				if (ExchangeRateControl_EV.Value == "" || ExchangeRateControl_EV.Value == "")
				{
					return Convert.ToDecimal("0");
				}
				return Convert.ToDecimal(this.ExchangeRateControl_EV.Value);
			}
			set
			{
				ExchangeRateControl_EV.Value = value.ToString();
			}
		}
		/// <summary>
		/// 汇率字符串形式
		/// </summary>
		public string ExchangeRateText
		{
			get
			{
				return this.ExchangeRateControl_EV.Value;
			}
			set
			{
				ExchangeRateControl_EV.Value = value;
			}
		}
		/// <summary>
		/// 金额
		/// </summary>
		public decimal Cash
		{

			get
			{
				if (ExchangeRateControl_CV.Value == "" || ExchangeRateControl_CV.Value == "")
				{
					return Convert.ToDecimal("0");
				}
				return Convert.ToDecimal(this.ExchangeRateControl_CV.Value);
			}
			set
			{
				ExchangeRateControl_CV.Value = value.ToString();
			}
		}
		/// <summary>
		/// 金额字符串形式
		/// </summary>
		public string CashText
		{
			get
			{
				return this.ExchangeRateControl_CV.Value;
			}
			set
			{
				ExchangeRateControl_CV.Value = value;
			}
		}
		/// <summary>
		/// 人民币金额
		/// </summary>
		public decimal RMB
		{
			get
			{
				if (ExchangeRateControl_R.Value == null || ExchangeRateControl_R.Value == "")
					return Convert.ToDecimal("0");
				else
					return Convert.ToDecimal(this.ExchangeRateControl_R.Value);

			}
			set
			{
				RMBText = value.ToString();
			}
		}
		/// <summary>
		/// RMB字符串类型
		/// </summary>
		public string RMBText
		{
			get
			{
				return this.ExchangeRateControl_R.Value;

			}
			set
			{
				ExchangeRateControl_R.Value = value;
				if (value != null || value != "")
				{
					this.ExchangeRateControl_V.Text = Convert.ToDecimal(value).ToString("N");
					this.ExchangeRateControl_O.Value = value;
				}
				else
				{
					this.ExchangeRateControl_V.Text = "0";
					this.ExchangeRateControl_O.Value = "0";
				}
			}
		}
		#endregion


		#region 标题设置
		/// <summary>
		/// 是否显示汇率,金额等,标题字段
		/// </summary>
		public bool IsShowTitle
		{
			get
			{
				return Lb_RMBTitle.Visible;
			}
			set
			{
				Lb_RMBTitle.Visible = value;
				this.Lb_CashTitle.Visible = value;
				this.Lb_ExchangeTitle.Visible = value;
				//txtExchangeRate.ValueDecimal = value;
			}
		}
		/// <summary>
		/// 人民币标题
		/// </summary>
		public string RMBTitle
		{
			get
			{
				return this.Lb_RMBTitle.Text;
			}
			set
			{
				this.Lb_RMBTitle.Text = value;
			}
		}
		/// <summary>
		/// 金额输入框标题
		/// </summary>
		public string CashTitle
		{
			get
			{
				return Lb_CashTitle.Text;
			}
			set
			{
				Lb_CashTitle.Text = value;
			}
		}
		/// <summary>
		/// 汇率输入框标题
		/// </summary>
		public string ExchangeTitle
		{
			get
			{
				return Lb_ExchangeTitle.Text;
			}
			set
			{
				Lb_ExchangeTitle.Text = value;
			}
		}
		#endregion


		#region 样式控制
		/// <summary>
		/// 金额输框长度
		/// </summary>
		public Unit CashWith
		{
			set
			{
				//ExchangeRateControl_C.Attributes.Add("style",value);
				//this.ExchangeRateControl_E.Width=value;
				ExchangeRateControl_C.Width = value;
			}
		}
		/// <summary>
		/// 汇率输放框宽度
		/// </summary>
		public Unit ExchangeWith
		{
			set
			{
				ExchangeRateControl_E.Width = value;
				//this.ExchangeRateControl_E.Width=value;
			}
		}
		/// <summary>
		/// 呈现状态为只读
		/// </summary>
		public bool IsAllowModify
		{
			get
			{
				return
					this.ExchangeRateControl_C.Enabled;
			}
			set
			{
				this.ExchangeRateControl_C.Enabled = value;
				this.ExchangeRateControl_E.Enabled = value;
				this.ExchangeRateControl_M.Enabled = value;
			}
		}
		/// <summary>
		/// 简易模式,只金额显示输入框,即非多币状态
		/// </summary>
		public bool SimpleMode
		{
			get
			{
				return hid_SimpleMode.Checked;
			}
			set
			{
				hid_SimpleMode.Checked = value;
				if (value)
				{
					this.ExchangeRateControl_E.Style.Add("display", "none");
					this.ExchangeRateControl_Y.Style.Add("display", "none");
					this.ExchangeRateControl_M.Style.Add("display", "none");
					this.ExchangeRateControl_EV.Style.Add("display", "none");
					this.ExchangeRateControl_V.Style.Add("display", "none");

				}
				else
				{
					this.ExchangeRateControl_E.Style.Add("display", "");
					this.ExchangeRateControl_Y.Style.Add("display", "");
					this.ExchangeRateControl_M.Style.Add("display", "");
				}
				this.IsShowTitle = !value;
				this.ExchangeRateControl_M.Visible = !value;

				//ddlExchangeRateType.Visible = value;
				/*this.ExchangeRateControl_E.Visible = value;
				this.ExchangeRateControl_R.Visible = value;
				this.IsShowTitle = value;
				this.ExchangeRateControl_Y.Visible=value;
				this.ExchangeRateControl_M.SelectedIndex = 0;*/
			}
		}
		/// <summary>
		/// 可见样式,显示币种信息true,可编辑状态,false可见状态
		/// </summary>
		public bool EditMode
		{

			get
			{
				if (ViewState["MyEditMode"] == null)
				{
					return true;
				}
				else
					return (bool)ViewState["MyEditMode"];
			}

			set
			{
				ViewState["MyEditMode"] = value;
				this.EditMoney.Visible = value;
				this.ViewMoney.Visible = !value;
				//return this.EditMoney.Visible;

			}
		}
		public string XMLUrl
		{
			get
			{
				String s = (String)ViewState["XMLUrl"];
				return ((s == null) ? "../Temp/" : "," + s);
			}

			set
			{
				ViewState["XMLUrl"] = value;
			}
		}
		/// <summary>
		/// 是否显示汇率框
		/// </summary>
		public bool IsShowExchange
		{


			get
			{
				if (ViewState["IsShowExchange"] == null)
				{
					return true;
				}
				else
				{
					return (bool)ViewState["IsShowExchange"];
				}
			}

			set
			{
				ViewState["IsShowExchange"] = value;
				if (!value)
				{
					this.ExchangeRateControl_E.Attributes.Add("display", "none");
				}
				else
				{
					this.ExchangeRateControl_E.Attributes.Add("display", "");
				}
			}
		}
		/// <summary>
		/// 是否允许金额改变
		/// </summary>
		public bool IsAllowCashChange
		{
			get
			{
				return this.ExchangeRateControl_C.Enabled;
			}
			set
			{
				this.ExchangeRateControl_C.Enabled = value;
			}
		}
		#endregion


		#region 事件设置
		/// <summary>
		/// 当币种改变时
		/// </summary>
		public string ValueChange
		{
			get
			{
				String s = (String)ViewState["ValueChange"];
				return ((s == null) ? String.Empty : s);
			}

			set
			{
				ViewState["ValueChange"] = value;
                this.ExchangeRateControl_CV.Attributes.Add("onblur", value);
				this.amount.Attributes.Add("onchange", value);
				this.unitprise.Attributes.Add("onchange", value);
			}

		}
		#endregion

		#endregion --- 属性集合 ---

		#region --- 公共方法 ---

		/// <summary>
		/// 控件初始化
		/// </summary>
		public void BindControl()
		{
			try
			{
				BindDropList();
				ExchangeRateSoure();
				InitControl();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(), ex, "");
			}
		}
		/// <summary>
		/// 装载控件数据
		/// </summary>
		private void LoadData()
		{

		
			try
			{
				EntityData entity = DAL.EntityDAO.ExchangeRateDAO.GetAllExchangeRate();

				string ud_sFilter = " MoneyType = '" + ExchangeRateControl_M.SelectedValue + "' and  Status in (0,1)";

				DataRow[] drs = entity.CurrentTable.Select(ud_sFilter, "CreateDate DESC", System.Data.DataViewRowState.CurrentRows);

				if (drs.Length > 0)
				{
					ExchangeRate = (decimal)drs[0][ddlExchangeRateType.SelectedValue] / 100;

					if ((int)drs[0]["Status"] == 1)
					{
						ddlExchangeRateType.SelectedIndex = 0;
						ddlExchangeRateType.Enabled = false;
						//	ExchangeRateControl_E.ReadOnly = true;
					}
					else
					{
						ddlExchangeRateType.Enabled = true;
						//ExchangeRateControl_E.ReadOnly = false;
					}
				}

			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(), ex, "");
			}

		}

		#endregion


		#region -----------数  据  操  作------------------------
		/// <summary>
		/// 生汇率数据源
		/// </summary>
		private void ExchangeRateSoure()
		{
			if (this.ViewMoney.Visible)
				return;
			string path = Page.Server.MapPath(XMLUrl) + "ExchangeRate.XML";
			QueryAgent aq = new QueryAgent();
			string sql = "Select * from ExchangeRate Where Status in (0,1)";
			DataSet ds = aq.ExecSqlForDataSet(sql);
			int count = ds.Tables[0].Rows.Count;
			for (int i = 0; i < count; i++)
			{
				ds.Tables[0].Rows[i]["RemittanceAverage"] = Convert.ToDecimal(ds.Tables[0].Rows[i]["RemittanceAverage"]) / 100;
			}
			if (ExchangeRateControl_EV.Value == null || ExchangeRateControl_EV.Value == "")
			{
				for (int i = 0; i < count; i++)
				{
					if (ds.Tables[0].Rows[i]["MoneyType"].ToString() == this.MoneyType)
					{
						this.ExchangeRateControl_EV.Value = ds.Tables[0].Rows[i]["RemittanceAverage"].ToString();
						break;
					}
				}
			}
			if (MoneyType == "人民币 (RMB)")
			{
				ExchangeRateControl_E.Enabled = false;
			}
			ds.WriteXml(path, XmlWriteMode.IgnoreSchema);
			Page.Response.Write("<xml id=\"ExchangeRate\" src=\"" + XMLUrl + "ExchangeRate.XML\"/>");
		}
		/// <summary>
		/// 邦定币种信息
		/// </summary>
		private void BindDropList()
		{
			DataView dv = new DataView();
			if (_MoneyTypeDataSource == null)
			{
				//设置缓存,如果没有使用10分种内过期
				if (Cache["MoneyTypeList"] == null)
				{
					EntityData entity = RmsPM.DAL.EntityDAO.SystemManageDAO.GetDictionaryItemByNameProject("币种", "");
					dv = new DataView(entity.CurrentTable);
					entity.Dispose();
					Page.Cache.Insert("MoneyTypeList", dv, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(25));
				}

				else
				{

					dv = (DataView)Page.Cache["MoneyTypeList"];

				}
			}
			else
			{
				dv = _MoneyTypeDataSource;
			}

			if (ExchangeRateControl_M.Items.Count <= 0)
			{
				string moneyType = this.MoneyType;
				string moneyTypeValue = this.MoneyTypeValue;

				this.ExchangeRateControl_M.DataSource = dv;
				this.ExchangeRateControl_M.DataTextField = this.MoneyTypeDataTextField == "" ? "Name" : this.MoneyTypeDataTextField;
				this.ExchangeRateControl_M.DataValueField = this.MoneyTypeDataValueField == "" ? "DictionaryItemCode" : this.MoneyTypeDataValueField;
				this.ExchangeRateControl_M.DataBind();
				if (moneyTypeValue != null)
				{
					ExchangeRateControl_M.SelectedIndex = ExchangeRateControl_M.Items.IndexOf(ExchangeRateControl_M.Items.FindByValue(moneyTypeValue));
				}
				else if (moneyType != null)
				{
					ExchangeRateControl_M.SelectedIndex = ExchangeRateControl_M.Items.IndexOf(ExchangeRateControl_M.Items.FindByText(moneyType));
				}
				this.ExchangeRateControl_H.Value = ExchangeRateControl_M.SelectedItem.Value;
			}
		}
		#endregion
		#region ---------------------控件初始化--------------------
		/// <summary>
		/// 生成客户代码
		/// </summary>
		private void InitControl()
		{
			//币种信息			
			//默认为可编辑状态
			//this.ExchangeRateControl_CV.Attributes.Add("onchange",this.ValueChange);
			//以上行由clm注释 将该逻辑直接添加到属性付值

			//如果为显示状态
			//if(ViewMoney.Visible)
			{
				if (this.MoneyType == "人民币 (RMB)")
				{
					this.MoneyValue.InnerHtml = this.MoneyType + ":" + this.Cash.ToString("N") + "&nbsp;元";
				}
				else
				{
					this.MoneyValue.InnerHtml = this.MoneyType + "&nbsp;:" + this.Cash.ToString("N") + "&nbsp;汇率:" + this.ExchangeRateText + "&nbsp;本币:" + this.RMB.ToString("N");
				}
			}
			//人民币为空时候,自动计算
			if (this.RMB == 0)
			{
				this.RMB = this.Cash * this.ExchangeRate;
				//ExchangeRateControl_Y.InnerHtml="元";
			}
			if (IsManyCurrency == "0")
				this.SimpleMode = true;
			else
				this.SimpleMode = false;
		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			//InitControl();
			try
			{
				string company = System.Configuration.ConfigurationManager.AppSettings["PMName"].ToLower();

				if (company != "gaokepm")
				{

					Amount_Unitprise.Style.Add("display", "none");
					//lbl_unitprise.Style.Add("display", "none");
					//amount.Style.Add("display", "none");
					//unitprise.Style.Add("display", "none");
				}
			}
			catch { }
			if (!IsPostBack)
			{
				BindControl();
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

		protected void ddlExchangeRateType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LoadData();
		}
	}
}
