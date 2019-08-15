namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class MeetRoomDAL
    {
        private SqlDataProcess _DataProcess;

        public MeetRoomDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public MeetRoomDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private MeetRoomModel _DataBind(int Code)
        {
            MeetRoomModel model = new MeetRoomModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from MeetRoom ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            SqlDataReader sqlDataReader = null;
            try
            {
                try
                {
                    sqlDataReader = this._DataProcess.GetSqlDataReader();
                    while (sqlDataReader.Read())
                    {
                        this.Initialize(sqlDataReader, model);
                    }
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            finally
            {
                if (sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }
            }
            return model;
        }

        private int _Delete(int Code)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from MeetRoom ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(MeetRoomModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("INSERT INTO MeetRoom (");
            builder2.Append("VALUES(");
            if (mObj.RoomName != null)
            {
                builder.Append("RoomName,");
                builder2.Append("@RoomName,");
                this._DataProcess.ProcessParametersAdd("@RoomName", SqlDbType.VarChar, 200, mObj.RoomName);
            }
            if (mObj.Capacity != null)
            {
                builder.Append("Capacity,");
                builder2.Append("@Capacity,");
                this._DataProcess.ProcessParametersAdd("@Capacity", SqlDbType.VarChar, 50, mObj.Capacity);
            }
            if (mObj.Place != null)
            {
                builder.Append("Place,");
                builder2.Append("@Place,");
                this._DataProcess.ProcessParametersAdd("@Place", SqlDbType.VarChar, 500, mObj.Place);
            }
            if (mObj.HardCondition != null)
            {
                builder.Append("HardCondition,");
                builder2.Append("@HardCondition,");
                this._DataProcess.ProcessParametersAdd("@HardCondition", SqlDbType.VarChar, 500, mObj.HardCondition);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark,");
                builder2.Append("@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 0x7d0, mObj.Remark);
            }
            builder.Remove(builder.Length - 1, 1);
            builder2.Remove(builder2.Length - 1, 1);
            builder.Append(") ");
            builder2.Append(") ");
            builder.Append(builder2.ToString());
            builder.Append("SELECT @Code = SCOPE_IDENTITY()");
            this._DataProcess.CommandText = builder.ToString();
            SqlParameter parameter = this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, null);
            parameter.Direction = ParameterDirection.Output;
            this._DataProcess.RunSql();
            mObj.Code = (int) parameter.Value;
            return mObj.Code;
        }

        private List<MeetRoomModel> _Select(MeetRoomQueryModel qmObj)
        {
            List<MeetRoomModel> list = new List<MeetRoomModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from MeetRoom ");
            builder.Append(qmObj.QueryConditionStr);
            if (qmObj.SortColumns.Length == 0)
            {
                builder.Append(" ORDER BY Code desc");
            }
            else
            {
                builder.Append(" ORDER BY " + qmObj.SortColumns);
            }
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.SqlParameters = qmObj.Parameters;
            SqlDataReader sqlDataReader = null;
            int num = 0;
            try
            {
                try
                {
                    sqlDataReader = this._DataProcess.GetSqlDataReader();
                    while (sqlDataReader.Read())
                    {
                        if ((num >= qmObj.StartRecord) && ((list.Count < qmObj.MaxRecords) || (qmObj.MaxRecords == -1)))
                        {
                            MeetRoomModel model = new MeetRoomModel();
                            this.Initialize(sqlDataReader, model);
                            list.Add(model);
                        }
                        num++;
                    }
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
            }
            finally
            {
                if (sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }
            }
            return list;
        }

        private int _Update(MeetRoomModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update MeetRoom set ");
            if (mObj.RoomName != null)
            {
                builder.Append("RoomName=@RoomName,");
                this._DataProcess.ProcessParametersAdd("@RoomName", SqlDbType.VarChar, 200, mObj.RoomName);
            }
            if (mObj.Capacity != null)
            {
                builder.Append("Capacity=@Capacity,");
                this._DataProcess.ProcessParametersAdd("@Capacity", SqlDbType.VarChar, 50, mObj.Capacity);
            }
            if (mObj.Place != null)
            {
                builder.Append("Place=@Place,");
                this._DataProcess.ProcessParametersAdd("@Place", SqlDbType.VarChar, 500, mObj.Place);
            }
            if (mObj.HardCondition != null)
            {
                builder.Append("HardCondition=@HardCondition,");
                this._DataProcess.ProcessParametersAdd("@HardCondition", SqlDbType.VarChar, 500, mObj.HardCondition);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark=@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 0x7d0, mObj.Remark);
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" where Code=@Code");
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, mObj.Code);
            this._DataProcess.CommandText = builder.ToString();
            return this._DataProcess.RunSql();
        }

        public int Delete(int Code)
        {
            return this._Delete(Code);
        }

        public int Delete(MeetRoomModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public MeetRoomModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, MeetRoomModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.RoomName = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.Capacity = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.Place = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.HardCondition = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.Remark = reader.GetString(5);
            }
        }

        public int Insert(MeetRoomModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<MeetRoomModel> Select()
        {
            MeetRoomQueryModel qmObj = new MeetRoomQueryModel();
            return this._Select(qmObj);
        }

        public List<MeetRoomModel> Select(MeetRoomQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(MeetRoomModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

