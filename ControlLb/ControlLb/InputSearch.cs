namespace ControlLb
{
    using System;
    using System.ComponentModel;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:InputSearch runat=server></{0}:InputSearch>"), DefaultProperty("Text")]
    public class InputSearch : WebControl
    {
        protected string _ClientID = "";
        protected string _ImageUrl = "../Images/ToolsItemSearch.Gif";
        protected string _Text;
        private int _TextHeight = 0x12;
        private int _TextWidth = 100;
        protected string _Title = "";
        protected string _XmlUrl = "";
        protected TextBox InputBox;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected string OpenMiddleWindows()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("function OpenNewSelectBox()\n{\nSelect=document.getElementById(\"ViseMessage_Control1_Select_ViseType\");if(Select.value==\"0\"){" + this.ReturnMiddleWindowsScript(this.Url + "?ReturnValue=GetReturnValue&xmlUrl=" + this.XmlUrl + "&ProjectCode=" + this.ProjectCode) + "\n}else if(Select.value==\"1\"){\talert(\"非合同签证状态,不充许选合同\");}}\n");
            return builder.ToString();
        }

        protected string OutPutJavaScript(string code)
        {
            return ("<SCRIPT language=\"javascript\">" + code + "</SCRIPT>");
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.AddAttribute(HtmlTextWriterAttribute.Width, this.Width.ToString());
            output.AddAttribute(HtmlTextWriterAttribute.Height, this.Height.ToString());
            output.RenderBeginTag(HtmlTextWriterTag.Table);
            output.RenderBeginTag(HtmlTextWriterTag.Tr);
            output.RenderBeginTag(HtmlTextWriterTag.Td);
            output.Write(this._Title);
            output.RenderEndTag();
            output.RenderBeginTag(HtmlTextWriterTag.Td);
            output.Write("<input type=\"text\" value=" + this.Text + " id=\"" + this.UniqueID + "Text\"  name=\"" + this.UniqueID + "Text\" style=width:" + this._TextWidth.ToString() + ";Height:" + this._TextHeight.ToString() + ">");
            output.Write("<input type=\"hidden\" value=" + this.Value + "  id=\"" + this.UniqueID + "Value\" name=\"" + this.UniqueID + "Value\" >");
            output.Write("<input type=\"hidden\" value=" + this.Remark + " id=\"" + this.UniqueID + "Remark\" name=\"" + this.UniqueID + "Remark\" >");
            output.Write("<input type=\"hidden\"  value=" + this.State + " id=\"" + this.UniqueID + "State\" name=\"" + this.UniqueID + "State\" >");
            output.RenderEndTag();
            output.RenderBeginTag(HtmlTextWriterTag.Td);
            output.Write("<A href=\"#\" onclick=\"OpenNewSelectBox();return false;\"><img src=\"" + this._ImageUrl + "\" border=\"0\"></A>");
            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();
            output.Write(this.OutPutJavaScript(this.ReturnJavaScript() + this.OpenMiddleWindows()));
        }

        protected string ReturnJavaScript()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("function GetReturnValue(text,value,remark,state)\n{\ndocument.getElementById(\"" + this.UniqueID + "Text\").value=text;\ndocument.getElementById(\"" + this.UniqueID + "Value\").value=value;\ndocument.getElementById(\"" + this.UniqueID + "Remark\").value=remark;\ndocument.getElementById(\"" + this.UniqueID + "State\").value=state;\n}\n");
            return builder.ToString();
        }

        protected string ReturnMiddleWindowsScript(string strUrl)
        {
            return ("window.open('" + strUrl + "','strName',\"width=640,height=480,fullscreen=0,top=\"+(window.screen.height-480)/2+\",left=\"+(window.screen.width-640)/2+\",menubar=no,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no\");");
        }

        public void Search()
        {
        }

        protected void SetControlValue()
        {
        }

        public string ImageUrl
        {
            get
            {
                return this._ImageUrl;
            }
            set
            {
                this._ImageUrl = value;
            }
        }

        public string ProjectCode
        {
            get
            {
                string text = (string) this.ViewState["ProjectCode"];
                return ((text == null) ? string.Empty : text);
            }
            set
            {
                this.ViewState["ProjectCode"] = value;
            }
        }

        public string Remark
        {
            get
            {
                string text = (string) this.ViewState["Remark"];
                return ((text == null) ? string.Empty : text);
            }
            set
            {
                this.ViewState["Remark"] = value;
            }
        }

        public string State
        {
            get
            {
                string text = (string) this.ViewState["State"];
                return ((text == null) ? string.Empty : text);
            }
            set
            {
                this.ViewState["State"] = value;
            }
        }

        [DefaultValue(""), Category("Appearance"), Bindable(true)]
        public string Text
        {
            get
            {
                return this._Text;
            }
            set
            {
                this._Text = value;
            }
        }

        public int TextHeight
        {
            get
            {
                return this._TextHeight;
            }
            set
            {
                this._TextHeight = value;
            }
        }

        public int TextWidth
        {
            get
            {
                return this._TextWidth;
            }
            set
            {
                this._TextWidth = value;
            }
        }

        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this._Title = value;
            }
        }

        public string Url
        {
            get
            {
                string text = (string) this.ViewState["Url"];
                return ((text == null) ? string.Empty : text);
            }
            set
            {
                this.ViewState["Url"] = value;
            }
        }

        public string Value
        {
            get
            {
                string text = (string) this.ViewState["Value"];
                return ((text == null) ? string.Empty : text);
            }
            set
            {
                this.ViewState["Value"] = value;
            }
        }

        public string XmlUrl
        {
            get
            {
                return this._XmlUrl;
            }
            set
            {
                this._XmlUrl = value;
            }
        }
    }
}

