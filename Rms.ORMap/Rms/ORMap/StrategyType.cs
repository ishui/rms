namespace Rms.ORMap
{
    using System;

    public enum StrategyType
    {
        /// <summary>
        /// 字符串相等
        /// </summary>
        StringEqual,
        StringEqualEx,

        /// <summary>
        /// 数字相等
        /// </summary>
        IntegerEqual,

        /// <summary>
        /// 日期时间相等
        /// </summary>
        DateTimeEqual,

        /// <summary>
        /// 仅指日期相等
        /// </summary>
        DateTimeEqualOnlyDate,

        /// <summary>
        /// 月份相等
        /// </summary>
        DateTimeEqualMonth,

        /// <summary>
        /// 年份相等
        /// </summary>
        DateTimeEqualYear,
        IntegerRange,
        FloatRange,
        DateTimeRange,
        DateTimeRangeOnlyDate,
        StringRange,
        StringIn,
        StringLike,
        StringLikeEx0,
        StringLikeEx1,
        NumberIn,
        Other
    }
}

