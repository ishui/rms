namespace RmsPM.Web.BiddingManage
{
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

    using RmsPM.Web.WorkFlowControl;
    using Rms.ORMap;
    using Infragistics.WebUI.WebDataInput;
    using System.Web.UI.WebControls;


    public partial class BiddingManage_BiddingDtlModify : BiddingControlBase
    {

        private ModuleState _PriceState;
        private string _TeamMoney;
        /// <summary>
        /// 金额状态填写


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

        public string BiddingEmitCode
        {
            get
            {
                if (this.ViewState["BiddingEmitCode"] == null)
                    return "";
                return this.ViewState["BiddingEmitCode"].ToString();
            }
            set
            {
                this.ViewState["BiddingEmitCode"] = value;
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

        public int BiddingDtlCount
        {
            get {
                return this.dgListEdit.Items.Count;
            }
        }
        public bool CheckItemValue
        {
            get
            {
                bool flag = true;
                foreach (DataGridItem dgItem in dgListEdit.Items)
                {


                    if (((TextBox)dgItem.FindControl("txtTitle")).Text.Trim().Length == 0)
                    {
                        flag = false;
                    }
                   // if(((WebNumericEdit)dgItem.FindControl("TxtTemMoney")).Value.ToString() == "0")
                   //{
                   //    flag = false;
                   // }
                    RmsPM.Web.UserControls.InputCostBudgetDtl cb = ((RmsPM.Web.UserControls.InputCostBudgetDtl)dgItem.FindControl("Inputcostbudgetdtl1"));
                    if (cb.CostCode.Length == 0)
                    {
                        flag  = false;
                    }
                }
                return flag;

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                this.ViewState["DeleteItems"] = "'no'";
        }
        /// ****************************************************************************
        /// <summary>
        /// 组件初始化


        /// </summary>
        /// ****************************************************************************
        public void InitControl()
        {
            if (this.State == ModuleState.Sightless)//不可见的
            {
                this.Visible = false;
            }
            else if (this.State == ModuleState.Operable)//可操作的
            {
                LoadData(true);
                EyeableDiv.Visible = false;
                OperableDiv.Visible = true;
            }
            else if (this.State == ModuleState.Eyeable)//可见的


            {
                LoadData(false);
                OperableDiv.Visible = false;
                EyeableDiv.Visible = true;
            }
            else if (this.State == ModuleState.Begin)//可见的


            {
                LoadData(false);
                OperableDiv.Visible = false;
                EyeableDiv.Visible = true;
            }
            else if (this.State == ModuleState.End)//可见的


            {
                LoadData(false);
                OperableDiv.Visible = false;
                EyeableDiv.Visible = true;
            }
            else
            {
                this.Visible = false;
            }

            //增加特殊需求

            string companyName = System.Configuration.ConfigurationManager.AppSettings["PMName"].ToString().ToLower();
            switch (companyName)
            {
                case "gaokepm":
                    if (OperableDiv.Visible)
                    {

                        this.dgListEdit.Columns[5].Visible = true;

                    }
                    if (EyeableDiv.Visible)
                    {
                        this.dgListView.Columns[4].Visible = true;
                    }
                    break;
                default:
                    if (OperableDiv.Visible)
                    {

                        this.dgListEdit.Columns[5].Visible = false;

                    }
                    if (EyeableDiv.Visible)
                    {
                        this.dgListView.Columns[4].Visible = false;
                    }
                    break;
            }

            //以下为对金额操作的权限


            if (this.PriceState == ModuleState.Operable)//可操作的
            {
                if (OperableDiv.Visible)
                {
                    this.dgListEdit.Columns[3].Visible = true;
                    this.dgListEdit.Columns[4].Visible = false;
                }

            }
            else if (this.PriceState == ModuleState.Eyeable)//可见的

            {
                if (OperableDiv.Visible)
                {
                    this.dgListEdit.Columns[3].Visible = false;
                    this.dgListEdit.Columns[4].Visible = true;
                }
            }
            else//无权限

            {
                if (OperableDiv.Visible)
                {
                    this.dgListEdit.Columns[3].Visible = false;
                    this.dgListEdit.Columns[4].Visible = true;
                    foreach (DataGridItem dgItem in dgListEdit.Items)
                    {
                        Label lb = (Label)dgItem.FindControl("lbTemMoney");
                        lb.Text = "*****";
                    }
                    
                }
                if (EyeableDiv.Visible)
                {
                    foreach (DataGridItem dgItem in dgListView.Items)
                    {
                        System.Web.UI.WebControls.Label lb1 = (Label)dgItem.FindControl("lbTemMoney");
                        lb1.Text = "*****";
                    }
                }
            }
           



        }
        /// ****************************************************************************
        /// <summary>
        /// 数据加载
        /// </summary>
        /// ****************************************************************************
        private void LoadData(bool Flag)
        {
            if (this.ApplicationCode != "")
            {
                if (Flag)
                {
                    this.dgListEdit.DataSource = GetSourceTable();
                    this.dgListEdit.DataBind();
                }
                else
                {
                    BLL.BiddingDtl cBiddingDtl = new BLL.BiddingDtl();
                    cBiddingDtl.BiddingCode = this.ApplicationCode;
                    DataTable dt;
                    DataTable dtc;
                    if (this.BiddingEmitCode == "")
                    {
                        cBiddingDtl.flag = "1";
                        dt = cBiddingDtl.GetBiddingDtls();
                        dtc = dt;
                    }
                    else
                    {
                        dt = cBiddingDtl.GetBiddingDtls();
                        dtc = cBiddingDtl.GetBiddingDtls();
                        dtc.Clear();
                        BLL.BiddingReturn bed = new BLL.BiddingReturn();
                        bed.BiddingEmitCode = this.BiddingEmitCode;
                        DataTable det = bed.GetBiddingReturns();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (det.Select(" BiddingDtlCode=" + dt.Rows[i]["BiddingDtlCode"].ToString()).Length != 0)
                            {
                                DataRow dr = dtc.NewRow();
                                dr.ItemArray = dt.Rows[i].ItemArray;
                                dtc.Rows.Add(dr);
                            }
                        }
                    }
                    this.dgListView.DataSource = dtc;
                    this.dgListView.DataBind();
                }
            }
            else
            {
                if (Flag)
                {
                    AddNewRows();
                }
            }
        }
        private void AddNewRows(DataTable dt, int RowsCount)
        {
            for (int i = 0; i < RowsCount; i++)
            {
                DataRow dr = dt.NewRow();
                dr["BiddingDtlCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("BiddingDtl");
                dr["Money"] = 0;
                dr["OtherMoney"] = 0;
                dr["BiddingCode"] = this.ApplicationCode;
                dt.Rows.Add(dr);
            }
        }
        public void AddNewRows()
        {
            DataTable dt = GetSourceTable();
            AddNewRows(dt, 1);
            this.dgListEdit.DataSource = dt;
            this.dgListEdit.DataBind();
 
        }
        private DataTable GetSourceTable()
        {
            BLL.BiddingDtl cBiddingDtl = new BLL.BiddingDtl();
            cBiddingDtl.BiddingCode = this.ApplicationCode;
            cBiddingDtl.flag = "1";
            DataTable dt = cBiddingDtl.GetBiddingDtls();

            foreach (DataGridItem dgItem in dgListEdit.Items)
            {
                if (dt.Select("BiddingDtlCode=" + dgItem.Cells[0].Text).Length == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["BiddingDtlCode"] = dgItem.Cells[0].Text;
                    dr["Title"] = ((TextBox)dgItem.FindControl("txtTitle")).Text;
                    dr["Remark"] = ((TextBox)dgItem.FindControl("txtRemark")).Text;
                    dr["Money"] = ((WebNumericEdit)dgItem.FindControl("TxtTemMoney")).Value;
                    //增加特殊需求

                    string companyName = System.Configuration.ConfigurationManager.AppSettings["PMName"].ToString().ToLower();
                    switch (companyName)
                    {
                        case "gaokepm":
                            dr["OtherMoney"] = System.Convert.ToDecimal(((WebNumericEdit)dgItem.FindControl("TxtOtherMoney")).Value);

                            break;
                        default:
                            dr["OtherMoney"] = 0;
                            break;
                    }
                    RmsPM.Web.UserControls.InputCostBudgetDtl cb = ((RmsPM.Web.UserControls.InputCostBudgetDtl)dgItem.FindControl("Inputcostbudgetdtl1"));
                    dr["CostCode"] = cb.CostCode;
                    dr["CostBudgetSetCode"] = cb.CostBudgetSetCode;
                    dr["PBSCode"] = cb.PBSCode;
                    dr["PBSType"] = cb.PBSType;
                    dr["BiddingCode"] = this.ApplicationCode;
                    dt.Rows.Add(dr);
                }
            }
            foreach (DataRow dr in dt.Select())
            {
                if (this.CheckDtlCode(dr["BiddingDtlCode"].ToString(), this.ViewState["DeleteItems"].ToString()))
                {
                    dt.Rows.Remove(dr);
                }
            }
            return dt;

        }
        /// <summary>
        /// 检查将要删除的ID是否存在
        /// </summary>
        public bool CheckDtlCode(string DtlCode,string DeleteItems)
        {
            string[] DtlCodetemps = DeleteItems.Split(',');
            foreach (string dtlcodetemp in DtlCodetemps)
            {
                if (DtlCode == dtlcodetemp)
                {
                    return true;
                }
            }
            return false;
        }

        /// ****************************************************************************
        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="flag">是否修改（true为修改，false为新增）</param>
        /// ****************************************************************************
        private void _SubmitData(bool flag)
        {
            BLL.BiddingDtl bd = new BLL.BiddingDtl();
            bd.BiddingCode = this.ApplicationCode;
            bd.flag = "1";
            EntityData entity = bd.GetBiddingDtlEntity();
            foreach (DataRow dr in entity.CurrentTable.Rows)
            {
                dr["Flag"] = "0";
            }
            bd.SubmitDtlEntity(entity);

            decimal TeamMoneyTemp = 0;
            foreach (DataGridItem dgItem in dgListEdit.Items)
            {
                BLL.BiddingDtl cBiddingDtl = new BLL.BiddingDtl();
                cBiddingDtl.BiddingDtlCode = dgItem.Cells[0].Text;
                cBiddingDtl.Title = ((TextBox)dgItem.FindControl("txtTitle")).Text;
                cBiddingDtl.remark = ((TextBox)dgItem.FindControl("txtRemark")).Text;
                cBiddingDtl.Money = ((WebNumericEdit)dgItem.FindControl("TxtTemMoney")).Value.ToString();
                //增加特殊需求

                string companyName = System.Configuration.ConfigurationManager.AppSettings["PMName"].ToString().ToLower();
                switch (companyName)
                {
                    case "gaokepm":
                        cBiddingDtl.OtherMoney = ((WebNumericEdit)dgItem.FindControl("TxtOtherMoney")).Value.ToString();

                        break;
                    default:
                        cBiddingDtl.OtherMoney = "0";
                        break;
                }
               
                RmsPM.Web.UserControls.InputCostBudgetDtl cb = ((RmsPM.Web.UserControls.InputCostBudgetDtl)dgItem.FindControl("Inputcostbudgetdtl1"));
                cBiddingDtl.CostCode = cb.CostCode;
                cBiddingDtl.CostBudgetSetCode = cb.CostBudgetSetCode;
                cBiddingDtl.PBSCode = cb.PBSCode;
                cBiddingDtl.PBSType = cb.PBSType;
                cBiddingDtl.flag = "1";
                if (flag)
                {
                    cBiddingDtl.BiddingCode = this.ApplicationCode;
                    cBiddingDtl.BiddingDtlSubmit();
                }
                else
                {
                    cBiddingDtl.BiddingCode = this.ApplicationCode;
                    cBiddingDtl.BiddingDtlAdd();
                }
                TeamMoneyTemp += decimal.Parse(((WebNumericEdit)dgItem.FindControl("TxtTemMoney")).Value.ToString());
            }
            BLL.Bidding b = new BLL.Bidding();
            b.BiddingCode = this.ApplicationCode;
            b.Money = TeamMoneyTemp.ToString();
            b.BiddingSubmit();
            this.TeamMoney = TeamMoneyTemp.ToString();
            
        }
        /// <summary>
        /// 重置
        /// </summary>
        public void AddData()
        {
            _SubmitData(false);
         }
        /// <summary>
        /// 调整
        /// </summary>
        public void SubmitData()
        {
            bool isEmitDtl = false;
            BLL.Bidding bd = new BLL.Bidding();
            bd.BiddingCode = this.ApplicationCode;
            DataTable dtselect = bd.GetBiddingReturn();
            DataTable dt = GetSourceTable();
            for (int i = 0; i < dtselect.Rows.Count; i++)
            {
                if (dt.Select("BiddingDtlCode='" + dtselect.Rows[i]["BiddingDtlCode"].ToString()+"'").Length > 0)
                    isEmitDtl = true;

            }
            if (isEmitDtl)
                _SubmitData(false);
            else
                _SubmitData(true);
        }
        /// ****************************************************************************
        /// <summary>
        /// 删除数据
        /// </summary>
        /// ****************************************************************************
        public void Delete()
        {
            BLL.BiddingDtl cBiddingDtl = new BLL.BiddingDtl();
            cBiddingDtl.BiddingCode = this.ApplicationCode;
            DataTable dtBiddingdtl=cBiddingDtl.GetBiddingDtls();
            foreach (DataRow tempdr in dtBiddingdtl.Select())
            {
                cBiddingDtl.BiddingDtlCode = tempdr["BiddingDtlCode"].ToString();
                cBiddingDtl.BiddingDtlDelete();
            }
            
        }


        protected void dgListEdit_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            this.ViewState["DeleteItems"] += ",'" + e.Item.Cells[0].Text + "'";
            DataTable dt = GetSourceTable();
            DataRow[] drw = dt.Select("BiddingDtlCode=" + e.Item.Cells[0].Text);
            
            foreach(DataRow dr in drw)
            {
                dt.Rows.Remove(dr);
            }
            this.dgListEdit.DataSource = dt;
            this.dgListEdit.DataBind();
        }
}
}