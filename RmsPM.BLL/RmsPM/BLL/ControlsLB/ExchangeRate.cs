namespace RmsPM.BLL.ControlsLB
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [DefaultProperty("Text"), ToolboxData("<{0}:ExchangeRate runat=server></{0}:ExchangeRate>")]
    public class ExchangeRate : TextBox
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Width = new Unit(Convert.ToInt32(this.Width) - 0x23);
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        protected override void Render(HtmlTextWriter output)
        {
            output.AddAttribute(HtmlTextWriterAttribute.Width, this.Width.ToString());
            output.AddAttribute(HtmlTextWriterAttribute.Height, this.Height.ToString());
            output.RenderBeginTag(HtmlTextWriterTag.Table);
            output.RenderBeginTag(HtmlTextWriterTag.Tr);
            output.AddAttribute(HtmlTextWriterAttribute.Width, "35");
            output.RenderBeginTag(HtmlTextWriterTag.Td);
            output.Write("汇率");
            output.RenderBeginTag(HtmlTextWriterTag.Td);
            base.Render(output);
            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();
        }
    }
}

