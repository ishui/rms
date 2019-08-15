namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class MaterialPurchasDtlComparer : IComparer<MaterialPurchasDtl>
    {
        private MaterialPurchasDtlColumn whichComparison;

        public MaterialPurchasDtlComparer()
        {
        }

        public MaterialPurchasDtlComparer(MaterialPurchasDtlColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(MaterialPurchasDtl a, MaterialPurchasDtl b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(MaterialPurchasDtl a, MaterialPurchasDtl b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(MaterialPurchasDtl entity)
        {
            return entity.GetHashCode();
        }

        public MaterialPurchasDtlColumn WhichComparison
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

