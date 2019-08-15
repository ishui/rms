namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class MaterialPurchasDtlProviderBaseCore : EntityProviderBase<MaterialPurchasDtl, MaterialPurchasDtlKey>
    {
        protected MaterialPurchasDtlProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, MaterialPurchasDtl entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if ((entity != null) && (base.CanDeepLoad(entity, "MaterialPurchas", "MaterialPurchasIDSource", deepLoadType, innerList) && (entity.MaterialPurchasIDSource == null)))
            {
                object[] pkItems = new object[1];
                int? materialPurchasID = entity.MaterialPurchasID;
                pkItems[0] = materialPurchasID.HasValue ? materialPurchasID.GetValueOrDefault() : 0;
                MaterialPurchas purchas = EntityManager.LocateEntity<MaterialPurchas>(EntityLocator.ConstructKeyFromPkItems(typeof(MaterialPurchas), pkItems), DataRepository.Provider.EnableEntityTracking);
                if (purchas != null)
                {
                    entity.MaterialPurchasIDSource = purchas;
                }
                else
                {
                    materialPurchasID = entity.MaterialPurchasID;
                    entity.MaterialPurchasIDSource = DataRepository.MaterialPurchasProvider.GetByMaterialPurchasID(materialPurchasID.HasValue ? materialPurchasID.GetValueOrDefault() : 0);
                }
                if (deep && (entity.MaterialPurchasIDSource != null))
                {
                    DataRepository.MaterialPurchasProvider.DeepLoad(transactionManager, entity.MaterialPurchasIDSource, deep, deepLoadType, childTypes, innerList);
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, MaterialPurchasDtl entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity == null)
            {
                return false;
            }
            if (base.CanDeepSave(entity, "MaterialPurchas", "MaterialPurchasIDSource", deepSaveType, innerList) && (entity.MaterialPurchasIDSource != null))
            {
                DataRepository.MaterialPurchasProvider.Save(transactionManager, entity.MaterialPurchasIDSource);
                entity.MaterialPurchasID = new int?(entity.MaterialPurchasIDSource.MaterialPurchasID);
            }
            this.Save(transactionManager, entity);
            return true;
        }

        public bool Delete(int materialPurchasDtlID)
        {
            return this.Delete(null, materialPurchasDtlID);
        }

        public abstract bool Delete(TransactionManager transactionManager, int materialPurchasDtlID);
        public override bool Delete(TransactionManager transactionManager, MaterialPurchasDtlKey key)
        {
            return this.Delete(transactionManager, key.MaterialPurchasDtlID);
        }

        public static TList<MaterialPurchasDtl> Fill(IDataReader reader, TList<MaterialPurchasDtl> rows, int start, int pageLength)
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
                MaterialPurchasDtl item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "MaterialPurchasDtl" + (reader.IsDBNull(reader.GetOrdinal("MaterialPurchasDtlID")) ? 0 : ((int) reader["MaterialPurchasDtlID"])).ToString();
                    item = EntityManager.LocateOrCreate<MaterialPurchasDtl>(text.ToString(), "MaterialPurchasDtl", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new MaterialPurchasDtl();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    decimal? nullable2;
                    DateTime? nullable3;
                    item.SuppressEntityEvents = true;
                    item.MaterialPurchasDtlID = (int) reader["MaterialPurchasDtlID"];
                    item.MaterialPurchasID = reader.IsDBNull(reader.GetOrdinal("MaterialPurchasID")) ? null : ((int?) reader["MaterialPurchasID"]);
                    item.TypeStandard = reader.IsDBNull(reader.GetOrdinal("TypeStandard")) ? null : ((string) reader["TypeStandard"]);
                    item.Unit = reader.IsDBNull(reader.GetOrdinal("Unit")) ? null : ((string) reader["Unit"]);
                    item.Number = reader.IsDBNull(reader.GetOrdinal("Number")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["Number"]);
                    item.NeedDate = reader.IsDBNull(reader.GetOrdinal("NeedDate")) ? ((DateTime?) (nullable3 = null)) : ((DateTime?) reader["NeedDate"]);
                    item.SignDate = reader.IsDBNull(reader.GetOrdinal("SignDate")) ? ((DateTime?) (nullable3 = null)) : ((DateTime?) reader["SignDate"]);
                    item.SearchPriceDtl = reader.IsDBNull(reader.GetOrdinal("SearchPriceDtl")) ? null : ((string) reader["SearchPriceDtl"]);
                    item.FinalPrice = reader.IsDBNull(reader.GetOrdinal("FinalPrice")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["FinalPrice"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override MaterialPurchasDtl Get(TransactionManager transactionManager, MaterialPurchasDtlKey key, int start, int pageLength)
        {
            return this.GetByMaterialPurchasDtlID(transactionManager, key.MaterialPurchasDtlID, start, pageLength);
        }

        public MaterialPurchasDtl GetByMaterialPurchasDtlID(int materialPurchasDtlID)
        {
            int count = -1;
            return this.GetByMaterialPurchasDtlID(null, materialPurchasDtlID, 0, 0x7fffffff, out count);
        }

        public MaterialPurchasDtl GetByMaterialPurchasDtlID(TransactionManager transactionManager, int materialPurchasDtlID)
        {
            int count = -1;
            return this.GetByMaterialPurchasDtlID(transactionManager, materialPurchasDtlID, 0, 0x7fffffff, out count);
        }

        public MaterialPurchasDtl GetByMaterialPurchasDtlID(int materialPurchasDtlID, int start, int pageLength)
        {
            int count = -1;
            return this.GetByMaterialPurchasDtlID(null, materialPurchasDtlID, start, pageLength, out count);
        }

        public MaterialPurchasDtl GetByMaterialPurchasDtlID(int materialPurchasDtlID, int start, int pageLength, out int count)
        {
            return this.GetByMaterialPurchasDtlID(null, materialPurchasDtlID, start, pageLength, out count);
        }

        public MaterialPurchasDtl GetByMaterialPurchasDtlID(TransactionManager transactionManager, int materialPurchasDtlID, int start, int pageLength)
        {
            int count = -1;
            return this.GetByMaterialPurchasDtlID(transactionManager, materialPurchasDtlID, start, pageLength, out count);
        }

        public abstract MaterialPurchasDtl GetByMaterialPurchasDtlID(TransactionManager transactionManager, int materialPurchasDtlID, int start, int pageLength, out int count);
        public TList<MaterialPurchasDtl> GetByMaterialPurchasID(int? materialPurchasID)
        {
            int count = -1;
            return this.GetByMaterialPurchasID(materialPurchasID, 0, 0x7fffffff, out count);
        }

        public TList<MaterialPurchasDtl> GetByMaterialPurchasID(TransactionManager transactionManager, int? materialPurchasID)
        {
            int count = -1;
            return this.GetByMaterialPurchasID(transactionManager, materialPurchasID, 0, 0x7fffffff, out count);
        }

        public TList<MaterialPurchasDtl> GetByMaterialPurchasID(int? materialPurchasID, int start, int pageLength)
        {
            int count = -1;
            return this.GetByMaterialPurchasID(null, materialPurchasID, start, pageLength, out count);
        }

        public TList<MaterialPurchasDtl> GetByMaterialPurchasID(int? materialPurchasID, int start, int pageLength, out int count)
        {
            return this.GetByMaterialPurchasID(null, materialPurchasID, start, pageLength, out count);
        }

        public TList<MaterialPurchasDtl> GetByMaterialPurchasID(TransactionManager transactionManager, int? materialPurchasID, int start, int pageLength)
        {
            int count = -1;
            return this.GetByMaterialPurchasID(transactionManager, materialPurchasID, start, pageLength, out count);
        }

        public abstract TList<MaterialPurchasDtl> GetByMaterialPurchasID(TransactionManager transactionManager, int? materialPurchasID, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, MaterialPurchasDtl entity)
        {
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.MaterialPurchasDtlID = (int) row["MaterialPurchasDtlID"];
            entity.MaterialPurchasID = Convert.IsDBNull(row["MaterialPurchasID"]) ? null : ((int?) row["MaterialPurchasID"]);
            entity.TypeStandard = Convert.IsDBNull(row["TypeStandard"]) ? null : ((string) row["TypeStandard"]);
            entity.Unit = Convert.IsDBNull(row["Unit"]) ? null : ((string) row["Unit"]);
            entity.Number = Convert.IsDBNull(row["Number"]) ? null : ((decimal?) row["Number"]);
            entity.NeedDate = Convert.IsDBNull(row["NeedDate"]) ? null : ((DateTime?) row["NeedDate"]);
            entity.SignDate = Convert.IsDBNull(row["SignDate"]) ? null : ((DateTime?) row["SignDate"]);
            entity.SearchPriceDtl = Convert.IsDBNull(row["SearchPriceDtl"]) ? null : ((string) row["SearchPriceDtl"]);
            entity.FinalPrice = Convert.IsDBNull(row["FinalPrice"]) ? null : ((decimal?) row["FinalPrice"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, MaterialPurchasDtl entity)
        {
            if (reader.Read())
            {
                entity.MaterialPurchasDtlID = (int) reader["MaterialPurchasDtlID"];
                entity.MaterialPurchasID = reader.IsDBNull(reader.GetOrdinal("MaterialPurchasID")) ? null : ((int?) reader["MaterialPurchasID"]);
                entity.TypeStandard = reader.IsDBNull(reader.GetOrdinal("TypeStandard")) ? null : ((string) reader["TypeStandard"]);
                entity.Unit = reader.IsDBNull(reader.GetOrdinal("Unit")) ? null : ((string) reader["Unit"]);
                entity.Number = reader.IsDBNull(reader.GetOrdinal("Number")) ? null : ((decimal?) reader["Number"]);
                entity.NeedDate = reader.IsDBNull(reader.GetOrdinal("NeedDate")) ? null : ((DateTime?) reader["NeedDate"]);
                entity.SignDate = reader.IsDBNull(reader.GetOrdinal("SignDate")) ? null : ((DateTime?) reader["SignDate"]);
                entity.SearchPriceDtl = reader.IsDBNull(reader.GetOrdinal("SearchPriceDtl")) ? null : ((string) reader["SearchPriceDtl"]);
                entity.FinalPrice = reader.IsDBNull(reader.GetOrdinal("FinalPrice")) ? null : ((decimal?) reader["FinalPrice"]);
                entity.AcceptChanges();
            }
        }
    }
}

