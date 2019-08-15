namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class ContractMaterialPlanProviderBaseCore : EntityProviderBase<ContractMaterialPlan, ContractMaterialPlanKey>
    {
        protected ContractMaterialPlanProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, ContractMaterialPlan entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if ((entity != null) && (base.CanDeepLoad(entity, "ContractMaterial", "ContractMaterialCodeSource", deepLoadType, innerList) && (entity.ContractMaterialCodeSource == null)))
            {
                object[] pkItems = new object[] { entity.ContractMaterialCode ?? string.Empty };
                ContractMaterial material = EntityManager.LocateEntity<ContractMaterial>(EntityLocator.ConstructKeyFromPkItems(typeof(ContractMaterial), pkItems), DataRepository.Provider.EnableEntityTracking);
                if (material != null)
                {
                    entity.ContractMaterialCodeSource = material;
                }
                else
                {
                    entity.ContractMaterialCodeSource = DataRepository.ContractMaterialProvider.GetByContractMaterialCode(entity.ContractMaterialCode ?? string.Empty);
                }
                if (deep && (entity.ContractMaterialCodeSource != null))
                {
                    DataRepository.ContractMaterialProvider.DeepLoad(transactionManager, entity.ContractMaterialCodeSource, deep, deepLoadType, childTypes, innerList);
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, ContractMaterialPlan entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity == null)
            {
                return false;
            }
            if (base.CanDeepSave(entity, "ContractMaterial", "ContractMaterialCodeSource", deepSaveType, innerList) && (entity.ContractMaterialCodeSource != null))
            {
                DataRepository.ContractMaterialProvider.Save(transactionManager, entity.ContractMaterialCodeSource);
                entity.ContractMaterialCode = entity.ContractMaterialCodeSource.ContractMaterialCode;
            }
            this.Save(transactionManager, entity);
            return true;
        }

        public bool Delete(string contractMaterialPlanCode)
        {
            return this.Delete(null, contractMaterialPlanCode);
        }

        public abstract bool Delete(TransactionManager transactionManager, string contractMaterialPlanCode);
        public override bool Delete(TransactionManager transactionManager, ContractMaterialPlanKey key)
        {
            return this.Delete(transactionManager, key.ContractMaterialPlanCode);
        }

        public static TList<ContractMaterialPlan> Fill(IDataReader reader, TList<ContractMaterialPlan> rows, int start, int pageLength)
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
                ContractMaterialPlan item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "ContractMaterialPlan" + (reader.IsDBNull(reader.GetOrdinal("ContractMaterialPlanCode")) ? string.Empty : ((string) reader["ContractMaterialPlanCode"])).ToString();
                    item = EntityManager.LocateOrCreate<ContractMaterialPlan>(text.ToString(), "ContractMaterialPlan", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new ContractMaterialPlan();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    item.SuppressEntityEvents = true;
                    item.ContractMaterialPlanCode = (string) reader["ContractMaterialPlanCode"];
                    item.OriginalContractMaterialPlanCode = item.ContractMaterialPlanCode;
                    item.ContractMaterialCode = reader.IsDBNull(reader.GetOrdinal("ContractMaterialCode")) ? null : ((string) reader["ContractMaterialCode"]);
                    item.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                    item.PlanDate = reader.IsDBNull(reader.GetOrdinal("PlanDate")) ? null : ((DateTime?) reader["PlanDate"]);
                    item.PlanQty = reader.IsDBNull(reader.GetOrdinal("PlanQty")) ? null : ((decimal?) reader["PlanQty"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override ContractMaterialPlan Get(TransactionManager transactionManager, ContractMaterialPlanKey key, int start, int pageLength)
        {
            return this.GetByContractMaterialPlanCode(transactionManager, key.ContractMaterialPlanCode, start, pageLength);
        }

        public TList<ContractMaterialPlan> GetByContractMaterialCode(string contractMaterialCode)
        {
            int count = -1;
            return this.GetByContractMaterialCode(contractMaterialCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractMaterialPlan> GetByContractMaterialCode(TransactionManager transactionManager, string contractMaterialCode)
        {
            int count = -1;
            return this.GetByContractMaterialCode(transactionManager, contractMaterialCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractMaterialPlan> GetByContractMaterialCode(string contractMaterialCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractMaterialCode(null, contractMaterialCode, start, pageLength, out count);
        }

        public TList<ContractMaterialPlan> GetByContractMaterialCode(string contractMaterialCode, int start, int pageLength, out int count)
        {
            return this.GetByContractMaterialCode(null, contractMaterialCode, start, pageLength, out count);
        }

        public TList<ContractMaterialPlan> GetByContractMaterialCode(TransactionManager transactionManager, string contractMaterialCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractMaterialCode(transactionManager, contractMaterialCode, start, pageLength, out count);
        }

        public abstract TList<ContractMaterialPlan> GetByContractMaterialCode(TransactionManager transactionManager, string contractMaterialCode, int start, int pageLength, out int count);
        public ContractMaterialPlan GetByContractMaterialPlanCode(string contractMaterialPlanCode)
        {
            int count = -1;
            return this.GetByContractMaterialPlanCode(null, contractMaterialPlanCode, 0, 0x7fffffff, out count);
        }

        public ContractMaterialPlan GetByContractMaterialPlanCode(TransactionManager transactionManager, string contractMaterialPlanCode)
        {
            int count = -1;
            return this.GetByContractMaterialPlanCode(transactionManager, contractMaterialPlanCode, 0, 0x7fffffff, out count);
        }

        public ContractMaterialPlan GetByContractMaterialPlanCode(string contractMaterialPlanCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractMaterialPlanCode(null, contractMaterialPlanCode, start, pageLength, out count);
        }

        public ContractMaterialPlan GetByContractMaterialPlanCode(string contractMaterialPlanCode, int start, int pageLength, out int count)
        {
            return this.GetByContractMaterialPlanCode(null, contractMaterialPlanCode, start, pageLength, out count);
        }

        public ContractMaterialPlan GetByContractMaterialPlanCode(TransactionManager transactionManager, string contractMaterialPlanCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractMaterialPlanCode(transactionManager, contractMaterialPlanCode, start, pageLength, out count);
        }

        public abstract ContractMaterialPlan GetByContractMaterialPlanCode(TransactionManager transactionManager, string contractMaterialPlanCode, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, ContractMaterialPlan entity)
        {
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.ContractMaterialPlanCode = (string) row["ContractMaterialPlanCode"];
            entity.OriginalContractMaterialPlanCode = (string) row["ContractMaterialPlanCode"];
            entity.ContractMaterialCode = Convert.IsDBNull(row["ContractMaterialCode"]) ? null : ((string) row["ContractMaterialCode"]);
            entity.ContractCode = Convert.IsDBNull(row["ContractCode"]) ? null : ((string) row["ContractCode"]);
            entity.PlanDate = Convert.IsDBNull(row["PlanDate"]) ? null : ((DateTime?) row["PlanDate"]);
            entity.PlanQty = Convert.IsDBNull(row["PlanQty"]) ? null : ((decimal?) row["PlanQty"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, ContractMaterialPlan entity)
        {
            if (reader.Read())
            {
                entity.ContractMaterialPlanCode = (string) reader["ContractMaterialPlanCode"];
                entity.OriginalContractMaterialPlanCode = (string) reader["ContractMaterialPlanCode"];
                entity.ContractMaterialCode = reader.IsDBNull(reader.GetOrdinal("ContractMaterialCode")) ? null : ((string) reader["ContractMaterialCode"]);
                entity.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                entity.PlanDate = reader.IsDBNull(reader.GetOrdinal("PlanDate")) ? null : ((DateTime?) reader["PlanDate"]);
                entity.PlanQty = reader.IsDBNull(reader.GetOrdinal("PlanQty")) ? null : ((decimal?) reader["PlanQty"]);
                entity.AcceptChanges();
            }
        }
    }
}

