namespace RmsOA.MODEL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class QueryBaseModel
    {
        private int _MaxRecords = -1;
        private List<SqlParameter> _Parameters;
        private string _QueryConditionStr = "";
        private string _SortColumns = "";
        private int _StartRecord = 0;

        public SqlParameter InsertParameter(string ParameterName, SqlDbType ParameterType, int Size, object Value)
        {
            SqlParameter item = new SqlParameter();
            item.ParameterName = ParameterName;
            item.SqlDbType = ParameterType;
            item.Size = Size;
            item.Value = Value;
            if (this._Parameters == null)
            {
                this._Parameters = new List<SqlParameter>();
            }
            this._Parameters.Add(item);
            return item;
        }

        public void QueryConditionStrAdd(string ConditionStr)
        {
            if (this.QueryConditionStr.Length == 0)
            {
                this.QueryConditionStr = this.QueryConditionStr + " where " + ConditionStr;
            }
            else
            {
                this.QueryConditionStr = this.QueryConditionStr + " and " + ConditionStr;
            }
        }

        public int MaxRecords
        {
            get
            {
                return this._MaxRecords;
            }
            set
            {
                this._MaxRecords = value;
            }
        }

        public List<SqlParameter> Parameters
        {
            get
            {
                return this._Parameters;
            }
        }

        public string QueryConditionStr
        {
            get
            {
                return this._QueryConditionStr;
            }
            set
            {
                this._QueryConditionStr = value;
            }
        }

        public string SortColumns
        {
            get
            {
                return this._SortColumns;
            }
            set
            {
                this._SortColumns = value;
            }
        }

        public int StartRecord
        {
            get
            {
                return this._StartRecord;
            }
            set
            {
                this._StartRecord = value;
            }
        }
    }
}

