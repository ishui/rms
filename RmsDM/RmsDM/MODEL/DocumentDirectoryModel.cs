namespace RmsDM.MODEL
{
    using System;

    public class DocumentDirectoryModel
    {
        private int _Code;
        private DateTime _CreateDate;
        private int _Deep;
        private string _DepartmentCode;
        private string _DirectoryName;
        private string _DirectoryNodeCode;
        private int _FileTemplateCode;
        private string _FullID;
        private int _OrderByID;
        private int _ParentCode;

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

        public DateTime CreateDate
        {
            get
            {
                return this._CreateDate;
            }
            set
            {
                this._CreateDate = value;
            }
        }

        public int Deep
        {
            get
            {
                return this._Deep;
            }
            set
            {
                this._Deep = value;
            }
        }

        public string DepartmentCode
        {
            get
            {
                return this._DepartmentCode;
            }
            set
            {
                this._DepartmentCode = value;
            }
        }

        public string DirectoryName
        {
            get
            {
                return this._DirectoryName;
            }
            set
            {
                this._DirectoryName = value;
            }
        }

        public string DirectoryNodeCode
        {
            get
            {
                return this._DirectoryNodeCode;
            }
            set
            {
                this._DirectoryNodeCode = value;
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

        public string FullID
        {
            get
            {
                return this._FullID;
            }
            set
            {
                this._FullID = value;
            }
        }

        public int OrderByID
        {
            get
            {
                return this._OrderByID;
            }
            set
            {
                this._OrderByID = value;
            }
        }

        public int ParentCode
        {
            get
            {
                return this._ParentCode;
            }
            set
            {
                this._ParentCode = value;
            }
        }
    }
}

