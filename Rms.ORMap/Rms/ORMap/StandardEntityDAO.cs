namespace Rms.ORMap
{
    using System;
    using System.Data;

    public class StandardEntityDAO : AbstractEntityDAO
    {
        public StandardEntityDAO(string entityName) : base(entityName)
        {
        }

        public StandardEntityDAO(string entityName, IDBCommon cdb) : base(entityName, cdb)
        {
        }

        public override void DeleteAllRow(EntityData entitydata)
        {
            try
            {
                string mainTableName = entitydata.MainTableName;
                foreach (DataRelation relation in entitydata.Relations)
                {
                    if (mainTableName == relation.ParentTable.TableName)
                    {
                        string tableName = relation.ChildTable.TableName;
                        DataTable table = entitydata.Tables[tableName];
                        foreach (DataRow row in table.Rows)
                        {
                            row.Delete();
                        }
                    }
                }
                DataTable table2 = entitydata.Tables[0];
                foreach (DataRow row2 in table2.Rows)
                {
                    row2.Delete();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public override void DeleteEntity(EntityData entitydata)
        {
            try
            {
                this.CheckData(entitydata);
                string className = entitydata.MainTableName;
                foreach (DataRelation relation in entitydata.Relations)
                {
                    if (className == relation.ParentTable.TableName)
                    {
                        string tableName = relation.ChildTable.TableName;
                        SqlStruct sqlStruct = SqlManager.GetSqlStruct(tableName, "Delete");
                        if (sqlStruct.SqlString.Length != 0)
                        {
                            base.db.SubmitDataTable(entitydata.Tables[tableName], sqlStruct, SqlOperatorType.Delete);
                        }
                    }
                }
                SqlStruct struct3 = SqlManager.GetSqlStruct(className, "Delete");
                if (struct3.SqlString.Length != 0)
                {
                    base.db.SubmitDataTable(entitydata.Tables[className], struct3, SqlOperatorType.Delete);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public override void InsertEntity(EntityData entitydata)
        {
            try
            {
                this.CheckData(entitydata);
                string className = entitydata.MainTableName;
                SqlStruct sqlStruct = SqlManager.GetSqlStruct(className, "Insert");
                if (sqlStruct.SqlString.Length != 0)
                {
                    base.db.SubmitDataTable(entitydata.Tables[className], sqlStruct, SqlOperatorType.Insert);
                }
                foreach (DataRelation relation in entitydata.Relations)
                {
                    if (className == relation.ParentTable.TableName)
                    {
                        string tableName = relation.ChildTable.TableName;
                        SqlStruct struct3 = SqlManager.GetSqlStruct(tableName, "Insert");
                        if (struct3.SqlString.Length != 0)
                        {
                            base.db.SubmitDataTable(entitydata.Tables[tableName], struct3, SqlOperatorType.Insert);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public override EntityData SelectAll()
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData(base.m_EntityName);
                foreach (DataRelation relation in entitydata.Relations)
                {
                    if (entitydata.Tables[base.m_EntityName].TableName == relation.ChildTable.TableName)
                    {
                        string tableName = relation.ParentTable.TableName;
                        SqlStruct struct2 = SqlManager.GetSqlStruct(base.m_EntityName, tableName, "SelectAll");
                        if (struct2.SqlString.Length != 0)
                        {
                            base.db.FillEntity(struct2.GetSqlStringWithOrder(), (string[]) null, (object[]) null, entitydata, tableName);
                        }
                    }
                }
                SqlStruct sqlStruct = SqlManager.GetSqlStruct(base.m_EntityName, "SelectAll");
                if (sqlStruct.SqlString.Length != 0)
                {
                    base.db.FillEntity(sqlStruct.GetSqlStringWithOrder(), (string[]) null, (object[]) null, entitydata, base.m_EntityName);
                }
                foreach (DataRelation relation2 in entitydata.Relations)
                {
                    if (entitydata.Tables[base.m_EntityName].TableName == relation2.ParentTable.TableName)
                    {
                        string text2 = relation2.ChildTable.TableName;
                        SqlStruct struct4 = SqlManager.GetSqlStruct(base.m_EntityName, text2, "SelectAll");
                        if (struct4.SqlString.Length != 0)
                        {
                            base.db.FillEntity(struct4.GetSqlStringWithOrder(), (string[]) null, (object[]) null, entitydata, text2);
                        }
                    }
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public override EntityData SelectbyPrimaryKey(object keyvalues)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData(base.m_EntityName);
                string className = entitydata.MainTableName;
                foreach (DataRelation relation in entitydata.Relations)
                {
                    if (className == relation.ChildTable.TableName)
                    {
                        string tableName = relation.ParentTable.TableName;
                        SqlStruct sqlStruct = SqlManager.GetSqlStruct(tableName, "Select");
                        if (sqlStruct.SqlString.Length != 0)
                        {
                            base.db.FillEntity(sqlStruct.SqlString, sqlStruct.ParamsList[0], keyvalues, entitydata, tableName);
                        }
                    }
                }
                SqlStruct struct3 = SqlManager.GetSqlStruct(className, "Select");
                if (struct3.SqlString.Length != 0)
                {
                    if (1 != struct3.ParamsList.Length)
                    {
                        throw new ApplicationException("参数列表不匹配");
                    }
                    base.db.FillEntity(struct3.SqlString, struct3.ParamsList[0], keyvalues, entitydata, base.m_EntityName);
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public override EntityData SelectbyPrimaryKey(object[] keyvalues)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData(base.m_EntityName);
                string className = entitydata.MainTableName;
                foreach (DataRelation relation in entitydata.Relations)
                {
                    if (className == relation.ChildTable.TableName)
                    {
                        string tableName = relation.ParentTable.TableName;
                        SqlStruct sqlStruct = SqlManager.GetSqlStruct(tableName, "Select");
                        if (sqlStruct.SqlString.Length != 0)
                        {
                            base.db.FillEntity(sqlStruct.SqlString, sqlStruct.ParamsList, keyvalues, entitydata, tableName);
                        }
                    }
                }
                SqlStruct struct3 = SqlManager.GetSqlStruct(className, "Select");
                if (struct3.SqlString.Length != 0)
                {
                    base.db.FillEntity(struct3.SqlString, struct3.ParamsList, keyvalues, entitydata, base.m_EntityName);
                }
                foreach (DataRelation relation2 in entitydata.Relations)
                {
                    if (className == relation2.ParentTable.TableName)
                    {
                        string text3 = relation2.ChildTable.TableName;
                        SqlStruct struct4 = SqlManager.GetSqlStruct(text3, "Select");
                        if (struct4.SqlString.Length != 0)
                        {
                            base.db.FillEntity(struct4.SqlString, struct4.ParamsList, keyvalues, entitydata, text3);
                        }
                    }
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public override void SubmitEntity(EntityData entitydata)
        {
            try
            {
                this.CheckData(entitydata);
                string className = entitydata.MainTableName;
                base.db.SubmitAllData(entitydata.Tables[className], SqlManager.GetSqlStruct(className, "Insert"), SqlManager.GetSqlStruct(className, "Update"), SqlManager.GetSqlStruct(className, "Delete"));
                foreach (DataRelation relation in entitydata.Relations)
                {
                    if (className == relation.ParentTable.TableName)
                    {
                        string tableName = relation.ChildTable.TableName;
                        base.db.SubmitAllData(entitydata.Tables[tableName], SqlManager.GetSqlStruct(tableName, "Insert"), SqlManager.GetSqlStruct(tableName, "Update"), SqlManager.GetSqlStruct(tableName, "Delete"));
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public override void UpdateEntity(EntityData entitydata)
        {
            try
            {
                this.CheckData(entitydata);
                string className = entitydata.MainTableName;
                SqlStruct sqlStruct = SqlManager.GetSqlStruct(className, "Update");
                if (sqlStruct.SqlString.Length != 0)
                {
                    base.db.SubmitDataTable(entitydata.Tables[className], sqlStruct, SqlOperatorType.Update);
                }
                foreach (DataRelation relation in entitydata.Relations)
                {
                    if (className == relation.ParentTable.TableName)
                    {
                        string tableName = relation.ChildTable.TableName;
                        SqlStruct struct3 = SqlManager.GetSqlStruct(tableName, "Update");
                        if (struct3.SqlString.Length != 0)
                        {
                            base.db.SubmitDataTable(entitydata.Tables[tableName], struct3, SqlOperatorType.Update);
                        }
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

