namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Web;
    using Rms.Interface.Sun;
    using Rms.Interface.UFSoft;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class VoucherRule
    {
        public static DataTable BuildVoucherDetailTableFromPayment(string codes, string ProjectCode, ref int ReceiptCount, string PMName)
        {
            EntityData data = new EntityData("VoucherDetail");
            DataTable dt = data.CurrentTable;
            BuildVoucherDetailTableFromPayment(dt, codes, ProjectCode, ref ReceiptCount, PMName);
            return dt;
        }

        public static DataTable BuildVoucherDetailTableFromPayment(DataTable dt, string codes, string ProjectCode, ref int ReceiptCount, string PMName)
        {
            ReceiptCount = 0;
            string[] textArray = codes.Split(",".ToCharArray());
            decimal num = 0M;
            string code = ProjectRule.GetSubjectSetCodeByProject(ProjectCode);
            string text2 = "";
            string text3 = "";
            EntityData subjectSetByCode = SubjectDAO.GetSubjectSetByCode(code);
            if (subjectSetByCode.HasRecord())
            {
                text2 = subjectSetByCode.GetString("FinanceInterfaceUnit");
                text3 = subjectSetByCode.GetString("FinanceInterfaceUser");
            }
            subjectSetByCode.Dispose();
            for (int i = 0; i < textArray.Length; i++)
            {
                string text4 = textArray[i];
                EntityData data2 = PaymentDAO.GetStandard_PayoutByCode(text4);
                if (data2.HasRecord())
                {
                    DataRow row;
                    string text9;
                    string text5 = "";
                    ReceiptCount += data2.GetInt("ReceiptCount");
                    num = 0M;
                    DataRow row2 = data2.CurrentTable.Rows[0];
                    DataTable table = data2.Tables["PayoutItem"];
                    foreach (DataRow row3 in table.Rows)
                    {
                        decimal num3 = ConvertRule.ToDecimal(row3["PayoutMoney"]);
                        if (num3 == 0M)
                        {
                            goto Label_045D;
                        }
                        string text6 = "";
                        string supplierAbbreviation = "";
                        num += num3;
                        row = dt.NewRow();
                        row["VoucherDetailCode"] = SystemManageDAO.GetNewSysCode("VoucherDetailCode");
                        row["SubjectCode"] = ConvertRule.ToString(row3["SubjectCode"]);
                        row["DebitMoney"] = num3;
                        row["CrebitMoney"] = 0;
                        row["RelaType"] = "付款";
                        row["RelaCode"] = text4;
                        row["CustCode"] = ProjectRule.GetSupplierCodeByName(ConvertRule.ToString(row2["Payer"]));
                        row["BillNo"] = ConvertRule.ToString(row2["BillNo"]);
                        row["CustCode"] = data2.GetString("SupplyCode");
                        EntityData data3 = PaymentDAO.GetV_PaymentItemByCode(ConvertRule.ToString(row3["PaymentItemCode"]));
                        if (!data3.HasRecord())
                        {
                            goto Label_0370;
                        }
                        switch (text2)
                        {
                            case "PaymentApply":
                                row["UFUnitCode"] = data3.GetString("UnitCode");
                                break;

                            case "PaymentApply,User":
                                row["UFUnitCode"] = data3.GetString("UnitCode");
                                goto Label_02A7;
                        }
                    Label_02A7:
                        text9 = text3;
                        if (text9 != null)
                        {
                            if (text9 == "PaymentApply")
                            {
                                row["PaymentCheckPerson"] = data3.GetString("ApplyPerson");
                            }
                            else if (text9 == "PaymentCheck")
                            {
                                goto Label_02E7;
                            }
                        }
                        goto Label_0301;
                    Label_02E7:
                        row["PaymentCheckPerson"] = data3.GetString("CheckPerson");
                    Label_0301:
                        row["ContractCode"] = data3.GetString("ContractCode");
                        row["ContractID"] = data3.GetString("ContractID");
                        text6 = data3.GetString("ContractName");
                        row["PBSType"] = data3.GetString("PBSType");
                        row["PBSCode"] = data3.GetString("PBSCode");
                    Label_0370:
                        data3.Dispose();
                        supplierAbbreviation = "";
                        text9 = PMName.ToLower();
                        if ((text9 != null) && (text9 == "xinchangningpm"))
                        {
                            supplierAbbreviation = SupplierRule.GetSupplierAbbreviation(data2.GetString("SupplyCode"));
                            if (text6 != "")
                            {
                                if (supplierAbbreviation != "")
                                {
                                    supplierAbbreviation = supplierAbbreviation + " ";
                                }
                                supplierAbbreviation = supplierAbbreviation + text6;
                            }
                            if (supplierAbbreviation.Trim() == "")
                            {
                                supplierAbbreviation = "付款";
                            }
                        }
                        else
                        {
                            supplierAbbreviation = "付款";
                        }
                        row["Summary"] = supplierAbbreviation;
                        if (text5.Trim() == "")
                        {
                            text5 = supplierAbbreviation;
                        }
                        dt.Rows.Add(row);
                    Label_045D:;
                    }
                    row = dt.NewRow();
                    row["VoucherDetailCode"] = SystemManageDAO.GetNewSysCode("VoucherDetailCode");
                    row["SubjectCode"] = ConvertRule.ToString(row2["SubjectCode"]);
                    row["DebitMoney"] = 0;
                    row["CrebitMoney"] = num;
                    text9 = PMName.ToLower();
                    if ((text9 != null) && (text9 == "xinchangningpm"))
                    {
                        if (text5.Trim() == "")
                        {
                            row["Summary"] = "付款";
                        }
                        else
                        {
                            row["Summary"] = text5;
                        }
                    }
                    else
                    {
                        row["Summary"] = "付款";
                    }
                    row["RelaType"] = "付款";
                    row["RelaCode"] = text4;
                    dt.Rows.Add(row);
                }
                data2.Dispose();
            }
            return dt;
        }

        public static void CheckVoucher(string VoucherCode, string User)
        {
            try
            {
                EntityData entity = PaymentDAO.GetVoucherByCode(VoucherCode);
                if (entity.HasRecord())
                {
                    DataRow currentRow = entity.CurrentRow;
                    currentRow["Status"] = 1;
                    currentRow["CheckDate"] = DateTime.Now;
                    currentRow["CheckPerson"] = User;
                    PaymentDAO.UpdateVoucher(entity);
                }
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable CheckVoucherFile(string codes)
        {
            DataTable table3;
            try
            {
                string[] textArray = codes.Split(",".ToCharArray());
                DataTable tbResult = new DataTable();
                tbResult.Columns.Add("Title", typeof(string));
                tbResult.Columns.Add("Desc", typeof(string));
                tbResult.Columns.Add("ErrLevel", typeof(string));
                foreach (string text in textArray)
                {
                    EntityData voucherByCode = PaymentDAO.GetVoucherByCode(text);
                    if (voucherByCode.HasRecord())
                    {
                        string desc;
                        string text2 = voucherByCode.GetString("VoucherID");
                        string projectCode = voucherByCode.GetString("ProjectCode");
                        string subjectSetCode = voucherByCode.GetString("SubjectSetCode");
                        int @int = voucherByCode.GetInt("Status");
                        string financeInterfaceSupplierCode = FinanceRule.GetFinanceInterfaceSupplierCode(subjectSetCode);
                        if (@int == 0)
                        {
                            desc = string.Format("凭证“{0}”未审核，不能导出", text2);
                            CheckVoucherFileNewRow(tbResult, desc, 1);
                        }
                        EntityData data2 = PaymentDAO.GetV_VoucherDetailByVoucherCode(text);
                        DataTable currentTable = data2.CurrentTable;
                        foreach (DataRow row in currentTable.Rows)
                        {
                            if ((ConvertRule.ToString(row["SupplyCode"]) != "") && (ConvertRule.ToString(row["SupplyU8Code"]) == ""))
                            {
                                desc = string.Format("凭证“{0}”中供应商“{1}”的财务编码为空", text2, ConvertRule.ToString(row["SupplyName"]));
                                CheckVoucherFileNewRow(tbResult, desc, 0);
                            }
                            if ((ConvertRule.ToString(row["CustCode"]) != "") && (FinanceRule.GetSupplierSubjectSetU8Code(ConvertRule.ToString(row["CustCode"]), projectCode, subjectSetCode, financeInterfaceSupplierCode) == ""))
                            {
                                desc = string.Format("凭证“{0}”中客户“{1}”的财务编码为空", text2, ConvertRule.ToString(row["CustName"]));
                                CheckVoucherFileNewRow(tbResult, desc, 0);
                            }
                            if ((ConvertRule.ToString(row["UFUnitCode"]) != "") && (FinanceRule.GetUFUnitSubjectSetU8Code(ConvertRule.ToString(row["UFUnitCode"]), subjectSetCode) == ""))
                            {
                                desc = string.Format("凭证“{0}”中部门“{1}”的财务编码为空", text2, ConvertRule.ToString(row["UFUnitName"]));
                                CheckVoucherFileNewRow(tbResult, desc, 0);
                            }
                            if ((ConvertRule.ToString(row["PaymentCheckPerson"]) != "") && (FinanceRule.GetSystemUserSubjectSetU8Code(ConvertRule.ToString(row["PaymentCheckPerson"]), subjectSetCode) == ""))
                            {
                                desc = string.Format("凭证“{0}”中人员“{1}”的财务编码为空", text2, ConvertRule.ToString(row["PaymentCheckPersonName"]));
                                CheckVoucherFileNewRow(tbResult, desc, 0);
                            }
                            if ((ConvertRule.ToString(row["PBSCode"]) != "") && (FinanceRule.GetBuildingSubjectSetU8Code(ConvertRule.ToString(row["PBSCode"]), subjectSetCode) == ""))
                            {
                                desc = string.Format("凭证“{0}”中单位工程“{1}”的财务编码为空", text2, ConvertRule.ToString(row["PBSName"]));
                                CheckVoucherFileNewRow(tbResult, desc, 0);
                            }
                            if ((ConvertRule.ToString(row["PBSType"]) == "P") && (FinanceRule.GetProjectSubjectSetU8Code(projectCode, subjectSetCode) == ""))
                            {
                                desc = string.Format("凭证“{0}”中单位工程-项目“{1}”的财务编码为空", text2, ConvertRule.ToString(row["ProjectName"]));
                                CheckVoucherFileNewRow(tbResult, desc, 0);
                            }
                            if ((ConvertRule.ToString(row["UFProjectCode"]) != "") && (ConvertRule.ToString(row["UFProjectU8Code"]) == ""))
                            {
                                desc = string.Format("凭证“{0}”中项目“{1}”的财务编码为空", text2, ConvertRule.ToString(row["UFProjectName"]));
                                CheckVoucherFileNewRow(tbResult, desc, 0);
                            }
                        }
                        data2.Dispose();
                    }
                    voucherByCode.Dispose();
                }
                table3 = tbResult;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table3;
        }

        private static void CheckVoucherFileNewRow(DataTable tbResult, string desc, int ErrLevel)
        {
            try
            {
                if (tbResult.Select("desc='" + desc + "'").Length == 0)
                {
                    DataRow row = tbResult.NewRow();
                    row["ErrLevel"] = ErrLevel;
                    row["Desc"] = desc;
                    tbResult.Rows.Add(row);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable CreateVoucherCheckResultTable()
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("Title", typeof(string));
                table.Columns.Add("Desc", typeof(string));
                table.Columns.Add("ErrLevel", typeof(string));
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static DataTable GetVoucherCheckResult(string codes)
        {
            DataTable table3;
            try
            {
                string[] textArray = codes.Split(",".ToCharArray());
                DataTable tbResult = CreateVoucherCheckResultTable();
                foreach (string text in textArray)
                {
                    EntityData voucherByCode = PaymentDAO.GetVoucherByCode(text);
                    if (voucherByCode.HasRecord())
                    {
                        string subjectSetCode = voucherByCode.GetString("SubjectSetCode");
                        EntityData data2 = PaymentDAO.GetV_VoucherDetailByVoucherCode(text);
                        DataTable tbDtl = data2.CurrentTable;
                        GetVoucherDetailCheckResult(tbResult, tbDtl, subjectSetCode, true);
                        data2.Dispose();
                    }
                    voucherByCode.Dispose();
                }
                table3 = tbResult;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table3;
        }

        private static void GetVoucherCheckResultNewRow(DataTable tbResult, string desc)
        {
            try
            {
                if (tbResult.Select("desc='" + desc + "'").Length == 0)
                {
                    DataRow row = tbResult.NewRow();
                    row["ErrLevel"] = 1;
                    row["Desc"] = desc;
                    tbResult.Rows.Add(row);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void GetVoucherDetailCheckResult(DataTable tbResult, DataTable tbDtl, string SubjectSetCode, bool isCheckAll)
        {
            try
            {
                string desc = "";
                foreach (DataRow row in tbDtl.Rows)
                {
                    string subjectCode = ConvertRule.ToString(row["SubjectCode"]);
                    if (subjectCode == "")
                    {
                        desc = "科目编号不能为空";
                        GetVoucherCheckResultNewRow(tbResult, desc);
                        if (!isCheckAll)
                        {
                            return;
                        }
                    }
                    else
                    {
                        desc = SubjectRule.CheckSubject(subjectCode, SubjectSetCode, string.Format("科目编号“{0}”", subjectCode));
                        if (desc != "")
                        {
                            GetVoucherCheckResultNewRow(tbResult, desc);
                            if (!isCheckAll)
                            {
                                return;
                            }
                        }
                    }
                    if (ConvertRule.ToString(row["Summary"]) == "")
                    {
                        desc = "摘要不能为空";
                        GetVoucherCheckResultNewRow(tbResult, desc);
                        if (!isCheckAll)
                        {
                            return;
                        }
                    }
                    if ((ConvertRule.ToDecimal(row["DebitMoney"]) == 0M) && (ConvertRule.ToDecimal(row["CrebitMoney"]) == 0M))
                    {
                        desc = "借贷金额不能都为0";
                        GetVoucherCheckResultNewRow(tbResult, desc);
                        if (!isCheckAll)
                        {
                            return;
                        }
                    }
                    if ((ConvertRule.ToDecimal(row["DebitMoney"]) != 0M) && (ConvertRule.ToDecimal(row["CrebitMoney"]) != 0M))
                    {
                        desc = "借贷金额只能有一项 !";
                        GetVoucherCheckResultNewRow(tbResult, desc);
                        if (!isCheckAll)
                        {
                            return;
                        }
                    }
                }
                string[] arrColumnName = new string[] { "DebitMoney", "CrebitMoney" };
                decimal[] numArray = MathRule.SumColumn(tbDtl, arrColumnName);
                if (numArray[0] != numArray[1])
                {
                    desc = "借贷不平衡，请检查 !";
                    GetVoucherCheckResultNewRow(tbResult, desc);
                    if (!isCheckAll)
                    {
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static string GetVoucherDetailCodeBySalPay(string code)
        {
            string text2;
            try
            {
                string text = "";
                EntityData voucherDetailByRelaCode = PaymentDAO.GetVoucherDetailByRelaCode("销售收入", code);
                if (voucherDetailByRelaCode.HasRecord())
                {
                    text = voucherDetailByRelaCode.GetString("VoucherDetailCode");
                }
                voucherDetailByRelaCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        private static string GetVoucherPath(HttpServerUtility Server)
        {
            return "../Temp/";
        }

        public static string GetVoucherTypeName(string code)
        {
            string text2;
            try
            {
                string text = "";
                EntityData voucherTypeByCode = PaymentDAO.GetVoucherTypeByCode(code);
                if (voucherTypeByCode.HasRecord())
                {
                    text = voucherTypeByCode.GetString("Name");
                }
                voucherTypeByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string MakeVoucherFile(string codes, HttpServerUtility Server, string SubjectSetCode)
        {
            string text3;
            try
            {
                string text = "";
                string text2 = "";
                EntityData entitySubjectSet = SubjectDAO.GetSubjectSetByCode(SubjectSetCode);
                if (entitySubjectSet.HasRecord())
                {
                    text2 = entitySubjectSet.GetString("FinanceInterface");
                }
                switch (text2)
                {
                    case "UFSoft":
                        text = OutputVoucherFileUFSoft(codes, Server, entitySubjectSet);
                        break;

                    case "UFSoft_V7":
                        text = OutputVoucherFileUFSoftV7(codes, Server, entitySubjectSet);
                        break;

                    case "Sun":
                        text = OutputVoucherFileSun(codes, Server, entitySubjectSet);
                        break;

                    default:
                        throw new Exception("未定义财务系统接口");
                }
                entitySubjectSet.Dispose();
                if (text != "")
                {
                    UpdateVoucherExportFlag(codes);
                }
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        private static string OutputVoucherFileSun(string codes, HttpServerUtility Server, EntityData entitySubjectSet)
        {
            string[] textArray = codes.Split(",".ToCharArray());
            string financeInterfaceSupplierCode = "";
            if (entitySubjectSet.HasRecord())
            {
                financeInterfaceSupplierCode = entitySubjectSet.GetString("FinanceInterfaceSupplierCode");
            }
            SunVoucher voucher = new SunVoucher();
            for (int i = 0; i < textArray.Length; i++)
            {
                string code = textArray[i];
                EntityData entity = PaymentDAO.GetVoucherByCode(code);
                try
                {
                    if (entity.CurrentTable.Rows.Count > 0)
                    {
                        DateTime today;
                        foreach (DataRow row in entity.CurrentTable.Rows)
                        {
                            row["OutPutDate"] = DateTime.Now;
                        }
                        string s = entity.GetDateTimeOnlyDate("MakeDate");
                        if ((s != null) && (s != ""))
                        {
                            today = DateTime.Parse(s);
                        }
                        else
                        {
                            today = DateTime.Today;
                        }
                        string text4 = entity.GetString("VoucherType");
                        string text5 = entity.GetString("VoucherCode");
                        string text6 = entity.GetString("VoucherID");
                        string projectCode = entity.GetString("ProjectCode");
                        string subjectSetCode = entity.GetString("SubjectSetCode");
                        EntityData data2 = PaymentDAO.GetV_VoucherDetailByVoucherCode(code);
                        for (int j = 0; j < data2.CurrentTable.Rows.Count; j++)
                        {
                            data2.SetCurrentRow(j);
                            SunVoucherItem item = new SunVoucherItem();
                            item.SubjectCode = data2.GetString("SubjectCode");
                            item.VoucherDate = today;
                            if (data2.GetDecimal("DebitMoney") != 0M)
                            {
                                item.Money = data2.GetDecimal("DebitMoney");
                                item.DebitType = SunVoucherItem.c_DebitType.Debit;
                            }
                            else
                            {
                                item.Money = data2.GetDecimal("CrebitMoney");
                                item.DebitType = SunVoucherItem.c_DebitType.Crebit;
                            }
                            item.BillNo = data2.GetString("BillNo");
                            item.Description = data2.GetString("Summary");
                            item.AnalysisCode3 = data2.GetString("ContractID").Replace("-", "");
                            item.AnalysisCode5 = FinanceRule.GetSupplierSubjectSetU8Code(data2.GetString("CustCode"), projectCode, subjectSetCode, financeInterfaceSupplierCode);
                            if (data2.GetString("PBSType") == "P")
                            {
                                item.AnalysisCode6 = FinanceRule.GetProjectSubjectSetU8Code(projectCode, subjectSetCode);
                            }
                            else
                            {
                                item.AnalysisCode6 = FinanceRule.GetBuildingSubjectSetU8Code(data2.GetString("PBSCode"), subjectSetCode);
                            }
                            item.AnalysisCode8 = FinanceRule.GetSystemUserSubjectSetU8Code(data2.GetString("PaymentCheckPerson"), subjectSetCode);
                            voucher.Items.Add(item);
                        }
                        data2.Dispose();
                    }
                }
                finally
                {
                    PaymentDAO.UpdateVoucher(entity);
                    entity.Dispose();
                }
            }
            string path = GetVoucherPath(Server);
            string text10 = Server.MapPath(path);
            string text11 = textArray[0];
            text11 = text11 + ".txt";
            string text12 = path + text11;
            string text13 = text10 + text11;
            DirectoryInfo info = new DirectoryInfo(text10);
            if (!info.Exists)
            {
                info.Create();
            }
            voucher.SaveAs(text13);
            return text12;
        }

        private static string OutputVoucherFileUFSoft(string codes, HttpServerUtility Server, EntityData entitySubjectSet)
        {
            string[] textArray = codes.Split(",".ToCharArray());
            U8_Voucher voucher = new U8_Voucher(VoucherType.未引入过文本, "V800");
            string text = "";
            string text2 = "";
            string financeInterfaceSupplierCode = "";
            if (entitySubjectSet.HasRecord())
            {
                text = entitySubjectSet.GetString("FinanceInterfaceUnit");
                text2 = entitySubjectSet.GetString("FinanceInterfaceExportName");
                financeInterfaceSupplierCode = entitySubjectSet.GetString("FinanceInterfaceSupplierCode");
            }
            for (int i = 0; i < textArray.Length; i++)
            {
                string code = textArray[i];
                EntityData entity = PaymentDAO.GetVoucherByCode(code);
                try
                {
                    if (entity.CurrentTable.Rows.Count > 0)
                    {
                        DateTime today;
                        foreach (DataRow row in entity.CurrentTable.Rows)
                        {
                            row["OutPutDate"] = DateTime.Now;
                        }
                        string s = entity.GetDateTimeOnlyDate("MakeDate");
                        if ((s != null) && (s != ""))
                        {
                            today = DateTime.Parse(s);
                        }
                        else
                        {
                            today = DateTime.Today;
                        }
                        string text6 = entity.GetString("VoucherType");
                        string voucherTypeName = GetVoucherTypeName(text6);
                        string text8 = entity.GetString("VoucherCode");
                        string text9 = entity.GetString("VoucherID");
                        string projectCode = entity.GetString("ProjectCode");
                        string subjectSetCode = entity.GetString("SubjectSetCode");
                        EntityData data2 = PaymentDAO.GetV_VoucherDetailByVoucherCode(code);
                        for (int j = 0; j < data2.CurrentTable.Rows.Count; j++)
                        {
                            data2.SetCurrentRow(j);
                            U8_VoucherItem item = new U8_VoucherItem();
                            item.Field01 = s;
                            string text18 = text2;
                            if ((text18 != null) && (text18 == "Name"))
                            {
                                item.Field02 = voucherTypeName;
                            }
                            else
                            {
                                item.Field02 = text6;
                            }
                            item.Field03 = text9;
                            item.Field04 = "0";
                            item.Field05 = data2.GetString("Summary");
                            item.Field06 = data2.GetString("SubjectCode");
                            item.Field07 = data2.GetDecimal("DebitMoney").ToString();
                            item.Field08 = data2.GetDecimal("CrebitMoney").ToString();
                            item.Field09 = "0";
                            item.Field10 = "0";
                            item.Field11 = "0";
                            item.Field14 = data2.GetString("BillNo").ToString();
                            if (text.IndexOf(",User") >= 0)
                            {
                                if (data2.GetString("PaymentCheckPerson") != "")
                                {
                                    item.Field16 = FinanceRule.GetSystemUserSubjectSetU8Code(data2.GetString("PaymentCheckPerson"), subjectSetCode);
                                }
                                else
                                {
                                    item.Field16 = FinanceRule.GetUFUnitSubjectSetU8Code(data2.GetString("UFUnitCode"), subjectSetCode);
                                }
                            }
                            else
                            {
                                item.Field16 = FinanceRule.GetUFUnitSubjectSetU8Code(data2.GetString("UFUnitCode"), subjectSetCode);
                                item.Field17 = FinanceRule.GetSystemUserSubjectSetU8Code(data2.GetString("PaymentCheckPerson"), subjectSetCode);
                            }
                            item.Field18 = FinanceRule.GetSupplierSubjectSetU8Code(data2.GetString("CustCode"), projectCode, subjectSetCode, financeInterfaceSupplierCode);
                            item.Field19 = data2.GetString("SupplyU8Code").ToString();
                            item.Field21 = data2.GetString("UFProjectU8Code");
                            item.Field22 = text9;
                            voucher.Items.Add(item);
                        }
                        data2.Dispose();
                    }
                }
                finally
                {
                    PaymentDAO.UpdateVoucher(entity);
                    entity.Dispose();
                }
            }
            string path = GetVoucherPath(Server);
            string text13 = Server.MapPath(path);
            string text14 = textArray[0];
            text14 = text14 + ".txt";
            string text15 = path + text14;
            string text16 = text13 + text14;
            DirectoryInfo info = new DirectoryInfo(text13);
            if (!info.Exists)
            {
                info.Create();
            }
            voucher.SaveAs(text16);
            return text15;
        }

        private static string OutputVoucherFileUFSoftV7(string codes, HttpServerUtility Server, EntityData entitySubjectSet)
        {
            string[] textArray = codes.Split(",".ToCharArray());
            string text = "";
            string text2 = "";
            string financeInterfaceSupplierCode = "";
            if (entitySubjectSet.HasRecord())
            {
                text = entitySubjectSet.GetString("FinanceInterfaceUnit");
                text2 = entitySubjectSet.GetString("FinanceInterfaceExportName");
                financeInterfaceSupplierCode = entitySubjectSet.GetString("FinanceInterfaceSupplierCode");
            }
            string path = GetVoucherPath(Server);
            string text5 = Server.MapPath(path);
            string text6 = "凭证导出_" + textArray[0] + ".mdb";
            string text7 = path + text6;
            string text8 = text5 + text6;
            DirectoryInfo info = new DirectoryInfo(text5);
            if (!info.Exists)
            {
                info.Create();
            }
            FileStream stream = File.OpenRead(Server.MapPath("../Template/") + "凭证导出UF.mdb");
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, (int) stream.Length);
            File.WriteAllBytes(text8, buffer);
            OleDbConnection selectConnection = new OleDbConnection(string.Format("Provider=Microsoft.Jet.OleDb.4.0;Data Source={0}", text8));
            try
            {
                selectConnection.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("select * from GL_accvouch", selectConnection);
                DataSet dataSet = new DataSet();
                da.Fill(dataSet);
                DataTable tb = dataSet.Tables[0];
                for (int i = 0; i < textArray.Length; i++)
                {
                    string code = textArray[i];
                    EntityData entity = PaymentDAO.GetVoucherByCode(code);
                    try
                    {
                        if (entity.CurrentTable.Rows.Count > 0)
                        {
                            DateTime today;
                            foreach (DataRow row in entity.CurrentTable.Rows)
                            {
                                row["OutPutDate"] = DateTime.Now;
                            }
                            string s = entity.GetDateTimeOnlyDate("MakeDate");
                            if ((s != null) && (s != ""))
                            {
                                today = DateTime.Parse(s);
                            }
                            else
                            {
                                today = DateTime.Today;
                            }
                            string text13 = entity.GetString("VoucherType");
                            string voucherTypeName = GetVoucherTypeName(text13);
                            string text15 = entity.GetString("VoucherCode");
                            string text16 = entity.GetString("VoucherID");
                            string projectCode = entity.GetString("ProjectCode");
                            string subjectSetCode = entity.GetString("SubjectSetCode");
                            EntityData data2 = PaymentDAO.GetV_VoucherDetailByVoucherCode(code);
                            for (int j = 0; j < data2.CurrentTable.Rows.Count; j++)
                            {
                                data2.SetCurrentRow(j);
                                DataRow row2 = tb.NewRow();
                                row2["iperiod"] = today.Month;
                                row2["csign"] = text13;
                                row2["isignseq"] = ConvertRule.ToInt(text13);
                                row2["ino_id"] = text16;
                                row2["inid"] = j + 1;
                                row2["dbill_date"] = today.ToString("yyyy-MM-dd");
                                row2["idoc"] = entity.GetInt("ReceiptCount");
                                row2["cbill"] = SystemRule.GetUserName(entity.GetString("Accountant"));
                                row2["ccheck"] = SystemRule.GetUserName(entity.GetString("CheckPerson"));
                                row2["ibook"] = 0;
                                row2["cdigest"] = data2.GetString("Summary");
                                row2["ccode"] = data2.GetString("SubjectCode");
                                row2["md"] = data2.GetDecimal("DebitMoney").ToString();
                                row2["mc"] = data2.GetDecimal("CrebitMoney").ToString();
                                row2["nfrat"] = 0;
                                row2["nd_s"] = 0;
                                row2["nc_s"] = 0;
                                row2["ccode_equal"] = data2.GetString("SubjectCode");
                                row2["cn_id"] = text16;
                                row2["ccus_id"] = FinanceRule.GetSupplierSubjectSetU8Code(data2.GetString("CustCode"), projectCode, subjectSetCode, financeInterfaceSupplierCode);
                                row2["csup_id"] = data2.GetString("SupplyU8Code").ToString();
                                if (data2.GetString("PBSType") == "P")
                                {
                                    row2["citem_id"] = FinanceRule.GetProjectSubjectSetU8Code(projectCode, subjectSetCode);
                                }
                                else
                                {
                                    row2["citem_id"] = FinanceRule.GetBuildingSubjectSetU8Code(data2.GetString("PBSCode"), subjectSetCode);
                                }
                                if (ConvertRule.ToString(row2["citem_id"]) != "")
                                {
                                    row2["citem_class"] = "04";
                                }
                                row2["bdelete"] = 0;
                                row2["bvouchedit"] = -1;
                                row2["bvouchAddordele"] = -1;
                                row2["bvouchmoneyhold"] = 0;
                                row2["bvalueedit"] = -1;
                                row2["bcodeedit"] = -1;
                                row2["bPCSedit"] = -1;
                                row2["bDeptedit"] = -1;
                                row2["bItemedit"] = -1;
                                row2["bCusSupInput"] = -1;
                                row2["bvouchAddordele"] = 0;
                                row2["bcusSupInput"] = 0;
                                tb.Rows.Add(row2);
                            }
                            data2.Dispose();
                        }
                    }
                    finally
                    {
                        PaymentDAO.UpdateVoucher(entity);
                        entity.Dispose();
                    }
                }
                SubmitAccessVoucherFile(selectConnection, da, tb);
                da.Dispose();
            }
            finally
            {
                selectConnection.Close();
                selectConnection.Dispose();
            }
            return text7;
        }

        private static void SubmitAccessVoucherFile(OleDbConnection conn, OleDbDataAdapter da, DataTable tb)
        {
            try
            {
                string cmdText = "insert into GL_accvouch (iperiod,csign,isignseq,ino_id,inid,dbill_date,idoc,cbill,ccheck,cbook,ibook,ccashier,iflag,ctext1,ctext2,cdigest,ccode,cexch_name,md,mc,md_f,mc_f,nfrat,nd_s,nc_s,csettle,cn_id,dt_date,cdept_id,cperson_id,ccus_id,csup_id,citem_id,citem_class,cname,ccode_equal,iflagbank,iflagPerson,bdelete,coutaccset,ioutyear,coutsysname,coutsysver,doutbilldate,ioutperiod,coutsign,coutno_id,doutdate,coutbillsign,coutid,bvouchedit,bvouchAddordele,bvouchmoneyhold,bvalueedit,bcodeedit,ccodecontrol,bPCSedit,bDeptedit,bItemedit,bCusSupInput,cDefine1,cDefine2,cDefine3,cDefine4,cDefine5,cDefine6,cDefine7,cDefine8,cDefine9,cDefine10,cDefine11,cDefine12,cDefine13,cDefine14,cDefine15,cDefine16,dReceive,cWLDZFlag,dWLDZTime,bFlagOut) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                OleDbCommand command = new OleDbCommand(cmdText);
                command.Parameters.Add("@iperiod", OleDbType.Decimal, 0x13, "iperiod");
                command.Parameters.Add("@csign", OleDbType.VarChar, 8, "csign");
                command.Parameters.Add("@isignseq", OleDbType.Decimal, 0x13, "isignseq");
                command.Parameters.Add("@ino_id", OleDbType.Decimal, 0x13, "ino_id");
                command.Parameters.Add("@inid", OleDbType.Decimal, 0x13, "inid");
                command.Parameters.Add("@dbill_date", OleDbType.Date, 8, "dbill_date");
                command.Parameters.Add("@idoc", OleDbType.Integer, 2, "idoc");
                command.Parameters.Add("@cbill", OleDbType.VarChar, 20, "cbill");
                command.Parameters.Add("@ccheck", OleDbType.VarChar, 20, "ccheck");
                command.Parameters.Add("@cbook", OleDbType.VarChar, 20, "cbook");
                command.Parameters.Add("@ibook", OleDbType.Integer, 1, "ibook");
                command.Parameters.Add("@ccashier", OleDbType.VarChar, 20, "ccashier");
                command.Parameters.Add("@iflag", OleDbType.Integer, 1, "iflag");
                command.Parameters.Add("@ctext1", OleDbType.VarChar, 10, "ctext1");
                command.Parameters.Add("@ctext2", OleDbType.VarChar, 10, "ctext2");
                command.Parameters.Add("@cdigest", OleDbType.VarChar, 60, "cdigest");
                command.Parameters.Add("@ccode", OleDbType.VarChar, 15, "ccode");
                command.Parameters.Add("@cexch_name", OleDbType.VarChar, 8, "cexch_name");
                command.Parameters.Add("@md", OleDbType.Decimal, 8, "md");
                command.Parameters.Add("@mc", OleDbType.Decimal, 8, "mc");
                command.Parameters.Add("@md_f", OleDbType.Decimal, 8, "md_f");
                command.Parameters.Add("@mc_f", OleDbType.Decimal, 8, "mc_f");
                command.Parameters.Add("@nfrat", OleDbType.Decimal, 8, "nfrat");
                command.Parameters.Add("@nd_s", OleDbType.Decimal, 8, "nd_s");
                command.Parameters.Add("@nc_s", OleDbType.Decimal, 8, "nc_s");
                command.Parameters.Add("@csettle", OleDbType.VarChar, 3, "csettle");
                command.Parameters.Add("@cn_id", OleDbType.VarChar, 30, "cn_id");
                command.Parameters.Add("@dt_date", OleDbType.Date, 8, "dt_date");
                command.Parameters.Add("@cdept_id", OleDbType.VarChar, 12, "cdept_id");
                command.Parameters.Add("@cperson_id", OleDbType.VarChar, 8, "cperson_id");
                command.Parameters.Add("@ccus_id", OleDbType.VarChar, 20, "ccus_id");
                command.Parameters.Add("@csup_id", OleDbType.VarChar, 20, "csup_id");
                command.Parameters.Add("@citem_id", OleDbType.VarChar, 20, "citem_id");
                command.Parameters.Add("@citem_class", OleDbType.VarChar, 2, "citem_class");
                command.Parameters.Add("@cname", OleDbType.VarChar, 20, "cname");
                command.Parameters.Add("@ccode_equal", OleDbType.VarChar, 50, "ccode_equal");
                command.Parameters.Add("@iflagbank", OleDbType.Integer, 1, "iflagbank");
                command.Parameters.Add("@iflagPerson", OleDbType.Integer, 1, "iflagPerson");
                command.Parameters.Add("@bdelete", OleDbType.Integer, 1, "bdelete");
                command.Parameters.Add("@coutaccset", OleDbType.VarChar, 3, "coutaccset");
                command.Parameters.Add("@ioutyear", OleDbType.Integer, 2, "ioutyear");
                command.Parameters.Add("@coutsysname", OleDbType.VarChar, 10, "coutsysname");
                command.Parameters.Add("@coutsysver", OleDbType.VarChar, 10, "coutsysver");
                command.Parameters.Add("@doutbilldate", OleDbType.Date, 8, "doutbilldate");
                command.Parameters.Add("@ioutperiod", OleDbType.Integer, 1, "ioutperiod");
                command.Parameters.Add("@coutsign", OleDbType.VarChar, 20, "coutsign");
                command.Parameters.Add("@coutno_id", OleDbType.VarChar, 30, "coutno_id");
                command.Parameters.Add("@doutdate", OleDbType.Date, 8, "doutdate");
                command.Parameters.Add("@coutbillsign", OleDbType.VarChar, 20, "coutbillsign");
                command.Parameters.Add("@coutid", OleDbType.VarChar, 50, "coutid");
                command.Parameters.Add("@bvouchedit", OleDbType.Integer, 1, "bvouchedit");
                command.Parameters.Add("@bvouchAddordele", OleDbType.Integer, 1, "bvouchAddordele");
                command.Parameters.Add("@bvouchmoneyhold", OleDbType.Integer, 1, "bvouchmoneyhold");
                command.Parameters.Add("@bvalueedit", OleDbType.Integer, 1, "bvalueedit");
                command.Parameters.Add("@bcodeedit", OleDbType.Integer, 1, "bcodeedit");
                command.Parameters.Add("@ccodecontrol", OleDbType.VarChar, 50, "ccodecontrol");
                command.Parameters.Add("@bPCSedit", OleDbType.Integer, 1, "bPCSedit");
                command.Parameters.Add("@bDeptedit", OleDbType.Integer, 1, "bDeptedit");
                command.Parameters.Add("@bItemedit", OleDbType.Integer, 1, "bItemedit");
                command.Parameters.Add("@bCusSupInput", OleDbType.Integer, 1, "bCusSupInput");
                command.Parameters.Add("@cDefine1", OleDbType.VarChar, 20, "cDefine1");
                command.Parameters.Add("@cDefine2", OleDbType.VarChar, 20, "cDefine2");
                command.Parameters.Add("@cDefine3", OleDbType.VarChar, 20, "cDefine3");
                command.Parameters.Add("@cDefine4", OleDbType.Date, 8, "cDefine4");
                command.Parameters.Add("@cDefine5", OleDbType.Integer, 4, "cDefine5");
                command.Parameters.Add("@cDefine6", OleDbType.Date, 8, "cDefine6");
                command.Parameters.Add("@cDefine7", OleDbType.Decimal, 8, "cDefine7");
                command.Parameters.Add("@cDefine8", OleDbType.VarChar, 4, "cDefine8");
                command.Parameters.Add("@cDefine9", OleDbType.VarChar, 8, "cDefine9");
                command.Parameters.Add("@cDefine10", OleDbType.VarChar, 60, "cDefine10");
                command.Parameters.Add("@cDefine11", OleDbType.VarChar, 120, "cDefine11");
                command.Parameters.Add("@cDefine12", OleDbType.VarChar, 120, "cDefine12");
                command.Parameters.Add("@cDefine13", OleDbType.VarChar, 120, "cDefine13");
                command.Parameters.Add("@cDefine14", OleDbType.VarChar, 120, "cDefine14");
                command.Parameters.Add("@cDefine15", OleDbType.Integer, 4, "cDefine15");
                command.Parameters.Add("@cDefine16", OleDbType.Decimal, 8, "cDefine16");
                command.Parameters.Add("@dReceive", OleDbType.Date, 8, "dReceive");
                command.Parameters.Add("@cWLDZFlag", OleDbType.VarChar, 1, "cWLDZFlag");
                command.Parameters.Add("@dWLDZTime", OleDbType.Date, 8, "dWLDZTime");
                command.Parameters.Add("@bFlagOut", OleDbType.Integer, 1, "bFlagOut");
                da.InsertCommand = command;
                da.InsertCommand.Connection = conn;
                try
                {
                    da.Update(tb);
                }
                finally
                {
                    da.InsertCommand.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void UpdateVoucherExportFlag(string codes)
        {
            try
            {
                foreach (string text in codes.Split(",".ToCharArray()))
                {
                    EntityData entity = PaymentDAO.GetVoucherByCode(text);
                    try
                    {
                        if (entity.CurrentTable.Rows.Count > 0)
                        {
                            entity.CurrentRow["Status"] = 2;
                            entity.CurrentRow["IsExported"] = 1;
                            entity.CurrentRow["ExportDate"] = DateTime.Now;
                            PaymentDAO.UpdateVoucher(entity);
                        }
                    }
                    finally
                    {
                        entity.Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void VoucherDetailAddColumnPaymentCheckPersonName(DataTable dt)
        {
            try
            {
                if (!dt.Columns.Contains("PaymentCheckPersonName"))
                {
                    dt.Columns.Add("PaymentCheckPersonName", typeof(string));
                }
                foreach (DataRow row in dt.Rows)
                {
                    if (ConvertRule.ToString(row["PaymentCheckPersonName"]) == "")
                    {
                        string userCode = ConvertRule.ToString(row["PaymentCheckPerson"]);
                        row["PaymentCheckPersonName"] = SystemRule.GetUserName(userCode);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void VoucherDetailAddColumnPBSName(DataTable dt)
        {
            try
            {
                if (!dt.Columns.Contains("PBSName"))
                {
                    dt.Columns.Add("PBSName", typeof(string));
                }
                foreach (DataRow row in dt.Rows)
                {
                    if (ConvertRule.ToString(row["PBSName"]) == "")
                    {
                        string pBSType = ConvertRule.ToString(row["PBSType"]);
                        string pBSCode = ConvertRule.ToString(row["PBSCode"]);
                        row["PBSName"] = CostBudgetRule.GetPBSName(pBSType, pBSCode);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

