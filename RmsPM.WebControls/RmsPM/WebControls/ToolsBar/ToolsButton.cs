namespace RmsPM.WebControls.ToolsBar
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [DefaultEvent("Click"), DefaultProperty("Text"), ToolboxData("<{0}:ToolsButton runat=server></{0}:ToolsButton>")]
    public class ToolsButton : Label, INamingContainer, IPostBackEventHandler
    {
        private bool g_bIsServerEvent;
        private static readonly object g_oClickEvent = new object();
        private string g_strEvent;
        private string imageUrl = "";

        public event EventHandler Click
        {
            add
            {
                base.Events.AddHandler(g_oClickEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(g_oClickEvent, value);
            }
        }

        protected virtual void OnClick(EventArgs e)
        {
            EventHandler handler = (EventHandler) base.Events[g_oClickEvent];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.EnsureChildControls();
            base.OnLoad(e);
        }

        public void RaisePostBackEvent(string m_strEvtArgument)
        {
            this.OnClick(new EventArgs());
        }

        protected override void Render(HtmlTextWriter output)
        {
            string text = "";
            text = ((text + "<table border=\"0\" cellspacing\"0\" cellpadding=\"0\" style=\"cursor:hand\"") + " onclick=\"" + (this.IsServerEvent ? this.Page.GetPostBackEventReference(this) : this.Event) + "\"") + "></tr>";
            if (this.ImageUrl != "")
            {
                text = ((text + "<td align=\"center\" valign=\"middle\">") + "&nbsp;<img src=\"" + this.ImageUrl + "\" border=\"0\" />") + "</td>";
            }
            text = (text + "<td nowrap align=\"center\" valign=\"middle\">&nbsp;" + base.Text) + "&nbsp;</td>" + "</tr></table>";
            output.Write(text);
        }

        [Category("Appearance"), DefaultValue(""), Bindable(true)]
        public string Event
        {
            get
            {
                return this.g_strEvent;
            }
            set
            {
                this.g_strEvent = value;
            }
        }

        [Bindable(true), Category("Appearance"), DefaultValue("")]
        public string ImageUrl
        {
            get
            {
                return this.imageUrl;
            }
            set
            {
                this.imageUrl = value;
            }
        }

        [Bindable(true), DefaultValue(false), Category("Appearance")]
        public bool IsServerEvent
        {
            get
            {
                return this.g_bIsServerEvent;
            }
            set
            {
                this.g_bIsServerEvent = value;
            }
        }
    }
}

