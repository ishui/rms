namespace RmsReport
{
    using System;
    using System.Data;

    public class ExcelGroup
    {
        public bool AutoSort = true;
        public int BlankGroupCount = 2;
        public int DetailHeight = 2;
        public int FooterHeight = 1;
        public string GroupFieldName;
        public int HeaderHeight = 0;
        public DataTable m_DataSource;

        public ExcelGroup(string AGroupFieldName)
        {
            this.GroupFieldName = AGroupFieldName;
        }

        public object DataSource
        {
            get
            {
                return this.m_DataSource;
            }
            set
            {
                this.m_DataSource = TExcel.TransDataSourceToTable(value);
            }
        }

        public int Height
        {
            get
            {
                return ((this.HeaderHeight + this.DetailHeight) + this.FooterHeight);
            }
        }
    }
}

