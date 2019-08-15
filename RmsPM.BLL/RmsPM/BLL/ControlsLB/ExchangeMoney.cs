namespace RmsPM.BLL.ControlsLB
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Rms.ControlLb;

    [DefaultProperty("Text"), ToolboxData("<{0}:ExchangeMoney runat=server></{0}:ExchangeMoney>")]
    public class ExchangeMoney : WebControl, INamingContainer
    {
        private MoneyList mylist = new MoneyList();
        private HtmlTable Table = new HtmlTable();
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
            base.CreateChildControls();
            this.Controls.Add(this.mylist);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        [DefaultValue(""), Bindable(true), Category("Appearance")]
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

