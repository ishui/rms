namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class ProjectComparer : IComparer<Project>
    {
        private ProjectColumn whichComparison;

        public ProjectComparer()
        {
        }

        public ProjectComparer(ProjectColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(Project a, Project b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(Project a, Project b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(Project entity)
        {
            return entity.GetHashCode();
        }

        public ProjectColumn WhichComparison
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

