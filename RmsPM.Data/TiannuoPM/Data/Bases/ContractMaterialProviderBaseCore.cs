namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class ContractMaterialProviderBaseCore : EntityProviderBase<ContractMaterial, ContractMaterialKey>
    {
        protected ContractMaterialProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, ContractMaterial entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity != null)
            {
                object[] pkItems;
                if (base.CanDeepLoad(entity, "Material", "MaterialCodeSource", deepLoadType, innerList) && (entity.MaterialCodeSource == null))
                {
                    pkItems = new object[1];
                    int? materialCode = entity.MaterialCode;
                    pkItems[0] = materialCode.HasValue ? materialCode.GetValueOrDefault() : 0;
                    Material material = EntityManager.LocateEntity<Material>(EntityLocator.ConstructKeyFromPkItems(typeof(Material), pkItems), DataRepository.Provider.EnableEntityTracking);
                    if (material != null)
                    {
                        entity.MaterialCodeSource = material;
                    }
                    else
                    {
                        materialCode = entity.MaterialCode;
                        entity.MaterialCodeSource = DataRepository.MaterialProvider.GetByMaterialCode(materialCode.HasValue ? materialCode.GetValueOrDefault() : 0);
                    }
                    if (deep && (entity.MaterialCodeSource != null))
                    {
                        DataRepository.MaterialProvider.DeepLoad(transactionManager, entity.MaterialCodeSource, deep, deepLoadType, childTypes, innerList);
                    }
                }
                if (base.CanDeepLoad(entity, "Contract", "ContractCodeSource", deepLoadType, innerList) && (entity.ContractCodeSource == null))
                {
                    pkItems = new object[] { entity.ContractCode ?? string.Empty };
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
                if (base.CanDeepLoad(entity, "List<ContractMaterialPlan>", "ContractMaterialPlanCollection", deepLoadType, innerList))
                {
                    entity.ContractMaterialPlanCollection = DataRepository.ContractMaterialPlanProvider.GetByContractMaterialCode(transactionManager, entity.ContractMaterialCode);
                    if (deep && (entity.ContractMaterialPlanCollection.Count > 0))
                    {
                        DataRepository.ContractMaterialPlanProvider.DeepLoad(transactionManager, entity.ContractMaterialPlanCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, ContractMaterial entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity == null)
            {
                return false;
            }
            if (base.CanDeepSave(entity, "Material", "MaterialCodeSource", deepSaveType, innerList) && (entity.MaterialCodeSource != null))
            {
                DataRepository.MaterialProvider.Save(transactionManager, entity.MaterialCodeSource);
                entity.MaterialCode = new int?(entity.MaterialCodeSource.MaterialCode);
            }
            if (base.CanDeepSave(entity, "Contract", "ContractCodeSource", deepSaveType, innerList) && (entity.ContractCodeSource != null))
            {
                DataRepository.ContractProvider.Save(transactionManager, entity.ContractCodeSource);
                entity.ContractCode = entity.ContractCodeSource.ContractCode;
            }
            this.Save(transactionManager, entity);
            if (base.CanDeepSave(entity, "List<ContractMaterialPlan>", "ContractMaterialPlanCollection", deepSaveType, innerList))
            {
                foreach (ContractMaterialPlan plan in entity.ContractMaterialPlanCollection)
                {
                    plan.ContractMaterialCode = entity.ContractMaterialCode;
                }
                if ((entity.ContractMaterialPlanCollection.Count > 0) || (entity.ContractMaterialPlanCollection.DeletedItems.Count > 0))
                {
                    DataRepository.ContractMaterialPlanProvider.DeepSave(transactionManager, entity.ContractMaterialPlanCollection, deepSaveType, childTypes, innerList);
                }
            }
            return true;
        }

        public bool Delete(string contractMaterialCode)
        {
            return this.Delete(null, contractMaterialCode);
        }

        public abstract bool Delete(TransactionManager transactionManager, string contractMaterialCode);
        public override bool Delete(TransactionManager transactionManager, ContractMaterialKey key)
        {
            return this.Delete(transactionManager, key.ContractMaterialCode);
        }

        public static TList<ContractMaterial> Fill(IDataReader reader, TList<ContractMaterial> rows, int start, int pageLength)
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
                ContractMaterial item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "ContractMaterial" + (reader.IsDBNull(reader.GetOrdinal("ContractMaterialCode")) ? string.Empty : ((string) reader["ContractMaterialCode"])).ToString();
                    item = EntityManager.LocateOrCreate<ContractMaterial>(text.ToString(), "ContractMaterial", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new ContractMaterial();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    decimal? nullable2;
                    item.SuppressEntityEvents = true;
                    item.ContractMaterialCode = (string) reader["ContractMaterialCode"];
                    item.OriginalContractMaterialCode = item.ContractMaterialCode;
                    item.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                    item.MaterialCode = reader.IsDBNull(reader.GetOrdinal("MaterialCode")) ? null : ((int?) reader["MaterialCode"]);
                    item.Qty = reader.IsDBNull(reader.GetOrdinal("Qty")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["Qty"]);
                    item.Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["Price"]);
                    item.Money = reader.IsDBNull(reader.GetOrdinal("Money")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["Money"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override ContractMaterial Get(TransactionManager transactionManager, ContractMaterialKey key, int start, int pageLength)
        {
            return this.GetByContractMaterialCode(transactionManager, key.ContractMaterialCode, start, pageLength);
        }

        public TList<ContractMaterial> GetByContractCode(string contractCode)
        {
            int count = -1;
            return this.GetByContractCode(contractCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractMaterial> GetByContractCode(TransactionManager transactionManager, string contractCode)
        {
            int count = -1;
            return this.GetByContractCode(transactionManager, contractCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractMaterial> GetByContractCode(string contractCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCode(null, contractCode, start, pageLength, out count);
        }

        public TList<ContractMaterial> GetByContractCode(string contractCode, int start, int pageLength, out int count)
        {
            return this.GetByContractCode(null, contractCode, start, pageLength, out count);
        }

        public TList<ContractMaterial> GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCode(transactionManager, contractCode, start, pageLength, out count);
        }

        public abstract TList<ContractMaterial> GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength, out int count);
        public ContractMaterial GetByContractMaterialCode(string contractMaterialCode)
        {
            int count = -1;
            return this.GetByContractMaterialCode(null, contractMaterialCode, 0, 0x7fffffff, out count);
        }

        public ContractMaterial GetByContractMaterialCode(TransactionManager transactionManager, string contractMaterialCode)
        {
            int count = -1;
            return this.GetByContractMaterialCode(transactionManager, contractMaterialCode, 0, 0x7fffffff, out count);
        }

        public ContractMaterial GetByContractMaterialCode(string contractMaterialCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractMaterialCode(null, contractMaterialCode, start, pageLength, out count);
        }

        public ContractMaterial GetByContractMaterialCode(string contractMaterialCode, int start, int pageLength, out int count)
        {
            return this.GetByContractMaterialCode(null, contractMaterialCode, start, pageLength, out count);
        }

        public ContractMaterial GetByContractMaterialCode(TransactionManager transactionManager, string contractMaterialCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractMaterialCode(transactionManager, contractMaterialCode, start, pageLength, out count);
        }

        public abstract ContractMaterial GetByContractMaterialCode(TransactionManager transactionManager, string contractMaterialCode, int start, int pageLength, out int count);
        public TList<ContractMaterial> GetByMaterialCode(int? materialCode)
        {
            int count = -1;
            return this.GetByMaterialCode(materialCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractMaterial> GetByMaterialCode(TransactionManager transactionManager, int? materialCode)
        {
            int count = -1;
            return this.GetByMaterialCode(transactionManager, materialCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractMaterial> GetByMaterialCode(int? materialCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByMaterialCode(null, materialCode, start, pageLength, out count);
        }

        public TList<ContractMaterial> GetByMaterialCode(int? materialCode, int start, int pageLength, out int count)
        {
            return this.GetByMaterialCode(null, materialCode, start, pageLength, out count);
        }

        public TList<ContractMaterial> GetByMaterialCode(TransactionManager transactionManager, int? materialCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByMaterialCode(transactionManager, materialCode, start, pageLength, out count);
        }

        public abstract TList<ContractMaterial> GetByMaterialCode(TransactionManager transactionManager, int? materialCode, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, ContractMaterial entity)
        {
            decimal? nullable2;
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.ContractMaterialCode = (string) row["ContractMaterialCode"];
            entity.OriginalContractMaterialCode = (string) row["ContractMaterialCode"];
            entity.ContractCode = Convert.IsDBNull(row["ContractCode"]) ? null : ((string) row["ContractCode"]);
            entity.MaterialCode = Convert.IsDBNull(row["MaterialCode"]) ? null : ((int?) row["MaterialCode"]);
            entity.Qty = Convert.IsDBNull(row["Qty"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["Qty"]);
            entity.Price = Convert.IsDBNull(row["Price"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["Price"]);
            entity.Money = Convert.IsDBNull(row["Money"]) ? null : ((decimal?) row["Money"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, ContractMaterial entity)
        {
            if (reader.Read())
            {
                decimal? nullable2;
                entity.ContractMaterialCode = (string) reader["ContractMaterialCode"];
                entity.OriginalContractMaterialCode = (string) reader["ContractMaterialCode"];
                entity.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                entity.MaterialCode = reader.IsDBNull(reader.GetOrdinal("MaterialCode")) ? null : ((int?) reader["MaterialCode"]);
                entity.Qty = reader.IsDBNull(reader.GetOrdinal("Qty")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["Qty"]);
                entity.Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["Price"]);
                entity.Money = reader.IsDBNull(reader.GetOrdinal("Money")) ? null : ((decimal?) reader["Money"]);
                entity.AcceptChanges();
            }
        }
    }
}

