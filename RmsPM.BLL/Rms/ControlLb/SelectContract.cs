namespace Rms.ControlLb
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [DefaultProperty("Text"), ToolboxData("<{0}:SelectContract runat=server></{0}:SelectContract>")]
    public class SelectContract : WebControl
    {
        private ImageButton IBSearch = new ImageButton();
        protected HtmlGenericControl SearchPanel;
        private HtmlTable Table = new HtmlTable();
        private HtmlTableRow Tr = new HtmlTableRow();
        private TextBox TxtCode = new TextBox();
        private TextBox TxtID = new TextBox();
        private TextBox TxtName = new TextBox();

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

        protected override void CreateChildControls()
        {
            this.TxtName.Width = this.NameWidth;
            this.TxtName.Height = this.Height;
            this.AddControlToTable(this.TxtName);
            this.TxtID.Width = this.IDWidth;
            this.TxtID.Height = this.Height;
            this.AddControlToTable(this.TxtID);
            base.CreateChildControls();
            this.TxtCode.Width = Unit.Point(0);
            this.AddControlToTable(this.IBSearch);
        }

        [Category("样式控件"), Description("合同号控件宽度"), DefaultValue(80), Bindable(true)]
        public Unit IDWidth
        {
            get
            {
                return this.TxtID.Width;
            }
            set
            {
                this.TxtID.Width = value;
            }
        }

        [DefaultValue(true), Description("是否显不名字框"), Category("样式控件"), Bindable(true)]
        public bool IsEditMode
        {
            get
            {
                return this.TxtName.Visible;
            }
            set
            {
                this.TxtName.Visible = value;
            }
        }

        [Bindable(true), Category("样式控件"), Description("是否显不ID框"), DefaultValue(true)]
        public bool IsShowID
        {
            get
            {
                return this.TxtID.Visible;
            }
            set
            {
                this.TxtID.Visible = value;
            }
        }

        [Bindable(true), DefaultValue(true), Description("是否显不名字框"), Category("样式控件")]
        public bool IsShowName
        {
            get
            {
                return this.TxtName.Visible;
            }
            set
            {
                this.TxtName.Visible = value;
            }
        }

        [DefaultValue(50), Description("名字控件宽度"), Bindable(true), Category("样式控件")]
        public Unit NameWidth
        {
            get
            {
                return this.TxtName.Width;
            }
            set
            {
                this.TxtName.Width = value;
            }
        }
    }
}

