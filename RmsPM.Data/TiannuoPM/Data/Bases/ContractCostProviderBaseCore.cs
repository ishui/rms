namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class ContractCostProviderBaseCore : EntityProviderBase<ContractCost, ContractCostKey>
    {
        protected ContractCostProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, ContractCost entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
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
                if (base.CanDeepLoad(entity, "List<PaymentItem>", "PaymentItemCollection", deepLoadType, innerList))
                {
                    entity.PaymentItemCollection = DataRepository.PaymentItemProvider.GetByContractCostCode(transactionManager, entity.ContractCostCode);
                    if (deep && (entity.PaymentItemCollection.Count > 0))
                    {
                        DataRepository.PaymentItemProvider.DeepLoad(transactionManager, entity.PaymentItemCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, ContractCost entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
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
            if (base.CanDeepSave(entity, "List<PaymentItem>", "PaymentItemCollection", deepSaveType, innerList))
            {
                foreach (PaymentItem item in entity.PaymentItemCollection)
                {
                    item.ContractCostCode = entity.ContractCostCode;
                }
                if ((entity.PaymentItemCollection.Count > 0) || (entity.PaymentItemCollection.DeletedItems.Count > 0))
                {
                    DataRepository.PaymentItemProvider.DeepSave(transactionManager, entity.PaymentItemCollection, deepSaveType, childTypes, innerList);
                }
            }
            return true;
        }

        public bool Delete(string contractCostCode)
        {
            return this.Delete(null, contractCostCode);
        }

        public abstract bool Delete(TransactionManager transactionManager, string contractCostCode);
        public override bool Delete(TransactionManager transactionManager, ContractCostKey key)
        {
            return this.Delete(transactionManager, key.ContractCostCode);
        }

        public static TList<ContractCost> Fill(IDataReader reader, TList<ContractCost> rows, int start, int pageLength)
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
                ContractCost item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "ContractCost" + (reader.IsDBNull(reader.GetOrdinal("ContractCostCode")) ? string.Empty : ((string) reader["ContractCostCode"])).ToString();
                    item = EntityManager.LocateOrCreate<ContractCost>(text.ToString(), "ContractCost", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new ContractCost();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    decimal? nullable;
                    item.SuppressEntityEvents = true;
                    item.ContractCostCode = (string) reader["ContractCostCode"];
                    item.OriginalContractCostCode = item.ContractCostCode;
                    item.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                    item.CostCode = reader.IsDBNull(reader.GetOrdinal("CostCode")) ? null : ((string) reader["CostCode"]);
                    item.Amount = reader.IsDBNull(reader.GetOrdinal("Amount")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["Amount"]);
                    item.Money = reader.IsDBNull(reader.GetOrdinal("Money")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["Money"]);
                    item.UnitPrise = reader.IsDBNull(reader.GetOrdinal("UnitPrise")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["UnitPrise"]);
                    item.Moneycash = reader.IsDBNull(reader.GetOrdinal("Moneycash")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["Moneycash"]);
                    item.OriginalMoneycash = reader.IsDBNull(reader.GetOrdinal("OriginalMoneycash")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["OriginalMoneycash"]);
                    item.MoneyType = reader.IsDBNull(reader.GetOrdinal("MoneyType")) ? null : ((string) reader["MoneyType"]);
                    item.ExchangeRate = reader.IsDBNull(reader.GetOrdinal("ExchangeRate")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ExchangeRate"]);
                    item.CostBudgetSetCode = reader.IsDBNull(reader.GetOrdinal("CostBudgetSetCode")) ? null : ((string) reader["CostBudgetSetCode"]);
                    item.Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : ((string) reader["Description"]);
                    item.OriginalMoney = reader.IsDBNull(reader.GetOrdinal("OriginalMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["OriginalMoney"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override ContractCost Get(TransactionManager transactionManager, ContractCostKey key, int start, int pageLength)
        {
            return this.GetByContractCostCode(transactionManager, key.ContractCostCode, start, pageLength);
        }

        public TList<ContractCost> GetByContractCode(string contractCode)
        {
            int count = -1;
            return this.GetByContractCode(contractCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractCost> GetByContractCode(TransactionManager transactionManager, string contractCode)
        {
            int count = -1;
            return this.GetByContractCode(transactionManager, contractCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractCost> GetByContractCode(string contractCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCode(null, contractCode, start, pageLength, out count);
        }

        public TList<ContractCost> GetByContractCode(string contractCode, int start, int pageLength, out int count)
        {
            return this.GetByContractCode(null, contractCode, start, pageLength, out count);
        }

        public TList<ContractCost> GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCode(transactionManager, contractCode, start, pageLength, out count);
        }

        public abstract TList<ContractCost> GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength, out int count);
        public ContractCost GetByContractCostCode(string contractCostCode)
        {
            int count = -1;
            return this.GetByContractCostCode(null, contractCostCode, 0, 0x7fffffff, out count);
        }

        public ContractCost GetByContractCostCode(TransactionManager transactionManager, string contractCostCode)
        {
            int count = -1;
            return this.GetByContractCostCode(transactionManager, contractCostCode, 0, 0x7fffffff, out count);
        }

        public ContractCost GetByContractCostCode(string contractCostCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCostCode(null, contractCostCode, start, pageLength, out count);
        }

        public ContractCost GetByContractCostCode(string contractCostCode, int start, int pageLength, out int count)
        {
            return this.GetByContractCostCode(null, contractCostCode, start, pageLength, out count);
        }

        public ContractCost GetByContractCostCode(TransactionManager transactionManager, string contractCostCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCostCode(transactionManager, contractCostCode, start, pageLength, out count);
        }

        public abstract ContractCost GetByContractCostCode(TransactionManager transactionManager, string contractCostCode, int start, int pageLength, out int count);
        public TList<ContractCost> GetByCostCode(string costCode)
        {
            int count = -1;
            return this.GetByCostCode(costCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractCost> GetByCostCode(TransactionManager transactionManager, string costCode)
        {
            int count = -1;
            return this.GetByCostCode(transactionManager, costCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractCost> GetByCostCode(string costCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByCostCode(null, costCode, start, pageLength, out count);
        }

        public TList<ContractCost> GetByCostCode(string costCode, int start, int pageLength, out int count)
        {
            return this.GetByCostCode(null, costCode, start, pageLength, out count);
        }

        public TList<ContractCost> GetByCostCode(TransactionManager transactionManager, string costCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByCostCode(transactionManager, costCode, start, pageLength, out count);
        }

        public abstract TList<ContractCost> GetByCostCode(TransactionManager transactionManager, string costCode, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, ContractCost entity)
        {
            decimal? nullable;
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.ContractCostCode = (string) row["ContractCostCode"];
            entity.OriginalContractCostCode = (string) row["ContractCostCode"];
            entity.ContractCode = Convert.IsDBNull(row["ContractCode"]) ? null : ((string) row["ContractCode"]);
            entity.CostCode = Convert.IsDBNull(row["CostCode"]) ? null : ((string) row["CostCode"]);
            entity.Amount = Convert.IsDBNull(row["Amount"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["Amount"]);
            entity.Money = Convert.IsDBNull(row["Money"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["Money"]);
            entity.UnitPrise = Convert.IsDBNull(row["UnitPrise"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["UnitPrise"]);
            entity.Moneycash = Convert.IsDBNull(row["Moneycash"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["Moneycash"]);
            entity.OriginalMoneycash = Convert.IsDBNull(row["OriginalMoneycash"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["OriginalMoneycash"]);
            entity.MoneyType = Convert.IsDBNull(row["MoneyType"]) ? null : ((string) row["MoneyType"]);
            entity.ExchangeRate = Convert.IsDBNull(row["ExchangeRate"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ExchangeRate"]);
            entity.CostBudgetSetCode = Convert.IsDBNull(row["CostBudgetSetCode"]) ? null : ((string) row["CostBudgetSetCode"]);
            entity.Description = Convert.IsDBNull(row["Description"]) ? null : ((string) row["Description"]);
            entity.OriginalMoney = Convert.IsDBNull(row["OriginalMoney"]) ? null : ((decimal?) row["OriginalMoney"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, ContractCost entity)
        {
            if (reader.Read())
            {
                decimal? nullable;
                entity.ContractCostCode = (string) reader["ContractCostCode"];
                entity.OriginalContractCostCode = (string) reader["ContractCostCode"];
                entity.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                entity.CostCode = reader.IsDBNull(reader.GetOrdinal("CostCode")) ? null : ((string) reader["CostCode"]);
                entity.Amount = reader.IsDBNull(reader.GetOrdinal("Amount")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["Amount"]);
                entity.Money = reader.IsDBNull(reader.GetOrdinal("Money")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["Money"]);
                entity.UnitPrise = reader.IsDBNull(reader.GetOrdinal("UnitPrise")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["UnitPrise"]);
                entity.Moneycash = reader.IsDBNull(reader.GetOrdinal("Moneycash")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["Moneycash"]);
                entity.OriginalMoneycash = reader.IsDBNull(reader.GetOrdinal("OriginalMoneycash")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["OriginalMoneycash"]);
                entity.MoneyType = reader.IsDBNull(reader.GetOrdinal("MoneyType")) ? null : ((string) reader["MoneyType"]);
                entity.ExchangeRate = reader.IsDBNull(reader.GetOrdinal("ExchangeRate")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ExchangeRate"]);
                entity.CostBudgetSetCode = reader.IsDBNull(reader.GetOrdinal("CostBudgetSetCode")) ? null : ((string) reader["CostBudgetSetCode"]);
                entity.Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : ((string) reader["Description"]);
                entity.OriginalMoney = reader.IsDBNull(reader.GetOrdinal("OriginalMoney")) ? null : ((decimal?) reader["OriginalMoney"]);
                entity.AcceptChanges();
            }
        }
    }
}

