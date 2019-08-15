namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class ContractAccountProviderBaseCore : EntityProviderBase<ContractAccount, ContractAccountKey>
    {
        protected ContractAccountProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, ContractAccount entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if ((entity != null) && (base.CanDeepLoad(entity, "Contract", "ContractCodeSource", deepLoadType, innerList) && (entity.ContractCodeSource == null)))
            {
                object[] pkItems = new object[] { entity.ContractCode ?? string.Empty };
                Contract contract = EntityManager.LocateEntity<Contract>(EntityLocator.ConstructKeyFromPkItems(typeof(Contract), pkItems), DataRepository.Provider.EnableEntityTracking);
                if (contract != null)
                {
                    entity.ContractCodeSource = contract;
                }
                else
                {
                    entity.ContractCodeSource = DataRepository.ContractProvider.GetByContractCode(entity.ContractCode ?? string.Empty);
                }
                if (deep && (entity.ContractCodeSource != null))
                {
                    DataRepository.ContractProvider.DeepLoad(transactionManager, entity.ContractCodeSource, deep, deepLoadType, childTypes, innerList);
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, ContractAccount entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity == null)
            {
                return false;
            }
            if (base.CanDeepSave(entity, "Contract", "ContractCodeSource", deepSaveType, innerList) && (entity.ContractCodeSource != null))
            {
                DataRepository.ContractProvider.Save(transactionManager, entity.ContractCodeSource);
                entity.ContractCode = entity.ContractCodeSource.ContractCode;
            }
            this.Save(transactionManager, entity);
            return true;
        }

        public bool Delete(string contractAccountCode)
        {
            return this.Delete(null, contractAccountCode);
        }

        public abstract bool Delete(TransactionManager transactionManager, string contractAccountCode);
        public override bool Delete(TransactionManager transactionManager, ContractAccountKey key)
        {
            return this.Delete(transactionManager, key.ContractAccountCode);
        }

        public static TList<ContractAccount> Fill(IDataReader reader, TList<ContractAccount> rows, int start, int pageLength)
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
                ContractAccount item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "ContractAccount" + (reader.IsDBNull(reader.GetOrdinal("ContractAccountCode")) ? string.Empty : ((string) reader["ContractAccountCode"])).ToString();
                    item = EntityManager.LocateOrCreate<ContractAccount>(text.ToString(), "ContractAccount", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new ContractAccount();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    item.SuppressEntityEvents = true;
                    item.ContractAccountCode = (string) reader["ContractAccountCode"];
                    item.OriginalContractAccountCode = item.ContractAccountCode;
                    item.ContractAccountID = reader.IsDBNull(reader.GetOrdinal("ContractAccountID")) ? null : ((string) reader["ContractAccountID"]);
                    item.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                    item.Reason = reader.IsDBNull(reader.GetOrdinal("Reason")) ? null : ((string) reader["Reason"]);
                    item.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : ((int?) reader["Status"]);
                    item.CreateDate = reader.IsDBNull(reader.GetOrdinal("CreateDate")) ? null : ((DateTime?) reader["CreateDate"]);
                    item.CreatePerson = reader.IsDBNull(reader.GetOrdinal("CreatePerson")) ? null : ((string) reader["CreatePerson"]);
                    item.ContractChangeCode = reader.IsDBNull(reader.GetOrdinal("ContractChangeCode")) ? null : ((string) reader["ContractChangeCode"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override ContractAccount Get(TransactionManager transactionManager, ContractAccountKey key, int start, int pageLength)
        {
            return this.GetByContractAccountCode(transactionManager, key.ContractAccountCode, start, pageLength);
        }

        public ContractAccount GetByContractAccountCode(string contractAccountCode)
        {
            int count = -1;
            return this.GetByContractAccountCode(null, contractAccountCode, 0, 0x7fffffff, out count);
        }

        public ContractAccount GetByContractAccountCode(TransactionManager transactionManager, string contractAccountCode)
        {
            int count = -1;
            return this.GetByContractAccountCode(transactionManager, contractAccountCode, 0, 0x7fffffff, out count);
        }

        public ContractAccount GetByContractAccountCode(string contractAccountCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractAccountCode(null, contractAccountCode, start, pageLength, out count);
        }

        public ContractAccount GetByContractAccountCode(string contractAccountCode, int start, int pageLength, out int count)
        {
            return this.GetByContractAccountCode(null, contractAccountCode, start, pageLength, out count);
        }

        public ContractAccount GetByContractAccountCode(TransactionManager transactionManager, string contractAccountCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractAccountCode(transactionManager, contractAccountCode, start, pageLength, out count);
        }

        public abstract ContractAccount GetByContractAccountCode(TransactionManager transactionManager, string contractAccountCode, int start, int pageLength, out int count);
        public TList<ContractAccount> GetByContractCode(string contractCode)
        {
            int count = -1;
            return this.GetByContractCode(contractCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractAccount> GetByContractCode(TransactionManager transactionManager, string contractCode)
        {
            int count = -1;
            return this.GetByContractCode(transactionManager, contractCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractAccount> GetByContractCode(string contractCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCode(null, contractCode, start, pageLength, out count);
        }

        public TList<ContractAccount> GetByContractCode(string contractCode, int start, int pageLength, out int count)
        {
            return this.GetByContractCode(null, contractCode, start, pageLength, out count);
        }

        public TList<ContractAccount> GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCode(transactionManager, contractCode, start, pageLength, out count);
        }

        public abstract TList<ContractAccount> GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, ContractAccount entity)
        {
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.ContractAccountCode = (string) row["ContractAccountCode"];
            entity.OriginalContractAccountCode = (string) row["ContractAccountCode"];
            entity.ContractAccountID = Convert.IsDBNull(row["ContractAccountID"]) ? null : ((string) row["ContractAccountID"]);
            entity.ContractCode = Convert.IsDBNull(row["ContractCode"]) ? null : ((string) row["ContractCode"]);
            entity.Reason = Convert.IsDBNull(row["Reason"]) ? null : ((string) row["Reason"]);
            entity.Status = Convert.IsDBNull(row["Status"]) ? null : ((int?) row["Status"]);
            entity.CreateDate = Convert.IsDBNull(row["CreateDate"]) ? null : ((DateTime?) row["CreateDate"]);
            entity.CreatePerson = Convert.IsDBNull(row["CreatePerson"]) ? null : ((string) row["CreatePerson"]);
            entity.ContractChangeCode = Convert.IsDBNull(row["ContractChangeCode"]) ? null : ((string) row["ContractChangeCode"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, ContractAccount entity)
        {
            if (reader.Read())
            {
                entity.ContractAccountCode = (string) reader["ContractAccountCode"];
                entity.OriginalContractAccountCode = (string) reader["ContractAccountCode"];
                entity.ContractAccountID = reader.IsDBNull(reader.GetOrdinal("ContractAccountID")) ? null : ((string) reader["ContractAccountID"]);
                entity.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                entity.Reason = reader.IsDBNull(reader.GetOrdinal("Reason")) ? null : ((string) reader["Reason"]);
                entity.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : ((int?) reader["Status"]);
                entity.CreateDate = reader.IsDBNull(reader.GetOrdinal("CreateDate")) ? null : ((DateTime?) reader["CreateDate"]);
                entity.CreatePerson = reader.IsDBNull(reader.GetOrdinal("CreatePerson")) ? null : ((string) reader["CreatePerson"]);
                entity.ContractChangeCode = reader.IsDBNull(reader.GetOrdinal("ContractChangeCode")) ? null : ((string) reader["ContractChangeCode"]);
                entity.AcceptChanges();
            }
        }
    }
}

