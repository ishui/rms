namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class PayoutProviderBaseCore : EntityProviderBase<Payout, PayoutKey>
    {
        protected PayoutProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, Payout entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
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
                if (base.CanDeepLoad(entity, "List<PayoutItem>", "PayoutItemCollection", deepLoadType, innerList))
                {
                    entity.PayoutItemCollection = DataRepository.PayoutItemProvider.GetByPayoutCode(transactionManager, entity.PayoutCode);
                    if (deep && (entity.PayoutItemCollection.Count > 0))
                    {
                        DataRepository.PayoutItemProvider.DeepLoad(transactionManager, entity.PayoutItemCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, Payout entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
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
            if (base.CanDeepSave(entity, "List<PayoutItem>", "PayoutItemCollection", deepSaveType, innerList))
            {
                foreach (PayoutItem item in entity.PayoutItemCollection)
                {
                    item.PayoutCode = entity.PayoutCode;
                }
                if ((entity.PayoutItemCollection.Count > 0) || (entity.PayoutItemCollection.DeletedItems.Count > 0))
                {
                    DataRepository.PayoutItemProvider.DeepSave(transactionManager, entity.PayoutItemCollection, deepSaveType, childTypes, innerList);
                }
            }
            return true;
        }

        public bool Delete(string payoutCode)
        {
            return this.Delete(null, payoutCode);
        }

        public abstract bool Delete(TransactionManager transactionManager, string payoutCode);
        public override bool Delete(TransactionManager transactionManager, PayoutKey key)
        {
            return this.Delete(transactionManager, key.PayoutCode);
        }

        public static TList<Payout> Fill(IDataReader reader, TList<Payout> rows, int start, int pageLength)
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
                Payout item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "Payout" + (reader.IsDBNull(reader.GetOrdinal("PayoutCode")) ? string.Empty : ((string) reader["PayoutCode"])).ToString();
                    item = EntityManager.LocateOrCreate<Payout>(text.ToString(), "Payout", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new Payout();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    DateTime? nullable;
                    decimal? nullable2;
                    int? nullable3;
                    item.SuppressEntityEvents = true;
                    item.PayoutCode = (string) reader["PayoutCode"];
                    item.OriginalPayoutCode = item.PayoutCode;
                    item.PayoutID = reader.IsDBNull(reader.GetOrdinal("PayoutID")) ? null : ((string) reader["PayoutID"]);
                    item.ProjectCode = reader.IsDBNull(reader.GetOrdinal("ProjectCode")) ? null : ((string) reader["ProjectCode"]);
                    item.PaymentCodes = reader.IsDBNull(reader.GetOrdinal("PaymentCodes")) ? null : ((string) reader["PaymentCodes"]);
                    item.PayoutDate = reader.IsDBNull(reader.GetOrdinal("PayoutDate")) ? ((DateTime?) (nullable = null)) : ((DateTime?) reader["PayoutDate"]);
                    item.PaymentType = reader.IsDBNull(reader.GetOrdinal("PaymentType")) ? null : ((string) reader["PaymentType"]);
                    item.Payer = reader.IsDBNull(reader.GetOrdinal("Payer")) ? null : ((string) reader["Payer"]);
                    item.SupplyCode = reader.IsDBNull(reader.GetOrdinal("SupplyCode")) ? null : ((string) reader["SupplyCode"]);
                    item.BillNo = reader.IsDBNull(reader.GetOrdinal("BillNo")) ? null : ((string) reader["BillNo"]);
                    item.InvoNo = reader.IsDBNull(reader.GetOrdinal("InvoNo")) ? null : ((string) reader["InvoNo"]);
                    item.BankName = reader.IsDBNull(reader.GetOrdinal("BankName")) ? null : ((string) reader["BankName"]);
                    item.BankAccount = reader.IsDBNull(reader.GetOrdinal("BankAccount")) ? null : ((string) reader["BankAccount"]);
                    item.SubjectCode = reader.IsDBNull(reader.GetOrdinal("SubjectCode")) ? null : ((string) reader["SubjectCode"]);
                    item.Money = reader.IsDBNull(reader.GetOrdinal("Money")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["Money"]);
                    item.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? ((int?) (nullable3 = null)) : ((int?) reader["Status"]);
                    item.InputPerson = reader.IsDBNull(reader.GetOrdinal("InputPerson")) ? null : ((string) reader["InputPerson"]);
                    item.InputDate = reader.IsDBNull(reader.GetOrdinal("InputDate")) ? ((DateTime?) (nullable = null)) : ((DateTime?) reader["InputDate"]);
                    item.CheckPerson = reader.IsDBNull(reader.GetOrdinal("CheckPerson")) ? null : ((string) reader["CheckPerson"]);
                    item.CheckDate = reader.IsDBNull(reader.GetOrdinal("CheckDate")) ? ((DateTime?) (nullable = null)) : ((DateTime?) reader["CheckDate"]);
                    item.CheckOpinion = reader.IsDBNull(reader.GetOrdinal("CheckOpinion")) ? null : ((string) reader["CheckOpinion"]);
                    item.SupplyName = reader.IsDBNull(reader.GetOrdinal("SupplyName")) ? null : ((string) reader["SupplyName"]);
                    item.Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? null : ((string) reader["Remark"]);
                    item.ReceiptCount = reader.IsDBNull(reader.GetOrdinal("ReceiptCount")) ? ((int?) (nullable3 = null)) : ((int?) reader["ReceiptCount"]);
                    item.GroupCode = reader.IsDBNull(reader.GetOrdinal("GroupCode")) ? null : ((string) reader["GroupCode"]);
                    item.IsApportioned = reader.IsDBNull(reader.GetOrdinal("IsApportioned")) ? ((int?) (nullable3 = null)) : ((int?) reader["IsApportioned"]);
                    item.Cash = reader.IsDBNull(reader.GetOrdinal("Cash")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["Cash"]);
                    item.MoneyType = reader.IsDBNull(reader.GetOrdinal("MoneyType")) ? null : ((string) reader["MoneyType"]);
                    item.ExchangeRate = reader.IsDBNull(reader.GetOrdinal("ExchangeRate")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["ExchangeRate"]);
                    item.VoucherNo = reader.IsDBNull(reader.GetOrdinal("VoucherNo")) ? null : ((string) reader["VoucherNo"]);
                    item.SubjectSetCode = reader.IsDBNull(reader.GetOrdinal("SubjectSetCode")) ? null : ((string) reader["SubjectSetCode"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override Payout Get(TransactionManager transactionManager, PayoutKey key, int start, int pageLength)
        {
            return this.GetByPayoutCode(transactionManager, key.PayoutCode, start, pageLength);
        }

        public Payout GetByPayoutCode(string payoutCode)
        {
            int count = -1;
            return this.GetByPayoutCode(null, payoutCode, 0, 0x7fffffff, out count);
        }

        public Payout GetByPayoutCode(TransactionManager transactionManager, string payoutCode)
        {
            int count = -1;
            return this.GetByPayoutCode(transactionManager, payoutCode, 0, 0x7fffffff, out count);
        }

        public Payout GetByPayoutCode(string payoutCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByPayoutCode(null, payoutCode, start, pageLength, out count);
        }

        public Payout GetByPayoutCode(string payoutCode, int start, int pageLength, out int count)
        {
            return this.GetByPayoutCode(null, payoutCode, start, pageLength, out count);
        }

        public Payout GetByPayoutCode(TransactionManager transactionManager, string payoutCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByPayoutCode(transactionManager, payoutCode, start, pageLength, out count);
        }

        public abstract Payout GetByPayoutCode(TransactionManager transactionManager, string payoutCode, int start, int pageLength, out int count);
        public TList<Payout> GetByProjectCode(string projectCode)
        {
            int count = -1;
            return this.GetByProjectCode(projectCode, 0, 0x7fffffff, out count);
        }

        public TList<Payout> GetByProjectCode(TransactionManager transactionManager, string projectCode)
        {
            int count = -1;
            return this.GetByProjectCode(transactionManager, projectCode, 0, 0x7fffffff, out count);
        }

        public TList<Payout> GetByProjectCode(string projectCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByProjectCode(null, projectCode, start, pageLength, out count);
        }

        public TList<Payout> GetByProjectCode(string projectCode, int start, int pageLength, out int count)
        {
            return this.GetByProjectCode(null, projectCode, start, pageLength, out count);
        }

        public TList<Payout> GetByProjectCode(TransactionManager transactionManager, string projectCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByProjectCode(transactionManager, projectCode, start, pageLength, out count);
        }

        public abstract TList<Payout> GetByProjectCode(TransactionManager transactionManager, string projectCode, int start, int pageLength, out int count);
        /*public static void RefreshEntity(DataSet dataSet, Payout entity)
        {
            DateTime? nullable;
            decimal? nullable2;
            int? nullable3;
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.PayoutCode = (string) row["PayoutCode"];
            entity.OriginalPayoutCode = (string) row["PayoutCode"];
            entity.PayoutID = Convert.IsDBNull(row["PayoutID"]) ? null : ((string) row["PayoutID"]);
            entity.ProjectCode = Convert.IsDBNull(row["ProjectCode"]) ? null : ((string) row["ProjectCode"]);
            entity.PaymentCodes = Convert.IsDBNull(row["PaymentCodes"]) ? null : ((string) row["PaymentCodes"]);
            entity.PayoutDate = Convert.IsDBNull(row["PayoutDate"]) ? ((DateTime?) (nullable = null)) : ((DateTime?) row["PayoutDate"]);
            entity.PaymentType = Convert.IsDBNull(row["PaymentType"]) ? null : ((string) row["PaymentType"]);
            entity.Payer = Convert.IsDBNull(row["Payer"]) ? null : ((string) row["Payer"]);
            entity.SupplyCode = Convert.IsDBNull(row["SupplyCode"]) ? null : ((string) row["SupplyCode"]);
            entity.BillNo = Convert.IsDBNull(row["BillNo"]) ? null : ((string) row["BillNo"]);
            entity.InvoNo = Convert.IsDBNull(row["InvoNo"]) ? null : ((string) row["InvoNo"]);
            entity.BankName = Convert.IsDBNull(row["BankName"]) ? null : ((string) row["BankName"]);
            entity.BankAccount = Convert.IsDBNull(row["BankAccount"]) ? null : ((string) row["BankAccount"]);
            entity.SubjectCode = Convert.IsDBNull(row["SubjectCode"]) ? null : ((string) row["SubjectCode"]);
            entity.Money = Convert.IsDBNull(row["Money"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["Money"]);
            entity.Status = Convert.IsDBNull(row["Status"]) ? ((int?) (nullable3 = null)) : ((int?) row["Status"]);
            entity.InputPerson = Convert.IsDBNull(row["InputPerson"]) ? null : ((string) row["InputPerson"]);
            entity.InputDate = Convert.IsDBNull(row["InputDate"]) ? ((DateTime?) (nullable = null)) : ((DateTime?) row["InputDate"]);
            entity.CheckPerson = Convert.IsDBNull(row["CheckPerson"]) ? null : ((string) row["CheckPerson"]);
            entity.CheckDate = Convert.IsDBNull(row["CheckDate"]) ? null : ((DateTime?) row["CheckDate"]);
            entity.CheckOpinion = Convert.IsDBNull(row["CheckOpinion"]) ? null : ((string) row["CheckOpinion"]);
            entity.SupplyName = Convert.IsDBNull(row["SupplyName"]) ? null : ((string) row["SupplyName"]);
            entity.Remark = Convert.IsDBNull(row["Remark"]) ? null : ((string) row["Remark"]);
            entity.ReceiptCount = Convert.IsDBNull(row["ReceiptCount"]) ? ((int?) (nullable3 = null)) : ((int?) row["ReceiptCount"]);
            entity.GroupCode = Convert.IsDBNull(row["GroupCode"]) ? null : ((string) row["GroupCode"]);
            entity.IsApportioned = Convert.IsDBNull(row["IsApportioned"]) ? null : ((int?) row["IsApportioned"]);
            entity.Cash = Convert.IsDBNull(row["Cash"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["Cash"]);
            entity.MoneyType = Convert.IsDBNull(row["MoneyType"]) ? null : ((string) row["MoneyType"]);
            entity.ExchangeRate = Convert.IsDBNull(row["ExchangeRate"]) ? null : ((decimal?) row["ExchangeRate"]);
            entity.VoucherNo = Convert.IsDBNull(row["VoucherNo"]) ? null : ((string) row["VoucherNo"]);
            entity.SubjectSetCode = Convert.IsDBNull(row["SubjectSetCode"]) ? null : ((string) row["SubjectSetCode"]);
            entity.AcceptChanges();
        }*/

        public static void RefreshEntity(IDataReader reader, Payout entity)
        {
            if (reader.Read())
            {
                DateTime? nullable;
                decimal? nullable2;
                int? nullable3;
                entity.PayoutCode = (string) reader["PayoutCode"];
                entity.OriginalPayoutCode = (string) reader["PayoutCode"];
                entity.PayoutID = reader.IsDBNull(reader.GetOrdinal("PayoutID")) ? null : ((string) reader["PayoutID"]);
                entity.ProjectCode = reader.IsDBNull(reader.GetOrdinal("ProjectCode")) ? null : ((string) reader["ProjectCode"]);
                entity.PaymentCodes = reader.IsDBNull(reader.GetOrdinal("PaymentCodes")) ? null : ((string) reader["PaymentCodes"]);
                entity.PayoutDate = reader.IsDBNull(reader.GetOrdinal("PayoutDate")) ? ((DateTime?) (nullable = null)) : ((DateTime?) reader["PayoutDate"]);
                entity.PaymentType = reader.IsDBNull(reader.GetOrdinal("PaymentType")) ? null : ((string) reader["PaymentType"]);
                entity.Payer = reader.IsDBNull(reader.GetOrdinal("Payer")) ? null : ((string) reader["Payer"]);
                entity.SupplyCode = reader.IsDBNull(reader.GetOrdinal("SupplyCode")) ? null : ((string) reader["SupplyCode"]);
                entity.BillNo = reader.IsDBNull(reader.GetOrdinal("BillNo")) ? null : ((string) reader["BillNo"]);
                entity.InvoNo = reader.IsDBNull(reader.GetOrdinal("InvoNo")) ? null : ((string) reader["InvoNo"]);
                entity.BankName = reader.IsDBNull(reader.GetOrdinal("BankName")) ? null : ((string) reader["BankName"]);
                entity.BankAccount = reader.IsDBNull(reader.GetOrdinal("BankAccount")) ? null : ((string) reader["BankAccount"]);
                entity.SubjectCode = reader.IsDBNull(reader.GetOrdinal("SubjectCode")) ? null : ((string) reader["SubjectCode"]);
                entity.Money = reader.IsDBNull(reader.GetOrdinal("Money")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["Money"]);
                entity.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? ((int?) (nullable3 = null)) : ((int?) reader["Status"]);
                entity.InputPerson = reader.IsDBNull(reader.GetOrdinal("InputPerson")) ? null : ((string) reader["InputPerson"]);
                entity.InputDate = reader.IsDBNull(reader.GetOrdinal("InputDate")) ? ((DateTime?) (nullable = null)) : ((DateTime?) reader["InputDate"]);
                entity.CheckPerson = reader.IsDBNull(reader.GetOrdinal("CheckPerson")) ? null : ((string) reader["CheckPerson"]);
                entity.CheckDate = reader.IsDBNull(reader.GetOrdinal("CheckDate")) ? null : ((DateTime?) reader["CheckDate"]);
                entity.CheckOpinion = reader.IsDBNull(reader.GetOrdinal("CheckOpinion")) ? null : ((string) reader["CheckOpinion"]);
                entity.SupplyName = reader.IsDBNull(reader.GetOrdinal("SupplyName")) ? null : ((string) reader["SupplyName"]);
                entity.Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? null : ((string) reader["Remark"]);
                entity.ReceiptCount = reader.IsDBNull(reader.GetOrdinal("ReceiptCount")) ? ((int?) (nullable3 = null)) : ((int?) reader["ReceiptCount"]);
                entity.GroupCode = reader.IsDBNull(reader.GetOrdinal("GroupCode")) ? null : ((string) reader["GroupCode"]);
                entity.IsApportioned = reader.IsDBNull(reader.GetOrdinal("IsApportioned")) ? null : ((int?) reader["IsApportioned"]);
                entity.Cash = reader.IsDBNull(reader.GetOrdinal("Cash")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["Cash"]);
                entity.MoneyType = reader.IsDBNull(reader.GetOrdinal("MoneyType")) ? null : ((string) reader["MoneyType"]);
                entity.ExchangeRate = reader.IsDBNull(reader.GetOrdinal("ExchangeRate")) ? null : ((decimal?) reader["ExchangeRate"]);
                entity.VoucherNo = reader.IsDBNull(reader.GetOrdinal("VoucherNo")) ? null : ((string) reader["VoucherNo"]);
                entity.SubjectSetCode = reader.IsDBNull(reader.GetOrdinal("SubjectSetCode")) ? null : ((string) reader["SubjectSetCode"]);
                entity.AcceptChanges();
            }
        }
    }
}

