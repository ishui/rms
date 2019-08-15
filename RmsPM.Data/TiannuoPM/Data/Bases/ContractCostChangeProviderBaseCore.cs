namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class ContractCostChangeProviderBaseCore : EntityProviderBase<ContractCostChange, ContractCostChangeKey>
    {
        protected ContractCostChangeProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, ContractCostChange entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
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

        internal override bool DeepSave(TransactionManager transactionManager, ContractCostChange entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
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

        public bool Delete(string contractCostChangeCode)
        {
            return this.Delete(null, contractCostChangeCode);
        }

        public abstract bool Delete(TransactionManager transactionManager, string contractCostChangeCode);
        public override bool Delete(TransactionManager transactionManager, ContractCostChangeKey key)
        {
            return this.Delete(transactionManager, key.ContractCostChangeCode);
        }

        public static TList<ContractCostChange> Fill(IDataReader reader, TList<ContractCostChange> rows, int start, int pageLength)
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
                ContractCostChange item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "ContractCostChange" + (reader.IsDBNull(reader.GetOrdinal("ContractCostChangeCode")) ? string.Empty : ((string) reader["ContractCostChangeCode"])).ToString();
                    item = EntityManager.LocateOrCreate<ContractCostChange>(text.ToString(), "ContractCostChange", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new ContractCostChange();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    decimal? nullable;
                    item.SuppressEntityEvents = true;
                    item.ContractCostChangeCode = (string) reader["ContractCostChangeCode"];
                    item.OriginalContractCostChangeCode = item.ContractCostChangeCode;
                    item.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                    item.ContractCostCode = reader.IsDBNull(reader.GetOrdinal("ContractCostCode")) ? null : ((string) reader["ContractCostCode"]);
                    item.ContractChangeCode = reader.IsDBNull(reader.GetOrdinal("ContractChangeCode")) ? null : ((string) reader["ContractChangeCode"]);
                    item.Cash = reader.IsDBNull(reader.GetOrdinal("Cash")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["Cash"]);
                    item.ChangeCash = reader.IsDBNull(reader.GetOrdinal("ChangeCash")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ChangeCash"]);
                    item.NewCash = reader.IsDBNull(reader.GetOrdinal("NewCash")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["NewCash"]);
                    item.OriginalCash = reader.IsDBNull(reader.GetOrdinal("OriginalCash")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["OriginalCash"]);
                    item.TotalChangeCash = reader.IsDBNull(reader.GetOrdinal("TotalChangeCash")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["TotalChangeCash"]);
                    item.MoneyType = reader.IsDBNull(reader.GetOrdinal("MoneyType")) ? null : ((string) reader["MoneyType"]);
                    item.ExchangeRate = reader.IsDBNull(reader.GetOrdinal("ExchangeRate")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ExchangeRate"]);
                    item.Money = reader.IsDBNull(reader.GetOrdinal("Money")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["Money"]);
                    item.ChangeMoney = reader.IsDBNull(reader.GetOrdinal("ChangeMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ChangeMoney"]);
                    item.NewMoney = reader.IsDBNull(reader.GetOrdinal("NewMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["NewMoney"]);
                    item.OriginalMoney = reader.IsDBNull(reader.GetOrdinal("OriginalMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["OriginalMoney"]);
                    item.TotalChangeMoney = reader.IsDBNull(reader.GetOrdinal("TotalChangeMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["TotalChangeMoney"]);
                    item.CostCode = reader.IsDBNull(reader.GetOrdinal("CostCode")) ? null : ((string) reader["CostCode"]);
                    item.CostBudgetSetCode = reader.IsDBNull(reader.GetOrdinal("CostBudgetSetCode")) ? null : ((string) reader["CostBudgetSetCode"]);
                    item.Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : ((string) reader["Description"]);
                    item.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : ((int?) reader["Status"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override ContractCostChange Get(TransactionManager transactionManager, ContractCostChangeKey key, int start, int pageLength)
        {
            return this.GetByContractCostChangeCode(transactionManager, key.ContractCostChangeCode, start, pageLength);
        }

        public TList<ContractCostChange> GetByContractChangeCode(string contractChangeCode)
        {
            int count = -1;
            return this.GetByContractChangeCode(contractChangeCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractCostChange> GetByContractChangeCode(TransactionManager transactionManager, string contractChangeCode)
        {
            int count = -1;
            return this.GetByContractChangeCode(transactionManager, contractChangeCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractCostChange> GetByContractChangeCode(string contractChangeCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractChangeCode(null, contractChangeCode, start, pageLength, out count);
        }

        public TList<ContractCostChange> GetByContractChangeCode(string contractChangeCode, int start, int pageLength, out int count)
        {
            return this.GetByContractChangeCode(null, contractChangeCode, start, pageLength, out count);
        }

        public TList<ContractCostChange> GetByContractChangeCode(TransactionManager transactionManager, string contractChangeCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractChangeCode(transactionManager, contractChangeCode, start, pageLength, out count);
        }

        public abstract TList<ContractCostChange> GetByContractChangeCode(TransactionManager transactionManager, string contractChangeCode, int start, int pageLength, out int count);
        public ContractCostChange GetByContractCostChangeCode(string contractCostChangeCode)
        {
            int count = -1;
            return this.GetByContractCostChangeCode(null, contractCostChangeCode, 0, 0x7fffffff, out count);
        }

        public ContractCostChange GetByContractCostChangeCode(TransactionManager transactionManager, string contractCostChangeCode)
        {
            int count = -1;
            return this.GetByContractCostChangeCode(transactionManager, contractCostChangeCode, 0, 0x7fffffff, out count);
        }

        public ContractCostChange GetByContractCostChangeCode(string contractCostChangeCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCostChangeCode(null, contractCostChangeCode, start, pageLength, out count);
        }

        public ContractCostChange GetByContractCostChangeCode(string contractCostChangeCode, int start, int pageLength, out int count)
        {
            return this.GetByContractCostChangeCode(null, contractCostChangeCode, start, pageLength, out count);
        }

        public ContractCostChange GetByContractCostChangeCode(TransactionManager transactionManager, string contractCostChangeCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCostChangeCode(transactionManager, contractCostChangeCode, start, pageLength, out count);
        }

        public abstract ContractCostChange GetByContractCostChangeCode(TransactionManager transactionManager, string contractCostChangeCode, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, ContractCostChange entity)
        {
            decimal? nullable;
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.ContractCostChangeCode = (string) row["ContractCostChangeCode"];
            entity.OriginalContractCostChangeCode = (string) row["ContractCostChangeCode"];
            entity.ContractCode = Convert.IsDBNull(row["ContractCode"]) ? null : ((string) row["ContractCode"]);
            entity.ContractCostCode = Convert.IsDBNull(row["ContractCostCode"]) ? null : ((string) row["ContractCostCode"]);
            entity.ContractChangeCode = Convert.IsDBNull(row["ContractChangeCode"]) ? null : ((string) row["ContractChangeCode"]);
            entity.Cash = Convert.IsDBNull(row["Cash"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["Cash"]);
            entity.ChangeCash = Convert.IsDBNull(row["ChangeCash"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ChangeCash"]);
            entity.NewCash = Convert.IsDBNull(row["NewCash"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["NewCash"]);
            entity.OriginalCash = Convert.IsDBNull(row["OriginalCash"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["OriginalCash"]);
            entity.TotalChangeCash = Convert.IsDBNull(row["TotalChangeCash"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["TotalChangeCash"]);
            entity.MoneyType = Convert.IsDBNull(row["MoneyType"]) ? null : ((string) row["MoneyType"]);
            entity.ExchangeRate = Convert.IsDBNull(row["ExchangeRate"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ExchangeRate"]);
            entity.Money = Convert.IsDBNull(row["Money"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["Money"]);
            entity.ChangeMoney = Convert.IsDBNull(row["ChangeMoney"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ChangeMoney"]);
            entity.NewMoney = Convert.IsDBNull(row["NewMoney"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["NewMoney"]);
            entity.OriginalMoney = Convert.IsDBNull(row["OriginalMoney"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["OriginalMoney"]);
            entity.TotalChangeMoney = Convert.IsDBNull(row["TotalChangeMoney"]) ? null : ((decimal?) row["TotalChangeMoney"]);
            entity.CostCode = Convert.IsDBNull(row["CostCode"]) ? null : ((string) row["CostCode"]);
            entity.CostBudgetSetCode = Convert.IsDBNull(row["CostBudgetSetCode"]) ? null : ((string) row["CostBudgetSetCode"]);
            entity.Description = Convert.IsDBNull(row["Description"]) ? null : ((string) row["Description"]);
            entity.Status = Convert.IsDBNull(row["Status"]) ? null : ((int?) row["Status"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, ContractCostChange entity)
        {
            if (reader.Read())
            {
                decimal? nullable;
                entity.ContractCostChangeCode = (string) reader["ContractCostChangeCode"];
                entity.OriginalContractCostChangeCode = (string) reader["ContractCostChangeCode"];
                entity.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                entity.ContractCostCode = reader.IsDBNull(reader.GetOrdinal("ContractCostCode")) ? null : ((string) reader["ContractCostCode"]);
                entity.ContractChangeCode = reader.IsDBNull(reader.GetOrdinal("ContractChangeCode")) ? null : ((string) reader["ContractChangeCode"]);
                entity.Cash = reader.IsDBNull(reader.GetOrdinal("Cash")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["Cash"]);
                entity.ChangeCash = reader.IsDBNull(reader.GetOrdinal("ChangeCash")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ChangeCash"]);
                entity.NewCash = reader.IsDBNull(reader.GetOrdinal("NewCash")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["NewCash"]);
                entity.OriginalCash = reader.IsDBNull(reader.GetOrdinal("OriginalCash")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["OriginalCash"]);
                entity.TotalChangeCash = reader.IsDBNull(reader.GetOrdinal("TotalChangeCash")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["TotalChangeCash"]);
                entity.MoneyType = reader.IsDBNull(reader.GetOrdinal("MoneyType")) ? null : ((string) reader["MoneyType"]);
                entity.ExchangeRate = reader.IsDBNull(reader.GetOrdinal("ExchangeRate")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ExchangeRate"]);
                entity.Money = reader.IsDBNull(reader.GetOrdinal("Money")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["Money"]);
                entity.ChangeMoney = reader.IsDBNull(reader.GetOrdinal("ChangeMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ChangeMoney"]);
                entity.NewMoney = reader.IsDBNull(reader.GetOrdinal("NewMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["NewMoney"]);
                entity.OriginalMoney = reader.IsDBNull(reader.GetOrdinal("OriginalMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["OriginalMoney"]);
                entity.TotalChangeMoney = reader.IsDBNull(reader.GetOrdinal("TotalChangeMoney")) ? null : ((decimal?) reader["TotalChangeMoney"]);
                entity.CostCode = reader.IsDBNull(reader.GetOrdinal("CostCode")) ? null : ((string) reader["CostCode"]);
                entity.CostBudgetSetCode = reader.IsDBNull(reader.GetOrdinal("CostBudgetSetCode")) ? null : ((string) reader["CostBudgetSetCode"]);
                entity.Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : ((string) reader["Description"]);
                entity.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : ((int?) reader["Status"]);
                entity.AcceptChanges();
            }
        }
    }
}

