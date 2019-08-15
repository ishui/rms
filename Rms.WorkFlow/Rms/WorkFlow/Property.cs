namespace Rms.WorkFlow
{
    using System;
    using System.Collections;

    public class Property
    {
        private string _ProcedureCode;
        private string _ProcedurePropertyName;
        private string _ProcedurePropertyType;
        private Hashtable _Propertys = new Hashtable();
        private string _Remak;
        private string _WorkFlowProcedurePropertyCode;

        public void AddNewProperty(Property PropertyCase)
        {
            this._Propertys.Add(PropertyCase.WorkFlowProcedurePropertyCode, PropertyCase);
        }

        public Property GetProperty(string WorkFlowProcedurePropertyCode)
        {
            return (Property) this._Propertys[WorkFlowProcedurePropertyCode];
        }

        public IDictionaryEnumerator GetPropertyEnumerator(string ProcedureCode)
        {
            return this._Propertys.GetEnumerator();
        }

        public void RemoveProperty(string WorkFlowProcedurePropertyCode)
        {
            this._Propertys.Remove(WorkFlowProcedurePropertyCode);
        }

        public string ProcedureCode
        {
            get
            {
                return this._ProcedureCode;
            }
            set
            {
                this._ProcedureCode = value;
            }
        }

        public string ProcedurePropertyName
        {
            get
            {
                return this._ProcedurePropertyName;
            }
            set
            {
                this._ProcedurePropertyName = value;
            }
        }

        public string ProcedurePropertyType
        {
            get
            {
                return this._ProcedurePropertyType;
            }
            set
            {
                this._ProcedurePropertyType = value;
            }
        }

        public string Remak
        {
            get
            {
                return this._Remak;
            }
            set
            {
                this._Remak = value;
            }
        }

        public string WorkFlowProcedurePropertyCode
        {
            get
            {
                return this._WorkFlowProcedurePropertyCode;
            }
            set
            {
                this._WorkFlowProcedurePropertyCode = value;
            }
        }
    }
}

