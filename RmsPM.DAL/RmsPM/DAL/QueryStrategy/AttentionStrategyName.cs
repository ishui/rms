namespace RmsPM.DAL.QueryStrategy
{
    using System;

    /// <summary>
    /// 关注检索名称
    /// </summary>
    public enum AttentionStrategyName
    {
        /// <summary>
        /// 项目编号, 单参数， 字符串相等型
        /// </summary>
        ProjectCode,

        /// <summary>
        /// 用户ID
        /// </summary>
        UserCode,

        /// <summary>
        /// 保存的地址
        /// </summary>
        Url,

        /// <summary>
        /// 参数: operationCode, userCode, stationCodes  // 例： 操作编号： 050101 （合同查阅） ； ahu （用户编号）； 100009，100010 岗位串 
        /// </summary>
        AccessRange,

        /// <summary>
        /// 模块
        /// </summary>
        AddModule,

        /// <summary>
        /// 添加的标题

        /// </summary>
        AddTitle,

        /// <summary>
        /// 搜索时间，双参数
        /// </summary>
        AddTime
    }
}

