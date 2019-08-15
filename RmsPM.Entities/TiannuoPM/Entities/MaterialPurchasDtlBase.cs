namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, CLSCompliant(true), DataObject]
    public abstract class MaterialPurchasDtlBase : EntityBase, IEntityId<MaterialPurchasDtlKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private MaterialPurchasDtlKey _entityId;
        private MaterialPurchas _materialPurchasIDSource;
        private ISite _site;
        private MaterialPurchasDtlEntityData backupData;
        private MaterialPurchasDtlEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<MaterialPurchasDtl> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event MaterialPurchasDtlEventHandler ColumnChanged;

        [field: NonSerialized]
        public event MaterialPurchasDtlEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public MaterialPurchasDtlBase()
        {
            this.inTxn = false;
            this._materialPurchasIDSource = null;
            this._site = null;
            this.entityData = new MaterialPurchasDtlEntityData();
            this.backupData = null;
        }

        public MaterialPurchasDtlBase(int? materialPurchasDtlMaterialPurchasID, string materialPurchasDtlTypeStandard, string materialPurchasDtlUnit, decimal? materialPurchasDtlNumber, DateTime? materialPurchasDtlNeedDate, DateTime? materialPurchasDtlSignDate, string materialPurchasDtlSearchPriceDtl, decimal? materialPurchasDtlFinalPrice)
        {
            this.inTxn = false;
            this._materialPurchasIDSource = null;
            this._site = null;
            this.entityData = new MaterialPurchasDtlEntityData();
            this.backupData = null;
            this.MaterialPurchasID = materialPurchasDtlMaterialPurchasID;
            this.TypeStandard = materialPurchasDtlTypeStandard;
            this.Unit = materialPurchasDtlUnit;
            this.Number = materialPurchasDtlNumber;
            this.NeedDate = materialPurchasDtlNeedDate;
            this.SignDate = materialPurchasDtlSignDate;
            this.SearchPriceDtl = materialPurchasDtlSearchPriceDtl;
            this.FinalPrice = materialPurchasDtlFinalPrice;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("TypeStandard", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Unit", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("SearchPriceDtl", 0x1388));
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

        public virtual MaterialPurchasDtl Copy()
        {
            MaterialPurchasDtl dtl = new MaterialPurchasDtl();
            dtl.MaterialPurchasDtlID = this.MaterialPurchasDtlID;
            dtl.MaterialPurchasID = this.MaterialPurchasID;
            dtl.TypeStandard = this.TypeStandard;
            dtl.Unit = this.Unit;
            dtl.Number = this.Number;
            dtl.NeedDate = this.NeedDate;
            dtl.SignDate = this.SignDate;
            dtl.SearchPriceDtl = this.SearchPriceDtl;
            dtl.FinalPrice = this.FinalPrice;
            dtl.AcceptChanges();
            return dtl;
        }

        public static MaterialPurchasDtl CreateMaterialPurchasDtl(int? materialPurchasDtlMaterialPurchasID, string materialPurchasDtlTypeStandard, string materialPurchasDtlUnit, decimal? materialPurchasDtlNumber, DateTime? materialPurchasDtlNeedDate, DateTime? materialPurchasDtlSignDate, string materialPurchasDtlSearchPriceDtl, decimal? materialPurchasDtlFinalPrice)
        {
            MaterialPurchasDtl dtl = new MaterialPurchasDtl();
            dtl.MaterialPurchasID = materialPurchasDtlMaterialPurchasID;
            dtl.TypeStandard = materialPurchasDtlTypeStandard;
            dtl.Unit = materialPurchasDtlUnit;
            dtl.Number = materialPurchasDtlNumber;
            dtl.NeedDate = materialPurchasDtlNeedDate;
            dtl.SignDate = materialPurchasDtlSignDate;
            dtl.SearchPriceDtl = materialPurchasDtlSearchPriceDtl;
            dtl.FinalPrice = materialPurchasDtlFinalPrice;
            return dtl;
        }

        public virtual MaterialPurchasDtl DeepCopy()
        {
            return EntityHelper.Clone<MaterialPurchasDtl>(this as MaterialPurchasDtl);
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

        public virtual bool Equals(MaterialPurchasDtlBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(MaterialPurchasDtlBase Object1, MaterialPurchasDtlBase Object2)
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
            if (Object1.MaterialPurchasDtlID != Object2.MaterialPurchasDtlID)
            {
                flag = false;
            }
            if (Object1.MaterialPurchasID.HasValue && Object2.MaterialPurchasID.HasValue)
            {
                if (Object1.MaterialPurchasID != Object2.MaterialPurchasID)
                {
                    flag = false;
                }
            }
            else if (!Object1.MaterialPurchasID.HasValue ^ !Object2.MaterialPurchasID.HasValue)
            {
                flag = false;
            }
            if ((Object1.TypeStandard != null) && (Object2.TypeStandard != null))
            {
                if (Object1.TypeStandard != Object2.TypeStandard)
                {
                    flag = false;
                }
            }
            else if ((Object1.TypeStandard == null) ^ (Object2.TypeStandard == null))
            {
                flag = false;
            }
            if ((Object1.Unit != null) && (Object2.Unit != null))
            {
                if (Object1.Unit != Object2.Unit)
                {
                    flag = false;
                }
            }
            else if ((Object1.Unit == null) ^ (Object2.Unit == null))
            {
                flag = false;
            }
            if (Object1.Number.HasValue && Object2.Number.HasValue)
            {
                if (Object1.Number != Object2.Number)
                {
                    flag = false;
                }
            }
            else if (!Object1.Number.HasValue ^ !Object2.Number.HasValue)
            {
                flag = false;
            }
            if (Object1.NeedDate.HasValue && Object2.NeedDate.HasValue)
            {
                if (Object1.NeedDate != Object2.NeedDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.NeedDate.HasValue ^ !Object2.NeedDate.HasValue)
            {
                flag = false;
            }
            if (Object1.SignDate.HasValue && Object2.SignDate.HasValue)
            {
                if (Object1.SignDate != Object2.SignDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.SignDate.HasValue ^ !Object2.SignDate.HasValue)
            {
                flag = false;
            }
            if ((Object1.SearchPriceDtl != null) && (Object2.SearchPriceDtl != null))
            {
                if (Object1.SearchPriceDtl != Object2.SearchPriceDtl)
                {
                    flag = false;
                }
            }
            else if ((Object1.SearchPriceDtl == null) ^ (Object2.SearchPriceDtl == null))
            {
                flag = false;
            }
            if (Object1.FinalPrice.HasValue && Object2.FinalPrice.HasValue)
            {
                if (Object1.FinalPrice != Object2.FinalPrice)
                {
                    flag = false;
                }
                return flag;
            }
            if (!Object1.FinalPrice.HasValue ^ !Object2.FinalPrice.HasValue)
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

        public void OnColumnChanged(MaterialPurchasDtlColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(MaterialPurchasDtlColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                MaterialPurchasDtlEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new MaterialPurchasDtlEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(MaterialPurchasDtlColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(MaterialPurchasDtlColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                MaterialPurchasDtlEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new MaterialPurchasDtlEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as MaterialPurchasDtl);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as MaterialPurchasDtlEntityData;
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
                    this.parentCollection.Remove((MaterialPurchasDtl) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{10}{9}- MaterialPurchasDtlID: {0}{9}- MaterialPurchasID: {1}{9}- TypeStandard: {2}{9}- Unit: {3}{9}- Number: {4}{9}- NeedDate: {5}{9}- SignDate: {6}{9}- SearchPriceDtl: {7}{9}- FinalPrice: {8}{9}", new object[] { this.MaterialPurchasDtlID, !this.MaterialPurchasID.HasValue ? string.Empty : this.MaterialPurchasID.ToString(), (this.TypeStandard == null) ? string.Empty : this.TypeStandard.ToString(), (this.Unit == null) ? string.Empty : this.Unit.ToString(), !this.Number.HasValue ? string.Empty : this.Number.ToString(), !this.NeedDate.HasValue ? string.Empty : this.NeedDate.ToString(), !this.SignDate.HasValue ? string.Empty : this.SignDate.ToString(), (this.SearchPriceDtl == null) ? string.Empty : this.SearchPriceDtl.ToString(), !this.FinalPrice.HasValue ? string.Empty : this.FinalPrice.ToString(), Environment.NewLine, base.GetType() });
        }

        [XmlIgnore]
        public MaterialPurchasDtlKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new MaterialPurchasDtlKey(this);
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
                    this.entityTrackingKey = "MaterialPurchasDtl" + this.MaterialPurchasDtlID.ToString();
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

        [Description("最终价格"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? FinalPrice
        {
            get
            {
                return this.entityData.FinalPrice;
            }
            set
            {
                if (this.entityData.FinalPrice != value)
                {
                    this.OnColumnChanging(MaterialPurchasDtlColumn.FinalPrice, this.entityData.FinalPrice);
                    this.entityData.FinalPrice = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialPurchasDtlColumn.FinalPrice, this.entityData.FinalPrice);
                    this.OnPropertyChanged("FinalPrice");
                }
            }
        }

        [ReadOnly(false), Description("明细Code"), DataObjectField(true, true, false), TiannuoPM.Entities.Bindable]
        public virtual int MaterialPurchasDtlID
        {
            get
            {
                return this.entityData.MaterialPurchasDtlID;
            }
            set
            {
                if (this.entityData.MaterialPurchasDtlID != value)
                {
                    this.OnColumnChanging(MaterialPurchasDtlColumn.MaterialPurchasDtlID, this.entityData.MaterialPurchasDtlID);
                    this.entityData.MaterialPurchasDtlID = value;
                    this.EntityId.MaterialPurchasDtlID = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialPurchasDtlColumn.MaterialPurchasDtlID, this.entityData.MaterialPurchasDtlID);
                    this.OnPropertyChanged("MaterialPurchasDtlID");
                }
            }
        }

        [Description("材料主表Code"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual int? MaterialPurchasID
        {
            get
            {
                return this.entityData.MaterialPurchasID;
            }
            set
            {
                if (this.entityData.MaterialPurchasID != value)
                {
                    this.OnColumnChanging(MaterialPurchasDtlColumn.MaterialPurchasID, this.entityData.MaterialPurchasID);
                    this.entityData.MaterialPurchasID = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialPurchasDtlColumn.MaterialPurchasID, this.entityData.MaterialPurchasID);
                    this.OnPropertyChanged("MaterialPurchasID");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Browsable(false), XmlIgnore]
        public virtual MaterialPurchas MaterialPurchasIDSource
        {
            get
            {
                return this._materialPurchasIDSource;
            }
            set
            {
                this._materialPurchasIDSource = value;
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true), Description("需要日期")]
        public virtual DateTime? NeedDate
        {
            get
            {
                return this.entityData.NeedDate;
            }
            set
            {
                if (this.entityData.NeedDate != value)
                {
                    this.OnColumnChanging(MaterialPurchasDtlColumn.NeedDate, this.entityData.NeedDate);
                    this.entityData.NeedDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialPurchasDtlColumn.NeedDate, this.entityData.NeedDate);
                    this.OnPropertyChanged("NeedDate");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true), Description("数量")]
        public virtual decimal? Number
        {
            get
            {
                return this.entityData.Number;
            }
            set
            {
                if (this.entityData.Number != value)
                {
                    this.OnColumnChanging(MaterialPurchasDtlColumn.Number, this.entityData.Number);
                    this.entityData.Number = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialPurchasDtlColumn.Number, this.entityData.Number);
                    this.OnPropertyChanged("Number");
                }
            }
        }

        [XmlIgnore, Browsable(false)]
        public override object ParentCollection
        {
            get
            {
                return this.parentCollection;
            }
            set
            {
                this.parentCollection = value as TList<MaterialPurchasDtl>;
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 0x1388), Description("寻价信息")]
        public virtual string SearchPriceDtl
        {
            get
            {
                return this.entityData.SearchPriceDtl;
            }
            set
            {
                if (this.entityData.SearchPriceDtl != value)
                {
                    this.OnColumnChanging(MaterialPurchasDtlColumn.SearchPriceDtl, this.entityData.SearchPriceDtl);
                    this.entityData.SearchPriceDtl = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialPurchasDtlColumn.SearchPriceDtl, this.entityData.SearchPriceDtl);
                    this.OnPropertyChanged("SearchPriceDtl");
                }
            }
        }

        [DataObjectField(false, false, true), TiannuoPM.Entities.Bindable, Description("希望签约日期")]
        public virtual DateTime? SignDate
        {
            get
            {
                return this.entityData.SignDate;
            }
            set
            {
                if (this.entityData.SignDate != value)
                {
                    this.OnColumnChanging(MaterialPurchasDtlColumn.SignDate, this.entityData.SignDate);
                    this.entityData.SignDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialPurchasDtlColumn.SignDate, this.entityData.SignDate);
                    this.OnPropertyChanged("SignDate");
                }
            }
        }

        [XmlIgnore, SoapIgnore, Browsable(false)]
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

        [Browsable(false), XmlIgnore]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { "MaterialPurchasDtlID", "MaterialPurchasID", "TypeStandard", "Unit", "Number", "NeedDate", "SignDate", "SearchPriceDtl", "FinalPrice" };
            }
        }

        [Browsable(false), XmlIgnore]
        public override string TableName
        {
            get
            {
                return "MaterialPurchasDtl";
            }
        }

        [DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable, Description("规格型号")]
        public virtual string TypeStandard
        {
            get
            {
                return this.entityData.TypeStandard;
            }
            set
            {
                if (this.entityData.TypeStandard != value)
                {
                    this.OnColumnChanging(MaterialPurchasDtlColumn.TypeStandard, this.entityData.TypeStandard);
                    this.entityData.TypeStandard = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialPurchasDtlColumn.TypeStandard, this.entityData.TypeStandard);
                    this.OnPropertyChanged("TypeStandard");
                }
            }
        }

        [DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable, Description("单位")]
        public virtual string Unit
        {
            get
            {
                return this.entityData.Unit;
            }
            set
            {
                if (this.entityData.Unit != value)
                {
                    this.OnColumnChanging(MaterialPurchasDtlColumn.Unit, this.entityData.Unit);
                    this.entityData.Unit = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(MaterialPurchasDtlColumn.Unit, this.entityData.Unit);
                    this.OnPropertyChanged("Unit");
                }
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class MaterialPurchasDtlEntityData : ICloneable
        {
            public decimal? FinalPrice = null;
            public int MaterialPurchasDtlID;
            public int? MaterialPurchasID = null;
            public DateTime? NeedDate = null;
            public decimal? Number = null;
            public string SearchPriceDtl = null;
            public DateTime? SignDate = null;
            public string TypeStandard = null;
            public string Unit = null;

            public object Clone()
            {
                MaterialPurchasDtlBase.MaterialPurchasDtlEntityData data = new MaterialPurchasDtlBase.MaterialPurchasDtlEntityData();
                data.MaterialPurchasDtlID = this.MaterialPurchasDtlID;
                data.MaterialPurchasID = this.MaterialPurchasID;
                data.TypeStandard = this.TypeStandard;
                data.Unit = this.Unit;
                data.Number = this.Number;
                data.NeedDate = this.NeedDate;
                data.SignDate = this.SignDate;
                data.SearchPriceDtl = this.SearchPriceDtl;
                data.FinalPrice = this.FinalPrice;
                return data;
            }
        }
    }
}

