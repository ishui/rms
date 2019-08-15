namespace RmsOA.BFL
{
    using System;

    public static class YDictionaryExpand
    {
        public static string[] AssetArea()
        {
            return new string[] { "", "北京", "上海" };
        }

        public static string[] AssetBuyType()
        {
            return new string[] { "", "统购", "独购" };
        }

        public static string[] AssetEquiType()
        {
            return new string[] { "", "电子计算机设备", "网络设备", "存储设备", "输入输出设备", "打印复印设备", "电话语音设备", "计算机软件", "办公家具", "办公用品", "交通工具", "其他" };
        }

        public static string[] AssetFailureReason()
        {
            return new string[] { "", "硬件故障", "软件故障", "系统设置安装", "权限配置", "数据备份", "其他" };
        }

        public static string[] AssetSortType()
        {
            return new string[] { "", "固定资产", "低值易耗品" };
        }

        public static string[] AssetStoreStatus()
        {
            return new string[] { "", "已领用", "库存", "报废", "维修申请中", "维修处理中" };
        }
    }
}

