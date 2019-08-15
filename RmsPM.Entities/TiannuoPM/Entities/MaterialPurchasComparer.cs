namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class MaterialPurchasComparer : IComparer<MaterialPurchas>
    {
        private MaterialPurchasColumn whichComparison;

        public MaterialPurchasComparer()
        {
        }

        public MaterialPurchasComparer(MaterialPurchasColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(MaterialPurchas a, MaterialPurchas b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(MaterialPurchas a, MaterialPurchas b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(MaterialPurchas entity)
        {
            return entity.GetHashCode();
        }

        public MaterialPurchasColumn WhichComparison
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

