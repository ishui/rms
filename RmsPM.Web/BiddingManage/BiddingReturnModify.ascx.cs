
namespace RmsPM.Web.BiddingManage
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using RmsPM.Web.WorkFlowControl;
    using Rms.ORMap;
    using RmsPM.BLL;
    using RmsPM.BFL;
    using Infragistics.WebUI.WebDataInput;
    using System.Collections.Generic;
    using System.Configuration;


    /// <summary>
    ///		BiddingReturnModify ��ժҪ˵����
    /// </summary>
    public partial class BiddingReturnModify : BiddingControlBase
    {

        /// <summary>
        /// ҵ�����
        /// </summary>
        private string _BiddingCode = "";
        protected bool _IsReturnView = true;
        private bool _IsWSZTB;
        private int _EmitState = 0;
        //protected string nowtate="";


        /// <summary>
        /// �Ƿ�ʹ��������Ͷ��
        /// </summary>
         public bool IsWSZTB
        {
            get
            {
                if (_IsWSZTB == false)
                {
                    if (this.ViewState["_IsWSZTB"] != null)
                        return (bool)this.ViewState["_IsWSZTB"];
                }
                return _IsWSZTB;
            }
            set
            {
                _IsWSZTB = value;
                this.ViewState["_IsWSZTB"] = value;
            }
        }
      

        public int EmitState
        {
            get {

                return BiddingBFL.GetEmit_State(this.BiddingEmitCode); ; 
            }
        }

        /// <summary>
        /// ҵ�����
        /// </summary>
        public string BiddingCode
        {
            get
            {
                if (_BiddingCode == "")
                {
                    if (this.ViewState["_BiddingCode"] != null)
                        return this.ViewState["_BiddingCode"].ToString();
                    return "";
                }
                return _BiddingCode;
            }
            set
            {
                _BiddingCode = value;
                this.ViewState["_BiddingCode"] = value;
            }
        }
        /// <summary>
        /// ����ҵ�����
        /// </summary>
        private string _BiddingEmitCode = "";

        /// <summary>
        /// ����ҵ�����
        /// </summary>
        public string BiddingEmitCode
        {
            get
            {
                if (_BiddingEmitCode == "")
                {
                    if (this.ViewState["_BiddingEmitCode"] != null)
                        return this.ViewState["_BiddingEmitCode"].ToString();
                    return "";
                }
                return _BiddingEmitCode;
            }
            set
            {
                _BiddingEmitCode = value;
                this.ViewState["_BiddingEmitCode"] = value;
            }
        }
        /// <summary>
        /// ����,��ʾ����,�ر�
        /// </summary>
        public bool IsReturnView
        {
            get
            {
                return _IsReturnView;
            }
            set
            {
                _IsReturnView = value;
            }
        }
        public string NowState
        {
            get
            {
                if (Request.QueryString["NowState"] + "" != "")
                {
                    return Request.QueryString["NowState"].ToString();
                }
                else
                {
                    return "3";
                }
            }
        }

        /// ****************************************************************************
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // �ڴ˴������û������Գ�ʼ��ҳ��			
        }

        /// ****************************************************************************
        /// <summary>
        /// �����ʼ��
        /// </summary>
        /// ****************************************************************************
        public void InitControl()
        {
            if (this.State == ModuleState.Sightless)//���ɼ���
            {
                this.Visible = false;
            }
            else if (this.State == ModuleState.Operable)//�ɲ�����
            {
                LoadData(true);
                EyeableDiv.Visible = false;
                OperableDiv.Visible = true;
            }
            else if (this.State == ModuleState.Eyeable)//�ɼ���
            {
                LoadData(false);
                OperableDiv.Visible = false;
                EyeableDiv.Visible = true;
            }
            else if (this.State == ModuleState.Begin)//�ɼ���
            {
                LoadData(false);
                OperableDiv.Visible = false;
                EyeableDiv.Visible = true;
            }
            else if (this.State == ModuleState.End)//�ɼ���
            {
                LoadData(false);
                OperableDiv.Visible = false;
                EyeableDiv.Visible = true;
            }
            else if (this.State == ModuleState.Other)// ��ѯ��������ϸ
            {
                View_LoadData();
                OperableDiv.Visible = false;
                EyeableDiv.Visible = true;
            }
            else
            {
                this.Visible = false;
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// ���ݼ���
        /// </summary>
        /// ****************************************************************************		
        private void LoadData(bool Flag)
        {
            this.CheckBox2.Visible = this.IsWSZTB;
            this.TbWSZTB.Visible = this.IsWSZTB;
            this.GridView1.Visible = this.IsWSZTB;
            if (this.BiddingEmitCode != "")
            {

                BLL.BiddingReturn cBiddingReturn = new BLL.BiddingReturn();
                cBiddingReturn.BiddingEmitCode = this.BiddingEmitCode;
                BLL.BiddingEmit biddingemit = new BLL.BiddingEmit();
                biddingemit.BiddingEmitCode = this.BiddingEmitCode;
                this.BiddingCode = biddingemit.BiddingCode;

                DataTable dt = cBiddingReturn.GetBiddingReturns();
                if (Flag)
                {
                    ////////////////////////////////////////////////////////////
                    BLL.BiddingDtl cBiddingDtl = new BLL.BiddingDtl();
                    cBiddingDtl.BiddingCode = this.ApplicationCode;
                    DataTable dtl;
                    dtl = cBiddingDtl.GetBiddingDtls();
                    BLL.BiddingReturn bed = new BLL.BiddingReturn();
                    bed.BiddingEmitCode = this.BiddingEmitCode;
                    DataTable det = bed.GetBiddingReturns();
                    for (int i = 0; i < dtl.Rows.Count; i++)
                    {
                        if ((det.Select(" BiddingDtlCode=" + dtl.Rows[i]["BiddingDtlCode"].ToString()).Length) == 0)
                            dtl.Rows.Remove(dt.Rows[i]);

                    }
                    ////////////////////////////////////////////////////////////
                    Bind_dgListEdit(dt);
                    this.txtTotalRemark.Text = biddingemit.TotalRemark;
                }
                else
                {
                    Bind_dgListView(dt);
                }
                dt.Dispose();
                LoadBiddingOpener(false);
                dpAllowAfterClose.SelectedValue = biddingemit.AllowAfterClose.ToString();
            }
            else
            {
                if (this.ViewState["dt"] == null)
                {
                    BLL.BiddingReturn cBiddingReturn = new BLL.BiddingReturn();
                    cBiddingReturn.BiddingEmitCode = this.ApplicationCode;
                    DataTable dt = cBiddingReturn.GetBiddingReturns();

                    //��������
                    dt.Columns.Add("BiddingSupplierCode", System.Type.GetType("System.String"));
                    dt.Columns.Add("State2", System.Type.GetType("System.String"));

                    BLL.BiddingPrejudication cBiddingPrejudication = new BLL.BiddingPrejudication();
                    cBiddingPrejudication.BiddingCode = this.BiddingCode;
                    DataTable dtPrejudication = cBiddingPrejudication.GetBiddingPrejudications();
                    for (int i = 0; i < dtPrejudication.Rows.Count; i++)
                    {
                        BLL.BiddingDtl bdl = new BLL.BiddingDtl();
                        bdl.BiddingCode = this.BiddingCode;
                        bdl.flag = "1";
                        DataTable dtbdl = bdl.GetBiddingDtls();
                        for (int x = 0; x < dtbdl.Rows.Count; x++)
                        {
                            BLL.BiddingSupplier cBiddingSupplier = new BLL.BiddingSupplier();
                            cBiddingSupplier.BiddingPrejudicationCode = dtPrejudication.Rows[i][0].ToString();
                            cBiddingSupplier.Flag = "1";
                            DataTable dtSupplier = cBiddingSupplier.GetBiddingSuppliers();
                            for (int j = 0; j < dtSupplier.Rows.Count; j++)
                            {
                                DataRow dr = dt.NewRow();
                                dr["BiddingReturnCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BiddingReturn");
                                dr["SupplierCode"] = dtSupplier.Rows[j]["SupplierCode"].ToString();
                                dr["BiddingDtlCode"] = dtbdl.Rows[x]["BiddingDtlCode"].ToString();
                                dr["OrderCode"] = dtSupplier.Rows[j]["OrderCode"].ToString();
                                dr["BiddingSupplierCode"] = dtSupplier.Rows[j]["BiddingSupplierCode"].ToString();
                                dr["State2"] = dtSupplier.Rows[j]["State"].ToString();
                                dt.Rows.Add(dr);
                            }
                            dtSupplier.Dispose();
                        }
                    }
                    dtPrejudication.Dispose();
                    this.ViewState["dt"] = dt;
                    dt.Dispose();
                }
                Bind_dgListView((DataTable)this.ViewState["dt"]);
                LoadBiddingOpener(true);
            }

        }
        /// <summary>
        /// ��ʾ����,���ɱ�༭
        /// </summary>
        private void View_LoadData()
        {
            //������Ͷ����Ϣ
            EntityData entity = new EntityData("BiddingEmitTo");
            string sql = "select * from BiddingEmitTo where BiddingEmitCode='" + BiddingEmitCode + "' ";
            QueryAgent QA = new QueryAgent();
            entity = QA.FillEntityData("BiddingEmitTo", sql);
            GridView1.DataSource = entity;
            GridView1.DataBind();


            if (entity.HasRecord())
                this.CheckBox2.Checked = true;
            this.CheckBox2.Enabled = false;
            this.CheckBox2.Visible = this.IsWSZTB;


            //��ʾ�ر굥λ��Ϣ
            BLL.BiddingReturn cBiddingReturn = new BLL.BiddingReturn();
            cBiddingReturn.BiddingEmitCode = this.BiddingEmitCode;
            DataTable dt = cBiddingReturn.GetBiddingReturns();

            BindDataGrid1(dt);
            //��ʾ��ע��Ϣ
            BLL.BiddingEmit biddingemit = new BLL.BiddingEmit();
            biddingemit.BiddingEmitCode = this.BiddingEmitCode;
            this.BiddingCode = biddingemit.BiddingCode;
            if (IsReturnView)
            {
                txtTotalRemark1.Text = biddingemit.TotalRemark;
                AttachMentList1.AttachMentType = "BiddingReturnModify1";
            }
            else
            {
                txtTotalRemark1.Text = biddingemit.TotalRemark2;
                AttachMentList1.AttachMentType = "BiddingReturnModify2";
            }
            AttachMentList1.MasterCode = biddingemit.BiddingEmitCode;
            this.AttachMentAdd2.Visible = false;
            txtTotalRemark1.Enabled = false;
            Datagrid1.Visible = true;
            dpAllowAfterClose.SelectedValue = biddingemit.AllowAfterClose.ToString();
            LoadBiddingOpener(false);
            
        }
        /// <summary>
        /// ��ʾ���굥λ�б�
        /// </summary>
        /// <param name="dt"></param>
        private void BindDataGrid1(DataTable dt)
        {
            dgListView.Visible = false;
            bool useForeignMoney = RmsPM.BLL.BiddingSystem.IsUseForeignMoney(this.BiddingEmitCode);
            if (!useForeignMoney)
            {
                Datagrid1.Columns[6].Visible = false;
            }
            Datagrid1.DataSource = dt;
            Datagrid1.DataBind();
        }
        /// <summary>
        /// ��dgListEdit �ڿɸ���״̬
        /// </summary>
        /// <param name="dt"></param>
        private void Bind_dgListEdit(DataTable dt)
        {
            this.dgListEdit.DataSource = dt;
            this.dgListEdit.DataBind();
            //this.gpControlEdit.RowsCount = dt.Rows.Count.ToString();
            //BinddingMoneyControl();
        }
        /// <summary>
        /// ��dgListView �ڲ鿴״̬
        /// </summary>
        /// <param name="dt"></param>
        private void Bind_dgListView(DataTable dt)
        {
            this.dgListView.DataSource = dt;
            this.dgListView.DataBind();
            //this.gpControlView.RowsCount = dt.Rows.Count.ToString();
        }
        //private void Get_BinddingEmit()
        //{
        //    BLL.BiddingEmit biEmit = new RmsPM.BLL.BiddingEmit();
        //    biEmit.BiddingEmitCode = this.BiddingEmitCode;
        //}
        #region ��ʾ�ϴ�����
        /// <summary>
        /// �ر���
        /// </summary>
        /// <param name="EmitCode"></param>
        public void Show_ttachMentAdd(string EmitCode)
        {
            AttachMentAdd1.AttachMentType = "BiddingReturnModify1";
            AttachMentAdd1.MasterCode = EmitCode;
        }
        public void Show_ttachMentAdd2(string EmitCode)
        {
            AttachMentAdd2.AttachMentType = "BiddingReturnModify2";
            AttachMentAdd2.MasterCode = EmitCode;
        }
        #endregion
        /// ****************************************************************************
        /// <summary>
        /// �ύ����
        /// </summary>
        /// ****************************************************************************
        public void SubmitData()
        {
            if (this.ApplicationCode != "")
            {
                DAL.QueryStrategy.BiddingReturnStrategyBuilder sb = new RmsPM.DAL.QueryStrategy.BiddingReturnStrategyBuilder();
                sb.AddStrategy(new Strategy(DAL.QueryStrategy.BiddingReturnStrategyName.BiddingCode, this.BiddingCode));

                string sql = sb.BuildMainQueryString();

                EntityData entity = new EntityData("BiddingReturn");
                dao.FillEntity(sql, entity);
                //ɾ��ԭ������
                BLL.BiddingSystem.DelHistoryPlace(this.BiddingCode);
                //����һ���±�
                DataTable dt = new DataTable();
                dt.Columns.Add("BiddingReturnCode", System.Type.GetType("System.String"));
                dt.Columns.Add("Money", System.Type.GetType("System.Decimal"));
                dt.Columns.Add("Remark", System.Type.GetType("System.String"));
                dt.Columns.Add("ReturnDate", System.Type.GetType("System.String"));
                dt.Columns.Add("State", System.Type.GetType("System.String"));
                dt.Columns.Add("BiddingDtlCode", System.Type.GetType("System.String"));
                DataRow dr;
                //��ȡ������Ϣ,����������
                for (int i = 0; i < this.dgListEdit.Items.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["BiddingReturnCode"] = this.dgListEdit.Items[i].Cells[0].Text.Trim();
                    dr["Money"] = this.GetControl(i).TotalMoney;
                    dr["Remark"] = ((HtmlTextArea)this.dgListEdit.Items[i].FindControl("txtRemark")).Value.Trim();
                    dr["ReturnDate"] = ((AspWebControl.Calendar)this.dgListEdit.Items[i].FindControl("txtReturnDate")).Value;
                    dr["BiddingDtlCode"] = this.dgListEdit.Items[i].Cells[8].Text.Trim();
                    //dr["State"]=i+1;
                    dt.Rows.Add(dr);
                }
                BLL.Bidding bd = new BLL.Bidding();
                bd.BiddingCode = this.BiddingCode;
                BLL.BiddingReturn br = new BLL.BiddingReturn();
                br.BiddingEmitCode = bd.BiddingLastEmit;
                DataTable dtreturn = br.GetBiddingReturns();
                foreach (DataRow drr in dtreturn.Rows)
                {
                    if (dt.Select("BiddingReturnCode ='" + drr["BiddingReturnCode"].ToString() + "'").Length == 0)
                    {
                        dr = dt.NewRow();
                        dr["BiddingReturnCode"] = drr["BiddingReturnCode"];
                        dr["BiddingDtlCode"] = drr["BiddingDtlCode"];
                        dr["Money"] = drr["Money"];
                        dr["Remark"] = drr["Remark"];
                        dr["ReturnDate"] = drr["ReturnDate"];
                        //dr.ItemArray.CopyTo(drr.ItemArray, 0);
                        dt.Rows.Add(dr);
                    }
                }


                DataView dv = new DataView(dt);

                dv.Sort = "BiddingDtlCode,Money";
                //for(int i=0;i<dv.Rows.Count;i++)
                int j = 0;
                string tempBDtlCode = "";
                decimal tempMoney = 0;

                foreach (DataRowView dr2 in dv)
                {
                    if (tempBDtlCode != dr2["BiddingDtlCode"].ToString())
                    {
                        tempBDtlCode = dr2["BiddingDtlCode"].ToString();
                        j = 0;
                        tempMoney = 0;
                    }


                    DataRow[] dr1 = entity.CurrentTable.Select("BiddingReturnCode='" + dr2["BiddingReturnCode"].ToString() + "' and " + "BiddingDtlCode='" + dr2["BiddingDtlCode"].ToString() + "'");
                    
                    if (dr1.Length > 0)
                    {
                        dr1[0]["Money"] = dr2["Money"];
                        dr1[0]["Remark"] = dr2["Remark"];
                        dr1[0]["ReturnDate"] = dr2["ReturnDate"];
                        //State�ֶ��д�����������������Ϊ������ر���Ϊ0����Ϊ��ʱ��������������������һ���ĵ�λ������һ����
                        if (!dr2["Money"].ToString().Equals("0") && !dr2["Money"].ToString().Equals(""))
                        {
                            if (tempMoney != System.Convert.ToDecimal(dr2["Money"]))
                                j++;
                            dr1[0]["State"] = j;
                        }
                        else
                        {
                            dr1[0]["State"] ="" ;
                        }
                        tempMoney = System.Convert.ToDecimal(dr2["Money"]);
                        
                    }
                }

                dao.SubmitEntity(entity);
                BLL.Bidding bidding = new BLL.Bidding();
                bidding.BiddingCode = this.BiddingCode;
                bidding.State = NowState;
                bidding.dao = dao;
                bidding.BiddingSubmit();
                //���±�ע	
                BLL.BiddingEmit addRemark = new RmsPM.BLL.BiddingEmit();
                addRemark.BiddingEmitCode = this.BiddingEmitCode;
                addRemark.TotalRemark = this.txtTotalRemark.Text;
                addRemark.dao = dao;
                addRemark.BiddingEmitUpdate();
            }
            else
            {
                //ɾ����ʷ�����¼
                BLL.BiddingSystem.DelHistoryPlace(this.BiddingCode);
                DataTable dt = (DataTable)ViewState["dt"];
                BLL.BiddingSupplier BSup = new RmsPM.BLL.BiddingSupplier();

                DAL.QueryStrategy.BiddingReturnStrategyBuilder sb = new RmsPM.DAL.QueryStrategy.BiddingReturnStrategyBuilder();
                sb.AddStrategy(new Strategy(DAL.QueryStrategy.BiddingReturnStrategyName.BiddingEmitCode, this.BiddingEmitCode));

                string sql = sb.BuildMainQueryString();

                EntityData entity = new EntityData("BiddingReturn");
                dao.FillEntity(sql, entity);

                if (dt.Rows.Count > 0)
                {
                    dao.DeleteAllRow(entity);
                    dao.SubmitEntity(entity);
                    string stat = "0";
                    string tempsuppliercodestr = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (((CheckBox)dgListView.Items[i].FindControl("CheckBox1")).Checked == true)
                        {
                            DataRow dr = entity.GetNewRecord();
                            dr["BiddingReturnCode"] = dt.Rows[i]["BiddingReturnCode"].ToString();
                            dr["BiddingDtlCode"] = dt.Rows[i]["BiddingDtlCode"].ToString();
                            dr["SupplierCode"] = dt.Rows[i]["SupplierCode"].ToString();
                            dr["OrderCode"] = dt.Rows[i]["OrderCode"].ToString();
                            dr["BiddingEmitCode"] = this.BiddingEmitCode;
                            dr["Abnegate"] = 0;
                            entity.AddNewRecord(dr);
                            //���¹�Ӧ�̱�							
                            stat = "1";


                            //���������Ϣ
                            RmsPM.BLL.BiddingGradeMessage cbiddingGradeMessage = new RmsPM.BLL.BiddingGradeMessage();

                            cbiddingGradeMessage.ApplicationCode = dt.Rows[i]["BiddingReturnCode"].ToString();
                            cbiddingGradeMessage.BiddingGradeTypeCode = "100002";
                            if (cbiddingGradeMessage.GetBiddings().Rows.Count == 0)
                            {
                                cbiddingGradeMessage.BiddingGradeMessageCode = "";
                                cbiddingGradeMessage.ProjectManage ="";
                                cbiddingGradeMessage.State = "1";
                                cbiddingGradeMessage.dao = dao;
                                cbiddingGradeMessage.BiddingGradeMessageAdd();
                            }

                            ////////////////������Ͷ�����ݲ���////////////////////
                            if (tempsuppliercodestr.IndexOf(dt.Rows[i]["SupplierCode"].ToString()) == -1&&this.CheckBox2.Checked)
                            {
                                tempsuppliercodestr += "," + dt.Rows[i]["SupplierCode"].ToString();
                                EmitToInsert(dt.Rows[i]["SupplierCode"].ToString(), this.BiddingEmitCode);
                                
                            }
                            ///////////////////////////////////////////////////////

                        }
                        //���¹�Ӧ���б�

                        BSup.BiddingSupplierCode = this.dgListView.DataKeys[i].ToString();
                        BSup.State = stat;
                        BSup.dao = dao;
                        BSup.BiddingSupplierUpdate();

                      
            
                        //BSup.BiddingSupplierUpdate();
                    }
                    dao.SubmitEntity(entity);
                    entity.Dispose();
                }

                //���濪����
                if (this.CheckBox2.Checked)
                {
                    BiddingBFL.Emit_SendMail(this.BiddingEmitCode, Server.MapPath(ConfigurationManager.AppSettings["VirtualDirectory"].ToString()) + @"\EmailTemplate.xml");
                    foreach (ListItem opener in chkOpener.Items)
                    {
                        if (opener.Selected)
                        {
                            BiddingBFL.InsertBiddingOpener(this.BiddingEmitCode, opener.Value);
                        }
                    }
                }
            }

        }
        /// <summary>
        /// ������Ͷ������д��
        /// </summary>
        /// <param name="SupplierCode"></param>
        /// <param name="EmitCode"></param>
        private void EmitToInsert(string SupplierCode,string EmitCode)
        {
            string pwd = BiddingBFL.NewPassword();
            string gid = BiddingBFL.NewBiddingSN();
            EntityData entity = new EntityData("BiddingEmit");
            string sql = "select * from BiddingEmit insert into BiddingEmitTo(BiddingEmitCode,SupplierCode,IsOnline,SerialNum,SuppPwd) values ('" + EmitCode + "','" + SupplierCode + "','0','" + gid + "','" + pwd + "') ";
            if (dao == null)
            {
                QueryAgent QA = new QueryAgent();
                entity = QA.FillEntityData("BiddingEmit", sql);
            }
            else
            {
                dao.EntityName = "BiddingEmit";
                dao.FillEntity(sql, entity);
            }
        }

        /// <summary>
        /// ���±�ע
        /// </summary>
        public void UpdateRemark()
        {
            //string lawOfPrice="";//ѹ��
            BLL.BiddingEmit addRemark = new RmsPM.BLL.BiddingEmit();
            addRemark.BiddingEmitCode = this.BiddingEmitCode;
            addRemark.TotalRemark2 = this.txtTotalRemark1.Text;

            BLL.Bidding bidd = new RmsPM.BLL.Bidding();
            bidd.BiddingCode = Request.QueryString["BiddingCode"];//this.BiddingCode;
            string thisState = bidd.State;
            //Response.Write(Rms.Web.JavaScript.Alert(true,thisState));
            if (thisState == "5" || thisState == "6")
            {
                addRemark.CreatUser = "1";
                //bidd.State = "6";
                //bidd.BiddingSubmit();
            }
            //addRemark.dao = dao;
            addRemark.BiddingEmitUpdate();
            //if()
            //BLL.BiddingEmit.UpdateRemarkByBiddingEmitCode(txtTotalRemark1.Text,this.BiddingEmitCode);

            //���濪���Ͷ�굥λ�鿴Ȩ��/shifou wangshang
            BiddingBFL.UpdateEmit_AllowAfterClose(this.BiddingEmitCode, Convert.ToInt32(dpAllowAfterClose.SelectedValue));
            int tmpWsZTB = 0;
            if (CheckBox2.Checked)
                tmpWsZTB = 1;
            else
                tmpWsZTB = 0;
            BiddingBFL.UpdateEmit_IsWsZTB(this.BiddingEmitCode, tmpWsZTB);

        }
        /// <summary>
        /// ����Ҫѡ�񷢱���̼�
        /// </summary>
        public void UpdateBiddingEmitSup()
        {
            for (int i = 0; i < dgListView.Items.Count; i++)
            {
                BLL.BiddingSupplier BSup = new RmsPM.BLL.BiddingSupplier();
                BSup.BiddingSupplierCode = this.dgListView.DataKeys[i].ToString();
                string stat = "0";
                if (((CheckBox)dgListView.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    stat = "1";
                }
                BSup.State = stat;
                BSup.BiddingSupplierUpdate();
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
            LoadEvent();

        }
        private void LoadEvent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
            this.dgListEdit.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgListEdit_ItemDataBound);
            this.Datagrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.Datagrid1_ItemDataBound);
            //this.dgListView.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgListView_ItemDataBound);
        }
        #endregion

        private void gpControlEdit_PageIndexChange(object sender, System.EventArgs e)
        {
            this.InitControl();
        }

        private void gpControlView_PageIndexChange(object sender, System.EventArgs e)
        {
            this.InitControl();
        }
        /// <summary>
        /// ɾ������
        /// </summary>
        public void Delete()
        {
            BLL.BiddingReturn cBiddingReturn = new BLL.BiddingReturn();
            cBiddingReturn.BiddingEmitCode = this.BiddingEmitCode;
            cBiddingReturn.dao = this.dao;
            cBiddingReturn.BiddingEmitReturnDelete();
        }
        public bool BiddingReturnCheck()
        {

            bool CheckValue = false;
            for (int i = 0; i < this.dgListEdit.Items.Count; i++)
            {
                string _BiddingReturnCode = this.dgListEdit.Items[i].Cells[0].Text.Trim();
                string _Money = this.GetControl(i).TotalMoney;
                string _Remark = ((HtmlTextArea)this.dgListEdit.Items[i].FindControl("txtRemark")).Value.Trim();
                string _ReturnDate = ((AspWebControl.Calendar)this.dgListEdit.Items[i].FindControl("txtReturnDate")).Value;

                if (!((_Money != "" && _ReturnDate != "") || (_Money != "" && _ReturnDate == "")))
                {
                    CheckValue = true;
                }
            }
            return CheckValue;
        }
        /// <summary>
        /// �ҵ� Control
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>

        public UserControls.ManyCurrencyCost GetControl(int i)
        {

            UserControls.ManyCurrencyCost er = (UserControls.ManyCurrencyCost)this.dgListEdit.Items[i].FindControl("ManyCurrencyCost1");
            return er;
        }
        /// <summary>
        /// �ؼ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgListEdit_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) ||
                (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataRowView myrow = ((DataRowView)e.Item.DataItem);
                UserControls.ManyCurrencyCost manycs = (UserControls.ManyCurrencyCost)e.Item.FindControl("Manycurrencycost1");
                if (manycs != null)
                {
                    manycs.IsEditMode = true;
                    manycs.CashMessageCode = myrow["BiddingReturnCode"].ToString();
                    manycs.LoadData();
                }
            }
        }
        /// <summary>
        /// �ؼ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Datagrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) ||
                (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataRowView myrow = ((DataRowView)e.Item.DataItem);
                UserControls_manycurrencycostInfo manycsinfo = (UserControls_manycurrencycostInfo)e.Item.FindControl("manycurrencycostInfo1");
                if (manycsinfo != null)
                {
                    manycsinfo.CashMessageTypeCode = myrow["BiddingReturnCode"].ToString();
                    manycsinfo.LoadData();
                }
            }
        }
        public void dgListEdit_ItemDataBound1(object sender, DataGridItemEventArgs e)
        {

        }

        /// <summary>
        /// ��ʾ�������б�
        /// 
        /// </summary>
        /// <param name="IsNew"></param>
        private void LoadBiddingOpener(bool IsNew)
        {
            List<string> listOpener = BiddingBFL.ListAvaiableBiddingOpener();
            List<string[]> listSelectedOpener = BiddingBFL.ListBiddingOpener(this.BiddingEmitCode);
            chkOpener.Items.Clear();
            foreach (string item in listOpener)
            {
                ListItem opener = new ListItem();
                opener.Text = SystemRule.GetUserName(item);
                opener.Value = item;
                if (IsNew)
                {
                    opener.Selected = true;
                }
                else
                {
                    foreach (string[] selectitem in listSelectedOpener)
                    {
                        if (selectitem[0] == item)
                        {
                            opener.Selected = true;
                            if (selectitem[1] != "")
                            {
                                opener.Text += "(" + selectitem[1] + ")";
                            }
                            else
                                opener.Text += "( - - - )";
                        }
                    }
                }
                chkOpener.Items.Add(opener);
            }
            chkOpener.Enabled = false;
        }
    }
}

