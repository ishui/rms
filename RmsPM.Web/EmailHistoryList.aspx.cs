using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RmsPM.BLL;

namespace RmsPM.Web
{
    public partial class EmailHistoryList : System.Web.UI.Page
    {
        #region EmailType���ʼ�����
        private string _EmailType = "";

        public string EmailType
        {
            get { return _EmailType; }
            set { _EmailType = value; }
        }
        #endregion

        #region MasterCode���������
        private string _MasterCode = "";

        public string MasterCode
        {
            get { return _MasterCode; }
            set { _MasterCode = value; }
        }
        #endregion
        
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
            LoadEvent();

        }
        private void LoadEvent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            MasterCode = Request["MasterCode"];
            EmailType = Request["EmailType"];

            LoadEmailHistory();
        }

        /// <summary>
        /// �����ʼ��б�
        /// </summary>
        private void LoadEmailHistory()
        {
            EmailHistory cEmailHistory = new EmailHistory();
            if (EmailType != "")
            {
                cEmailHistory.EmailType = EmailType;
            }
            if (MasterCode != "")
            {
                cEmailHistory.MasterCode = MasterCode;
            }

            string Receiver = "";
            DataTable dt = new DataTable();
            switch (EmailType)
            { 
                case "BiddingEmitTo":
                    dt = SupplierRule.GetSupplierByCode(Request["SupplierCode"]).CurrentTable;
                    if (dt.Rows.Count > 0)
                    {
                        Receiver = dt.Rows[0]["Email"].ToString();
                    }
                    dt.Dispose();
                    break;
                default:
                    Receiver = "";
                    break;
            }
            if (Receiver != "")
            {
                cEmailHistory.Receiver = Receiver;
            }

            dt = cEmailHistory.GetEmailHistorys();
            dt.Columns.Add(new DataColumn("EmailTypeCN", Type.GetType("System.String")));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["EmailTypeCN"] = GetEmailTypeCNByEmailType(dt.Rows[i]["EmailType"].ToString());
            }

            this.gvEmailHistoryList.DataSource = dt;
            this.gvEmailHistoryList.AutoGenerateColumns = false;
            this.gvEmailHistoryList.Columns[6].Visible = false;
            this.gvEmailHistoryList.DataBind();
        }

        private string GetEmailTypeCNByEmailType(string EmailType)
        {
            switch (EmailType)
            {
                case "BiddingEmitTo":
                    return "��Ͷ��֪ͨ";
                    break;
                default:
                    return "";
                    break;
            }
        }
    }
}
