namespace RmsPM.DAL.QueryStrategy
{
    using System;

    public enum UserStrategyName
    {
        /// <summary>
        /// 用户编号，字符串相等
        /// </summary>
        UserCode,

        /// <summary>
        /// 用户编号串
        /// </summary>
        UserCodes,

        /// <summary>
        /// 用户和岗位字符串一起
        /// </summary>
        UsersAndStations,

        /// <summary>
        /// 登录名
        /// </summary>
        UserID,

        /// <summary>
        /// 用户名，字符串相等
        /// </summary>
        UserName,

        /// <summary>
        /// 密码，字符串相等
        /// </summary>
        PassWord,

        /// <summary>
        /// ？？名称
        /// </summary>
        OwnName,

        /// <summary>
        /// 状态，是否禁用，整形相等，0 正常，1 禁用
        /// </summary>
        Status,

        /// <summary>
        /// 岗位
        /// </summary>
        StationCode,

        /// <summary>
        /// 岗位串
        /// </summary>
        StationCodes,

        /// <summary>
        /// 未定岗
        /// </summary>
        NoStation,

        /// <summary>
        /// 角色编号
        /// </summary>
        RoleCode,

        /// <summary>
        /// 加入该项目的用户，检查该项目中的角色中所有的人
        /// </summary>
        ProjectCode,

        /// <summary>
        /// 单位, 该单位以及下属所有单位
        /// </summary>
        UnitCodeEx,

        /// <summary>
        /// 单位, 该单位下的直属员工
        /// </summary>
        UnitCode,

        /// <summary>
        /// 性别
        /// </summary>
        Sex,

        /// <summary>
        /// 工作项
        /// </summary>
        WBSCode,

        /// <summary>
        /// 工号
        /// </summary>
        SortID,

        /// <summary>
        /// 登录名或用户名匹配
        /// </summary>
        UserIdorUserName,

        /// <summary>
        /// 取回密码
        /// </summary>
        GetPassWord,

        /// <summary>
        /// 用户简称
        /// </summary>
        ShortUserName
    }
}

