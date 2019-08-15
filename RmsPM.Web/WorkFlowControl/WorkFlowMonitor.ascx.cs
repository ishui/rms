namespace RmsPM.Web.WorkFlowControl
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    using RmsPM.DAL.QueryStrategy;
    using Rms.ORMap;
    using Rms.WorkFlow;
    using System.Collections;

    /// *******************************************************************************************
    /// <summary>
    ///		WorkFlowMonitor ��ժҪ˵�������̼�������б����
    /// </summary>
    /// *******************************************************************************************
    public partial class WorkFlowMonitor : System.Web.UI.UserControl
    {
        /// <summary>
        /// ���̴���
        /// </summary>
        private string _ProcedureName = "";
        /// <summary>
        /// ���̴���
        /// </summary>
        public string ProcedureName
        {
            get
            {
                return _ProcedureName;
            }
            set
            {
                _ProcedureName = value;
            }
        }
        /// <summary>
        /// �����û�
        /// </summary>
        private string _ActUser = "";
        /// <summary>
        /// �����û�
        /// </summary>
        public string ActUser
        {
            get
            {
                return _ActUser;
            }
            set
            {
                _ActUser = value;
            }
        }
        /// <summary>
        /// �����û�
        /// </summary>
        private string _IsNotActUser = "";
        /// <summary>
        /// �����û�
        /// </summary>
        public string IsNotActUser
        {
            get
            {
                return _IsNotActUser;
            }
            set
            {
                _IsNotActUser = value;
            }
        }

        /// <summary>
        /// ����״̬����
        /// </summary>
        private string _Status = "";
        /// <summary>
        /// ����״̬����
        /// </summary>
        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }
        /// <summary>
        /// �����ˣ�������ʹ�ã�
        /// </summary>
        private string _SendActUser = "";
        /// <summary>
        /// �����ˣ�������ʹ�ã�
        /// </summary>
        public string SendActUser
        {
            get
            {
                return _SendActUser;
            }
            set
            {
                _SendActUser = value;
            }
        }

        /// <summary>
        /// ����״̬����
        /// </summary>
        private string _CaseStatus = "";
        /// <summary>
        /// ����״̬����
        /// </summary>
        public string CaseStatus
        {
            get
            {
                return _CaseStatus;
            }
            set
            {
                _CaseStatus = value;
            }
        }

        /// <summary>
        /// ����Ψһ��ʾ
        /// </summary>
        private Boolean _DistinctWorkFlow = false;
        /// <summary>
        /// ����״̬����
        /// </summary>
        public Boolean DistinctWorkFlow
        {
            get
            {
                return _DistinctWorkFlow;
            }
            set
            {
                _DistinctWorkFlow = value;
            }
        }

        /// <summary>
        /// ���̹���
        /// </summary>
        private DataTable _Filter = null;
        /// <summary>
        /// ���̹���
        /// </summary>
        public DataTable Filter
        {
            get
            {
                return _Filter;
            }
            set
            {
                _Filter = value;
            }
        }

        /// <summary>
        /// ��ҳ��ʾ
        /// </summary>
        private Boolean _AllowPaging = true;
        /// <summary>
        /// ��ҳ��ʾ
        /// </summary>
        public Boolean AllowPaging
        {
            get
            {
                return _AllowPaging;
            }
            set
            {
                _AllowPaging = value;
            }
        }
        private string _CaseCode = "";
        public string CaseCode { get { return _CaseCode; } set { _CaseCode = value; } }
        private string _TaskName = "";
        public string TaskName { get { return _TaskName; } set { _TaskName = value; } }
        private string _Title = "";
        public string Title { get { return _Title; } set { _Title = value; } }
        private string _ProjectCode = "";
        public string ProjectCode { get { return _ProjectCode; } set { _ProjectCode = value; } }
        private string _ucPerson = "";
        public string ucPerson { get { return _ucPerson; } set { _ucPerson = value; } }
        private string _DateStart = "";
        public string DateStart { get { return _DateStart; } set { _DateStart = value; } }
        private string _DateEnd = "";
        public string DateEnd { get { return _DateEnd; } set { _DateEnd = value; } }
        private string _ucToPerson = "";
        public string ucToPerson { get { return _ucToPerson; } set { _ucToPerson = value; } }
        private string _CalendarStart = "";
        public string CalendarStart { get { return _CalendarStart; } set { _CalendarStart = value; } }
        private string _CalendarEnd = "";
        public string CalendarEnd { get { return _CalendarEnd; } set { _CalendarEnd = value; } }

        /// ****************************************************************************
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if(!IsPostBack)
            {
                this.dgList.Columns[11].Visible = ((User)(Session["User"])).HasOperationRight("090102");
                this.dgList.Columns[12].Visible = this.dgList.Columns[11].Visible;

                //temp,����Ҫ��ʱ��Ͻ� update by kenny 20070209
                string company = System.Configuration.ConfigurationManager.AppSettings["PMName"].ToLower();
                switch (company)
                {
                    case "tianyangoa":
                        this.dgList.Columns[7].Visible = true;
                        break;
                    default:
                        this.dgList.Columns[7].Visible = false;
                        break;
                }
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// ****************************************************************************
        private void IniPage()
        {
        }

        /// ****************************************************************************
        /// <summary>
        /// ���ݼ���
        /// </summary>
        /// ****************************************************************************
        private void LoadData()
        {
            try
            {
                string sql = (string)this.ViewState["SqlString"];
                QueryAgent qa = new QueryAgent();
                EntityData entity = qa.FillEntityData("WorkFlowAct", sql);
                qa.Dispose();
                this.dgList.DataSource = entity.CurrentTable;
            
                this.dgList.DataBind();
                this.gpControl.RowsCount = entity.CurrentTable.Rows.Count.ToString();
                entity.Dispose();

               

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// ****************************************************************************
        private void BuildSqlString()
        {

            WorkFlowActStrategyBuilder sb = new WorkFlowActStrategyBuilder();

            switch (this.Status)
            {
                case "":
                    sb.AddStrategy(new Strategy(WorkFlowActStrategyName.Status, "'DealWith','Begin'"));
                    break;
                case "All":
                    break;
                default:
                    char[] delimiter = ",".ToCharArray();
                    string ud_sRule = "";

                    foreach (string tmp in this.Status.Split(delimiter))
                    {
                        if (ud_sRule != "")
                        {
                            ud_sRule += ",'" + tmp + "'";
                        }
                        else
                        {
                            ud_sRule = "'" + tmp + "'";
                        }
                    }
                    if (ud_sRule != "")
                    {
                        sb.AddStrategy(new Strategy(WorkFlowActStrategyName.Status, ud_sRule));
                    }
                    else
                    {
                        sb.AddStrategy(new Strategy(WorkFlowActStrategyName.Status, "'DealWith','Begin'"));
                    }
                    break;
            }

            switch (this.CaseStatus)
            {
                case "":
                    break;
                case "All":
                    break;
                default:
                    char[] delimiter = ",".ToCharArray();
                    string ud_sRule = "";
                    foreach (string tmp in this.CaseStatus.Split(delimiter))
                    {
                        if (ud_sRule != "")
                        {
                            ud_sRule += ",'" + tmp + "'";
                        }
                        else
                        {
                            ud_sRule = "'" + tmp + "'";
                        }
                    }
                    if (ud_sRule != "")
                    {
                        sb.AddStrategy(new Strategy(WorkFlowActStrategyName.CaseStatus, ud_sRule));
                    }
                    break;
            }

            if (this._ProcedureName != "")
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.ProcedureCodeIn, BLL.WorkFlowRule.GetProcedureCodeListByName(this._ProcedureName)));
            if (this._ActUser != "")
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.IsCaseActUser, this._ActUser));
            if (this._SendActUser != "")
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.SendActUser, this._SendActUser));
            if (this._IsNotActUser != "")
            {
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.IsNotActUser, this._IsNotActUser));

                ArrayList arA = new ArrayList();
                arA.Add("090201");
                arA.Add(((User)Session["User"]).UserCode);
                arA.Add(((User)Session["User"]).BuildStationCodes());
                arA.Add("");
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.AccessRange, arA));
            }
            if (this._IsNotActUser == "" && this._ActUser == "")
            {
                ArrayList arA = new ArrayList();
                arA.Add("090201");
                arA.Add(((User)Session["User"]).UserCode);
                arA.Add(((User)Session["User"]).BuildStationCodes());
                arA.Add(((User)Session["User"]).UserCode);
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.AccessRange, arA));
            }

            if (this._CaseCode != "")
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.FlowNumber, this._CaseCode));
            if (this._TaskName != "")
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.CurrentTaskName, this._TaskName));
            if (this._Title != "")
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.Title, this._Title));
            if (this._ProjectCode != "")
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.ProjectCode, this._ProjectCode));
            if (this._ucPerson != "")
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.FromUserCode, this._ucPerson));
            if (this._DateStart != "" || this._DateEnd != "")
            {
                if (this._DateStart == "")
                    this._DateStart = DateTime.MinValue.ToString();
                if (this._DateEnd == "")
                    this._DateEnd = DateTime.MaxValue.ToString();
                ArrayList arrlist = new ArrayList();
                arrlist.Add(this._DateStart);
                arrlist.Add(this._DateEnd);
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.FromDate, arrlist));
            }
            if (this._ucToPerson != "")
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.ActUserCode, this._ucToPerson));
            if (this._CalendarStart != "" || this._CalendarEnd != "")
            {
                if (this._CalendarStart == "")
                    this._CalendarStart = DateTime.MinValue.ToString();
                if (this._CalendarEnd == "")
                    this._CalendarEnd = DateTime.MaxValue.ToShortDateString();
                ArrayList arrlist = new ArrayList();
                arrlist.Add(this._CalendarStart);
                arrlist.Add(this._CalendarEnd);
                sb.AddStrategy(new Strategy(WorkFlowActStrategyName.SignDate, arrlist));
            }

            sb.AddStrategy(new Strategy(WorkFlowActStrategyName.CopyState, "0"));

            string sql = sb.BuildMainQueryString();
            this.ViewState["SelectString"] = sql;
            //����
            string sortsql = BLL.GridSort.GetSortSQL(ViewState, "FromDate desc");
            if (sortsql != "")
            {
                sql = sql + " order by " + sortsql;
            }
            this.ViewState.Add("SqlString", sql);
            this.ViewState["sorttype"] = " desc";
            this.ViewState["sortfield"] = "FromDate";
        }

        /// ****************************************************************************
        /// <summary>
        /// ��ҳ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void gpControl_PageIndexChange(object sender, System.EventArgs e)
        {
            LoadData();
        }
        /// ****************************************************************************
        /// <summary>
        /// �ﶨ����
        /// </summary>
        /// ****************************************************************************
        public void DataBound()
        {
            //this.dgList.CurrentPageIndex = 0;
            this.gpControl.CurrentPageIndex = 1;
            BuildSqlString();
            LoadData();
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
        protected void dgList_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            this.gpControl.CurrentPageIndex = 1;
            string sql = "";

            if (this.ViewState["sorttype"] == "")
            {
                this.ViewState["sorttype"] = " ";
                this.ViewState["sortfield"] = e.SortExpression;
            }
            else
            {
                if (this.ViewState["sortfield"].ToString() == e.SortExpression)
                {
                    if (this.ViewState["sorttype"].ToString() == " ")
                    {
                        this.ViewState["sorttype"] = " desc";
                    }
                    else
                    {
                        this.ViewState["sorttype"] = " ";
                    }
                }
                else
                {
                    this.ViewState["sortfield"] = e.SortExpression;
                    this.ViewState["sorttype"] = " desc";
                }
            }

            string sortsql = this.ViewState["sortfield"].ToString() + this.ViewState["sorttype"].ToString();
            sql = this.ViewState["SelectString"].ToString() + " order by " + sortsql;
            this.ViewState.Add("SqlString", sql);
            LoadData();
        }
    }
}
