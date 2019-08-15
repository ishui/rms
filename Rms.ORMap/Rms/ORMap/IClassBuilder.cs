namespace Rms.ORMap
{
    using System;

    public interface IClassBuilder
    {
        EntityData BuildClass(string className);
        SqlStruct GetSqlStruct(string className, string sqlStringName);
    }
}

