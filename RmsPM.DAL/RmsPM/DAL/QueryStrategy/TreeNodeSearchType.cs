namespace RmsPM.DAL.QueryStrategy
{
    using System;

    public enum TreeNodeSearchType
    {
        AllSubNodeIncludeSelf,
        AllSubNodeNotIncludeSelf,
        FirstChildNode,
        AllSubLeafNode,
        AllSubNotLeafNode,
        OnlySelfNode
    }
}

