namespace Rms.ControlLb
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Web.Caching;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    [DefaultProperty("Text"), ParseChildren(true), ToolboxData("<{0}:ManyCurrency runat=server></{0}:ManyCurrency>"), PersistChildren(false)]
    public class ManyCurrency : WebControl, INamingContainer
    {
        private string _DictionaryName = "币种";
        private DataSet _MoneyTypeSoure;
        private DropDownList DDl_MoneyTypeList = new DropDownList();
        private Label lb_ExchangeTitle = new Label();
        private Label lb_RMB = new Label();
        private Label lb_RMBTitle = new Label();
        private Label Lb_Title = new Label();
        private TextBox ShowCash = new TextBox();
        private TextBox ShowExchange = new TextBox();
        private HtmlTable Table = new HtmlTable();
        private HtmlTableCell tbc = new HtmlTableCell();
        private HtmlTableRow Tr = new HtmlTableRow();
        private TextBox ValueCash = new TextBox();
        private TextBox ValueExchange = new TextBox();

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            this.EnsureChildControls();
            base.AddAttributesToRender(writer);
        }

        protected void AddControlToTable(Control controlName)
        {
            HtmlTableCell child = new HtmlTableCell();
            child.Controls.Add(controlName);
            this.Tr.Controls.Add(child);
        }

        public void BindMoneyList()
        {
            try
            {
                if (this.DDl_MoneyTypeList.Items.Count <= 0)
                {
                    if (this.Page.Cache["MoneyTypeList"] == null)
                    {
                        DataSet set;
                        if (this._MoneyTypeSoure == null)
                        {
                            EntityData dictionaryItemByNameProject = SystemManageDAO.GetDictionaryItemByNameProject(this._DictionaryName, "");
                            set = dictionaryItemByNameProject;
                            dictionaryItemByNameProject.Dispose();
                        }
                        else
                        {
                            set = this._MoneyTypeSoure;
                        }
                        this.Page.Cache.Insert("MoneyTypeList", set, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(25));
                    }
                    this.DDl_MoneyTypeList.DataSource = (DataSet) this.Page.Cache["MoneyTypeList"];
                    this.DDl_MoneyTypeList.DataTextField = "Name";
                    this.DDl_MoneyTypeList.DataValueField = "DictionaryItemCode";
                    this.DDl_MoneyTypeList.DataBind();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        protected override void CreateChildControls()
        {
            this.Table.Border = 0;
            this.Table.CellPadding = 0;
            this.Table.Width = "100%";
            this.Table.Height = "100%";
            this.Lb_Title.Visible = this.IsShowTitle;
            this.AddControlToTable(this.Lb_Title);
            this.ShowCash.Attributes.Add("onfocus", "Moneyonfocus(this.id)");
            this.ShowCash.Attributes.Add("onblur", "Moneyonblur(this.id)");
            this.ShowCash.Attributes.Add("onKeypress", "JHshNumberText(this.id)");
            if (this.ValueChange != string.Empty)
            {
                this.ShowCash.Attributes.Add("onchange", "MoneyChange(this.id)," + this.ValueChange);
            }
            else
            {
                this.ShowCash.Attributes.Add("onchange", "MoneyChange(this.id)");
            }
            this.ShowCash.ID = this.UniqueID + "_C";
            this.ShowCash.Width = this.Width;
            this.ShowCash.Height = this.Height;
            this.AddControlToTable(this.ShowCash);
            this.ValueCash.Style.Add("display", "none");
            this.ValueCash.ID = this.UniqueID + "_V";
            this.AddControlToTable(this.ValueCash);
            this.BindMoneyList();
            this.DDl_MoneyTypeList.Visible = this.ShowAllBox;
            this.DDl_MoneyTypeList.ID = this.UniqueID + "_T";
            this.DDl_MoneyTypeList.Attributes.Add("onchange", "MoneyTypeChanged(this.id);return false;");
            this.AddControlToTable(this.DDl_MoneyTypeList);
            this.lb_ExchangeTitle.Visible = this.IsShowTitle;
            this.lb_ExchangeTitle.Visible = this.ShowAllBox;
            this.AddControlToTable(this.lb_ExchangeTitle);
            this.ShowExchange.Attributes.Add("onfocus", "Moneyonfocus(this.id)");
            this.ShowExchange.Attributes.Add("onblur", "Moneyonblur(this.id)");
            this.ShowExchange.Attributes.Add("onKeypress", "JHshNumberText(this.id)");
            if (this.ValueChange != string.Empty)
            {
                this.ShowExchange.Attributes.Add("onchange", "MoneyChange(this.id)," + this.ExchangeChange);
            }
            else
            {
                this.ShowExchange.Attributes.Add("onchange", "MoneyChange(this.id)");
            }
            this.ShowExchange.ID = this.UniqueID + "_E";
            this.ShowExchange.Width = this.Width;
            this.ShowExchange.Height = this.Height;
            this.AddControlToTable(this.ShowExchange);
            this.ValueExchange.Style.Add("display", "none");
            this.ValueExchange.ID = this.UniqueID + "_F";
            this.AddControlToTable(this.ValueExchange);
            this.ExchangeRateSoure();
            this.lb_RMBTitle.Visible = this.IsShowTitle;
            this.AddControlToTable(this.lb_RMBTitle);
            this.lb_RMB.Visible = this.ShowAllBox;
            this.lb_RMB.ID = this.UniqueID + "_R";
            this.AddControlToTable(this.lb_RMB);
            this.Table.Controls.Add(this.Tr);
            this.Controls.Add(this.Table);
        }

        public void ExchangeRateSoure()
        {
            int num2;
            string fileName = this.Page.Server.MapPath(this.ExchangeXMLURl + "ExchangeRate.XML");
            QueryAgent agent = new QueryAgent();
            string queryString = "Select * from ExchangeRate Where Status in (0,1)";
            DataSet set = agent.ExecSqlForDataSet(queryString);
            int count = set.Tables[0].Rows.Count;
            for (num2 = 0; num2 < count; num2++)
            {
                set.Tables[0].Rows[num2]["RemittanceAverage"] = Convert.ToDecimal(set.Tables[0].Rows[num2]["RemittanceAverage"]) / 100M;
            }
            for (num2 = 0; num2 < count; num2++)
            {
                if (set.Tables[0].Rows[num2]["MoneyType"].ToString() == this.MoneyTypeText)
                {
                    this.ExchangeValue = set.Tables[0].Rows[num2]["MoneyType"].ToString();
                    break;
                }
            }
            set.WriteXml(fileName, XmlWriteMode.IgnoreSchema);
            this.Page.Response.Write("<xml id=\"ExchangeRate\" src=\"" + this.ExchangeXMLURl + "ExchangeRate.XML\"/>");
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
        }

        protected override object SaveViewState()
        {
            return base.SaveViewState();
        }

        [DefaultValue((string) null), Category("公共属性"), Bindable(true), Description("金额Decimal格式")]
        public decimal CashDecimal
        {
            get
            {
                this.EnsureChildControls();
                if ((this.ValueCash.Text == null) || (this.ValueCash.Text == ""))
                {
                    return Convert.ToDecimal("0");
                }
                return Convert.ToDecimal(this.ValueCash.Text);
            }
            set
            {
                this.EnsureChildControls();
                string text = value.ToString();
                this.ValueCash.Text = text;
            }
        }

        [DefaultValue((string) null), Description("金额"), Bindable(true), Category("公共属性")]
        public string CashValue
        {
            get
            {
                if (this.ValueCash.Text == null)
                {
                    return ((TextBox) this.Parent.FindControl(this.UniqueID + "_V")).Text;
                }
                return this.ValueCash.Text;
            }
            set
            {
                this.ValueCash.Text = value;
            }
        }

        [Bindable(true), Description("金额控件宽度"), Category("样式控件"), DefaultValue(100)]
        public Unit CashWith
        {
            get
            {
                return this.ShowCash.Width;
            }
            set
            {
                this.ShowCash.Width = value;
            }
        }

        [Bindable(true), Description("下拉框样式控件"), Category("样式控件"), DefaultValue("")]
        public string DropListCssClass
        {
            get
            {
                return this.DDl_MoneyTypeList.CssClass;
            }
            set
            {
                this.DDl_MoneyTypeList.CssClass = value;
            }
        }

        [Description("汇率改变事件,后要加括号,如:change()如果多个中间可加,"), Category("客户端事件设置"), Bindable(true)]
        public string ExchangeChange
        {
            get
            {
                string text = (string) this.ViewState["ExchangeChange"];
                return ((text == null) ? string.Empty : text);
            }
            set
            {
                this.ViewState["ExchangeChange"] = value;
            }
        }

        [Bindable(true), DefaultValue((string) null), Description("汇率的Decimal格式"), Category("公共属性")]
        public decimal ExchangeDecimal
        {
            get
            {
                this.EnsureChildControls();
                if ((this.ValueExchange.Text == null) || (this.ValueExchange.Text == ""))
                {
                    return Convert.ToDecimal("0");
                }
                return Convert.ToDecimal(this.ValueExchange.Text);
            }
            set
            {
                this.EnsureChildControls();
                string text = value.ToString();
                this.ValueExchange.Text = text;
            }
        }

        [Category("标题信息"), Bindable(true), Description("标题"), DefaultValue("汇率:")]
        public string ExchangeTitle
        {
            get
            {
                return this.lb_ExchangeTitle.Text;
            }
            set
            {
                this.lb_ExchangeTitle.Text = value;
            }
        }

        [Bindable(true), Description("汇率"), Category("公共属性"), DefaultValue((string) null)]
        public string ExchangeValue
        {
            get
            {
                this.EnsureChildControls();
                return this.ValueExchange.Text;
            }
            set
            {
                this.EnsureChildControls();
                this.ValueExchange.Text = value;
            }
        }

        [Bindable(true), DefaultValue(60), Description("汇率空格宽度"), Category("样式控件")]
        public Unit ExchangeWith
        {
            get
            {
                return this.ShowExchange.Width;
            }
            set
            {
                this.ShowExchange.Width = value;
            }
        }

        [DefaultValue("../Images/ManyCurrency/XML/"), Description("Exchange文件目录"), Category("文件目录设置"), Bindable(true)]
        public string ExchangeXMLURl
        {
            get
            {
                string text = (string) this.ViewState["ExchangeXMLURl"];
                return ((text == null) ? string.Empty : text);
            }
            set
            {
                this.ViewState["ExchangeXMLURl"] = value;
            }
        }

        [DefaultValue(true), Description("标题"), Category("标题信息"), Bindable(true)]
        public bool IsShowTitle
        {
            get
            {
                return this.Lb_Title.Visible;
            }
            set
            {
                this.Lb_Title.Visible = value;
            }
        }

        [Bindable(true), Description("javascript路径"), Category("样式控件"), DefaultValue("../images/RmsControls/javaScripts/ManyCurrency.js")]
        public string JavaScriptUrl
        {
            get
            {
                string text = (string) this.ViewState["JavaScriptUrl"];
                return ((text == null) ? string.Empty : text);
            }
            set
            {
                this.ViewState["JavaScriptUrl"] = value;
            }
        }

        [DefaultValue(""), Bindable(true), Category("样式控件"), Description("文字显示CSS")]
        public string LabelCssClass
        {
            get
            {
                return this.Lb_Title.CssClass;
            }
            set
            {
                this.lb_ExchangeTitle.CssClass = this.lb_RMB.CssClass = value;
                this.lb_RMBTitle.CssClass = this.Lb_Title.CssClass = value;
            }
        }

        [Description("金额输入框CSS"), Bindable(true), Category("样式控件"), DefaultValue("")]
        public string MoneyTextCssClass
        {
            get
            {
                return this.ShowCash.CssClass;
            }
            set
            {
                this.ShowCash.CssClass = this.ShowExchange.CssClass = value;
            }
        }

        [DefaultValue((string) null), Description("货币种类"), Category("公共属性"), Bindable(true)]
        public string MoneyTypeCode
        {
            get
            {
                this.EnsureChildControls();
                if (this.DDl_MoneyTypeList.Items.Count > 0)
                {
                    return this.DDl_MoneyTypeList.SelectedItem.Value;
                }
                return null;
            }
            set
            {
                this.EnsureChildControls();
                this.DDl_MoneyTypeList.SelectedIndex = this.DDl_MoneyTypeList.Items.IndexOf(this.DDl_MoneyTypeList.Items.FindByValue(value));
            }
        }

        [Bindable(true), Description("货币种类名字"), Category("公共属性"), DefaultValue((string) null)]
        public DataSet MoneyTypeSoure
        {
            get
            {
                return this._MoneyTypeSoure;
            }
            set
            {
                this._MoneyTypeSoure = value;
            }
        }

        [Category("公共属性"), Description("货币种类名字"), Bindable(true), DefaultValue((string) null)]
        public string MoneyTypeText
        {
            get
            {
                this.EnsureChildControls();
                if (this.DDl_MoneyTypeList.Items.Count > 0)
                {
                    return this.DDl_MoneyTypeList.SelectedItem.Text;
                }
                return null;
            }
            set
            {
                this.EnsureChildControls();
                this.DDl_MoneyTypeList.SelectedIndex = this.DDl_MoneyTypeList.Items.IndexOf(this.DDl_MoneyTypeList.Items.FindByText(value));
            }
        }

        [DefaultValue((string) null), Description("折合RMB汇率的Decimal格式"), Category("公共属性"), Bindable(true)]
        public decimal RMBDecimal
        {
            get
            {
                this.EnsureChildControls();
                if ((this.lb_RMB.Text == null) || (this.lb_RMB.Text == ""))
                {
                    return Convert.ToDecimal("0");
                }
                return Convert.ToDecimal(this.lb_RMB.Text);
            }
            set
            {
                this.EnsureChildControls();
                string text = value.ToString();
                this.lb_RMB.Text = text;
            }
        }

        [Bindable(true), Description("标题"), Category("标题信息"), DefaultValue("折合人民币:")]
        public string RMBTitle
        {
            get
            {
                return this.lb_RMBTitle.Text;
            }
            set
            {
                this.lb_RMBTitle.Text = value;
            }
        }

        [Category("公共属性"), Bindable(true), Description("折合RMB汇率的Decimal格式"), DefaultValue((string) null)]
        public string RMBValue
        {
            get
            {
                this.EnsureChildControls();
                return this.lb_RMB.Text;
            }
            set
            {
                this.EnsureChildControls();
                this.lb_RMB.Text = value;
            }
        }

        [DefaultValue(true), Description("true ,显示所有的框,false只显示金额输入框"), Category("样式控件"), Bindable(true)]
        public bool ShowAllBox
        {
            get
            {
                return this.DDl_MoneyTypeList.Visible;
            }
            set
            {
                this.DDl_MoneyTypeList.Visible = value;
            }
        }

        [Category("标题信息"), Bindable(true), DefaultValue("金额:"), Description("标题")]
        public string Title
        {
            get
            {
                return this.Lb_Title.Text;
            }
            set
            {
                this.Lb_Title.Text = value;
            }
        }

        [Description("金额改变事件,后要加括号,如:change()"), Bindable(true), Category("客户端事件设置")]
        public string ValueChange
        {
            get
            {
                string text = (string) this.ViewState["ValueChange"];
                return ((text == null) ? string.Empty : text);
            }
            set
            {
                this.ViewState["ValueChange"] = value;
            }
        }

        [DefaultValue(350)]
        public override Unit Width
        {
            get
            {
                return base.Width;
            }
            set
            {
                base.Width = value;
            }
        }
    }
}

