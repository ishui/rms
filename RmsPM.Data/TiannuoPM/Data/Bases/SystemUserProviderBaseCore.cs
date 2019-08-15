namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class SystemUserProviderBaseCore : EntityProviderBase<SystemUser, SystemUserKey>
    {
        protected SystemUserProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, SystemUser entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if ((entity != null) && base.CanDeepLoad(entity, "List<InspectSituation>", "InspectSituationCollection", deepLoadType, innerList))
            {
                entity.InspectSituationCollection = DataRepository.InspectSituationProvider.GetByInspectUser(transactionManager, entity.UserCode);
                if (deep && (entity.InspectSituationCollection.Count > 0))
                {
                    DataRepository.InspectSituationProvider.DeepLoad(transactionManager, entity.InspectSituationCollection, deep, deepLoadType, childTypes, innerList);
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, SystemUser entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity == null)
            {
                return false;
            }
            this.Save(transactionManager, entity);
            if (base.CanDeepSave(entity, "List<InspectSituation>", "InspectSituationCollection", deepSaveType, innerList))
            {
                foreach (InspectSituation situation in entity.InspectSituationCollection)
                {
                    situation.InspectUser = entity.UserCode;
                }
                if ((entity.InspectSituationCollection.Count > 0) || (entity.InspectSituationCollection.DeletedItems.Count > 0))
                {
                    DataRepository.InspectSituationProvider.DeepSave(transactionManager, entity.InspectSituationCollection, deepSaveType, childTypes, innerList);
                }
            }
            return true;
        }

        public bool Delete(string userCode)
        {
            return this.Delete(null, userCode);
        }

        public abstract bool Delete(TransactionManager transactionManager, string userCode);
        public override bool Delete(TransactionManager transactionManager, SystemUserKey key)
        {
            return this.Delete(transactionManager, key.UserCode);
        }

        public static TList<SystemUser> Fill(IDataReader reader, TList<SystemUser> rows, int start, int pageLength)
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
                SystemUser item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "SystemUser" + (reader.IsDBNull(reader.GetOrdinal("UserCode")) ? string.Empty : ((string) reader["UserCode"])).ToString();
                    item = EntityManager.LocateOrCreate<SystemUser>(text.ToString(), "SystemUser", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new SystemUser();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    item.SuppressEntityEvents = true;
                    item.UserCode = (string) reader["UserCode"];
                    item.OriginalUserCode = item.UserCode;
                    item.UserID = reader.IsDBNull(reader.GetOrdinal("UserID")) ? null : ((string) reader["UserID"]);
                    item.UserName = reader.IsDBNull(reader.GetOrdinal("UserName")) ? null : ((string) reader["UserName"]);
                    item.OwnName = reader.IsDBNull(reader.GetOrdinal("OwnName")) ? null : ((string) reader["OwnName"]);
                    item.PassWord = reader.IsDBNull(reader.GetOrdinal("PassWord")) ? null : ((string) reader["PassWord"]);
                    item.Sex = reader.IsDBNull(reader.GetOrdinal("Sex")) ? null : ((string) reader["Sex"]);
                    item.Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : ((string) reader["Phone"]);
                    item.MailBox = reader.IsDBNull(reader.GetOrdinal("MailBox")) ? null : ((string) reader["MailBox"]);
                    item.Note = reader.IsDBNull(reader.GetOrdinal("Note")) ? null : ((string) reader["Note"]);
                    item.BirthDay = reader.IsDBNull(reader.GetOrdinal("BirthDay")) ? null : ((DateTime?) reader["BirthDay"]);
                    item.PhoneHome = reader.IsDBNull(reader.GetOrdinal("PhoneHome")) ? null : ((string) reader["PhoneHome"]);
                    item.Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : ((string) reader["Address"]);
                    item.Mobile = reader.IsDBNull(reader.GetOrdinal("Mobile")) ? null : ((string) reader["Mobile"]);
                    item.Fax = reader.IsDBNull(reader.GetOrdinal("Fax")) ? null : ((string) reader["Fax"]);
                    item.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : ((int?) reader["Status"]);
                    item.LastProjectCode = reader.IsDBNull(reader.GetOrdinal("LastProjectCode")) ? null : ((string) reader["LastProjectCode"]);
                    item.SortID = reader.IsDBNull(reader.GetOrdinal("SortID")) ? null : ((string) reader["SortID"]);
                    item.ShortUserName = reader.IsDBNull(reader.GetOrdinal("ShortUserName")) ? null : ((string) reader["ShortUserName"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override SystemUser Get(TransactionManager transactionManager, SystemUserKey key, int start, int pageLength)
        {
            return this.GetByUserCode(transactionManager, key.UserCode, start, pageLength);
        }

        public SystemUser GetByUserCode(string userCode)
        {
            int count = -1;
            return this.GetByUserCode(null, userCode, 0, 0x7fffffff, out count);
        }

        public SystemUser GetByUserCode(TransactionManager transactionManager, string userCode)
        {
            int count = -1;
            return this.GetByUserCode(transactionManager, userCode, 0, 0x7fffffff, out count);
        }

        public SystemUser GetByUserCode(string userCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByUserCode(null, userCode, start, pageLength, out count);
        }

        public SystemUser GetByUserCode(string userCode, int start, int pageLength, out int count)
        {
            return this.GetByUserCode(null, userCode, start, pageLength, out count);
        }

        public SystemUser GetByUserCode(TransactionManager transactionManager, string userCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByUserCode(transactionManager, userCode, start, pageLength, out count);
        }

        public abstract SystemUser GetByUserCode(TransactionManager transactionManager, string userCode, int start, int pageLength, out int count);
        public SystemUser GetByUserID(string userID)
        {
            int count = -1;
            return this.GetByUserID(null, userID, 0, 0x7fffffff, out count);
        }

        public SystemUser GetByUserID(TransactionManager transactionManager, string userID)
        {
            int count = -1;
            return this.GetByUserID(transactionManager, userID, 0, 0x7fffffff, out count);
        }

        public SystemUser GetByUserID(string userID, int start, int pageLength)
        {
            int count = -1;
            return this.GetByUserID(null, userID, start, pageLength, out count);
        }

        public SystemUser GetByUserID(string userID, int start, int pageLength, out int count)
        {
            return this.GetByUserID(null, userID, start, pageLength, out count);
        }

        public SystemUser GetByUserID(TransactionManager transactionManager, string userID, int start, int pageLength)
        {
            int count = -1;
            return this.GetByUserID(transactionManager, userID, start, pageLength, out count);
        }

        public abstract SystemUser GetByUserID(TransactionManager transactionManager, string userID, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, SystemUser entity)
        {
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.UserCode = (string) row["UserCode"];
            entity.OriginalUserCode = (string) row["UserCode"];
            entity.UserID = Convert.IsDBNull(row["UserID"]) ? null : ((string) row["UserID"]);
            entity.UserName = Convert.IsDBNull(row["UserName"]) ? null : ((string) row["UserName"]);
            entity.OwnName = Convert.IsDBNull(row["OwnName"]) ? null : ((string) row["OwnName"]);
            entity.PassWord = Convert.IsDBNull(row["PassWord"]) ? null : ((string) row["PassWord"]);
            entity.Sex = Convert.IsDBNull(row["Sex"]) ? null : ((string) row["Sex"]);
            entity.Phone = Convert.IsDBNull(row["Phone"]) ? null : ((string) row["Phone"]);
            entity.MailBox = Convert.IsDBNull(row["MailBox"]) ? null : ((string) row["MailBox"]);
            entity.Note = Convert.IsDBNull(row["Note"]) ? null : ((string) row["Note"]);
            entity.BirthDay = Convert.IsDBNull(row["BirthDay"]) ? null : ((DateTime?) row["BirthDay"]);
            entity.PhoneHome = Convert.IsDBNull(row["PhoneHome"]) ? null : ((string) row["PhoneHome"]);
            entity.Address = Convert.IsDBNull(row["Address"]) ? null : ((string) row["Address"]);
            entity.Mobile = Convert.IsDBNull(row["Mobile"]) ? null : ((string) row["Mobile"]);
            entity.Fax = Convert.IsDBNull(row["Fax"]) ? null : ((string) row["Fax"]);
            entity.Status = Convert.IsDBNull(row["Status"]) ? null : ((int?) row["Status"]);
            entity.LastProjectCode = Convert.IsDBNull(row["LastProjectCode"]) ? null : ((string) row["LastProjectCode"]);
            entity.SortID = Convert.IsDBNull(row["SortID"]) ? null : ((string) row["SortID"]);
            entity.ShortUserName = Convert.IsDBNull(row["ShortUserName"]) ? null : ((string) row["ShortUserName"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, SystemUser entity)
        {
            if (reader.Read())
            {
                entity.UserCode = (string) reader["UserCode"];
                entity.OriginalUserCode = (string) reader["UserCode"];
                entity.UserID = reader.IsDBNull(reader.GetOrdinal("UserID")) ? null : ((string) reader["UserID"]);
                entity.UserName = reader.IsDBNull(reader.GetOrdinal("UserName")) ? null : ((string) reader["UserName"]);
                entity.OwnName = reader.IsDBNull(reader.GetOrdinal("OwnName")) ? null : ((string) reader["OwnName"]);
                entity.PassWord = reader.IsDBNull(reader.GetOrdinal("PassWord")) ? null : ((string) reader["PassWord"]);
                entity.Sex = reader.IsDBNull(reader.GetOrdinal("Sex")) ? null : ((string) reader["Sex"]);
                entity.Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : ((string) reader["Phone"]);
                entity.MailBox = reader.IsDBNull(reader.GetOrdinal("MailBox")) ? null : ((string) reader["MailBox"]);
                entity.Note = reader.IsDBNull(reader.GetOrdinal("Note")) ? null : ((string) reader["Note"]);
                entity.BirthDay = reader.IsDBNull(reader.GetOrdinal("BirthDay")) ? null : ((DateTime?) reader["BirthDay"]);
                entity.PhoneHome = reader.IsDBNull(reader.GetOrdinal("PhoneHome")) ? null : ((string) reader["PhoneHome"]);
                entity.Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : ((string) reader["Address"]);
                entity.Mobile = reader.IsDBNull(reader.GetOrdinal("Mobile")) ? null : ((string) reader["Mobile"]);
                entity.Fax = reader.IsDBNull(reader.GetOrdinal("Fax")) ? null : ((string) reader["Fax"]);
                entity.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : ((int?) reader["Status"]);
                entity.LastProjectCode = reader.IsDBNull(reader.GetOrdinal("LastProjectCode")) ? null : ((string) reader["LastProjectCode"]);
                entity.SortID = reader.IsDBNull(reader.GetOrdinal("SortID")) ? null : ((string) reader["SortID"]);
                entity.ShortUserName = reader.IsDBNull(reader.GetOrdinal("ShortUserName")) ? null : ((string) reader["ShortUserName"]);
                entity.AcceptChanges();
            }
        }
    }
}

