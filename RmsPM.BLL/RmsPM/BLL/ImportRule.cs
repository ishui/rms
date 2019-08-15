namespace RmsPM.BLL
{
    using System;
    using System.Collections;
    using System.Data;
    using Rms.ORMap;

    public class ImportRule
    {
      
        public static string[] SplitCsvLine(string sParam)
        {
            string[] textArray2;
            try
            {
                int index;
                string text = sParam;
                text = text.Replace("'", "") + ",";
                ArrayList list = new ArrayList();
                bool flag = false;
                int startIndex = 0;
                int length = text.Length;
                for (index = 0; index < length; index++)
                {
                    if (text[index] == "\""[0])
                    {
                        flag = !flag;
                    }
                    if ((text[index] == ","[0]) && !flag)
                    {
                        string text2 = text.Substring(startIndex, index - startIndex).Replace("\"", "");
                        list.Add(text2);
                        startIndex = index + 1;
                    }
                }
                string[] textArray = new string[list.Count];
                for (index = 0; index < list.Count; index++)
                {
                    textArray[index] = list[index].ToString();
                }
                textArray2 = textArray;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return textArray2;
        }

        public class ImportBase
        {
            private DataTable m_tbDefine = null;
            protected int MinColCount = 1;

            public ImportBase()
            {
                this.CreateDefine();
                this.InitDefine();
            }

            private void CreateDefine()
            {
                try
                {
                    this.m_tbDefine = new DataTable();
                    this.m_tbDefine.Columns.Add("FieldName");
                    this.m_tbDefine.Columns.Add("FieldDesc");
                    this.m_tbDefine.Columns.Add("NotNull", typeof(bool));
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }

            protected virtual bool CustomCheckValid(string[] arrVal, ref string hint)
            {
                hint = "";
                return true;
            }

            protected virtual bool CustomCheckValid(string FieldName, string val, string FieldDesc, ref string hint)
            {
                hint = "";
                return true;
            }

            public string GetDefineFieldDesc()
            {
                string text2;
                try
                {
                    text2 = ConvertRule.Concat(this.m_tbDefine, "FieldDesc", ",");
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                return text2;
            }

            public DataRow GetDefineRow(string FieldName)
            {
                DataRow row2;
                try
                {
                    DataRow row = null;
                    DataRow[] rowArray = this.m_tbDefine.Select("FieldName");
                    if (rowArray.Length > 0)
                    {
                        row = rowArray[0];
                    }
                    row2 = row;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                return row2;
            }

            protected virtual object GetImportFieldValue(string FieldName, string val)
            {
                return val;
            }

            protected virtual DataRow ImportNewRow(string[] arrVal)
            {
                return null;
            }

            protected int ImportRow(string sline, EntityData entity, ref string hint, ref string Name)
            {
                int num = -1;
                hint = "";
                try
                {
                    int index;
                    DataRow row;
                    string fieldName;
                    string val;
                    if (sline.Trim() == "")
                    {
                        hint = "行为空";
                        return num;
                    }
                    string[] arr = ImportRule.SplitCsvLine(sline);
                    if (arr.Length < this.MinColCount)
                    {
                        hint = string.Format("列不足{0}个", this.MinColCount);
                        return num;
                    }
                    int count = this.tbDefine.Rows.Count;
                    for (index = 0; index < count; index++)
                    {
                        row = this.tbDefine.Rows[index];
                        fieldName = ConvertRule.ToString(row["FieldName"]);
                        string text2 = ConvertRule.ToString(row["FieldName"]);
                        bool flag = ConvertRule.ToBool(row["NotNull"]);
                        val = ConvertRule.ToString(ConvertRule.GetArrayItem(arr, index)).Trim();
                        if (flag && (val == ""))
                        {
                            hint = string.Format("{0}不能为空", text2);
                            return num;
                        }
                        if (!this.CustomCheckValid(fieldName, val, text2, ref hint))
                        {
                            return num;
                        }
                    }
                    if (!this.CustomCheckValid(arr, ref hint))
                    {
                        return num;
                    }
                    if (this.ImportNewRow(arr) == null)
                    {
                        throw new Exception("未定义导入函数ImportNewRow");
                    }
                    for (index = 0; index < count; index++)
                    {
                        row = this.tbDefine.Rows[index];
                        fieldName = row["FieldName"].ToString();
                        if (fieldName != "")
                        {
                            val = ConvertRule.ToString(ConvertRule.GetArrayItem(arr, index)).Trim();
                            this.GetImportFieldValue(fieldName, val);
                        }
                    }
                    num = 1;
                }
                catch (Exception exception)
                {
                    hint = "异常出错：" + exception.Message;
                }
                return num;
            }

            protected virtual void InitDefine()
            {
            }

            public DataTable tbDefine
            {
                get
                {
                    return this.m_tbDefine;
                }
            }
        }
    }
}

