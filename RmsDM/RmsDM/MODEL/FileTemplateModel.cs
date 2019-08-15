namespace RmsDM.MODEL
{
    using System;

    public class FileTemplateModel
    {
        private int _Code;
        private string _FileTemplateName;
        private int _FileTemplateTypeCode;
        private string _SortCode;

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

        public string FileTemplateName
        {
            get
            {
                return this._FileTemplateName;
            }
            set
            {
                this._FileTemplateName = value;
            }
        }

        public int FileTemplateTypeCode
        {
            get
            {
                return this._FileTemplateTypeCode;
            }
            set
            {
                this._FileTemplateTypeCode = value;
            }
        }

        public string SortCode
        {
            get
            {
                return this._SortCode;
            }
            set
            {
                this._SortCode = value;
            }
        }
    }
}

