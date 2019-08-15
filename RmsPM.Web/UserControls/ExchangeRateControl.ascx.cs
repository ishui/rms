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
	///		ExchangeRate ��ժҪ˵����
	/// </summary>
	public partial class ExchangeRateControl : System.Web.UI.UserControl
	{
		protected static string IsManyCurrency = ConfigurationSettings.AppSettings["IsManyCurrency"];

		#region --- ˽�г�Ա���� ---

		protected DataView _MoneyTypeDataSource = null;
		protected string _MoneyTypeDataTextField = "";
		protected string _MoneyTypeDataValueField = "";

		#endregion --- ˽�г�Ա���� ---

		#region --- ���Լ��� ---

		#region ��������

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
		/// ��������Դ
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
		/// �������ı���
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
		/// ������ֵ��
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
		/// �����ı�
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
		/// ����ֵ
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
		/// ����
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
		/// �����ַ�����ʽ
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
		/// ���
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
		/// ����ַ�����ʽ
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
		/// ����ҽ��
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
		/// RMB�ַ�������
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


		#region ��������
		/// <summary>
		/// �Ƿ���ʾ����,����,�����ֶ�
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
		/// ����ұ���
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
		/// �����������
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
		/// ������������
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


		#region ��ʽ����
		/// <summary>
		/// �����򳤶�
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
		/// ������ſ���
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
		/// ����״̬Ϊֻ��
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
		/// ����ģʽ,ֻ�����ʾ�����,���Ƕ��״̬
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
		/// �ɼ���ʽ,��ʾ������Ϣtrue,�ɱ༭״̬,false�ɼ�״̬
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
		/// �Ƿ���ʾ���ʿ�
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
		/// �Ƿ�������ı�
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


		#region �¼�����
		/// <summary>
		/// �����ָı�ʱ
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

		#endregion --- ���Լ��� ---

		#region --- �������� ---

		/// <summary>
		/// �ؼ���ʼ��
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
		/// װ�ؿؼ�����
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


		#region -----------��  ��  ��  ��------------------------
		/// <summary>
		/// ����������Դ
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
			if (MoneyType == "����� (RMB)")
			{
				ExchangeRateControl_E.Enabled = false;
			}
			ds.WriteXml(path, XmlWriteMode.IgnoreSchema);
			Page.Response.Write("<xml id=\"ExchangeRate\" src=\"" + XMLUrl + "ExchangeRate.XML\"/>");
		}
		/// <summary>
		/// �������Ϣ
		/// </summary>
		private void BindDropList()
		{
			DataView dv = new DataView();
			if (_MoneyTypeDataSource == null)
			{
				//���û���,���û��ʹ��10�����ڹ���
				if (Cache["MoneyTypeList"] == null)
				{
					EntityData entity = RmsPM.DAL.EntityDAO.SystemManageDAO.GetDictionaryItemByNameProject("����", "");
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
		#region ---------------------�ؼ���ʼ��--------------------
		/// <summary>
		/// ���ɿͻ�����
		/// </summary>
		private void InitControl()
		{
			//������Ϣ			
			//Ĭ��Ϊ�ɱ༭״̬
			//this.ExchangeRateControl_CV.Attributes.Add("onchange",this.ValueChange);
			//��������clmע�� �����߼�ֱ����ӵ����Ը�ֵ

			//���Ϊ��ʾ״̬
			//if(ViewMoney.Visible)
			{
				if (this.MoneyType == "����� (RMB)")
				{
					this.MoneyValue.InnerHtml = this.MoneyType + ":" + this.Cash.ToString("N") + "&nbsp;Ԫ";
				}
				else
				{
					this.MoneyValue.InnerHtml = this.MoneyType + "&nbsp;:" + this.Cash.ToString("N") + "&nbsp;����:" + this.ExchangeRateText + "&nbsp;����:" + this.RMB.ToString("N");
				}
			}
			//�����Ϊ��ʱ��,�Զ�����
			if (this.RMB == 0)
			{
				this.RMB = this.Cash * this.ExchangeRate;
				//ExchangeRateControl_Y.InnerHtml="Ԫ";
			}
			if (IsManyCurrency == "0")
				this.SimpleMode = true;
			else
				this.SimpleMode = false;
		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
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

		protected void ddlExchangeRateType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LoadData();
		}
	}
}
