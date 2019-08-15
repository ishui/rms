namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class ContractProviderBaseCore : EntityProviderBase<Contract, ContractKey>
    {
        protected ContractProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, Contract entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
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
                if (base.CanDeepLoad(entity, "List<ContractCost>", "ContractCostCollection", deepLoadType, innerList))
                {
                    entity.ContractCostCollection = DataRepository.ContractCostProvider.GetByContractCode(transactionManager, entity.ContractCode);
                    if (deep && (entity.ContractCostCollection.Count > 0))
                    {
                        DataRepository.ContractCostProvider.DeepLoad(transactionManager, entity.ContractCostCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
                if (base.CanDeepLoad(entity, "List<Payment>", "PaymentCollection", deepLoadType, innerList))
                {
                    entity.PaymentCollection = DataRepository.PaymentProvider.GetByContractCode(transactionManager, entity.ContractCode);
                    if (deep && (entity.PaymentCollection.Count > 0))
                    {
                        DataRepository.PaymentProvider.DeepLoad(transactionManager, entity.PaymentCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
                if (base.CanDeepLoad(entity, "List<ContractChange>", "ContractChangeCollection", deepLoadType, innerList))
                {
                    entity.ContractChangeCollection = DataRepository.ContractChangeProvider.GetByContractCode(transactionManager, entity.ContractCode);
                    if (deep && (entity.ContractChangeCollection.Count > 0))
                    {
                        DataRepository.ContractChangeProvider.DeepLoad(transactionManager, entity.ContractChangeCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
                if (base.CanDeepLoad(entity, "List<ContractCostPlan>", "ContractCostPlanCollection", deepLoadType, innerList))
                {
                    entity.ContractCostPlanCollection = DataRepository.ContractCostPlanProvider.GetByContractCode(transactionManager, entity.ContractCode);
                    if (deep && (entity.ContractCostPlanCollection.Count > 0))
                    {
                        DataRepository.ContractCostPlanProvider.DeepLoad(transactionManager, entity.ContractCostPlanCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
                if (base.CanDeepLoad(entity, "List<ContractBill>", "ContractBillCollection", deepLoadType, innerList))
                {
                    entity.ContractBillCollection = DataRepository.ContractBillProvider.GetByContractCode(transactionManager, entity.ContractCode);
                    if (deep && (entity.ContractBillCollection.Count > 0))
                    {
                        DataRepository.ContractBillProvider.DeepLoad(transactionManager, entity.ContractBillCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
                if (base.CanDeepLoad(entity, "List<ContractAccount>", "ContractAccountCollection", deepLoadType, innerList))
                {
                    entity.ContractAccountCollection = DataRepository.ContractAccountProvider.GetByContractCode(transactionManager, entity.ContractCode);
                    if (deep && (entity.ContractAccountCollection.Count > 0))
                    {
                        DataRepository.ContractAccountProvider.DeepLoad(transactionManager, entity.ContractAccountCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
                if (base.CanDeepLoad(entity, "List<ContractMaterial>", "ContractMaterialCollection", deepLoadType, innerList))
                {
                    entity.ContractMaterialCollection = DataRepository.ContractMaterialProvider.GetByContractCode(transactionManager, entity.ContractCode);
                    if (deep && (entity.ContractMaterialCollection.Count > 0))
                    {
                        DataRepository.ContractMaterialProvider.DeepLoad(transactionManager, entity.ContractMaterialCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, Contract entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
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
            if (base.CanDeepSave(entity, "List<ContractCost>", "ContractCostCollection", deepSaveType, innerList))
            {
                foreach (ContractCost cost in entity.ContractCostCollection)
                {
                    cost.ContractCode = entity.ContractCode;
                }
                if ((entity.ContractCostCollection.Count > 0) || (entity.ContractCostCollection.DeletedItems.Count > 0))
                {
                    DataRepository.ContractCostProvider.DeepSave(transactionManager, entity.ContractCostCollection, deepSaveType, childTypes, innerList);
                }
            }
            if (base.CanDeepSave(entity, "List<Payment>", "PaymentCollection", deepSaveType, innerList))
            {
                foreach (Payment payment in entity.PaymentCollection)
                {
                    payment.ContractCode = entity.ContractCode;
                }
                if ((entity.PaymentCollection.Count > 0) || (entity.PaymentCollection.DeletedItems.Count > 0))
                {
                    DataRepository.PaymentProvider.DeepSave(transactionManager, entity.PaymentCollection, deepSaveType, childTypes, innerList);
                }
            }
            if (base.CanDeepSave(entity, "List<ContractChange>", "ContractChangeCollection", deepSaveType, innerList))
            {
                foreach (ContractChange change in entity.ContractChangeCollection)
                {
                    change.ContractCode = entity.ContractCode;
                }
                if ((entity.ContractChangeCollection.Count > 0) || (entity.ContractChangeCollection.DeletedItems.Count > 0))
                {
                    DataRepository.ContractChangeProvider.DeepSave(transactionManager, entity.ContractChangeCollection, deepSaveType, childTypes, innerList);
                }
            }
            if (base.CanDeepSave(entity, "List<ContractCostPlan>", "ContractCostPlanCollection", deepSaveType, innerList))
            {
                foreach (ContractCostPlan plan in entity.ContractCostPlanCollection)
                {
                    plan.ContractCode = entity.ContractCode;
                }
                if ((entity.ContractCostPlanCollection.Count > 0) || (entity.ContractCostPlanCollection.DeletedItems.Count > 0))
                {
                    DataRepository.ContractCostPlanProvider.DeepSave(transactionManager, entity.ContractCostPlanCollection, deepSaveType, childTypes, innerList);
                }
            }
            if (base.CanDeepSave(entity, "List<ContractBill>", "ContractBillCollection", deepSaveType, innerList))
            {
                foreach (ContractBill bill in entity.ContractBillCollection)
                {
                    bill.ContractCode = entity.ContractCode;
                }
                if ((entity.ContractBillCollection.Count > 0) || (entity.ContractBillCollection.DeletedItems.Count > 0))
                {
                    DataRepository.ContractBillProvider.DeepSave(transactionManager, entity.ContractBillCollection, deepSaveType, childTypes, innerList);
                }
            }
            if (base.CanDeepSave(entity, "List<ContractAccount>", "ContractAccountCollection", deepSaveType, innerList))
            {
                foreach (ContractAccount account in entity.ContractAccountCollection)
                {
                    account.ContractCode = entity.ContractCode;
                }
                if ((entity.ContractAccountCollection.Count > 0) || (entity.ContractAccountCollection.DeletedItems.Count > 0))
                {
                    DataRepository.ContractAccountProvider.DeepSave(transactionManager, entity.ContractAccountCollection, deepSaveType, childTypes, innerList);
                }
            }
            if (base.CanDeepSave(entity, "List<ContractMaterial>", "ContractMaterialCollection", deepSaveType, innerList))
            {
                foreach (ContractMaterial material in entity.ContractMaterialCollection)
                {
                    material.ContractCode = entity.ContractCode;
                }
                if ((entity.ContractMaterialCollection.Count > 0) || (entity.ContractMaterialCollection.DeletedItems.Count > 0))
                {
                    DataRepository.ContractMaterialProvider.DeepSave(transactionManager, entity.ContractMaterialCollection, deepSaveType, childTypes, innerList);
                }
            }
            return true;
        }

        public bool Delete(string contractCode)
        {
            return this.Delete(null, contractCode);
        }

        public abstract bool Delete(TransactionManager transactionManager, string contractCode);
        public override bool Delete(TransactionManager transactionManager, ContractKey key)
        {
            return this.Delete(transactionManager, key.ContractCode);
        }

        public static TList<Contract> Fill(IDataReader reader, TList<Contract> rows, int start, int pageLength)
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
                Contract item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "Contract" + (reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? string.Empty : ((string) reader["ContractCode"])).ToString();
                    item = EntityManager.LocateOrCreate<Contract>(text.ToString(), "Contract", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new Contract();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    DateTime? nullable;
                    decimal? nullable2;
                    int? nullable3;
                    item.SuppressEntityEvents = true;
                    item.ContractCode = (string) reader["ContractCode"];
                    item.OriginalContractCode = item.ContractCode;
                    item.ContractID = reader.IsDBNull(reader.GetOrdinal("ContractID")) ? null : ((string) reader["ContractID"]);
                    item.ProjectCode = reader.IsDBNull(reader.GetOrdinal("ProjectCode")) ? null : ((string) reader["ProjectCode"]);
                    item.ContractName = reader.IsDBNull(reader.GetOrdinal("ContractName")) ? null : ((string) reader["ContractName"]);
                    item.Type = reader.IsDBNull(reader.GetOrdinal("Type")) ? null : ((string) reader["Type"]);
                    item.SupplierCode = reader.IsDBNull(reader.GetOrdinal("SupplierCode")) ? null : ((string) reader["SupplierCode"]);
                    item.Supplier2Code = reader.IsDBNull(reader.GetOrdinal("Supplier2Code")) ? null : ((string) reader["Supplier2Code"]);
                    item.ContractPerson = reader.IsDBNull(reader.GetOrdinal("ContractPerson")) ? null : ((string) reader["ContractPerson"]);
                    item.ContractDate = reader.IsDBNull(reader.GetOrdinal("ContractDate")) ? ((DateTime?) (nullable = null)) : ((DateTime?) reader["ContractDate"]);
                    item.TotalMoney = reader.IsDBNull(reader.GetOrdinal("TotalMoney")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["TotalMoney"]);
                    item.CreateDate = reader.IsDBNull(reader.GetOrdinal("CreateDate")) ? ((DateTime?) (nullable = null)) : ((DateTime?) reader["CreateDate"]);
                    item.CreatePerson = reader.IsDBNull(reader.GetOrdinal("CreatePerson")) ? null : ((string) reader["CreatePerson"]);
                    item.LastModifyPerson = reader.IsDBNull(reader.GetOrdinal("LastModifyPerson")) ? null : ((string) reader["LastModifyPerson"]);
                    item.LastModifyDate = reader.IsDBNull(reader.GetOrdinal("LastModifyDate")) ? ((DateTime?) (nullable = null)) : ((DateTime?) reader["LastModifyDate"]);
                    item.Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? null : ((string) reader["Remark"]);
                    item.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? ((int?) (nullable3 = null)) : ((int?) reader["Status"]);
                    item.CheckPerson = reader.IsDBNull(reader.GetOrdinal("CheckPerson")) ? null : ((string) reader["CheckPerson"]);
                    item.CheckOpinion = reader.IsDBNull(reader.GetOrdinal("CheckOpinion")) ? null : ((string) reader["CheckOpinion"]);
                    item.CheckDate = reader.IsDBNull(reader.GetOrdinal("CheckDate")) ? ((DateTime?) (nullable = null)) : ((DateTime?) reader["CheckDate"]);
                    item.ContractObject = reader.IsDBNull(reader.GetOrdinal("ContractObject")) ? null : ((string) reader["ContractObject"]);
                    item.UnitCode = reader.IsDBNull(reader.GetOrdinal("UnitCode")) ? null : ((string) reader["UnitCode"]);
                    item.ThirdParty = reader.IsDBNull(reader.GetOrdinal("ThirdParty")) ? null : ((string) reader["ThirdParty"]);
                    item.BeforeAccountTotalMoney = reader.IsDBNull(reader.GetOrdinal("BeforeAccountTotalMoney")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["BeforeAccountTotalMoney"]);
                    item.OldSumMoney = reader.IsDBNull(reader.GetOrdinal("oldSumMoney")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["oldSumMoney"]);
                    item.OriginalMoney = reader.IsDBNull(reader.GetOrdinal("OriginalMoney")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["OriginalMoney"]);
                    item.Mostly = reader.IsDBNull(reader.GetOrdinal("Mostly")) ? ((int?) (nullable3 = null)) : ((int?) reader["Mostly"]);
                    item.BiddingCode = reader.IsDBNull(reader.GetOrdinal("BiddingCode")) ? null : ((string) reader["BiddingCode"]);
                    item.BudgetMoney = reader.IsDBNull(reader.GetOrdinal("BudgetMoney")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["BudgetMoney"]);
                    item.AdjustMoney = reader.IsDBNull(reader.GetOrdinal("AdjustMoney")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["AdjustMoney"]);
                    item.DevelopUnit = reader.IsDBNull(reader.GetOrdinal("DevelopUnit")) ? null : ((string) reader["DevelopUnit"]);
                    item.CreateMode = reader.IsDBNull(reader.GetOrdinal("CreateMode")) ? null : ((string) reader["CreateMode"]);
                    item.WorkTime = reader.IsDBNull(reader.GetOrdinal("WorkTime")) ? null : ((string) reader["WorkTime"]);
                    item.MarkSegment = reader.IsDBNull(reader.GetOrdinal("MarkSegment")) ? null : ((string) reader["MarkSegment"]);
                    item.GroupName = reader.IsDBNull(reader.GetOrdinal("GroupName")) ? null : ((string) reader["GroupName"]);
                    item.Building = reader.IsDBNull(reader.GetOrdinal("Building")) ? null : ((string) reader["Building"]);
                    item.PayMode = reader.IsDBNull(reader.GetOrdinal("PayMode")) ? null : ((string) reader["PayMode"]);
                    item.QualityRequire = reader.IsDBNull(reader.GetOrdinal("QualityRequire")) ? null : ((string) reader["QualityRequire"]);
                    item.ContractArea = reader.IsDBNull(reader.GetOrdinal("ContractArea")) ? null : ((string) reader["ContractArea"]);
                    item.ContractDefaultValueCode = reader.IsDBNull(reader.GetOrdinal("ContractDefaultValueCode")) ? null : ((string) reader["ContractDefaultValueCode"]);
                    item.BaoHan = reader.IsDBNull(reader.GetOrdinal("BaoHan")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["BaoHan"]);
                    item.PerformingCircs = reader.IsDBNull(reader.GetOrdinal("PerformingCircs")) ? null : ((string) reader["PerformingCircs"]);
                    item.AccountStatus = reader.IsDBNull(reader.GetOrdinal("AccountStatus")) ? ((int?) (nullable3 = null)) : ((int?) reader["AccountStatus"]);
                    item.AuditingStatus = reader.IsDBNull(reader.GetOrdinal("AuditingStatus")) ? ((int?) (nullable3 = null)) : ((int?) reader["AuditingStatus"]);
                    item.ChangeStatus = reader.IsDBNull(reader.GetOrdinal("ChangeStatus")) ? ((int?) (nullable3 = null)) : ((int?) reader["ChangeStatus"]);
                    item.ChangeCount = reader.IsDBNull(reader.GetOrdinal("ChangeCount")) ? ((int?) (nullable3 = null)) : ((int?) reader["ChangeCount"]);
                    item.WorkStartDate = reader.IsDBNull(reader.GetOrdinal("WorkStartDate")) ? ((DateTime?) (nullable = null)) : ((DateTime?) reader["WorkStartDate"]);
                    item.WorkEndDate = reader.IsDBNull(reader.GetOrdinal("WorkEndDate")) ? ((DateTime?) (nullable = null)) : ((DateTime?) reader["WorkEndDate"]);
                    item.PerCash0 = reader.IsDBNull(reader.GetOrdinal("PerCash0")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash0"]);
                    item.PerCash1 = reader.IsDBNull(reader.GetOrdinal("PerCash1")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash1"]);
                    item.PerCash2 = reader.IsDBNull(reader.GetOrdinal("PerCash2")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash2"]);
                    item.PerCash3 = reader.IsDBNull(reader.GetOrdinal("PerCash3")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash3"]);
                    item.PerCash4 = reader.IsDBNull(reader.GetOrdinal("PerCash4")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash4"]);
                    item.PerCash5 = reader.IsDBNull(reader.GetOrdinal("PerCash5")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash5"]);
                    item.PerCash6 = reader.IsDBNull(reader.GetOrdinal("PerCash6")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash6"]);
                    item.PerCash7 = reader.IsDBNull(reader.GetOrdinal("PerCash7")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash7"]);
                    item.PerCash8 = reader.IsDBNull(reader.GetOrdinal("PerCash8")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash8"]);
                    item.PerCash9 = reader.IsDBNull(reader.GetOrdinal("PerCash9")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash9"]);
                    item.StampDutyID = reader.IsDBNull(reader.GetOrdinal("StampDutyID")) ? ((int?) (nullable3 = null)) : ((int?) reader["StampDutyID"]);
                    item.StampDuty = reader.IsDBNull(reader.GetOrdinal("StampDuty")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["StampDuty"]);
                    item.AdIssueDate = reader.IsDBNull(reader.GetOrdinal("AdIssueDate")) ? ((DateTime?) (nullable = null)) : ((DateTime?) reader["AdIssueDate"]);
                    item.MoneyType = reader.IsDBNull(reader.GetOrdinal("MoneyType")) ? null : ((string) reader["MoneyType"]);
                    item.ExchangeRate = reader.IsDBNull(reader.GetOrdinal("ExchangeRate")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["ExchangeRate"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override Contract Get(TransactionManager transactionManager, ContractKey key, int start, int pageLength)
        {
            return this.GetByContractCode(transactionManager, key.ContractCode, start, pageLength);
        }

        public Contract GetByContractCode(string contractCode)
        {
            int count = -1;
            return this.GetByContractCode(null, contractCode, 0, 0x7fffffff, out count);
        }

        public Contract GetByContractCode(TransactionManager transactionManager, string contractCode)
        {
            int count = -1;
            return this.GetByContractCode(transactionManager, contractCode, 0, 0x7fffffff, out count);
        }

        public Contract GetByContractCode(string contractCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCode(null, contractCode, start, pageLength, out count);
        }

        public Contract GetByContractCode(string contractCode, int start, int pageLength, out int count)
        {
            return this.GetByContractCode(null, contractCode, start, pageLength, out count);
        }

        public Contract GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCode(transactionManager, contractCode, start, pageLength, out count);
        }

        public abstract Contract GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength, out int count);
        public TList<Contract> GetByProjectCode(string projectCode)
        {
            int count = -1;
            return this.GetByProjectCode(projectCode, 0, 0x7fffffff, out count);
        }

        public TList<Contract> GetByProjectCode(TransactionManager transactionManager, string projectCode)
        {
            int count = -1;
            return this.GetByProjectCode(transactionManager, projectCode, 0, 0x7fffffff, out count);
        }

        public TList<Contract> GetByProjectCode(string projectCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByProjectCode(null, projectCode, start, pageLength, out count);
        }

        public TList<Contract> GetByProjectCode(string projectCode, int start, int pageLength, out int count)
        {
            return this.GetByProjectCode(null, projectCode, start, pageLength, out count);
        }

        public TList<Contract> GetByProjectCode(TransactionManager transactionManager, string projectCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByProjectCode(transactionManager, projectCode, start, pageLength, out count);
        }

        public abstract TList<Contract> GetByProjectCode(TransactionManager transactionManager, string projectCode, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, Contract entity)
        {
            DateTime? nullable;
            decimal? nullable2;
            int? nullable3;
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.ContractCode = (string) row["ContractCode"];
            entity.OriginalContractCode = (string) row["ContractCode"];
            entity.ContractID = Convert.IsDBNull(row["ContractID"]) ? null : ((string) row["ContractID"]);
            entity.ProjectCode = Convert.IsDBNull(row["ProjectCode"]) ? null : ((string) row["ProjectCode"]);
            entity.ContractName = Convert.IsDBNull(row["ContractName"]) ? null : ((string) row["ContractName"]);
            entity.Type = Convert.IsDBNull(row["Type"]) ? null : ((string) row["Type"]);
            entity.SupplierCode = Convert.IsDBNull(row["SupplierCode"]) ? null : ((string) row["SupplierCode"]);
            entity.Supplier2Code = Convert.IsDBNull(row["Supplier2Code"]) ? null : ((string) row["Supplier2Code"]);
            entity.ContractPerson = Convert.IsDBNull(row["ContractPerson"]) ? null : ((string) row["ContractPerson"]);
            entity.ContractDate = Convert.IsDBNull(row["ContractDate"]) ? ((DateTime?) (nullable = null)) : ((DateTime?) row["ContractDate"]);
            entity.TotalMoney = Convert.IsDBNull(row["TotalMoney"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["TotalMoney"]);
            entity.CreateDate = Convert.IsDBNull(row["CreateDate"]) ? ((DateTime?) (nullable = null)) : ((DateTime?) row["CreateDate"]);
            entity.CreatePerson = Convert.IsDBNull(row["CreatePerson"]) ? null : ((string) row["CreatePerson"]);
            entity.LastModifyPerson = Convert.IsDBNull(row["LastModifyPerson"]) ? null : ((string) row["LastModifyPerson"]);
            entity.LastModifyDate = Convert.IsDBNull(row["LastModifyDate"]) ? ((DateTime?) (nullable = null)) : ((DateTime?) row["LastModifyDate"]);
            entity.Remark = Convert.IsDBNull(row["Remark"]) ? null : ((string) row["Remark"]);
            entity.Status = Convert.IsDBNull(row["Status"]) ? ((int?) (nullable3 = null)) : ((int?) row["Status"]);
            entity.CheckPerson = Convert.IsDBNull(row["CheckPerson"]) ? null : ((string) row["CheckPerson"]);
            entity.CheckOpinion = Convert.IsDBNull(row["CheckOpinion"]) ? null : ((string) row["CheckOpinion"]);
            entity.CheckDate = Convert.IsDBNull(row["CheckDate"]) ? ((DateTime?) (nullable = null)) : ((DateTime?) row["CheckDate"]);
            entity.ContractObject = Convert.IsDBNull(row["ContractObject"]) ? null : ((string) row["ContractObject"]);
            entity.UnitCode = Convert.IsDBNull(row["UnitCode"]) ? null : ((string) row["UnitCode"]);
            entity.ThirdParty = Convert.IsDBNull(row["ThirdParty"]) ? null : ((string) row["ThirdParty"]);
            entity.BeforeAccountTotalMoney = Convert.IsDBNull(row["BeforeAccountTotalMoney"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["BeforeAccountTotalMoney"]);
            entity.OldSumMoney = Convert.IsDBNull(row["oldSumMoney"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["oldSumMoney"]);
            entity.OriginalMoney = Convert.IsDBNull(row["OriginalMoney"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["OriginalMoney"]);
            entity.Mostly = Convert.IsDBNull(row["Mostly"]) ? ((int?) (nullable3 = null)) : ((int?) row["Mostly"]);
            entity.BiddingCode = Convert.IsDBNull(row["BiddingCode"]) ? null : ((string) row["BiddingCode"]);
            entity.BudgetMoney = Convert.IsDBNull(row["BudgetMoney"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["BudgetMoney"]);
            entity.AdjustMoney = Convert.IsDBNull(row["AdjustMoney"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["AdjustMoney"]);
            entity.DevelopUnit = Convert.IsDBNull(row["DevelopUnit"]) ? null : ((string) row["DevelopUnit"]);
            entity.CreateMode = Convert.IsDBNull(row["CreateMode"]) ? null : ((string) row["CreateMode"]);
            entity.WorkTime = Convert.IsDBNull(row["WorkTime"]) ? null : ((string) row["WorkTime"]);
            entity.MarkSegment = Convert.IsDBNull(row["MarkSegment"]) ? null : ((string) row["MarkSegment"]);
            entity.GroupName = Convert.IsDBNull(row["GroupName"]) ? null : ((string) row["GroupName"]);
            entity.Building = Convert.IsDBNull(row["Building"]) ? null : ((string) row["Building"]);
            entity.PayMode = Convert.IsDBNull(row["PayMode"]) ? null : ((string) row["PayMode"]);
            entity.QualityRequire = Convert.IsDBNull(row["QualityRequire"]) ? null : ((string) row["QualityRequire"]);
            entity.ContractArea = Convert.IsDBNull(row["ContractArea"]) ? null : ((string) row["ContractArea"]);
            entity.ContractDefaultValueCode = Convert.IsDBNull(row["ContractDefaultValueCode"]) ? null : ((string) row["ContractDefaultValueCode"]);
            entity.BaoHan = Convert.IsDBNull(row["BaoHan"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["BaoHan"]);
            entity.PerformingCircs = Convert.IsDBNull(row["PerformingCircs"]) ? null : ((string) row["PerformingCircs"]);
            entity.AccountStatus = Convert.IsDBNull(row["AccountStatus"]) ? ((int?) (nullable3 = null)) : ((int?) row["AccountStatus"]);
            entity.AuditingStatus = Convert.IsDBNull(row["AuditingStatus"]) ? ((int?) (nullable3 = null)) : ((int?) row["AuditingStatus"]);
            entity.ChangeStatus = Convert.IsDBNull(row["ChangeStatus"]) ? ((int?) (nullable3 = null)) : ((int?) row["ChangeStatus"]);
            entity.ChangeCount = Convert.IsDBNull(row["ChangeCount"]) ? ((int?) (nullable3 = null)) : ((int?) row["ChangeCount"]);
            entity.WorkStartDate = Convert.IsDBNull(row["WorkStartDate"]) ? ((DateTime?) (nullable = null)) : ((DateTime?) row["WorkStartDate"]);
            entity.WorkEndDate = Convert.IsDBNull(row["WorkEndDate"]) ? ((DateTime?) (nullable = null)) : ((DateTime?) row["WorkEndDate"]);
            entity.PerCash0 = Convert.IsDBNull(row["PerCash0"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["PerCash0"]);
            entity.PerCash1 = Convert.IsDBNull(row["PerCash1"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["PerCash1"]);
            entity.PerCash2 = Convert.IsDBNull(row["PerCash2"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["PerCash2"]);
            entity.PerCash3 = Convert.IsDBNull(row["PerCash3"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["PerCash3"]);
            entity.PerCash4 = Convert.IsDBNull(row["PerCash4"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["PerCash4"]);
            entity.PerCash5 = Convert.IsDBNull(row["PerCash5"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["PerCash5"]);
            entity.PerCash6 = Convert.IsDBNull(row["PerCash6"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["PerCash6"]);
            entity.PerCash7 = Convert.IsDBNull(row["PerCash7"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["PerCash7"]);
            entity.PerCash8 = Convert.IsDBNull(row["PerCash8"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["PerCash8"]);
            entity.PerCash9 = Convert.IsDBNull(row["PerCash9"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["PerCash9"]);
            entity.StampDutyID = Convert.IsDBNull(row["StampDutyID"]) ? null : ((int?) row["StampDutyID"]);
            entity.StampDuty = Convert.IsDBNull(row["StampDuty"]) ? ((decimal?) (nullable2 = null)) : ((decimal?) row["StampDuty"]);
            entity.AdIssueDate = Convert.IsDBNull(row["AdIssueDate"]) ? null : ((DateTime?) row["AdIssueDate"]);
            entity.MoneyType = Convert.IsDBNull(row["MoneyType"]) ? null : ((string) row["MoneyType"]);
            entity.ExchangeRate = Convert.IsDBNull(row["ExchangeRate"]) ? null : ((decimal?) row["ExchangeRate"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, Contract entity)
        {
            if (reader.Read())
            {
                DateTime? nullable;
                decimal? nullable2;
                int? nullable3;
                entity.ContractCode = (string) reader["ContractCode"];
                entity.OriginalContractCode = (string) reader["ContractCode"];
                entity.ContractID = reader.IsDBNull(reader.GetOrdinal("ContractID")) ? null : ((string) reader["ContractID"]);
                entity.ProjectCode = reader.IsDBNull(reader.GetOrdinal("ProjectCode")) ? null : ((string) reader["ProjectCode"]);
                entity.ContractName = reader.IsDBNull(reader.GetOrdinal("ContractName")) ? null : ((string) reader["ContractName"]);
                entity.Type = reader.IsDBNull(reader.GetOrdinal("Type")) ? null : ((string) reader["Type"]);
                entity.SupplierCode = reader.IsDBNull(reader.GetOrdinal("SupplierCode")) ? null : ((string) reader["SupplierCode"]);
                entity.Supplier2Code = reader.IsDBNull(reader.GetOrdinal("Supplier2Code")) ? null : ((string) reader["Supplier2Code"]);
                entity.ContractPerson = reader.IsDBNull(reader.GetOrdinal("ContractPerson")) ? null : ((string) reader["ContractPerson"]);
                entity.ContractDate = reader.IsDBNull(reader.GetOrdinal("ContractDate")) ? ((DateTime?) (nullable = null)) : ((DateTime?) reader["ContractDate"]);
                entity.TotalMoney = reader.IsDBNull(reader.GetOrdinal("TotalMoney")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["TotalMoney"]);
                entity.CreateDate = reader.IsDBNull(reader.GetOrdinal("CreateDate")) ? ((DateTime?) (nullable = null)) : ((DateTime?) reader["CreateDate"]);
                entity.CreatePerson = reader.IsDBNull(reader.GetOrdinal("CreatePerson")) ? null : ((string) reader["CreatePerson"]);
                entity.LastModifyPerson = reader.IsDBNull(reader.GetOrdinal("LastModifyPerson")) ? null : ((string) reader["LastModifyPerson"]);
                entity.LastModifyDate = reader.IsDBNull(reader.GetOrdinal("LastModifyDate")) ? ((DateTime?) (nullable = null)) : ((DateTime?) reader["LastModifyDate"]);
                entity.Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? null : ((string) reader["Remark"]);
                entity.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? ((int?) (nullable3 = null)) : ((int?) reader["Status"]);
                entity.CheckPerson = reader.IsDBNull(reader.GetOrdinal("CheckPerson")) ? null : ((string) reader["CheckPerson"]);
                entity.CheckOpinion = reader.IsDBNull(reader.GetOrdinal("CheckOpinion")) ? null : ((string) reader["CheckOpinion"]);
                entity.CheckDate = reader.IsDBNull(reader.GetOrdinal("CheckDate")) ? ((DateTime?) (nullable = null)) : ((DateTime?) reader["CheckDate"]);
                entity.ContractObject = reader.IsDBNull(reader.GetOrdinal("ContractObject")) ? null : ((string) reader["ContractObject"]);
                entity.UnitCode = reader.IsDBNull(reader.GetOrdinal("UnitCode")) ? null : ((string) reader["UnitCode"]);
                entity.ThirdParty = reader.IsDBNull(reader.GetOrdinal("ThirdParty")) ? null : ((string) reader["ThirdParty"]);
                entity.BeforeAccountTotalMoney = reader.IsDBNull(reader.GetOrdinal("BeforeAccountTotalMoney")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["BeforeAccountTotalMoney"]);
                entity.OldSumMoney = reader.IsDBNull(reader.GetOrdinal("oldSumMoney")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["oldSumMoney"]);
                entity.OriginalMoney = reader.IsDBNull(reader.GetOrdinal("OriginalMoney")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["OriginalMoney"]);
                entity.Mostly = reader.IsDBNull(reader.GetOrdinal("Mostly")) ? ((int?) (nullable3 = null)) : ((int?) reader["Mostly"]);
                entity.BiddingCode = reader.IsDBNull(reader.GetOrdinal("BiddingCode")) ? null : ((string) reader["BiddingCode"]);
                entity.BudgetMoney = reader.IsDBNull(reader.GetOrdinal("BudgetMoney")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["BudgetMoney"]);
                entity.AdjustMoney = reader.IsDBNull(reader.GetOrdinal("AdjustMoney")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["AdjustMoney"]);
                entity.DevelopUnit = reader.IsDBNull(reader.GetOrdinal("DevelopUnit")) ? null : ((string) reader["DevelopUnit"]);
                entity.CreateMode = reader.IsDBNull(reader.GetOrdinal("CreateMode")) ? null : ((string) reader["CreateMode"]);
                entity.WorkTime = reader.IsDBNull(reader.GetOrdinal("WorkTime")) ? null : ((string) reader["WorkTime"]);
                entity.MarkSegment = reader.IsDBNull(reader.GetOrdinal("MarkSegment")) ? null : ((string) reader["MarkSegment"]);
                entity.GroupName = reader.IsDBNull(reader.GetOrdinal("GroupName")) ? null : ((string) reader["GroupName"]);
                entity.Building = reader.IsDBNull(reader.GetOrdinal("Building")) ? null : ((string) reader["Building"]);
                entity.PayMode = reader.IsDBNull(reader.GetOrdinal("PayMode")) ? null : ((string) reader["PayMode"]);
                entity.QualityRequire = reader.IsDBNull(reader.GetOrdinal("QualityRequire")) ? null : ((string) reader["QualityRequire"]);
                entity.ContractArea = reader.IsDBNull(reader.GetOrdinal("ContractArea")) ? null : ((string) reader["ContractArea"]);
                entity.ContractDefaultValueCode = reader.IsDBNull(reader.GetOrdinal("ContractDefaultValueCode")) ? null : ((string) reader["ContractDefaultValueCode"]);
                entity.BaoHan = reader.IsDBNull(reader.GetOrdinal("BaoHan")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["BaoHan"]);
                entity.PerformingCircs = reader.IsDBNull(reader.GetOrdinal("PerformingCircs")) ? null : ((string) reader["PerformingCircs"]);
                entity.AccountStatus = reader.IsDBNull(reader.GetOrdinal("AccountStatus")) ? ((int?) (nullable3 = null)) : ((int?) reader["AccountStatus"]);
                entity.AuditingStatus = reader.IsDBNull(reader.GetOrdinal("AuditingStatus")) ? ((int?) (nullable3 = null)) : ((int?) reader["AuditingStatus"]);
                entity.ChangeStatus = reader.IsDBNull(reader.GetOrdinal("ChangeStatus")) ? ((int?) (nullable3 = null)) : ((int?) reader["ChangeStatus"]);
                entity.ChangeCount = reader.IsDBNull(reader.GetOrdinal("ChangeCount")) ? ((int?) (nullable3 = null)) : ((int?) reader["ChangeCount"]);
                entity.WorkStartDate = reader.IsDBNull(reader.GetOrdinal("WorkStartDate")) ? ((DateTime?) (nullable = null)) : ((DateTime?) reader["WorkStartDate"]);
                entity.WorkEndDate = reader.IsDBNull(reader.GetOrdinal("WorkEndDate")) ? ((DateTime?) (nullable = null)) : ((DateTime?) reader["WorkEndDate"]);
                entity.PerCash0 = reader.IsDBNull(reader.GetOrdinal("PerCash0")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash0"]);
                entity.PerCash1 = reader.IsDBNull(reader.GetOrdinal("PerCash1")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash1"]);
                entity.PerCash2 = reader.IsDBNull(reader.GetOrdinal("PerCash2")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash2"]);
                entity.PerCash3 = reader.IsDBNull(reader.GetOrdinal("PerCash3")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash3"]);
                entity.PerCash4 = reader.IsDBNull(reader.GetOrdinal("PerCash4")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash4"]);
                entity.PerCash5 = reader.IsDBNull(reader.GetOrdinal("PerCash5")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash5"]);
                entity.PerCash6 = reader.IsDBNull(reader.GetOrdinal("PerCash6")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash6"]);
                entity.PerCash7 = reader.IsDBNull(reader.GetOrdinal("PerCash7")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash7"]);
                entity.PerCash8 = reader.IsDBNull(reader.GetOrdinal("PerCash8")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash8"]);
                entity.PerCash9 = reader.IsDBNull(reader.GetOrdinal("PerCash9")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["PerCash9"]);
                entity.StampDutyID = reader.IsDBNull(reader.GetOrdinal("StampDutyID")) ? null : ((int?) reader["StampDutyID"]);
                entity.StampDuty = reader.IsDBNull(reader.GetOrdinal("StampDuty")) ? ((decimal?) (nullable2 = null)) : ((decimal?) reader["StampDuty"]);
                entity.AdIssueDate = reader.IsDBNull(reader.GetOrdinal("AdIssueDate")) ? null : ((DateTime?) reader["AdIssueDate"]);
                entity.MoneyType = reader.IsDBNull(reader.GetOrdinal("MoneyType")) ? null : ((string) reader["MoneyType"]);
                entity.ExchangeRate = reader.IsDBNull(reader.GetOrdinal("ExchangeRate")) ? null : ((decimal?) reader["ExchangeRate"]);
                entity.AcceptChanges();
            }
        }
    }
}

