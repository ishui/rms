namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;

    public class FileStreamOP
    {
        private int count;
        private FileHelpMethod initModel;
        private char splitChar;

        public FileStreamOP()
        {
        }

        public FileStreamOP(char splitChar, int count)
        {
            this.splitChar = splitChar;
            this.count = count;
        }

        public void SortData(List<string> listValue)
        {
            if ((listValue == null) || (listValue.Count < 2))
            {
                throw new Exception("选择导入的文件没有数据");
            }
            if (listValue[0].Split(new char[] { this.splitChar }).Length != this.count)
            {
                throw new Exception("导入文件的内容与要求的格式不符合");
            }
            this.initModel(listValue);
        }

        public int Count
        {
            get
            {
                return this.count;
            }
            set
            {
                this.count = value;
            }
        }

        public FileHelpMethod InitModel
        {
            get
            {
                return this.initModel;
            }
            set
            {
                this.initModel = value;
            }
        }

        public char SpitChar
        {
            get
            {
                return this.splitChar;
            }
            set
            {
                this.splitChar = value;
            }
        }
    }
}

