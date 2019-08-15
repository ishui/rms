namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class MaterialPurchasProviderBaseCore : EntityProviderBase<MaterialPurchas, MaterialPurchasKey>
    {
        protected MaterialPurchasProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, MaterialPurchas entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity != null)
            {
                if (base.CanDeepLoad(entity, "Project", "ProjectCodeSource", deepLoadType, innerList) && (entity.ProjectCodeSource == null))
                {
                    object[] pkItems = new object[] { entity.ProjectCode ?? string.Empty };
                    Project project = EntityManager.LocateEntity<Project>(EntityLocator.ConstructKeyFromPkItems(typeof(Project), pkItems), DataRepository.Provider.EnableEntityTracking);
                    if (project != null)
                    {
                        entity.ProjectCodeSource = project;
                    }
                    else
                    {
                        entity.ProjectCodeSource = DataRepository.ProjectProvider.GetByProjectCode(entity.ProjectCode ?? string.Empty);
                    }
                    if (deep && (entity.ProjectCodeSource != null))
                    {
                        DataRepository.ProjectProvider.DeepLoad(transactionManager, entity.ProjectCodeSource, deep, deepLoadType, childTypes, innerList);
                    }
                }
                if (base.CanDeepLoad(entity, "List<MaterialPurchasDtl>", "MaterialPurchasDtlCollection", deepLoadType, innerList))
                {
                    entity.MaterialPurchasDtlCollection = DataRepository.MaterialPurchasDtlProvider.GetByMaterialPurchasID(transactionManager, new int?(entity.MaterialPurchasID));
                    if (deep && (entity.MaterialPurchasDtlCollection.Count > 0))
                    {
                        DataRepository.MaterialPurchasDtlProvider.DeepLoad(transactionManager, entity.MaterialPurchasDtlCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, MaterialPurchas entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity == null)
            {
                return false;
            }
            if (base.CanDeepSave(entity, "Project", "ProjectCodeSource", deepSaveType, innerList) && (entity.ProjectCodeSource != null))
            {
                DataRepository.ProjectProvider.Save(transactionManager, entity.ProjectCodeSource);
                entity.ProjectCode = entity.ProjectCodeSource.ProjectCode;
            }
            this.Save(transactionManager, entity);
            if (base.CanDeepSave(entity, "List<MaterialPurchasDtl>", "MaterialPurchasDtlCollection", deepSaveType, innerList))
            {
                foreach (MaterialPurchasDtl dtl in entity.MaterialPurchasDtlCollection)
                {
                    dtl.MaterialPurchasID = new int?(entity.MaterialPurchasID);
                }
                if ((entity.MaterialPurchasDtlCollection.Count > 0) || (entity.MaterialPurchasDtlCollection.DeletedItems.Count > 0))
                {
                    DataRepository.MaterialPurchasDtlProvider.DeepSave(transactionManager, entity.MaterialPurchasDtlCollection, deepSaveType, childTypes, innerList);
                }
            }
            return true;
        }

        public bool Delete(int materialPurchasID)
        {
            return this.Delete(null, materialPurchasID);
        }

        public abstract bool Delete(TransactionManager transactionManager, int materialPurchasID);
        public override bool Delete(TransactionManager transactionManager, MaterialPurchasKey key)
        {
            return this.Delete(transactionManager, key.MaterialPurchasID);
        }

        public static TList<MaterialPurchas> Fill(IDataReader reader, TList<MaterialPurchas> rows, int start, int pageLength)
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
                MaterialPurchas item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "MaterialPurchas" + (reader.IsDBNull(reader.GetOrdinal("MaterialPurchasID")) ? 0 : ((int) reader["MaterialPurchasID"])).ToString();
                    item = EntityManager.LocateOrCreate<MaterialPurchas>(text.ToString(), "MaterialPurchas", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new MaterialPurchas();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    item.SuppressEntityEvents = true;
                    item.MaterialPurchasID = (int) reader["MaterialPurchasID"];
                    item.MaterialPurchasCode = reader.IsDBNull(reader.GetOrdinal("MaterialPurchasCode")) ? null : ((string) reader["MaterialPurchasCode"]);
                    item.PurchasUnitCode = reader.IsDBNull(reader.GetOrdinal("PurchasUnitCode")) ? null : ((string) reader["PurchasUnitCode"]);
                    item.PurchasDate = reader.IsDBNull(reader.GetOrdinal("PurchasDate")) ? null : ((DateTime?) reader["PurchasDate"]);
                    item.ProjectCode = reader.IsDBNull(reader.GetOrdinal("ProjectCode")) ? null : ((string) reader["ProjectCode"]);
                    item.Title = reader.IsDBNull(reader.GetOrdinal("Title")) ? null : ((string) reader["Title"]);
                    item.Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : ((string) reader["Description"]);
                    item.FollowUserCode = reader.IsDBNull(reader.GetOrdinal("FollowUserCode")) ? null : ((string) reader["FollowUserCode"]);
                    item.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : ((string) reader["Status"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override MaterialPurchas Get(TransactionManager transactionManager, MaterialPurchasKey key, int start, int pageLength)
        {
            return this.GetByMaterialPurchasID(transactionManager, key.MaterialPurchasID, start, pageLength);
        }

        public MaterialPurchas GetByMaterialPurchasID(int materialPurchasID)
        {
            int count = -1;
            return this.GetByMaterialPurchasID(null, materialPurchasID, 0, 0x7fffffff, out count);
        }

        public MaterialPurchas GetByMaterialPurchasID(TransactionManager transactionManager, int materialPurchasID)
        {
            int count = -1;
            return this.GetByMaterialPurchasID(transactionManager, materialPurchasID, 0, 0x7fffffff, out count);
        }

        public MaterialPurchas GetByMaterialPurchasID(int materialPurchasID, int start, int pageLength)
        {
            int count = -1;
            return this.GetByMaterialPurchasID(null, materialPurchasID, start, pageLength, out count);
        }

        public MaterialPurchas GetByMaterialPurchasID(int materialPurchasID, int start, int pageLength, out int count)
        {
            return this.GetByMaterialPurchasID(null, materialPurchasID, start, pageLength, out count);
        }

        public MaterialPurchas GetByMaterialPurchasID(TransactionManager transactionManager, int materialPurchasID, int start, int pageLength)
        {
            int count = -1;
            return this.GetByMaterialPurchasID(transactionManager, materialPurchasID, start, pageLength, out count);
        }

        public abstract MaterialPurchas GetByMaterialPurchasID(TransactionManager transactionManager, int materialPurchasID, int start, int pageLength, out int count);
        public TList<MaterialPurchas> GetByProjectCode(string projectCode)
        {
            int count = -1;
            return this.GetByProjectCode(projectCode, 0, 0x7fffffff, out count);
        }

        public TList<MaterialPurchas> GetByProjectCode(TransactionManager transactionManager, string projectCode)
        {
            int count = -1;
            return this.GetByProjectCode(transactionManager, projectCode, 0, 0x7fffffff, out count);
        }

        public TList<MaterialPurchas> GetByProjectCode(string projectCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByProjectCode(null, projectCode, start, pageLength, out count);
        }

        public TList<MaterialPurchas> GetByProjectCode(string projectCode, int start, int pageLength, out int count)
        {
            return this.GetByProjectCode(null, projectCode, start, pageLength, out count);
        }

        public TList<MaterialPurchas> GetByProjectCode(TransactionManager transactionManager, string projectCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByProjectCode(transactionManager, projectCode, start, pageLength, out count);
        }

        public abstract TList<MaterialPurchas> GetByProjectCode(TransactionManager transactionManager, string projectCode, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, MaterialPurchas entity)
        {
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.MaterialPurchasID = (int) row["MaterialPurchasID"];
            entity.MaterialPurchasCode = Convert.IsDBNull(row["MaterialPurchasCode"]) ? null : ((string) row["MaterialPurchasCode"]);
            entity.PurchasUnitCode = Convert.IsDBNull(row["PurchasUnitCode"]) ? null : ((string) row["PurchasUnitCode"]);
            entity.PurchasDate = Convert.IsDBNull(row["PurchasDate"]) ? null : ((DateTime?) row["PurchasDate"]);
            entity.ProjectCode = Convert.IsDBNull(row["ProjectCode"]) ? null : ((string) row["ProjectCode"]);
            entity.Title = Convert.IsDBNull(row["Title"]) ? null : ((string) row["Title"]);
            entity.Description = Convert.IsDBNull(row["Description"]) ? null : ((string) row["Description"]);
            entity.FollowUserCode = Convert.IsDBNull(row["FollowUserCode"]) ? null : ((string) row["FollowUserCode"]);
            entity.Status = Convert.IsDBNull(row["Status"]) ? null : ((string) row["Status"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, MaterialPurchas entity)
        {
            if (reader.Read())
            {
                entity.MaterialPurchasID = (int) reader["MaterialPurchasID"];
                entity.MaterialPurchasCode = reader.IsDBNull(reader.GetOrdinal("MaterialPurchasCode")) ? null : ((string) reader["MaterialPurchasCode"]);
                entity.PurchasUnitCode = reader.IsDBNull(reader.GetOrdinal("PurchasUnitCode")) ? null : ((string) reader["PurchasUnitCode"]);
                entity.PurchasDate = reader.IsDBNull(reader.GetOrdinal("PurchasDate")) ? null : ((DateTime?) reader["PurchasDate"]);
                entity.ProjectCode = reader.IsDBNull(reader.GetOrdinal("ProjectCode")) ? null : ((string) reader["ProjectCode"]);
                entity.Title = reader.IsDBNull(reader.GetOrdinal("Title")) ? null : ((string) reader["Title"]);
                entity.Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : ((string) reader["Description"]);
                entity.FollowUserCode = reader.IsDBNull(reader.GetOrdinal("FollowUserCode")) ? null : ((string) reader["FollowUserCode"]);
                entity.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : ((string) reader["Status"]);
                entity.AcceptChanges();
            }
        }
    }
}

