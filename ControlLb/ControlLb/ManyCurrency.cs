namespace ControlLb
{
    using Infragistics.WebUI.WebDataInput;
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [DefaultProperty("Text"), ToolboxData("<{0}:ManyCurrency runat=server></{0}:ManyCurrency>")]
    public class ManyCurrency : WebControl
    {
        private string _Exchange;
        private string _ImageDirectory = "../images/infragistics/images/";
        private string _JavaScriptFileName = "../images/infragistics/20051/scripts/ig_edit.js";
        private string _JavaScriptFileNameCommon = "../images/infragistics/20051/scripts/ig_shared.js";
        private string _MoneyName;
        private string _MoneyValue;
        private string _Value;
        private DropDownList select = new DropDownList();
        private HtmlTable Table = new HtmlTable();
        private TextBox tb = new TextBox();
        private HtmlTableCell TbLeft = new HtmlTableCell();
        private HtmlTableCell TbRight = new HtmlTableCell();
        private HtmlTableCell TdCenter = new HtmlTableCell();
        private string text;
        private Label Title = new Label();
        private HtmlTableRow Tr = new HtmlTableRow();
        private WebNumericEdit WebMoney = new WebNumericEdit();

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            this.EnsureChildControls();
            base.AddAttributesToRender(writer);
        }

        protected override void CreateChildControls()
        {
            this.Table.Border = 0;
            this.Table.CellPadding = 0;
            this.Table.Width = "100%";
            this.Table.Height = "100%";
            this.WebMoney.ImageDirectory = this._ImageDirectory;
            this.WebMoney.JavaScriptFileName = this._JavaScriptFileName;
            this.WebMoney.JavaScriptFileNameCommon = this._JavaScriptFileNameCommon;
            this.WebMoney.ID = this.UniqueID + "Money";
            this.TbLeft.Controls.Add(this.WebMoney);
            this.Title.Text = "币种: ";
            this.Title.ID = this.UniqueID + "Title";
            this.Title.CssClass = this.CssClass;
            this.TbLeft.Controls.Add(this.Title);
            this.TbLeft.NoWrap = true;
            this.select.ID = this.UniqueID + "select";
            this.TdCenter.Controls.Add(this.select);
            this.tb.Text = "";
            this.tb.Width = 60;
            this.tb.ID = this.UniqueID + "Exchange";
            this.TbRight.Controls.Add(this.tb);
            this.Tr.Controls.Add(this.TbLeft);
            this.Tr.Controls.Add(this.TdCenter);
            this.Tr.Controls.Add(this.TbRight);
            this.Table.Controls.Add(this.Tr);
            this.Controls.Add(this.Table);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public void WriteValue()
        {
            this.MoneyName = ((DropDownList) this.Parent.FindControl(this.UniqueID + "select")).SelectedItem.Text;
            this.MoneyValue = ((DropDownList) this.Parent.FindControl(this.UniqueID + "select")).SelectedItem.Value;
            this.Exchange = ((TextBox) this.Parent.FindControl(this.UniqueID + "Exchange")).Text;
        }

        public string Exchange
        {
            get
            {
                return this._Exchange;
            }
            set
            {
                this._Exchange = value;
            }
        }

        public string ImageDirectory
        {
            get
            {
                return this._ImageDirectory;
            }
            set
            {
                this._ImageDirectory = value;
            }
        }

        public string JavaScriptFileName
        {
            get
            {
                return this._JavaScriptFileName;
            }
            set
            {
                this._JavaScriptFileName = value;
            }
        }

        public string JavaScriptFileNameCommon
        {
            get
            {
                return this._JavaScriptFileNameCommon;
            }
            set
            {
                this._JavaScriptFileNameCommon = value;
            }
        }

        public string MoneyName
        {
            get
            {
                return this._MoneyName;
            }
            set
            {
                this._MoneyName = value;
            }
        }

        public string MoneyValue
        {
            get
            {
                return this._MoneyValue;
            }
            set
            {
                this._MoneyValue = value;
            }
        }

        [DefaultValue(""), Category("Appearance"), Bindable(true)]
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

        public string Value
        {
            get
            {
                return this._Value;
            }
            set
            {
                this._Value = value;
            }
        }
    }
}

