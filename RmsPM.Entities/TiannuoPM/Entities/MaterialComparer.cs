namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class MaterialComparer : IComparer<Material>
    {
        private MaterialColumn whichComparison;

        public MaterialComparer()
        {
        }

        public MaterialComparer(MaterialColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(Material a, Material b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(Material a, Material b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(Material entity)
        {
            return entity.GetHashCode();
        }

        public MaterialColumn WhichComparison
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

