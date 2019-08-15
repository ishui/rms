namespace Rms.WorkFlow
{
    using System;

    public class Actor
    {
        private string m_ActorCode;
        private int m_ActorType;

        public string ActorCode
        {
            get
            {
                return this.m_ActorCode;
            }
            set
            {
                this.m_ActorCode = value;
            }
        }

        public int ActorType
        {
            get
            {
                return this.m_ActorType;
            }
            set
            {
                this.m_ActorType = value;
            }
        }
    }
}

