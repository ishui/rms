namespace TiannuoPM.Entities
{
    using System;

    public class ProjectEventArgs : EventArgs
    {
        private ProjectColumn column;
        private object value;

        public ProjectEventArgs(ProjectColumn column)
        {
            this.column = column;
        }

        public ProjectEventArgs(ProjectColumn column, object value)
        {
            this.column = column;
            this.value = value;
        }

        public ProjectColumn Column
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

