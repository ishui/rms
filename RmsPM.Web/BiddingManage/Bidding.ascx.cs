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
    using System.Configuration;

    /// <summary>
    ///		Bidding ��ժҪ˵����
    /// </summary>
    public partial class Bidding : BiddingControlBase
    {
        private ModuleState _PriceState;
        private ModuleState _BiddingFileState;

        /// <summary>
        /// �б��ļ�
        /// </summary>
        public ModuleState BiddingFileState
        {
            get
            {
                if (_BiddingFileState == ModuleState.Unbeknown)
                {
                    if (this.ViewState["_BiddingFileState"] != null)
                        return (ModuleState)this.ViewState["_BiddingFileState"];
                    return ModuleState.Unbeknown;
                }
                return _BiddingFileState;
            }
            set
            {
                _BiddingFileState = value;
                this.ViewState["_BiddingFileState"] = value;
            }
        }

        /// <summary>
        /// ���״̬��д
        /// </summary>
        public ModuleState PriceState
        {
            get
            {
                if (_PriceState == ModuleState.Unbeknown)
                {
                    if (this.ViewState["_PriceState"] != null)
                        return (ModuleState)this.ViewState["_PriceState"];
                    return ModuleState.Unbeknown;
                }
                return _PriceState;
            }
            set
            {
                _PriceState = value;
                this.ViewState["_PriceState"] = value;
            }
        }

        /// <summary>
        /// ��Ͷ��ƻ�״̬
        /// </summary>
        public string BiddingState
        {
            get
            {
                if (this.ViewState["BiddingState"] != null)
                    return this.ViewState["BiddingState"].ToString();
                return "";
            }
            set
            {
                this.ViewState["BiddingState"] = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string mostly
        {
            get
            {
                return this.ViewState["mostly"].ToString();
            }
            set
            {
                this.ViewState["mostly"] = value;
            }
        }
        public string TeamMoney
        {
            get
            {
                if (this.ViewState["_TeamMoney"] == null)
                    return "";
                return this.ViewState["_TeamMoney"].ToString();
            }
            set
            {
                this.ViewState["_TeamMoney"] = value;
            }
        }
        /// <summary>
        /// Ͷ�굥λԤ�������
        /// </summary>
        public int PrejudicationsCount
        {
            get
            {
                BLL.BiddingManage bm = new BLL.BiddingManage();
                bm.BiddingCode = this.ApplicationCode;
                return bm.GetBiddingPrejudications().Rows.Count;
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string SystemGroup
        {
            get
            {
                return this.inputSystemGroup.Value.ToString();
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
        }

        /// ****************************************************************************
        /// <summary>
        /// �����ʼ��
        /// </summary>
        /// ****************************************************************************
        public void InitControl()
        {
            if (System.Configuration.ConfigurationSettings.AppSettings["PMName"] + "" == "YinRunPM")
            {
                this.ArrangedDateTr.Visible = false;
            }


            if (this.State == ModuleState.Sightless)//���ɼ���
            {
                this.Visible = false;
            }
            else if (this.State == ModuleState.Operable)//�ɲ�����
            {
                LoadData(true);
                View_Edit_state();
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
            else
            {
                this.Visible = false;
            }


            //����Ϊ�Խ�������Ȩ��
            if (this.PriceState == ModuleState.Operable)//�ɲ�����
            {

            }
            else if (this.PriceState == ModuleState.Eyeable)//�ɼ���
            {

            }
            else//��Ȩ��
            {
                if (OperableDiv.Visible)
                {
                    this.txtMoney.Text = "*****";

                }
                if (EyeableDiv.Visible)
                {
                    this.tdMoney.Text = "*****";
                }
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// ���ݼ���
        /// </summary>
        /// ****************************************************************************
        private void LoadData(bool Flag)
        {

            if ((Session["User"] == null) && (Request["DebugUser"]+"" != ""))
            {
                Session["User"] = new User(Request["DebugUser"] + "");
            }

			if ((Session["User"] == null) && (ConfigurationSettings.AppSettings["IsDebug"] == "1") && (ConfigurationSettings.AppSettings["DebugUser"] != ""))
			{
				Session["User"] = new User(ConfigurationSettings.AppSettings["DebugUser"]);
			}



            if (Session["User"] != null)
            {
               User user = (User)Session["User"];
               this.btnLastUpdate.Visible = user.HasRight("2111");
               this.btnlastUpdate2.Visible = user.HasRight("2111");

            }
            this.inputSystemGroup.ClassCode = "0501";
          //tangchenpm �����б����ͼ�Ԥ����
            string company = System.Configuration.ConfigurationManager.AppSettings["PMName"].ToLower();
           switch (company)
           {
               case "tangchenpm":
                   this.TrBiddingTypeAndMoneyEdit.Visible = false;
                   this.TrBiddingTypeAndMoneyView.Visible = false;
                   break;
           }
            

            if (this.ApplicationCode != "")
            {
                BLL.BiddingManage bm = new BLL.BiddingManage();
                bm.BiddingCode = this.ApplicationCode;
                this.BiddingState = bm.State;
                this.ViewState["mostly"] = bm.Accessory;
                DataTable dt = bm.GetBiddingCosts();


                this.btnLastUpdate.Attributes["onclick"] = "javascript:BiddingLog();";
                this.btnlastUpdate2.Attributes["onclick"] = "javascript:BiddingLog();";
                if (Flag)
                {
                    this.txtTitle.Value = bm.Title;
                    this.txtContent.Value = bm.Content;
                    this.TxtAddress.Value = bm.BiddingAddress;
                   
                    this.txtMoney.Text = BLL.MathRule.GetDecimalShowString(bm.Money);
                    this.txtArrangedDate.Value = bm.ArrangedDate;
                    this.txtStandardDate.Value = bm.StandardDate;
                    this.txtPrejudicationDate.Value = bm.PrejudicationDate;
                    this.txtEmitDate.Value = bm.EmitDate;
                    this.txtReturnDate.Value = bm.ReturnDate;
                    this.txtConfirmDate.Value = bm.ConfirmDate;
                    this.txtRemark.Value = bm.Remark;
                    this.txtProject.InnerHtml = BLL.ProjectRule.GetProjectName(bm.ProjectCode);
                    this.ProjectCode = bm.ProjectCode;
                    this.inputSystemGroup.Value = bm.Type;
                    this.chkaccssory.Checked = (bm.Accessory == "1");
                    Inputunit1.Value = bm.BiddingRemark1;

                    RmsPM.BLL.BiddingSystem.Set_BiddingTypeDictionary(this.selBiddingType);
                    string BiddingType = bm.BiddingType;
                    if (BiddingType == null || BiddingType == "")
                    {
                        this.selBiddingType.SelectedIndex = 0;
                    }
                    else
                    {
                        this.selBiddingType.Value = BiddingType;
                    }

                    this.BiddingFileInfo1.State = BiddingFileState;
                    this.BiddingFileInfo1.BiddingCode = bm.BiddingCode;
                    this.BiddingFileInfo1.ProjectCode = this.ProjectCode;
                    this.BiddingFileInfo1.InitControl();
                }
                else
                {
                    this.tdType.InnerHtml = BLL.ContractRule.GetContractTypeName(bm.Type);
                    this.tdTitle.InnerHtml = bm.Title + "&nbsp;&nbsp;(" + BLL.BiddingSystem.GetStateMessage(bm.State) + ")";
                    this.tdBiddingType.Text = BLL.BiddingSystem.GetBiddingTypeName(bm.BiddingType.ToString());
                    this.tdMoney.Text = BLL.MathRule.GetDecimalShowString(bm.Money);
                    this.tdContent.InnerHtml = bm.Content.Replace("\n", "<br>");
                    this.tdArrangedDate.InnerHtml = bm.ArrangedDate;
                    this.tdStandardDate.InnerHtml = bm.StandardDate;
                    this.tdPrejudicationDate.InnerHtml = bm.PrejudicationDate;
                    this.tdEmitDate.InnerHtml = bm.EmitDate;
                    this.tdReturnDate.InnerHtml = bm.ReturnDate;
                    this.tdConfirmDate.InnerHtml = bm.ConfirmDate;
                    this.tdRemark.InnerHtml = bm.Remark.Replace("\n", "<br>");
                    this.tdProjectName.InnerHtml = BLL.ProjectRule.GetProjectName(bm.ProjectCode);
                    this.lblAddress.Text = bm.BiddingAddress;

                    this.lblUnit.Text = RmsPM.BLL.SystemRule.GetUnitFullName(bm.BiddingRemark1);

                    EntityData entity = DAL.EntityDAO.CostBudgetDAO.GetV_CostBudgetSetByCode(bm.CostBudgetSetCode);
                    if (entity.HasRecord())
                    {
                        if (bm.Accessory == "1")
                            this.tdCostBudget.InnerHtml = "��";
                        else
                            this.tdCostBudget.InnerHtml = "��";
                        //this.tdPBSType.InnerHtml = bm.PBSType;
                        //this.tdPBSCode.InnerHtml = entity.GetString("PBSName");
                    }

                    this.EyeaBiddingFileInfo.State = BiddingFileState;
                    this.EyeaBiddingFileInfo.BiddingCode = bm.BiddingCode;
                    this.EyeaBiddingFileInfo.ProjectCode = this.ProjectCode;
                    this.EyeaBiddingFileInfo.InitControl();

                }
            }
            else
            {
                this.ViewState["mostly"] = "";
                this.btnLastUpdate.Visible = false;
                this.btnlastUpdate2.Visible = false;
                if (Flag)
                {

                    if (this.Request["ProjectCode"] != null)
                    {
                        ProjectCode = Request["ProjectCode"];

                    }
                    else if (this.Session["ProjectCode"] != null)
                    {
                        ProjectCode = Session["ProjectCode"].ToString();
                    }
                    this.ApplicationCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("Bidding");
                    this.txtProject.InnerHtml = BLL.ProjectRule.GetProjectName(this.ProjectCode);
                    this.txtArrangedDate.Value = DateTime.Now.ToString();

                    this.txtStandardDate.Value = DateTime.Now.ToString();
                    this.txtPrejudicationDate.Value = DateTime.Now.ToString();
                    this.txtEmitDate.Value = DateTime.Now.ToString();
                    this.txtReturnDate.Value = DateTime.Now.ToString();
                    this.txtConfirmDate.Value = DateTime.Now.ToString();
                    

                    //ȡ����ǰ�û���������
                    this.Inputunit1.Value = SetNewAddedUnit(ProjectCode);
                    RmsPM.BLL.BiddingSystem.Set_BiddingTypeDictionary(this.selBiddingType);

                }


                this.BiddingFileInfo1.State = ModuleState.Unbeknown;

                this.BiddingFileInfo1.ProjectCode = this.ProjectCode;
                this.BiddingFileInfo1.InitControl();

                this.EyeaBiddingFileInfo.State = ModuleState.Unbeknown;
                this.EyeaBiddingFileInfo.ProjectCode = this.ProjectCode;
                this.EyeaBiddingFileInfo.InitControl();
            }

            //string company = System.Configuration.ConfigurationManager.AppSettings["PMName"].ToLower();
            //switch (company)
            //{
            //    case "shimaopm":
            //        this.selBiddingType.Visible = false;
            //        break;
            //}


        }
        private string Get_BiddingState()
        {
            BLL.BiddingManage bm = new BLL.BiddingManage();
            bm.BiddingCode = this.ApplicationCode;
            string state;
            try
            {
                state = bm.State;
                return state;
            }
            catch
            {
                state = "0";
                return state;
            }
        }
        /// <summary>
        /// ֻ���޸��ݶ����״̬
        /// </summary>
        /// <returns></returns>
        private void View_Edit_state()
        {

            //�Ƿ��Ѵ���б�ƻ�,���ڵĻ�,����State����
            //С��0�����κβ���
            try
            {
                if (Convert.ToInt32(Get_BiddingState()) > 0)
                {
                    Prejudication_Change(false);
                    if (this.BiddingState == "2" || this.BiddingState == "3")
                    {
                        ObligateMoney_Change(false);
                    }
                    else if (this.BiddingState == "4")
                    {
                        Auditing_Change(false);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, ex.Message));
            }
        }
        /// <summary>
        /// ֻ���޸��ݶ����
        /// </summary>
        /// <param name="bl"></param>
        private void ObligateMoney_Change(bool bl)
        {
            this.txtTitle.Disabled = !bl;
            this.txtContent.Disabled = !bl;
            this.txtArrangedDate.ReadOnly = !bl;
            this.txtStandardDate.ReadOnly = !bl;
            this.txtPrejudicationDate.ReadOnly = !bl;
            this.txtConfirmDate.ReadOnly = !bl;
            this.TxtAddress.Disabled = !bl;
            this.txtRemark.Disabled = !bl;
        }

        /// <summary>
        /// ��Ԥ��֮����Ը��ĵ�
        /// </summary>
        /// <param name="bl"></param>
        private void Prejudication_Change(bool bl)
        {
            this.txtTitle.Disabled = !bl;
            this.txtContent.Disabled = !bl;
            this.txtArrangedDate.ReadOnly = !bl;
            this.txtStandardDate.ReadOnly = !bl;
            this.txtPrejudicationDate.ReadOnly = !bl;
        }
        /// <summary>
        /// ֻ���޸��ݶ����
        /// </summary>
        /// <param name="bl"></param>
        private void Auditing_Change(bool bl)
        {
            this.txtTitle.Disabled = !bl;
            this.txtContent.Disabled = !bl;
            this.txtArrangedDate.ReadOnly = !bl;
            this.txtStandardDate.ReadOnly = !bl;
            this.txtPrejudicationDate.ReadOnly = !bl;
            this.TxtAddress.Disabled = !bl;
            this.txtEmitDate.ReadOnly = !bl;
            this.txtReturnDate.ReadOnly = !bl;
            this.txtConfirmDate.ReadOnly = !bl;
            this.txtRemark.Disabled = !bl;
        }

        /// ****************************************************************************
        /// <summary>
        /// �ύ����
        /// </summary>
        /// ****************************************************************************
        public void SubmitData()
        {
            try
            {
                string state = Get_BiddingState();
                BLL.BiddingManage bm = new BLL.BiddingManage();
                bm.BiddingCode = this.ApplicationCode;
                //�Ƿ��Ѵ���б�ƻ�,���ڵĻ�,����State����		
                bm.Type = this.inputSystemGroup.Value;
                bm.Title = this.txtTitle.Value;
                bm.Content = this.txtContent.Value;
                bm.BiddingAddress = this.TxtAddress.Value;
                bm.Accessory = "";
                bm.ArrangedDate = this.txtArrangedDate.Value;

                if (this.txtStandardDate.Value.Trim() != "")
                    bm.StandardDate = this.txtStandardDate.Value;
                bm.PrejudicationDate = this.txtPrejudicationDate.Value;
                bm.EmitDate = this.txtEmitDate.Value;
                bm.ReturnDate = this.txtReturnDate.Value;
                bm.ConfirmDate = this.txtConfirmDate.Value;
                bm.State = state;
                bm.Remark = this.txtRemark.Value;
                bm.BiddingType = this.selBiddingType.Value;
                //bm.Money = this.Manycurrencycost1.TotalMoneyText;
                bm.BiddingRemark1 = Inputunit1.Value;
                if (this.chkaccssory.Checked)
                    bm.Accessory = "1";
                else
                    bm.Accessory = "0";
                bm.ProjectCode = this.ProjectCode;

                //bm.ObligateMoney = this.TeamMoney;

                bm.dao = this.dao;
                bm.BiddingSubmit();
                this.TeamMoney = bm.Money;

                if (this.ApplicationCode == "")
                    this.ApplicationCode = bm.BiddingCode;
                this.BiddingState = bm.State;

            }
            catch (Exception ex)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, ex.Message));
                throw ex;
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// ɾ������
        /// </summary>
        /// ****************************************************************************
        public void Delete()
        {
            BLL.BiddingManage bm = new BLL.BiddingManage();
            bm.BiddingCode = this.ApplicationCode;
            bm.BiddingDelete();
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

        protected void btnLastUpdate_ServerClick(object sender, EventArgs e)
        {
           
        }

        private string SetNewAddedUnit(string projectCode)
        {
            //�õ���Ŀ�µĲ���
            EntityData UnitinProject = BLL.SystemRule.GetUnitUnderProject(projectCode);
            int StationInUnitCount;
            string UnitStrInProject;
            int UnitRowCount = UnitinProject.CurrentTable.Rows.Count;
            string SelectedUnit = "";
            int RoleUnitCount = 0;
            int SelectUnitCount = 0;
            //ȡ����ǰ�û���������
            DataTable dtUnit = RmsPM.BLL.SystemRule.GetUnitListByUserCode(this.UserCode);
            if (dtUnit != null)
            {
                RoleUnitCount = dtUnit.Rows.Count;
            }
            //�õ���Ŀ�µ�ǰ�û���Ӧ�Ĳ���
            for (int i = 0; i < UnitRowCount; i++)
            {
                UnitinProject.SetCurrentRow(i);
                UnitStrInProject = UnitinProject.GetString("UnitCode");
                for (int k = 0; k < RoleUnitCount; k++)
                {
                    if (UnitStrInProject == dtUnit.Rows[k]["unitcode"].ToString())
                    {
                        SelectUnitCount++;
                        SelectedUnit = UnitStrInProject;
                    }
                }
            }
            UnitinProject.Dispose();
            //���������ڸ���Ŀ���ж�����ţ��Ͳ���
            if (SelectUnitCount == 1)
            {
                return SelectedUnit;
            }
            else
            {
                return "";
            }
        }

    }
}