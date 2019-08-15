namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, DataObject, CLSCompliant(true)]
    public abstract class PaymentItemBase : EntityBase, IEntityId<PaymentItemKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private ContractCost _contractCostCodeSource;
        private PaymentItemKey _entityId;
        private Payment _paymentCodeSource;
        private ISite _site;
        private PaymentItemEntityData backupData;
        private PaymentItemEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<PaymentItem> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event PaymentItemEventHandler ColumnChanged;

        [field: NonSerialized]
        public event PaymentItemEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public PaymentItemBase()
        {
            this.inTxn = false;
            this._contractCostCodeSource = null;
            this._paymentCodeSource = null;
            this._site = null;
            this.entityData = new PaymentItemEntityData();
            this.backupData = null;
        }

        public PaymentItemBase(string paymentItemPaymentItemCode, string paymentItemPaymentCode, string paymentItemCostCode, string paymentItemPaymentType, decimal? paymentItemItemMoney, string paymentItemSummary, string paymentItemRemark, string paymentItemAllocateCode, decimal? paymentItemOldItemMoney, string paymentItemAlloType, string paymentItemCostBudgetSetCode, string paymentItemPBSType, string paymentItemPBSCode, string paymentItemDescription, string paymentItemContractCostCode, decimal? paymentItemItemCash, string paymentItemMoneyType, decimal? paymentItemExchangeRate, decimal? paymentItemItemCash0, decimal? paymentItemItemCash1, decimal? paymentItemItemCash2, decimal? paymentItemItemCash3, decimal? paymentItemItemCash4, decimal? paymentItemItemCash5, decimal? paymentItemItemCash6, decimal? paymentItemItemCash7, decimal? paymentItemItemCash8, decimal? paymentItemItemCash9)
        {
            this.inTxn = false;
            this._contractCostCodeSource = null;
            this._paymentCodeSource = null;
            this._site = null;
            this.entityData = new PaymentItemEntityData();
            this.backupData = null;
            this.PaymentItemCode = paymentItemPaymentItemCode;
            this.PaymentCode = paymentItemPaymentCode;
            this.CostCode = paymentItemCostCode;
            this.PaymentType = paymentItemPaymentType;
            this.ItemMoney = paymentItemItemMoney;
            this.Summary = paymentItemSummary;
            this.Remark = paymentItemRemark;
            this.AllocateCode = paymentItemAllocateCode;
            this.OldItemMoney = paymentItemOldItemMoney;
            this.AlloType = paymentItemAlloType;
            this.CostBudgetSetCode = paymentItemCostBudgetSetCode;
            this.PBSType = paymentItemPBSType;
            this.PBSCode = paymentItemPBSCode;
            this.Description = paymentItemDescription;
            this.ContractCostCode = paymentItemContractCostCode;
            this.ItemCash = paymentItemItemCash;
            this.MoneyType = paymentItemMoneyType;
            this.ExchangeRate = paymentItemExchangeRate;
            this.ItemCash0 = paymentItemItemCash0;
            this.ItemCash1 = paymentItemItemCash1;
            this.ItemCash2 = paymentItemItemCash2;
            this.ItemCash3 = paymentItemItemCash3;
            this.ItemCash4 = paymentItemItemCash4;
            this.ItemCash5 = paymentItemItemCash5;
            this.ItemCash6 = paymentItemItemCash6;
            this.ItemCash7 = paymentItemItemCash7;
            this.ItemCash8 = paymentItemItemCash8;
            this.ItemCash9 = paymentItemItemCash9;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.NotNull), "PaymentItemCode");
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PaymentItemCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PaymentCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("CostCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PaymentType", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Summary", 800));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Remark", 800));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("AllocateCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("AlloType", 1));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("CostBudgetSetCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PBSType", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PBSCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Description", 800));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractCostCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("MoneyType", 50));
        }

        public override void CancelChanges()
        {
            IEditableObject obj2 = this;
            obj2.CancelEdit();
        }

        public object Clone()
        {
            return this.Copy();
        }

        public virtual int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public virtual PaymentItem Copy()
        {
            PaymentItem item = new PaymentItem();
            item.PaymentItemCode = this.PaymentItemCode;
            item.OriginalPaymentItemCode = this.OriginalPaymentItemCode;
            item.PaymentCode = this.PaymentCode;
            item.CostCode = this.CostCode;
            item.PaymentType = this.PaymentType;
            item.ItemMoney = this.ItemMoney;
            item.Summary = this.Summary;
            item.Remark = this.Remark;
            item.AllocateCode = this.AllocateCode;
            item.OldItemMoney = this.OldItemMoney;
            item.AlloType = this.AlloType;
            item.CostBudgetSetCode = this.CostBudgetSetCode;
            item.PBSType = this.PBSType;
            item.PBSCode = this.PBSCode;
            item.Description = this.Description;
            item.ContractCostCode = this.ContractCostCode;
            item.ItemCash = this.ItemCash;
            item.MoneyType = this.MoneyType;
            item.ExchangeRate = this.ExchangeRate;
            item.ItemCash0 = this.ItemCash0;
            item.ItemCash1 = this.ItemCash1;
            item.ItemCash2 = this.ItemCash2;
            item.ItemCash3 = this.ItemCash3;
            item.ItemCash4 = this.ItemCash4;
            item.ItemCash5 = this.ItemCash5;
            item.ItemCash6 = this.ItemCash6;
            item.ItemCash7 = this.ItemCash7;
            item.ItemCash8 = this.ItemCash8;
            item.ItemCash9 = this.ItemCash9;
            item.AcceptChanges();
            return item;
        }

        public static PaymentItem CreatePaymentItem(string paymentItemPaymentItemCode, string paymentItemPaymentCode, string paymentItemCostCode, string paymentItemPaymentType, decimal? paymentItemItemMoney, string paymentItemSummary, string paymentItemRemark, string paymentItemAllocateCode, decimal? paymentItemOldItemMoney, string paymentItemAlloType, string paymentItemCostBudgetSetCode, string paymentItemPBSType, string paymentItemPBSCode, string paymentItemDescription, string paymentItemContractCostCode, decimal? paymentItemItemCash, string paymentItemMoneyType, decimal? paymentItemExchangeRate, decimal? paymentItemItemCash0, decimal? paymentItemItemCash1, decimal? paymentItemItemCash2, decimal? paymentItemItemCash3, decimal? paymentItemItemCash4, decimal? paymentItemItemCash5, decimal? paymentItemItemCash6, decimal? paymentItemItemCash7, decimal? paymentItemItemCash8, decimal? paymentItemItemCash9)
        {
            PaymentItem item = new PaymentItem();
            item.PaymentItemCode = paymentItemPaymentItemCode;
            item.PaymentCode = paymentItemPaymentCode;
            item.CostCode = paymentItemCostCode;
            item.PaymentType = paymentItemPaymentType;
            item.ItemMoney = paymentItemItemMoney;
            item.Summary = paymentItemSummary;
            item.Remark = paymentItemRemark;
            item.AllocateCode = paymentItemAllocateCode;
            item.OldItemMoney = paymentItemOldItemMoney;
            item.AlloType = paymentItemAlloType;
            item.CostBudgetSetCode = paymentItemCostBudgetSetCode;
            item.PBSType = paymentItemPBSType;
            item.PBSCode = paymentItemPBSCode;
            item.Description = paymentItemDescription;
            item.ContractCostCode = paymentItemContractCostCode;
            item.ItemCash = paymentItemItemCash;
            item.MoneyType = paymentItemMoneyType;
            item.ExchangeRate = paymentItemExchangeRate;
            item.ItemCash0 = paymentItemItemCash0;
            item.ItemCash1 = paymentItemItemCash1;
            item.ItemCash2 = paymentItemItemCash2;
            item.ItemCash3 = paymentItemItemCash3;
            item.ItemCash4 = paymentItemItemCash4;
            item.ItemCash5 = paymentItemItemCash5;
            item.ItemCash6 = paymentItemItemCash6;
            item.ItemCash7 = paymentItemItemCash7;
            item.ItemCash8 = paymentItemItemCash8;
            item.ItemCash9 = paymentItemItemCash9;
            return item;
        }

        public virtual PaymentItem DeepCopy()
        {
            return EntityHelper.Clone<PaymentItem>(this as PaymentItem);
        }

        public void Dispose()
        {
            this.parentCollection = null;
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                EventHandler disposed = this.Disposed;
                if (disposed != null)
                {
                    disposed(this, EventArgs.Empty);
                }
            }
        }

        public virtual bool Equals(PaymentItemBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(PaymentItemBase Object1, PaymentItemBase Object2)
        {
            if ((Object1 == null) && (Object2 == null))
            {
                return true;
            }
            if ((Object1 == null) ^ (Object2 == null))
            {
                return false;
            }
            bool flag = true;
            if (Object1.PaymentItemCode != Object2.PaymentItemCode)
            {
                flag = false;
            }
            if ((Object1.PaymentCode != null) && (Object2.PaymentCode != null))
            {
                if (Object1.PaymentCode != Object2.PaymentCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.PaymentCode == null) ^ (Object2.PaymentCode == null))
            {
                flag = false;
            }
            if ((Object1.CostCode != null) && (Object2.CostCode != null))
            {
                if (Object1.CostCode != Object2.CostCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.CostCode == null) ^ (Object2.CostCode == null))
            {
                flag = false;
            }
            if ((Object1.PaymentType != null) && (Object2.PaymentType != null))
            {
                if (Object1.PaymentType != Object2.PaymentType)
                {
                    flag = false;
                }
            }
            else if ((Object1.PaymentType == null) ^ (Object2.PaymentType == null))
            {
                flag = false;
            }
            if (Object1.ItemMoney.HasValue && Object2.ItemMoney.HasValue)
            {
                if (Object1.ItemMoney != Object2.ItemMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.ItemMoney.HasValue ^ !Object2.ItemMoney.HasValue)
            {
                flag = false;
            }
            if ((Object1.Summary != null) && (Object2.Summary != null))
            {
                if (Object1.Summary != Object2.Summary)
                {
                    flag = false;
                }
            }
            else if ((Object1.Summary == null) ^ (Object2.Summary == null))
            {
                flag = false;
            }
            if ((Object1.Remark != null) && (Object2.Remark != null))
            {
                if (Object1.Remark != Object2.Remark)
                {
                    flag = false;
                }
            }
            else if ((Object1.Remark == null) ^ (Object2.Remark == null))
            {
                flag = false;
            }
            if ((Object1.AllocateCode != null) && (Object2.AllocateCode != null))
            {
                if (Object1.AllocateCode != Object2.AllocateCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.AllocateCode == null) ^ (Object2.AllocateCode == null))
            {
                flag = false;
            }
            if (Object1.OldItemMoney.HasValue && Object2.OldItemMoney.HasValue)
            {
                if (Object1.OldItemMoney != Object2.OldItemMoney)
                {
                    flag = false;
                }
            }
            else if (!Object1.OldItemMoney.HasValue ^ !Object2.OldItemMoney.HasValue)
            {
                flag = false;
            }
            if ((Object1.AlloType != null) && (Object2.AlloType != null))
            {
                if (Object1.AlloType != Object2.AlloType)
                {
                    flag = false;
                }
            }
            else if ((Object1.AlloType == null) ^ (Object2.AlloType == null))
            {
                flag = false;
            }
            if ((Object1.CostBudgetSetCode != null) && (Object2.CostBudgetSetCode != null))
            {
                if (Object1.CostBudgetSetCode != Object2.CostBudgetSetCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.CostBudgetSetCode == null) ^ (Object2.CostBudgetSetCode == null))
            {
                flag = false;
            }
            if ((Object1.PBSType != null) && (Object2.PBSType != null))
            {
                if (Object1.PBSType != Object2.PBSType)
                {
                    flag = false;
                }
            }
            else if ((Object1.PBSType == null) ^ (Object2.PBSType == null))
            {
                flag = false;
            }
            if ((Object1.PBSCode != null) && (Object2.PBSCode != null))
            {
                if (Object1.PBSCode != Object2.PBSCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.PBSCode == null) ^ (Object2.PBSCode == null))
            {
                flag = false;
            }
            if ((Object1.Description != null) && (Object2.Description != null))
            {
                if (Object1.Description != Object2.Description)
                {
                    flag = false;
                }
            }
            else if ((Object1.Description == null) ^ (Object2.Description == null))
            {
                flag = false;
            }
            if ((Object1.ContractCostCode != null) && (Object2.ContractCostCode != null))
            {
                if (Object1.ContractCostCode != Object2.ContractCostCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.ContractCostCode == null) ^ (Object2.ContractCostCode == null))
            {
                flag = false;
            }
            if (Object1.ItemCash.HasValue && Object2.ItemCash.HasValue)
            {
                if (Object1.ItemCash != Object2.ItemCash)
                {
                    flag = false;
                }
            }
            else if (!Object1.ItemCash.HasValue ^ !Object2.ItemCash.HasValue)
            {
                flag = false;
            }
            if ((Object1.MoneyType != null) && (Object2.MoneyType != null))
            {
                if (Object1.MoneyType != Object2.MoneyType)
                {
                    flag = false;
                }
            }
            else if ((Object1.MoneyType == null) ^ (Object2.MoneyType == null))
            {
                flag = false;
            }
            if (Object1.ExchangeRate.HasValue && Object2.ExchangeRate.HasValue)
            {
                if (Object1.ExchangeRate != Object2.ExchangeRate)
                {
                    flag = false;
                }
            }
            else if (!Object1.ExchangeRate.HasValue ^ !Object2.ExchangeRate.HasValue)
            {
                flag = false;
            }
            if (Object1.ItemCash0.HasValue && Object2.ItemCash0.HasValue)
            {
                if (Object1.ItemCash0 != Object2.ItemCash0)
                {
                    flag = false;
                }
            }
            else if (!Object1.ItemCash0.HasValue ^ !Object2.ItemCash0.HasValue)
            {
                flag = false;
            }
            if (Object1.ItemCash1.HasValue && Object2.ItemCash1.HasValue)
            {
                if (Object1.ItemCash1 != Object2.ItemCash1)
                {
                    flag = false;
                }
            }
            else if (!Object1.ItemCash1.HasValue ^ !Object2.ItemCash1.HasValue)
            {
                flag = false;
            }
            if (Object1.ItemCash2.HasValue && Object2.ItemCash2.HasValue)
            {
                if (Object1.ItemCash2 != Object2.ItemCash2)
                {
                    flag = false;
                }
            }
            else if (!Object1.ItemCash2.HasValue ^ !Object2.ItemCash2.HasValue)
            {
                flag = false;
            }
            if (Object1.ItemCash3.HasValue && Object2.ItemCash3.HasValue)
            {
                if (Object1.ItemCash3 != Object2.ItemCash3)
                {
                    flag = false;
                }
            }
            else if (!Object1.ItemCash3.HasValue ^ !Object2.ItemCash3.HasValue)
            {
                flag = false;
            }
            if (Object1.ItemCash4.HasValue && Object2.ItemCash4.HasValue)
            {
                if (Object1.ItemCash4 != Object2.ItemCash4)
                {
                    flag = false;
                }
            }
            else if (!Object1.ItemCash4.HasValue ^ !Object2.ItemCash4.HasValue)
            {
                flag = false;
            }
            if (Object1.ItemCash5.HasValue && Object2.ItemCash5.HasValue)
            {
                if (Object1.ItemCash5 != Object2.ItemCash5)
                {
                    flag = false;
                }
            }
            else if (!Object1.ItemCash5.HasValue ^ !Object2.ItemCash5.HasValue)
            {
                flag = false;
            }
            if (Object1.ItemCash6.HasValue && Object2.ItemCash6.HasValue)
            {
                if (Object1.ItemCash6 != Object2.ItemCash6)
                {
                    flag = false;
                }
            }
            else if (!Object1.ItemCash6.HasValue ^ !Object2.ItemCash6.HasValue)
            {
                flag = false;
            }
            if (Object1.ItemCash7.HasValue && Object2.ItemCash7.HasValue)
            {
                if (Object1.ItemCash7 != Object2.ItemCash7)
                {
                    flag = false;
                }
            }
            else if (!Object1.ItemCash7.HasValue ^ !Object2.ItemCash7.HasValue)
            {
                flag = false;
            }
            if (Object1.ItemCash8.HasValue && Object2.ItemCash8.HasValue)
            {
                if (Object1.ItemCash8 != Object2.ItemCash8)
                {
                    flag = false;
                }
            }
            else if (!Object1.ItemCash8.HasValue ^ !Object2.ItemCash8.HasValue)
            {
                flag = false;
            }
            if (Object1.ItemCash9.HasValue && Object2.ItemCash9.HasValue)
            {
                if (Object1.ItemCash9 != Object2.ItemCash9)
                {
                    flag = false;
                }
                return flag;
            }
            if (!Object1.ItemCash9.HasValue ^ !Object2.ItemCash9.HasValue)
            {
                flag = false;
            }
            return flag;
        }

        public static object MakeCopyOf(object x)
        {
            if (x is ICloneable)
            {
                return ((ICloneable) x).Clone();
            }
            throw new NotSupportedException("Object Does Not Implement the ICloneable Interface.");
        }

        public void OnCancelAddNew()
        {
            if (!base.SuppressEntityEvents)
            {
                CancelAddNewEventHandler cancelAddNew = this.CancelAddNew;
                if (cancelAddNew != null)
                {
                    cancelAddNew(this, EventArgs.Empty);
                }
            }
        }

        public void OnColumnChanged(PaymentItemColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(PaymentItemColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                PaymentItemEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new PaymentItemEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(PaymentItemColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(PaymentItemColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                PaymentItemEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new PaymentItemEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as PaymentItem);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as PaymentItemEntityData;
                this.inTxn = true;
            }
        }

        void IEditableObject.CancelEdit()
        {
            if (this.inTxn)
            {
                this.entityData = this.backupData;
                this.backupData = null;
                this.inTxn = false;
                if (base.bindingIsNew && (this.parentCollection != null))
                {
                    this.parentCollection.Remove((PaymentItem) this);
                }
            }
        }

        void IEditableObject.EndEdit()
        {
            if (this.inTxn)
            {
                this.backupData = null;
                if (base.IsDirty)
                {
                    if (base.bindingIsNew)
                    {
                        this.EntityState = EntityState.Added;
                        base.bindingIsNew = false;
                    }
                    else if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                }
                base.bindingIsNew = false;
                this.inTxn = false;
            }
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{29}{28}- PaymentItemCode: {0}{28}- PaymentCode: {1}{28}- CostCode: {2}{28}- PaymentType: {3}{28}- ItemMoney: {4}{28}- Summary: {5}{28}- Remark: {6}{28}- AllocateCode: {7}{28}- OldItemMoney: {8}{28}- AlloType: {9}{28}- CostBudgetSetCode: {10}{28}- PBSType: {11}{28}- PBSCode: {12}{28}- Description: {13}{28}- ContractCostCode: {14}{28}- ItemCash: {15}{28}- MoneyType: {16}{28}- ExchangeRate: {17}{28}- ItemCash0: {18}{28}- ItemCash1: {19}{28}- ItemCash2: {20}{28}- ItemCash3: {21}{28}- ItemCash4: {22}{28}- ItemCash5: {23}{28}- ItemCash6: {24}{28}- ItemCash7: {25}{28}- ItemCash8: {26}{28}- ItemCash9: {27}{28}", new object[] { 
                this.PaymentItemCode, (this.PaymentCode == null) ? string.Empty : this.PaymentCode.ToString(), (this.CostCode == null) ? string.Empty : this.CostCode.ToString(), (this.PaymentType == null) ? string.Empty : this.PaymentType.ToString(), !this.ItemMoney.HasValue ? string.Empty : this.ItemMoney.ToString(), (this.Summary == null) ? string.Empty : this.Summary.ToString(), (this.Remark == null) ? string.Empty : this.Remark.ToString(), (this.AllocateCode == null) ? string.Empty : this.AllocateCode.ToString(), !this.OldItemMoney.HasValue ? string.Empty : this.OldItemMoney.ToString(), (this.AlloType == null) ? string.Empty : this.AlloType.ToString(), (this.CostBudgetSetCode == null) ? string.Empty : this.CostBudgetSetCode.ToString(), (this.PBSType == null) ? string.Empty : this.PBSType.ToString(), (this.PBSCode == null) ? string.Empty : this.PBSCode.ToString(), (this.Description == null) ? string.Empty : this.Description.ToString(), (this.ContractCostCode == null) ? string.Empty : this.ContractCostCode.ToString(), !this.ItemCash.HasValue ? string.Empty : this.ItemCash.ToString(), 
                (this.MoneyType == null) ? string.Empty : this.MoneyType.ToString(), !this.ExchangeRate.HasValue ? string.Empty : this.ExchangeRate.ToString(), !this.ItemCash0.HasValue ? string.Empty : this.ItemCash0.ToString(), !this.ItemCash1.HasValue ? string.Empty : this.ItemCash1.ToString(), !this.ItemCash2.HasValue ? string.Empty : this.ItemCash2.ToString(), !this.ItemCash3.HasValue ? string.Empty : this.ItemCash3.ToString(), !this.ItemCash4.HasValue ? string.Empty : this.ItemCash4.ToString(), !this.ItemCash5.HasValue ? string.Empty : this.ItemCash5.ToString(), !this.ItemCash6.HasValue ? string.Empty : this.ItemCash6.ToString(), !this.ItemCash7.HasValue ? string.Empty : this.ItemCash7.ToString(), !this.ItemCash8.HasValue ? string.Empty : this.ItemCash8.ToString(), !this.ItemCash9.HasValue ? string.Empty : this.ItemCash9.ToString(), Environment.NewLine, base.GetType()
             });
        }

        [TiannuoPM.Entities.Bindable, Description("合同明细编号"), DataObjectField(false, false, true, 50)]
        public virtual string AllocateCode
        {
            get
            {
                return this.entityData.AllocateCode;
            }
            set
            {
                if (this.entityData.AllocateCode != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.AllocateCode, this.entityData.AllocateCode);
                    this.entityData.AllocateCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.AllocateCode, this.entityData.AllocateCode);
                    this.OnPropertyChanged("AllocateCode");
                }
            }
        }

        [DataObjectField(false, false, true, 1), TiannuoPM.Entities.Bindable, Description("x")]
        public virtual string AlloType
        {
            get
            {
                return this.entityData.AlloType;
            }
            set
            {
                if (this.entityData.AlloType != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.AlloType, this.entityData.AlloType);
                    this.entityData.AlloType = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.AlloType, this.entityData.AlloType);
                    this.OnPropertyChanged("AlloType");
                }
            }
        }

        [Description("x"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string ContractCostCode
        {
            get
            {
                return this.entityData.ContractCostCode;
            }
            set
            {
                if (this.entityData.ContractCostCode != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.ContractCostCode, this.entityData.ContractCostCode);
                    this.entityData.ContractCostCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.ContractCostCode, this.entityData.ContractCostCode);
                    this.OnPropertyChanged("ContractCostCode");
                }
            }
        }

        [XmlIgnore, Browsable(false), TiannuoPM.Entities.Bindable]
        public virtual ContractCost ContractCostCodeSource
        {
            get
            {
                return this._contractCostCodeSource;
            }
            set
            {
                this._contractCostCodeSource = value;
            }
        }

        [DataObjectField(false, false, true, 50), Description("预算表编号"), TiannuoPM.Entities.Bindable]
        public virtual string CostBudgetSetCode
        {
            get
            {
                return this.entityData.CostBudgetSetCode;
            }
            set
            {
                if (this.entityData.CostBudgetSetCode != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.CostBudgetSetCode, this.entityData.CostBudgetSetCode);
                    this.entityData.CostBudgetSetCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.CostBudgetSetCode, this.entityData.CostBudgetSetCode);
                    this.OnPropertyChanged("CostBudgetSetCode");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("cost编号？？好像cost现在没用？"), TiannuoPM.Entities.Bindable]
        public virtual string CostCode
        {
            get
            {
                return this.entityData.CostCode;
            }
            set
            {
                if (this.entityData.CostCode != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.CostCode, this.entityData.CostCode);
                    this.entityData.CostCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.CostCode, this.entityData.CostCode);
                    this.OnPropertyChanged("CostCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 800), Description("")]
        public virtual string Description
        {
            get
            {
                return this.entityData.Description;
            }
            set
            {
                if (this.entityData.Description != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.Description, this.entityData.Description);
                    this.entityData.Description = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.Description, this.entityData.Description);
                    this.OnPropertyChanged("Description");
                }
            }
        }

        [XmlIgnore]
        public PaymentItemKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new PaymentItemKey(this);
                }
                return this._entityId;
            }
            set
            {
                if (value != null)
                {
                    value.Entity = this;
                }
                this._entityId = value;
            }
        }

        [XmlIgnore]
        public override string EntityTrackingKey
        {
            get
            {
                if (this.entityTrackingKey == null)
                {
                    this.entityTrackingKey = "PaymentItem" + this.PaymentItemCode.ToString();
                }
                return this.entityTrackingKey;
            }
            set
            {
                if (value != null)
                {
                    this.entityTrackingKey = value;
                }
            }
        }

        [DataObjectField(false, false, true), Description(""), TiannuoPM.Entities.Bindable]
        public virtual decimal? ExchangeRate
        {
            get
            {
                return this.entityData.ExchangeRate;
            }
            set
            {
                if (this.entityData.ExchangeRate != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.ExchangeRate, this.entityData.ExchangeRate);
                    this.entityData.ExchangeRate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.ExchangeRate, this.entityData.ExchangeRate);
                    this.OnPropertyChanged("ExchangeRate");
                }
            }
        }

        [Description("原币金额"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? ItemCash
        {
            get
            {
                return this.entityData.ItemCash;
            }
            set
            {
                if (this.entityData.ItemCash != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.ItemCash, this.entityData.ItemCash);
                    this.entityData.ItemCash = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.ItemCash, this.entityData.ItemCash);
                    this.OnPropertyChanged("ItemCash");
                }
            }
        }

        [DataObjectField(false, false, true), Description("请款金额"), TiannuoPM.Entities.Bindable]
        public virtual decimal? ItemCash0
        {
            get
            {
                return this.entityData.ItemCash0;
            }
            set
            {
                if (this.entityData.ItemCash0 != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.ItemCash0, this.entityData.ItemCash0);
                    this.entityData.ItemCash0 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.ItemCash0, this.entityData.ItemCash0);
                    this.OnPropertyChanged("ItemCash0");
                }
            }
        }

        [Description("预付款"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? ItemCash1
        {
            get
            {
                return this.entityData.ItemCash1;
            }
            set
            {
                if (this.entityData.ItemCash1 != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.ItemCash1, this.entityData.ItemCash1);
                    this.entityData.ItemCash1 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.ItemCash1, this.entityData.ItemCash1);
                    this.OnPropertyChanged("ItemCash1");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true), Description("验收款")]
        public virtual decimal? ItemCash2
        {
            get
            {
                return this.entityData.ItemCash2;
            }
            set
            {
                if (this.entityData.ItemCash2 != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.ItemCash2, this.entityData.ItemCash2);
                    this.entityData.ItemCash2 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.ItemCash2, this.entityData.ItemCash2);
                    this.OnPropertyChanged("ItemCash2");
                }
            }
        }

        [Description("保修款"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? ItemCash3
        {
            get
            {
                return this.entityData.ItemCash3;
            }
            set
            {
                if (this.entityData.ItemCash3 != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.ItemCash3, this.entityData.ItemCash3);
                    this.entityData.ItemCash3 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.ItemCash3, this.entityData.ItemCash3);
                    this.OnPropertyChanged("ItemCash3");
                }
            }
        }

        [DataObjectField(false, false, true), Description("结算扣款"), TiannuoPM.Entities.Bindable]
        public virtual decimal? ItemCash4
        {
            get
            {
                return this.entityData.ItemCash4;
            }
            set
            {
                if (this.entityData.ItemCash4 != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.ItemCash4, this.entityData.ItemCash4);
                    this.entityData.ItemCash4 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.ItemCash4, this.entityData.ItemCash4);
                    this.OnPropertyChanged("ItemCash4");
                }
            }
        }

        [DataObjectField(false, false, true), Description("扣款保留"), TiannuoPM.Entities.Bindable]
        public virtual decimal? ItemCash5
        {
            get
            {
                return this.entityData.ItemCash5;
            }
            set
            {
                if (this.entityData.ItemCash5 != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.ItemCash5, this.entityData.ItemCash5);
                    this.entityData.ItemCash5 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.ItemCash5, this.entityData.ItemCash5);
                    this.OnPropertyChanged("ItemCash5");
                }
            }
        }

        [DataObjectField(false, false, true), TiannuoPM.Entities.Bindable, Description("其他扣款")]
        public virtual decimal? ItemCash6
        {
            get
            {
                return this.entityData.ItemCash6;
            }
            set
            {
                if (this.entityData.ItemCash6 != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.ItemCash6, this.entityData.ItemCash6);
                    this.entityData.ItemCash6 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.ItemCash6, this.entityData.ItemCash6);
                    this.OnPropertyChanged("ItemCash6");
                }
            }
        }

        [DataObjectField(false, false, true), TiannuoPM.Entities.Bindable, Description("")]
        public virtual decimal? ItemCash7
        {
            get
            {
                return this.entityData.ItemCash7;
            }
            set
            {
                if (this.entityData.ItemCash7 != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.ItemCash7, this.entityData.ItemCash7);
                    this.entityData.ItemCash7 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.ItemCash7, this.entityData.ItemCash7);
                    this.OnPropertyChanged("ItemCash7");
                }
            }
        }

        [DataObjectField(false, false, true), Description(""), TiannuoPM.Entities.Bindable]
        public virtual decimal? ItemCash8
        {
            get
            {
                return this.entityData.ItemCash8;
            }
            set
            {
                if (this.entityData.ItemCash8 != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.ItemCash8, this.entityData.ItemCash8);
                    this.entityData.ItemCash8 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.ItemCash8, this.entityData.ItemCash8);
                    this.OnPropertyChanged("ItemCash8");
                }
            }
        }

        [DataObjectField(false, false, true), Description(""), TiannuoPM.Entities.Bindable]
        public virtual decimal? ItemCash9
        {
            get
            {
                return this.entityData.ItemCash9;
            }
            set
            {
                if (this.entityData.ItemCash9 != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.ItemCash9, this.entityData.ItemCash9);
                    this.entityData.ItemCash9 = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.ItemCash9, this.entityData.ItemCash9);
                    this.OnPropertyChanged("ItemCash9");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true), Description("付款金额")]
        public virtual decimal? ItemMoney
        {
            get
            {
                return this.entityData.ItemMoney;
            }
            set
            {
                if (this.entityData.ItemMoney != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.ItemMoney, this.entityData.ItemMoney);
                    this.entityData.ItemMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.ItemMoney, this.entityData.ItemMoney);
                    this.OnPropertyChanged("ItemMoney");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description(""), DataObjectField(false, false, true, 50)]
        public virtual string MoneyType
        {
            get
            {
                return this.entityData.MoneyType;
            }
            set
            {
                if (this.entityData.MoneyType != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.MoneyType, this.entityData.MoneyType);
                    this.entityData.MoneyType = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.MoneyType, this.entityData.MoneyType);
                    this.OnPropertyChanged("MoneyType");
                }
            }
        }

        [Description("结算前请款金额"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? OldItemMoney
        {
            get
            {
                return this.entityData.OldItemMoney;
            }
            set
            {
                if (this.entityData.OldItemMoney != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.OldItemMoney, this.entityData.OldItemMoney);
                    this.entityData.OldItemMoney = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.OldItemMoney, this.entityData.OldItemMoney);
                    this.OnPropertyChanged("OldItemMoney");
                }
            }
        }

        [Browsable(false)]
        public virtual string OriginalPaymentItemCode
        {
            get
            {
                return this.entityData.OriginalPaymentItemCode;
            }
            set
            {
                this.entityData.OriginalPaymentItemCode = value;
            }
        }

        [Browsable(false), XmlIgnore]
        public override object ParentCollection
        {
            get
            {
                return this.parentCollection;
            }
            set
            {
                this.parentCollection = value as TList<PaymentItem>;
            }
        }

        [DataObjectField(false, false, true, 50), Description("请款code"), TiannuoPM.Entities.Bindable]
        public virtual string PaymentCode
        {
            get
            {
                return this.entityData.PaymentCode;
            }
            set
            {
                if (this.entityData.PaymentCode != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.PaymentCode, this.entityData.PaymentCode);
                    this.entityData.PaymentCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.PaymentCode, this.entityData.PaymentCode);
                    this.OnPropertyChanged("PaymentCode");
                }
            }
        }

        [XmlIgnore, Browsable(false), TiannuoPM.Entities.Bindable]
        public virtual Payment PaymentCodeSource
        {
            get
            {
                return this._paymentCodeSource;
            }
            set
            {
                this._paymentCodeSource = value;
            }
        }

        [TiannuoPM.Entities.Bindable, Description("请款明细code"), DataObjectField(true, false, false, 50)]
        public virtual string PaymentItemCode
        {
            get
            {
                return this.entityData.PaymentItemCode;
            }
            set
            {
                if (this.entityData.PaymentItemCode != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.PaymentItemCode, this.entityData.PaymentItemCode);
                    this.entityData.PaymentItemCode = value;
                    this.EntityId.PaymentItemCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.PaymentItemCode, this.entityData.PaymentItemCode);
                    this.OnPropertyChanged("PaymentItemCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("x付款类型"), DataObjectField(false, false, true, 50)]
        public virtual string PaymentType
        {
            get
            {
                return this.entityData.PaymentType;
            }
            set
            {
                if (this.entityData.PaymentType != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.PaymentType, this.entityData.PaymentType);
                    this.entityData.PaymentType = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.PaymentType, this.entityData.PaymentType);
                    this.OnPropertyChanged("PaymentType");
                }
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<PayoutItem> PayoutItemCollection
        {
            get
            {
                return this.entityData.PayoutItemCollection;
            }
            set
            {
                this.entityData.PayoutItemCollection = value;
            }
        }

        [TiannuoPM.Entities.Bindable, Description("x"), DataObjectField(false, false, true, 50)]
        public virtual string PBSCode
        {
            get
            {
                return this.entityData.PBSCode;
            }
            set
            {
                if (this.entityData.PBSCode != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.PBSCode, this.entityData.PBSCode);
                    this.entityData.PBSCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.PBSCode, this.entityData.PBSCode);
                    this.OnPropertyChanged("PBSCode");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("x"), TiannuoPM.Entities.Bindable]
        public virtual string PBSType
        {
            get
            {
                return this.entityData.PBSType;
            }
            set
            {
                if (this.entityData.PBSType != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.PBSType, this.entityData.PBSType);
                    this.entityData.PBSType = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.PBSType, this.entityData.PBSType);
                    this.OnPropertyChanged("PBSType");
                }
            }
        }

        [DataObjectField(false, false, true, 800), Description("？备注"), TiannuoPM.Entities.Bindable]
        public virtual string Remark
        {
            get
            {
                return this.entityData.Remark;
            }
            set
            {
                if (this.entityData.Remark != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.Remark, this.entityData.Remark);
                    this.entityData.Remark = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.Remark, this.entityData.Remark);
                    this.OnPropertyChanged("Remark");
                }
            }
        }

        [SoapIgnore, Browsable(false), XmlIgnore]
        public ISite Site
        {
            get
            {
                return this._site;
            }
            set
            {
                this._site = value;
            }
        }

        [DataObjectField(false, false, true, 800), TiannuoPM.Entities.Bindable, Description("款项名称")]
        public virtual string Summary
        {
            get
            {
                return this.entityData.Summary;
            }
            set
            {
                if (this.entityData.Summary != value)
                {
                    this.OnColumnChanging(PaymentItemColumn.Summary, this.entityData.Summary);
                    this.entityData.Summary = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(PaymentItemColumn.Summary, this.entityData.Summary);
                    this.OnPropertyChanged("Summary");
                }
            }
        }

        [Browsable(false), XmlIgnore]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { 
                    "PaymentItemCode", "PaymentCode", "CostCode", "PaymentType", "ItemMoney", "Summary", "Remark", "AllocateCode", "OldItemMoney", "AlloType", "CostBudgetSetCode", "PBSType", "PBSCode", "Description", "ContractCostCode", "ItemCash", 
                    "MoneyType", "ExchangeRate", "ItemCash0", "ItemCash1", "ItemCash2", "ItemCash3", "ItemCash4", "ItemCash5", "ItemCash6", "ItemCash7", "ItemCash8", "ItemCash9"
                 };
            }
        }

        [Browsable(false), XmlIgnore]
        public override string TableName
        {
            get
            {
                return "PaymentItem";
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class PaymentItemEntityData : ICloneable
        {
            public string AllocateCode = null;
            public string AlloType = null;
            public string ContractCostCode = null;
            public string CostBudgetSetCode = null;
            public string CostCode = null;
            public string Description = null;
            public decimal? ExchangeRate = null;
            public decimal? ItemCash = null;
            public decimal? ItemCash0 = 0;
            public decimal? ItemCash1 = 0;
            public decimal? ItemCash2 = 0;
            public decimal? ItemCash3 = 0;
            public decimal? ItemCash4 = 0;
            public decimal? ItemCash5 = 0;
            public decimal? ItemCash6 = 0;
            public decimal? ItemCash7 = 0;
            public decimal? ItemCash8 = 0;
            public decimal? ItemCash9 = 0;
            public decimal? ItemMoney = null;
            public string MoneyType = null;
            public decimal? OldItemMoney = null;
            public string OriginalPaymentItemCode;
            public string PaymentCode = null;
            public string PaymentItemCode;
            public string PaymentType = null;
            private TList<PayoutItem> payoutItemPaymentItemCode;
            public string PBSCode = null;
            public string PBSType = null;
            public string Remark = null;
            public string Summary = null;

            public object Clone()
            {
                PaymentItemBase.PaymentItemEntityData data = new PaymentItemBase.PaymentItemEntityData();
                data.PaymentItemCode = this.PaymentItemCode;
                data.OriginalPaymentItemCode = this.OriginalPaymentItemCode;
                data.PaymentCode = this.PaymentCode;
                data.CostCode = this.CostCode;
                data.PaymentType = this.PaymentType;
                data.ItemMoney = this.ItemMoney;
                data.Summary = this.Summary;
                data.Remark = this.Remark;
                data.AllocateCode = this.AllocateCode;
                data.OldItemMoney = this.OldItemMoney;
                data.AlloType = this.AlloType;
                data.CostBudgetSetCode = this.CostBudgetSetCode;
                data.PBSType = this.PBSType;
                data.PBSCode = this.PBSCode;
                data.Description = this.Description;
                data.ContractCostCode = this.ContractCostCode;
                data.ItemCash = this.ItemCash;
                data.MoneyType = this.MoneyType;
                data.ExchangeRate = this.ExchangeRate;
                data.ItemCash0 = this.ItemCash0;
                data.ItemCash1 = this.ItemCash1;
                data.ItemCash2 = this.ItemCash2;
                data.ItemCash3 = this.ItemCash3;
                data.ItemCash4 = this.ItemCash4;
                data.ItemCash5 = this.ItemCash5;
                data.ItemCash6 = this.ItemCash6;
                data.ItemCash7 = this.ItemCash7;
                data.ItemCash8 = this.ItemCash8;
                data.ItemCash9 = this.ItemCash9;
                return data;
            }

            public TList<PayoutItem> PayoutItemCollection
            {
                get
                {
                    if (this.payoutItemPaymentItemCode == null)
                    {
                        this.payoutItemPaymentItemCode = new TList<PayoutItem>();
                    }
                    return this.payoutItemPaymentItemCode;
                }
                set
                {
                    this.payoutItemPaymentItemCode = value;
                }
            }
        }
    }
}

