namespace Rms.ControlLb
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [DefaultProperty("Text"), ToolboxData("<{0}:MoneyInput runat=server></{0}:MoneyInput>")]
    public class MoneyInput : WebControl, INamingContainer
    {
        private TextBox showTB = new TextBox();
        private HtmlTable Table = new HtmlTable();
        private HtmlTableCell tbc = new HtmlTableCell();
        private string text;
        private HtmlTableRow Tr = new HtmlTableRow();
        private TextBox valueInput = new TextBox();

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            this.EnsureChildControls();
            base.AddAttributesToRender(writer);
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.showTB.Attributes.Add("onfocus", "Moneyonfocus(this.id,\"" + this.ConID + "\")");
            this.showTB.Attributes.Add("onblur", "Moneyonblur(this.id,\"" + this.ConID + "\")");
            this.showTB.Attributes.Add("onKeypress", "JHshNumberText(this.id)");
            if (this.ValueChange != string.Empty)
            {
                this.showTB.Attributes.Add("onchange", "MoneyChange(this.id,\"" + this.ConID + "\")," + this.ValueChange);
            }
            else
            {
                this.showTB.Attributes.Add("onchange", "MoneyChange(this.id,\"" + this.ConID + "\")");
            }
            this.showTB.ID = this.UniqueID + "_M";
            this.showTB.Width = this.Width;
            this.showTB.Height = this.Height;
            this.tbc.Controls.Add(this.showTB);
            this.valueInput.Style.Add("display", "none");
            this.valueInput.ID = this.UniqueID + "_V";
            this.tbc.Controls.Add(this.valueInput);
            this.Tr.Controls.Add(this.tbc);
            this.Table.Controls.Add(this.Tr);
            this.Controls.Add(this.Table);
            string s = "<SCRIPT language=\"javascript\"src=\"JavaScriptUrl\"></SCRIPT>";
            this.Page.Response.Write(s);
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
        }

        public string ConID
        {
            get
            {
                string text = (string) this.ViewState["ConID"];
                return ((text == null) ? string.Empty : text);
            }
            set
            {
                this.ViewState["ConID"] = value;
            }
        }

        [Bindable(true), Category("样式设置"), Description("CSS设置")]
        public override string CssClass
        {
            get
            {
                return this.showTB.CssClass;
            }
            set
            {
                this.showTB.CssClass = value;
            }
        }

        [Category("金额数值"), Description("得到或设置值Decimal类型"), Bindable(true)]
        public decimal DecimalValue
        {
            get
            {
                this.EnsureChildControls();
                if ((this.showTB.Text == null) || (this.showTB.Text == ""))
                {
                    return Convert.ToDecimal("0");
                }
                return Convert.ToDecimal(this.showTB.Text);
            }
            set
            {
                string text = value.ToString();
                this.showTB.Text = text;
            }
        }

        [Bindable(true), Description("JavaScript路径"), Category("样式设置")]
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

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }

        [Description("金额"), Bindable(true), Category("金额数值")]
        public string Value
        {
            get
            {
                this.EnsureChildControls();
                return this.showTB.Text;
            }
            set
            {
                this.showTB.Text = value;
            }
        }

        [Category("客户端事件设置"), Description("金额改变事件,后要加括号,如:change()"), Bindable(true)]
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
    }
}

