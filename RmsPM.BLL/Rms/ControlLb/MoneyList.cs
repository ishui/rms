namespace Rms.ControlLb
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    [DefaultProperty("Text"), ToolboxData("<{0}:MoneyList runat=server></{0}:MoneyList>")]
    public class MoneyList : DropDownList
    {
        [Bindable(true), DefaultValue(""), Category("Appearance")]
        private bool _AutoRun = true;
        public string _DefaultSelectText = "";
        private string _DefaultSelectValue = null;
        private string _DictionaryName = "币种";
        private string _MoneyType;
        private string _MoneyTypeID;
        private string _ProjectCode = "";

        public void BindSource()
        {
            try
            {
                if (this.Items.Count > 0)
                {
                    this.SetValue();
                }
                else
                {
                    EntityData dictionaryItemByNameProject = SystemManageDAO.GetDictionaryItemByNameProject(this._DictionaryName, this._ProjectCode);
                    int count = dictionaryItemByNameProject.CurrentTable.Rows.Count;
                    this.DataSource = dictionaryItemByNameProject;
                    this.DataTextField = "Name";
                    this.DataValueField = "DictionaryItemCode";
                    this.DataBind();
                    dictionaryItemByNameProject.Dispose();
                }
                this.DefaultSelect();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void DefaultSelect()
        {
            if (this.DefaultSelectValue != null)
            {
                this.SelectedIndex = this.Items.IndexOf(this.Items.FindByValue(this.DefaultSelectValue));
            }
            else
            {
                this.SelectedIndex = this.Items.IndexOf(this.Items.FindByText(this.DefaultSelectText));
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this._AutoRun)
            {
                this.BindSource();
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }

        public void SetValue()
        {
            this.MoneyType = this.Items[this.SelectedIndex].Text;
            this.MoneyTypeID = this.Items[this.SelectedIndex].Value;
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

        public string MoneyType
        {
            get
            {
                return this._MoneyType;
            }
            set
            {
                this._MoneyType = value;
            }
        }

        public string MoneyTypeID
        {
            get
            {
                return this._MoneyTypeID;
            }
            set
            {
                this._MoneyTypeID = value;
            }
        }
    }
}

