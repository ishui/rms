namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class PayoutItemProviderBaseCore : EntityProviderBase<PayoutItem, PayoutItemKey>
    {
        protected PayoutItemProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, PayoutItem entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity != null)
            {
                object[] pkItems;
                if (base.CanDeepLoad(entity, "Payout", "PayoutCodeSource", deepLoadType, innerList) && (entity.PayoutCodeSource == null))
                {
                    pkItems = new object[] { entity.PayoutCode ?? string.Empty };
                    Payout payout = EntityManager.LocateEntity<Payout>(EntityLocator.ConstructKeyFromPkItems(typeof(Payout), pkItems), DataRepository.Provider.EnableEntityTracking);
                    if (payout != null)
                    {
                        entity.PayoutCodeSource = payout;
                    }
                    else
                    {
                        entity.PayoutCodeSource = DataRepository.PayoutProvider.GetByPayoutCode(entity.PayoutCode ?? string.Empty);
                    }
                    if (deep && (entity.PayoutCodeSource != null))
                    {
                        DataRepository.PayoutProvider.DeepLoad(transactionManager, entity.PayoutCodeSource, deep, deepLoadType, childTypes, innerList);
                    }
                }
                if (base.CanDeepLoad(entity, "PaymentItem", "PaymentItemCodeSource", deepLoadType, innerList) && (entity.PaymentItemCodeSource == null))
                {
                    pkItems = new object[] { entity.PaymentItemCode ?? string.Empty };
                    PaymentItem item = EntityManager.LocateEntity<PaymentItem>(EntityLocator.ConstructKeyFromPkItems(typeof(PaymentItem), pkItems), DataRepository.Provider.EnableEntityTracking);
                    if (item != null)
                    {
                        entity.PaymentItemCodeSource = item;
                    }
                    else
                    {
                        entity.PaymentItemCodeSource = DataRepository.PaymentItemProvider.GetByPaymentItemCode(entity.PaymentItemCode ?? string.Empty);
                    }
                    if (deep && (entity.PaymentItemCodeSource != null))
                    {
                        DataRepository.PaymentItemProvider.DeepLoad(transactionManager, entity.PaymentItemCodeSource, deep, deepLoadType, childTypes, innerList);
                    }
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, PayoutItem entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity == null)
            {
                return false;
            }
            if (base.CanDeepSave(entity, "Payout", "PayoutCodeSource", deepSaveType, innerList) && (entity.PayoutCodeSource != null))
            {
                DataRepository.PayoutProvider.Save(transactionManager, entity.PayoutCodeSource);
                entity.PayoutCode = entity.PayoutCodeSource.PayoutCode;
            }
            if (base.CanDeepSave(entity, "PaymentItem", "PaymentItemCodeSource", deepSaveType, innerList) && (entity.PaymentItemCodeSource != null))
            {
                DataRepository.PaymentItemProvider.Save(transactionManager, entity.PaymentItemCodeSource);
                entity.PaymentItemCode = entity.PaymentItemCodeSource.PaymentItemCode;
            }
            this.Save(transactionManager, entity);
            return true;
        }

        public bool Delete(string payoutItemCode)
        {
            return this.Delete(null, payoutItemCode);
        }

        public abstract bool Delete(TransactionManager transactionManager, string payoutItemCode);
        public override bool Delete(TransactionManager transactionManager, PayoutItemKey key)
        {
            return this.Delete(transactionManager, key.PayoutItemCode);
        }

        public static TList<PayoutItem> Fill(IDataReader reader, TList<PayoutItem> rows, int start, int pageLength)
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
                PayoutItem item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "PayoutItem" + (reader.IsDBNull(reader.GetOrdinal("PayoutItemCode")) ? string.Empty : ((string) reader["PayoutItemCode"])).ToString();
                    item = EntityManager.LocateOrCreate<PayoutItem>(text.ToString(), "PayoutItem", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new PayoutItem();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    decimal? nullable;
                    item.SuppressEntityEvents = true;
                    item.PayoutItemCode = (string) reader["PayoutItemCode"];
                    item.OriginalPayoutItemCode = item.PayoutItemCode;
                    item.PayoutCode = reader.IsDBNull(reader.GetOrdinal("PayoutCode")) ? null : ((string) reader["PayoutCode"]);
                    item.PaymentItemCode = reader.IsDBNull(reader.GetOrdinal("PaymentItemCode")) ? null : ((string) reader["PaymentItemCode"]);
                    item.PayoutMoney = reader.IsDBNull(reader.GetOrdinal("PayoutMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["PayoutMoney"]);
                    item.SubjectCode = reader.IsDBNull(reader.GetOrdinal("SubjectCode")) ? null : ((string) reader["SubjectCode"]);
                    item.Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? null : ((string) reader["Remark"]);
                    item.AlloType = reader.IsDBNull(reader.GetOrdinal("AlloType")) ? null : ((string) reader["AlloType"]);
                    item.IsManualAlloc = reader.IsDBNull(reader.GetOrdinal("IsManualAlloc")) ? null : ((int?) reader["IsManualAlloc"]);
                    item.PayoutCash = reader.IsDBNull(reader.GetOrdinal("PayoutCash")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["PayoutCash"]);
                    item.MoneyType = reader.IsDBNull(reader.GetOrdinal("MoneyType")) ? null : ((string) reader["MoneyType"]);
                    item.ExchangeRate = reader.IsDBNull(reader.GetOrdinal("ExchangeRate")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ExchangeRate"]);
                    item.PayoutMoneyType = reader.IsDBNull(reader.GetOrdinal("PayoutMoneyType")) ? null : ((string) reader["PayoutMoneyType"]);
                    item.PayoutExchangeRate = reader.IsDBNull(reader.GetOrdinal("PayoutExchangeRate")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["PayoutExchangeRate"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override PayoutItem Get(TransactionManager transactionManager, PayoutItemKey key, int start, int pageLength)
        {
            return this.GetByPayoutItemCode(transactionManager, key.PayoutItemCode, start, pageLength);
        }

        public TList<PayoutItem> GetByPaymentItemCode(string paymentItemCode)
        {
            int count = -1;
            return this.GetByPaymentItemCode(paymentItemCode, 0, 0x7fffffff, out count);
        }

        public TList<PayoutItem> GetByPaymentItemCode(TransactionManager transactionManager, string paymentItemCode)
        {
            int count = -1;
            return this.GetByPaymentItemCode(transactionManager, paymentItemCode, 0, 0x7fffffff, out count);
        }

        public TList<PayoutItem> GetByPaymentItemCode(string paymentItemCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByPaymentItemCode(null, paymentItemCode, start, pageLength, out count);
        }

        public TList<PayoutItem> GetByPaymentItemCode(string paymentItemCode, int start, int pageLength, out int count)
        {
            return this.GetByPaymentItemCode(null, paymentItemCode, start, pageLength, out count);
        }

        public TList<PayoutItem> GetByPaymentItemCode(TransactionManager transactionManager, string paymentItemCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByPaymentItemCode(transactionManager, paymentItemCode, start, pageLength, out count);
        }

        public abstract TList<PayoutItem> GetByPaymentItemCode(TransactionManager transactionManager, string paymentItemCode, int start, int pageLength, out int count);
        public TList<PayoutItem> GetByPayoutCode(string payoutCode)
        {
            int count = -1;
            return this.GetByPayoutCode(payoutCode, 0, 0x7fffffff, out count);
        }

        public TList<PayoutItem> GetByPayoutCode(TransactionManager transactionManager, string payoutCode)
        {
            int count = -1;
            return this.GetByPayoutCode(transactionManager, payoutCode, 0, 0x7fffffff, out count);
        }

        public TList<PayoutItem> GetByPayoutCode(string payoutCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByPayoutCode(null, payoutCode, start, pageLength, out count);
        }

        public TList<PayoutItem> GetByPayoutCode(string payoutCode, int start, int pageLength, out int count)
        {
            return this.GetByPayoutCode(null, payoutCode, start, pageLength, out count);
        }

        public TList<PayoutItem> GetByPayoutCode(TransactionManager transactionManager, string payoutCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByPayoutCode(transactionManager, payoutCode, start, pageLength, out count);
        }

        public abstract TList<PayoutItem> GetByPayoutCode(TransactionManager transactionManager, string payoutCode, int start, int pageLength, out int count);
        public PayoutItem GetByPayoutItemCode(string payoutItemCode)
        {
            int count = -1;
            return this.GetByPayoutItemCode(null, payoutItemCode, 0, 0x7fffffff, out count);
        }

        public PayoutItem GetByPayoutItemCode(TransactionManager transactionManager, string payoutItemCode)
        {
            int count = -1;
            return this.GetByPayoutItemCode(transactionManager, payoutItemCode, 0, 0x7fffffff, out count);
        }

        public PayoutItem GetByPayoutItemCode(string payoutItemCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByPayoutItemCode(null, payoutItemCode, start, pageLength, out count);
        }

        public PayoutItem GetByPayoutItemCode(string payoutItemCode, int start, int pageLength, out int count)
        {
            return this.GetByPayoutItemCode(null, payoutItemCode, start, pageLength, out count);
        }

        public PayoutItem GetByPayoutItemCode(TransactionManager transactionManager, string payoutItemCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByPayoutItemCode(transactionManager, payoutItemCode, start, pageLength, out count);
        }

        public abstract PayoutItem GetByPayoutItemCode(TransactionManager transactionManager, string payoutItemCode, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, PayoutItem entity)
        {
            decimal? nullable;
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.PayoutItemCode = (string) row["PayoutItemCode"];
            entity.OriginalPayoutItemCode = (string) row["PayoutItemCode"];
            entity.PayoutCode = Convert.IsDBNull(row["PayoutCode"]) ? null : ((string) row["PayoutCode"]);
            entity.PaymentItemCode = Convert.IsDBNull(row["PaymentItemCode"]) ? null : ((string) row["PaymentItemCode"]);
            entity.PayoutMoney = Convert.IsDBNull(row["PayoutMoney"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["PayoutMoney"]);
            entity.SubjectCode = Convert.IsDBNull(row["SubjectCode"]) ? null : ((string) row["SubjectCode"]);
            entity.Remark = Convert.IsDBNull(row["Remark"]) ? null : ((string) row["Remark"]);
            entity.AlloType = Convert.IsDBNull(row["AlloType"]) ? null : ((string) row["AlloType"]);
            entity.IsManualAlloc = Convert.IsDBNull(row["IsManualAlloc"]) ? null : ((int?) row["IsManualAlloc"]);
            entity.PayoutCash = Convert.IsDBNull(row["PayoutCash"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["PayoutCash"]);
            entity.MoneyType = Convert.IsDBNull(row["MoneyType"]) ? null : ((string) row["MoneyType"]);
            entity.ExchangeRate = Convert.IsDBNull(row["ExchangeRate"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ExchangeRate"]);
            entity.PayoutMoneyType = Convert.IsDBNull(row["PayoutMoneyType"]) ? null : ((string) row["PayoutMoneyType"]);
            entity.PayoutExchangeRate = Convert.IsDBNull(row["PayoutExchangeRate"]) ? null : ((decimal?) row["PayoutExchangeRate"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, PayoutItem entity)
        {
            if (reader.Read())
            {
                decimal? nullable;
                entity.PayoutItemCode = (string) reader["PayoutItemCode"];
                entity.OriginalPayoutItemCode = (string) reader["PayoutItemCode"];
                entity.PayoutCode = reader.IsDBNull(reader.GetOrdinal("PayoutCode")) ? null : ((string) reader["PayoutCode"]);
                entity.PaymentItemCode = reader.IsDBNull(reader.GetOrdinal("PaymentItemCode")) ? null : ((string) reader["PaymentItemCode"]);
                entity.PayoutMoney = reader.IsDBNull(reader.GetOrdinal("PayoutMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["PayoutMoney"]);
                entity.SubjectCode = reader.IsDBNull(reader.GetOrdinal("SubjectCode")) ? null : ((string) reader["SubjectCode"]);
                entity.Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? null : ((string) reader["Remark"]);
                entity.AlloType = reader.IsDBNull(reader.GetOrdinal("AlloType")) ? null : ((string) reader["AlloType"]);
                entity.IsManualAlloc = reader.IsDBNull(reader.GetOrdinal("IsManualAlloc")) ? null : ((int?) reader["IsManualAlloc"]);
                entity.PayoutCash = reader.IsDBNull(reader.GetOrdinal("PayoutCash")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["PayoutCash"]);
                entity.MoneyType = reader.IsDBNull(reader.GetOrdinal("MoneyType")) ? null : ((string) reader["MoneyType"]);
                entity.ExchangeRate = reader.IsDBNull(reader.GetOrdinal("ExchangeRate")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ExchangeRate"]);
                entity.PayoutMoneyType = reader.IsDBNull(reader.GetOrdinal("PayoutMoneyType")) ? null : ((string) reader["PayoutMoneyType"]);
                entity.PayoutExchangeRate = reader.IsDBNull(reader.GetOrdinal("PayoutExchangeRate")) ? null : ((decimal?) reader["PayoutExchangeRate"]);
                entity.AcceptChanges();
            }
        }
    }
}

