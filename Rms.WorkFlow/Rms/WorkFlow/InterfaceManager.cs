namespace Rms.WorkFlow
{
    using System;

    public sealed class InterfaceManager
    {
        private static IActorIO m_IAtorIO;
        private static IDefinition m_IDefinition;
        private static ISystemCode m_ISystemCode;
        private static IWorkCase m_IWorkCase;

        private InterfaceManager()
        {
        }

        public static IActorIO iActorIO
        {
            get
            {
                return m_IAtorIO;
            }
            set
            {
                m_IAtorIO = value;
            }
        }

        public static IDefinition iDefinition
        {
            get
            {
                return m_IDefinition;
            }
            set
            {
                m_IDefinition = value;
            }
        }

        public static ISystemCode iSystemCode
        {
            get
            {
                return m_ISystemCode;
            }
            set
            {
                m_ISystemCode = value;
            }
        }

        public static IWorkCase iWorkCase
        {
            get
            {
                return m_IWorkCase;
            }
            set
            {
                m_IWorkCase = value;
            }
        }
    }
}

