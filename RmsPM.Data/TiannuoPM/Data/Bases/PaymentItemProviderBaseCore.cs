namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;
    using System.Xml;
    public abstract class PaymentItemProviderBaseCore : EntityProviderBase<PaymentItem, PaymentItemKey>
    {
        protected PaymentItemProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, PaymentItem entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity != null)
            {
                object[] pkItems;
                if (base.CanDeepLoad(entity, "ContractCost", "ContractCostCodeSource", deepLoadType, innerList) && (entity.ContractCostCodeSource == null))
                {
                    pkItems = new object[] { entity.ContractCostCode ?? string.Empty };
                    ContractCost cost = EntityManager.LocateEntity<ContractCost>(EntityLocator.ConstructKeyFromPkItems(typeof(ContractCost), pkItems), DataRepository.Provider.EnableEntityTracking);
                    if (cost != null)
                    {
                        entity.ContractCostCodeSource = cost;
                    }
                    else
                    {
                        entity.ContractCostCodeSource = DataRepository.ContractCostProvider.GetByContractCostCode(entity.ContractCostCode ?? string.Empty);
                    }
                    if (deep && (entity.ContractCostCodeSource != null))
                    {
                        DataRepository.ContractCostProvider.DeepLoad(transactionManager, entity.ContractCostCodeSource, deep, deepLoadType, childTypes, innerList);
                    }
                }
                if (base.CanDeepLoad(entity, "Payment", "PaymentCodeSource", deepLoadType, innerList) && (entity.PaymentCodeSource == null))
                {
                    pkItems = new object[] { entity.PaymentCode ?? string.Empty };
                    Payment payment = EntityManager.LocateEntity<Payment>(EntityLocator.ConstructKeyFromPkItems(typeof(Payment), pkItems), DataRepository.Provider.EnableEntityTracking);
                    if (payment != null)
                    {
                        entity.PaymentCodeSource = payment;
                    }
                    else
                    {
                        entity.PaymentCodeSource = DataRepository.PaymentProvider.GetByPaymentCode(entity.PaymentCode ?? string.Empty);
                    }
                    if (deep && (entity.PaymentCodeSource != null))
                    {
                        DataRepository.PaymentProvider.DeepLoad(transactionManager, entity.PaymentCodeSource, deep, deepLoadType, childTypes, innerList);
                    }
                }
                if (base.CanDeepLoad(entity, "List<PayoutItem>", "PayoutItemCollection", deepLoadType, innerList))
                {
                    entity.PayoutItemCollection = DataRepository.PayoutItemProvider.GetByPaymentItemCode(transactionManager, entity.PaymentItemCode);
                    if (deep && (entity.PayoutItemCollection.Count > 0))
                    {
                        DataRepository.PayoutItemProvider.DeepLoad(transactionManager, entity.PayoutItemCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, PaymentItem entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity == null)
            {
                return false;
            }
            if (base.CanDeepSave(entity, "ContractCost", "ContractCostCodeSource", deepSaveType, innerList) && (entity.ContractCostCodeSource != null))
            {
                DataRepository.ContractCostProvider.Save(transactionManager, entity.ContractCostCodeSource);
                entity.ContractCostCode = entity.ContractCostCodeSource.ContractCostCode;
            }
            if (base.CanDeepSave(entity, "Payment", "PaymentCodeSource", deepSaveType, innerList) && (entity.PaymentCodeSource != null))
            {
                DataRepository.PaymentProvider.Save(transactionManager, entity.PaymentCodeSource);
                entity.PaymentCode = entity.PaymentCodeSource.PaymentCode;
            }
            this.Save(transactionManager, entity);
            if (base.CanDeepSave(entity, "List<PayoutItem>", "PayoutItemCollection", deepSaveType, innerList))
            {
                foreach (PayoutItem item in entity.PayoutItemCollection)
                {
                    item.PaymentItemCode = entity.PaymentItemCode;
                }
                if ((entity.PayoutItemCollection.Count > 0) || (entity.PayoutItemCollection.DeletedItems.Count > 0))
                {
                    DataRepository.PayoutItemProvider.DeepSave(transactionManager, entity.PayoutItemCollection, deepSaveType, childTypes, innerList);
                }
            }
            return true;
        }

        public bool Delete(string paymentItemCode)
        {
            return this.Delete(null, paymentItemCode);
        }

        public abstract bool Delete(TransactionManager transactionManager, string paymentItemCode);
        public override bool Delete(TransactionManager transactionManager, PaymentItemKey key)
        {
            return this.Delete(transactionManager, key.PaymentItemCode);
        }

        public static TList<PaymentItem> Fill(IDataReader reader, TList<PaymentItem> rows, int start, int pageLength)
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
                PaymentItem item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "PaymentItem" + (reader.IsDBNull(reader.GetOrdinal("PaymentItemCode")) ? string.Empty : ((string) reader["PaymentItemCode"])).ToString();
                    item = EntityManager.LocateOrCreate<PaymentItem>(text.ToString(), "PaymentItem", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new PaymentItem();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    decimal? nullable;
                    item.SuppressEntityEvents = true;
                    item.PaymentItemCode = (string) reader["PaymentItemCode"];
                    item.OriginalPaymentItemCode = item.PaymentItemCode;
                    item.PaymentCode = reader.IsDBNull(reader.GetOrdinal("PaymentCode")) ? null : ((string) reader["PaymentCode"]);
                    item.CostCode = reader.IsDBNull(reader.GetOrdinal("CostCode")) ? null : ((string) reader["CostCode"]);
                    item.PaymentType = reader.IsDBNull(reader.GetOrdinal("PaymentType")) ? null : ((string) reader["PaymentType"]);
                    item.ItemMoney = reader.IsDBNull(reader.GetOrdinal("ItemMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemMoney"]);
                    item.Summary = reader.IsDBNull(reader.GetOrdinal("Summary")) ? null : ((string) reader["Summary"]);
                    item.Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? null : ((string) reader["Remark"]);
                    item.AllocateCode = reader.IsDBNull(reader.GetOrdinal("AllocateCode")) ? null : ((string) reader["AllocateCode"]);
                    item.OldItemMoney = reader.IsDBNull(reader.GetOrdinal("OldItemMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["OldItemMoney"]);
                    item.AlloType = reader.IsDBNull(reader.GetOrdinal("AlloType")) ? null : ((string) reader["AlloType"]);
                    item.CostBudgetSetCode = reader.IsDBNull(reader.GetOrdinal("CostBudgetSetCode")) ? null : ((string) reader["CostBudgetSetCode"]);
                    item.PBSType = reader.IsDBNull(reader.GetOrdinal("PBSType")) ? null : ((string) reader["PBSType"]);
                    item.PBSCode = reader.IsDBNull(reader.GetOrdinal("PBSCode")) ? null : ((string) reader["PBSCode"]);
                    item.Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : ((string) reader["Description"]);
                    item.ContractCostCode = reader.IsDBNull(reader.GetOrdinal("ContractCostCode")) ? null : ((string) reader["ContractCostCode"]);
                    item.ItemCash = reader.IsDBNull(reader.GetOrdinal("ItemCash")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash"]);
                    item.MoneyType = reader.IsDBNull(reader.GetOrdinal("MoneyType")) ? null : ((string) reader["MoneyType"]);
                    item.ExchangeRate = reader.IsDBNull(reader.GetOrdinal("ExchangeRate")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ExchangeRate"]);
                    item.ItemCash0 = reader.IsDBNull(reader.GetOrdinal("ItemCash0")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash0"]);
                    item.ItemCash1 = reader.IsDBNull(reader.GetOrdinal("ItemCash1")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash1"]);
                    item.ItemCash2 = reader.IsDBNull(reader.GetOrdinal("ItemCash2")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash2"]);
                    item.ItemCash3 = reader.IsDBNull(reader.GetOrdinal("ItemCash3")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash3"]);
                    item.ItemCash4 = reader.IsDBNull(reader.GetOrdinal("ItemCash4")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash4"]);
                    item.ItemCash5 = reader.IsDBNull(reader.GetOrdinal("ItemCash5")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash5"]);
                    item.ItemCash6 = reader.IsDBNull(reader.GetOrdinal("ItemCash6")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash6"]);
                    item.ItemCash7 = reader.IsDBNull(reader.GetOrdinal("ItemCash7")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash7"]);
                    item.ItemCash8 = reader.IsDBNull(reader.GetOrdinal("ItemCash8")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash8"]);
                    item.ItemCash9 = reader.IsDBNull(reader.GetOrdinal("ItemCash9")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash9"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override PaymentItem Get(TransactionManager transactionManager, PaymentItemKey key, int start, int pageLength)
        {
            return this.GetByPaymentItemCode(transactionManager, key.PaymentItemCode, start, pageLength);
        }

        public TList<PaymentItem> GetByContractCostCode(string contractCostCode)
        {
            int count = -1;
            return this.GetByContractCostCode(contractCostCode, 0, 0x7fffffff, out count);
        }

        public TList<PaymentItem> GetByContractCostCode(TransactionManager transactionManager, string contractCostCode)
        {
            int count = -1;
            return this.GetByContractCostCode(transactionManager, contractCostCode, 0, 0x7fffffff, out count);
        }

        public TList<PaymentItem> GetByContractCostCode(string contractCostCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCostCode(null, contractCostCode, start, pageLength, out count);
        }

        public TList<PaymentItem> GetByContractCostCode(string contractCostCode, int start, int pageLength, out int count)
        {
            return this.GetByContractCostCode(null, contractCostCode, start, pageLength, out count);
        }

        public TList<PaymentItem> GetByContractCostCode(TransactionManager transactionManager, string contractCostCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCostCode(transactionManager, contractCostCode, start, pageLength, out count);
        }

        public abstract TList<PaymentItem> GetByContractCostCode(TransactionManager transactionManager, string contractCostCode, int start, int pageLength, out int count);
        public TList<PaymentItem> GetByPaymentCode(string paymentCode)
        {
            int count = -1;
            return this.GetByPaymentCode(paymentCode, 0, 0x7fffffff, out count);
        }

        public TList<PaymentItem> GetByPaymentCode(TransactionManager transactionManager, string paymentCode)
        {
            int count = -1;
            return this.GetByPaymentCode(transactionManager, paymentCode, 0, 0x7fffffff, out count);
        }

        public TList<PaymentItem> GetByPaymentCode(string paymentCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByPaymentCode(null, paymentCode, start, pageLength, out count);
        }

        public TList<PaymentItem> GetByPaymentCode(string paymentCode, int start, int pageLength, out int count)
        {
            return this.GetByPaymentCode(null, paymentCode, start, pageLength, out count);
        }

        public TList<PaymentItem> GetByPaymentCode(TransactionManager transactionManager, string paymentCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByPaymentCode(transactionManager, paymentCode, start, pageLength, out count);
        }

        public abstract TList<PaymentItem> GetByPaymentCode(TransactionManager transactionManager, string paymentCode, int start, int pageLength, out int count);
        public PaymentItem GetByPaymentItemCode(string paymentItemCode)
        {
            int count = -1;
            return this.GetByPaymentItemCode(null, paymentItemCode, 0, 0x7fffffff, out count);
        }

        public PaymentItem GetByPaymentItemCode(TransactionManager transactionManager, string paymentItemCode)
        {
            int count = -1;
            return this.GetByPaymentItemCode(transactionManager, paymentItemCode, 0, 0x7fffffff, out count);
        }

        public PaymentItem GetByPaymentItemCode(string paymentItemCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByPaymentItemCode(null, paymentItemCode, start, pageLength, out count);
        }

        public PaymentItem GetByPaymentItemCode(string paymentItemCode, int start, int pageLength, out int count)
        {
            return this.GetByPaymentItemCode(null, paymentItemCode, start, pageLength, out count);
        }

        public PaymentItem GetByPaymentItemCode(TransactionManager transactionManager, string paymentItemCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByPaymentItemCode(transactionManager, paymentItemCode, start, pageLength, out count);
        }

        public abstract PaymentItem GetByPaymentItemCode(TransactionManager transactionManager, string paymentItemCode, int start, int pageLength, out int count);
        
        public static void RefreshEntity(DataSet dataSet, PaymentItem entity)
        {
            decimal? nullable;
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.PaymentItemCode = (string) row["PaymentItemCode"];
            entity.OriginalPaymentItemCode = (string) row["PaymentItemCode"];
            entity.PaymentCode = Convert.IsDBNull(row["PaymentCode"]) ? null : ((string) row["PaymentCode"]);
            entity.CostCode = Convert.IsDBNull(row["CostCode"]) ? null : ((string) row["CostCode"]);
            entity.PaymentType = Convert.IsDBNull(row["PaymentType"]) ? null : ((string) row["PaymentType"]);
            entity.ItemMoney = Convert.IsDBNull(row["ItemMoney"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ItemMoney"]);
            entity.Summary = Convert.IsDBNull(row["Summary"]) ? null : ((string) row["Summary"]);
            entity.Remark = Convert.IsDBNull(row["Remark"]) ? null : ((string) row["Remark"]);
            entity.AllocateCode = Convert.IsDBNull(row["AllocateCode"]) ? null : ((string) row["AllocateCode"]);
            entity.OldItemMoney = Convert.IsDBNull(row["OldItemMoney"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["OldItemMoney"]);
            entity.AlloType = Convert.IsDBNull(row["AlloType"]) ? null : ((string) row["AlloType"]);
            entity.CostBudgetSetCode = Convert.IsDBNull(row["CostBudgetSetCode"]) ? null : ((string) row["CostBudgetSetCode"]);
            entity.PBSType = Convert.IsDBNull(row["PBSType"]) ? null : ((string) row["PBSType"]);
            entity.PBSCode = Convert.IsDBNull(row["PBSCode"]) ? null : ((string) row["PBSCode"]);
            entity.Description = Convert.IsDBNull(row["Description"]) ? null : ((string) row["Description"]);
            entity.ContractCostCode = Convert.IsDBNull(row["ContractCostCode"]) ? null : ((string) row["ContractCostCode"]);
            entity.ItemCash = Convert.IsDBNull(row["ItemCash"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ItemCash"]);
            entity.MoneyType = Convert.IsDBNull(row["MoneyType"]) ? null : ((string) row["MoneyType"]);
            entity.ExchangeRate = Convert.IsDBNull(row["ExchangeRate"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ExchangeRate"]);
            entity.ItemCash0 = Convert.IsDBNull(row["ItemCash0"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ItemCash0"]);
            entity.ItemCash1 = Convert.IsDBNull(row["ItemCash1"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ItemCash1"]);
            entity.ItemCash2 = Convert.IsDBNull(row["ItemCash2"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ItemCash2"]);
            entity.ItemCash3 = Convert.IsDBNull(row["ItemCash3"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ItemCash3"]);
            entity.ItemCash4 = Convert.IsDBNull(row["ItemCash4"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ItemCash4"]);
            entity.ItemCash5 = Convert.IsDBNull(row["ItemCash5"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ItemCash5"]);
            entity.ItemCash6 = Convert.IsDBNull(row["ItemCash6"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ItemCash6"]);
            entity.ItemCash7 = Convert.IsDBNull(row["ItemCash7"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ItemCash7"]);
            entity.ItemCash8 = Convert.IsDBNull(row["ItemCash8"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ItemCash8"]);
            entity.ItemCash9 = Convert.IsDBNull(row["ItemCash9"]) ? null : ((decimal?) row["ItemCash9"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, PaymentItem entity)
        {
            if (reader.Read())
            {
                decimal? nullable;
                entity.PaymentItemCode = (string) reader["PaymentItemCode"];
                entity.OriginalPaymentItemCode = (string) reader["PaymentItemCode"];
                entity.PaymentCode = reader.IsDBNull(reader.GetOrdinal("PaymentCode")) ? null : ((string) reader["PaymentCode"]);
                entity.CostCode = reader.IsDBNull(reader.GetOrdinal("CostCode")) ? null : ((string) reader["CostCode"]);
                entity.PaymentType = reader.IsDBNull(reader.GetOrdinal("PaymentType")) ? null : ((string) reader["PaymentType"]);
                entity.ItemMoney = reader.IsDBNull(reader.GetOrdinal("ItemMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemMoney"]);
                entity.Summary = reader.IsDBNull(reader.GetOrdinal("Summary")) ? null : ((string) reader["Summary"]);
                entity.Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? null : ((string) reader["Remark"]);
                entity.AllocateCode = reader.IsDBNull(reader.GetOrdinal("AllocateCode")) ? null : ((string) reader["AllocateCode"]);
                entity.OldItemMoney = reader.IsDBNull(reader.GetOrdinal("OldItemMoney")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["OldItemMoney"]);
                entity.AlloType = reader.IsDBNull(reader.GetOrdinal("AlloType")) ? null : ((string) reader["AlloType"]);
                entity.CostBudgetSetCode = reader.IsDBNull(reader.GetOrdinal("CostBudgetSetCode")) ? null : ((string) reader["CostBudgetSetCode"]);
                entity.PBSType = reader.IsDBNull(reader.GetOrdinal("PBSType")) ? null : ((string) reader["PBSType"]);
                entity.PBSCode = reader.IsDBNull(reader.GetOrdinal("PBSCode")) ? null : ((string) reader["PBSCode"]);
                entity.Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : ((string) reader["Description"]);
                entity.ContractCostCode = reader.IsDBNull(reader.GetOrdinal("ContractCostCode")) ? null : ((string) reader["ContractCostCode"]);
                entity.ItemCash = reader.IsDBNull(reader.GetOrdinal("ItemCash")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash"]);
                entity.MoneyType = reader.IsDBNull(reader.GetOrdinal("MoneyType")) ? null : ((string) reader["MoneyType"]);
                entity.ExchangeRate = reader.IsDBNull(reader.GetOrdinal("ExchangeRate")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ExchangeRate"]);
                entity.ItemCash0 = reader.IsDBNull(reader.GetOrdinal("ItemCash0")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash0"]);
                entity.ItemCash1 = reader.IsDBNull(reader.GetOrdinal("ItemCash1")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash1"]);
                entity.ItemCash2 = reader.IsDBNull(reader.GetOrdinal("ItemCash2")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash2"]);
                entity.ItemCash3 = reader.IsDBNull(reader.GetOrdinal("ItemCash3")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash3"]);
                entity.ItemCash4 = reader.IsDBNull(reader.GetOrdinal("ItemCash4")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash4"]);
                entity.ItemCash5 = reader.IsDBNull(reader.GetOrdinal("ItemCash5")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash5"]);
                entity.ItemCash6 = reader.IsDBNull(reader.GetOrdinal("ItemCash6")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash6"]);
                entity.ItemCash7 = reader.IsDBNull(reader.GetOrdinal("ItemCash7")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash7"]);
                entity.ItemCash8 = reader.IsDBNull(reader.GetOrdinal("ItemCash8")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ItemCash8"]);
                entity.ItemCash9 = reader.IsDBNull(reader.GetOrdinal("ItemCash9")) ? null : ((decimal?) reader["ItemCash9"]);
                entity.AcceptChanges();
            }
        }
    }
}

