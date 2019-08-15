namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class ContractCostPlanProviderBaseCore : EntityProviderBase<ContractCostPlan, ContractCostPlanKey>
    {
        protected ContractCostPlanProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, ContractCostPlan entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
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

        internal override bool DeepSave(TransactionManager transactionManager, ContractCostPlan entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
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

        public bool Delete(string contractCostPlanCode)
        {
            return this.Delete(null, contractCostPlanCode);
        }

        public abstract bool Delete(TransactionManager transactionManager, string contractCostPlanCode);
        public override bool Delete(TransactionManager transactionManager, ContractCostPlanKey key)
        {
            return this.Delete(transactionManager, key.ContractCostPlanCode);
        }

        public static TList<ContractCostPlan> Fill(IDataReader reader, TList<ContractCostPlan> rows, int start, int pageLength)
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
                ContractCostPlan item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "ContractCostPlan" + (reader.IsDBNull(reader.GetOrdinal("ContractCostPlanCode")) ? string.Empty : ((string) reader["ContractCostPlanCode"])).ToString();
                    item = EntityManager.LocateOrCreate<ContractCostPlan>(text.ToString(), "ContractCostPlan", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new ContractCostPlan();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    item.SuppressEntityEvents = true;
                    item.ContractCostPlanCode = (string) reader["ContractCostPlanCode"];
                    item.OriginalContractCostPlanCode = item.ContractCostPlanCode;
                    item.ContractCostCode = reader.IsDBNull(reader.GetOrdinal("ContractCostCode")) ? null : ((string) reader["ContractCostCode"]);
                    item.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                    item.Money = reader.IsDBNull(reader.GetOrdinal("Money")) ? null : ((decimal?) reader["Money"]);
                    item.PlanningPayDate = reader.IsDBNull(reader.GetOrdinal("PlanningPayDate")) ? null : ((DateTime?) reader["PlanningPayDate"]);
                    item.PayConditionText = reader.IsDBNull(reader.GetOrdinal("PayConditionText")) ? null : ((string) reader["PayConditionText"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override ContractCostPlan Get(TransactionManager transactionManager, ContractCostPlanKey key, int start, int pageLength)
        {
            return this.GetByContractCostPlanCode(transactionManager, key.ContractCostPlanCode, start, pageLength);
        }

        public TList<ContractCostPlan> GetByContractCode(string contractCode)
        {
            int count = -1;
            return this.GetByContractCode(contractCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractCostPlan> GetByContractCode(TransactionManager transactionManager, string contractCode)
        {
            int count = -1;
            return this.GetByContractCode(transactionManager, contractCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractCostPlan> GetByContractCode(string contractCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCode(null, contractCode, start, pageLength, out count);
        }

        public TList<ContractCostPlan> GetByContractCode(string contractCode, int start, int pageLength, out int count)
        {
            return this.GetByContractCode(null, contractCode, start, pageLength, out count);
        }

        public TList<ContractCostPlan> GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCode(transactionManager, contractCode, start, pageLength, out count);
        }

        public abstract TList<ContractCostPlan> GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength, out int count);
        public ContractCostPlan GetByContractCostPlanCode(string contractCostPlanCode)
        {
            int count = -1;
            return this.GetByContractCostPlanCode(null, contractCostPlanCode, 0, 0x7fffffff, out count);
        }

        public ContractCostPlan GetByContractCostPlanCode(TransactionManager transactionManager, string contractCostPlanCode)
        {
            int count = -1;
            return this.GetByContractCostPlanCode(transactionManager, contractCostPlanCode, 0, 0x7fffffff, out count);
        }

        public ContractCostPlan GetByContractCostPlanCode(string contractCostPlanCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCostPlanCode(null, contractCostPlanCode, start, pageLength, out count);
        }

        public ContractCostPlan GetByContractCostPlanCode(string contractCostPlanCode, int start, int pageLength, out int count)
        {
            return this.GetByContractCostPlanCode(null, contractCostPlanCode, start, pageLength, out count);
        }

        public ContractCostPlan GetByContractCostPlanCode(TransactionManager transactionManager, string contractCostPlanCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCostPlanCode(transactionManager, contractCostPlanCode, start, pageLength, out count);
        }

        public abstract ContractCostPlan GetByContractCostPlanCode(TransactionManager transactionManager, string contractCostPlanCode, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, ContractCostPlan entity)
        {
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.ContractCostPlanCode = (string) row["ContractCostPlanCode"];
            entity.OriginalContractCostPlanCode = (string) row["ContractCostPlanCode"];
            entity.ContractCostCode = Convert.IsDBNull(row["ContractCostCode"]) ? null : ((string) row["ContractCostCode"]);
            entity.ContractCode = Convert.IsDBNull(row["ContractCode"]) ? null : ((string) row["ContractCode"]);
            entity.Money = Convert.IsDBNull(row["Money"]) ? null : ((decimal?) row["Money"]);
            entity.PlanningPayDate = Convert.IsDBNull(row["PlanningPayDate"]) ? null : ((DateTime?) row["PlanningPayDate"]);
            entity.PayConditionText = Convert.IsDBNull(row["PayConditionText"]) ? null : ((string) row["PayConditionText"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, ContractCostPlan entity)
        {
            if (reader.Read())
            {
                entity.ContractCostPlanCode = (string) reader["ContractCostPlanCode"];
                entity.OriginalContractCostPlanCode = (string) reader["ContractCostPlanCode"];
                entity.ContractCostCode = reader.IsDBNull(reader.GetOrdinal("ContractCostCode")) ? null : ((string) reader["ContractCostCode"]);
                entity.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                entity.Money = reader.IsDBNull(reader.GetOrdinal("Money")) ? null : ((decimal?) reader["Money"]);
                entity.PlanningPayDate = reader.IsDBNull(reader.GetOrdinal("PlanningPayDate")) ? null : ((DateTime?) reader["PlanningPayDate"]);
                entity.PayConditionText = reader.IsDBNull(reader.GetOrdinal("PayConditionText")) ? null : ((string) reader["PayConditionText"]);
                entity.AcceptChanges();
            }
        }
    }
}

