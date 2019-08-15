namespace RmsOA.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using RmsOA.MODEL;

    public class YF_AssetManageDAL
    {
        private SqlDataProcess _DataProcess;

        public YF_AssetManageDAL(SqlConnection Connection)
        {
            this._DataProcess = new SqlDataProcess(Connection);
        }

        public YF_AssetManageDAL(SqlTransaction Transaction)
        {
            this._DataProcess = new SqlDataProcess(Transaction);
        }

        private YF_AssetManageModel _DataBind(int Code)
        {
            YF_AssetManageModel model = new YF_AssetManageModel();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from YF_AssetManage ");
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
            builder.Append("delete from YF_AssetManage ");
            builder.Append(" where Code=@Code");
            this._DataProcess.CommandText = builder.ToString();
            this._DataProcess.ProcessParametersAdd("@Code", SqlDbType.Int, 4, Code);
            return this._DataProcess.RunSql();
        }

        private int _Insert(YF_AssetManageModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            builder.Append("INSERT INTO YF_AssetManage (");
            builder2.Append("VALUES(");
            if (mObj.FacilityName != null)
            {
                builder.Append("FacilityName,");
                builder2.Append("@FacilityName,");
                this._DataProcess.ProcessParametersAdd("@FacilityName", SqlDbType.VarChar, 100, mObj.FacilityName);
            }
            if (mObj.Area != null)
            {
                builder.Append("Area,");
                builder2.Append("@Area,");
                this._DataProcess.ProcessParametersAdd("@Area", SqlDbType.VarChar, 100, mObj.Area);
            }
            if (mObj.Producer != null)
            {
                builder.Append("Producer,");
                builder2.Append("@Producer,");
                this._DataProcess.ProcessParametersAdd("@Producer", SqlDbType.VarChar, 100, mObj.Producer);
            }
            if (mObj.BuyCorp != null)
            {
                builder.Append("BuyCorp,");
                builder2.Append("@BuyCorp,");
                this._DataProcess.ProcessParametersAdd("@BuyCorp", SqlDbType.VarChar, 100, mObj.BuyCorp);
            }
            if (mObj.ModelNO != null)
            {
                builder.Append("ModelNO,");
                builder2.Append("@ModelNO,");
                this._DataProcess.ProcessParametersAdd("@ModelNO", SqlDbType.VarChar, 100, mObj.ModelNO);
            }
            if (mObj.LayCorp != null)
            {
                builder.Append("LayCorp,");
                builder2.Append("@LayCorp,");
                this._DataProcess.ProcessParametersAdd("@LayCorp", SqlDbType.VarChar, 100, mObj.LayCorp);
            }
            if (mObj.Type != null)
            {
                builder.Append("Type,");
                builder2.Append("@Type,");
                this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 50, mObj.Type);
            }
            if (mObj.Counts != null)
            {
                builder.Append("Counts,");
                builder2.Append("@Counts,");
                this._DataProcess.ProcessParametersAdd("@Counts", SqlDbType.VarChar, 50, mObj.Counts);
            }
            if (mObj.UseDept != null)
            {
                builder.Append("UseDept,");
                builder2.Append("@UseDept,");
                this._DataProcess.ProcessParametersAdd("@UseDept", SqlDbType.VarChar, 50, mObj.UseDept);
            }
            if (mObj.LayPlace != null)
            {
                builder.Append("LayPlace,");
                builder2.Append("@LayPlace,");
                this._DataProcess.ProcessParametersAdd("@LayPlace", SqlDbType.VarChar, 50, mObj.LayPlace);
            }
            if (mObj.CountUnit != null)
            {
                builder.Append("CountUnit,");
                builder2.Append("@CountUnit,");
                this._DataProcess.ProcessParametersAdd("@CountUnit", SqlDbType.VarChar, 50, mObj.CountUnit);
            }
            if (mObj.ProdArea != null)
            {
                builder.Append("ProdArea,");
                builder2.Append("@ProdArea,");
                this._DataProcess.ProcessParametersAdd("@ProdArea", SqlDbType.VarChar, 50, mObj.ProdArea);
            }
            if (mObj.SortNO != null)
            {
                builder.Append("SortNO,");
                builder2.Append("@SortNO,");
                this._DataProcess.ProcessParametersAdd("@SortNO", SqlDbType.VarChar, 50, mObj.SortNO);
            }
            if (mObj.SortType != null)
            {
                builder.Append("SortType,");
                builder2.Append("@SortType,");
                this._DataProcess.ProcessParametersAdd("@SortType", SqlDbType.VarChar, 50, mObj.SortType);
            }
            if (mObj.FreeMain != null)
            {
                builder.Append("FreeMain,");
                builder2.Append("@FreeMain,");
                this._DataProcess.ProcessParametersAdd("@FreeMain", SqlDbType.VarChar, 20, mObj.FreeMain);
            }
            if (mObj.EquiType != null)
            {
                builder.Append("EquiType,");
                builder2.Append("@EquiType,");
                this._DataProcess.ProcessParametersAdd("@EquiType", SqlDbType.VarChar, 50, mObj.EquiType);
            }
            if (mObj.Provider != null)
            {
                builder.Append("Provider,");
                builder2.Append("@Provider,");
                this._DataProcess.ProcessParametersAdd("@Provider", SqlDbType.VarChar, 50, mObj.Provider);
            }
            if (mObj.BuyDate != DateTime.MinValue)
            {
                builder.Append("BuyDate,");
                builder2.Append("@BuyDate,");
                this._DataProcess.ProcessParametersAdd("@BuyDate", SqlDbType.DateTime, 8, mObj.BuyDate);
            }
            if (mObj.Price != 0M)
            {
                builder.Append("Price,");
                builder2.Append("@Price,");
                this._DataProcess.ProcessParametersAdd("@Price", SqlDbType.Decimal, 9, mObj.Price);
            }
            if (mObj.Register != null)
            {
                builder.Append("Register,");
                builder2.Append("@Register,");
                this._DataProcess.ProcessParametersAdd("@Register", SqlDbType.VarChar, 50, mObj.Register);
            }
            if (mObj.MainCardPlace != null)
            {
                builder.Append("MainCardPlace,");
                builder2.Append("@MainCardPlace,");
                this._DataProcess.ProcessParametersAdd("@MainCardPlace", SqlDbType.VarChar, 50, mObj.MainCardPlace);
            }
            if (mObj.StoreStatus != null)
            {
                builder.Append("StoreStatus,");
                builder2.Append("@StoreStatus,");
                this._DataProcess.ProcessParametersAdd("@StoreStatus", SqlDbType.VarChar, 50, mObj.StoreStatus);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark,");
                builder2.Append("@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 50, mObj.Remark);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status,");
                builder2.Append("@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            }
            if (mObj.DeptUnit != null)
            {
                builder.Append("DeptUnit,");
                builder2.Append("@DeptUnit,");
                this._DataProcess.ProcessParametersAdd("@DeptUnit", SqlDbType.VarChar, 50, mObj.DeptUnit);
            }
            if (mObj.BookINTime != DateTime.MinValue)
            {
                builder.Append("BookINTime,");
                builder2.Append("@BookINTime,");
                this._DataProcess.ProcessParametersAdd("@BookINTime", SqlDbType.DateTime, 8, mObj.BookINTime);
            }
            if (mObj.BuyType != null)
            {
                builder.Append("BuyType,");
                builder2.Append("@BuyType,");
                this._DataProcess.ProcessParametersAdd("@BuyType", SqlDbType.VarChar, 50, mObj.BuyType);
            }
            if (mObj.CodeNO != null)
            {
                builder.Append("CodeNO,");
                builder2.Append("@CodeNO,");
                this._DataProcess.ProcessParametersAdd("@CodeNO", SqlDbType.VarChar, 50, mObj.CodeNO);
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

        private List<YF_AssetManageModel> _Select(YF_AssetManageQueryModel qmObj)
        {
            List<YF_AssetManageModel> list = new List<YF_AssetManageModel>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from YF_AssetManage ");
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
                            YF_AssetManageModel model = new YF_AssetManageModel();
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

        private int _Update(YF_AssetManageModel mObj)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update YF_AssetManage set ");
            if (mObj.FacilityName != null)
            {
                builder.Append("FacilityName=@FacilityName,");
                this._DataProcess.ProcessParametersAdd("@FacilityName", SqlDbType.VarChar, 100, mObj.FacilityName);
            }
            if (mObj.Area != null)
            {
                builder.Append("Area=@Area,");
                this._DataProcess.ProcessParametersAdd("@Area", SqlDbType.VarChar, 100, mObj.Area);
            }
            if (mObj.Producer != null)
            {
                builder.Append("Producer=@Producer,");
                this._DataProcess.ProcessParametersAdd("@Producer", SqlDbType.VarChar, 100, mObj.Producer);
            }
            if (mObj.BuyCorp != null)
            {
                builder.Append("BuyCorp=@BuyCorp,");
                this._DataProcess.ProcessParametersAdd("@BuyCorp", SqlDbType.VarChar, 100, mObj.BuyCorp);
            }
            if (mObj.ModelNO != null)
            {
                builder.Append("ModelNO=@ModelNO,");
                this._DataProcess.ProcessParametersAdd("@ModelNO", SqlDbType.VarChar, 100, mObj.ModelNO);
            }
            if (mObj.LayCorp != null)
            {
                builder.Append("LayCorp=@LayCorp,");
                this._DataProcess.ProcessParametersAdd("@LayCorp", SqlDbType.VarChar, 100, mObj.LayCorp);
            }
            if (mObj.Type != null)
            {
                builder.Append("Type=@Type,");
                this._DataProcess.ProcessParametersAdd("@Type", SqlDbType.VarChar, 50, mObj.Type);
            }
            if (mObj.Counts != null)
            {
                builder.Append("Counts=@Counts,");
                this._DataProcess.ProcessParametersAdd("@Counts", SqlDbType.VarChar, 50, mObj.Counts);
            }
            if (mObj.UseDept != null)
            {
                builder.Append("UseDept=@UseDept,");
                this._DataProcess.ProcessParametersAdd("@UseDept", SqlDbType.VarChar, 50, mObj.UseDept);
            }
            if (mObj.LayPlace != null)
            {
                builder.Append("LayPlace=@LayPlace,");
                this._DataProcess.ProcessParametersAdd("@LayPlace", SqlDbType.VarChar, 50, mObj.LayPlace);
            }
            if (mObj.CountUnit != null)
            {
                builder.Append("CountUnit=@CountUnit,");
                this._DataProcess.ProcessParametersAdd("@CountUnit", SqlDbType.VarChar, 50, mObj.CountUnit);
            }
            if (mObj.ProdArea != null)
            {
                builder.Append("ProdArea=@ProdArea,");
                this._DataProcess.ProcessParametersAdd("@ProdArea", SqlDbType.VarChar, 50, mObj.ProdArea);
            }
            if (mObj.SortNO != null)
            {
                builder.Append("SortNO=@SortNO,");
                this._DataProcess.ProcessParametersAdd("@SortNO", SqlDbType.VarChar, 50, mObj.SortNO);
            }
            if (mObj.SortType != null)
            {
                builder.Append("SortType=@SortType,");
                this._DataProcess.ProcessParametersAdd("@SortType", SqlDbType.VarChar, 50, mObj.SortType);
            }
            if (mObj.FreeMain != null)
            {
                builder.Append("FreeMain=@FreeMain,");
                this._DataProcess.ProcessParametersAdd("@FreeMain", SqlDbType.VarChar, 20, mObj.FreeMain);
            }
            if (mObj.EquiType != null)
            {
                builder.Append("EquiType=@EquiType,");
                this._DataProcess.ProcessParametersAdd("@EquiType", SqlDbType.VarChar, 50, mObj.EquiType);
            }
            if (mObj.Provider != null)
            {
                builder.Append("Provider=@Provider,");
                this._DataProcess.ProcessParametersAdd("@Provider", SqlDbType.VarChar, 50, mObj.Provider);
            }
            if (mObj.BuyDate != DateTime.MinValue)
            {
                builder.Append("BuyDate=@BuyDate,");
                this._DataProcess.ProcessParametersAdd("@BuyDate", SqlDbType.DateTime, 8, mObj.BuyDate);
            }
            if (mObj.Price != 0M)
            {
                builder.Append("Price=@Price,");
                this._DataProcess.ProcessParametersAdd("@Price", SqlDbType.Decimal, 9, mObj.Price);
            }
            if (mObj.Register != null)
            {
                builder.Append("Register=@Register,");
                this._DataProcess.ProcessParametersAdd("@Register", SqlDbType.VarChar, 50, mObj.Register);
            }
            if (mObj.MainCardPlace != null)
            {
                builder.Append("MainCardPlace=@MainCardPlace,");
                this._DataProcess.ProcessParametersAdd("@MainCardPlace", SqlDbType.VarChar, 50, mObj.MainCardPlace);
            }
            if (mObj.StoreStatus != null)
            {
                builder.Append("StoreStatus=@StoreStatus,");
                this._DataProcess.ProcessParametersAdd("@StoreStatus", SqlDbType.VarChar, 50, mObj.StoreStatus);
            }
            if (mObj.Remark != null)
            {
                builder.Append("Remark=@Remark,");
                this._DataProcess.ProcessParametersAdd("@Remark", SqlDbType.VarChar, 50, mObj.Remark);
            }
            if (mObj.Status != null)
            {
                builder.Append("Status=@Status,");
                this._DataProcess.ProcessParametersAdd("@Status", SqlDbType.VarChar, 50, mObj.Status);
            }
            if (mObj.DeptUnit != null)
            {
                builder.Append("DeptUnit=@DeptUnit,");
                this._DataProcess.ProcessParametersAdd("@DeptUnit", SqlDbType.VarChar, 50, mObj.DeptUnit);
            }
            if (mObj.BookINTime != DateTime.MinValue)
            {
                builder.Append("BookINTime=@BookINTime,");
                this._DataProcess.ProcessParametersAdd("@BookINTime", SqlDbType.DateTime, 8, mObj.BookINTime);
            }
            if (mObj.BuyType != null)
            {
                builder.Append("BuyType=@BuyType,");
                this._DataProcess.ProcessParametersAdd("@BuyType", SqlDbType.VarChar, 50, mObj.BuyType);
            }
            if (mObj.CodeNO != null)
            {
                builder.Append("CodeNO=@CodeNO,");
                this._DataProcess.ProcessParametersAdd("@CodeNO", SqlDbType.VarChar, 50, mObj.CodeNO);
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

        public int Delete(YF_AssetManageModel mObj)
        {
            return this._Delete(mObj.Code);
        }

        public YF_AssetManageModel GetModel(int Code)
        {
            return this._DataBind(Code);
        }

        private void Initialize(SqlDataReader reader, YF_AssetManageModel obj)
        {
            if (reader.GetValue(0) != DBNull.Value)
            {
                obj.Code = reader.GetInt32(0);
            }
            if (reader.GetValue(1) != DBNull.Value)
            {
                obj.FacilityName = reader.GetString(1);
            }
            if (reader.GetValue(2) != DBNull.Value)
            {
                obj.Area = reader.GetString(2);
            }
            if (reader.GetValue(3) != DBNull.Value)
            {
                obj.Producer = reader.GetString(3);
            }
            if (reader.GetValue(4) != DBNull.Value)
            {
                obj.BuyCorp = reader.GetString(4);
            }
            if (reader.GetValue(5) != DBNull.Value)
            {
                obj.ModelNO = reader.GetString(5);
            }
            if (reader.GetValue(6) != DBNull.Value)
            {
                obj.LayCorp = reader.GetString(6);
            }
            if (reader.GetValue(7) != DBNull.Value)
            {
                obj.Type = reader.GetString(7);
            }
            if (reader.GetValue(8) != DBNull.Value)
            {
                obj.Counts = reader.GetString(8);
            }
            if (reader.GetValue(9) != DBNull.Value)
            {
                obj.UseDept = reader.GetString(9);
            }
            if (reader.GetValue(10) != DBNull.Value)
            {
                obj.LayPlace = reader.GetString(10);
            }
            if (reader.GetValue(11) != DBNull.Value)
            {
                obj.CountUnit = reader.GetString(11);
            }
            if (reader.GetValue(12) != DBNull.Value)
            {
                obj.ProdArea = reader.GetString(12);
            }
            if (reader.GetValue(13) != DBNull.Value)
            {
                obj.SortNO = reader.GetString(13);
            }
            if (reader.GetValue(14) != DBNull.Value)
            {
                obj.SortType = reader.GetString(14);
            }
            if (reader.GetValue(15) != DBNull.Value)
            {
                obj.FreeMain = reader.GetString(15);
            }
            if (reader.GetValue(0x10) != DBNull.Value)
            {
                obj.EquiType = reader.GetString(0x10);
            }
            if (reader.GetValue(0x11) != DBNull.Value)
            {
                obj.Provider = reader.GetString(0x11);
            }
            if (reader.GetValue(0x12) != DBNull.Value)
            {
                obj.BuyDate = reader.GetDateTime(0x12);
            }
            if (reader.GetValue(0x13) != DBNull.Value)
            {
                obj.Price = reader.GetDecimal(0x13);
            }
            if (reader.GetValue(20) != DBNull.Value)
            {
                obj.Register = reader.GetString(20);
            }
            if (reader.GetValue(0x15) != DBNull.Value)
            {
                obj.MainCardPlace = reader.GetString(0x15);
            }
            if (reader.GetValue(0x16) != DBNull.Value)
            {
                obj.StoreStatus = reader.GetString(0x16);
            }
            if (reader.GetValue(0x17) != DBNull.Value)
            {
                obj.Remark = reader.GetString(0x17);
            }
            if (reader.GetValue(0x18) != DBNull.Value)
            {
                obj.Status = reader.GetString(0x18);
            }
            if (reader.GetValue(0x19) != DBNull.Value)
            {
                obj.DeptUnit = reader.GetString(0x19);
            }
            if (reader.GetValue(0x1a) != DBNull.Value)
            {
                obj.BookINTime = reader.GetDateTime(0x1a);
            }
            if (reader.GetValue(0x1b) != DBNull.Value)
            {
                obj.BuyType = reader.GetString(0x1b);
            }
            if (reader.GetValue(0x1c) != DBNull.Value)
            {
                obj.CodeNO = reader.GetString(0x1c);
            }
        }

        public int Insert(YF_AssetManageModel mObj)
        {
            return this._Insert(mObj);
        }

        public List<YF_AssetManageModel> Select()
        {
            YF_AssetManageQueryModel qmObj = new YF_AssetManageQueryModel();
            return this._Select(qmObj);
        }

        public List<YF_AssetManageModel> Select(YF_AssetManageQueryModel qmObj)
        {
            return this._Select(qmObj);
        }

        public int Update(YF_AssetManageModel mObj)
        {
            return this._Update(mObj);
        }
    }
}

