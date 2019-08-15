namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, CLSCompliant(true), DataObject]
    public abstract class SystemUserBase : EntityBase, IEntityId<SystemUserKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private SystemUserKey _entityId;
        private ISite _site;
        private SystemUserEntityData backupData;
        private SystemUserEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<SystemUser> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event SystemUserEventHandler ColumnChanged;

        [field: NonSerialized]
        public event SystemUserEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public SystemUserBase()
        {
            this.inTxn = false;
            this._site = null;
            this.entityData = new SystemUserEntityData();
            this.backupData = null;
        }

        public SystemUserBase(string systemUserUserCode, string systemUserUserID, string systemUserUserName, string systemUserOwnName, string systemUserPassWord, string systemUserSex, string systemUserPhone, string systemUserMailBox, string systemUserNote, DateTime? systemUserBirthDay, string systemUserPhoneHome, string systemUserAddress, string systemUserMobile, string systemUserFax, int? systemUserStatus, string systemUserLastProjectCode, string systemUserSortID, string systemUserShortUserName)
        {
            this.inTxn = false;
            this._site = null;
            this.entityData = new SystemUserEntityData();
            this.backupData = null;
            this.UserCode = systemUserUserCode;
            this.UserID = systemUserUserID;
            this.UserName = systemUserUserName;
            this.OwnName = systemUserOwnName;
            this.PassWord = systemUserPassWord;
            this.Sex = systemUserSex;
            this.Phone = systemUserPhone;
            this.MailBox = systemUserMailBox;
            this.Note = systemUserNote;
            this.BirthDay = systemUserBirthDay;
            this.PhoneHome = systemUserPhoneHome;
            this.Address = systemUserAddress;
            this.Mobile = systemUserMobile;
            this.Fax = systemUserFax;
            this.Status = systemUserStatus;
            this.LastProjectCode = systemUserLastProjectCode;
            this.SortID = systemUserSortID;
            this.ShortUserName = systemUserShortUserName;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.NotNull), "UserCode");
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("UserCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("UserID", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("UserName", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("OwnName", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PassWord", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Sex", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Phone", 0x10));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("MailBox", 0x7d0));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Note", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PhoneHome", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Address", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Mobile", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Fax", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("LastProjectCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("SortID", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ShortUserName", 200));
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

        public virtual SystemUser Copy()
        {
            SystemUser user = new SystemUser();
            user.UserCode = this.UserCode;
            user.OriginalUserCode = this.OriginalUserCode;
            user.UserID = this.UserID;
            user.UserName = this.UserName;
            user.OwnName = this.OwnName;
            user.PassWord = this.PassWord;
            user.Sex = this.Sex;
            user.Phone = this.Phone;
            user.MailBox = this.MailBox;
            user.Note = this.Note;
            user.BirthDay = this.BirthDay;
            user.PhoneHome = this.PhoneHome;
            user.Address = this.Address;
            user.Mobile = this.Mobile;
            user.Fax = this.Fax;
            user.Status = this.Status;
            user.LastProjectCode = this.LastProjectCode;
            user.SortID = this.SortID;
            user.ShortUserName = this.ShortUserName;
            user.AcceptChanges();
            return user;
        }

        public static SystemUser CreateSystemUser(string systemUserUserCode, string systemUserUserID, string systemUserUserName, string systemUserOwnName, string systemUserPassWord, string systemUserSex, string systemUserPhone, string systemUserMailBox, string systemUserNote, DateTime? systemUserBirthDay, string systemUserPhoneHome, string systemUserAddress, string systemUserMobile, string systemUserFax, int? systemUserStatus, string systemUserLastProjectCode, string systemUserSortID, string systemUserShortUserName)
        {
            SystemUser user = new SystemUser();
            user.UserCode = systemUserUserCode;
            user.UserID = systemUserUserID;
            user.UserName = systemUserUserName;
            user.OwnName = systemUserOwnName;
            user.PassWord = systemUserPassWord;
            user.Sex = systemUserSex;
            user.Phone = systemUserPhone;
            user.MailBox = systemUserMailBox;
            user.Note = systemUserNote;
            user.BirthDay = systemUserBirthDay;
            user.PhoneHome = systemUserPhoneHome;
            user.Address = systemUserAddress;
            user.Mobile = systemUserMobile;
            user.Fax = systemUserFax;
            user.Status = systemUserStatus;
            user.LastProjectCode = systemUserLastProjectCode;
            user.SortID = systemUserSortID;
            user.ShortUserName = systemUserShortUserName;
            return user;
        }

        public virtual SystemUser DeepCopy()
        {
            return EntityHelper.Clone<SystemUser>(this as SystemUser);
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

        public virtual bool Equals(SystemUserBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(SystemUserBase Object1, SystemUserBase Object2)
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
            if (Object1.UserCode != Object2.UserCode)
            {
                flag = false;
            }
            if ((Object1.UserID != null) && (Object2.UserID != null))
            {
                if (Object1.UserID != Object2.UserID)
                {
                    flag = false;
                }
            }
            else if ((Object1.UserID == null) ^ (Object2.UserID == null))
            {
                flag = false;
            }
            if ((Object1.UserName != null) && (Object2.UserName != null))
            {
                if (Object1.UserName != Object2.UserName)
                {
                    flag = false;
                }
            }
            else if ((Object1.UserName == null) ^ (Object2.UserName == null))
            {
                flag = false;
            }
            if ((Object1.OwnName != null) && (Object2.OwnName != null))
            {
                if (Object1.OwnName != Object2.OwnName)
                {
                    flag = false;
                }
            }
            else if ((Object1.OwnName == null) ^ (Object2.OwnName == null))
            {
                flag = false;
            }
            if ((Object1.PassWord != null) && (Object2.PassWord != null))
            {
                if (Object1.PassWord != Object2.PassWord)
                {
                    flag = false;
                }
            }
            else if ((Object1.PassWord == null) ^ (Object2.PassWord == null))
            {
                flag = false;
            }
            if ((Object1.Sex != null) && (Object2.Sex != null))
            {
                if (Object1.Sex != Object2.Sex)
                {
                    flag = false;
                }
            }
            else if ((Object1.Sex == null) ^ (Object2.Sex == null))
            {
                flag = false;
            }
            if ((Object1.Phone != null) && (Object2.Phone != null))
            {
                if (Object1.Phone != Object2.Phone)
                {
                    flag = false;
                }
            }
            else if ((Object1.Phone == null) ^ (Object2.Phone == null))
            {
                flag = false;
            }
            if ((Object1.MailBox != null) && (Object2.MailBox != null))
            {
                if (Object1.MailBox != Object2.MailBox)
                {
                    flag = false;
                }
            }
            else if ((Object1.MailBox == null) ^ (Object2.MailBox == null))
            {
                flag = false;
            }
            if ((Object1.Note != null) && (Object2.Note != null))
            {
                if (Object1.Note != Object2.Note)
                {
                    flag = false;
                }
            }
            else if ((Object1.Note == null) ^ (Object2.Note == null))
            {
                flag = false;
            }
            if (Object1.BirthDay.HasValue && Object2.BirthDay.HasValue)
            {
                if (Object1.BirthDay != Object2.BirthDay)
                {
                    flag = false;
                }
            }
            else if (!Object1.BirthDay.HasValue ^ !Object2.BirthDay.HasValue)
            {
                flag = false;
            }
            if ((Object1.PhoneHome != null) && (Object2.PhoneHome != null))
            {
                if (Object1.PhoneHome != Object2.PhoneHome)
                {
                    flag = false;
                }
            }
            else if ((Object1.PhoneHome == null) ^ (Object2.PhoneHome == null))
            {
                flag = false;
            }
            if ((Object1.Address != null) && (Object2.Address != null))
            {
                if (Object1.Address != Object2.Address)
                {
                    flag = false;
                }
            }
            else if ((Object1.Address == null) ^ (Object2.Address == null))
            {
                flag = false;
            }
            if ((Object1.Mobile != null) && (Object2.Mobile != null))
            {
                if (Object1.Mobile != Object2.Mobile)
                {
                    flag = false;
                }
            }
            else if ((Object1.Mobile == null) ^ (Object2.Mobile == null))
            {
                flag = false;
            }
            if ((Object1.Fax != null) && (Object2.Fax != null))
            {
                if (Object1.Fax != Object2.Fax)
                {
                    flag = false;
                }
            }
            else if ((Object1.Fax == null) ^ (Object2.Fax == null))
            {
                flag = false;
            }
            if (Object1.Status.HasValue && Object2.Status.HasValue)
            {
                if (Object1.Status != Object2.Status)
                {
                    flag = false;
                }
            }
            else if (!Object1.Status.HasValue ^ !Object2.Status.HasValue)
            {
                flag = false;
            }
            if ((Object1.LastProjectCode != null) && (Object2.LastProjectCode != null))
            {
                if (Object1.LastProjectCode != Object2.LastProjectCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.LastProjectCode == null) ^ (Object2.LastProjectCode == null))
            {
                flag = false;
            }
            if ((Object1.SortID != null) && (Object2.SortID != null))
            {
                if (Object1.SortID != Object2.SortID)
                {
                    flag = false;
                }
            }
            else if ((Object1.SortID == null) ^ (Object2.SortID == null))
            {
                flag = false;
            }
            if ((Object1.ShortUserName != null) && (Object2.ShortUserName != null))
            {
                if (Object1.ShortUserName != Object2.ShortUserName)
                {
                    flag = false;
                }
                return flag;
            }
            if ((Object1.ShortUserName == null) ^ (Object2.ShortUserName == null))
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

        public void OnColumnChanged(SystemUserColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(SystemUserColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                SystemUserEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new SystemUserEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(SystemUserColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(SystemUserColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                SystemUserEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new SystemUserEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as SystemUser);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as SystemUserEntityData;
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
                    this.parentCollection.Remove((SystemUser) this);
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
            return string.Format(CultureInfo.InvariantCulture, "{19}{18}- UserCode: {0}{18}- UserID: {1}{18}- UserName: {2}{18}- OwnName: {3}{18}- PassWord: {4}{18}- Sex: {5}{18}- Phone: {6}{18}- MailBox: {7}{18}- Note: {8}{18}- BirthDay: {9}{18}- PhoneHome: {10}{18}- Address: {11}{18}- Mobile: {12}{18}- Fax: {13}{18}- Status: {14}{18}- LastProjectCode: {15}{18}- SortID: {16}{18}- ShortUserName: {17}{18}", new object[] { 
                this.UserCode, (this.UserID == null) ? string.Empty : this.UserID.ToString(), (this.UserName == null) ? string.Empty : this.UserName.ToString(), (this.OwnName == null) ? string.Empty : this.OwnName.ToString(), (this.PassWord == null) ? string.Empty : this.PassWord.ToString(), (this.Sex == null) ? string.Empty : this.Sex.ToString(), (this.Phone == null) ? string.Empty : this.Phone.ToString(), (this.MailBox == null) ? string.Empty : this.MailBox.ToString(), (this.Note == null) ? string.Empty : this.Note.ToString(), !this.BirthDay.HasValue ? string.Empty : this.BirthDay.ToString(), (this.PhoneHome == null) ? string.Empty : this.PhoneHome.ToString(), (this.Address == null) ? string.Empty : this.Address.ToString(), (this.Mobile == null) ? string.Empty : this.Mobile.ToString(), (this.Fax == null) ? string.Empty : this.Fax.ToString(), !this.Status.HasValue ? string.Empty : this.Status.ToString(), (this.LastProjectCode == null) ? string.Empty : this.LastProjectCode.ToString(), 
                (this.SortID == null) ? string.Empty : this.SortID.ToString(), (this.ShortUserName == null) ? string.Empty : this.ShortUserName.ToString(), Environment.NewLine, base.GetType()
             });
        }

        [TiannuoPM.Entities.Bindable, Description(""), DataObjectField(false, false, true, 50)]
        public virtual string Address
        {
            get
            {
                return this.entityData.Address;
            }
            set
            {
                if (this.entityData.Address != value)
                {
                    this.OnColumnChanging(SystemUserColumn.Address, this.entityData.Address);
                    this.entityData.Address = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(SystemUserColumn.Address, this.entityData.Address);
                    this.OnPropertyChanged("Address");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description(""), DataObjectField(false, false, true)]
        public virtual DateTime? BirthDay
        {
            get
            {
                return this.entityData.BirthDay;
            }
            set
            {
                if (this.entityData.BirthDay != value)
                {
                    this.OnColumnChanging(SystemUserColumn.BirthDay, this.entityData.BirthDay);
                    this.entityData.BirthDay = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(SystemUserColumn.BirthDay, this.entityData.BirthDay);
                    this.OnPropertyChanged("BirthDay");
                }
            }
        }

        [XmlIgnore]
        public SystemUserKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new SystemUserKey(this);
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
                    this.entityTrackingKey = "SystemUser" + this.UserCode.ToString();
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

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50), Description("")]
        public virtual string Fax
        {
            get
            {
                return this.entityData.Fax;
            }
            set
            {
                if (this.entityData.Fax != value)
                {
                    this.OnColumnChanging(SystemUserColumn.Fax, this.entityData.Fax);
                    this.entityData.Fax = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(SystemUserColumn.Fax, this.entityData.Fax);
                    this.OnPropertyChanged("Fax");
                }
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<InspectSituation> InspectSituationCollection
        {
            get
            {
                return this.entityData.InspectSituationCollection;
            }
            set
            {
                this.entityData.InspectSituationCollection = value;
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50), Description("")]
        public virtual string LastProjectCode
        {
            get
            {
                return this.entityData.LastProjectCode;
            }
            set
            {
                if (this.entityData.LastProjectCode != value)
                {
                    this.OnColumnChanging(SystemUserColumn.LastProjectCode, this.entityData.LastProjectCode);
                    this.entityData.LastProjectCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(SystemUserColumn.LastProjectCode, this.entityData.LastProjectCode);
                    this.OnPropertyChanged("LastProjectCode");
                }
            }
        }

        [Description(""), DataObjectField(false, false, true, 0x7d0), TiannuoPM.Entities.Bindable]
        public virtual string MailBox
        {
            get
            {
                return this.entityData.MailBox;
            }
            set
            {
                if (this.entityData.MailBox != value)
                {
                    this.OnColumnChanging(SystemUserColumn.MailBox, this.entityData.MailBox);
                    this.entityData.MailBox = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(SystemUserColumn.MailBox, this.entityData.MailBox);
                    this.OnPropertyChanged("MailBox");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50), Description("")]
        public virtual string Mobile
        {
            get
            {
                return this.entityData.Mobile;
            }
            set
            {
                if (this.entityData.Mobile != value)
                {
                    this.OnColumnChanging(SystemUserColumn.Mobile, this.entityData.Mobile);
                    this.entityData.Mobile = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(SystemUserColumn.Mobile, this.entityData.Mobile);
                    this.OnPropertyChanged("Mobile");
                }
            }
        }

        [Description(""), DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable]
        public virtual string Note
        {
            get
            {
                return this.entityData.Note;
            }
            set
            {
                if (this.entityData.Note != value)
                {
                    this.OnColumnChanging(SystemUserColumn.Note, this.entityData.Note);
                    this.entityData.Note = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(SystemUserColumn.Note, this.entityData.Note);
                    this.OnPropertyChanged("Note");
                }
            }
        }

        [Browsable(false)]
        public virtual string OriginalUserCode
        {
            get
            {
                return this.entityData.OriginalUserCode;
            }
            set
            {
                this.entityData.OriginalUserCode = value;
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string OwnName
        {
            get
            {
                return this.entityData.OwnName;
            }
            set
            {
                if (this.entityData.OwnName != value)
                {
                    this.OnColumnChanging(SystemUserColumn.OwnName, this.entityData.OwnName);
                    this.entityData.OwnName = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(SystemUserColumn.OwnName, this.entityData.OwnName);
                    this.OnPropertyChanged("OwnName");
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
                this.parentCollection = value as TList<SystemUser>;
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50), Description("")]
        public virtual string PassWord
        {
            get
            {
                return this.entityData.PassWord;
            }
            set
            {
                if (this.entityData.PassWord != value)
                {
                    this.OnColumnChanging(SystemUserColumn.PassWord, this.entityData.PassWord);
                    this.entityData.PassWord = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(SystemUserColumn.PassWord, this.entityData.PassWord);
                    this.OnPropertyChanged("PassWord");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 0x10), Description("")]
        public virtual string Phone
        {
            get
            {
                return this.entityData.Phone;
            }
            set
            {
                if (this.entityData.Phone != value)
                {
                    this.OnColumnChanging(SystemUserColumn.Phone, this.entityData.Phone);
                    this.entityData.Phone = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(SystemUserColumn.Phone, this.entityData.Phone);
                    this.OnPropertyChanged("Phone");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description(""), TiannuoPM.Entities.Bindable]
        public virtual string PhoneHome
        {
            get
            {
                return this.entityData.PhoneHome;
            }
            set
            {
                if (this.entityData.PhoneHome != value)
                {
                    this.OnColumnChanging(SystemUserColumn.PhoneHome, this.entityData.PhoneHome);
                    this.entityData.PhoneHome = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(SystemUserColumn.PhoneHome, this.entityData.PhoneHome);
                    this.OnPropertyChanged("PhoneHome");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string Sex
        {
            get
            {
                return this.entityData.Sex;
            }
            set
            {
                if (this.entityData.Sex != value)
                {
                    this.OnColumnChanging(SystemUserColumn.Sex, this.entityData.Sex);
                    this.entityData.Sex = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(SystemUserColumn.Sex, this.entityData.Sex);
                    this.OnPropertyChanged("Sex");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description(""), DataObjectField(false, false, true, 200)]
        public virtual string ShortUserName
        {
            get
            {
                return this.entityData.ShortUserName;
            }
            set
            {
                if (this.entityData.ShortUserName != value)
                {
                    this.OnColumnChanging(SystemUserColumn.ShortUserName, this.entityData.ShortUserName);
                    this.entityData.ShortUserName = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(SystemUserColumn.ShortUserName, this.entityData.ShortUserName);
                    this.OnPropertyChanged("ShortUserName");
                }
            }
        }

        [XmlIgnore, Browsable(false), SoapIgnore]
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

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string SortID
        {
            get
            {
                return this.entityData.SortID;
            }
            set
            {
                if (this.entityData.SortID != value)
                {
                    this.OnColumnChanging(SystemUserColumn.SortID, this.entityData.SortID);
                    this.entityData.SortID = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(SystemUserColumn.SortID, this.entityData.SortID);
                    this.OnPropertyChanged("SortID");
                }
            }
        }

        [DataObjectField(false, false, true), Description(""), TiannuoPM.Entities.Bindable]
        public virtual int? Status
        {
            get
            {
                return this.entityData.Status;
            }
            set
            {
                if (this.entityData.Status != value)
                {
                    this.OnColumnChanging(SystemUserColumn.Status, this.entityData.Status);
                    this.entityData.Status = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(SystemUserColumn.Status, this.entityData.Status);
                    this.OnPropertyChanged("Status");
                }
            }
        }

        [Browsable(false), XmlIgnore]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { 
                    "UserCode", "UserID", "UserName", "OwnName", "PassWord", "Sex", "Phone", "MailBox", "Note", "BirthDay", "PhoneHome", "Address", "Mobile", "Fax", "Status", "LastProjectCode", 
                    "SortID", "ShortUserName"
                 };
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string TableName
        {
            get
            {
                return "SystemUser";
            }
        }

        [DataObjectField(true, false, false, 50), TiannuoPM.Entities.Bindable, Description("")]
        public virtual string UserCode
        {
            get
            {
                return this.entityData.UserCode;
            }
            set
            {
                if (this.entityData.UserCode != value)
                {
                    this.OnColumnChanging(SystemUserColumn.UserCode, this.entityData.UserCode);
                    this.entityData.UserCode = value;
                    this.EntityId.UserCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(SystemUserColumn.UserCode, this.entityData.UserCode);
                    this.OnPropertyChanged("UserCode");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description(""), DataObjectField(false, false, true, 50)]
        public virtual string UserID
        {
            get
            {
                return this.entityData.UserID;
            }
            set
            {
                if (this.entityData.UserID != value)
                {
                    this.OnColumnChanging(SystemUserColumn.UserID, this.entityData.UserID);
                    this.entityData.UserID = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(SystemUserColumn.UserID, this.entityData.UserID);
                    this.OnPropertyChanged("UserID");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50), Description("")]
        public virtual string UserName
        {
            get
            {
                return this.entityData.UserName;
            }
            set
            {
                if (this.entityData.UserName != value)
                {
                    this.OnColumnChanging(SystemUserColumn.UserName, this.entityData.UserName);
                    this.entityData.UserName = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(SystemUserColumn.UserName, this.entityData.UserName);
                    this.OnPropertyChanged("UserName");
                }
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class SystemUserEntityData : ICloneable
        {
            public string Address = null;
            public DateTime? BirthDay = null;
            public string Fax = null;
            private TList<InspectSituation> inspectSituationInspectUser;
            public string LastProjectCode = null;
            public string MailBox = null;
            public string Mobile = null;
            public string Note = null;
            public string OriginalUserCode;
            public string OwnName = null;
            public string PassWord = null;
            public string Phone = null;
            public string PhoneHome = null;
            public string Sex = null;
            public string ShortUserName = null;
            public string SortID = null;
            public int? Status = null;
            public string UserCode;
            public string UserID = null;
            public string UserName = null;

            public object Clone()
            {
                SystemUserBase.SystemUserEntityData data = new SystemUserBase.SystemUserEntityData();
                data.UserCode = this.UserCode;
                data.OriginalUserCode = this.OriginalUserCode;
                data.UserID = this.UserID;
                data.UserName = this.UserName;
                data.OwnName = this.OwnName;
                data.PassWord = this.PassWord;
                data.Sex = this.Sex;
                data.Phone = this.Phone;
                data.MailBox = this.MailBox;
                data.Note = this.Note;
                data.BirthDay = this.BirthDay;
                data.PhoneHome = this.PhoneHome;
                data.Address = this.Address;
                data.Mobile = this.Mobile;
                data.Fax = this.Fax;
                data.Status = this.Status;
                data.LastProjectCode = this.LastProjectCode;
                data.SortID = this.SortID;
                data.ShortUserName = this.ShortUserName;
                return data;
            }

            public TList<InspectSituation> InspectSituationCollection
            {
                get
                {
                    if (this.inspectSituationInspectUser == null)
                    {
                        this.inspectSituationInspectUser = new TList<InspectSituation>();
                    }
                    return this.inspectSituationInspectUser;
                }
                set
                {
                    this.inspectSituationInspectUser = value;
                }
            }
        }
    }
}

