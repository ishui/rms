namespace ControlLb
{
    using System;
    using System.Drawing;
    using System.Web.UI;

    public class LinkControl2 : Control
    {
        private Color color = Color.Blue;
        private int fontSize = 20;
        private string hyperLink = "http://www.dy.com";
        //wrox
        private string text = "This is the dy site";

        protected override void Render(HtmlTextWriter output)
        {
            output.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(this.color));
            output.RenderBeginTag(HtmlTextWriterTag.Table);
            output.RenderBeginTag(HtmlTextWriterTag.Tr);
            output.RenderBeginTag(HtmlTextWriterTag.Td);
            output.AddAttribute(HtmlTextWriterAttribute.Href, this.hyperLink);
            output.AddStyleAttribute(HtmlTextWriterStyle.FontSize, this.fontSize.ToString());
            output.AddStyleAttribute(HtmlTextWriterStyle.Color, ColorTranslator.ToHtml(this.color));
            output.RenderBeginTag(HtmlTextWriterTag.A);
            output.Write(this.text);
            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();
        }

        public int FontSize
        {
            get
            {
                return this.fontSize;
            }
            set
            {
                this.fontSize = value;
            }
        }

        public string HyperLink
        {
            get
            {
                return this.hyperLink;
            }
            set
            {
                if (value.IndexOf("http://") == -1)
                {
                    throw new Exception("Specify Http as the protocol");
                }
                this.hyperLink = value;
            }
        }

        public Color LinkColor
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }

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

