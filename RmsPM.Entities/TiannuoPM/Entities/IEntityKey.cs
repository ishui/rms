namespace TiannuoPM.Entities
{
    using System;
    using System.Collections;

    public interface IEntityKey
    {
        void Load(IDictionary values);
        IDictionary ToDictionary();
    }
}

