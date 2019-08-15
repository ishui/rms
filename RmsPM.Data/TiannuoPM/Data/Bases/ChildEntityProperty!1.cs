namespace TiannuoPM.Data.Bases
{
    using System;

    public class ChildEntityProperty<ChildEntityTypesEnum> : IChildEntityProperty
    {
        private ChildEntityTypesEnum name;

        public Enum ChildEntityType
        {
            get
            {
                return (this.Name as Enum);
            }
        }

        public ChildEntityTypesEnum Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
    }
}

