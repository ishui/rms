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

namespace RmsPM.Web.Finance
{
    /// <summary>
    /// PayoutModify ��ժҪ˵����
    /// </summary>
    public partial class PayoutModify : PageBase
    {

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                IniPage();
                LoadData();
            }
        }

        private void IniPage()
        {
            try
            {
                this.inputSystemGroup.ClassCode = "0602";

                this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
                this.txtPayoutCode.Value = Request.QueryString["PayoutCode"];
                this.txtPaymentCode.Value = Request.QueryString["PaymentCode"];
                //				this.txtIsContract.Value = Request.QueryString["IsContract"];

                this.ucInputSubject.ProjectCode = this.txtProjectCode.Value;
                BLL.PageFacade.LoadDictionarySelect(this.sltPaymentType, "��������", "");

                if (hidDDLRMBValue.Value == "")
                {
                    this.ucExchangeRate.BindControl();
                    this.hidDDLRMBValue.Value = this.ucExchangeRate.MoneyTypeValue;
                }

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
            }
        }

        private void LoadData()
        {
            string PayoutCode = this.txtPayoutCode.Value;
            string ud_sProjectCode = Request["ProjectCode"] + "";

            try
            {
                DataTable tbDtl;
                //				string WBSCode = "";

                //����ʱ���봫����Ŀ����
                if ((PayoutCode == "") && (this.txtProjectCode.Value == ""))
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "����Ŀ���룬��������"));
                    return;
                }

                if (PayoutCode != "")
                {
                    //�޸�
                    this.txtIsNew.Value = "0";

                    EntityData entity = DAL.EntityDAO.PaymentDAO.GetPayoutByCode(PayoutCode);
                    if (entity.HasRecord())
                    {
                        DataRow dr = entity.CurrentRow;

                        this.txtProjectCode.Value = entity.GetString("ProjectCode");
                        this.txtSubjectSetCode.Value = entity.GetString("SubjectSetCode");

                        this.dtPayoutDate.Value = entity.GetDateTimeOnlyDate("PayoutDate");
                        //						this.lblMoney.Text = BLL.MathRule.GetDecimalShowString(dr["Money"]);

                        this.txtPayoutID.Value = entity.GetString("PayoutID");


                        this.ucExchangeRate.Cash = entity.GetDecimal("Cash");
                        this.ucExchangeRate.MoneyType = entity.GetString("MoneyType");
                        this.ucExchangeRate.ExchangeRate = entity.GetDecimal("ExchangeRate");
                        this.ucExchangeRate.BindControl();



                        this.txtMoney.Value = BLL.StringRule.BuildGeneralNumberString(entity.GetDecimal("Money"));

                        //						this.txtMoney.Value = BLL.StringRule.BuildGeneralNumberString(entity.GetDecimal("Money"));

                        this.lblSupplyName.Text = entity.GetString("SupplyName");
                        this.txtPayer.Value = entity.GetString("Payer");
                        this.txtSupplyCode.Value = entity.GetString("SupplyCode");

                        this.sltPaymentType.Value = entity.GetString("PaymentType");
                        this.txtBillNo.Value = entity.GetString("BillNo");
                        this.txtInvoNo.Value = entity.GetString("InvoNo");
                        this.txtReceiptCount.Value = entity.GetInt("ReceiptCount").ToString();

                        this.ucInputSubject.ProjectCode = this.txtProjectCode.Value;
                        this.ucInputSubject.Value = entity.GetString("SubjectCode");

                        this.txtStatus.Value = entity.GetInt("Status").ToString();

                        this.inputSystemGroup.Value = entity.GetString("GroupCode");

                        tbDtl = BLL.PaymentRule.GeneratePayoutItemTable(PayoutCode);

                        //�޸�ʱ����ϸȱʡΪѡ��
                        foreach (DataRow drTemp in tbDtl.Rows)
                        {
                            drTemp["Checked"] = 1;
                        }
                    }
                    else
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "���������"));
                        return;
                    }
                    entity.Dispose();

                    BLL.PaymentRule.GeneratePaymentItemTableValue(tbDtl);
                }
                else
                {
                    //����
                    this.txtIsNew.Value = "1";
                    this.txtSubjectSetCode.Value = BLL.ProjectRule.GetSubjectSetCodeByProject(this.txtProjectCode.Value);
                    string PaymentCodes = this.txtPaymentCode.Value;
                    string ud_sSortID = Request["Type"] + "";
                    string ud_sGroupCode = "";


                    if (PaymentCodes == "")
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "δ��������"));
                        Response.Write(Rms.Web.JavaScript.WinClose(true));
                        return;
                    }

                    switch (this.up_sPMNameLower)
                    { 
                        case "shimaopm":
                            string ud_sProjectName = BLL.ProjectRule.GetProjectName(ud_sProjectCode);
                            ud_sSortID = BLL.SystemGroupRule.GetSystemGroupSortIDByGroupNameAndClassCode(ud_sProjectName, "0602");
                            break;
                        default:
                            break;

                    }

                    if (ud_sSortID.Trim() != "")
                    {
                        ud_sGroupCode = BLL.SystemGroupRule.GetSystemGroupCodeBySortID(ud_sSortID.Trim(), "0602");
                    }

                    //ȱʡֵ
                    this.dtPayoutDate.Value = DateTime.Today.ToString("yyyy-MM-dd");

                    string payer = "";
                    string SupplyName = "";
                    tbDtl = BLL.PaymentRule.GeneratePayoutItemTable(PayoutCode);

                    string[] arrPaymentCode = PaymentCodes.Split(",".ToCharArray());
                    int iCount = arrPaymentCode.Length;
                    for (int i = 0; i < iCount; i++)
                    {
                        string PaymentCode = arrPaymentCode[i];

                        //����Ϣ
                        EntityData entityPayment = DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(PaymentCode);
                        if (!entityPayment.HasRecord())
                        {
                            Response.Write(Rms.Web.JavaScript.Alert(true, string.Format("����{0}��������", PaymentCode)));
                            Response.Write(Rms.Web.JavaScript.WinClose(true));
                            return;
                        }

                        if (i > 0)
                        {
                            //��������ͬһ�ܿ���
                            if ((entityPayment.GetString("SupplyName") != SupplyName) || (entityPayment.GetString("Payer") != payer))
                            {
                                Response.Write(Rms.Web.JavaScript.Alert(true, "ֻ��ͬһ�ܿλ���ܿ��˲���һ�𸶿�"));
                                Response.Write(Rms.Web.JavaScript.WinClose(true));
                                return;
                            }
                        }
                        else
                        {
                            //��������ͬһ�ܿ���
                            SupplyName = entityPayment.GetString("SupplyName");
                            payer = entityPayment.GetString("Payer");
                            this.txtSupplyCode.Value = entityPayment.GetString("SupplyCode");
                            this.lblSupplyName.Text = entityPayment.GetString("SupplyName");
                            this.txtPayer.Value = payer;
                        }

                        string PaymentID = entityPayment.GetString("PaymentID");

                        //ȱʡ�������ɸ�����ϸ
                        entityPayment.SetCurrentTable("PaymentItem");
                        foreach (DataRow dr in entityPayment.CurrentTable.Rows)
                        {
                            DataRow drNew = tbDtl.NewRow();

                            int sno = BLL.ConvertRule.ToInt(this.txtDetailSno.Value.Trim()) + 1;
                            this.txtDetailSno.Value = sno.ToString();

                            drNew["PayoutItemCode"] = -sno;
                            drNew["PaymentItemCode"] = BLL.ConvertRule.ToString(dr["PaymentItemCode"]);
                            drNew["PaymentID"] = PaymentID;
                            drNew["Summary"] = BLL.ConvertRule.ToString(dr["Summary"]);
                            drNew["CostCode"] = BLL.ConvertRule.ToString(dr["CostCode"]);
                            drNew["ItemMoney"] = BLL.ConvertRule.ToDecimalObj(dr["ItemMoney"]);
                            drNew["SubjectCode"] = BLL.CBSRule.GetCBSSubjectCode(BLL.ConvertRule.ToString(dr["CostCode"]));
                            drNew["Checked"] = 0;


                            drNew["ItemCash"] = BLL.ConvertRule.ToDecimalObj(dr["ItemCash"]);
                            drNew["PaymentMoneyType"] = BLL.ConvertRule.ToDecimalObj(dr["MoneyType"]);
                            drNew["PaymentExchangeRate"] = BLL.ConvertRule.ToDecimalObj(dr["ExchangeRate"]);


                            //						drNew["PayoutMoney"] = BLL.ConvertRule.ToDecimalObj(dr["ItemMoney"]);

                            BLL.PaymentRule.GeneratePaymentItemTableValue(drNew);

                            //��ʣ�������ʱ�������ɵ�����
                            if (BLL.ConvertRule.ToDecimal(drNew["RemainItemMoney"]) != 0)
                            {
                                tbDtl.Rows.Add(drNew);
                            }
                        }
                        entityPayment.Dispose();
                    }

                    //������ϸ�ĸ�����=����ʣ����
                    //					foreach(DataRow dr in tbDtl.Rows) 
                    //					{
                    //						string PaymentItemCode = BLL.ConvertRule.ToString(dr["PaymentItemCode"]);
                    //						decimal PayoutMoney = BLL.ConvertRule.ToDecimal(dr["RemainItemMoney"]);
                    //						dr["PayoutMoney"] = PayoutMoney;
                    //					}

                    //����ʱ������ȱʡ�����ܶ�=������
                    string[] arrField = { "ItemMoney", "TotalPayoutMoney" };
                    decimal[] arrSum = BLL.MathRule.SumColumn(tbDtl, arrField);
                    decimal TotalPayoutMoney = arrSum[0] - arrSum[1];

                    this.txtMoney.Value = TotalPayoutMoney;
                    this.ucExchangeRate.Cash = TotalPayoutMoney;
                    this.ucExchangeRate.BindControl();

                   					this.txtMoney.Value = TotalPayoutMoney.ToString("N");

                    //����ʱ�������ɸ����ţ�PayoutID = PayoutCode
                    PayoutCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutCode");
                    this.txtPayoutID.Value = PayoutCode;
                    this.txtPayoutCode.Value = PayoutCode;

                    if (ud_sGroupCode != "")
                    {
                        this.inputSystemGroup.Value = ud_sGroupCode;
                    }
                }

                BLL.PaymentRule.VoucherDetailAddColumnSubjectName(tbDtl, this.txtSubjectSetCode.Value);
                tbDtl.Columns.Add("SubjectHint", typeof(String));
                BindDataGrid(tbDtl);

                //				BLL.PageFacade.LoadWBSTaskFullNameSelect(this.sltTask, "", this.txtProjectCode.Value);
                //				this.sltTask.Value = WBSCode;

                ((HtmlInputText)this.ucExchangeRate.FindControl("ExchangeRateControl_R")).Value = (this.ucExchangeRate.Cash * this.ucExchangeRate.ExchangeRate).ToString();

                //����ʱ���Բ�¼��Ŀ����˺��޸�ʱ����¼���Ŀ 2018.6.12
                if (BLL.ConvertRule.ToInt(this.txtStatus.Value) == 0)
                {
                    this.lblSubjectCodeHint.Visible = false;
                }

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ���ݳ���" + ex.Message));
            }

        }

        /// <summary>
        /// ��ʾ�����ϸ
        /// </summary>
        private void BindDataGrid(DataTable tb)
        {
            try
            {
                string[] arrField = { "ItemMoney", "TotalPayoutMoney", "RemainItemMoney", "PayoutMoney" };
                decimal[] arrSum = BLL.MathRule.SumColumn(tb, arrField);
                this.dgList.Columns[4].FooterText = arrSum[0].ToString("N");
                this.dgList.Columns[5].FooterText = arrSum[1].ToString("N");
                this.dgList.Columns[6].FooterText = arrSum[2].ToString("N");
                this.txtSumPayoutMoney.Value = arrSum[3].ToString("N");

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
            this.dgList.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemCreated);
            this.dgList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgList_DeleteCommand);
            this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);
            this.btnSave.ServerClick += new System.EventHandler(this.btnSave_ServerClick);
        }
        #endregion

        /// <summary>
        /// ��Ч�Լ��
        /// </summary>
        /// <param name="Hint"></param>
        /// <returns></returns>
        private bool CheckValid(ref string Hint, DataTable tbDtl)
        {
            Hint = "";

            if (this.dtPayoutDate.Value.Trim() == "")
            {
                Hint = "�����븶������ ��";
                return false;
            }

            if (this.inputSystemGroup.Value.Trim() == "")
            {
                Hint = "������ϵͳ���� ��";
                return false;
            }

            string SubjectCode = this.ucInputSubject.Value;

            //����ʱ���Բ�¼��Ŀ����˺��޸�ʱ����¼���Ŀ
            if (BLL.ConvertRule.ToInt(this.txtStatus.Value) > 0)
            {
                if ((SubjectCode == null) || (SubjectCode.Trim().Length == 0))
                {
                    Hint = "�����������Ŀ ��";
                    return false;
                }
            }

            Hint = BLL.SubjectRule.CheckSubject(SubjectCode, txtSubjectSetCode.Value, string.Format("��Ŀ��š�{0}��", SubjectCode));
            if (Hint != "")
                return false;

            if (this.txtReceiptCount.Value != "")
            {
                if (!Rms.Check.StringCheck.IsInt(this.txtReceiptCount.Value))
                {
                    Hint = "������������������ ��";
                    return false;
                }
            }

            /*
                        if ( dtPay != "" )
                        {
                            if ( !Rms.Check.StringCheck.IsDateTime(dtPay))
                            {
                                Response.Write(Rms.Web.JavaScript.Alert(true,"����ʱ����д���� ��"));
                                return;
                            }
                        }

                        string money = this.txtMoney.Text.Trim();
                        if ( money != "" )
                        {
                            if ( !Rms.Check.StringCheck.IsNumber(money) )
                            {
                                Response.Write(Rms.Web.JavaScript.Alert(true,"�ܽ����д���� ��"));
                                return;
                            }
                        }
            */

            int status = BLL.ConvertRule.ToInt(this.txtStatus.Value);
            EntityData entityOld = null;
            //			if (status > 0) 
            if (this.txtPayoutCode.Value != "")  //�޸�ʱ��Ҫȡ�޸�ǰ�ĵ��ݽ��
            {
                entityOld = DAL.EntityDAO.PaymentDAO.GetPayoutItemByPayoutCode(this.txtPayoutCode.Value);
            }

            foreach (DataRow dr in tbDtl.Rows)
            {
                string PayoutItemCode = BLL.ConvertRule.ToString(dr["PayoutItemCode"]);
                decimal ItemMoney = BLL.ConvertRule.ToDecimal(dr["ItemMoney"]);
                decimal TotalPayoutMoney = BLL.ConvertRule.ToDecimal(dr["TotalPayoutMoney"]);
                decimal PayoutMoney = BLL.ConvertRule.ToDecimal(dr["PayoutMoney"]);
                decimal RemainItemMoney = ItemMoney - TotalPayoutMoney;

                //�޸�ʱ��ʣ�ึ������Ҫ���ϱ����޸�ǰ�ĸ�����
                if (entityOld != null)
                {
                    DataRow[] drs = entityOld.CurrentTable.Select("PayoutItemCode='" + PayoutItemCode + "'");
                    if (drs.Length > 0)
                    {
                        RemainItemMoney = RemainItemMoney + BLL.ConvertRule.ToDecimal(drs[0]["PayoutMoney"]);
                    }
                }

                if (PayoutMoney > RemainItemMoney)
                {
                    Hint = string.Format("���θ����{0}�����ܳ���δ����{1}��", PayoutMoney, RemainItemMoney);
                    return false;
                }

                //���θ����Ϊ0ʱ�ż��
                if (PayoutMoney != 0)
                {
                    SubjectCode = BLL.ConvertRule.ToString(dr["SubjectCode"]);

                    //����ʱ���Բ�¼��Ŀ����˺��޸�ʱ����¼���Ŀ
                    if (BLL.ConvertRule.ToInt(this.txtStatus.Value) > 0)
                    {
                        if ((SubjectCode == null) || (SubjectCode.Trim().Length == 0))
                        {
                            Hint = "�������Ŀ��� ��";
                            return false;
                        }
                    }

                    Hint = BLL.SubjectRule.CheckSubject(SubjectCode, txtSubjectSetCode.Value, string.Format("��Ŀ��š�{0}��", SubjectCode));
                    if (Hint != "")
                        return false;
                }
            }

            /*
            if ( txtMoney.Value != "")
            {
                if ( !Rms.Check.StringCheck.IsNumber(txtMoney.Value))
                {
                    Hint = "�����ܶ����ָ�ʽ����ȷ !";
                    return false;
                }
            }
            */

            decimal Money = this.txtMoney.ValueDecimal;
            Money = this.ucExchangeRate.Cash * this.ucExchangeRate.ExchangeRate;

            decimal DtlMoney = BLL.MathRule.SumColumn(tbDtl, "PayoutMoney");
            if (Money != DtlMoney)
            {
                Hint = "�����ܶ����ϸ��ƽ������ !";
                return false;
            }

            return true;
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Save(DataTable tbDtl)
        {
            string PayoutCode = this.txtPayoutCode.Value;
            string projectCode = this.txtProjectCode.Value;
            string ud_sPaymentCodes = Request["PaymentCode"] + "";

            bool isNew = (this.txtIsNew.Value.Trim() == "1");
            int status = BLL.ConvertRule.ToInt(this.txtStatus.Value);

            try
            {
                //��˺��޸�ʱ��Ҫ���¼��������Ѹ�״̬
                //�޸�ǰ����
                if (status > 0)
                {
                    BLL.PaymentRule.UpdatePaymentStatusByPayout(PayoutCode, base.user.UserCode);
                }

                EntityData entity = null;
                DataRow dr = null;

                if (isNew)
                {
                    entity = new EntityData("Standard_Payout");
                    dr = entity.GetNewRecord();

                    //					PayoutCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutCode");
                    //					this.txtPayoutCode.Value = PayoutCode;

                    dr["PayoutCode"] = PayoutCode;
                    dr["PayoutID"] = PayoutCode;
                    dr["ProjectCode"] = projectCode;
                    dr["SubjectSetCode"] = this.txtSubjectSetCode.Value;
                    dr["InputPerson"] = base.user.UserCode;
                    dr["InputDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                    dr["Status"] = 0;
                    entity.AddNewRecord(dr);
                }
                else
                {
                    entity = DAL.EntityDAO.PaymentDAO.GetStandard_PayoutByCode(PayoutCode);
                    dr = entity.CurrentRow;
                }

                //				dr["PayoutID"] = this.txtPayoutID.Value;

                dr["PaymentCodes"] = ud_sPaymentCodes;

                dr["Payer"] = this.txtPayer.Value;
                dr["SupplyCode"] = this.txtSupplyCode.Value;
                dr["SupplyName"] = this.lblSupplyName.Text;

                dr["IsApportioned"] = 0;

                dr["PayoutDate"] = BLL.ConvertRule.ToDate(this.dtPayoutDate.Value);

                dr["PaymentType"] = this.sltPaymentType.Value;
                dr["BillNo"] = this.txtBillNo.Value;
                dr["InvoNo"] = this.txtInvoNo.Value;
                dr["ReceiptCount"] = BLL.ConvertRule.ToIntObj(this.txtReceiptCount.Value);
                dr["SubjectCode"] = this.ucInputSubject.Value;

                dr["GroupCode"] = this.inputSystemGroup.Value;

                //��ϸ�ܽ��
                dr["Money"] = BLL.MathRule.SumColumn(tbDtl, "PayoutMoney");

                dr["Cash"] = this.ucExchangeRate.Cash;
                dr["MoneyType"] = this.ucExchangeRate.MoneyType;
                dr["ExchangeRate"] = this.ucExchangeRate.ExchangeRate;

                SaveDetail(entity, tbDtl);

                DAL.EntityDAO.PaymentDAO.SubmitAllStandard_Payout(entity);
                entity.Dispose();

                //��˺��޸�ʱ��Ҫ���¼��������Ѹ�״̬
                //�޸ĺ�����
                if (status > 0)
                {
                    BLL.PaymentRule.UpdatePaymentStatusByPayout(PayoutCode, base.user.UserCode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ���渶���ϸ
        /// </summary>
        private void SaveDetail(EntityData entity, DataTable tb)
        {
            try
            {
                entity.SetCurrentTable("Payout");
                string PayoutCode = entity.GetString("PayoutCode");
                string ProjectCode = entity.GetString("ProjectCode");

                //���θ�����Ϊ0��ɾ��
                for (int i = tb.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = tb.Rows[i];
                    if (BLL.ConvertRule.ToDecimal(dr["PayoutMoney"]) == 0)
                    {
                        tb.Rows.Remove(dr);
                    }
                }

                //�ɵ���ϸ
                entity.SetCurrentTable("PayoutItem");

                //ɾ��ԭ��������û�е�
                foreach (DataRow dr in entity.CurrentTable.Rows)
                {
                    string PayoutItemCode = dr["PayoutItemCode"].ToString();
                    if (tb.Select("PayoutItemCode='" + PayoutItemCode + "'").Length == 0)
                    {
                        dr.Delete();

                        //ɾ�������̯��¥��
                        DataRow[] drs = entity.Tables["PayoutItemBuilding"].Select("PayoutItemCode='" + PayoutItemCode + "'");
                        foreach (DataRow drI in drs)
                        {
                            drI.Delete();
                        }
                    }
                }

                //�������޸�
                foreach (DataRow dr in tb.Rows)
                {
                    string PayoutItemCode = dr["PayoutItemCode"].ToString();
                    DataRow drNew;
                    DataRow[] drs;

                    //������ϸ
                    entity.SetCurrentTable("PayoutItem");
                    drs = entity.CurrentTable.Select("PayoutItemCode='" + PayoutItemCode + "'");

                    if (drs.Length == 0)
                    {
                        drNew = entity.CurrentTable.NewRow();

                        PayoutItemCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutItemCode");
                        drNew["PayoutItemCode"] = PayoutItemCode;
                        drNew["PayoutCode"] = PayoutCode;
                        drNew["PaymentItemCode"] = BLL.ConvertRule.ToString(dr["PaymentItemCode"]);

                        entity.CurrentTable.Rows.Add(drNew);
                    }
                    else
                    {
                        drNew = drs[0];
                    }

                    drNew["PayoutMoney"] = BLL.ConvertRule.ToDecimalObj(dr["PayoutMoney"]);
                    drNew["PayoutCash"] = BLL.ConvertRule.ToDecimalObj(dr["PayoutCash"]);
                    drNew["MoneyType"] = dr["MoneyType"].ToString();
                    drNew["ExchangeRate"] = BLL.ConvertRule.ToDecimalObj(dr["ExchangeRate"]);

                    drNew["SubjectCode"] = BLL.ConvertRule.ToString(dr["SubjectCode"]);

                    //���������ϸʱ��ȡ����ϸ�ķ�̯��ʽ�����µ������ϸ  begin---------------------
                    if (drs.Length == 0)
                    {
                        string PaymentItemCode = BLL.ConvertRule.ToString(dr["PaymentItemCode"]);
                        EntityData entityPaymentItem = DAL.EntityDAO.PaymentDAO.GetPaymentItemByCode(PaymentItemCode);
                        EntityData entityPaymentItemBuilding = DAL.EntityDAO.PaymentDAO.GetPaymentItemBuildingByPaymentItemCode(PaymentItemCode);

                        drNew["AlloType"] = entityPaymentItem.GetString("AlloType");
                        drNew["IsManualAlloc"] = 0;

                        //�������Ʒ�̯��ϸ  begin-------------------------
                        entity.SetCurrentTable("PayoutItemBuilding");

                        //ɾ�������̯��¥��
                        DataRow[] drsTemp = entity.Tables["PayoutItemBuilding"].Select("PayoutItemCode='" + PayoutItemCode + "'");
                        foreach (DataRow drI in drsTemp)
                        {
                            drI.Delete();
                        }

                        foreach (DataRow drSrc in entityPaymentItemBuilding.CurrentTable.Rows)
                        {
                            DataRow drDst = entity.GetNewRecord();

                            drDst["SystemID"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutItemBuildingCode");
                            drDst["PayoutItemCode"] = PayoutItemCode;
                            drDst["PayoutCode"] = PayoutCode;
                            drDst["BuildingCode"] = drSrc["BuildingCode"];
                            drDst["PBSUnitCode"] = drSrc["PBSUnitCode"];
                            drDst["ItemBuildingMoney"] = decimal.Zero;

                            entity.AddNewRecord(drDst);
                        }
                        //�������Ʒ�̯��ϸ  end-------------------------

                        entityPaymentItem.Dispose();
                        entityPaymentItemBuilding.Dispose();
                    }
                    //���������ϸʱ��ȡ����ϸ�ķ�̯��ʽ�����µ������ϸ  end---------------------

                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
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
        private void btnSave_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                //�µ���ϸ��
                DataTable tbDtl = ScreenToTable(true);
                if (tbDtl == null) return;

                string Hint = "";
                if (!CheckValid(ref Hint, tbDtl))
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
                    BindDataGrid(tbDtl);
                    return;
                }

                Save(tbDtl);
            }
            catch (Exception ex)
            {
                Response.Write(JavaScript.Alert(true, "����ʧ�ܣ�" + ex.Message));
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                return;
            }

            GoBack();
        }

        /// <summary>
        /// ����
        /// </summary>
        private void GoBack()
        {
            Response.Write(Rms.Web.JavaScript.ScriptStart);

            //�����ʼ�ս��븶���Ϣҳ�棨Ӧ���� -> ���
            Response.Write(string.Format("window.opener.location = '../Finance/PayoutInfo.aspx?PayoutCode={0}';", this.txtPayoutCode.Value));
            //			Response.Write("window.opener.location = window.opener.location;");
            Response.Write(Rms.Web.JavaScript.WinClose(false));
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }

        /// <summary>
        /// ��Ļ���ݱ��浽��ʱ��
        /// </summary>
        /// <returns></returns>
        private DataTable ScreenToTable(bool isBindGrid)
        {
            DataTable tb = BLL.PaymentRule.GeneratePayoutItemTable("");
            BLL.PaymentRule.VoucherDetailAddColumnSubjectName(tb, this.txtSubjectSetCode.Value);
            tb.Columns.Add("SubjectHint", typeof(String));

            foreach (DataGridItem item in this.dgList.Items)
            {
                HtmlInputHidden txtSelect = (HtmlInputHidden)item.FindControl("txtSelect");
                HtmlInputHidden txtPayoutItemCode = (HtmlInputHidden)item.FindControl("txtPayoutItemCode");
                HtmlInputHidden txtSummary = (HtmlInputHidden)item.FindControl("txtSummary");
                HtmlInputHidden txtItemMoney = (HtmlInputHidden)item.FindControl("txtItemMoney");
                HtmlInputHidden txtTotalPayoutMoney = (HtmlInputHidden)item.FindControl("txtTotalPayoutMoney");
                HtmlInputHidden txtRemainItemMoney = (HtmlInputHidden)item.FindControl("txtRemainItemMoney");

                WebNumericEdit txtPayoutMoney = (WebNumericEdit)item.FindControl("txtPayoutMoney");
                //				HtmlInputText txtPayoutMoney = (HtmlInputText)item.FindControl("txtPayoutMoney");

                HtmlInputHidden txtCostCode = (HtmlInputHidden)item.FindControl("txtCostCode");
                HtmlInputHidden txtCostName = (HtmlInputHidden)item.FindControl("txtCostName");
                RmsPM.Web.UserControls.InputSubject ucInputSubject = (RmsPM.Web.UserControls.InputSubject)item.FindControl("ucInputSubject");

                HtmlInputHidden txtBuildingCodeAll = (HtmlInputHidden)item.FindControl("txtBuildingCodeAll");
                HtmlInputHidden txtBuildingNameAll = (HtmlInputHidden)item.FindControl("txtBuildingNameAll");

                HtmlInputHidden txtPaymentItemCode = (HtmlInputHidden)item.FindControl("txtPaymentItemCode");
                HtmlInputHidden txtPaymentID = (HtmlInputHidden)item.FindControl("txtPaymentID");

                RmsPM.Web.UserControls.ExchangeRateControl ucItemCash = (RmsPM.Web.UserControls.ExchangeRateControl)item.FindControl("ucItemCash");

                string PayoutItemCode = txtPayoutItemCode.Value;

                /*
                if ( txtPayoutMoney.Value != "")
                {
                    if ( !Rms.Check.StringCheck.IsNumber(txtPayoutMoney.Value))
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "������ָ�ʽ����ȷ !"));
                        return null;
                    }
                }
                */

                DataRow dr = tb.NewRow();

                dr["Checked"] = BLL.ConvertRule.ToInt(txtSelect.Value);
                dr["PayoutItemCode"] = PayoutItemCode;
                dr["Summary"] = txtSummary.Value;
                dr["ItemMoney"] = BLL.ConvertRule.ToDecimalObj(txtItemMoney.Value);
                dr["TotalPayoutMoney"] = BLL.ConvertRule.ToDecimalObj(txtTotalPayoutMoney.Value);
                dr["RemainItemMoney"] = BLL.ConvertRule.ToDecimalObj(txtRemainItemMoney.Value);

                dr["PayoutCash"] = ucItemCash.Cash;
                dr["MoneyType"] = ucItemCash.MoneyType;
                dr["ExchangeRate"] = ucItemCash.ExchangeRate;

                dr["PayoutMoney"] = ucItemCash.Cash * ucItemCash.ExchangeRate;

//                dr["PayoutMoney"] = txtPayoutMoney.ValueDecimal;
//                dr["PayoutMoney"] = BLL.ConvertRule.ToDecimalObj(txtPayoutMoney.Value);

                dr["CostCode"] = txtCostCode.Value;
                dr["CostName"] = txtCostName.Value;
                dr["SubjectCode"] = ucInputSubject.Value;
                dr["SubjectName"] = ucInputSubject.Text;
                dr["SubjectHint"] = ucInputSubject.Hint;

                dr["BuildingCodeAll"] = txtBuildingCodeAll.Value;
                dr["BuildingNameAll"] = txtBuildingNameAll.Value;

                dr["PaymentItemCode"] = txtPaymentItemCode.Value;
                dr["PaymentID"] = txtPaymentID.Value;

                tb.Rows.Add(dr);
            }

            if (isBindGrid)
            {
                this.dgList.DataSource = tb;
                this.dgList.DataBind();
            }

            return tb;
        }

        private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.AlternatingItem:
                case ListItemType.Item:
                    RmsPM.Web.UserControls.ExchangeRateControl ucItemCash = (RmsPM.Web.UserControls.ExchangeRateControl)e.Item.FindControl("ucItemCash");

                    DataRowView ud_drvItem = (DataRowView)e.Item.DataItem;

                    if (ud_drvItem["MoneyType"] != DBNull.Value)
                    {
                        ucItemCash.Cash = BLL.ConvertRule.ToDecimal(ud_drvItem["PayoutCash"]);
                        ucItemCash.MoneyType = ud_drvItem["MoneyType"].ToString();
                        ucItemCash.ExchangeRate = BLL.ConvertRule.ToDecimal(ud_drvItem["ExchangeRate"]);
                    }

                    if (ud_drvItem["Checked"] != DBNull.Value && ud_drvItem["Checked"].ToString() == "0")
                    {
                        ucItemCash.IsAllowModify = false;
                    }
                    else
                    {
                        ucItemCash.IsAllowModify = true;
                    }

                    ucItemCash.BindControl();

                    break;
                case ListItemType.Footer:
                    Label lblSumPayoutMoney = (Label)e.Item.FindControl("lblSumPayoutMoney");
                    lblSumPayoutMoney.Text = this.txtSumPayoutMoney.Value;
                    break;
            }
        }

        /// <summary>
        /// ɾ����ϸ
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dgList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                string code = this.dgList.DataKeys[e.Item.ItemIndex].ToString();

                DataTable tb = ScreenToTable(false);
                if (tb == null) return;

                DataRow[] drs = tb.Select("PayoutItemCode='" + code + "'");
                if (drs.Length > 0)
                {
                    tb.Rows.Remove(drs[0]);
                }

                BindDataGrid(tb);
            }
            catch (Exception ex)
            {
                Response.Write(JavaScript.Alert(true, "ɾ����ϸʧ�ܣ�" + ex.Message));
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }

        private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    RmsPM.Web.UserControls.InputSubject ucInputSubject = (RmsPM.Web.UserControls.InputSubject)e.Item.FindControl("ucInputSubject");
                    
                    ucInputSubject.ProjectCode = this.txtProjectCode.Value;

                    break;
            }
        }

    }
}
