namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class SupplierMaterialImport : ImportRule.ImportBase
    {
        public DataRow ImportSupplierMaterialSingle(string val, EntityData entity, ref string hint, ref string SupplierName, ref string GroupFullName, bool isTest, DataTable tbAllSystemGroup)
        {
            Exception exception;
            hint = "";
            DataRow row = null;
            try
            {
                int index;
                DataRow row2;
                string text4;
                DataColumn column;
                string s;
                string[] arr = ImportRule.SplitCsvLine(val);
                if (arr.Length < base.MinColCount)
                {
                    hint = string.Format("列不足{0}个", base.MinColCount);
                    return null;
                }
                SupplierName = arr[1].Trim();
                GroupFullName = "";
                if (SupplierName == "")
                {
                    hint = "厂商为空";
                    return null;
                }
                string text = arr[0].Trim();
                string text2 = "";
                if (text != "")
                {
                    DataRow[] rowArray = tbAllSystemGroup.Select("FullName = '" + text + "'");
                    if (rowArray.Length > 0)
                    {
                        text2 = rowArray[0]["GroupCode"].ToString();
                        GroupFullName = text;
                    }
                }
                if (text2 == "")
                {
                    hint = string.Format("未知的类型“{0}”", text);
                    return null;
                }
                string supplierCodeByName = "";
                if (SupplierName != "")
                {
                    supplierCodeByName = ProjectRule.GetSupplierCodeByName(SupplierName);
                }
                if (supplierCodeByName == "")
                {
                    hint = string.Format("厂商信息中找不到厂商“{0}”", SupplierName);
                    return null;
                }
                int count = base.tbDefine.Rows.Count;
                for (index = 0; index < count; index++)
                {
                    row2 = base.tbDefine.Rows[index];
                    text4 = row2["FieldName"].ToString();
                    string text5 = row2["FieldDesc"].ToString();
                    column = entity.CurrentTable.Columns[text4];
                    if ((text4 != "") && (text4 != "GroupCode"))
                    {
                        s = ConvertRule.ToString(ConvertRule.GetArrayItem(arr, index)).Trim();
                        if ((column.DataType == typeof(string)) && (StringRule.LenB(s) > column.MaxLength))
                        {
                            hint = string.Format("{0}（{1}）长度超过{2}位（现为{3}位）", new object[] { text5, s, column.MaxLength, s.Length });
                            return null;
                        }
                    }
                }
                row = entity.CurrentTable.NewRow();
                try
                {
                    if (isTest)
                    {
                        row["SupplierMaterialCode"] = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        row["SupplierMaterialCode"] = SystemManageDAO.GetNewSysCode("SupplierMaterialCode").ToString();
                    }
                    count = base.tbDefine.Rows.Count;
                    for (index = 0; index < count; index++)
                    {
                        row2 = base.tbDefine.Rows[index];
                        text4 = row2["FieldName"].ToString();
                        column = entity.CurrentTable.Columns[text4];
                        if (text4 != "")
                        {
                            string text7 = text4;
                            if (text7 == null)
                            {
                                goto Label_03AE;
                            }
                            if (text7 != "GroupCode")
                            {
                                if (text7 == "SupplierCode")
                                {
                                    goto Label_03A1;
                                }
                                goto Label_03AE;
                            }
                            row[text4] = text2;
                        }
                        continue;
                    Label_03A1:
                        row[text4] = supplierCodeByName;
                        continue;
                    Label_03AE:
                        s = ConvertRule.ToString(ConvertRule.GetArrayItem(arr, index)).Trim();
                        if (s == "")
                        {
                            try
                            {
                                row[text4] = s;
                            }
                            catch
                            {
                                row[text4] = DBNull.Value;
                            }
                        }
                        else
                        {
                            row[text4] = s;
                        }
                    }
                    entity.CurrentTable.Rows.Add(row);
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    row = null;
                    throw exception;
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                hint = "异常出错：" + exception.Message;
            }
            return row;
        }

        protected override void InitDefine()
        {
            base.MinColCount = 7;
            base.tbDefine.Rows.Add(new object[] { "GroupCode", "类型全名", true });
            base.tbDefine.Rows.Add(new object[] { "SupplierCode", "厂商名称", true });
            base.tbDefine.Rows.Add(new object[] { "Brand", "品牌" });
            base.tbDefine.Rows.Add(new object[] { "Model", "型号" });
            base.tbDefine.Rows.Add(new object[] { "Nation", "进口/国产" });
            base.tbDefine.Rows.Add(new object[] { "AreaCode", "产地" });
            base.tbDefine.Rows.Add(new object[] { "Spec", "规格" });
            base.tbDefine.Rows.Add(new object[] { "SampleID", "样品序号" });
            base.tbDefine.Rows.Add(new object[] { "Unit", "单位" });
            base.tbDefine.Rows.Add(new object[] { "Price", "单价" });
        }
    }
}

