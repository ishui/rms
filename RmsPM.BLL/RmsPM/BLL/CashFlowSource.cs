namespace RmsPM.BLL
{
    using System;
    using System.Drawing;

    public class CashFlowSource
    {

        /// <summary>
        /// 曲线颜色
        /// </summary>
        private Color m_Color;
        public Color Color
        {
            get
            {
                return this.m_Color;
            }
        }
       
        /// <summary>
        /// 描述
        /// </summary>
        private string m_Desc = "";
        public string Desc
        {
            get
            {
                return this.m_Desc;
            }
        }

        /// <summary>
        /// 标识
        /// </summary>
        private string m_Id = "";
        public string Id
        {
            get
            {
                return this.m_Id;
            }
        }

        public CashFlowSource(string a_Id, string a_Desc, System.Drawing.Color a_Color)
        {
            this.m_Id = a_Id;
            this.m_Desc = a_Desc;
            this.m_Color = a_Color;
        }
    }
}

