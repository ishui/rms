using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;
using Infragistics.WebUI.WebDataInput;
using RmsPM.Web;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// CostBatchPaymentModify 的摘要说明。
	/// </summary>
    public partial class CostBatchPaymentModify : PageBase
    {
        protected System.Web.UI.HtmlControls.HtmlInputButton btnAddFromCost;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtDetailCount;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                IniPage();
                LoadData();
            }
        }

        public string PaymentCode
        {
            get
            {
                return this.txtPaymentCode.Value;
            }
            set
            {
                this.txtPaymentCode.Value = value;
            }

        }

        private void IniPage()
        {
            try
            {
                this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
                this.txtPaymentCode.Value = Request.QueryString["PaymentCode"];
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
            }
        }

        private void LoadData()
        {
            try
            {
                EntityData entity = null;
                DataTable tbDtl = new DataTable();

                //新增时必须传入项目代码
                if ((PaymentCode == "") && (this.txtProjectCode.Value == ""))
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "无项目代码，不能新增"));
                    Response.Write(Rms.Web.JavaScript.WinClose(true));
                    return;
                }

                if (PaymentCode != "") //修改
                {
                    this.txtIsNew.Value = "0";

                    entity = DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(PaymentCode);
                    SetControlMessage(entity);

                    switch (this.up_sPMName.ToUpper())
                    {
                        case "SHIMAOPM":
                            //世茂：只显示本部门的预算表 xyq 2007.7.25
                            BLL.PageFacade.LoadCostBudgetSetSelect(this.sltCostBudgetSet, "", this.txtProjectCode.Value, user.m_EntityDataAccessUnit);
                            break;

                        default:
                            BLL.PageFacade.LoadCostBudgetSetSelect(this.sltCostBudgetSet, "", this.txtProjectCode.Value);
                            break;

                    }

                    //显示明细
                    DataTable tbPaymentItem = entity.Tables["PaymentItem"];
                    if (tbPaymentItem.Rows.Count > 0)
                    {
                        string CostBudgetSetCode = BLL.ConvertRule.ToString(tbPaymentItem.Rows[0]["CostBudgetSetCode"]);
                        this.sltCostBudgetSet.Value = CostBudgetSetCode;
                        LoadCostBudgetSet(CostBudgetSetCode);

                        tbDtl = GenerateEmptyDtl(CostBudgetSetCode, this.txtProjectCode.Value);
                        FillDtl(tbDtl, tbPaymentItem);
                        BindDataGrid(tbDtl);
                    }

                    //是否要自动同步付款单
                    if ((entity.GetInt("Status") == 1) || (entity.GetInt("Status") == 2))
                    {
                        this.txtAutoCreatePayout.Value = "1";
                        this.trAutoCreatePayout.Visible = true;
                    }
                }
                else
                {
                    //新增
                    this.txtIsNew.Value = "1";

                    switch (this.up_sPMName.ToUpper())
                    {
                        case "SHIMAOPM":
                            //世茂：只显示本部门的预算表 xyq 2007.7.25
                            BLL.PageFacade.LoadCostBudgetSetSelect(this.sltCostBudgetSet, "", this.txtProjectCode.Value, user.m_EntityDataAccessUnit);
                            break;

                        default:
                            BLL.PageFacade.LoadCostBudgetSetSelect(this.sltCostBudgetSet, "", this.txtProjectCode.Value);
                            break;

                    }

                    //缺省值
                    this.dtPayDate.Value = DateTime.Today.ToString("yyyy-MM-dd");

                    //                    this.ucGroup.Value = RmsPM.BLL.SystemGroupRule.getsystemgroup(ud_sSortID, ud_sClassCode);
                    //                    this.ucGroup.SelectAllLeaf = false;

                    //新增时立即生成请款单序号，PaymentID = PaymentCode
                    PaymentCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PaymentCode");
                    this.txtPaymentID.Value = PaymentCode;
                    this.txtPaymentCode.Value = PaymentCode;

                    tbDtl = GenerateEmptyDtl("", this.txtProjectCode.Value);
                }

                if (entity != null) entity.Dispose();

                //暂存明细表
                Session["CostBatchPaymentModify_tbDtl"] = tbDtl;

                BindDataGrid(tbDtl);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "显示数据出错：" + ex.Message));
            }
        }

        /// <summary>
        /// 显示请款单明细
        /// </summary>
        private void BindDataGrid(DataTable tb)
        {
            try
            {
                this.dgList.DataSource = tb;
                this.dgList.DataBind();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
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
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        override protected void InitEventHandler()
        {
            base.InitEventHandler();
        }

        /// <summary>
        /// 有效性检查
        /// </summary>
        /// <param name="Hint"></param>
        /// <returns></returns>
        private bool CheckValid(ref string Hint, DataTable tbDtl)
        {
            Hint = "";

            if (this.txtCostBudgetSetCode.Value.Trim() == "")
            {
                Hint = "请选择预算表";
                return false;
            }

            if (this.dtPayDate.Value.Trim() == "")
            {
                Hint = "请输入最后付款日";
                return false;
            }

            if (this.ucGroup.Value == "")
            {
                Hint = "请输入请款类型";
                return false;
            }

            //新增时检查是否能操作该类型
            if (this.txtIsNew.Value == "1")
            {
                if (!user.HasTypeOperationRight("060111", this.ucGroup.Value))
                {
                    Hint = "您不能操作这类批量请款";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save(DataTable tbDtl)
        {
            string paymentCode = this.txtPaymentCode.Value;
            string projectCode = this.txtProjectCode.Value;

            bool isNew = (this.txtIsNew.Value.Trim() == "1");

            try
            {
                EntityData entity = null;
                DataRow dr = null;

                if (isNew)
                {
                    entity = new EntityData("Standard_Payment");
                    dr = entity.GetNewRecord();

                    //					paymentCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PaymentCode");
                    //					this.txtPaymentCode.Value = paymentCode;

                    dr["PaymentCode"] = paymentCode;
                    dr["PaymentID"] = paymentCode;
                    dr["ProjectCode"] = projectCode;
                    dr["ApplyPerson"] = base.user.UserCode;
                    dr["ApplyDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                    dr["Status"] = 0;
                    entity.AddNewRecord(dr);
                }
                else
                {
                    entity = DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(paymentCode);
                    dr = entity.CurrentRow;
                }

                dr["Payer"] = "成本批量请款";

                //非合同请款
                dr["IsContract"] = 0;
                dr["ContractCode"] = "";

                dr["GroupCode"] = this.ucGroup.Value;
                dr["PayDate"] = BLL.ConvertRule.ToDate(this.dtPayDate.Value);

                //明细总金额
                dr["Money"] = BLL.MathRule.SumColumn(tbDtl, "ItemMoney");

                SaveDetail(entity, tbDtl);

                DAL.EntityDAO.PaymentDAO.SubmitAllStandard_Payment(entity);
                entity.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
		/// 保存请款单明细
		/// </summary>
		private void SaveDetail(EntityData entity, DataTable tb) 
		{
			try 
			{
				entity.SetCurrentTable("Payment");
				string PaymentCode = entity.GetString("PaymentCode");
				string ProjectCode = entity.GetString("ProjectCode");

				//旧的明细
				entity.SetCurrentTable("PaymentItem");

				//删除原先有现在没有的
				foreach(DataRow dr in entity.CurrentTable.Rows) 
				{
                    string CostCode = dr["CostCode"].ToString();
                    if (tb.Select("CostCode='" + CostCode + "'").Length == 0) 
					{
						dr.Delete();
					}
				}

				//新增或修改
				foreach(DataRow dr in tb.Rows) 
				{
                    string CostCode = dr["CostCode"].ToString();
					DataRow drNew;
					DataRow[] drs;

					//请款明细
					entity.SetCurrentTable("PaymentItem");
                    drs = entity.CurrentTable.Select("CostCode='" + CostCode + "'");

					if (drs.Length == 0) 
					{
						drNew = entity.CurrentTable.NewRow();

						string PaymentItemCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PaymentItemCode");
						drNew["PaymentItemCode"] = PaymentItemCode;
						drNew["PaymentCode"] = PaymentCode;
                        drNew["CostCode"] = CostCode;

						entity.CurrentTable.Rows.Add(drNew);
					}
					else 
					{
						drNew = drs[0];
					}

					drNew["Summary"] = dr["CostName"];

					drNew["CostBudgetSetCode"] = this.txtCostBudgetSetCode.Value;
					drNew["PBSType"] = this.txtPBSType.Value;
                    drNew["PBSCode"] = this.txtPBSCode.Value;

					drNew["ContractCostCashCode"] = "";
					drNew["ItemCash"] = dr["ItemMoney"];
					drNew["MoneyType"] = "人民币 (RMB)";
					drNew["ExchangeRate"] = 1;
                    drNew["ItemMoney"] = dr["ItemMoney"];

                    drNew["ItemCash0"] = dr["ItemMoney"];
                    
                    drNew["AlloType"] = "";

				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				throw ex;
			}
		}

        private bool IsArrayInclude(object[] arr, object val)
        {
            try
            {
                bool ret = false;

                foreach (object item in arr)
                {
                    if (BLL.ConvertRule.ToString(item) == BLL.ConvertRule.ToString(val))
                    {
                        ret = true;
                        break;
                    }
                }

                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                //新的明细表
                DataTable tbDtl = ScreenToTable(true);
                if (tbDtl == null) return;

//                Response.Write(Rms.Web.JavaScript.Alert(true, "2 tbDtl:" + tbDtl.Rows.Count.ToString()));

                string Hint = "";
                if (!CheckValid(ref Hint, tbDtl))
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
                    return;
                }

                DeleteNullDtl(tbDtl);

                Save(tbDtl);
            }
            catch (Exception ex)
            {
                Response.Write(JavaScript.Alert(true, "保存失败：" + ex.Message));
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                return;
            }

            //审核后修改时，自动同步付款单金额
            if (this.txtAutoCreatePayout.Value == "1")
            {
                try
                {
                    BLL.PaymentRule.AutoCreatePayoutFromPayment(this.txtPaymentCode.Value, user.UserCode);
                }
                catch (Exception ex)
                {
                    Response.Write(JavaScript.Alert(true, "自动更新付款单失败：" + ex.Message));
                    Response.Write(JavaScript.WinClose(true));
                    ApplicationLog.WriteLog(this.ToString(), ex, "");
                }
            }

            GoBack();
        }

        /// <summary>
        /// 返回
        /// </summary>
        private void GoBack()
        {
            string paymentCode = this.txtPaymentCode.Value;
            string projectCode = Request["ProjectCode"] + "";

            Response.Write(Rms.Web.JavaScript.ScriptStart);

            Response.Write("window.opener.location = window.opener.location;");
            Response.Write("window.location.href='..//Finance/PaymentInfo.aspx?ProjectCode=" + projectCode + "&PaymentCode=" + paymentCode + "';");
            //			Response.Write(Rms.Web.JavaScript.WinClose(false));
            Response.Write(Rms.Web.JavaScript.ScriptEnd);

        }

        /// <summary>
        /// 删除空的请款明细
        /// </summary>
        /// <param name="tb"></param>
        private void DeleteNullDtl(DataTable tb)
        {
            try
            {
                //删除空的明细
                int c = tb.Rows.Count;
                for (int i = c - 1; i >= 0; i--)
                {
                    DataRow dr = tb.Rows[i];
                    bool isnull = false;

                    //非叶结点的费用项也要删除
                    if (!BLL.ConvertRule.ToBool(dr["IsLeafCBS"]))
                    {
                        isnull = true;
                    }

                    if (BLL.ConvertRule.ToDecimal(dr["ItemMoney"]) == 0)
                    {
                        isnull = true;
                    }

                    if (isnull)
                    {
                        tb.Rows.Remove(dr);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 屏幕数据保存到临时表
        /// </summary>
        /// <returns></returns>
        private DataTable ScreenToTable(bool isBindGrid)
        {
            DataTable tb = (DataTable)Session["CostBatchPaymentModify_tbDtl"];

            foreach (RepeaterItem item in this.dgList.Items)
			{
                HtmlInputHidden txtCostCode = (HtmlInputHidden)item.FindControl("txtCostCode");
                HtmlInputText txtItemMoney = (HtmlInputText)item.FindControl("txtItemMoney");
			
                string CostCode = txtCostCode.Value;

                DataRow dr = null;
                DataRow[] drs = tb.Select("CostCode='" + CostCode + "'");
                if (drs.Length > 0)
                {
                    dr = drs[0];
                }
                else
                {
                    throw new Exception("费用项" + CostCode + "在临时表中不存在，不能保存");
                }

                dr["ItemMoney"] = BLL.ConvertRule.ToDecimal(txtItemMoney.Value);

                /*
                dr["ItemCash"] = txtItemMoney.ValueDecimal;
				dr["MoneyType"] = "人民币 (RMB)";
				dr["ExchangeRate"] = 1;
                dr["ItemMoney"] = txtItemMoney.ValueDecimal;
                 * */
			}

            //暂存明细表
            Session["CostBatchPaymentModify_tbDtl"] = tb;

            if (isBindGrid)
            {
                BindDataGrid(tb);
            }

            return tb;
        }

        private void SetControlMessage(EntityData entity)
        {
            //修改
            if (entity.HasRecord())
            {
                DataRow dr = entity.CurrentRow;
                this.txtPaymentCode.Value = entity.GetString("PaymentCode");
                this.txtProjectCode.Value = entity.GetString("ProjectCode");
                this.txtStatus.Value = entity.GetInt("Status").ToString();

                this.ucGroup.Value = entity.GetString("GroupCode");
                this.dtPayDate.Value = entity.GetDateTimeOnlyDate("PayDate");
                this.txtMoney.Value = BLL.MathRule.GetDecimalShowString(dr["Money"]);

                this.txtPaymentID.Value = this.txtPaymentCode.Value;
            }
            else
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "请款单不存在"));
                Response.Write(Rms.Web.JavaScript.WinClose(true));
                return;
            }
        }

        /// <summary>
        /// 生成缺省的明细（该预算表的所有费用项）
        /// </summary>
        /// <param name="CostBudgetSetCode"></param>
        /// <param name="ProjectCode"></param>
        /// <returns></returns>
        private DataTable GenerateEmptyDtl(string CostBudgetSetCode, string ProjectCode)
        {
            try
            {
                DataTable tbDtl = new DataTable("PaymentItem");

                //费用项信息
                tbDtl.Columns.Add("CostCode");
                tbDtl.Columns.Add("CostName");
                tbDtl.Columns.Add("SortID");
                tbDtl.Columns.Add("Deep", typeof(int));
                tbDtl.Columns.Add("ParentCode");
                tbDtl.Columns.Add("FullCode");
                tbDtl.Columns.Add("ChildCount", typeof(int));
                tbDtl.Columns.Add("IsLeafCBS", typeof(bool));
                tbDtl.Columns.Add("MeasurementUnit");

                tbDtl.Columns.Add("IsExpand", typeof(int));

                tbDtl.Columns.Add("ItemMoney", typeof(decimal)); //请款金额

                if (CostBudgetSetCode == "") return tbDtl;

                //取当前的费用项
                EntityData entityAllCBS = BLL.CostBudgetRule.GetAllCBSBySet(ProjectCode, CostBudgetSetCode);
                if (entityAllCBS == null) return tbDtl;

                //复制费用项
                foreach (DataRow drCBS in entityAllCBS.CurrentTable.Rows)
                {
                    DataRow drNew = tbDtl.NewRow();

                    BLL.CostBudgetPageRule.FillCostBudgetDtlCBSData(drNew, drCBS);

                    if (BLL.ConvertRule.ToInt(drNew["Deep"]) == 1) //根节点费用项
                    {
                        drNew["ParentCode"] = "";
                    }

                    tbDtl.Rows.Add(drNew);
                }

                entityAllCBS.Dispose();

                return tbDtl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 填明细金额
        /// </summary>
        /// <param name="tbEmpty"></param>
        /// <param name="tbData"></param>
        private void FillDtl(DataTable tbDtl, DataTable tbData)
        {
            try
            {
                foreach (DataRow drDtl in tbDtl.Rows)
                {
                    string CostCode = drDtl["CostCode"].ToString();
                    decimal ItemMoney = 0;

                    DataRow[] drsData = tbData.Select("CostCode='" + CostCode + "'");
                    if (drsData.Length > 0)
                    {
                        ItemMoney = BLL.ConvertRule.ToDecimal(drsData[0]["ItemMoney"]);
                    }

                    drDtl["ItemMoney"] = ItemMoney;
                }

                //金额累加到父项
                foreach (DataRow drDtl in tbDtl.Rows)
                {
                    if (!BLL.ConvertRule.ToBool(drDtl["IsLeafCBS"])) //非叶结点
                    {
                        DataRow[] drsChild = tbDtl.Select("FullCode like '" + BLL.ConvertRule.ToString(drDtl["FullCode"]) + "%'");
                        decimal TotalMoney = BLL.MathRule.SumColumn(drsChild, "ItemMoney");
                        drDtl["ItemMoney"] = TotalMoney;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 显示预算表信息
        /// </summary>
        /// <param name="CostBudgetSetCode"></param>
        private void LoadCostBudgetSet(string CostBudgetSetCode)
        {
            try
            {
                this.txtCostBudgetSetCode.Value = CostBudgetSetCode;

                EntityData entitySet = DAL.EntityDAO.CostBudgetDAO.GetCostBudgetSetByCode(CostBudgetSetCode);
                if (entitySet.HasRecord())
                {
                    this.txtPBSType.Value = entitySet.GetString("PBSType");
                    this.txtPBSCode.Value = entitySet.GetString("PBSCode");
                }
                entitySet.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 选择预算表后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCostBudgetSetChange_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string CostBudgetSetCode = this.sltCostBudgetSet.Value.Trim();

                if (CostBudgetSetCode == "") return;

                LoadCostBudgetSet(CostBudgetSetCode);

                //生成缺省明细表
                DataTable tbDtl = GenerateEmptyDtl(CostBudgetSetCode, this.txtProjectCode.Value);

                //暂存明细表
                Session["CostBatchPaymentModify_tbDtl"] = tbDtl;

                BindDataGrid(tbDtl);
            }
            catch (Exception ex)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "选择预算表后出错：" + ex.Message));
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }
    }
}
