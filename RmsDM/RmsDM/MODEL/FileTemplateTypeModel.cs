namespace RmsDM.MODEL
{
    using System;

    public class FileTemplateTypeModel
    {
        private int _Code;
        private int _Deep;
        private string _FileTemplateTypeName;
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

        public string FileTemplateTypeName
        {
            get
            {
                return this._FileTemplateTypeName;
            }
            set
            {
                this._FileTemplateTypeName = value;
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

