﻿namespace TiannuoPM.Entities
{
    using System;

    public class InspectSituationEventArgs : EventArgs
    {
        private InspectSituationColumn column;
        private object value;

        public InspectSituationEventArgs(InspectSituationColumn column)
        {
            this.column = column;
        }

        public InspectSituationEventArgs(InspectSituationColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public InspectSituationColumn Column
        {
            get
            {
                return this.column;
            }
        }

        public object Value
        {
            get
            {
                return this.value;
            }
        }
    }
}

