namespace TiannuoPM.Entities
{
    using System;
    using System.Collections.Generic;

    public class InspectSituationComparer : IComparer<InspectSituation>
    {
        private InspectSituationColumn whichComparison;

        public InspectSituationComparer()
        {
        }

        public InspectSituationComparer(InspectSituationColumn column)
        {
            this.whichComparison = column;
        }

        public int Compare(InspectSituation a, InspectSituation b)
        {
            EntityPropertyComparer comparer = new EntityPropertyComparer(this.whichComparison.ToString());
            return comparer.Compare(a, b);
        }

        public bool Equals(InspectSituation a, InspectSituation b)
        {
            return (this.Compare(a, b) == 0);
        }

        public int GetHashCode(InspectSituation entity)
        {
            return entity.GetHashCode();
        }

        public InspectSituationColumn WhichComparison
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

