namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class SupplierImport : ImportRule.ImportBase
    {
        public DataRow ImportSupplierSingle(string val, EntityData entity, ref string hint, ref string SupplierName, ref string SupplierTypeFullName, bool isTest, DataTable tbAllSystemGroup)
        {
            hint = "";
            DataRow row = null;
            try
            {
                int index;
                DataRow row2;
                string text3;
                DataColumn column;
                string text5;
                string[] arr = ImportRule.SplitCsvLine(val);
                if (arr.Length < base.MinColCount)
                {
                    hint = "列不足3个";
                    return null;
                }
                SupplierName = arr[0].Trim();
                SupplierTypeFullName = "";
                if (SupplierName == "")
                {
                    hint = "厂商名称为空";
                    return null;
                }
                string text = arr[2].Trim();
                string text2 = "";
                if (text != "")
                {
                    DataRow[] rowArray = tbAllSystemGroup.Select("FullName = '" + text + "'");
                    if (rowArray.Length > 0)
                    {
                        text2 = rowArray[0]["GroupCode"].ToString();
                        SupplierTypeFullName = text;
                    }
                }
                if (text2 == "")
                {
                    hint = string.Format("未知的厂商类型“{0}”", text);
                    return null;
                }
                int count = base.tbDefine.Rows.Count;
                for (index = 0; index < count; index++)
                {
                    row2 = base.tbDefine.Rows[index];
                    text3 = row2["FieldName"].ToString();
                    string text4 = row2["FieldDesc"].ToString();
                    column = entity.CurrentTable.Columns[text3];
                    if ((text3 != "") && (text3 != "SupplierTypeCode"))
                    {
                        text5 = ConvertRule.ToString(ConvertRule.GetArrayItem(arr, index)).Trim();
                        if ((column.DataType == typeof(string)) && (text5.Length > column.MaxLength))
                        {
                            hint = string.Format("{0}（{1}）长度超过{2}位（现为{3}位）", new object[] { text4, text5, column.MaxLength, text5.Length });
                            return null;
                        }
                    }
                }
                DataRow[] rowArray2 = entity.CurrentTable.Select("SupplierName='" + SupplierName + "' and SupplierTypeCode = '" + text2 + "'");
                if (rowArray2.Length > 0)
                {
                    row = rowArray2[0];
                }
                else
                {
                    row = entity.CurrentTable.NewRow();
                    if (isTest)
                    {
                        row["SupplierCode"] = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        row["SupplierCode"] = SystemManageDAO.GetNewSysCode("SupplierCode").ToString();
                    }
                    entity.CurrentTable.Rows.Add(row);
                }
                count = base.tbDefine.Rows.Count;
                for (index = 0; index < count; index++)
                {
                    row2 = base.tbDefine.Rows[index];
                    text3 = row2["FieldName"].ToString();
                    column = entity.CurrentTable.Columns[text3];
                    if (text3 != "")
                    {
                        string text6 = text3;
                        if ((text6 != null) && (text6 == "SupplierTypeCode"))
                        {
                            row[text3] = text2;
                        }
                        else
                        {
                            text5 = ConvertRule.ToString(ConvertRule.GetArrayItem(arr, index)).Trim();
                            if (text5 == "")
                            {
                                try
                                {
                                    row[text3] = text5;
                                }
                                catch
                                {
                                    row[text3] = DBNull.Value;
                                }
                            }
                            else
                            {
                                row[text3] = text5;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                hint = "异常出错：" + exception.Message;
            }
            return row;
        }

        protected override void InitDefine()
        {
            base.MinColCount = 3;
            base.tbDefine.Rows.Add(new object[] { "SupplierName", "厂商名称", true });
            base.tbDefine.Rows.Add(new object[] { "Abbreviation", "厂商简称" });
            base.tbDefine.Rows.Add(new object[] { "SupplierTypeCode", "类型全名", true });
            base.tbDefine.Rows.Add(new object[] { "Structure", "企业性质" });
            base.tbDefine.Rows.Add(new object[] { "Quality", "资质" });
            base.tbDefine.Rows.Add(new object[] { "AreaCode", "地区" });
            base.tbDefine.Rows.Add(new object[] { "RegisteredAddress", "地址" });
            base.tbDefine.Rows.Add(new object[] { "ContractPerson", "联系人" });
            base.tbDefine.Rows.Add(new object[] { "OfficePhone", "电话" });
            base.tbDefine.Rows.Add(new object[] { "Mobile", "手机" });
            base.tbDefine.Rows.Add(new object[] { "Fax", "传真" });
            base.tbDefine.Rows.Add(new object[] { "Email", "E-Mail" });
            base.tbDefine.Rows.Add(new object[] { "WebAddress", "网址" });
            base.tbDefine.Rows.Add(new object[] { "Achievement", "业绩" });
            base.tbDefine.Rows.Add(new object[] { "RegisteredCapital", "注册资金" });
            base.tbDefine.Rows.Add(new object[] { "WorkScope", "经营范围" });
            base.tbDefine.Rows.Add(new object[] { "CheckOpinion", "评价意见" });
            base.tbDefine.Rows.Add(new object[] { "CreditLevel", "信用等级" });
            base.tbDefine.Rows.Add(new object[] { "Remark", "备注" });
        }
    }
}

