namespace RmsDM.MODEL
{
    using System;

    public class FileTemplateVersionModel
    {
        private int _Code;
        private int _FileTemplateCode;
        private string _IsAvailability;
        private string _IsPigeonhole;
        private string _MarkingSNRule;
        private string _PigeonholeTime;
        private string _RecordKind;
        private string _SaveTerm;
        private string _VersionNumber;
        private string _WorkFlowProcedureName;

        public int Code
        {
            get
            {
                return this._Code;
            }
            set
            {
                this._Code = value;
            }
        }

        public int FileTemplateCode
        {
            get
            {
                return this._FileTemplateCode;
            }
            set
            {
                this._FileTemplateCode = value;
            }
        }

        public string IsAvailability
        {
            get
            {
                return this._IsAvailability;
            }
            set
            {
                this._IsAvailability = value;
            }
        }

        public string IsPigeonhole
        {
            get
            {
                return this._IsPigeonhole;
            }
            set
            {
                this._IsPigeonhole = value;
            }
        }

        public string MarkingSNRule
        {
            get
            {
                return this._MarkingSNRule;
            }
            set
            {
                this._MarkingSNRule = value;
            }
        }

        public string PigeonholeTime
        {
            get
            {
                return this._PigeonholeTime;
            }
            set
            {
                this._PigeonholeTime = value;
            }
        }

        public string RecordKind
        {
            get
            {
                return this._RecordKind;
            }
            set
            {
                this._RecordKind = value;
            }
        }

        public string SaveTerm
        {
            get
            {
                return this._SaveTerm;
            }
            set
            {
                this._SaveTerm = value;
            }
        }

        public string VersionNumber
        {
            get
            {
                return this._VersionNumber;
            }
            set
            {
                this._VersionNumber = value;
            }
        }

        public string WorkFlowProcedureName
        {
            get
            {
                return this._WorkFlowProcedureName;
            }
            set
            {
                this._WorkFlowProcedureName = value;
            }
        }
    }
}

