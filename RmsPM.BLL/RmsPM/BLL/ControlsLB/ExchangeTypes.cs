namespace RmsPM.BLL.ControlsLB
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:ExchangeTypes runat=server></{0}:ExchangeTypes>"), DefaultProperty("Text")]
    public class ExchangeTypes : DropDownList
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.Items.Count <= 0)
            {
                this.Items.Add(new ListItem("现汇买入价", "0000"));
                this.Items.Add(new ListItem("现汇买出价", "0001"));
                this.Items.Add(new ListItem("现钞买入价", "0002"));
                this.Items.Add(new ListItem("现钞买出价", "0003"));
                this.Items.Add(new ListItem("中间价", "0004"));
                this.SelectedIndex = 4;
            }
        }

        [Category("Appearance"), DefaultValue(""), Bindable(true)]
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }
    }
}

