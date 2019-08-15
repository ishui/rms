namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable]
    public abstract class EntityBaseCore : IEntity, INotifyPropertyChanged, IDataErrorInfo, IDeserializationCallback
    {
        [NonSerialized]
        private TiannuoPM.Entities.Validation.ValidationRules _validationRules;
        protected bool bindingIsNew = true;
        private TiannuoPM.Entities.EntityState currentEntityState = TiannuoPM.Entities.EntityState.Added;
        private bool isEntityTracked = false;
        private bool suppressEntityEvents = false;
        [NonSerialized]
        private object tag;

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected EntityBaseCore()
        {
        }

        public virtual void AcceptChanges()
        {
            this.bindingIsNew = false;
            this.currentEntityState = TiannuoPM.Entities.EntityState.Unchanged;
            this.OnPropertyChanged(string.Empty);
        }

        protected virtual void AddValidationRules()
        {
        }

        public abstract void CancelChanges();
        public virtual void MarkToDelete()
        {
            if (this.currentEntityState != TiannuoPM.Entities.EntityState.Added)
            {
                this.currentEntityState = TiannuoPM.Entities.EntityState.Deleted;
            }
        }

        public void OnDeserialization(object sender)
        {
            if (!this.suppressEntityEvents)
            {
                this.ValidationRules.Target = this;
                this.AddValidationRules();
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (!this.suppressEntityEvents)
            {
                this.ValidationRules.ValidateRules(e.PropertyName);
                if (null != this.PropertyChanged)
                {
                    this.PropertyChanged(this, e);
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (!this.suppressEntityEvents)
            {
                this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }

        public virtual void RemoveDeleteMark()
        {
            if (this.currentEntityState != TiannuoPM.Entities.EntityState.Added)
            {
                this.currentEntityState = TiannuoPM.Entities.EntityState.Changed;
            }
        }

        public void Validate()
        {
            this.ValidationRules.ValidateRules();
        }

        public void Validate(Enum column)
        {
            this.Validate(column.ToString());
        }

        public void Validate(string propertyName)
        {
            this.ValidationRules.ValidateRules(propertyName);
        }

        [XmlIgnore]
        public virtual TiannuoPM.Entities.Validation.BrokenRulesList BrokenRulesList
        {
            get
            {
                return this.ValidationRules.GetBrokenRules();
            }
        }

        [XmlIgnore, Browsable(false)]
        public virtual TiannuoPM.Entities.EntityState EntityState
        {
            get
            {
                return this.currentEntityState;
            }
            set
            {
                this.currentEntityState = value;
            }
        }

        [Browsable(false), XmlIgnore]
        public abstract string EntityTrackingKey { get; set; }

        public string Error
        {
            get
            {
                string text = string.Empty;
                if (!this.IsValid)
                {
                    text = this.ValidationRules.GetBrokenRules().ToString();
                }
                return text;
            }
        }

        [Browsable(false), XmlIgnore]
        public bool IsDeleted
        {
            get
            {
                return (this.currentEntityState == TiannuoPM.Entities.EntityState.Deleted);
            }
        }

        [XmlIgnore, Browsable(false)]
        public bool IsDirty
        {
            get
            {
                return (this.currentEntityState != TiannuoPM.Entities.EntityState.Unchanged);
            }
        }

        [XmlIgnore, Browsable(false), System.ComponentModel.Bindable(false)]
        public bool IsEntityTracked
        {
            get
            {
                return this.isEntityTracked;
            }
            set
            {
                this.isEntityTracked = value;
            }
        }

        [Browsable(false), XmlIgnore]
        public bool IsNew
        {
            get
            {
                return (this.currentEntityState == TiannuoPM.Entities.EntityState.Added);
            }
            set
            {
                this.currentEntityState = TiannuoPM.Entities.EntityState.Added;
            }
        }

        [Browsable(false)]
        public virtual bool IsValid
        {
            get
            {
                return this.ValidationRules.IsValid;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string propertyErrorDescriptions = string.Empty;
                if (!this.IsValid)
                {
                    propertyErrorDescriptions = this.ValidationRules.GetBrokenRules().GetPropertyErrorDescriptions(columnName);
                }
                return propertyErrorDescriptions;
            }
        }

        [XmlIgnore]
        public abstract object ParentCollection { get; set; }

        [System.ComponentModel.Bindable(false), XmlIgnore, Browsable(false)]
        public bool SuppressEntityEvents
        {
            get
            {
                return this.suppressEntityEvents;
            }
            set
            {
                this.suppressEntityEvents = value;
            }
        }

        public abstract string[] TableColumns { get; }

        public abstract string TableName { get; }

        [Description("Object containing data to be associated with this object"), System.ComponentModel.Bindable(false), Localizable(false)]
        public virtual object Tag
        {
            get
            {
                return this.tag;
            }
            set
            {
                if (this.tag != value)
                {
                    this.tag = value;
                }
            }
        }

        [XmlIgnore]
        protected TiannuoPM.Entities.Validation.ValidationRules ValidationRules
        {
            get
            {
                if (this._validationRules == null)
                {
                    this._validationRules = new TiannuoPM.Entities.Validation.ValidationRules(this);
                    this.AddValidationRules();
                }
                return this._validationRules;
            }
        }
    }
}

