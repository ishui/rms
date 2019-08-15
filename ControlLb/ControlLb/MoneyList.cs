namespace ControlLb
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using RmsPM.BLL;

    [ToolboxData("<{0}:MoneyList runat=server></{0}:MoneyList>"), DefaultProperty("Text")]
    public class MoneyList : DropDownList
    {
        [DefaultValue(""), Bindable(true), Category("Appearance")]
        private bool _AutoRun = true;
        public string _DefaultSelectText = "人民币";
        private string _DefaultSelectValue = null;
        private string _DictionaryName = "币种";

        public void BindSource()
        {
            try
            {
                PageFacade.LoadDictionarySelect(this, this._DictionaryName, this._DefaultSelectText, this._DefaultSelectValue, "");
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }

        public bool AutoRun
        {
            get
            {
                return this._AutoRun;
            }
            set
            {
                this._AutoRun = value;
            }
        }

        public string DefaultSelectText
        {
            get
            {
                return this._DefaultSelectText;
            }
            set
            {
                this._DefaultSelectText = value;
            }
        }

        public string DefaultSelectValue
        {
            get
            {
                return this._DefaultSelectValue;
            }
            set
            {
                this._DefaultSelectValue = value;
            }
        }

        public string DictionaryName
        {
            get
            {
                return this._DictionaryName;
            }
            set
            {
                this._DictionaryName = value;
            }
        }
    }
}

