namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class ContractBillProviderBaseCore : EntityProviderBase<ContractBill, ContractBillKey>
    {
        protected ContractBillProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, ContractBill entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if ((entity != null) && (base.CanDeepLoad(entity, "Contract", "ContractCodeSource", deepLoadType, innerList) && (entity.ContractCodeSource == null)))
            {
                object[] pkItems = new object[] { entity.ContractCode ?? string.Empty };
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
        }

        internal override bool DeepSave(TransactionManager transactionManager, ContractBill entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity == null)
            {
                return false;
            }
            if (base.CanDeepSave(entity, "Contract", "ContractCodeSource", deepSaveType, innerList) && (entity.ContractCodeSource != null))
            {
                DataRepository.ContractProvider.Save(transactionManager, entity.ContractCodeSource);
                entity.ContractCode = entity.ContractCodeSource.ContractCode;
            }
            this.Save(transactionManager, entity);
            return true;
        }

        public bool Delete(int code)
        {
            return this.Delete(null, code);
        }

        public abstract bool Delete(TransactionManager transactionManager, int code);
        public override bool Delete(TransactionManager transactionManager, ContractBillKey key)
        {
            return this.Delete(transactionManager, key.Code);
        }

        public static TList<ContractBill> Fill(IDataReader reader, TList<ContractBill> rows, int start, int pageLength)
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
                ContractBill item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "ContractBill" + (reader.IsDBNull(reader.GetOrdinal("Code")) ? 0 : ((int) reader["Code"])).ToString();
                    item = EntityManager.LocateOrCreate<ContractBill>(text.ToString(), "ContractBill", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new ContractBill();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    item.SuppressEntityEvents = true;
                    item.Code = (int) reader["Code"];
                    item.ProjectCode = reader.IsDBNull(reader.GetOrdinal("ProjectCode")) ? null : ((string) reader["ProjectCode"]);
                    item.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                    item.BillNo = reader.IsDBNull(reader.GetOrdinal("BillNo")) ? null : ((string) reader["BillNo"]);
                    item.BillMoney = reader.IsDBNull(reader.GetOrdinal("BillMoney")) ? null : ((decimal?) reader["BillMoney"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override ContractBill Get(TransactionManager transactionManager, ContractBillKey key, int start, int pageLength)
        {
            return this.GetByCode(transactionManager, key.Code, start, pageLength);
        }

        public ContractBill GetByCode(int code)
        {
            int count = -1;
            return this.GetByCode(null, code, 0, 0x7fffffff, out count);
        }

        public ContractBill GetByCode(TransactionManager transactionManager, int code)
        {
            int count = -1;
            return this.GetByCode(transactionManager, code, 0, 0x7fffffff, out count);
        }

        public ContractBill GetByCode(int code, int start, int pageLength)
        {
            int count = -1;
            return this.GetByCode(null, code, start, pageLength, out count);
        }

        public ContractBill GetByCode(int code, int start, int pageLength, out int count)
        {
            return this.GetByCode(null, code, start, pageLength, out count);
        }

        public ContractBill GetByCode(TransactionManager transactionManager, int code, int start, int pageLength)
        {
            int count = -1;
            return this.GetByCode(transactionManager, code, start, pageLength, out count);
        }

        public abstract ContractBill GetByCode(TransactionManager transactionManager, int code, int start, int pageLength, out int count);
        public TList<ContractBill> GetByContractCode(string contractCode)
        {
            int count = -1;
            return this.GetByContractCode(contractCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractBill> GetByContractCode(TransactionManager transactionManager, string contractCode)
        {
            int count = -1;
            return this.GetByContractCode(transactionManager, contractCode, 0, 0x7fffffff, out count);
        }

        public TList<ContractBill> GetByContractCode(string contractCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCode(null, contractCode, start, pageLength, out count);
        }

        public TList<ContractBill> GetByContractCode(string contractCode, int start, int pageLength, out int count)
        {
            return this.GetByContractCode(null, contractCode, start, pageLength, out count);
        }

        public TList<ContractBill> GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByContractCode(transactionManager, contractCode, start, pageLength, out count);
        }

        public abstract TList<ContractBill> GetByContractCode(TransactionManager transactionManager, string contractCode, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, ContractBill entity)
        {
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.Code = (int) row["Code"];
            entity.ProjectCode = Convert.IsDBNull(row["ProjectCode"]) ? null : ((string) row["ProjectCode"]);
            entity.ContractCode = Convert.IsDBNull(row["ContractCode"]) ? null : ((string) row["ContractCode"]);
            entity.BillNo = Convert.IsDBNull(row["BillNo"]) ? null : ((string) row["BillNo"]);
            entity.BillMoney = Convert.IsDBNull(row["BillMoney"]) ? null : ((decimal?) row["BillMoney"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, ContractBill entity)
        {
            if (reader.Read())
            {
                entity.Code = (int) reader["Code"];
                entity.ProjectCode = reader.IsDBNull(reader.GetOrdinal("ProjectCode")) ? null : ((string) reader["ProjectCode"]);
                entity.ContractCode = reader.IsDBNull(reader.GetOrdinal("ContractCode")) ? null : ((string) reader["ContractCode"]);
                entity.BillNo = reader.IsDBNull(reader.GetOrdinal("BillNo")) ? null : ((string) reader["BillNo"]);
                entity.BillMoney = reader.IsDBNull(reader.GetOrdinal("BillMoney")) ? null : ((decimal?) reader["BillMoney"]);
                entity.AcceptChanges();
            }
        }
    }
}

