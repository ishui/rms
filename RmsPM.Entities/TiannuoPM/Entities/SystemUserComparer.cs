namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class SystemUserComparer : IComparer<SystemUser>
    {
        private SystemUserColumn whichComparison;

        public SystemUserComparer()
        {
        }

        public SystemUserComparer(SystemUserColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(SystemUser a, SystemUser b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(SystemUser a, SystemUser b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(SystemUser entity)
        {
            return entity.GetHashCode();
        }

        public SystemUserColumn WhichComparison
        {
            get
            {
                return this.whichComparison;
            }
            set
            {
                this.whichComparison = value;
            }
        }
    }
}

