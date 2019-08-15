namespace Rms.ORMap
{
    using System;

    public class QueryOrder
    {
        private string m_Name;
        private bool m_Sort;

        public QueryOrder()
        {
            this.m_Name = "";
            this.m_Sort = true;
        }

        public QueryOrder(string name, bool sort)
        {
            this.m_Name = "";
            this.m_Sort = true;
            this.m_Name = name;
            this.m_Sort = sort;
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
            }
        }

        public bool Sort
        {
            get
            {
                return this.m_Sort;
            }
            set
            {
                this.m_Sort = value;
            }
        }
    }
}

