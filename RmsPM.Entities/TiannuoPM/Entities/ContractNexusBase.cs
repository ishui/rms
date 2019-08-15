namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, CLSCompliant(true), DataObject]
    public abstract class ContractNexusBase : EntityBase, IEntityId<ContractNexusKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private ContractChange _contractChangeCodeSource;
        private ContractNexusKey _entityId;
        private ISite _site;
        private ContractNexusEntityData backupData;
        private ContractNexusEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<ContractNexus> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event ContractNexusEventHandler ColumnChanged;

        [field: NonSerialized]
        public event ContractNexusEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public ContractNexusBase()
        {
            this.inTxn = false;
            this._contractChangeCodeSource = null;
            this._site = null;
            this.entityData = new ContractNexusEntityData();
            this.backupData = null;
        }

        public ContractNexusBase(string contractNexusContractNexusCode, string contractNexusContractCode, string contractNexusContractChangeCode, string contractNexusCode, string contractNexusType, string contractNexusName, string contractNexusID, string contractNexusPerson, DateTime? contractNexusDate, string contractNexusPath, decimal? contractNexusMoney)
        {
            this.inTxn = false;
            this._contractChangeCodeSource = null;
            this._site = null;
            this.entityData = new ContractNexusEntityData();
            this.backupData = null;
            this.ContractNexusCode = contractNexusContractNexusCode;
            this.ContractCode = contractNexusContractCode;
            this.ContractChangeCode = contractNexusContractChangeCode;
            this.Code = contractNexusCode;
            this.Type = contractNexusType;
            this.Name = contractNexusName;
            this.ID = contractNexusID;
            this.Person = contractNexusPerson;
            this.Date = contractNexusDate;
            this.Path = contractNexusPath;
            this.Money = contractNexusMoney;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.NotNull), "ContractNexusCode");
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractNexusCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ContractChangeCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Code", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Type", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Name", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ID", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Person", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Path", 500));
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

        public virtual ContractNexus Copy()
        {
            ContractNexus nexus = new ContractNexus();
            nexus.ContractNexusCode = this.ContractNexusCode;
            nexus.OriginalContractNexusCode = this.OriginalContractNexusCode;
            nexus.ContractCode = this.ContractCode;
            nexus.ContractChangeCode = this.ContractChangeCode;
            nexus.Code = this.Code;
            nexus.Type = this.Type;
            nexus.Name = this.Name;
            nexus.ID = this.ID;
            nexus.Person = this.Person;
            nexus.Date = this.Date;
            nexus.Path = this.Path;
            nexus.Money = this.Money;
            nexus.AcceptChanges();
            return nexus;
        }

        public static ContractNexus CreateContractNexus(string contractNexusContractNexusCode, string contractNexusContractCode, string contractNexusContractChangeCode, string contractNexusCode, string contractNexusType, string contractNexusName, string contractNexusID, string contractNexusPerson, DateTime? contractNexusDate, string contractNexusPath, decimal? contractNexusMoney)
        {
            ContractNexus nexus = new ContractNexus();
            nexus.ContractNexusCode = contractNexusContractNexusCode;
            nexus.ContractCode = contractNexusContractCode;
            nexus.ContractChangeCode = contractNexusContractChangeCode;
            nexus.Code = contractNexusCode;
            nexus.Type = contractNexusType;
            nexus.Name = contractNexusName;
            nexus.ID = contractNexusID;
            nexus.Person = contractNexusPerson;
            nexus.Date = contractNexusDate;
            nexus.Path = contractNexusPath;
            nexus.Money = contractNexusMoney;
            return nexus;
        }

        public virtual ContractNexus DeepCopy()
        {
            return EntityHelper.Clone<ContractNexus>(this as ContractNexus);
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

        public virtual bool Equals(ContractNexusBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(ContractNexusBase Object1, ContractNexusBase Object2)
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
            if (Object1.ContractNexusCode != Object2.ContractNexusCode)
            {
                flag = false;
            }
            if ((Object1.ContractCode != null) && (Object2.ContractCode != null))
            {
                if (Object1.ContractCode != Object2.ContractCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.ContractCode == null) ^ (Object2.ContractCode == null))
            {
                flag = false;
            }
            if ((Object1.ContractChangeCode != null) && (Object2.ContractChangeCode != null))
            {
                if (Object1.ContractChangeCode != Object2.ContractChangeCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.ContractChangeCode == null) ^ (Object2.ContractChangeCode == null))
            {
                flag = false;
            }
            if ((Object1.Code != null) && (Object2.Code != null))
            {
                if (Object1.Code != Object2.Code)
                {
                    flag = false;
                }
            }
            else if ((Object1.Code == null) ^ (Object2.Code == null))
            {
                flag = false;
            }
            if ((Object1.Type != null) && (Object2.Type != null))
            {
                if (Object1.Type != Object2.Type)
                {
                    flag = false;
                }
            }
            else if ((Object1.Type == null) ^ (Object2.Type == null))
            {
                flag = false;
            }
            if ((Object1.Name != null) && (Object2.Name != null))
            {
                if (Object1.Name != Object2.Name)
                {
                    flag = false;
                }
            }
            else if ((Object1.Name == null) ^ (Object2.Name == null))
            {
                flag = false;
            }
            if ((Object1.ID != null) && (Object2.ID != null))
            {
                if (Object1.ID != Object2.ID)
                {
                    flag = false;
                }
            }
            else if ((Object1.ID == null) ^ (Object2.ID == null))
            {
                flag = false;
            }
            if ((Object1.Person != null) && (Object2.Person != null))
            {
                if (Object1.Person != Object2.Person)
                {
                    flag = false;
                }
            }
            else if ((Object1.Person == null) ^ (Object2.Person == null))
            {
                flag = false;
            }
            if (Object1.Date.HasValue && Object2.Date.HasValue)
            {
                if (Object1.Date != Object2.Date)
                {
                    flag = false;
                }
            }
            else if (!Object1.Date.HasValue ^ !Object2.Date.HasValue)
            {
                flag = false;
            }
            if ((Object1.Path != null) && (Object2.Path != null))
            {
                if (Object1.Path != Object2.Path)
                {
                    flag = false;
                }
            }
            else if ((Object1.Path == null) ^ (Object2.Path == null))
            {
                flag = false;
            }
            if (Object1.Money.HasValue && Object2.Money.HasValue)
            {
                if (Object1.Money != Object2.Money)
                {
                    flag = false;
                }
                return flag;
            }
            if (!Object1.Money.HasValue ^ !Object2.Money.HasValue)
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

        public void OnColumnChanged(ContractNexusColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(ContractNexusColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                ContractNexusEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new ContractNexusEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(ContractNexusColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(ContractNexusColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                ContractNexusEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new ContractNexusEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as ContractNexus);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as ContractNexusEntityData;
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
                    this.parentCollection.Remove((ContractNexus) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{12}{11}- ContractNexusCode: {0}{11}- ContractCode: {1}{11}- ContractChangeCode: {2}{11}- Code: {3}{11}- Type: {4}{11}- Name: {5}{11}- ID: {6}{11}- Person: {7}{11}- Date: {8}{11}- Path: {9}{11}- Money: {10}{11}", new object[] { this.ContractNexusCode, (this.ContractCode == null) ? string.Empty : this.ContractCode.ToString(), (this.ContractChangeCode == null) ? string.Empty : this.ContractChangeCode.ToString(), (this.Code == null) ? string.Empty : this.Code.ToString(), (this.Type == null) ? string.Empty : this.Type.ToString(), (this.Name == null) ? string.Empty : this.Name.ToString(), (this.ID == null) ? string.Empty : this.ID.ToString(), (this.Person == null) ? string.Empty : this.Person.ToString(), !this.Date.HasValue ? string.Empty : this.Date.ToString(), (this.Path == null) ? string.Empty : this.Path.ToString(), !this.Money.HasValue ? string.Empty : this.Money.ToString(), Environment.NewLine, base.GetType() });
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50), Description("编号")]
        public virtual string Code
        {
            get
            {
                return this.entityData.Code;
            }
            set
            {
                if (this.entityData.Code != value)
                {
                    this.OnColumnChanging(ContractNexusColumn.Code, this.entityData.Code);
                    this.entityData.Code = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractNexusColumn.Code, this.entityData.Code);
                    this.OnPropertyChanged("Code");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("合同变更code"), TiannuoPM.Entities.Bindable]
        public virtual string ContractChangeCode
        {
            get
            {
                return this.entityData.ContractChangeCode;
            }
            set
            {
                if (this.entityData.ContractChangeCode != value)
                {
                    this.OnColumnChanging(ContractNexusColumn.ContractChangeCode, this.entityData.ContractChangeCode);
                    this.entityData.ContractChangeCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractNexusColumn.ContractChangeCode, this.entityData.ContractChangeCode);
                    this.OnPropertyChanged("ContractChangeCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, XmlIgnore, Browsable(false)]
        public virtual ContractChange ContractChangeCodeSource
        {
            get
            {
                return this._contractChangeCodeSource;
            }
            set
            {
                this._contractChangeCodeSource = value;
            }
        }

        [Description("合同code"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string ContractCode
        {
            get
            {
                return this.entityData.ContractCode;
            }
            set
            {
                if (this.entityData.ContractCode != value)
                {
                    this.OnColumnChanging(ContractNexusColumn.ContractCode, this.entityData.ContractCode);
                    this.entityData.ContractCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractNexusColumn.ContractCode, this.entityData.ContractCode);
                    this.OnPropertyChanged("ContractCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("合同关系code"), DataObjectField(true, false, false, 50)]
        public virtual string ContractNexusCode
        {
            get
            {
                return this.entityData.ContractNexusCode;
            }
            set
            {
                if (this.entityData.ContractNexusCode != value)
                {
                    this.OnColumnChanging(ContractNexusColumn.ContractNexusCode, this.entityData.ContractNexusCode);
                    this.entityData.ContractNexusCode = value;
                    this.EntityId.ContractNexusCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractNexusColumn.ContractNexusCode, this.entityData.ContractNexusCode);
                    this.OnPropertyChanged("ContractNexusCode");
                }
            }
        }

        [Description("日期"), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual DateTime? Date
        {
            get
            {
                return this.entityData.Date;
            }
            set
            {
                if (this.entityData.Date != value)
                {
                    this.OnColumnChanging(ContractNexusColumn.Date, this.entityData.Date);
                    this.entityData.Date = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractNexusColumn.Date, this.entityData.Date);
                    this.OnPropertyChanged("Date");
                }
            }
        }

        [XmlIgnore]
        public ContractNexusKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new ContractNexusKey(this);
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
                    this.entityTrackingKey = "ContractNexus" + this.ContractNexusCode.ToString();
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

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50), Description("编号")]
        public virtual string ID
        {
            get
            {
                return this.entityData.ID;
            }
            set
            {
                if (this.entityData.ID != value)
                {
                    this.OnColumnChanging(ContractNexusColumn.ID, this.entityData.ID);
                    this.entityData.ID = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractNexusColumn.ID, this.entityData.ID);
                    this.OnPropertyChanged("ID");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description("金额"), DataObjectField(false, false, true)]
        public virtual decimal? Money
        {
            get
            {
                return this.entityData.Money;
            }
            set
            {
                if (this.entityData.Money != value)
                {
                    this.OnColumnChanging(ContractNexusColumn.Money, this.entityData.Money);
                    this.entityData.Money = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractNexusColumn.Money, this.entityData.Money);
                    this.OnPropertyChanged("Money");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description("名称"), TiannuoPM.Entities.Bindable]
        public virtual string Name
        {
            get
            {
                return this.entityData.Name;
            }
            set
            {
                if (this.entityData.Name != value)
                {
                    this.OnColumnChanging(ContractNexusColumn.Name, this.entityData.Name);
                    this.entityData.Name = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractNexusColumn.Name, this.entityData.Name);
                    this.OnPropertyChanged("Name");
                }
            }
        }

        [Browsable(false)]
        public virtual string OriginalContractNexusCode
        {
            get
            {
                return this.entityData.OriginalContractNexusCode;
            }
            set
            {
                this.entityData.OriginalContractNexusCode = value;
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
                this.parentCollection = value as TList<ContractNexus>;
            }
        }

        [Description("路径"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 500)]
        public virtual string Path
        {
            get
            {
                return this.entityData.Path;
            }
            set
            {
                if (this.entityData.Path != value)
                {
                    this.OnColumnChanging(ContractNexusColumn.Path, this.entityData.Path);
                    this.entityData.Path = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractNexusColumn.Path, this.entityData.Path);
                    this.OnPropertyChanged("Path");
                }
            }
        }

        [Description("创建者"), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string Person
        {
            get
            {
                return this.entityData.Person;
            }
            set
            {
                if (this.entityData.Person != value)
                {
                    this.OnColumnChanging(ContractNexusColumn.Person, this.entityData.Person);
                    this.entityData.Person = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractNexusColumn.Person, this.entityData.Person);
                    this.OnPropertyChanged("Person");
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

        [XmlIgnore, Browsable(false)]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { "ContractNexusCode", "ContractCode", "ContractChangeCode", "Code", "Type", "Name", "ID", "Person", "Date", "Path", "Money" };
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string TableName
        {
            get
            {
                return "ContractNexus";
            }
        }

        [TiannuoPM.Entities.Bindable, Description("类型"), DataObjectField(false, false, true, 50)]
        public virtual string Type
        {
            get
            {
                return this.entityData.Type;
            }
            set
            {
                if (this.entityData.Type != value)
                {
                    this.OnColumnChanging(ContractNexusColumn.Type, this.entityData.Type);
                    this.entityData.Type = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ContractNexusColumn.Type, this.entityData.Type);
                    this.OnPropertyChanged("Type");
                }
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class ContractNexusEntityData : ICloneable
        {
            public string Code = null;
            public string ContractChangeCode = null;
            public string ContractCode = null;
            public string ContractNexusCode;
            public DateTime? Date = null;
            public string ID = null;
            public decimal? Money = null;
            public string Name = null;
            public string OriginalContractNexusCode;
            public string Path = null;
            public string Person = null;
            public string Type = null;

            public object Clone()
            {
                ContractNexusBase.ContractNexusEntityData data = new ContractNexusBase.ContractNexusEntityData();
                data.ContractNexusCode = this.ContractNexusCode;
                data.OriginalContractNexusCode = this.OriginalContractNexusCode;
                data.ContractCode = this.ContractCode;
                data.ContractChangeCode = this.ContractChangeCode;
                data.Code = this.Code;
                data.Type = this.Type;
                data.Name = this.Name;
                data.ID = this.ID;
                data.Person = this.Person;
                data.Date = this.Date;
                data.Path = this.Path;
                data.Money = this.Money;
                return data;
            }
        }
    }
}

