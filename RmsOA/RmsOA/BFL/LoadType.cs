namespace RmsOA.BFL
{
    using System;

    public enum LoadType
    {
        [EnumFiledDescribe("插入")]
        Insert = 1,
        [EnumFiledDescribe("搜索")]
        Search = 2,
        [EnumFiledDescribe("更新")]
        UpDate = 0
    }
}

