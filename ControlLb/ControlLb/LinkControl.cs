namespace ControlLb
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:LinkControl runat=server></{0}:LinkControl>"), DefaultProperty("Text")]
    public class LinkControl : DataGrid
    {
        private int _Border;
        private int _Coloum = 0;
        private Color _HeaderColor;
        private int _Row = 0;
        private int _TableHeight;
        private int _TableWeight;
        private Color bgColor;
        private string text = "鼎耀";
        private string url = "http://www.dy.com";
        //wmbus

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.AddAttribute(HtmlTextWriterAttribute.Background, ColorTranslator.ToHtml(this.BgColor));
            writer.AddAttribute(HtmlTextWriterAttribute.Border, this._Border.ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Width, this._TableWeight.ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Height, this._TableHeight.ToString());
            writer.Write("测试表格");
            writer.RenderEndTag();
            base.AddAttributesToRender(writer);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(HtmlTextWriterAttribute.Bgcolor, ColorTranslator.ToHtml(this._HeaderColor));
            writer.AddAttribute(HtmlTextWriterAttribute.Colspan, this._Coloum.ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Height, this._TableHeight.ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Bgcolor, "Red");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.RenderBeginTag(HtmlTextWriterTag.B);
            writer.Write("动态生成表格");
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
            for (int i = 0; i < this._Row; i++)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                for (int j = 0; j < this._Coloum; j++)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write("动态" + j + "列");
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
            }
            writer.RenderEndTag();
            base.RenderContents(writer);
        }

        public Color BgColor
        {
            get
            {
                return this.bgColor;
            }
            set
            {
                this.bgColor = value;
            }
        }

        public int Border
        {
            get
            {
                return this._Border;
            }
            set
            {
                this._Border = value;
            }
        }

        public int Coloum
        {
            get
            {
                return this._Coloum;
            }
            set
            {
                this._Coloum = value;
            }
        }

        public Color HeaderColor
        {
            get
            {
                return this._HeaderColor;
            }
            set
            {
                this._HeaderColor = value;
            }
        }

        public int Row
        {
            get
            {
                return this._Row;
            }
            set
            {
                this._Row = value;
            }
        }

        public int TableHeight
        {
            get
            {
                return this._TableHeight;
            }
            set
            {
                this._TableHeight = value;
            }
        }

        public int TableWeight
        {
            get
            {
                return this._TableWeight;
            }
            set
            {
                this._TableWeight = value;
            }
        }

        [Category("Appearance"), DefaultValue(""), Bindable(true)]
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

        public string Url
        {
            get
            {
                return this.url;
            }
            set
            {
                this.url = value;
            }
        }
    }
}

