namespace RmsPM.WebControls.MainMenu
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class MenuInfo : ArrayList
    {
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
    }
}

