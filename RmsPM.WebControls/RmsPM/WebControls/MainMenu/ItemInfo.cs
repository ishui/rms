namespace RmsPM.WebControls.MainMenu
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class ItemInfo : ArrayList
    {
        private string g_strCssClass;
        private string g_strEvent;
        private string g_strText;

        public ItemInfo(string m_strText, string m_strEvent, string m_strCssClass)
        {
            this.g_strText = m_strText;
            this.g_strEvent = m_strEvent;
            this.g_strCssClass = m_strCssClass;
        }

        public string CssClass
        {
            get
            {
                return this.g_strCssClass;
            }
            set
            {
                this.g_strCssClass = value;
            }
        }

        public string Event
        {
            get
            {
                return this.g_strEvent;
            }
            set
            {
                this.g_strEvent = value;
            }
        }

        public RmsPM.WebControls.MainMenu.ItemInfo this[int m_iIndex]
        {
            get
            {
                return (RmsPM.WebControls.MainMenu.ItemInfo) base[m_iIndex];
            }
            set
            {
                base[m_iIndex] = value;
            }
        }

        public string Text
        {
            get
            {
                return this.g_strText;
            }
            set
            {
                this.g_strText = value;
            }
        }
    }
}

