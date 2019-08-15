namespace RmsPM.Web.UserControls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using Rms.ORMap;

    /// <summary>
    /// InputSystemGroup ��ժҪ˵����
    /// </summary>
    public partial class InputSystemGroup : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.divName.InnerText = this.txtName.Value;
            this.divHint.InnerText = this.txtHint.Value;

            if (this.Visible)
            {
                this.txtInput.Attributes["ClientID"] = this.ClientID;

                //				string reload = Rms.Web.JavaScript.ScriptStart;
                //				reload += @"var ClientID = '" + this.ClientID + "';" + "\n" ;
                //				reload += Rms.Web.JavaScript.ScriptEnd;
                //				Response.Write(reload);

                if (!Page.IsPostBack)
                {
                    IniPage();
                }
            }
        }

        #region Web ������������ɵĴ���
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: �õ����� ASP.NET Web ���������������ġ�
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
        ///		�޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        public Boolean Enable
        {
            set
            {
                hid_Enable.Checked = value;
                div_SearchButton.Visible = hid_Enable.Checked;
                this.txtInput.Visible = hid_Enable.Checked;
            }
            get
            {
                return hid_Enable.Checked;
            }
        }

        public string ProjectCode
        {
            get
            {
                return Request["ProjectCode"] + "";
            }
        }

        private void IniPage()
        {
            try
            {
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "��ʼ��ҳ��ʧ��");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ��ʧ�ܣ�" + ex.Message));
            }
        }

        /// <summary>
        /// ���ô�����룬��ʼ��
        /// </summary>
        /// <param name="ClassCode"></param>
        private void SetClass(string ClassCode)
        {
            try
            {
                this.txtClassCode.Value = ClassCode;
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "��ʼ��ҳ��ʧ��");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ��ʧ�ܣ�" + ex.Message));
            }
        }

        public string ClassCode
        {
            get { return this.txtClassCode.Value; }
            set { SetClass(value); }
        }

        /// <summary>
        /// ���ô���
        /// </summary>
        /// <param name="code"></param>
        private void SetCode(string code)
        {
            try
            {
                this.txtCode.Value = code;
                this.txtHint.Value = "";

                if (ClassCode == "1603")
                {
                    EntityData entity = DAL.EntityDAO.OADAO.GetOAFileTypeByCode(code);
                    if (entity.HasRecord())
                    {
                        this.txtName.Value = entity.GetString("TypeName");
                        this.txtSortID.Value = entity.GetString("OAFileTypeCode");
                        this.txtFullID.Value = "";
                    }
                    entity.Dispose();
                }
                else
                {
                    EntityData entity = DAL.EntityDAO.SystemManageDAO.GetV_SystemGroupByCode(code);
                    if (entity.HasRecord())
                    {
                        this.txtName.Value = entity.GetString("FullName");
                        this.txtSortID.Value = entity.GetString("SortID");
                        this.txtFullID.Value = entity.GetString("FullID");

                        if (!SelectAllLeaf)
                        {
                            if (!BLL.SystemGroupRule.IsSystemGroupLeafNode(code))
                            {
                                this.txtHint.Value = "����ĩ����� ��";
                            }
                        }
                    }
                    entity.Dispose();
                }

                this.txtInput.Value = this.txtSortID.Value;

                this.divName.InnerText = this.txtName.Value;
                this.divHint.InnerText = this.txtHint.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Value
        {
            get { return this.txtCode.Value; }
            set { SetCode(value); }
        }

        public string Text
        {
            get { return this.txtName.Value; }
        }

        public string Hint
        {
            get { return this.txtHint.Value; }
        }

        public string SortID
        {
            get { return this.txtSortID.Value; }
        }

        public string FullID
        {
            get { return this.txtFullID.Value; }
        }

        /// <summary>
        /// �Ƿ������ѡ����
        /// </summary>
        public bool SelectAllLeaf
        {
            get { return (this.txtSelectAllLeaf.Value == "1"); }
            set { this.txtSelectAllLeaf.Value = (value ? "1" : ""); }
        }

        protected string imagePath = "../images/";
        public string ImagePath
        {
            set
            {
                this.imagePath = value;
            }
        }

        /// <summary>
        /// ѡ��ֵ�仯ʱ����
        /// </summary>
        public event EventHandler Change;
        /// <summary>
        /// ���������¼��������أ�Ĭ�ϲ�������
        /// </summary>
        public bool AutoPostBack
        {
            get
            {
                if (this.ViewState["_AutoPostBack"] == null)
                    return false;
                return (bool)this.ViewState["_AutoPostBack"];
            }
            set
            {
                this.ViewState["_AutoPostBack"] = value;
                this.btnChange.Visible = true;
            }
        }

        protected void btnChange_ServerClick(object sender, EventArgs e)
        {
            this.Change(this, EventArgs.Empty);
        }
    }
}
