namespace ControlLb
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:RmsManyCurrency runat=server></{0}:RmsManyCurrency>"), DefaultProperty("Text")]
    public class testContro : WebControl, INamingContainer
    {
        private HtmlSelect select = new HtmlSelect();
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        private HtmlTable Table = new HtmlTable();
        private TextBox tb = new TextBox();
        private HtmlTableCell TbLeft = new HtmlTableCell();
        private HtmlTableCell TbRight = new HtmlTableCell();
        private HtmlTableCell TdCenter = new HtmlTableCell();
        private HtmlTableRow Tr = new HtmlTableRow();

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            this.EnsureChildControls();
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
    }
}

