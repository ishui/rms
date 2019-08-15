namespace RmsPM.DAL.QueryStrategy
{
    using System;

    /// <summary>
    /// 枚举
    /// </summary>
    public enum AttrSetFrameItemStrategyName
    {
        /// <summary>
        /// 结构、项 关联表编号, 字符串相等

        /// </summary>
        AttrSetFrameItemCodeEq,

        /// <summary>
        /// 属性定义表结构(列)编号, 字符串相等

        /// </summary>
        AttrSetFrameCodeEq,

        /// <summary>
        /// 属性定义表结构(列)编号, 编号串

        /// </summary>
        AttrSetFrameCodeIn,

        /// <summary>
        /// 属性定义表结构(行)编号, 字符串相等

        /// </summary>
        AttrSetFrameCode2Eq,

        /// <summary>
        /// 属性定义表结构(行)编号, 编号串

        /// </summary>
        AttrSetFrameCode2In,

        /// <summary>
        /// 属性定义项编号, 字符串相等

        /// </summary>
        AttrSetItemCodeEq,

        /// <summary>
        /// 属性定义项编号, 编号串

        /// </summary>
        AttrSetItemCodeIn
    }
}

