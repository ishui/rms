namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class ContractChangeProviderBaseCore : EntityProviderBase<ContractChange, ContractChangeKey>
    {
        protected ContractChangeProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, ContractChange entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity != null)
            {
                if (base.CanDeepLoad(entity, "Contract", "ContractCodeSource", deepLoadType, innerList) && (entity.ContractCodeSource == null))
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
                if (base.CanDeepLoad(entity, "List<ContractCostChange>", "ContractCostChangeCollection", deepLoadType, innerList))
                {
                    entity.ContractCostChangeCollection = DataRepository.ContractCostChangeProvider.GetByContractChangeCode(transactionManager, entity.ContractChangeCode);
                    if (deep && (entity.ContractCostChangeCollection.Count > 0))
                    {
                        DataRepository.ContractCostChangeProvider.DeepLoad(transactionManager, entity.ContractCostChangeCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
                if (base.CanDeepLoad(entity, "List<ContractNexus>", "ContractNexusCollection", deepLoadType, innerList))
                {
                    entity.ContractNexusCollection = DataRepository.ContractNexusProvider.GetByContractChangeCode(transactionManager, entity.ContractChangeCode);
                    if (deep && (entity.ContractNexusCollection.Count > 0))
                    {
                        DataRepository.ContractNexusProvider.DeepLoad(transactionManager, entity.ContractNexusCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, ContractChange entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
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
            if (base.CanDeepSave(entity, "List<ContractCostChange>", "ContractCostChangeCollection", deepSaveType, innerList))
            {
                foreach (ContractCostChange change in entity.ContractCostChangeCollection)
                {
                    change.ContractChangeCode = entity.ContractChangeCode;
                }
                if ((entity.ContractCostChangeCollection.Count > 0) || (entity.ContractCostChangeCollection.DeletedItems.Count > 0))
                {
                    DataRepository.ContractCostChangeProvider.DeepSave(transactionManager, entity.ContractCostChangeCollection, deepSaveType, childTypes, innerList);
                }
            }
            if (base.CanDeepSave(entity, "List<ContractNexus>", "ContractNexusCollection", deepSaveType, innerList))
            {
                foreach (ContractNexus nexus in entity.ContractNexusCollection)
                {
                    nexus.ContractChangeCode = entity.ContractChangeCode;
                }
                if ((entity.ContractNexusCollection.Count > 0) || (entity.ContractNexusCollection.DeletedItems.Count > 0))
                {
                    DataRepository.ContractNexusProvider.DeepSave(transactionManager, entity.ContractNexusCollection, deepSaveType, childTypes, innerList);
                }
            }
            return true;
        }

        public bool Delete(string contractChangeCode)
        {
            return this.Delete(null, contractChangeCode);
        }

        public abstract bool Delete(TransactionManager transactionManager, string contractChangeCode);
        public override bool Delete(TransactionManager transactionManager, ContractChangeKey key)
        {
            return this.Delete(transactionManager, key.ContractChangeCode);
        }

        public static TList<ContractChange> Fill(IDataReader reader, TList<ContractChange> rows, int start, int pageLength)
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
                ContractChange item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "ContractChange" + (reader.IsDBNull(reader.GetOrdinal("ContractChangeCode")) ? string.Empty : ((string) reader["ContractChangeCode"])).ToString();
                    item = EntityManager.LocateOrCreate<ContractChange>(text.ToString(), "ContractChange", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new ContractChange();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    decimal? nullable;
                    DateTime? nullable3;
                    item.SuppressEntityEvents = true;
                    item.ContractChangeCode = (string) reader["ContractChangeCode"];
                    item.OriginalContractChangeCode = item.ContractChangeCode;
                    item.ContractChangeId = reader.IsDBNull(reader.GetOrdinal("ContractChangeId")) ? null : ((string) reader["ContractChangeId"]);
                    item.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                    item.Voucher = reader.IsDBNull(reader.GetOrdinal("Voucher")) ? null : ((string) reader["Voucher"]);
                    item.Money = reader.IsDBNull(reader.GetOrdinal("Money")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["Money"]);
                    item.ChangeMoney = reader.IsDBNull(reader.GetOrdinal("ChangeMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ChangeMoney"]);
                    item.NewMoney = reader.IsDBNull(reader.GetOrdinal("NewMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["NewMoney"]);
                    item.OriginalMoney = reader.IsDBNull(reader.GetOrdinal("OriginalMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["OriginalMoney"]);
                    item.TotalChangeMoney = reader.IsDBNull(reader.GetOrdinal("TotalChangeMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["TotalChangeMoney"]);
                    item.SupplierChangeMoney = reader.IsDBNull(reader.GetOrdinal("SupplierChangeMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["SupplierChangeMoney"]);
                    item.ConsultantAuditMoney = reader.IsDBNull(reader.GetOrdinal("ConsultantAuditMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ConsultantAuditMoney"]);
                    item.ProjectAuditMoney = reader.IsDBNull(reader.GetOrdinal("ProjectAuditMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ProjectAuditMoney"]);
                    item.ChangeReason = reader.IsDBNull(reader.GetOrdinal("ChangeReason")) ? null : ((string) reader["ChangeReason"]);
                    item.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : ((int?) reader["Status"]);
                    item.ChangePerson = reader.IsDBNull(reader.GetOrdinal("ChangePerson")) ? null : ((string) reader["ChangePerson"]);
                    item.ChangeDate = reader.IsDBNull(reader.GetOrdinal("ChangeDate")) ? ((DateTime?) (nullable3 = null)) : ((DateTime?) reader["ChangeDate"]);
                    item.ChangeType = reader.IsDBNull(reader.GetOrdinal("ChangeType")) ? null : ((string) reader["ChangeType"]);
                    item.CheckPerson = reader.IsDBNull(reader.GetOrdinal("CheckPerson")) ? null : ((string) reader["CheckPerson"]);
                    item.CheckDate = reader.IsDBNull(reader.GetOrdinal("CheckDate")) ? ((DateTime?) (nullable3 = null)) : ((DateTime?) reader["CheckDate"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override ContractChange Get(TransactionManager transactionManager, ContractChangeKey key, int start, int pageLength)
        {
            return this.GetByContractChangeCode(transactionManager, key.ContractChangeCode, start, pageLength);
        }

        public ContractChange GetByContractChangeCode(string contractChangeCode)
        {
            int count = -1;
            return this.GetByContractChangeCode(null, contractChangeCode, 0, 0x7fffffff, out count);
        }

        public ContractChange GetByContractChangeCode(TransactionManager transactionManager, string contractChangeCode)
        {
            int count = -1;
            return this.GetByContractChangeCode(transactionManager, contractChangeCode, 0, 0x7fffffff, out count);
        }

        public ContractChange GetByContractChangeCode(string contractChangeCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractChangeCode(null, contractChangeCode, start, pageLength, out count);
        }

        public ContractChange GetByContractChangeCode(string contractChangeCode, int start, int pageLength, out int count)
        {
            return this.GetByContractChangeCode(null, contractChangeCode, start, pageLength, out count);
        }

        public ContractChange GetByContractChangeCode(TransactionManager transactionManager, string contractChangeCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractChangeCode(transactionManager, contractChangeCode, start, pageLength, out count);
        }

        public abstract ContractChange GetByContractChangeCode(TransactionManager transactionManager, string contractChangeCode, int start, int pageLength, out int count);
        public TList<ContractChange> GetByContractCode(string contractCode)
        {
            int count = -1;
            return this.GetByContractCode(contractCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractChange> GetByContractCode(TransactionManager transactionManager, string contractCode)
        {
            int count = -1;
            return this.GetByContractCode(transactionManager, contractCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractChange> GetByContractCode(string contractCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCode(null, contractCode, start, pageLength, out count);
        }

        public TList<ContractChange> GetByContractCode(string contractCode, int start, int pageLength, out int count)
        {
            return this.GetByContractCode(null, contractCode, start, pageLength, out count);
        }

        public TList<ContractChange> GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCode(transactionManager, contractCode, start, pageLength, out count);
        }

        public abstract TList<ContractChange> GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, ContractChange entity)
        {
            decimal? nullable;
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.ContractChangeCode = (string) row["ContractChangeCode"];
            entity.OriginalContractChangeCode = (string) row["ContractChangeCode"];
            entity.ContractChangeId = Convert.IsDBNull(row["ContractChangeId"]) ? null : ((string) row["ContractChangeId"]);
            entity.ContractCode = Convert.IsDBNull(row["ContractCode"]) ? null : ((string) row["ContractCode"]);
            entity.Voucher = Convert.IsDBNull(row["Voucher"]) ? null : ((string) row["Voucher"]);
            entity.Money = Convert.IsDBNull(row["Money"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["Money"]);
            entity.ChangeMoney = Convert.IsDBNull(row["ChangeMoney"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ChangeMoney"]);
            entity.NewMoney = Convert.IsDBNull(row["NewMoney"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["NewMoney"]);
            entity.OriginalMoney = Convert.IsDBNull(row["OriginalMoney"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["OriginalMoney"]);
            entity.TotalChangeMoney = Convert.IsDBNull(row["TotalChangeMoney"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["TotalChangeMoney"]);
            entity.SupplierChangeMoney = Convert.IsDBNull(row["SupplierChangeMoney"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["SupplierChangeMoney"]);
            entity.ConsultantAuditMoney = Convert.IsDBNull(row["ConsultantAuditMoney"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ConsultantAuditMoney"]);
            entity.ProjectAuditMoney = Convert.IsDBNull(row["ProjectAuditMoney"]) ? null : ((decimal?) row["ProjectAuditMoney"]);
            entity.ChangeReason = Convert.IsDBNull(row["ChangeReason"]) ? null : ((string) row["ChangeReason"]);
            entity.Status = Convert.IsDBNull(row["Status"]) ? null : ((int?) row["Status"]);
            entity.ChangePerson = Convert.IsDBNull(row["ChangePerson"]) ? null : ((string) row["ChangePerson"]);
            entity.ChangeDate = Convert.IsDBNull(row["ChangeDate"]) ? null : ((DateTime?) row["ChangeDate"]);
            entity.ChangeType = Convert.IsDBNull(row["ChangeType"]) ? null : ((string) row["ChangeType"]);
            entity.CheckPerson = Convert.IsDBNull(row["CheckPerson"]) ? null : ((string) row["CheckPerson"]);
            entity.CheckDate = Convert.IsDBNull(row["CheckDate"]) ? null : ((DateTime?) row["CheckDate"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, ContractChange entity)
        {
            if (reader.Read())
            {
                decimal? nullable;
                entity.ContractChangeCode = (string) reader["ContractChangeCode"];
                entity.OriginalContractChangeCode = (string) reader["ContractChangeCode"];
                entity.ContractChangeId = reader.IsDBNull(reader.GetOrdinal("ContractChangeId")) ? null : ((string) reader["ContractChangeId"]);
                entity.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                entity.Voucher = reader.IsDBNull(reader.GetOrdinal("Voucher")) ? null : ((string) reader["Voucher"]);
                entity.Money = reader.IsDBNull(reader.GetOrdinal("Money")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["Money"]);
                entity.ChangeMoney = reader.IsDBNull(reader.GetOrdinal("ChangeMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ChangeMoney"]);
                entity.NewMoney = reader.IsDBNull(reader.GetOrdinal("NewMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["NewMoney"]);
                entity.OriginalMoney = reader.IsDBNull(reader.GetOrdinal("OriginalMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["OriginalMoney"]);
                entity.TotalChangeMoney = reader.IsDBNull(reader.GetOrdinal("TotalChangeMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["TotalChangeMoney"]);
                entity.SupplierChangeMoney = reader.IsDBNull(reader.GetOrdinal("SupplierChangeMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["SupplierChangeMoney"]);
                entity.ConsultantAuditMoney = reader.IsDBNull(reader.GetOrdinal("ConsultantAuditMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ConsultantAuditMoney"]);
                entity.ProjectAuditMoney = reader.IsDBNull(reader.GetOrdinal("ProjectAuditMoney")) ? null : ((decimal?) reader["ProjectAuditMoney"]);
                entity.ChangeReason = reader.IsDBNull(reader.GetOrdinal("ChangeReason")) ? null : ((string) reader["ChangeReason"]);
                entity.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : ((int?) reader["Status"]);
                entity.ChangePerson = reader.IsDBNull(reader.GetOrdinal("ChangePerson")) ? null : ((string) reader["ChangePerson"]);
                entity.ChangeDate = reader.IsDBNull(reader.GetOrdinal("ChangeDate")) ? null : ((DateTime?) reader["ChangeDate"]);
                entity.ChangeType = reader.IsDBNull(reader.GetOrdinal("ChangeType")) ? null : ((string) reader["ChangeType"]);
                entity.CheckPerson = reader.IsDBNull(reader.GetOrdinal("CheckPerson")) ? null : ((string) reader["CheckPerson"]);
                entity.CheckDate = reader.IsDBNull(reader.GetOrdinal("CheckDate")) ? null : ((DateTime?) reader["CheckDate"]);
                entity.AcceptChanges();
            }
        }
    }
}

