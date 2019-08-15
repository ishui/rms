namespace Rms.ORMap
{
    using System;
    using System.Data;

    [Serializable]
    public class EntityData : DataSet
    {
        private string m_ClassName;
        private int m_CurrentRowIndex;
        private int m_CurrentTableIndex;
        private string m_EntityTypeName;
        private string m_MainTableName;

        public EntityData()
        {
            this.m_EntityTypeName = "";
            this.m_ClassName = "";
            this.m_MainTableName = "";
            this.m_CurrentTableIndex = 0;
            this.m_CurrentRowIndex = 0;
        }

        public EntityData(string className)
        {
            this.m_EntityTypeName = "";
            this.m_ClassName = "";
            this.m_MainTableName = "";
            this.m_CurrentTableIndex = 0;
            this.m_CurrentRowIndex = 0;
            try
            {
                this.m_ClassName = className;
                this.m_EntityTypeName = "";
                EntityDataManager.CloneEntityStruct(this, className);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 在当前表中添加新的数据
        /// </summary>
        /// <param name="row"></param>
        public void AddNewRecord(DataRow row)
        {
            base.Tables[this.m_CurrentTableIndex].Rows.Add(row);
        }

        /// <summary>
        /// 在指定表中添加新的数据
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableName"></param>
        public void AddNewRecord(DataRow row, string tableName)
        {
            if (!base.Tables.Contains(tableName))
            {
                throw new ApplicationException("不存在该表");
            }
            try
            {
                base.Tables[tableName].Rows.Add(row);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 复制数据
        /// </summary>
        /// <returns></returns>
        public EntityData CloneData()
        {
            EntityData data2;
            try
            {
                EntityData data = (EntityData) base.Clone();
                data.EntityTypeName = this.EntityTypeName;
                data.ClassName = this.ClassName;
                data.MainTableName = this.MainTableName;
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        /// <summary>
        /// 复制数据结构
        /// </summary>
        /// <param name="cloneentitydata"></param>
        public void CloneDataStucture(EntityData cloneentitydata)
        {
            try
            {
                cloneentitydata.EntityTypeName = this.EntityTypeName;
                cloneentitydata.ClassName = this.ClassName;
                cloneentitydata.MainTableName = this.MainTableName;
                cloneentitydata.Relations.Clear();
                cloneentitydata.Tables.Clear();
                foreach (DataTable table in base.Tables)
                {
                    cloneentitydata.Tables.Add(table.Clone());
                }
                foreach (DataRelation relation in base.Relations)
                {
                    DataTable childTable = relation.ChildTable;
                    DataTable parentTable = relation.ParentTable;
                    int length = relation.ChildColumns.Length;
                    int num2 = relation.ParentColumns.Length;
                    DataColumn[] childColumns = new DataColumn[length];
                    DataColumn[] parentColumns = new DataColumn[num2];
                    for (int i = 0; i < length; i++)
                    {
                        childColumns[i] = cloneentitydata.Tables[childTable.TableName].Columns[relation.ChildColumns[i].ColumnName];
                    }
                    for (int j = 0; j < num2; j++)
                    {
                        parentColumns[j] = cloneentitydata.Tables[parentTable.TableName].Columns[relation.ParentColumns[j].ColumnName];
                    }
                    cloneentitydata.Relations.Add(relation.RelationName, parentColumns, childColumns);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 删除所有数据记录
        /// </summary>
        /// <param name="tableName"></param>
        public void DeleteAllTableRow(string tableName)
        {
            if (!base.Tables.Contains(tableName))
            {
                throw new ApplicationException("不存在该表：" + tableName);
            }
            DataTable table = base.Tables[tableName];
            int count = table.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                table.Rows[i].Delete();
            }
        }

        /// <summary>
        /// 将布尔型字段数据作为布尔型输出、将NUll作为flase输出
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool GetBoolean(string columnName)
        {
            bool flag;
            try
            {
                if (base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    return false;
                }
                flag = (bool) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName];
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        /// <summary>
        /// 将字节型字段数据作为字节型输出、将NUll作为0输出
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public byte GetByte(string columnName)
        {
            byte num;
            try
            {
                if (base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    return 0;
                }
                num = (byte) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName];
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        /// <summary>
        /// 将字符型字段数据作为字符型输出、将NUll作为""输出
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string GetChar(string columnName)
        {
            string text;
            try
            {
                if (base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    return "";
                }
                text = ((char) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName]).ToString();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }

        /// <summary>
        /// 将时间型字段数据作为时间型输出、将NUll作为""输出
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string GetDateTime(string columnName)
        {
            string text;
            try
            {
                if (base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    return "";
                }
                text = ((DateTime) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName]).ToString();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }

        /// <summary>
        /// 将时间型字段数据作为格式化时间型输出、将NUll作为""输出
        /// </summary>
        /// <param name="columnName">字段</param>
        /// <param name="formatString">格式化参数</param>
        /// <returns></returns>
        public string GetDateTime(string columnName, string formatString)
        {
            string text;
            try
            {
                if (base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    return "";
                }
                text = ((DateTime) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName]).ToString(formatString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }

        /// <summary>
        /// 将时间型字段数据作为日期型输出、将NUll作为""输出
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string GetDateTimeOnlyDate(string columnName)
        {
            string text;
            try
            {
                if (base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    return "";
                }
                text = ((DateTime) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName]).ToString("yyyy-MM-dd");
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }

        /// <summary>
        /// 将28位有效数字的高精度小数类型作为小数类型输出、将NUll作为0M输出
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public decimal GetDecimal(string columnName)
        {
            decimal num;
            try
            {
                if (base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    return 0M;
                }
                num = (decimal) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName];
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        /// <summary>
        /// 将28位有效数字的高精度小数类型作为字符串型输出、将NUll作为""输出
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string GetDecimalString(string columnName)
        {
            string text2;
            try
            {
                string text = "";
                if (!base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    text = ((decimal) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName]).ToString("F");
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        /// <summary>
        /// 将双精度浮点数类型作为双精度浮点数类型输出、将NUll作为0输出
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public double GetDouble(string columnName)
        {
            double num;
            try
            {
                if (base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    return 0;
                }
                num = (double) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName];
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        /// <summary>
        /// 将双精度浮点数类型作为双精度浮点数类型输出、将NUll作为""输出
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string GetDoubleString(string columnName)
        {
            string text2;
            try
            {
                string text = "";
                if (!base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    text = ((double) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName]).ToString("F");
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        /// <summary>
        /// 将浮点型字段数据作为浮点型输出、将NUll作为Of输出
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public float GetFloat(string columnName)
        {
            float num;
            try
            {
                if (base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    return 0f;
                }
                num = (float) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName];
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        /// <summary>
        /// 将浮点型字段数据作为字符输出、将NUll作为“”输出
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string GetFloatString(string columnName)
        {
            string text2;
            try
            {
                string text = "";
                if (!base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    text = ((float) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName]).ToString("F");
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        /// <summary>
        /// 将整型字段数据作为整型输出、将NUll作为0输出
        /// </summary>
        /// <param name="columnName">字段</param>
        /// <returns></returns>
        public int GetInt(string columnName)
        {
            int num;
            try
            {
                if (base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    return 0;
                }
                num = (int) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName];
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        /// <summary>
        /// 将整型字段数据作为字符串输出、将NUll作为“”输出
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string GetIntString(string columnName)
        {
            string text2;
            try
            {
                string text = "";
                if (!base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    text = ((int) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName]).ToString();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        /// <summary>
        /// 将长整型字段数据作为长整型输出、将NUll作为0输出
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public long GetLong(string columnName)
        {
            long num;
            try
            {
                if (base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    return (long) 0;
                }
                num = (long) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName];
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        /// <summary>
        /// 将长整型字段数据作为字符串输出、将NUll作为“”输出
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string GetLongString(string columnName)
        {
            string text2;
            try
            {
                string text = "";
                if (base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    text = ((long) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName]).ToString();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public DataRow GetNewRecord()
        {
            return base.Tables[this.m_CurrentTableIndex].NewRow();
        }

        public DataRow GetNewRecord(string tableName)
        {
            if (!base.Tables.Contains(tableName))
            {
                throw new ApplicationException("不存在该表");
            }
            return base.Tables[tableName].NewRow();
        }

        public DataRow GetRecord()
        {
            return base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex];
        }

        public DataRow GetRecord(int index)
        {
            if (base.Tables.Count < 1)
            {
                return null;
            }
            if (base.Tables[0].Rows.Count < (index + 1))
            {
                return null;
            }
            return base.Tables[this.m_CurrentTableIndex].Rows[index];
        }

        public DataRow GetRecord(string tableName)
        {
            if (!base.Tables.Contains(tableName))
            {
                throw new ApplicationException("不存在该表");
            }
            if (base.Tables[tableName].Rows.Count < 1)
            {
                return null;
            }
            return base.Tables[tableName].Rows[this.m_CurrentRowIndex];
        }

        public DataRow GetRecord(string tableName, int index)
        {
            if (!base.Tables.Contains(tableName))
            {
                throw new ApplicationException("不存在该表");
            }
            if (base.Tables[tableName].Rows.Count < (index + 1))
            {
                return null;
            }
            return base.Tables[tableName].Rows[index];
        }

        public sbyte GetSByte(string columnName)
        {
            sbyte num;
            try
            {
                if (base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    return 0;
                }
                num = (sbyte) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName];
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        /// <summary>
        /// 将短字符串型字段数据作为短字符串输出、将NUll作为“”输出
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public short GetShort(string columnName)
        {
            short num;
            try
            {
                if (base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    return 0;
                }
                num = (short) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName];
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num;
        }

        /// <summary>
        /// 将短字符串型字段数据作为字符串输出、将NUll作为“”输出
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string GetShortString(string columnName)
        {
            string text2;
            try
            {
                string text = "";
                if (base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    text = ((short) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName]).ToString();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        /// <summary>
        /// 将字符串型字段数据作为字符串输出、将NUll作为“”输出
        /// </summary>
        /// <param name="columnName">字段</param>
        /// <returns></returns>
        public string GetString(string columnName)
        {
            string text;
            try
            {
                if (base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex].IsNull(columnName))
                {
                    return "";
                }
                text = (string) base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex][columnName];
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text;
        }

        /// <summary>
        /// 数据集中是否有数据
        /// </summary>
        /// <returns></returns>
        public bool HasRecord()
        {
            return (base.Tables[this.m_CurrentTableIndex].Rows.Count > 0);
        }

        public void SetCurrentRow(int rowIndex)
        {
            if ((rowIndex < 0) || (rowIndex >= base.Tables[this.m_CurrentTableIndex].Rows.Count))
            {
                throw new ApplicationException("数据行索引超出范围");
            }
            this.m_CurrentRowIndex = rowIndex;
        }

        public void SetCurrentTable(int tableIndex)
        {
            if ((tableIndex < 0) || (tableIndex >= base.Tables.Count))
            {
                throw new ApplicationException("数据表索引超出范围");
            }
            this.m_CurrentTableIndex = tableIndex;
            this.m_CurrentRowIndex = 0;
        }

        public void SetCurrentTable(string tableName)
        {
            if (!base.Tables.Contains(tableName))
            {
                throw new ApplicationException("不存在该表");
            }
            this.m_CurrentTableIndex = base.Tables.IndexOf(tableName);
            this.m_CurrentRowIndex = 0;
        }

        public string ClassName
        {
            get
            {
                return this.m_ClassName;
            }
            set
            {
                this.m_ClassName = value;
            }
        }

        public DataRow CurrentRow
        {
            get
            {
                if (this.m_CurrentRowIndex >= base.Tables[this.m_CurrentTableIndex].Rows.Count)
                {
                    return null;
                }
                return base.Tables[this.m_CurrentTableIndex].Rows[this.m_CurrentRowIndex];
            }
        }

        public int CurrentRowIndex
        {
            get
            {
                return this.m_CurrentRowIndex;
            }
        }

        public DataTable CurrentTable
        {
            get
            {
                return base.Tables[this.m_CurrentTableIndex];
            }
        }

        public int CurrentTableIndex
        {
            get
            {
                return this.m_CurrentTableIndex;
            }
        }

        public string EntityTypeName
        {
            get
            {
                return this.m_EntityTypeName;
            }
            set
            {
                this.m_EntityTypeName = value;
            }
        }

        public string MainTableName
        {
            get
            {
                return this.m_MainTableName;
            }
            set
            {
                this.m_MainTableName = value;
            }
        }
    }
}

