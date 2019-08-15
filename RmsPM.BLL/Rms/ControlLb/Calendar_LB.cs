namespace Rms.ControlLb
{
    using AspWebControl;
    using System;
    using System.ComponentModel;
    using System.Web.UI;

    [ToolboxData("<{0}:Calendar_LB runat=server></{0}:Calendar_LB>"), DefaultProperty("Text")]
    public class Calendar_LB : Calendar
    {
        protected override void Render(HtmlTextWriter output)
        {
            if (this.IsEditMode)
            {
                base.Render(output);
            }
            else
            {
                output.AddAttribute(HtmlTextWriterAttribute.Id, this.ID);
                output.RenderBeginTag(HtmlTextWriterTag.Span);
                output.Write(base.Value);
                output.RenderEndTag();
                base.Visible = false;
            }
        }

        public bool IsEditMode
        {
            get
            {
                return ((this.ViewState["IsEditMode"] == null) || ((bool) this.ViewState["IsEditMode"]));
            }
            set
            {
                this.ViewState["IsEditMode"] = value;
            }
        }
    }
}

