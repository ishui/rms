namespace TiannuoPM.Entities
{
    using System;
    using System.Data;

    [Serializable]
    public enum ProjectColumn
    {
        [ColumnEnum("AfforestingRate", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("AfforestingRate")]
        AfforestingRate = 8,
        [ColumnEnum("AfforestingSpace", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("AfforestingSpace")]
        AfforestingSpace = 0x24,
        [EnumTextValue("Area"), ColumnEnum("Area", typeof(string), DbType.AnsiString, false, false, true, 50)]
        Area = 4,
        [EnumTextValue("BlockID"), ColumnEnum("BlockID", typeof(string), DbType.AnsiString, false, false, true, 50)]
        BlockID = 6,
        [EnumTextValue("BlockName"), ColumnEnum("BlockName", typeof(string), DbType.AnsiString, false, false, true, 50)]
        BlockName = 5,
        [ColumnEnum("BsBuildingSpace", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("BsBuildingSpace")]
        BsBuildingSpace = 0x2e,
        [ColumnEnum("BuildingDensity", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("BuildingDensity")]
        BuildingDensity = 12,
        [ColumnEnum("BuildingSpaceForVolumeRate", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("BuildingSpaceForVolumeRate")]
        BuildingSpaceForVolumeRate = 13,
        [ColumnEnum("BuildingSpaceNotVolumeRate", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("BuildingSpaceNotVolumeRate")]
        BuildingSpaceNotVolumeRate = 14,
        [ColumnEnum("BuildSpace", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("BuildSpace")]
        BuildSpace = 7,
        [ColumnEnum("CenterAfforestingRate", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("CenterAfforestingRate")]
        CenterAfforestingRate = 0x26,
        [ColumnEnum("CenterAfforestingSpace", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("CenterAfforestingSpace")]
        CenterAfforestingSpace = 0x25,
        [ColumnEnum("City", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("City")]
        City = 3,
        [ColumnEnum("DevelopUnit", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("DevelopUnit")]
        DevelopUnit = 0x1b,
        [ColumnEnum("DevelopUnitAddress", typeof(string), DbType.AnsiString, false, false, true, 200), EnumTextValue("DevelopUnitAddress")]
        DevelopUnitAddress = 0x31,
        [ColumnEnum("HouseBuildingSpace", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("HouseBuildingSpace")]
        HouseBuildingSpace = 0x2d,
        [EnumTextValue("HouseCount"), ColumnEnum("HouseCount", typeof(decimal), DbType.Decimal, false, false, true)]
        HouseCount = 0x29,
        [EnumTextValue("HouseUse"), ColumnEnum("HouseUse", typeof(string), DbType.AnsiString, false, false, true, 50)]
        HouseUse = 0x2a,
        [ColumnEnum("ImagePath", typeof(string), DbType.AnsiString, false, false, true, 200), EnumTextValue("ImagePath")]
        ImagePath = 0x16,
        [ColumnEnum("IsEstimate", typeof(int), DbType.Int32, false, false, true), EnumTextValue("IsEstimate")]
        IsEstimate = 15,
        [EnumTextValue("IsUseShortName"), ColumnEnum("IsUseShortName", typeof(string), DbType.AnsiString, false, false, true, 100)]
        IsUseShortName = 0x34,
        [ColumnEnum("jd", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("jd")]
        Jd = 0x18,
        [ColumnEnum("JDBM", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("JDBM")]
        JDBM = 0x1a,
        [EnumTextValue("jdxz"), ColumnEnum("jdxz", typeof(string), DbType.AnsiString, false, false, true, 50)]
        Jdxz = 0x19,
        [ColumnEnum("jgDate", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("jgDate")]
        JgDate = 30,
        [ColumnEnum("kgDate", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("kgDate")]
        KgDate = 0x1d,
        [EnumTextValue("Manager"), ColumnEnum("Manager", typeof(string), DbType.AnsiString, false, false, true, 150)]
        Manager = 0x2f,
        [EnumTextValue("ParkingSpace"), ColumnEnum("ParkingSpace", typeof(decimal), DbType.Decimal, false, false, true)]
        ParkingSpace = 0x27,
        [EnumTextValue("peripheryspace"), ColumnEnum("peripheryspace", typeof(decimal), DbType.Decimal, false, false, true)]
        Peripheryspace = 0x33,
        [ColumnEnum("PlanEndDate", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("PlanEndDate")]
        PlanEndDate = 0x20,
        [ColumnEnum("PlannedVolumeRate", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("PlannedVolumeRate")]
        PlannedVolumeRate = 11,
        [ColumnEnum("PlanStartDate", typeof(DateTime), DbType.DateTime, false, false, true), EnumTextValue("PlanStartDate")]
        PlanStartDate = 0x1f,
        [ColumnEnum("ProjectAddress", typeof(string), DbType.AnsiString, false, false, true, 200), EnumTextValue("ProjectAddress")]
        ProjectAddress = 0x15,
        [ColumnEnum("ProjectCode", typeof(string), DbType.AnsiString, true, false, false, 50), EnumTextValue("ProjectCode")]
        ProjectCode = 1,
        [EnumTextValue("ProjectId"), ColumnEnum("ProjectId", typeof(string), DbType.AnsiString, false, false, true, 50)]
        ProjectId = 20,
        [ColumnEnum("ProjectName", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ProjectName")]
        ProjectName = 2,
        [ColumnEnum("ProjectShortName", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("ProjectShortName")]
        ProjectShortName = 0x21,
        [EnumTextValue("PTFeeType"), ColumnEnum("PTFeeType", typeof(string), DbType.AnsiString, false, false, true, 50)]
        PTFeeType = 0x2b,
        [EnumTextValue("PTFeeVoucherID"), ColumnEnum("PTFeeVoucherID", typeof(string), DbType.AnsiString, false, false, true, 50)]
        PTFeeVoucherID = 0x2c,
        [ColumnEnum("Remark", typeof(string), DbType.AnsiString, false, false, true, 800), EnumTextValue("Remark")]
        Remark = 0x10,
        [ColumnEnum("SalProjectCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("SalProjectCode")]
        SalProjectCode = 0x1c,
        [ColumnEnum("Status", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("Status")]
        Status = 0x13,
        [ColumnEnum("SubjectSetCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("SubjectSetCode")]
        SubjectSetCode = 0x12,
        [EnumTextValue("Tj_date"), ColumnEnum("Tj_date", typeof(DateTime), DbType.DateTime, false, false, true)]
        Tj_date = 0x11,
        [ColumnEnum("TotalBuildingSpace", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("TotalBuildingSpace")]
        TotalBuildingSpace = 9,
        [ColumnEnum("TotalFloorSpace", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("TotalFloorSpace")]
        TotalFloorSpace = 10,
        [ColumnEnum("U8Code", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("U8Code")]
        U8Code = 0x30,
        [ColumnEnum("UnderBuildingSpace", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("UnderBuildingSpace")]
        UnderBuildingSpace = 0x22,
        [ColumnEnum("UnderFloorSpace", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("UnderFloorSpace")]
        UnderFloorSpace = 0x23,
        [ColumnEnum("UnderParkingSpace", typeof(decimal), DbType.Decimal, false, false, true), EnumTextValue("UnderParkingSpace")]
        UnderParkingSpace = 40,
        [ColumnEnum("UnitCode", typeof(string), DbType.AnsiString, false, false, true, 50), EnumTextValue("UnitCode")]
        UnitCode = 0x17,
        [EnumTextValue("waterspace"), ColumnEnum("waterspace", typeof(decimal), DbType.Decimal, false, false, true)]
        Waterspace = 50
    }
}

