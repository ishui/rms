namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class DictionaryItemProviderBaseCore : EntityProviderBase<DictionaryItem, DictionaryItemKey>
    {
        protected DictionaryItemProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, DictionaryItem entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if ((entity != null) && (base.CanDeepLoad(entity, "DictionaryName", "DictionaryNameCodeSource", deepLoadType, innerList) && (entity.DictionaryNameCodeSource == null)))
            {
                object[] pkItems = new object[] { entity.DictionaryNameCode ?? string.Empty };
                DictionaryName name = EntityManager.LocateEntity<DictionaryName>(EntityLocator.ConstructKeyFromPkItems(typeof(DictionaryName), pkItems), DataRepository.Provider.EnableEntityTracking);
                if (name != null)
                {
                    entity.DictionaryNameCodeSource = name;
                }
                else
                {
                    entity.DictionaryNameCodeSource = DataRepository.DictionaryNameProvider.GetByDictionaryNameCode(entity.DictionaryNameCode ?? string.Empty);
                }
                if (deep && (entity.DictionaryNameCodeSource != null))
                {
                    DataRepository.DictionaryNameProvider.DeepLoad(transactionManager, entity.DictionaryNameCodeSource, deep, deepLoadType, childTypes, innerList);
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, DictionaryItem entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity == null)
            {
                return false;
            }
            if (base.CanDeepSave(entity, "DictionaryName", "DictionaryNameCodeSource", deepSaveType, innerList) && (entity.DictionaryNameCodeSource != null))
            {
                DataRepository.DictionaryNameProvider.Save(transactionManager, entity.DictionaryNameCodeSource);
                entity.DictionaryNameCode = entity.DictionaryNameCodeSource.DictionaryNameCode;
            }
            this.Save(transactionManager, entity);
            return true;
        }

        public bool Delete(string dictionaryItemCode)
        {
            return this.Delete(null, dictionaryItemCode);
        }

        public abstract bool Delete(TransactionManager transactionManager, string dictionaryItemCode);
        public override bool Delete(TransactionManager transactionManager, DictionaryItemKey key)
        {
            return this.Delete(transactionManager, key.DictionaryItemCode);
        }

        public static TList<DictionaryItem> Fill(IDataReader reader, TList<DictionaryItem> rows, int start, int pageLength)
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
                DictionaryItem item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "DictionaryItem" + (reader.IsDBNull(reader.GetOrdinal("DictionaryItemCode")) ? string.Empty : ((string) reader["DictionaryItemCode"])).ToString();
                    item = EntityManager.LocateOrCreate<DictionaryItem>(text.ToString(), "DictionaryItem", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new DictionaryItem();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    item.SuppressEntityEvents = true;
                    item.DictionaryItemCode = (string) reader["DictionaryItemCode"];
                    item.OriginalDictionaryItemCode = item.DictionaryItemCode;
                    item.ProjectCode = reader.IsDBNull(reader.GetOrdinal("ProjectCode")) ? null : ((string) reader["ProjectCode"]);
                    item.DictionaryNameCode = reader.IsDBNull(reader.GetOrdinal("DictionaryNameCode")) ? null : ((string) reader["DictionaryNameCode"]);
                    item.SortID = reader.IsDBNull(reader.GetOrdinal("SortID")) ? null : ((int?) reader["SortID"]);
                    item.Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : ((string) reader["Name"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override DictionaryItem Get(TransactionManager transactionManager, DictionaryItemKey key, int start, int pageLength)
        {
            return this.GetByDictionaryItemCode(transactionManager, key.DictionaryItemCode, start, pageLength);
        }

        public DictionaryItem GetByDictionaryItemCode(string dictionaryItemCode)
        {
            int count = -1;
            return this.GetByDictionaryItemCode(null, dictionaryItemCode, 0, 0x7fffffff, out count);
        }

        public DictionaryItem GetByDictionaryItemCode(TransactionManager transactionManager, string dictionaryItemCode)
        {
            int count = -1;
            return this.GetByDictionaryItemCode(transactionManager, dictionaryItemCode, 0, 0x7fffffff, out count);
        }

        public DictionaryItem GetByDictionaryItemCode(string dictionaryItemCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByDictionaryItemCode(null, dictionaryItemCode, start, pageLength, out count);
        }

        public DictionaryItem GetByDictionaryItemCode(string dictionaryItemCode, int start, int pageLength, out int count)
        {
            return this.GetByDictionaryItemCode(null, dictionaryItemCode, start, pageLength, out count);
        }

        public DictionaryItem GetByDictionaryItemCode(TransactionManager transactionManager, string dictionaryItemCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByDictionaryItemCode(transactionManager, dictionaryItemCode, start, pageLength, out count);
        }

        public abstract DictionaryItem GetByDictionaryItemCode(TransactionManager transactionManager, string dictionaryItemCode, int start, int pageLength, out int count);
        public TList<DictionaryItem> GetByDictionaryNameCode(string dictionaryNameCode)
        {
            int count = -1;
            return this.GetByDictionaryNameCode(dictionaryNameCode, 0, 0x7fffffff, out count);
        }

        public TList<DictionaryItem> GetByDictionaryNameCode(TransactionManager transactionManager, string dictionaryNameCode)
        {
            int count = -1;
            return this.GetByDictionaryNameCode(transactionManager, dictionaryNameCode, 0, 0x7fffffff, out count);
        }

        public TList<DictionaryItem> GetByDictionaryNameCode(string dictionaryNameCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByDictionaryNameCode(null, dictionaryNameCode, start, pageLength, out count);
        }

        public TList<DictionaryItem> GetByDictionaryNameCode(string dictionaryNameCode, int start, int pageLength, out int count)
        {
            return this.GetByDictionaryNameCode(null, dictionaryNameCode, start, pageLength, out count);
        }

        public TList<DictionaryItem> GetByDictionaryNameCode(TransactionManager transactionManager, string dictionaryNameCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByDictionaryNameCode(transactionManager, dictionaryNameCode, start, pageLength, out count);
        }

        public abstract TList<DictionaryItem> GetByDictionaryNameCode(TransactionManager transactionManager, string dictionaryNameCode, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, DictionaryItem entity)
        {
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.DictionaryItemCode = (string) row["DictionaryItemCode"];
            entity.OriginalDictionaryItemCode = (string) row["DictionaryItemCode"];
            entity.ProjectCode = Convert.IsDBNull(row["ProjectCode"]) ? null : ((string) row["ProjectCode"]);
            entity.DictionaryNameCode = Convert.IsDBNull(row["DictionaryNameCode"]) ? null : ((string) row["DictionaryNameCode"]);
            entity.SortID = Convert.IsDBNull(row["SortID"]) ? null : ((int?) row["SortID"]);
            entity.Name = Convert.IsDBNull(row["Name"]) ? null : ((string) row["Name"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, DictionaryItem entity)
        {
            if (reader.Read())
            {
                entity.DictionaryItemCode = (string) reader["DictionaryItemCode"];
                entity.OriginalDictionaryItemCode = (string) reader["DictionaryItemCode"];
                entity.ProjectCode = reader.IsDBNull(reader.GetOrdinal("ProjectCode")) ? null : ((string) reader["ProjectCode"]);
                entity.DictionaryNameCode = reader.IsDBNull(reader.GetOrdinal("DictionaryNameCode")) ? null : ((string) reader["DictionaryNameCode"]);
                entity.SortID = reader.IsDBNull(reader.GetOrdinal("SortID")) ? null : ((int?) reader["SortID"]);
                entity.Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : ((string) reader["Name"]);
                entity.AcceptChanges();
            }
        }
    }
}

