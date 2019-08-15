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
	/// CostBatchPaymentModify ��ժҪ˵����
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
            }
        }

        private void LoadData()
        {
            try
            {
                EntityData entity = null;
                DataTable tbDtl = new DataTable();

                //����ʱ���봫����Ŀ����
                if ((PaymentCode == "") && (this.txtProjectCode.Value == ""))
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "����Ŀ���룬��������"));
                    Response.Write(Rms.Web.JavaScript.WinClose(true));
                    return;
                }

                if (PaymentCode != "") //�޸�
                {
                    this.txtIsNew.Value = "0";

                    entity = DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(PaymentCode);
                    SetControlMessage(entity);

                    switch (this.up_sPMName.ToUpper())
                    {
                        case "SHIMAOPM":
                            //��ï��ֻ��ʾ�����ŵ�Ԥ��� xyq 2007.7.25
                            BLL.PageFacade.LoadCostBudgetSetSelect(this.sltCostBudgetSet, "", this.txtProjectCode.Value, user.m_EntityDataAccessUnit);
                            break;

                        default:
                            BLL.PageFacade.LoadCostBudgetSetSelect(this.sltCostBudgetSet, "", this.txtProjectCode.Value);
                            break;

                    }

                    //��ʾ��ϸ
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

                    //�Ƿ�Ҫ�Զ�ͬ�����
                    if ((entity.GetInt("Status") == 1) || (entity.GetInt("Status") == 2))
                    {
                        this.txtAutoCreatePayout.Value = "1";
                        this.trAutoCreatePayout.Visible = true;
                    }
                }
                else
                {
                    //����
                    this.txtIsNew.Value = "1";

                    switch (this.up_sPMName.ToUpper())
                    {
                        case "SHIMAOPM":
                            //��ï��ֻ��ʾ�����ŵ�Ԥ��� xyq 2007.7.25
                            BLL.PageFacade.LoadCostBudgetSetSelect(this.sltCostBudgetSet, "", this.txtProjectCode.Value, user.m_EntityDataAccessUnit);
                            break;

                        default:
                            BLL.PageFacade.LoadCostBudgetSetSelect(this.sltCostBudgetSet, "", this.txtProjectCode.Value);
                            break;

                    }

                    //ȱʡֵ
                    this.dtPayDate.Value = DateTime.Today.ToString("yyyy-MM-dd");

                    //                    this.ucGroup.Value = RmsPM.BLL.SystemGroupRule.getsystemgroup(ud_sSortID, ud_sClassCode);
                    //                    this.ucGroup.SelectAllLeaf = false;

                    //����ʱ������������ţ�PaymentID = PaymentCode
                    PaymentCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PaymentCode");
                    this.txtPaymentID.Value = PaymentCode;
                    this.txtPaymentCode.Value = PaymentCode;

                    tbDtl = GenerateEmptyDtl("", this.txtProjectCode.Value);
                }

                if (entity != null) entity.Dispose();

                //�ݴ���ϸ��
                Session["CostBatchPaymentModify_tbDtl"] = tbDtl;

                BindDataGrid(tbDtl);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ���ݳ���" + ex.Message));
            }
        }

        /// <summary>
        /// ��ʾ����ϸ
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ�б����" + ex.Message));
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
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
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
        /// ��Ч�Լ��
        /// </summary>
        /// <param name="Hint"></param>
        /// <returns></returns>
        private bool CheckValid(ref string Hint, DataTable tbDtl)
        {
            Hint = "";

            if (this.txtCostBudgetSetCode.Value.Trim() == "")
            {
                Hint = "��ѡ��Ԥ���";
                return false;
            }

            if (this.dtPayDate.Value.Trim() == "")
            {
                Hint = "��������󸶿���";
                return false;
            }

            if (this.ucGroup.Value == "")
            {
                Hint = "�������������";
                return false;
            }

            //����ʱ����Ƿ��ܲ���������
            if (this.txtIsNew.Value == "1")
            {
                if (!user.HasTypeOperationRight("060111", this.ucGroup.Value))
                {
                    Hint = "�����ܲ��������������";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ����
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

                dr["Payer"] = "�ɱ��������";

                //�Ǻ�ͬ���
                dr["IsContract"] = 0;
                dr["ContractCode"] = "";

                dr["GroupCode"] = this.ucGroup.Value;
                dr["PayDate"] = BLL.ConvertRule.ToDate(this.dtPayDate.Value);

                //��ϸ�ܽ��
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
		/// ��������ϸ
		/// </summary>
		private void SaveDetail(EntityData entity, DataTable tb) 
		{
			try 
			{
				entity.SetCurrentTable("Payment");
				string PaymentCode = entity.GetString("PaymentCode");
				string ProjectCode = entity.GetString("ProjectCode");

				//�ɵ���ϸ
				entity.SetCurrentTable("PaymentItem");

				//ɾ��ԭ��������û�е�
				foreach(DataRow dr in entity.CurrentTable.Rows) 
				{
                    string CostCode = dr["CostCode"].ToString();
                    if (tb.Select("CostCode='" + CostCode + "'").Length == 0) 
					{
						dr.Delete();
					}
				}

				//�������޸�
				foreach(DataRow dr in tb.Rows) 
				{
                    string CostCode = dr["CostCode"].ToString();
					DataRow drNew;
					DataRow[] drs;

					//�����ϸ
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
					drNew["MoneyType"] = "����� (RMB)";
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
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                //�µ���ϸ��
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
                Response.Write(JavaScript.Alert(true, "����ʧ�ܣ�" + ex.Message));
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                return;
            }

            //��˺��޸�ʱ���Զ�ͬ��������
            if (this.txtAutoCreatePayout.Value == "1")
            {
                try
                {
                    BLL.PaymentRule.AutoCreatePayoutFromPayment(this.txtPaymentCode.Value, user.UserCode);
                }
                catch (Exception ex)
                {
                    Response.Write(JavaScript.Alert(true, "�Զ����¸��ʧ�ܣ�" + ex.Message));
                    Response.Write(JavaScript.WinClose(true));
                    ApplicationLog.WriteLog(this.ToString(), ex, "");
                }
            }

            GoBack();
        }

        /// <summary>
        /// ����
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
        /// ɾ���յ������ϸ
        /// </summary>
        /// <param name="tb"></param>
        private void DeleteNullDtl(DataTable tb)
        {
            try
            {
                //ɾ���յ���ϸ
                int c = tb.Rows.Count;
                for (int i = c - 1; i >= 0; i--)
                {
                    DataRow dr = tb.Rows[i];
                    bool isnull = false;

                    //��Ҷ���ķ�����ҲҪɾ��
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
        /// ��Ļ���ݱ��浽��ʱ��
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
                    throw new Exception("������" + CostCode + "����ʱ���в����ڣ����ܱ���");
                }

                dr["ItemMoney"] = BLL.ConvertRule.ToDecimal(txtItemMoney.Value);

                /*
                dr["ItemCash"] = txtItemMoney.ValueDecimal;
				dr["MoneyType"] = "����� (RMB)";
				dr["ExchangeRate"] = 1;
                dr["ItemMoney"] = txtItemMoney.ValueDecimal;
                 * */
			}

            //�ݴ���ϸ��
            Session["CostBatchPaymentModify_tbDtl"] = tb;

            if (isBindGrid)
            {
                BindDataGrid(tb);
            }

            return tb;
        }

        private void SetControlMessage(EntityData entity)
        {
            //�޸�
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
                Response.Write(Rms.Web.JavaScript.Alert(true, "��������"));
                Response.Write(Rms.Web.JavaScript.WinClose(true));
                return;
            }
        }

        /// <summary>
        /// ����ȱʡ����ϸ����Ԥ�������з����
        /// </summary>
        /// <param name="CostBudgetSetCode"></param>
        /// <param name="ProjectCode"></param>
        /// <returns></returns>
        private DataTable GenerateEmptyDtl(string CostBudgetSetCode, string ProjectCode)
        {
            try
            {
                DataTable tbDtl = new DataTable("PaymentItem");

                //��������Ϣ
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

                tbDtl.Columns.Add("ItemMoney", typeof(decimal)); //�����

                if (CostBudgetSetCode == "") return tbDtl;

                //ȡ��ǰ�ķ�����
                EntityData entityAllCBS = BLL.CostBudgetRule.GetAllCBSBySet(ProjectCode, CostBudgetSetCode);
                if (entityAllCBS == null) return tbDtl;

                //���Ʒ�����
                foreach (DataRow drCBS in entityAllCBS.CurrentTable.Rows)
                {
                    DataRow drNew = tbDtl.NewRow();

                    BLL.CostBudgetPageRule.FillCostBudgetDtlCBSData(drNew, drCBS);

                    if (BLL.ConvertRule.ToInt(drNew["Deep"]) == 1) //���ڵ������
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
        /// ����ϸ���
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

                //����ۼӵ�����
                foreach (DataRow drDtl in tbDtl.Rows)
                {
                    if (!BLL.ConvertRule.ToBool(drDtl["IsLeafCBS"])) //��Ҷ���
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
        /// ��ʾԤ�����Ϣ
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
        /// ѡ��Ԥ����
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

                //����ȱʡ��ϸ��
                DataTable tbDtl = GenerateEmptyDtl(CostBudgetSetCode, this.txtProjectCode.Value);

                //�ݴ���ϸ��
                Session["CostBatchPaymentModify_tbDtl"] = tbDtl;

                BindDataGrid(tbDtl);
            }
            catch (Exception ex)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "ѡ��Ԥ�������" + ex.Message));
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }
    }
}
