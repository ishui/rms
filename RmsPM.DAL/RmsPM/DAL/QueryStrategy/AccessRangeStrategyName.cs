namespace RmsPM.DAL.QueryStrategy
{
    using System;

    /// <summary>
    /// 分发范围检索名称
    /// </summary>
    public enum AccessRangeStrategyName
    {
        /// <summary>
        /// 分发范围编号
        /// </summary>
        AccessRangeCode,

        /// <summary>
        /// 资源编号
        /// </summary>
        ResourceCode,

        /// <summary>
        /// 单位编号
        /// </summary>
        UnitCode,

        /// <summary>
        /// 操作编号
        /// </summary>
        OperationCode,

        OperationCodeLike,
        OperationCodeIn,

        /// <summary>
        /// 由AccessRangeType和RelationCode组成，   
        /// AccessRangeType: 0 用户, 1 角色; RelationCode 相应的就是用户编号和角色编号
        /// </summary>
        AccessRelation0,

        /// <summary>
        /// 由AccessRangeType和RelationCode组成，

        /// 第一个参数是用户编号； 第二个参数是岗位串。注意： 用户编号只有一个；  岗位编号由‘，’ 分隔开
        /// </summary>
        AccessRelation1,

        /// <summary>
        /// 系统类别编号
        /// </summary>
        GroupCode,

        /// <summary>
        /// 级别：0或空=全部；1=本人
        /// </summary>
        RoleLevel
    }
}

