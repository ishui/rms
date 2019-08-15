namespace Rms.WorkFlow
{
    using System;
    using System.Collections;

    public class CaseProperty
    {
        private string _CaseCode;
        private string _ProcedurePropertyCode;
        private string _ProcedurePropertyValue;
        private Hashtable _Propertys = new Hashtable();
        private string _Remak;
        private string _WorkFlowCasePropertyCode;
        private bool m_IsMarkDelete = false;
        private bool m_IsNew = false;

        public void AddNewCaseProperty(CaseProperty CaseCaseProperty)
        {
            this._Propertys.Add(CaseCaseProperty.WorkFlowCasePropertyCode, CaseCaseProperty);
        }

        public CaseProperty GetCaseProperty(string WorkFlowCasePropertyCode)
        {
            return (CaseProperty) this._Propertys[WorkFlowCasePropertyCode];
        }

        public IDictionaryEnumerator GetCasePropertyEnumerator(string ProcedureCode)
        {
            return this._Propertys.GetEnumerator();
        }

        public void RemoveCaseProperty(string WorkFlowCasePropertyCode)
        {
            this._Propertys.Remove(WorkFlowCasePropertyCode);
        }

        public string CaseCode
        {
            get
            {
                return this._CaseCode;
            }
            set
            {
                this._CaseCode = value;
            }
        }

        public bool IsMarkDelete
        {
            get
            {
                return this.m_IsMarkDelete;
            }
            set
            {
                this.m_IsMarkDelete = value;
            }
        }

        public bool IsNew
        {
            get
            {
                return this.m_IsNew;
            }
            set
            {
                this.m_IsNew = value;
            }
        }

        public string ProcedurePropertyCode
        {
            get
            {
                return this._ProcedurePropertyCode;
            }
            set
            {
                this._ProcedurePropertyCode = value;
            }
        }

        public string ProcedurePropertyValue
        {
            get
            {
                return this._ProcedurePropertyValue;
            }
            set
            {
                this._ProcedurePropertyValue = value;
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

        public string WorkFlowCasePropertyCode
        {
            get
            {
                return this._WorkFlowCasePropertyCode;
            }
            set
            {
                this._WorkFlowCasePropertyCode = value;
            }
        }
    }
}

