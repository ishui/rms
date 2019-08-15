namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class PaymentProviderBaseCore : EntityProviderBase<Payment, PaymentKey>
    {
        protected PaymentProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, Payment entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
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
                if (base.CanDeepLoad(entity, "List<PaymentItem>", "PaymentItemCollection", deepLoadType, innerList))
                {
                    entity.PaymentItemCollection = DataRepository.PaymentItemProvider.GetByPaymentCode(transactionManager, entity.PaymentCode);
                    if (deep && (entity.PaymentItemCollection.Count > 0))
                    {
                        DataRepository.PaymentItemProvider.DeepLoad(transactionManager, entity.PaymentItemCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, Payment entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
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
            if (base.CanDeepSave(entity, "Contract", "ContractCodeSource", deepSaveType, innerList) && (entity.ContractCodeSource != null))
            {
                DataRepository.ContractProvider.Save(transactionManager, entity.ContractCodeSource);
                entity.ContractCode = entity.ContractCodeSource.ContractCode;
            }
            this.Save(transactionManager, entity);
            if (base.CanDeepSave(entity, "List<PaymentItem>", "PaymentItemCollection", deepSaveType, innerList))
            {
                foreach (PaymentItem item in entity.PaymentItemCollection)
                {
                    item.PaymentCode = entity.PaymentCode;
                }
                if ((entity.PaymentItemCollection.Count > 0) || (entity.PaymentItemCollection.DeletedItems.Count > 0))
                {
                    DataRepository.PaymentItemProvider.DeepSave(transactionManager, entity.PaymentItemCollection, deepSaveType, childTypes, innerList);
                }
            }
            return true;
        }

        public bool Delete(string paymentCode)
        {
            return this.Delete(null, paymentCode);
        }

        public abstract bool Delete(TransactionManager transactionManager, string paymentCode);
        public override bool Delete(TransactionManager transactionManager, PaymentKey key)
        {
            return this.Delete(transactionManager, key.PaymentCode);
        }

        public static TList<Payment> Fill(IDataReader reader, TList<Payment> rows, int start, int pageLength)
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
                Payment item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "Payment" + (reader.IsDBNull(reader.GetOrdinal("PaymentCode")) ? string.Empty : ((string) reader["PaymentCode"])).ToString();
                    item = EntityManager.LocateOrCreate<Payment>(text.ToString(), "Payment", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new Payment();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    int? nullable;
                    DateTime? nullable2;
                    decimal? nullable3;
                    item.SuppressEntityEvents = true;
                    item.PaymentCode = (string) reader["PaymentCode"];
                    item.OriginalPaymentCode = item.PaymentCode;
                    item.PaymentTitle = reader.IsDBNull(reader.GetOrdinal("PaymentTitle")) ? null : ((string) reader["PaymentTitle"]);
                    item.PaymentID = reader.IsDBNull(reader.GetOrdinal("PaymentID")) ? null : ((string) reader["PaymentID"]);
                    item.VoucherID = reader.IsDBNull(reader.GetOrdinal("VoucherID")) ? null : ((string) reader["VoucherID"]);
                    item.RecieptCount = reader.IsDBNull(reader.GetOrdinal("RecieptCount")) ? ((int?) (nullable = null)) : ((int?) reader["RecieptCount"]);
                    item.ProjectCode = reader.IsDBNull(reader.GetOrdinal("ProjectCode")) ? null : ((string) reader["ProjectCode"]);
                    item.ApplyPerson = reader.IsDBNull(reader.GetOrdinal("ApplyPerson")) ? null : ((string) reader["ApplyPerson"]);
                    item.ApplyDate = reader.IsDBNull(reader.GetOrdinal("ApplyDate")) ? ((DateTime?) (nullable2 = null)) : ((DateTime?) reader["ApplyDate"]);
                    item.Accountant = reader.IsDBNull(reader.GetOrdinal("Accountant")) ? null : ((string) reader["Accountant"]);
                    item.AccountDate = reader.IsDBNull(reader.GetOrdinal("AccountDate")) ? ((DateTime?) (nullable2 = null)) : ((DateTime?) reader["AccountDate"]);
                    item.Payer = reader.IsDBNull(reader.GetOrdinal("Payer")) ? null : ((string) reader["Payer"]);
                    item.PayDate = reader.IsDBNull(reader.GetOrdinal("PayDate")) ? ((DateTime?) (nullable2 = null)) : ((DateTime?) reader["PayDate"]);
                    item.Purpose = reader.IsDBNull(reader.GetOrdinal("Purpose")) ? null : ((string) reader["Purpose"]);
                    item.Money = reader.IsDBNull(reader.GetOrdinal("Money")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["Money"]);
                    item.CheckPerson = reader.IsDBNull(reader.GetOrdinal("CheckPerson")) ? null : ((string) reader["CheckPerson"]);
                    item.CheckDate = reader.IsDBNull(reader.GetOrdinal("CheckDate")) ? ((DateTime?) (nullable2 = null)) : ((DateTime?) reader["CheckDate"]);
                    item.CheckOpinion = reader.IsDBNull(reader.GetOrdinal("CheckOpinion")) ? null : ((string) reader["CheckOpinion"]);
                    item.IsContract = reader.IsDBNull(reader.GetOrdinal("IsContract")) ? ((int?) (nullable = null)) : ((int?) reader["IsContract"]);
                    item.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                    item.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? ((int?) (nullable = null)) : ((int?) reader["Status"]);
                    item.WBSCode = reader.IsDBNull(reader.GetOrdinal("WBSCode")) ? null : ((string) reader["WBSCode"]);
                    item.IsApportion = reader.IsDBNull(reader.GetOrdinal("IsApportion")) ? ((int?) (nullable = null)) : ((int?) reader["IsApportion"]);
                    item.SupplyCode = reader.IsDBNull(reader.GetOrdinal("SupplyCode")) ? null : ((string) reader["SupplyCode"]);
                    item.UnitCode = reader.IsDBNull(reader.GetOrdinal("UnitCode")) ? null : ((string) reader["UnitCode"]);
                    item.SupplyName = reader.IsDBNull(reader.GetOrdinal("SupplyName")) ? null : ((string) reader["SupplyName"]);
                    item.Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? null : ((string) reader["Remark"]);
                    item.OldMoney = reader.IsDBNull(reader.GetOrdinal("OldMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["OldMoney"]);
                    item.GroupCode = reader.IsDBNull(reader.GetOrdinal("GroupCode")) ? null : ((string) reader["GroupCode"]);
                    item.BankName = reader.IsDBNull(reader.GetOrdinal("BankName")) ? null : ((string) reader["BankName"]);
                    item.BankAccount = reader.IsDBNull(reader.GetOrdinal("BankAccount")) ? null : ((string) reader["BankAccount"]);
                    item.OtherAttachMent = reader.IsDBNull(reader.GetOrdinal("OtherAttachMent")) ? null : ((string) reader["OtherAttachMent"]);
                    item.PayType = reader.IsDBNull(reader.GetOrdinal("PayType")) ? ((int?) (nullable = null)) : ((int?) reader["PayType"]);
                    item.Issue = reader.IsDBNull(reader.GetOrdinal("Issue")) ? ((int?) (nullable = null)) : ((int?) reader["Issue"]);
                    item.IssueMode = reader.IsDBNull(reader.GetOrdinal("IssueMode")) ? null : ((string) reader["IssueMode"]);
                    item.FactPayDate = reader.IsDBNull(reader.GetOrdinal("FactPayDate")) ? ((DateTime?) (nullable2 = null)) : ((DateTime?) reader["FactPayDate"]);
                    item.TotalPayMoney = reader.IsDBNull(reader.GetOrdinal("TotalPayMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["TotalPayMoney"]);
                    item.AHMoney = reader.IsDBNull(reader.GetOrdinal("AHMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHMoney"]);
                    item.APMoney = reader.IsDBNull(reader.GetOrdinal("APMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["APMoney"]);
                    item.UPMoney = reader.IsDBNull(reader.GetOrdinal("UPMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["UPMoney"]);
                    item.SupplierApplyMoney = reader.IsDBNull(reader.GetOrdinal("SupplierApplyMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["SupplierApplyMoney"]);
                    item.MaxPayMoney = reader.IsDBNull(reader.GetOrdinal("MaxPayMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["MaxPayMoney"]);
                    item.PlanPayMoney = reader.IsDBNull(reader.GetOrdinal("PlanPayMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["PlanPayMoney"]);
                    item.ContractMoney = reader.IsDBNull(reader.GetOrdinal("ContractMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["ContractMoney"]);
                    item.AdjustedContractMoney = reader.IsDBNull(reader.GetOrdinal("AdjustedContractMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AdjustedContractMoney"]);
                    item.PayoutMoney = reader.IsDBNull(reader.GetOrdinal("PayoutMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["PayoutMoney"]);
                    item.AHCash = reader.IsDBNull(reader.GetOrdinal("AHCash")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash"]);
                    item.APCash = reader.IsDBNull(reader.GetOrdinal("APCash")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["APCash"]);
                    item.UPCash = reader.IsDBNull(reader.GetOrdinal("UPCash")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["UPCash"]);
                    item.PayoutCash = reader.IsDBNull(reader.GetOrdinal("PayoutCash")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["PayoutCash"]);
                    item.AHCash0 = reader.IsDBNull(reader.GetOrdinal("AHCash0")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash0"]);
                    item.AHCash1 = reader.IsDBNull(reader.GetOrdinal("AHCash1")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash1"]);
                    item.AHCash2 = reader.IsDBNull(reader.GetOrdinal("AHCash2")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash2"]);
                    item.AHCash3 = reader.IsDBNull(reader.GetOrdinal("AHCash3")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash3"]);
                    item.AHCash4 = reader.IsDBNull(reader.GetOrdinal("AHCash4")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash4"]);
                    item.AHCash5 = reader.IsDBNull(reader.GetOrdinal("AHCash5")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash5"]);
                    item.AHCash6 = reader.IsDBNull(reader.GetOrdinal("AHCash6")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash6"]);
                    item.AHCash7 = reader.IsDBNull(reader.GetOrdinal("AHCash7")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash7"]);
                    item.AHCash8 = reader.IsDBNull(reader.GetOrdinal("AHCash8")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash8"]);
                    item.AHCash9 = reader.IsDBNull(reader.GetOrdinal("AHCash9")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash9"]);
                    item.PaymentName = reader.IsDBNull(reader.GetOrdinal("PaymentName")) ? null : ((string) reader["PaymentName"]);
                    item.TotalViseChangeMoney = reader.IsDBNull(reader.GetOrdinal("TotalViseChangeMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["TotalViseChangeMoney"]);
                    item.SumCode = reader.IsDBNull(reader.GetOrdinal("SumCode")) ? null : ((string) reader["SumCode"]);
                    item.PaymentCodition = reader.IsDBNull(reader.GetOrdinal("PaymentCodition")) ? null : ((string) reader["PaymentCodition"]);
                    item.CheckRemark = reader.IsDBNull(reader.GetOrdinal("CheckRemark")) ? null : ((string) reader["CheckRemark"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override Payment Get(TransactionManager transactionManager, PaymentKey key, int start, int pageLength)
        {
            return this.GetByPaymentCode(transactionManager, key.PaymentCode, start, pageLength);
        }

        public TList<Payment> GetByContractCode(string contractCode)
        {
            int count = -1;
            return this.GetByContractCode(contractCode, 0, 0x7fffffff, out count);
        }

        public TList<Payment> GetByContractCode(TransactionManager transactionManager, string contractCode)
        {
            int count = -1;
            return this.GetByContractCode(transactionManager, contractCode, 0, 0x7fffffff, out count);
        }

        public TList<Payment> GetByContractCode(string contractCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCode(null, contractCode, start, pageLength, out count);
        }

        public TList<Payment> GetByContractCode(string contractCode, int start, int pageLength, out int count)
        {
            return this.GetByContractCode(null, contractCode, start, pageLength, out count);
        }

        public TList<Payment> GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCode(transactionManager, contractCode, start, pageLength, out count);
        }

        public abstract TList<Payment> GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength, out int count);
        public Payment GetByPaymentCode(string paymentCode)
        {
            int count = -1;
            return this.GetByPaymentCode(null, paymentCode, 0, 0x7fffffff, out count);
        }

        public Payment GetByPaymentCode(TransactionManager transactionManager, string paymentCode)
        {
            int count = -1;
            return this.GetByPaymentCode(transactionManager, paymentCode, 0, 0x7fffffff, out count);
        }

        public Payment GetByPaymentCode(string paymentCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByPaymentCode(null, paymentCode, start, pageLength, out count);
        }

        public Payment GetByPaymentCode(string paymentCode, int start, int pageLength, out int count)
        {
            return this.GetByPaymentCode(null, paymentCode, start, pageLength, out count);
        }

        public Payment GetByPaymentCode(TransactionManager transactionManager, string paymentCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByPaymentCode(transactionManager, paymentCode, start, pageLength, out count);
        }

        public abstract Payment GetByPaymentCode(TransactionManager transactionManager, string paymentCode, int start, int pageLength, out int count);
        public TList<Payment> GetByProjectCode(string projectCode)
        {
            int count = -1;
            return this.GetByProjectCode(projectCode, 0, 0x7fffffff, out count);
        }

        public TList<Payment> GetByProjectCode(TransactionManager transactionManager, string projectCode)
        {
            int count = -1;
            return this.GetByProjectCode(transactionManager, projectCode, 0, 0x7fffffff, out count);
        }

        public TList<Payment> GetByProjectCode(string projectCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByProjectCode(null, projectCode, start, pageLength, out count);
        }

        public TList<Payment> GetByProjectCode(string projectCode, int start, int pageLength, out int count)
        {
            return this.GetByProjectCode(null, projectCode, start, pageLength, out count);
        }

        public TList<Payment> GetByProjectCode(TransactionManager transactionManager, string projectCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByProjectCode(transactionManager, projectCode, start, pageLength, out count);
        }

        public abstract TList<Payment> GetByProjectCode(TransactionManager transactionManager, string projectCode, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, Payment entity)
        {
            int? nullable;
            DateTime? nullable2;
            decimal? nullable3;
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.PaymentCode = (string) row["PaymentCode"];
            entity.OriginalPaymentCode = (string) row["PaymentCode"];
            entity.PaymentTitle = Convert.IsDBNull(row["PaymentTitle"]) ? null : ((string) row["PaymentTitle"]);
            entity.PaymentID = Convert.IsDBNull(row["PaymentID"]) ? null : ((string) row["PaymentID"]);
            entity.VoucherID = Convert.IsDBNull(row["VoucherID"]) ? null : ((string) row["VoucherID"]);
            entity.RecieptCount = Convert.IsDBNull(row["RecieptCount"]) ? ((int?) (nullable = null)) : ((int?) row["RecieptCount"]);
            entity.ProjectCode = Convert.IsDBNull(row["ProjectCode"]) ? null : ((string) row["ProjectCode"]);
            entity.ApplyPerson = Convert.IsDBNull(row["ApplyPerson"]) ? null : ((string) row["ApplyPerson"]);
            entity.ApplyDate = Convert.IsDBNull(row["ApplyDate"]) ? ((DateTime?) (nullable2 = null)) : ((DateTime?) row["ApplyDate"]);
            entity.Accountant = Convert.IsDBNull(row["Accountant"]) ? null : ((string) row["Accountant"]);
            entity.AccountDate = Convert.IsDBNull(row["AccountDate"]) ? ((DateTime?) (nullable2 = null)) : ((DateTime?) row["AccountDate"]);
            entity.Payer = Convert.IsDBNull(row["Payer"]) ? null : ((string) row["Payer"]);
            entity.PayDate = Convert.IsDBNull(row["PayDate"]) ? ((DateTime?) (nullable2 = null)) : ((DateTime?) row["PayDate"]);
            entity.Purpose = Convert.IsDBNull(row["Purpose"]) ? null : ((string) row["Purpose"]);
            entity.Money = Convert.IsDBNull(row["Money"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["Money"]);
            entity.CheckPerson = Convert.IsDBNull(row["CheckPerson"]) ? null : ((string) row["CheckPerson"]);
            entity.CheckDate = Convert.IsDBNull(row["CheckDate"]) ? ((DateTime?) (nullable2 = null)) : ((DateTime?) row["CheckDate"]);
            entity.CheckOpinion = Convert.IsDBNull(row["CheckOpinion"]) ? null : ((string) row["CheckOpinion"]);
            entity.IsContract = Convert.IsDBNull(row["IsContract"]) ? ((int?) (nullable = null)) : ((int?) row["IsContract"]);
            entity.ContractCode = Convert.IsDBNull(row["ContractCode"]) ? null : ((string) row["ContractCode"]);
            entity.Status = Convert.IsDBNull(row["Status"]) ? ((int?) (nullable = null)) : ((int?) row["Status"]);
            entity.WBSCode = Convert.IsDBNull(row["WBSCode"]) ? null : ((string) row["WBSCode"]);
            entity.IsApportion = Convert.IsDBNull(row["IsApportion"]) ? ((int?) (nullable = null)) : ((int?) row["IsApportion"]);
            entity.SupplyCode = Convert.IsDBNull(row["SupplyCode"]) ? null : ((string) row["SupplyCode"]);
            entity.UnitCode = Convert.IsDBNull(row["UnitCode"]) ? null : ((string) row["UnitCode"]);
            entity.SupplyName = Convert.IsDBNull(row["SupplyName"]) ? null : ((string) row["SupplyName"]);
            entity.Remark = Convert.IsDBNull(row["Remark"]) ? null : ((string) row["Remark"]);
            entity.OldMoney = Convert.IsDBNull(row["OldMoney"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["OldMoney"]);
            entity.GroupCode = Convert.IsDBNull(row["GroupCode"]) ? null : ((string) row["GroupCode"]);
            entity.BankName = Convert.IsDBNull(row["BankName"]) ? null : ((string) row["BankName"]);
            entity.BankAccount = Convert.IsDBNull(row["BankAccount"]) ? null : ((string) row["BankAccount"]);
            entity.OtherAttachMent = Convert.IsDBNull(row["OtherAttachMent"]) ? null : ((string) row["OtherAttachMent"]);
            entity.PayType = Convert.IsDBNull(row["PayType"]) ? ((int?) (nullable = null)) : ((int?) row["PayType"]);
            entity.Issue = Convert.IsDBNull(row["Issue"]) ? null : ((int?) row["Issue"]);
            entity.IssueMode = Convert.IsDBNull(row["IssueMode"]) ? null : ((string) row["IssueMode"]);
            entity.FactPayDate = Convert.IsDBNull(row["FactPayDate"]) ? null : ((DateTime?) row["FactPayDate"]);
            entity.TotalPayMoney = Convert.IsDBNull(row["TotalPayMoney"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["TotalPayMoney"]);
            entity.AHMoney = Convert.IsDBNull(row["AHMoney"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["AHMoney"]);
            entity.APMoney = Convert.IsDBNull(row["APMoney"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["APMoney"]);
            entity.UPMoney = Convert.IsDBNull(row["UPMoney"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["UPMoney"]);
            entity.SupplierApplyMoney = Convert.IsDBNull(row["SupplierApplyMoney"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["SupplierApplyMoney"]);
            entity.MaxPayMoney = Convert.IsDBNull(row["MaxPayMoney"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["MaxPayMoney"]);
            entity.PlanPayMoney = Convert.IsDBNull(row["PlanPayMoney"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["PlanPayMoney"]);
            entity.ContractMoney = Convert.IsDBNull(row["ContractMoney"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["ContractMoney"]);
            entity.AdjustedContractMoney = Convert.IsDBNull(row["AdjustedContractMoney"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["AdjustedContractMoney"]);
            entity.PayoutMoney = Convert.IsDBNull(row["PayoutMoney"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["PayoutMoney"]);
            entity.AHCash = Convert.IsDBNull(row["AHCash"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["AHCash"]);
            entity.APCash = Convert.IsDBNull(row["APCash"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["APCash"]);
            entity.UPCash = Convert.IsDBNull(row["UPCash"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["UPCash"]);
            entity.PayoutCash = Convert.IsDBNull(row["PayoutCash"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["PayoutCash"]);
            entity.AHCash0 = Convert.IsDBNull(row["AHCash0"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["AHCash0"]);
            entity.AHCash1 = Convert.IsDBNull(row["AHCash1"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["AHCash1"]);
            entity.AHCash2 = Convert.IsDBNull(row["AHCash2"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["AHCash2"]);
            entity.AHCash3 = Convert.IsDBNull(row["AHCash3"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["AHCash3"]);
            entity.AHCash4 = Convert.IsDBNull(row["AHCash4"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["AHCash4"]);
            entity.AHCash5 = Convert.IsDBNull(row["AHCash5"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["AHCash5"]);
            entity.AHCash6 = Convert.IsDBNull(row["AHCash6"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["AHCash6"]);
            entity.AHCash7 = Convert.IsDBNull(row["AHCash7"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["AHCash7"]);
            entity.AHCash8 = Convert.IsDBNull(row["AHCash8"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["AHCash8"]);
            entity.AHCash9 = Convert.IsDBNull(row["AHCash9"]) ? ((decimal?) (nullable3 = null)) : ((decimal?) row["AHCash9"]);
            entity.PaymentName = Convert.IsDBNull(row["PaymentName"]) ? null : ((string) row["PaymentName"]);
            entity.TotalViseChangeMoney = Convert.IsDBNull(row["TotalViseChangeMoney"]) ? null : ((decimal?) row["TotalViseChangeMoney"]);
            entity.SumCode = Convert.IsDBNull(row["SumCode"]) ? null : ((string) row["SumCode"]);
            entity.PaymentCodition = Convert.IsDBNull(row["PaymentCodition"]) ? null : ((string) row["PaymentCodition"]);
            entity.CheckRemark = Convert.IsDBNull(row["CheckRemark"]) ? null : ((string) row["CheckRemark"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, Payment entity)
        {
            if (reader.Read())
            {
                int? nullable;
                DateTime? nullable2;
                decimal? nullable3;
                entity.PaymentCode = (string) reader["PaymentCode"];
                entity.OriginalPaymentCode = (string) reader["PaymentCode"];
                entity.PaymentTitle = reader.IsDBNull(reader.GetOrdinal("PaymentTitle")) ? null : ((string) reader["PaymentTitle"]);
                entity.PaymentID = reader.IsDBNull(reader.GetOrdinal("PaymentID")) ? null : ((string) reader["PaymentID"]);
                entity.VoucherID = reader.IsDBNull(reader.GetOrdinal("VoucherID")) ? null : ((string) reader["VoucherID"]);
                entity.RecieptCount = reader.IsDBNull(reader.GetOrdinal("RecieptCount")) ? ((int?) (nullable = null)) : ((int?) reader["RecieptCount"]);
                entity.ProjectCode = reader.IsDBNull(reader.GetOrdinal("ProjectCode")) ? null : ((string) reader["ProjectCode"]);
                entity.ApplyPerson = reader.IsDBNull(reader.GetOrdinal("ApplyPerson")) ? null : ((string) reader["ApplyPerson"]);
                entity.ApplyDate = reader.IsDBNull(reader.GetOrdinal("ApplyDate")) ? ((DateTime?) (nullable2 = null)) : ((DateTime?) reader["ApplyDate"]);
                entity.Accountant = reader.IsDBNull(reader.GetOrdinal("Accountant")) ? null : ((string) reader["Accountant"]);
                entity.AccountDate = reader.IsDBNull(reader.GetOrdinal("AccountDate")) ? ((DateTime?) (nullable2 = null)) : ((DateTime?) reader["AccountDate"]);
                entity.Payer = reader.IsDBNull(reader.GetOrdinal("Payer")) ? null : ((string) reader["Payer"]);
                entity.PayDate = reader.IsDBNull(reader.GetOrdinal("PayDate")) ? ((DateTime?) (nullable2 = null)) : ((DateTime?) reader["PayDate"]);
                entity.Purpose = reader.IsDBNull(reader.GetOrdinal("Purpose")) ? null : ((string) reader["Purpose"]);
                entity.Money = reader.IsDBNull(reader.GetOrdinal("Money")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["Money"]);
                entity.CheckPerson = reader.IsDBNull(reader.GetOrdinal("CheckPerson")) ? null : ((string) reader["CheckPerson"]);
                entity.CheckDate = reader.IsDBNull(reader.GetOrdinal("CheckDate")) ? ((DateTime?) (nullable2 = null)) : ((DateTime?) reader["CheckDate"]);
                entity.CheckOpinion = reader.IsDBNull(reader.GetOrdinal("CheckOpinion")) ? null : ((string) reader["CheckOpinion"]);
                entity.IsContract = reader.IsDBNull(reader.GetOrdinal("IsContract")) ? ((int?) (nullable = null)) : ((int?) reader["IsContract"]);
                entity.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                entity.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? ((int?) (nullable = null)) : ((int?) reader["Status"]);
                entity.WBSCode = reader.IsDBNull(reader.GetOrdinal("WBSCode")) ? null : ((string) reader["WBSCode"]);
                entity.IsApportion = reader.IsDBNull(reader.GetOrdinal("IsApportion")) ? ((int?) (nullable = null)) : ((int?) reader["IsApportion"]);
                entity.SupplyCode = reader.IsDBNull(reader.GetOrdinal("SupplyCode")) ? null : ((string) reader["SupplyCode"]);
                entity.UnitCode = reader.IsDBNull(reader.GetOrdinal("UnitCode")) ? null : ((string) reader["UnitCode"]);
                entity.SupplyName = reader.IsDBNull(reader.GetOrdinal("SupplyName")) ? null : ((string) reader["SupplyName"]);
                entity.Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? null : ((string) reader["Remark"]);
                entity.OldMoney = reader.IsDBNull(reader.GetOrdinal("OldMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["OldMoney"]);
                entity.GroupCode = reader.IsDBNull(reader.GetOrdinal("GroupCode")) ? null : ((string) reader["GroupCode"]);
                entity.BankName = reader.IsDBNull(reader.GetOrdinal("BankName")) ? null : ((string) reader["BankName"]);
                entity.BankAccount = reader.IsDBNull(reader.GetOrdinal("BankAccount")) ? null : ((string) reader["BankAccount"]);
                entity.OtherAttachMent = reader.IsDBNull(reader.GetOrdinal("OtherAttachMent")) ? null : ((string) reader["OtherAttachMent"]);
                entity.PayType = reader.IsDBNull(reader.GetOrdinal("PayType")) ? ((int?) (nullable = null)) : ((int?) reader["PayType"]);
                entity.Issue = reader.IsDBNull(reader.GetOrdinal("Issue")) ? null : ((int?) reader["Issue"]);
                entity.IssueMode = reader.IsDBNull(reader.GetOrdinal("IssueMode")) ? null : ((string) reader["IssueMode"]);
                entity.FactPayDate = reader.IsDBNull(reader.GetOrdinal("FactPayDate")) ? null : ((DateTime?) reader["FactPayDate"]);
                entity.TotalPayMoney = reader.IsDBNull(reader.GetOrdinal("TotalPayMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["TotalPayMoney"]);
                entity.AHMoney = reader.IsDBNull(reader.GetOrdinal("AHMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHMoney"]);
                entity.APMoney = reader.IsDBNull(reader.GetOrdinal("APMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["APMoney"]);
                entity.UPMoney = reader.IsDBNull(reader.GetOrdinal("UPMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["UPMoney"]);
                entity.SupplierApplyMoney = reader.IsDBNull(reader.GetOrdinal("SupplierApplyMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["SupplierApplyMoney"]);
                entity.MaxPayMoney = reader.IsDBNull(reader.GetOrdinal("MaxPayMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["MaxPayMoney"]);
                entity.PlanPayMoney = reader.IsDBNull(reader.GetOrdinal("PlanPayMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["PlanPayMoney"]);
                entity.ContractMoney = reader.IsDBNull(reader.GetOrdinal("ContractMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["ContractMoney"]);
                entity.AdjustedContractMoney = reader.IsDBNull(reader.GetOrdinal("AdjustedContractMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AdjustedContractMoney"]);
                entity.PayoutMoney = reader.IsDBNull(reader.GetOrdinal("PayoutMoney")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["PayoutMoney"]);
                entity.AHCash = reader.IsDBNull(reader.GetOrdinal("AHCash")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash"]);
                entity.APCash = reader.IsDBNull(reader.GetOrdinal("APCash")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["APCash"]);
                entity.UPCash = reader.IsDBNull(reader.GetOrdinal("UPCash")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["UPCash"]);
                entity.PayoutCash = reader.IsDBNull(reader.GetOrdinal("PayoutCash")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["PayoutCash"]);
                entity.AHCash0 = reader.IsDBNull(reader.GetOrdinal("AHCash0")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash0"]);
                entity.AHCash1 = reader.IsDBNull(reader.GetOrdinal("AHCash1")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash1"]);
                entity.AHCash2 = reader.IsDBNull(reader.GetOrdinal("AHCash2")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash2"]);
                entity.AHCash3 = reader.IsDBNull(reader.GetOrdinal("AHCash3")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash3"]);
                entity.AHCash4 = reader.IsDBNull(reader.GetOrdinal("AHCash4")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash4"]);
                entity.AHCash5 = reader.IsDBNull(reader.GetOrdinal("AHCash5")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash5"]);
                entity.AHCash6 = reader.IsDBNull(reader.GetOrdinal("AHCash6")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash6"]);
                entity.AHCash7 = reader.IsDBNull(reader.GetOrdinal("AHCash7")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash7"]);
                entity.AHCash8 = reader.IsDBNull(reader.GetOrdinal("AHCash8")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash8"]);
                entity.AHCash9 = reader.IsDBNull(reader.GetOrdinal("AHCash9")) ? ((decimal?) (nullable3 = null)) : ((decimal?) reader["AHCash9"]);
                entity.PaymentName = reader.IsDBNull(reader.GetOrdinal("PaymentName")) ? null : ((string) reader["PaymentName"]);
                entity.TotalViseChangeMoney = reader.IsDBNull(reader.GetOrdinal("TotalViseChangeMoney")) ? null : ((decimal?) reader["TotalViseChangeMoney"]);
                entity.SumCode = reader.IsDBNull(reader.GetOrdinal("SumCode")) ? null : ((string) reader["SumCode"]);
                entity.PaymentCodition = reader.IsDBNull(reader.GetOrdinal("PaymentCodition")) ? null : ((string) reader["PaymentCodition"]);
                entity.CheckRemark = reader.IsDBNull(reader.GetOrdinal("CheckRemark")) ? null : ((string) reader["CheckRemark"]);
                entity.AcceptChanges();
            }
        }
    }
}

