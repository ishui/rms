namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    using System.Xml;

    public abstract class InspectSituationProviderBaseCore : EntityProviderBase<InspectSituation, InspectSituationKey>
    {
        protected InspectSituationProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, InspectSituation entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity != null)
            {
                object[] pkItems;
                if (base.CanDeepLoad(entity, "Project", "ProjectCodeSource", deepLoadType, innerList) && (entity.ProjectCodeSource == null))
                {
                    pkItems = new object[] { entity.ProjectCode ?? string.Empty };
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
                if (base.CanDeepLoad(entity, "SystemUser", "InspectUserSource", deepLoadType, innerList) && (entity.InspectUserSource == null))
                {
                    pkItems = new object[] { entity.InspectUser ?? string.Empty };
                    SystemUser user = EntityManager.LocateEntity<SystemUser>(EntityLocator.ConstructKeyFromPkItems(typeof(SystemUser), pkItems), DataRepository.Provider.EnableEntityTracking);
                    if (user != null)
                    {
                        entity.InspectUserSource = user;
                    }
                    else
                    {
                        entity.InspectUserSource = DataRepository.SystemUserProvider.GetByUserCode(entity.InspectUser ?? string.Empty);
                    }
                    if (deep && (entity.InspectUserSource != null))
                    {
                        DataRepository.SystemUserProvider.DeepLoad(transactionManager, entity.InspectUserSource, deep, deepLoadType, childTypes, innerList);
                    }
                }
                if (base.CanDeepLoad(entity, "List<Trouble>", "TroubleCollection", deepLoadType, innerList))
                {
                    entity.TroubleCollection = DataRepository.TroubleProvider.GetByInspectSituationID(transactionManager, entity.InspectSituationID);
                    if (deep && (entity.TroubleCollection.Count > 0))
                    {
                        DataRepository.TroubleProvider.DeepLoad(transactionManager, entity.TroubleCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, InspectSituation entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
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
            if (base.CanDeepSave(entity, "SystemUser", "InspectUserSource", deepSaveType, innerList) && (entity.InspectUserSource != null))
            {
                DataRepository.SystemUserProvider.Save(transactionManager, entity.InspectUserSource);
                entity.InspectUser = entity.InspectUserSource.UserCode;
            }
            this.Save(transactionManager, entity);
            if (base.CanDeepSave(entity, "List<Trouble>", "TroubleCollection", deepSaveType, innerList))
            {
                foreach (Trouble trouble in entity.TroubleCollection)
                {
                    trouble.InspectSituationID = entity.InspectSituationID;
                }
                if ((entity.TroubleCollection.Count > 0) || (entity.TroubleCollection.DeletedItems.Count > 0))
                {
                    DataRepository.TroubleProvider.DeepSave(transactionManager, entity.TroubleCollection, deepSaveType, childTypes, innerList);
                }
            }
            return true;
        }

        public bool Delete(int inspectSituationID)
        {
            return this.Delete(null, inspectSituationID);
        }

        public abstract bool Delete(TransactionManager transactionManager, int inspectSituationID);
        public override bool Delete(TransactionManager transactionManager, InspectSituationKey key)
        {
            return this.Delete(transactionManager, key.InspectSituationID);
        }

        public static TList<InspectSituation> Fill(IDataReader reader, TList<InspectSituation> rows, int start, int pageLength)
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
                InspectSituation item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "InspectSituation" + (reader.IsDBNull(reader.GetOrdinal("InspectSituationID")) ? 0 : ((int) reader["InspectSituationID"])).ToString();
                    item = EntityManager.LocateOrCreate<InspectSituation>(text.ToString(), "InspectSituation", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new InspectSituation();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    item.SuppressEntityEvents = true;
                    item.InspectSituationID = (int) reader["InspectSituationID"];
                    item.InspectSituationNO = (string) reader["InspectSituationNO"];
                    item.ProjectCode = reader.IsDBNull(reader.GetOrdinal("ProjectCode")) ? null : ((string) reader["ProjectCode"]);
                    item.InspectDate = reader.IsDBNull(reader.GetOrdinal("InspectDate")) ? null : ((DateTime?) reader["InspectDate"]);
                    item.Weather = reader.IsDBNull(reader.GetOrdinal("Weather")) ? null : ((string) reader["Weather"]);
                    item.InspectUserIpecialty = reader.IsDBNull(reader.GetOrdinal("InspectUserIpecialty")) ? null : ((string) reader["InspectUserIpecialty"]);
                    item.InspectUser = reader.IsDBNull(reader.GetOrdinal("InspectUser")) ? null : ((string) reader["InspectUser"]);
                    item.KeyPoint = reader.IsDBNull(reader.GetOrdinal("KeyPoint")) ? null : ((string) reader["KeyPoint"]);
                    item.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : ((int?) reader["Status"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override InspectSituation Get(TransactionManager transactionManager, InspectSituationKey key, int start, int pageLength)
        {
            return this.GetByInspectSituationID(transactionManager, key.InspectSituationID, start, pageLength);
        }

        public InspectSituation GetByInspectSituationID(int inspectSituationID)
        {
            int count = -1;
            return this.GetByInspectSituationID(null, inspectSituationID, 0, 0x7fffffff, out count);
        }

        public InspectSituation GetByInspectSituationID(TransactionManager transactionManager, int inspectSituationID)
        {
            int count = -1;
            return this.GetByInspectSituationID(transactionManager, inspectSituationID, 0, 0x7fffffff, out count);
        }

        public InspectSituation GetByInspectSituationID(int inspectSituationID, int start, int pageLength)
        {
            int count = -1;
            return this.GetByInspectSituationID(null, inspectSituationID, start, pageLength, out count);
        }

        public InspectSituation GetByInspectSituationID(int inspectSituationID, int start, int pageLength, out int count)
        {
            return this.GetByInspectSituationID(null, inspectSituationID, start, pageLength, out count);
        }

        public InspectSituation GetByInspectSituationID(TransactionManager transactionManager, int inspectSituationID, int start, int pageLength)
        {
            int count = -1;
            return this.GetByInspectSituationID(transactionManager, inspectSituationID, start, pageLength, out count);
        }

        public abstract InspectSituation GetByInspectSituationID(TransactionManager transactionManager, int inspectSituationID, int start, int pageLength, out int count);
        public TList<InspectSituation> GetByInspectUser(string inspectUser)
        {
            int count = -1;
            return this.GetByInspectUser(inspectUser, 0, 0x7fffffff, out count);
        }

        public TList<InspectSituation> GetByInspectUser(TransactionManager transactionManager, string inspectUser)
        {
            int count = -1;
            return this.GetByInspectUser(transactionManager, inspectUser, 0, 0x7fffffff, out count);
        }

        public TList<InspectSituation> GetByInspectUser(string inspectUser, int start, int pageLength)
        {
            int count = -1;
            return this.GetByInspectUser(null, inspectUser, start, pageLength, out count);
        }

        public TList<InspectSituation> GetByInspectUser(string inspectUser, int start, int pageLength, out int count)
        {
            return this.GetByInspectUser(null, inspectUser, start, pageLength, out count);
        }

        public TList<InspectSituation> GetByInspectUser(TransactionManager transactionManager, string inspectUser, int start, int pageLength)
        {
            int count = -1;
            return this.GetByInspectUser(transactionManager, inspectUser, start, pageLength, out count);
        }

        public abstract TList<InspectSituation> GetByInspectUser(TransactionManager transactionManager, string inspectUser, int start, int pageLength, out int count);
        public TList<InspectSituation> GetByProjectCode(string projectCode)
        {
            int count = -1;
            return this.GetByProjectCode(projectCode, 0, 0x7fffffff, out count);
        }

        public TList<InspectSituation> GetByProjectCode(TransactionManager transactionManager, string projectCode)
        {
            int count = -1;
            return this.GetByProjectCode(transactionManager, projectCode, 0, 0x7fffffff, out count);
        }

        public TList<InspectSituation> GetByProjectCode(string projectCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByProjectCode(null, projectCode, start, pageLength, out count);
        }

        public TList<InspectSituation> GetByProjectCode(string projectCode, int start, int pageLength, out int count)
        {
            return this.GetByProjectCode(null, projectCode, start, pageLength, out count);
        }

        public TList<InspectSituation> GetByProjectCode(TransactionManager transactionManager, string projectCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByProjectCode(transactionManager, projectCode, start, pageLength, out count);
        }

        public abstract TList<InspectSituation> GetByProjectCode(TransactionManager transactionManager, string projectCode, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, InspectSituation entity)
        {
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.InspectSituationID = (int) row["InspectSituationID"];
            entity.InspectSituationNO = (string) row["InspectSituationNO"];
            entity.ProjectCode = Convert.IsDBNull(row["ProjectCode"]) ? null : ((string) row["ProjectCode"]);
            entity.InspectDate = Convert.IsDBNull(row["InspectDate"]) ? null : ((DateTime?) row["InspectDate"]);
            entity.Weather = Convert.IsDBNull(row["Weather"]) ? null : ((string) row["Weather"]);
            entity.InspectUserIpecialty = Convert.IsDBNull(row["InspectUserIpecialty"]) ? null : ((string) row["InspectUserIpecialty"]);
            entity.InspectUser = Convert.IsDBNull(row["InspectUser"]) ? null : ((string) row["InspectUser"]);
            entity.KeyPoint = Convert.IsDBNull(row["KeyPoint"]) ? null : ((string) row["KeyPoint"]);
            entity.Status = Convert.IsDBNull(row["Status"]) ? null : ((int?) row["Status"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, InspectSituation entity)
        {
            if (reader.Read())
            {
                entity.InspectSituationID = (int) reader["InspectSituationID"];
                entity.InspectSituationNO = (string) reader["InspectSituationNO"];
                entity.ProjectCode = reader.IsDBNull(reader.GetOrdinal("ProjectCode")) ? null : ((string) reader["ProjectCode"]);
                entity.InspectDate = reader.IsDBNull(reader.GetOrdinal("InspectDate")) ? null : ((DateTime?) reader["InspectDate"]);
                entity.Weather = reader.IsDBNull(reader.GetOrdinal("Weather")) ? null : ((string) reader["Weather"]);
                entity.InspectUserIpecialty = reader.IsDBNull(reader.GetOrdinal("InspectUserIpecialty")) ? null : ((string) reader["InspectUserIpecialty"]);
                entity.InspectUser = reader.IsDBNull(reader.GetOrdinal("InspectUser")) ? null : ((string) reader["InspectUser"]);
                entity.KeyPoint = reader.IsDBNull(reader.GetOrdinal("KeyPoint")) ? null : ((string) reader["KeyPoint"]);
                entity.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : ((int?) reader["Status"]);
                entity.AcceptChanges();
            }
        }
    }
}

