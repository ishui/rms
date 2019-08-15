namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class MaterialCostImport : ImportRule.ImportBase
    {
        public DataRow ImportMaterialCostSingle(string val, EntityData entity, ref string hint, ref string Description, ref string GroupFullName, bool isTest, DataTable tbAllSystemGroup)
        {
            Exception exception;
            hint = "";
            DataRow row = null;
            try
            {
                int index;
                DataRow row2;
                string text3;
                DataColumn column;
                string s;
                string[] arr = ImportRule.SplitCsvLine(val);
                if (arr.Length < base.MinColCount)
                {
                    hint = "列不足3个";
                    return null;
                }
                Description = arr[6].Trim();
                GroupFullName = "";
                if (Description == "")
                {
                    hint = "描述为空";
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
                int count = base.tbDefine.Rows.Count;
                for (index = 0; index < count; index++)
                {
                    row2 = base.tbDefine.Rows[index];
                    text3 = row2["FieldName"].ToString();
                    string text4 = row2["FieldDesc"].ToString();
                    column = entity.CurrentTable.Columns[text3];
                    if ((text3 != "") && (text3 != "GroupCode"))
                    {
                        s = ConvertRule.ToString(ConvertRule.GetArrayItem(arr, index)).Trim();
                        if ((column.DataType == typeof(string)) && (StringRule.LenB(s) > column.MaxLength))
                        {
                            hint = string.Format("{0}（{1}）长度超过{2}位（现为{3}位）", new object[] { text4, s, column.MaxLength, s.Length });
                            return null;
                        }
                    }
                }
                row = entity.CurrentTable.NewRow();
                try
                {
                    if (isTest)
                    {
                        row["MaterialCostCode"] = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        row["MaterialCostCode"] = SystemManageDAO.GetNewSysCode("MaterialCostCode").ToString();
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
                            if ((text6 != null) && (text6 == "GroupCode"))
                            {
                                row[text3] = text2;
                            }
                            else
                            {
                                s = ConvertRule.ToString(ConvertRule.GetArrayItem(arr, index)).Trim();
                                if (s == "")
                                {
                                    try
                                    {
                                        row[text3] = s;
                                    }
                                    catch
                                    {
                                        row[text3] = DBNull.Value;
                                    }
                                }
                                else
                                {
                                    row[text3] = s;
                                }
                            }
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
            base.tbDefine.Rows.Add(new object[] { "Unit", "单位" });
            base.tbDefine.Rows.Add(new object[] { "Price", "单价" });
            base.tbDefine.Rows.Add(new object[] { "Project", "项目" });
            base.tbDefine.Rows.Add(new object[] { "BiddingDate", "定标日期" });
            base.tbDefine.Rows.Add(new object[] { "AreaCode", "地区" });
            base.tbDefine.Rows.Add(new object[] { "Description", "描述", true });
            base.tbDefine.Rows.Add(new object[] { "DescriptionEn", "description" });
            base.tbDefine.Rows.Add(new object[] { "Category", "category" });
        }
    }
}

