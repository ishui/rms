namespace Rms.ControlLb
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [DefaultProperty("Text"), ToolboxData("<{0}:TextBox_Lable runat=server></{0}:TextBox_Lable>")]
    public class TextBox_Lable : TextBox
    {
        private TextBox tb = new TextBox();

        protected override void Render(HtmlTextWriter output)
        {
            if (!this.IsEditMode)
            {
                output.AddAttribute(HtmlTextWriterAttribute.Width, this.Width.ToString());
                output.AddAttribute(HtmlTextWriterAttribute.Height, this.Height.ToString());
                output.AddAttribute(HtmlTextWriterAttribute.Id, this.ID);
                output.AddAttribute(HtmlTextWriterAttribute.Style, "Width:" + this.Width.ToString() + ";Height:" + this.Height.ToString());
                output.RenderBeginTag(HtmlTextWriterTag.Div);
                output.Write(this.Text);
                output.RenderEndTag();
            }
            else if (this.IsValidator)
            {
                output.RenderBeginTag(HtmlTextWriterTag.Table);
                output.RenderBeginTag(HtmlTextWriterTag.Tr);
                output.AddAttribute(HtmlTextWriterAttribute.Width, this.Width.ToString());
                output.AddAttribute(HtmlTextWriterAttribute.Style, "BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none");
                output.RenderBeginTag(HtmlTextWriterTag.Td);
                base.Attributes.Add("onblur", "IsValidator(this.name,'" + this.ErrorMessage + "')");
                base.Render(output);
                output.RenderEndTag();
                output.AddAttribute(HtmlTextWriterAttribute.Width, "60");
                output.AddAttribute(HtmlTextWriterAttribute.Style, "BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none");
                output.RenderBeginTag(HtmlTextWriterTag.Td);
                output.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID + "Er");
                output.RenderBeginTag(HtmlTextWriterTag.Div);
                output.Write("<FONT color='red'>*</FONT>");
                output.RenderEndTag();
                output.RenderEndTag();
                output.RenderEndTag();
                output.RenderEndTag();
            }
            else
            {
                base.Render(output);
            }
        }

        private void Validator()
        {
            if (this.IsValidator && (this.Text == ""))
            {
                HtmlGenericControl control = (HtmlGenericControl) this.Page.FindControl(this.UniqueID + "Er");
                control.InnerHtml = this.ErrorMessage;
                throw new Exception("值为空");
            }
        }

        [Description("是否进行验证"), Bindable(true), DefaultValue("")]
        public string ErrorMessage
        {
            get
            {
                return ((this.ViewState["ErrorMessage"] == null) ? "不允许为空" : ((string) this.ViewState["ErrorMessage"]));
            }
            set
            {
                this.ViewState["ErrorMessage"] = value;
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

        [DefaultValue(""), Bindable(true), Description("是否进行验证")]
        public bool IsValidator
        {
            get
            {
                return ((this.ViewState["IsValidator"] == null) || ((bool) this.ViewState["IsValidator"]));
            }
            set
            {
                this.ViewState["IsValidator"] = value;
            }
        }

        [Bindable(true), Description("将数据提交服务器的Button"), DefaultValue("")]
        public string ReturnButton
        {
            get
            {
                return ((this.ViewState["ReturnButton"] == null) ? "" : ((string) this.ViewState["ReturnButton"]));
            }
            set
            {
                this.ViewState["ReturnButton"] = value;
            }
        }
    }
}

