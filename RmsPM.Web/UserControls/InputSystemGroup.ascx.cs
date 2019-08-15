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
    /// InputSystemGroup 的摘要说明。
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

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		设计器支持所需的方法 - 不要使用代码编辑器
        ///		修改此方法的内容。
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
                ApplicationLog.WriteLog(this.ToString(), ex, "初始化页面失败");
                Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
            }
        }

        /// <summary>
        /// 设置大类代码，初始化
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
                ApplicationLog.WriteLog(this.ToString(), ex, "初始化页面失败");
                Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面失败：" + ex.Message));
            }
        }

        public string ClassCode
        {
            get { return this.txtClassCode.Value; }
            set { SetClass(value); }
        }

        /// <summary>
        /// 设置代码
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
                                this.txtHint.Value = "不是末级类别 ！";
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
        /// 是否可任意选择结点
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
        /// 选择值变化时触发
        /// </summary>
        public event EventHandler Change;
        /// <summary>
        /// 服务器端事件触发开关（默认不触发）
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
