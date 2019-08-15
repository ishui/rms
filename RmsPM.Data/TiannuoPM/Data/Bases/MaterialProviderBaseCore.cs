namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class MaterialProviderBaseCore : EntityProviderBase<Material, MaterialKey>
    {
        protected MaterialProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, Material entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if ((entity != null) && base.CanDeepLoad(entity, "List<ContractMaterial>", "ContractMaterialCollection", deepLoadType, innerList))
            {
                entity.ContractMaterialCollection = DataRepository.ContractMaterialProvider.GetByMaterialCode(transactionManager, new int?(entity.MaterialCode));
                if (deep && (entity.ContractMaterialCollection.Count > 0))
                {
                    DataRepository.ContractMaterialProvider.DeepLoad(transactionManager, entity.ContractMaterialCollection, deep, deepLoadType, childTypes, innerList);
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, Material entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity == null)
            {
                return false;
            }
            this.Save(transactionManager, entity);
            if (base.CanDeepSave(entity, "List<ContractMaterial>", "ContractMaterialCollection", deepSaveType, innerList))
            {
                foreach (ContractMaterial material in entity.ContractMaterialCollection)
                {
                    material.MaterialCode = new int?(entity.MaterialCode);
                }
                if ((entity.ContractMaterialCollection.Count > 0) || (entity.ContractMaterialCollection.DeletedItems.Count > 0))
                {
                    DataRepository.ContractMaterialProvider.DeepSave(transactionManager, entity.ContractMaterialCollection, deepSaveType, childTypes, innerList);
                }
            }
            return true;
        }

        public bool Delete(int materialCode)
        {
            return this.Delete(null, materialCode);
        }

        public abstract bool Delete(TransactionManager transactionManager, int materialCode);
        public override bool Delete(TransactionManager transactionManager, MaterialKey key)
        {
            return this.Delete(transactionManager, key.MaterialCode);
        }

        public static TList<Material> Fill(IDataReader reader, TList<Material> rows, int start, int pageLength)
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
                Material item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "Material" + (reader.IsDBNull(reader.GetOrdinal("MaterialCode")) ? 0 : ((int) reader["MaterialCode"])).ToString();
                    item = EntityManager.LocateOrCreate<Material>(text.ToString(), "Material", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new Material();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    item.SuppressEntityEvents = true;
                    item.MaterialCode = (int) reader["MaterialCode"];
                    item.OriginalMaterialCode = item.MaterialCode;
                    item.MaterialName = reader.IsDBNull(reader.GetOrdinal("MaterialName")) ? null : ((string) reader["MaterialName"]);
                    item.GroupCode = reader.IsDBNull(reader.GetOrdinal("GroupCode")) ? null : ((string) reader["GroupCode"]);
                    item.Spec = reader.IsDBNull(reader.GetOrdinal("Spec")) ? null : ((string) reader["Spec"]);
                    item.Unit = reader.IsDBNull(reader.GetOrdinal("Unit")) ? null : ((string) reader["Unit"]);
                    item.StandardPrice = reader.IsDBNull(reader.GetOrdinal("StandardPrice")) ? null : ((decimal?) reader["StandardPrice"]);
                    item.InputPerson = reader.IsDBNull(reader.GetOrdinal("InputPerson")) ? null : ((string) reader["InputPerson"]);
                    item.InputDate = reader.IsDBNull(reader.GetOrdinal("InputDate")) ? null : ((DateTime?) reader["InputDate"]);
                    item.Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? null : ((string) reader["Remark"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override Material Get(TransactionManager transactionManager, MaterialKey key, int start, int pageLength)
        {
            return this.GetByMaterialCode(transactionManager, key.MaterialCode, start, pageLength);
        }

        public Material GetByMaterialCode(int materialCode)
        {
            int count = -1;
            return this.GetByMaterialCode(null, materialCode, 0, 0x7fffffff, out count);
        }

        public Material GetByMaterialCode(TransactionManager transactionManager, int materialCode)
        {
            int count = -1;
            return this.GetByMaterialCode(transactionManager, materialCode, 0, 0x7fffffff, out count);
        }

        public Material GetByMaterialCode(int materialCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByMaterialCode(null, materialCode, start, pageLength, out count);
        }

        public Material GetByMaterialCode(int materialCode, int start, int pageLength, out int count)
        {
            return this.GetByMaterialCode(null, materialCode, start, pageLength, out count);
        }

        public Material GetByMaterialCode(TransactionManager transactionManager, int materialCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByMaterialCode(transactionManager, materialCode, start, pageLength, out count);
        }

        public abstract Material GetByMaterialCode(TransactionManager transactionManager, int materialCode, int start, int pageLength, out int count);
       
        public static void RefreshEntity(DataSet dataSet, Material entity)
        {
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.MaterialCode = (int) row["MaterialCode"];
            entity.OriginalMaterialCode = (int) row["MaterialCode"];
            entity.MaterialName = Convert.IsDBNull(row["MaterialName"]) ? null : ((string) row["MaterialName"]);
            entity.GroupCode = Convert.IsDBNull(row["GroupCode"]) ? null : ((string) row["GroupCode"]);
            entity.Spec = Convert.IsDBNull(row["Spec"]) ? null : ((string) row["Spec"]);
            entity.Unit = Convert.IsDBNull(row["Unit"]) ? null : ((string) row["Unit"]);
            entity.StandardPrice = Convert.IsDBNull(row["StandardPrice"]) ? null : ((decimal?) row["StandardPrice"]);
            entity.InputPerson = Convert.IsDBNull(row["InputPerson"]) ? null : ((string) row["InputPerson"]);
            entity.InputDate = Convert.IsDBNull(row["InputDate"]) ? null : ((DateTime?) row["InputDate"]);
            entity.Remark = Convert.IsDBNull(row["Remark"]) ? null : ((string) row["Remark"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, Material entity)
        {
            if (reader.Read())
            {
                entity.MaterialCode = (int) reader["MaterialCode"];
                entity.OriginalMaterialCode = (int) reader["MaterialCode"];
                entity.MaterialName = reader.IsDBNull(reader.GetOrdinal("MaterialName")) ? null : ((string) reader["MaterialName"]);
                entity.GroupCode = reader.IsDBNull(reader.GetOrdinal("GroupCode")) ? null : ((string) reader["GroupCode"]);
                entity.Spec = reader.IsDBNull(reader.GetOrdinal("Spec")) ? null : ((string) reader["Spec"]);
                entity.Unit = reader.IsDBNull(reader.GetOrdinal("Unit")) ? null : ((string) reader["Unit"]);
                entity.StandardPrice = reader.IsDBNull(reader.GetOrdinal("StandardPrice")) ? null : ((decimal?) reader["StandardPrice"]);
                entity.InputPerson = reader.IsDBNull(reader.GetOrdinal("InputPerson")) ? null : ((string) reader["InputPerson"]);
                entity.InputDate = reader.IsDBNull(reader.GetOrdinal("InputDate")) ? null : ((DateTime?) reader["InputDate"]);
                entity.Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? null : ((string) reader["Remark"]);
                entity.AcceptChanges();
            }
        }
    }
}

