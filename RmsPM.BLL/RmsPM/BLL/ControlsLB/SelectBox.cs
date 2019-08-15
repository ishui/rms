namespace RmsPM.BLL.ControlsLB
{
    using System;
    using System.ComponentModel;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:SelectBox runat=server></{0}:SelectBox>"), DefaultProperty("Text")]
    public class SelectBox : WebControl, INamingContainer
    {
        private ImageButton IBSearch = new ImageButton();
        private HtmlInputText inputCode = new HtmlInputText();
        private TextBox inputName = new TextBox();
        private HtmlInputText inputValue = new HtmlInputText();
        private Label showLable = new Label();
        private HtmlTable Table = new HtmlTable();
        private HtmlTableRow Tr = new HtmlTableRow();

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            this.EnsureChildControls();
            base.AddAttributesToRender(writer);
            writer.Write(this.OutPutJavaScript(this.ReturnJavaScript() + this.OpenMiddleWindows()));
            this.showLable.Text = this.Text + "(" + this.Value + ")";
            this.Table.Visible = this.IsEditMode;
            this.showLable.Visible = !this.IsEditMode;
        }

        protected void AddControlToTable(Control controlName)
        {
            HtmlTableCell child = new HtmlTableCell();
            child.Style.Add("BORDER-TOP-STYLE", "none");
            child.Style.Add("BORDER-RIGHT-STYLE", "none");
            child.Style.Add("BORDER-LEFT-STYLE", "none");
            child.Style.Add("BORDER-BOTTOM-STYLE", "none");
            child.Align = "left";
            child.Controls.Add(controlName);
            this.Tr.Controls.Add(child);
        }

        protected override void CreateChildControls()
        {
            this.inputName.ID = this.ID + "_N";
            this.AddControlToTable(this.inputName);
            this.inputCode.ID = this.ID + "_C";
            this.inputCode.Style.Add("display", "none");
            this.AddControlToTable(this.inputCode);
            this.inputValue.ID = this.ID + "_V";
            this.inputValue.Style.Add("display", "none");
            this.AddControlToTable(this.inputValue);
            this.IBSearch.ID = this.ID + "_S";
            this.IBSearch.ImageUrl = this.ButtonImage;
            this.IBSearch.Attributes.Add("OnClick", "Search(this.id);return false");
            this.AddControlToTable(this.IBSearch);
            this.Table.Controls.Add(this.Tr);
            this.Controls.Add(this.Table);
            this.showLable.Text = this.Text + "(" + this.Code + ")";
            this.Controls.Add(this.showLable);
        }

        protected string OpenMiddleWindows()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("function Search(id)\n{\n" + this.ReturnMiddleWindowsScript(this.Url + "?id='+id+'&ReturnValue=GetReturnValue&xmlUrl=" + this.XmlUrl) + "\n return false;}\n");
            return builder.ToString();
        }

        protected string OutPutJavaScript(string code)
        {
            return ("<SCRIPT language=\"javascript\">" + code + "</SCRIPT>");
        }

        protected string ReturnJavaScript()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("function GetReturnValue(text,value,Code,id)\n{\nleid=id.substring(0,id.length-1);document.all(leid+'N').value=text;\ndocument.all(leid+'V').value=value;\ndocument.all(leid+'C').value=Code;\n}\n");
            return builder.ToString();
        }

        protected string ReturnMiddleWindowsScript(string strUrl)
        {
            return ("window.open('" + strUrl + "','strName',\"width=640,height=480,fullscreen=0,top=\"+(window.screen.height-480)/2+\",left=\"+(window.screen.width-640)/2+\",menubar=no,toolbar=no,scrollbars=yes,status=no,titlebar=no,resizable=yes,location=no\");");
        }

        [Category("样式控件"), Bindable(true), Description("Box样式")]
        public string BoxCssClass
        {
            get
            {
                return this.inputName.CssClass;
            }
            set
            {
                this.inputName.CssClass = value;
            }
        }

        [Description("Box高度"), Category("样式控件"), Bindable(true)]
        public Unit BoxHeight
        {
            get
            {
                return this.inputName.Height;
            }
            set
            {
                this.inputName.Height = value;
            }
        }

        [Category("样式控件"), Bindable(true), Description("Box宽度")]
        public Unit BoxWith
        {
            get
            {
                return this.inputName.Width;
            }
            set
            {
                this.inputName.Width = value;
            }
        }

        [Description("图形URL"), Category("样式控件"), Bindable(true)]
        public string ButtonImage
        {
            get
            {
                return this.IBSearch.ImageUrl;
            }
            set
            {
                this.IBSearch.ImageUrl = value;
            }
        }

        [Bindable(true), Description("Code"), Category("公有属性")]
        public string Code
        {
            get
            {
                return this.inputCode.Value;
            }
            set
            {
                this.inputCode.Value = value;
            }
        }

        [Category("样式控件"), Bindable(true), Description("是否处于可编辑状态")]
        public bool IsEditMode
        {
            get
            {
                return ((this.ViewState["IsEditMode"] != null) && ((bool) this.ViewState["IsEditMode"]));
            }
            set
            {
                this.ViewState["IsEditMode"] = value;
            }
        }

        [Category("样式控件"), Description("Lable样式"), Bindable(true)]
        public string LableCssClass
        {
            get
            {
                return this.showLable.CssClass;
            }
            set
            {
                this.showLable.CssClass = value;
            }
        }

        [Bindable(true), Category("公有属性"), Description("ProjectCode")]
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

        [Category("公有属性"), Bindable(true), Description("Name")]
        public string Text
        {
            get
            {
                return this.inputName.Text;
            }
            set
            {
                this.inputName.Text = value;
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

        [Description("Value"), Bindable(true), Category("公有属性")]
        public string Value
        {
            get
            {
                return this.inputValue.Value;
            }
            set
            {
                this.inputValue.Value = value;
            }
        }

        public string XmlUrl
        {
            get
            {
                string text = (string) this.ViewState["XmlUrl"];
                return ((text == null) ? string.Empty : text);
            }
            set
            {
                this.ViewState["XmlUrl"] = value;
            }
        }
    }
}

