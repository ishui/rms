namespace ControlLb
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:WebCustomControl1 runat=server></{0}:WebCustomControl1>"), DefaultProperty("Text")]
    public class WebCustomControl1 : WebControl
    {
        private HtmlSelect select = new HtmlSelect();
        private HtmlTable Table = new HtmlTable();
        private TextBox tb = new TextBox();
        private HtmlTableCell TbLeft = new HtmlTableCell();
        private HtmlTableCell TbRight = new HtmlTableCell();
        private HtmlTableCell TdCenter = new HtmlTableCell();
        private string text;
        private HtmlTableRow Tr = new HtmlTableRow();

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
            base.CreateChildControls();
            this.TdCenter.InnerHtml = "币种:";
            this.TdCenter.Controls.Add(this.select);
            this.tb.Text = "gaoyuantest";
            this.TbRight.Controls.Add(this.tb);
            this.Tr.Controls.Add(this.TbLeft);
            this.Tr.Controls.Add(this.TdCenter);
            this.Tr.Controls.Add(this.TbRight);
            this.Table.Controls.Add(this.Tr);
            this.Controls.Add(this.Table);
            base.CreateChildControls();
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
    }
}

