namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class TroubleProviderBaseCore : EntityProviderBase<Trouble, TroubleKey>
    {
        protected TroubleProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, Trouble entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if ((entity != null) && (base.CanDeepLoad(entity, "InspectSituation", "InspectSituationIDSource", deepLoadType, innerList) && (entity.InspectSituationIDSource == null)))
            {
                object[] pkItems = new object[] { entity.InspectSituationID };
                InspectSituation situation = EntityManager.LocateEntity<InspectSituation>(EntityLocator.ConstructKeyFromPkItems(typeof(InspectSituation), pkItems), DataRepository.Provider.EnableEntityTracking);
                if (situation != null)
                {
                    entity.InspectSituationIDSource = situation;
                }
                else
                {
                    entity.InspectSituationIDSource = DataRepository.InspectSituationProvider.GetByInspectSituationID(entity.InspectSituationID);
                }
                if (deep && (entity.InspectSituationIDSource != null))
                {
                    DataRepository.InspectSituationProvider.DeepLoad(transactionManager, entity.InspectSituationIDSource, deep, deepLoadType, childTypes, innerList);
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, Trouble entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity == null)
            {
                return false;
            }
            if (base.CanDeepSave(entity, "InspectSituation", "InspectSituationIDSource", deepSaveType, innerList) && (entity.InspectSituationIDSource != null))
            {
                DataRepository.InspectSituationProvider.Save(transactionManager, entity.InspectSituationIDSource);
                entity.InspectSituationID = entity.InspectSituationIDSource.InspectSituationID;
            }
            this.Save(transactionManager, entity);
            return true;
        }

        public bool Delete(int troubleID)
        {
            return this.Delete(null, troubleID);
        }

        public abstract bool Delete(TransactionManager transactionManager, int troubleID);
        public override bool Delete(TransactionManager transactionManager, TroubleKey key)
        {
            return this.Delete(transactionManager, key.TroubleID);
        }

        public static TList<Trouble> Fill(IDataReader reader, TList<Trouble> rows, int start, int pageLength)
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
                Trouble item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "Trouble" + (reader.IsDBNull(reader.GetOrdinal("TroubleID")) ? 0 : ((int) reader["TroubleID"])).ToString();
                    item = EntityManager.LocateOrCreate<Trouble>(text.ToString(), "Trouble", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new Trouble();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    item.SuppressEntityEvents = true;
                    item.TroubleID = (int) reader["TroubleID"];
                    item.InspectSituationID = (int) reader["InspectSituationID"];
                    item.Requirement = reader.IsDBNull(reader.GetOrdinal("Requirement")) ? null : ((string) reader["Requirement"]);
                    item.Suggestion = reader.IsDBNull(reader.GetOrdinal("Suggestion")) ? null : ((string) reader["Suggestion"]);
                    item.ExecutionTime = reader.IsDBNull(reader.GetOrdinal("ExecutionTime")) ? null : ((DateTime?) reader["ExecutionTime"]);
                    item.Place = reader.IsDBNull(reader.GetOrdinal("place")) ? null : ((string) reader["place"]);
                    item.TroubleCompendium = reader.IsDBNull(reader.GetOrdinal("TroubleCompendium")) ? null : ((string) reader["TroubleCompendium"]);
                    item.Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? null : ((string) reader["Remark"]);
                    item.Status = (int) reader["Status"];
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override Trouble Get(TransactionManager transactionManager, TroubleKey key, int start, int pageLength)
        {
            return this.GetByTroubleID(transactionManager, key.TroubleID, start, pageLength);
        }

        public TList<Trouble> GetByInspectSituationID(int inspectSituationID)
        {
            int count = -1;
            return this.GetByInspectSituationID(inspectSituationID, 0, 0x7fffffff, out count);
        }

        public TList<Trouble> GetByInspectSituationID(TransactionManager transactionManager, int inspectSituationID)
        {
            int count = -1;
            return this.GetByInspectSituationID(transactionManager, inspectSituationID, 0, 0x7fffffff, out count);
        }

        public TList<Trouble> GetByInspectSituationID(int inspectSituationID, int start, int pageLength)
        {
            int count = -1;
            return this.GetByInspectSituationID(null, inspectSituationID, start, pageLength, out count);
        }

        public TList<Trouble> GetByInspectSituationID(int inspectSituationID, int start, int pageLength, out int count)
        {
            return this.GetByInspectSituationID(null, inspectSituationID, start, pageLength, out count);
        }

        public TList<Trouble> GetByInspectSituationID(TransactionManager transactionManager, int inspectSituationID, int start, int pageLength)
        {
            int count = -1;
            return this.GetByInspectSituationID(transactionManager, inspectSituationID, start, pageLength, out count);
        }

        public abstract TList<Trouble> GetByInspectSituationID(TransactionManager transactionManager, int inspectSituationID, int start, int pageLength, out int count);
        public Trouble GetByTroubleID(int troubleID)
        {
            int count = -1;
            return this.GetByTroubleID(null, troubleID, 0, 0x7fffffff, out count);
        }

        public Trouble GetByTroubleID(TransactionManager transactionManager, int troubleID)
        {
            int count = -1;
            return this.GetByTroubleID(transactionManager, troubleID, 0, 0x7fffffff, out count);
        }

        public Trouble GetByTroubleID(int troubleID, int start, int pageLength)
        {
            int count = -1;
            return this.GetByTroubleID(null, troubleID, start, pageLength, out count);
        }

        public Trouble GetByTroubleID(int troubleID, int start, int pageLength, out int count)
        {
            return this.GetByTroubleID(null, troubleID, start, pageLength, out count);
        }

        public Trouble GetByTroubleID(TransactionManager transactionManager, int troubleID, int start, int pageLength)
        {
            int count = -1;
            return this.GetByTroubleID(transactionManager, troubleID, start, pageLength, out count);
        }

        public abstract Trouble GetByTroubleID(TransactionManager transactionManager, int troubleID, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, Trouble entity)
        {
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.TroubleID = (int) row["TroubleID"];
            entity.InspectSituationID = (int) row["InspectSituationID"];
            entity.Requirement = Convert.IsDBNull(row["Requirement"]) ? null : ((string) row["Requirement"]);
            entity.Suggestion = Convert.IsDBNull(row["Suggestion"]) ? null : ((string) row["Suggestion"]);
            entity.ExecutionTime = Convert.IsDBNull(row["ExecutionTime"]) ? null : ((DateTime?) row["ExecutionTime"]);
            entity.Place = Convert.IsDBNull(row["place"]) ? null : ((string) row["place"]);
            entity.TroubleCompendium = Convert.IsDBNull(row["TroubleCompendium"]) ? null : ((string) row["TroubleCompendium"]);
            entity.Remark = Convert.IsDBNull(row["Remark"]) ? null : ((string) row["Remark"]);
            entity.Status = (int) row["Status"];
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, Trouble entity)
        {
            if (reader.Read())
            {
                entity.TroubleID = (int) reader["TroubleID"];
                entity.InspectSituationID = (int) reader["InspectSituationID"];
                entity.Requirement = reader.IsDBNull(reader.GetOrdinal("Requirement")) ? null : ((string) reader["Requirement"]);
                entity.Suggestion = reader.IsDBNull(reader.GetOrdinal("Suggestion")) ? null : ((string) reader["Suggestion"]);
                entity.ExecutionTime = reader.IsDBNull(reader.GetOrdinal("ExecutionTime")) ? null : ((DateTime?) reader["ExecutionTime"]);
                entity.Place = reader.IsDBNull(reader.GetOrdinal("place")) ? null : ((string) reader["place"]);
                entity.TroubleCompendium = reader.IsDBNull(reader.GetOrdinal("TroubleCompendium")) ? null : ((string) reader["TroubleCompendium"]);
                entity.Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? null : ((string) reader["Remark"]);
                entity.Status = (int) reader["Status"];
                entity.AcceptChanges();
            }
        }
    }
}

