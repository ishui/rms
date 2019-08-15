namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class ContractNexusProviderBaseCore : EntityProviderBase<ContractNexus, ContractNexusKey>
    {
        protected ContractNexusProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, ContractNexus entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if ((entity != null) && (base.CanDeepLoad(entity, "ContractChange", "ContractChangeCodeSource", deepLoadType, innerList) && (entity.ContractChangeCodeSource == null)))
            {
                object[] pkItems = new object[] { entity.ContractChangeCode ?? string.Empty };
                ContractChange change = EntityManager.LocateEntity<ContractChange>(EntityLocator.ConstructKeyFromPkItems(typeof(ContractChange), pkItems), DataRepository.Provider.EnableEntityTracking);
                if (change != null)
                {
                    entity.ContractChangeCodeSource = change;
                }
                else
                {
                    entity.ContractChangeCodeSource = DataRepository.ContractChangeProvider.GetByContractChangeCode(entity.ContractChangeCode ?? string.Empty);
                }
                if (deep && (entity.ContractChangeCodeSource != null))
                {
                    DataRepository.ContractChangeProvider.DeepLoad(transactionManager, entity.ContractChangeCodeSource, deep, deepLoadType, childTypes, innerList);
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, ContractNexus entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity == null)
            {
                return false;
            }
            if (base.CanDeepSave(entity, "ContractChange", "ContractChangeCodeSource", deepSaveType, innerList) && (entity.ContractChangeCodeSource != null))
            {
                DataRepository.ContractChangeProvider.Save(transactionManager, entity.ContractChangeCodeSource);
                entity.ContractChangeCode = entity.ContractChangeCodeSource.ContractChangeCode;
            }
            this.Save(transactionManager, entity);
            return true;
        }

        public bool Delete(string contractNexusCode)
        {
            return this.Delete(null, contractNexusCode);
        }

        public abstract bool Delete(TransactionManager transactionManager, string contractNexusCode);
        public override bool Delete(TransactionManager transactionManager, ContractNexusKey key)
        {
            return this.Delete(transactionManager, key.ContractNexusCode);
        }

        public static TList<ContractNexus> Fill(IDataReader reader, TList<ContractNexus> rows, int start, int pageLength)
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
                ContractNexus item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "ContractNexus" + (reader.IsDBNull(reader.GetOrdinal("ContractNexusCode")) ? string.Empty : ((string) reader["ContractNexusCode"])).ToString();
                    item = EntityManager.LocateOrCreate<ContractNexus>(text.ToString(), "ContractNexus", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new ContractNexus();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    item.SuppressEntityEvents = true;
                    item.ContractNexusCode = (string) reader["ContractNexusCode"];
                    item.OriginalContractNexusCode = item.ContractNexusCode;
                    item.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                    item.ContractChangeCode = reader.IsDBNull(reader.GetOrdinal("ContractChangeCode")) ? null : ((string) reader["ContractChangeCode"]);
                    item.Code = reader.IsDBNull(reader.GetOrdinal("Code")) ? null : ((string) reader["Code"]);
                    item.Type = reader.IsDBNull(reader.GetOrdinal("Type")) ? null : ((string) reader["Type"]);
                    item.Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : ((string) reader["Name"]);
                    item.ID = reader.IsDBNull(reader.GetOrdinal("ID")) ? null : ((string) reader["ID"]);
                    item.Person = reader.IsDBNull(reader.GetOrdinal("Person")) ? null : ((string) reader["Person"]);
                    item.Date = reader.IsDBNull(reader.GetOrdinal("Date")) ? null : ((DateTime?) reader["Date"]);
                    item.Path = reader.IsDBNull(reader.GetOrdinal("Path")) ? null : ((string) reader["Path"]);
                    item.Money = reader.IsDBNull(reader.GetOrdinal("Money")) ? null : ((decimal?) reader["Money"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override ContractNexus Get(TransactionManager transactionManager, ContractNexusKey key, int start, int pageLength)
        {
            return this.GetByContractNexusCode(transactionManager, key.ContractNexusCode, start, pageLength);
        }

        public TList<ContractNexus> GetByContractChangeCode(string contractChangeCode)
        {
            int count = -1;
            return this.GetByContractChangeCode(contractChangeCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractNexus> GetByContractChangeCode(TransactionManager transactionManager, string contractChangeCode)
        {
            int count = -1;
            return this.GetByContractChangeCode(transactionManager, contractChangeCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractNexus> GetByContractChangeCode(string contractChangeCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractChangeCode(null, contractChangeCode, start, pageLength, out count);
        }

        public TList<ContractNexus> GetByContractChangeCode(string contractChangeCode, int start, int pageLength, out int count)
        {
            return this.GetByContractChangeCode(null, contractChangeCode, start, pageLength, out count);
        }

        public TList<ContractNexus> GetByContractChangeCode(TransactionManager transactionManager, string contractChangeCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractChangeCode(transactionManager, contractChangeCode, start, pageLength, out count);
        }

        public abstract TList<ContractNexus> GetByContractChangeCode(TransactionManager transactionManager, string contractChangeCode, int start, int pageLength, out int count);
        public ContractNexus GetByContractNexusCode(string contractNexusCode)
        {
            int count = -1;
            return this.GetByContractNexusCode(null, contractNexusCode, 0, 0x7fffffff, out count);
        }

        public ContractNexus GetByContractNexusCode(TransactionManager transactionManager, string contractNexusCode)
        {
            int count = -1;
            return this.GetByContractNexusCode(transactionManager, contractNexusCode, 0, 0x7fffffff, out count);
        }

        public ContractNexus GetByContractNexusCode(string contractNexusCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractNexusCode(null, contractNexusCode, start, pageLength, out count);
        }

        public ContractNexus GetByContractNexusCode(string contractNexusCode, int start, int pageLength, out int count)
        {
            return this.GetByContractNexusCode(null, contractNexusCode, start, pageLength, out count);
        }

        public ContractNexus GetByContractNexusCode(TransactionManager transactionManager, string contractNexusCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractNexusCode(transactionManager, contractNexusCode, start, pageLength, out count);
        }

        public abstract ContractNexus GetByContractNexusCode(TransactionManager transactionManager, string contractNexusCode, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, ContractNexus entity)
        {
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.ContractNexusCode = (string) row["ContractNexusCode"];
            entity.OriginalContractNexusCode = (string) row["ContractNexusCode"];
            entity.ContractCode = Convert.IsDBNull(row["ContractCode"]) ? null : ((string) row["ContractCode"]);
            entity.ContractChangeCode = Convert.IsDBNull(row["ContractChangeCode"]) ? null : ((string) row["ContractChangeCode"]);
            entity.Code = Convert.IsDBNull(row["Code"]) ? null : ((string) row["Code"]);
            entity.Type = Convert.IsDBNull(row["Type"]) ? null : ((string) row["Type"]);
            entity.Name = Convert.IsDBNull(row["Name"]) ? null : ((string) row["Name"]);
            entity.ID = Convert.IsDBNull(row["ID"]) ? null : ((string) row["ID"]);
            entity.Person = Convert.IsDBNull(row["Person"]) ? null : ((string) row["Person"]);
            entity.Date = Convert.IsDBNull(row["Date"]) ? null : ((DateTime?) row["Date"]);
            entity.Path = Convert.IsDBNull(row["Path"]) ? null : ((string) row["Path"]);
            entity.Money = Convert.IsDBNull(row["Money"]) ? null : ((decimal?) row["Money"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, ContractNexus entity)
        {
            if (reader.Read())
            {
                entity.ContractNexusCode = (string) reader["ContractNexusCode"];
                entity.OriginalContractNexusCode = (string) reader["ContractNexusCode"];
                entity.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                entity.ContractChangeCode = reader.IsDBNull(reader.GetOrdinal("ContractChangeCode")) ? null : ((string) reader["ContractChangeCode"]);
                entity.Code = reader.IsDBNull(reader.GetOrdinal("Code")) ? null : ((string) reader["Code"]);
                entity.Type = reader.IsDBNull(reader.GetOrdinal("Type")) ? null : ((string) reader["Type"]);
                entity.Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : ((string) reader["Name"]);
                entity.ID = reader.IsDBNull(reader.GetOrdinal("ID")) ? null : ((string) reader["ID"]);
                entity.Person = reader.IsDBNull(reader.GetOrdinal("Person")) ? null : ((string) reader["Person"]);
                entity.Date = reader.IsDBNull(reader.GetOrdinal("Date")) ? null : ((DateTime?) reader["Date"]);
                entity.Path = reader.IsDBNull(reader.GetOrdinal("Path")) ? null : ((string) reader["Path"]);
                entity.Money = reader.IsDBNull(reader.GetOrdinal("Money")) ? null : ((decimal?) reader["Money"]);
                entity.AcceptChanges();
            }
        }
    }
}

