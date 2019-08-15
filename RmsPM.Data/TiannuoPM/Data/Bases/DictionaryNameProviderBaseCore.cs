namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class DictionaryNameProviderBaseCore : EntityProviderBase<DictionaryName, DictionaryNameKey>
    {
        protected DictionaryNameProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, DictionaryName entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if ((entity != null) && base.CanDeepLoad(entity, "List<DictionaryItem>", "DictionaryItemCollection", deepLoadType, innerList))
            {
                entity.DictionaryItemCollection = DataRepository.DictionaryItemProvider.GetByDictionaryNameCode(transactionManager, entity.DictionaryNameCode);
                if (deep && (entity.DictionaryItemCollection.Count > 0))
                {
                    DataRepository.DictionaryItemProvider.DeepLoad(transactionManager, entity.DictionaryItemCollection, deep, deepLoadType, childTypes, innerList);
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, DictionaryName entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity == null)
            {
                return false;
            }
            this.Save(transactionManager, entity);
            if (base.CanDeepSave(entity, "List<DictionaryItem>", "DictionaryItemCollection", deepSaveType, innerList))
            {
                foreach (DictionaryItem item in entity.DictionaryItemCollection)
                {
                    item.DictionaryNameCode = entity.DictionaryNameCode;
                }
                if ((entity.DictionaryItemCollection.Count > 0) || (entity.DictionaryItemCollection.DeletedItems.Count > 0))
                {
                    DataRepository.DictionaryItemProvider.DeepSave(transactionManager, entity.DictionaryItemCollection, deepSaveType, childTypes, innerList);
                }
            }
            return true;
        }

        public bool Delete(string dictionaryNameCode)
        {
            return this.Delete(null, dictionaryNameCode);
        }

        public abstract bool Delete(TransactionManager transactionManager, string dictionaryNameCode);
        public override bool Delete(TransactionManager transactionManager, DictionaryNameKey key)
        {
            return this.Delete(transactionManager, key.DictionaryNameCode);
        }

        public static TList<DictionaryName> Fill(IDataReader reader, TList<DictionaryName> rows, int start, int pageLength)
        {
            int num;
            for (num = 0; num < start; num++)
            {
                if (!reader.Read())
                {
                    return rows;
                }
            }
            for (num = 0; num < pageLength; num++)
            {
                if (!reader.Read())
                {
                    return rows;
                }
                string text = null;
                DictionaryName item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "DictionaryName" + (reader.IsDBNull(reader.GetOrdinal("DictionaryNameCode")) ? string.Empty : ((string) reader["DictionaryNameCode"])).ToString();
                    item = EntityManager.LocateOrCreate<DictionaryName>(text.ToString(), "DictionaryName", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new DictionaryName();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    item.SuppressEntityEvents = true;
                    item.DictionaryNameCode = (string) reader["DictionaryNameCode"];
                    item.OriginalDictionaryNameCode = item.DictionaryNameCode;
                    item.NAME = reader.IsDBNull(reader.GetOrdinal("NAME")) ? null : ((string) reader["NAME"]);
                    item.ProjectCode = reader.IsDBNull(reader.GetOrdinal("ProjectCode")) ? null : ((string) reader["ProjectCode"]);
                    item.Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? null : ((string) reader["Remark"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override DictionaryName Get(TransactionManager transactionManager, DictionaryNameKey key, int start, int pageLength)
        {
            return this.GetByDictionaryNameCode(transactionManager, key.DictionaryNameCode, start, pageLength);
        }

        public DictionaryName GetByDictionaryNameCode(string dictionaryNameCode)
        {
            int count = -1;
            return this.GetByDictionaryNameCode(null, dictionaryNameCode, 0, 0x7fffffff, out count);
        }

        public DictionaryName GetByDictionaryNameCode(TransactionManager transactionManager, string dictionaryNameCode)
        {
            int count = -1;
            return this.GetByDictionaryNameCode(transactionManager, dictionaryNameCode, 0, 0x7fffffff, out count);
        }

        public DictionaryName GetByDictionaryNameCode(string dictionaryNameCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByDictionaryNameCode(null, dictionaryNameCode, start, pageLength, out count);
        }

        public DictionaryName GetByDictionaryNameCode(string dictionaryNameCode, int start, int pageLength, out int count)
        {
            return this.GetByDictionaryNameCode(null, dictionaryNameCode, start, pageLength, out count);
        }

        public DictionaryName GetByDictionaryNameCode(TransactionManager transactionManager, string dictionaryNameCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByDictionaryNameCode(transactionManager, dictionaryNameCode, start, pageLength, out count);
        }

        public abstract DictionaryName GetByDictionaryNameCode(TransactionManager transactionManager, string dictionaryNameCode, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, DictionaryName entity)
        {
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.DictionaryNameCode = (string) row["DictionaryNameCode"];
            entity.OriginalDictionaryNameCode = (string) row["DictionaryNameCode"];
            entity.NAME = Convert.IsDBNull(row["NAME"]) ? null : ((string) row["NAME"]);
            entity.ProjectCode = Convert.IsDBNull(row["ProjectCode"]) ? null : ((string) row["ProjectCode"]);
            entity.Remark = Convert.IsDBNull(row["Remark"]) ? null : ((string) row["Remark"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, DictionaryName entity)
        {
            if (reader.Read())
            {
                entity.DictionaryNameCode = (string) reader["DictionaryNameCode"];
                entity.OriginalDictionaryNameCode = (string) reader["DictionaryNameCode"];
                entity.NAME = reader.IsDBNull(reader.GetOrdinal("NAME")) ? null : ((string) reader["NAME"]);
                entity.ProjectCode = reader.IsDBNull(reader.GetOrdinal("ProjectCode")) ? null : ((string) reader["ProjectCode"]);
                entity.Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? null : ((string) reader["Remark"]);
                entity.AcceptChanges();
            }
        }
    }
}

