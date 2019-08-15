namespace Rms.WorkFlow
{
    using System;
    using System.Collections;

    public class WorkCase
    {
        private Hashtable m_Acts = new Hashtable();
        private string m_ApplicationCode;
        private string m_CaseCode;
        private Hashtable m_CasePropertys = new Hashtable();
        private string m_CreateDate = "";
        private Hashtable m_DataPackages = new Hashtable();
        private string m_FinishDate = "";
        private bool m_IsMarkDelete = false;
        private bool m_IsNew = false;
        private Hashtable m_Opinions = new Hashtable();
        private string m_ProcedureCode;
        private string m_ProjectCode;
        private string m_SourceUnitCode;
        private string m_SourceUserCode;
        private WorkCaseStatus m_Status;
        private string m_Subject;
        private string m_Transactor;
        private string m_TransactUnit;

        public void AddNewAct(Act act)
        {
            this.m_Acts.Add(act.ActCode, act);
        }

        public void AddNewCaseProperty(CaseProperty CasePropertyCase)
        {
            this.m_CasePropertys.Add(CasePropertyCase.WorkFlowCasePropertyCode, CasePropertyCase);
        }

        public void AddNewDataPackage(DataPackage dataPackage)
        {
            this.m_DataPackages.Add(dataPackage.DataPackageCode, dataPackage);
        }

        public void AddNewOpinion(Opinion opinion)
        {
            this.m_Opinions.Add(opinion.OpinionCode, opinion);
        }

        public Act GetAct(string actCode)
        {
            return (Act) this.m_Acts[actCode];
        }

        public IDictionaryEnumerator GetActEnumerator()
        {
            return this.m_Acts.GetEnumerator();
        }

        public CaseProperty GetCaseProperty(string CasePropertyCode)
        {
            return (CaseProperty) this.m_CasePropertys[CasePropertyCode];
        }

        public IDictionaryEnumerator GetCasePropertyEnumerator()
        {
            return this.m_CasePropertys.GetEnumerator();
        }

        public DataPackage GetDataPackage(string dataPackageCode)
        {
            return (DataPackage) this.m_DataPackages[dataPackageCode];
        }

        public IDictionaryEnumerator GetDataPackageEnumerator()
        {
            return this.m_DataPackages.GetEnumerator();
        }

        public Opinion GetOpinion(string opinionCode)
        {
            return (Opinion) this.m_Opinions[opinionCode];
        }

        public IDictionaryEnumerator GetOpinionEnumerator()
        {
            return this.m_Opinions.GetEnumerator();
        }

        public void RemoveAct(string actCode, bool isMarkDelete)
        {
            if (isMarkDelete)
            {
                ((Act) this.m_Acts["actCode"]).IsMarkDelete = true;
            }
            else
            {
                this.m_Acts.Remove(actCode);
            }
        }

        public void RemoveCaseProperty(string CasePropertyCode, bool isMarkDelete)
        {
            if (isMarkDelete)
            {
                ((CaseProperty) this.m_CasePropertys["CasePropertyCode"]).IsMarkDelete = true;
            }
            else
            {
                this.m_CasePropertys.Remove(CasePropertyCode);
            }
        }

        public void RemoveDataPackage(string dataPackageCode, bool isMarkDelete)
        {
            if (isMarkDelete)
            {
                ((DataPackage) this.m_DataPackages["dataPackageCode"]).IsMarkDelete = true;
            }
            else
            {
                this.m_DataPackages.Remove(dataPackageCode);
            }
        }

        public void RemoveOpinion(string opinionCode, bool isMarkDelete)
        {
            if (isMarkDelete)
            {
                ((Opinion) this.m_Opinions["opinionCode"]).IsMarkDelete = true;
            }
            else
            {
                this.m_Opinions.Remove(opinionCode);
            }
        }

        public string ApplicationCode
        {
            get
            {
                return this.m_ApplicationCode;
            }
            set
            {
                this.m_ApplicationCode = value;
            }
        }

        public string CaseCode
        {
            get
            {
                return this.m_CaseCode;
            }
            set
            {
                this.m_CaseCode = value;
            }
        }

        public string CreateDate
        {
            get
            {
                return this.m_CreateDate;
            }
            set
            {
                this.m_CreateDate = value;
            }
        }

        public string FinishDate
        {
            get
            {
                return this.m_FinishDate;
            }
            set
            {
                this.m_FinishDate = value;
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

        public string ProcedureCode
        {
            get
            {
                return this.m_ProcedureCode;
            }
            set
            {
                this.m_ProcedureCode = value;
            }
        }

        public string ProjectCode
        {
            get
            {
                return this.m_ProjectCode;
            }
            set
            {
                this.m_ProjectCode = value;
            }
        }

        public string SourceUnitCode
        {
            get
            {
                return this.m_SourceUnitCode;
            }
            set
            {
                this.m_SourceUnitCode = value;
            }
        }

        public string SourceUserCode
        {
            get
            {
                return this.m_SourceUserCode;
            }
            set
            {
                this.m_SourceUserCode = value;
            }
        }

        public WorkCaseStatus Status
        {
            get
            {
                return this.m_Status;
            }
            set
            {
                this.m_Status = value;
            }
        }

        public string Subject
        {
            get
            {
                return this.m_Subject;
            }
            set
            {
                this.m_Subject = value;
            }
        }

        public string Transactor
        {
            get
            {
                return this.m_Transactor;
            }
            set
            {
                this.m_Transactor = value;
            }
        }

        public string TransactUnit
        {
            get
            {
                return this.m_TransactUnit;
            }
            set
            {
                this.m_TransactUnit = value;
            }
        }
    }
}

