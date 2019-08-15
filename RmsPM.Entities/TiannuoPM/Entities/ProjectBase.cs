namespace TiannuoPM.Entities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using TiannuoPM.Entities.Validation;

    [Serializable, DataObject, CLSCompliant(true)]
    public abstract class ProjectBase : EntityBase, IEntityId<ProjectKey>, IEntity, IComparable, IEditableObject, IComponent, IDisposable, INotifyPropertyChanged
    {
        private ProjectKey _entityId;
        private ISite _site;
        private ProjectEntityData backupData;
        private ProjectEntityData entityData;
        private string entityTrackingKey;
        private bool inTxn;
        [NonSerialized]
        private TList<Project> parentCollection;

        [field: NonSerialized]
        public event CancelAddNewEventHandler CancelAddNew;

        [field: NonSerialized]
        public event ProjectEventHandler ColumnChanged;

        [field: NonSerialized]
        public event ProjectEventHandler ColumnChanging;

        [field: NonSerialized]
        public event EventHandler Disposed;

        public ProjectBase()
        {
            this.inTxn = false;
            this._site = null;
            this.entityData = new ProjectEntityData();
            this.backupData = null;
        }

        public ProjectBase(string projectProjectCode, string projectProjectName, string projectCity, string projectArea, string projectBlockName, string projectBlockID, decimal? projectBuildSpace, decimal? projectAfforestingRate, decimal? projectTotalBuildingSpace, decimal? projectTotalFloorSpace, decimal? projectPlannedVolumeRate, decimal? projectBuildingDensity, decimal? projectBuildingSpaceForVolumeRate, decimal? projectBuildingSpaceNotVolumeRate, int? projectIsEstimate, string projectRemark, DateTime? projectTj_date, string projectSubjectSetCode, string projectStatus, string projectProjectId, string projectProjectAddress, string projectImagePath, string projectUnitCode, string projectJd, string projectJdxz, string projectJDBM, string projectDevelopUnit, string projectSalProjectCode, DateTime? projectKgDate, DateTime? projectJgDate, DateTime? projectPlanStartDate, DateTime? projectPlanEndDate, string projectProjectShortName, decimal? projectUnderBuildingSpace, decimal? projectUnderFloorSpace, decimal? projectAfforestingSpace, decimal? projectCenterAfforestingSpace, decimal? projectCenterAfforestingRate, decimal? projectParkingSpace, decimal? projectUnderParkingSpace, decimal? projectHouseCount, string projectHouseUse, string projectPTFeeType, string projectPTFeeVoucherID, decimal? projectHouseBuildingSpace, decimal? projectBsBuildingSpace, string projectManager, string projectU8Code, string projectDevelopUnitAddress, decimal? projectWaterspace, decimal? projectPeripheryspace, string projectIsUseShortName)
        {
            this.inTxn = false;
            this._site = null;
            this.entityData = new ProjectEntityData();
            this.backupData = null;
            this.ProjectCode = projectProjectCode;
            this.ProjectName = projectProjectName;
            this.City = projectCity;
            this.Area = projectArea;
            this.BlockName = projectBlockName;
            this.BlockID = projectBlockID;
            this.BuildSpace = projectBuildSpace;
            this.AfforestingRate = projectAfforestingRate;
            this.TotalBuildingSpace = projectTotalBuildingSpace;
            this.TotalFloorSpace = projectTotalFloorSpace;
            this.PlannedVolumeRate = projectPlannedVolumeRate;
            this.BuildingDensity = projectBuildingDensity;
            this.BuildingSpaceForVolumeRate = projectBuildingSpaceForVolumeRate;
            this.BuildingSpaceNotVolumeRate = projectBuildingSpaceNotVolumeRate;
            this.IsEstimate = projectIsEstimate;
            this.Remark = projectRemark;
            this.Tj_date = projectTj_date;
            this.SubjectSetCode = projectSubjectSetCode;
            this.Status = projectStatus;
            this.ProjectId = projectProjectId;
            this.ProjectAddress = projectProjectAddress;
            this.ImagePath = projectImagePath;
            this.UnitCode = projectUnitCode;
            this.Jd = projectJd;
            this.Jdxz = projectJdxz;
            this.JDBM = projectJDBM;
            this.DevelopUnit = projectDevelopUnit;
            this.SalProjectCode = projectSalProjectCode;
            this.KgDate = projectKgDate;
            this.JgDate = projectJgDate;
            this.PlanStartDate = projectPlanStartDate;
            this.PlanEndDate = projectPlanEndDate;
            this.ProjectShortName = projectProjectShortName;
            this.UnderBuildingSpace = projectUnderBuildingSpace;
            this.UnderFloorSpace = projectUnderFloorSpace;
            this.AfforestingSpace = projectAfforestingSpace;
            this.CenterAfforestingSpace = projectCenterAfforestingSpace;
            this.CenterAfforestingRate = projectCenterAfforestingRate;
            this.ParkingSpace = projectParkingSpace;
            this.UnderParkingSpace = projectUnderParkingSpace;
            this.HouseCount = projectHouseCount;
            this.HouseUse = projectHouseUse;
            this.PTFeeType = projectPTFeeType;
            this.PTFeeVoucherID = projectPTFeeVoucherID;
            this.HouseBuildingSpace = projectHouseBuildingSpace;
            this.BsBuildingSpace = projectBsBuildingSpace;
            this.Manager = projectManager;
            this.U8Code = projectU8Code;
            this.DevelopUnitAddress = projectDevelopUnitAddress;
            this.Waterspace = projectWaterspace;
            this.Peripheryspace = projectPeripheryspace;
            this.IsUseShortName = projectIsUseShortName;
        }

        protected override void AddValidationRules()
        {
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.NotNull), "ProjectCode");
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ProjectCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ProjectName", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("City", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Area", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("BlockName", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("BlockID", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Remark", 800));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("SubjectSetCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Status", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ProjectId", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ProjectAddress", 200));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ImagePath", 200));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("UnitCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Jd", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Jdxz", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("JDBM", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("DevelopUnit", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("SalProjectCode", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("ProjectShortName", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("HouseUse", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PTFeeType", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("PTFeeVoucherID", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("Manager", 150));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("U8Code", 50));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("DevelopUnitAddress", 200));
            base.ValidationRules.AddRule(new ValidationRuleHandler(CommonRules.StringMaxLength), new CommonRules.MaxLengthRuleArgs("IsUseShortName", 100));
        }

        public override void CancelChanges()
        {
            IEditableObject obj2 = this;
            obj2.CancelEdit();
        }

        public object Clone()
        {
            return this.Copy();
        }

        public virtual int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public virtual Project Copy()
        {
            Project project = new Project();
            project.ProjectCode = this.ProjectCode;
            project.OriginalProjectCode = this.OriginalProjectCode;
            project.ProjectName = this.ProjectName;
            project.City = this.City;
            project.Area = this.Area;
            project.BlockName = this.BlockName;
            project.BlockID = this.BlockID;
            project.BuildSpace = this.BuildSpace;
            project.AfforestingRate = this.AfforestingRate;
            project.TotalBuildingSpace = this.TotalBuildingSpace;
            project.TotalFloorSpace = this.TotalFloorSpace;
            project.PlannedVolumeRate = this.PlannedVolumeRate;
            project.BuildingDensity = this.BuildingDensity;
            project.BuildingSpaceForVolumeRate = this.BuildingSpaceForVolumeRate;
            project.BuildingSpaceNotVolumeRate = this.BuildingSpaceNotVolumeRate;
            project.IsEstimate = this.IsEstimate;
            project.Remark = this.Remark;
            project.Tj_date = this.Tj_date;
            project.SubjectSetCode = this.SubjectSetCode;
            project.Status = this.Status;
            project.ProjectId = this.ProjectId;
            project.ProjectAddress = this.ProjectAddress;
            project.ImagePath = this.ImagePath;
            project.UnitCode = this.UnitCode;
            project.Jd = this.Jd;
            project.Jdxz = this.Jdxz;
            project.JDBM = this.JDBM;
            project.DevelopUnit = this.DevelopUnit;
            project.SalProjectCode = this.SalProjectCode;
            project.KgDate = this.KgDate;
            project.JgDate = this.JgDate;
            project.PlanStartDate = this.PlanStartDate;
            project.PlanEndDate = this.PlanEndDate;
            project.ProjectShortName = this.ProjectShortName;
            project.UnderBuildingSpace = this.UnderBuildingSpace;
            project.UnderFloorSpace = this.UnderFloorSpace;
            project.AfforestingSpace = this.AfforestingSpace;
            project.CenterAfforestingSpace = this.CenterAfforestingSpace;
            project.CenterAfforestingRate = this.CenterAfforestingRate;
            project.ParkingSpace = this.ParkingSpace;
            project.UnderParkingSpace = this.UnderParkingSpace;
            project.HouseCount = this.HouseCount;
            project.HouseUse = this.HouseUse;
            project.PTFeeType = this.PTFeeType;
            project.PTFeeVoucherID = this.PTFeeVoucherID;
            project.HouseBuildingSpace = this.HouseBuildingSpace;
            project.BsBuildingSpace = this.BsBuildingSpace;
            project.Manager = this.Manager;
            project.U8Code = this.U8Code;
            project.DevelopUnitAddress = this.DevelopUnitAddress;
            project.Waterspace = this.Waterspace;
            project.Peripheryspace = this.Peripheryspace;
            project.IsUseShortName = this.IsUseShortName;
            project.AcceptChanges();
            return project;
        }

        public static Project CreateProject(string projectProjectCode, string projectProjectName, string projectCity, string projectArea, string projectBlockName, string projectBlockID, decimal? projectBuildSpace, decimal? projectAfforestingRate, decimal? projectTotalBuildingSpace, decimal? projectTotalFloorSpace, decimal? projectPlannedVolumeRate, decimal? projectBuildingDensity, decimal? projectBuildingSpaceForVolumeRate, decimal? projectBuildingSpaceNotVolumeRate, int? projectIsEstimate, string projectRemark, DateTime? projectTj_date, string projectSubjectSetCode, string projectStatus, string projectProjectId, string projectProjectAddress, string projectImagePath, string projectUnitCode, string projectJd, string projectJdxz, string projectJDBM, string projectDevelopUnit, string projectSalProjectCode, DateTime? projectKgDate, DateTime? projectJgDate, DateTime? projectPlanStartDate, DateTime? projectPlanEndDate, string projectProjectShortName, decimal? projectUnderBuildingSpace, decimal? projectUnderFloorSpace, decimal? projectAfforestingSpace, decimal? projectCenterAfforestingSpace, decimal? projectCenterAfforestingRate, decimal? projectParkingSpace, decimal? projectUnderParkingSpace, decimal? projectHouseCount, string projectHouseUse, string projectPTFeeType, string projectPTFeeVoucherID, decimal? projectHouseBuildingSpace, decimal? projectBsBuildingSpace, string projectManager, string projectU8Code, string projectDevelopUnitAddress, decimal? projectWaterspace, decimal? projectPeripheryspace, string projectIsUseShortName)
        {
            Project project = new Project();
            project.ProjectCode = projectProjectCode;
            project.ProjectName = projectProjectName;
            project.City = projectCity;
            project.Area = projectArea;
            project.BlockName = projectBlockName;
            project.BlockID = projectBlockID;
            project.BuildSpace = projectBuildSpace;
            project.AfforestingRate = projectAfforestingRate;
            project.TotalBuildingSpace = projectTotalBuildingSpace;
            project.TotalFloorSpace = projectTotalFloorSpace;
            project.PlannedVolumeRate = projectPlannedVolumeRate;
            project.BuildingDensity = projectBuildingDensity;
            project.BuildingSpaceForVolumeRate = projectBuildingSpaceForVolumeRate;
            project.BuildingSpaceNotVolumeRate = projectBuildingSpaceNotVolumeRate;
            project.IsEstimate = projectIsEstimate;
            project.Remark = projectRemark;
            project.Tj_date = projectTj_date;
            project.SubjectSetCode = projectSubjectSetCode;
            project.Status = projectStatus;
            project.ProjectId = projectProjectId;
            project.ProjectAddress = projectProjectAddress;
            project.ImagePath = projectImagePath;
            project.UnitCode = projectUnitCode;
            project.Jd = projectJd;
            project.Jdxz = projectJdxz;
            project.JDBM = projectJDBM;
            project.DevelopUnit = projectDevelopUnit;
            project.SalProjectCode = projectSalProjectCode;
            project.KgDate = projectKgDate;
            project.JgDate = projectJgDate;
            project.PlanStartDate = projectPlanStartDate;
            project.PlanEndDate = projectPlanEndDate;
            project.ProjectShortName = projectProjectShortName;
            project.UnderBuildingSpace = projectUnderBuildingSpace;
            project.UnderFloorSpace = projectUnderFloorSpace;
            project.AfforestingSpace = projectAfforestingSpace;
            project.CenterAfforestingSpace = projectCenterAfforestingSpace;
            project.CenterAfforestingRate = projectCenterAfforestingRate;
            project.ParkingSpace = projectParkingSpace;
            project.UnderParkingSpace = projectUnderParkingSpace;
            project.HouseCount = projectHouseCount;
            project.HouseUse = projectHouseUse;
            project.PTFeeType = projectPTFeeType;
            project.PTFeeVoucherID = projectPTFeeVoucherID;
            project.HouseBuildingSpace = projectHouseBuildingSpace;
            project.BsBuildingSpace = projectBsBuildingSpace;
            project.Manager = projectManager;
            project.U8Code = projectU8Code;
            project.DevelopUnitAddress = projectDevelopUnitAddress;
            project.Waterspace = projectWaterspace;
            project.Peripheryspace = projectPeripheryspace;
            project.IsUseShortName = projectIsUseShortName;
            return project;
        }

        public virtual Project DeepCopy()
        {
            return EntityHelper.Clone<Project>(this as Project);
        }

        public void Dispose()
        {
            this.parentCollection = null;
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                EventHandler disposed = this.Disposed;
                if (disposed != null)
                {
                    disposed(this, EventArgs.Empty);
                }
            }
        }

        public virtual bool Equals(ProjectBase toObject)
        {
            if (toObject == null)
            {
                return false;
            }
            return Equals(this, toObject);
        }

        public static bool Equals(ProjectBase Object1, ProjectBase Object2)
        {
            if ((Object1 == null) && (Object2 == null))
            {
                return true;
            }
            if ((Object1 == null) ^ (Object2 == null))
            {
                return false;
            }
            bool flag = true;
            if (Object1.ProjectCode != Object2.ProjectCode)
            {
                flag = false;
            }
            if ((Object1.ProjectName != null) && (Object2.ProjectName != null))
            {
                if (Object1.ProjectName != Object2.ProjectName)
                {
                    flag = false;
                }
            }
            else if ((Object1.ProjectName == null) ^ (Object2.ProjectName == null))
            {
                flag = false;
            }
            if ((Object1.City != null) && (Object2.City != null))
            {
                if (Object1.City != Object2.City)
                {
                    flag = false;
                }
            }
            else if ((Object1.City == null) ^ (Object2.City == null))
            {
                flag = false;
            }
            if ((Object1.Area != null) && (Object2.Area != null))
            {
                if (Object1.Area != Object2.Area)
                {
                    flag = false;
                }
            }
            else if ((Object1.Area == null) ^ (Object2.Area == null))
            {
                flag = false;
            }
            if ((Object1.BlockName != null) && (Object2.BlockName != null))
            {
                if (Object1.BlockName != Object2.BlockName)
                {
                    flag = false;
                }
            }
            else if ((Object1.BlockName == null) ^ (Object2.BlockName == null))
            {
                flag = false;
            }
            if ((Object1.BlockID != null) && (Object2.BlockID != null))
            {
                if (Object1.BlockID != Object2.BlockID)
                {
                    flag = false;
                }
            }
            else if ((Object1.BlockID == null) ^ (Object2.BlockID == null))
            {
                flag = false;
            }
            if (Object1.BuildSpace.HasValue && Object2.BuildSpace.HasValue)
            {
                if (Object1.BuildSpace != Object2.BuildSpace)
                {
                    flag = false;
                }
            }
            else if (!Object1.BuildSpace.HasValue ^ !Object2.BuildSpace.HasValue)
            {
                flag = false;
            }
            if (Object1.AfforestingRate.HasValue && Object2.AfforestingRate.HasValue)
            {
                if (Object1.AfforestingRate != Object2.AfforestingRate)
                {
                    flag = false;
                }
            }
            else if (!Object1.AfforestingRate.HasValue ^ !Object2.AfforestingRate.HasValue)
            {
                flag = false;
            }
            if (Object1.TotalBuildingSpace.HasValue && Object2.TotalBuildingSpace.HasValue)
            {
                if (Object1.TotalBuildingSpace != Object2.TotalBuildingSpace)
                {
                    flag = false;
                }
            }
            else if (!Object1.TotalBuildingSpace.HasValue ^ !Object2.TotalBuildingSpace.HasValue)
            {
                flag = false;
            }
            if (Object1.TotalFloorSpace.HasValue && Object2.TotalFloorSpace.HasValue)
            {
                if (Object1.TotalFloorSpace != Object2.TotalFloorSpace)
                {
                    flag = false;
                }
            }
            else if (!Object1.TotalFloorSpace.HasValue ^ !Object2.TotalFloorSpace.HasValue)
            {
                flag = false;
            }
            if (Object1.PlannedVolumeRate.HasValue && Object2.PlannedVolumeRate.HasValue)
            {
                if (Object1.PlannedVolumeRate != Object2.PlannedVolumeRate)
                {
                    flag = false;
                }
            }
            else if (!Object1.PlannedVolumeRate.HasValue ^ !Object2.PlannedVolumeRate.HasValue)
            {
                flag = false;
            }
            if (Object1.BuildingDensity.HasValue && Object2.BuildingDensity.HasValue)
            {
                if (Object1.BuildingDensity != Object2.BuildingDensity)
                {
                    flag = false;
                }
            }
            else if (!Object1.BuildingDensity.HasValue ^ !Object2.BuildingDensity.HasValue)
            {
                flag = false;
            }
            if (Object1.BuildingSpaceForVolumeRate.HasValue && Object2.BuildingSpaceForVolumeRate.HasValue)
            {
                if (Object1.BuildingSpaceForVolumeRate != Object2.BuildingSpaceForVolumeRate)
                {
                    flag = false;
                }
            }
            else if (!Object1.BuildingSpaceForVolumeRate.HasValue ^ !Object2.BuildingSpaceForVolumeRate.HasValue)
            {
                flag = false;
            }
            if (Object1.BuildingSpaceNotVolumeRate.HasValue && Object2.BuildingSpaceNotVolumeRate.HasValue)
            {
                if (Object1.BuildingSpaceNotVolumeRate != Object2.BuildingSpaceNotVolumeRate)
                {
                    flag = false;
                }
            }
            else if (!Object1.BuildingSpaceNotVolumeRate.HasValue ^ !Object2.BuildingSpaceNotVolumeRate.HasValue)
            {
                flag = false;
            }
            if (Object1.IsEstimate.HasValue && Object2.IsEstimate.HasValue)
            {
                if (Object1.IsEstimate != Object2.IsEstimate)
                {
                    flag = false;
                }
            }
            else if (!Object1.IsEstimate.HasValue ^ !Object2.IsEstimate.HasValue)
            {
                flag = false;
            }
            if ((Object1.Remark != null) && (Object2.Remark != null))
            {
                if (Object1.Remark != Object2.Remark)
                {
                    flag = false;
                }
            }
            else if ((Object1.Remark == null) ^ (Object2.Remark == null))
            {
                flag = false;
            }
            if (Object1.Tj_date.HasValue && Object2.Tj_date.HasValue)
            {
                if (Object1.Tj_date != Object2.Tj_date)
                {
                    flag = false;
                }
            }
            else if (!Object1.Tj_date.HasValue ^ !Object2.Tj_date.HasValue)
            {
                flag = false;
            }
            if ((Object1.SubjectSetCode != null) && (Object2.SubjectSetCode != null))
            {
                if (Object1.SubjectSetCode != Object2.SubjectSetCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.SubjectSetCode == null) ^ (Object2.SubjectSetCode == null))
            {
                flag = false;
            }
            if ((Object1.Status != null) && (Object2.Status != null))
            {
                if (Object1.Status != Object2.Status)
                {
                    flag = false;
                }
            }
            else if ((Object1.Status == null) ^ (Object2.Status == null))
            {
                flag = false;
            }
            if ((Object1.ProjectId != null) && (Object2.ProjectId != null))
            {
                if (Object1.ProjectId != Object2.ProjectId)
                {
                    flag = false;
                }
            }
            else if ((Object1.ProjectId == null) ^ (Object2.ProjectId == null))
            {
                flag = false;
            }
            if ((Object1.ProjectAddress != null) && (Object2.ProjectAddress != null))
            {
                if (Object1.ProjectAddress != Object2.ProjectAddress)
                {
                    flag = false;
                }
            }
            else if ((Object1.ProjectAddress == null) ^ (Object2.ProjectAddress == null))
            {
                flag = false;
            }
            if ((Object1.ImagePath != null) && (Object2.ImagePath != null))
            {
                if (Object1.ImagePath != Object2.ImagePath)
                {
                    flag = false;
                }
            }
            else if ((Object1.ImagePath == null) ^ (Object2.ImagePath == null))
            {
                flag = false;
            }
            if ((Object1.UnitCode != null) && (Object2.UnitCode != null))
            {
                if (Object1.UnitCode != Object2.UnitCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.UnitCode == null) ^ (Object2.UnitCode == null))
            {
                flag = false;
            }
            if ((Object1.Jd != null) && (Object2.Jd != null))
            {
                if (Object1.Jd != Object2.Jd)
                {
                    flag = false;
                }
            }
            else if ((Object1.Jd == null) ^ (Object2.Jd == null))
            {
                flag = false;
            }
            if ((Object1.Jdxz != null) && (Object2.Jdxz != null))
            {
                if (Object1.Jdxz != Object2.Jdxz)
                {
                    flag = false;
                }
            }
            else if ((Object1.Jdxz == null) ^ (Object2.Jdxz == null))
            {
                flag = false;
            }
            if ((Object1.JDBM != null) && (Object2.JDBM != null))
            {
                if (Object1.JDBM != Object2.JDBM)
                {
                    flag = false;
                }
            }
            else if ((Object1.JDBM == null) ^ (Object2.JDBM == null))
            {
                flag = false;
            }
            if ((Object1.DevelopUnit != null) && (Object2.DevelopUnit != null))
            {
                if (Object1.DevelopUnit != Object2.DevelopUnit)
                {
                    flag = false;
                }
            }
            else if ((Object1.DevelopUnit == null) ^ (Object2.DevelopUnit == null))
            {
                flag = false;
            }
            if ((Object1.SalProjectCode != null) && (Object2.SalProjectCode != null))
            {
                if (Object1.SalProjectCode != Object2.SalProjectCode)
                {
                    flag = false;
                }
            }
            else if ((Object1.SalProjectCode == null) ^ (Object2.SalProjectCode == null))
            {
                flag = false;
            }
            if (Object1.KgDate.HasValue && Object2.KgDate.HasValue)
            {
                if (Object1.KgDate != Object2.KgDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.KgDate.HasValue ^ !Object2.KgDate.HasValue)
            {
                flag = false;
            }
            if (Object1.JgDate.HasValue && Object2.JgDate.HasValue)
            {
                if (Object1.JgDate != Object2.JgDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.JgDate.HasValue ^ !Object2.JgDate.HasValue)
            {
                flag = false;
            }
            if (Object1.PlanStartDate.HasValue && Object2.PlanStartDate.HasValue)
            {
                if (Object1.PlanStartDate != Object2.PlanStartDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.PlanStartDate.HasValue ^ !Object2.PlanStartDate.HasValue)
            {
                flag = false;
            }
            if (Object1.PlanEndDate.HasValue && Object2.PlanEndDate.HasValue)
            {
                if (Object1.PlanEndDate != Object2.PlanEndDate)
                {
                    flag = false;
                }
            }
            else if (!Object1.PlanEndDate.HasValue ^ !Object2.PlanEndDate.HasValue)
            {
                flag = false;
            }
            if ((Object1.ProjectShortName != null) && (Object2.ProjectShortName != null))
            {
                if (Object1.ProjectShortName != Object2.ProjectShortName)
                {
                    flag = false;
                }
            }
            else if ((Object1.ProjectShortName == null) ^ (Object2.ProjectShortName == null))
            {
                flag = false;
            }
            if (Object1.UnderBuildingSpace.HasValue && Object2.UnderBuildingSpace.HasValue)
            {
                if (Object1.UnderBuildingSpace != Object2.UnderBuildingSpace)
                {
                    flag = false;
                }
            }
            else if (!Object1.UnderBuildingSpace.HasValue ^ !Object2.UnderBuildingSpace.HasValue)
            {
                flag = false;
            }
            if (Object1.UnderFloorSpace.HasValue && Object2.UnderFloorSpace.HasValue)
            {
                if (Object1.UnderFloorSpace != Object2.UnderFloorSpace)
                {
                    flag = false;
                }
            }
            else if (!Object1.UnderFloorSpace.HasValue ^ !Object2.UnderFloorSpace.HasValue)
            {
                flag = false;
            }
            if (Object1.AfforestingSpace.HasValue && Object2.AfforestingSpace.HasValue)
            {
                if (Object1.AfforestingSpace != Object2.AfforestingSpace)
                {
                    flag = false;
                }
            }
            else if (!Object1.AfforestingSpace.HasValue ^ !Object2.AfforestingSpace.HasValue)
            {
                flag = false;
            }
            if (Object1.CenterAfforestingSpace.HasValue && Object2.CenterAfforestingSpace.HasValue)
            {
                if (Object1.CenterAfforestingSpace != Object2.CenterAfforestingSpace)
                {
                    flag = false;
                }
            }
            else if (!Object1.CenterAfforestingSpace.HasValue ^ !Object2.CenterAfforestingSpace.HasValue)
            {
                flag = false;
            }
            if (Object1.CenterAfforestingRate.HasValue && Object2.CenterAfforestingRate.HasValue)
            {
                if (Object1.CenterAfforestingRate != Object2.CenterAfforestingRate)
                {
                    flag = false;
                }
            }
            else if (!Object1.CenterAfforestingRate.HasValue ^ !Object2.CenterAfforestingRate.HasValue)
            {
                flag = false;
            }
            if (Object1.ParkingSpace.HasValue && Object2.ParkingSpace.HasValue)
            {
                if (Object1.ParkingSpace != Object2.ParkingSpace)
                {
                    flag = false;
                }
            }
            else if (!Object1.ParkingSpace.HasValue ^ !Object2.ParkingSpace.HasValue)
            {
                flag = false;
            }
            if (Object1.UnderParkingSpace.HasValue && Object2.UnderParkingSpace.HasValue)
            {
                if (Object1.UnderParkingSpace != Object2.UnderParkingSpace)
                {
                    flag = false;
                }
            }
            else if (!Object1.UnderParkingSpace.HasValue ^ !Object2.UnderParkingSpace.HasValue)
            {
                flag = false;
            }
            if (Object1.HouseCount.HasValue && Object2.HouseCount.HasValue)
            {
                if (Object1.HouseCount != Object2.HouseCount)
                {
                    flag = false;
                }
            }
            else if (!Object1.HouseCount.HasValue ^ !Object2.HouseCount.HasValue)
            {
                flag = false;
            }
            if ((Object1.HouseUse != null) && (Object2.HouseUse != null))
            {
                if (Object1.HouseUse != Object2.HouseUse)
                {
                    flag = false;
                }
            }
            else if ((Object1.HouseUse == null) ^ (Object2.HouseUse == null))
            {
                flag = false;
            }
            if ((Object1.PTFeeType != null) && (Object2.PTFeeType != null))
            {
                if (Object1.PTFeeType != Object2.PTFeeType)
                {
                    flag = false;
                }
            }
            else if ((Object1.PTFeeType == null) ^ (Object2.PTFeeType == null))
            {
                flag = false;
            }
            if ((Object1.PTFeeVoucherID != null) && (Object2.PTFeeVoucherID != null))
            {
                if (Object1.PTFeeVoucherID != Object2.PTFeeVoucherID)
                {
                    flag = false;
                }
            }
            else if ((Object1.PTFeeVoucherID == null) ^ (Object2.PTFeeVoucherID == null))
            {
                flag = false;
            }
            if (Object1.HouseBuildingSpace.HasValue && Object2.HouseBuildingSpace.HasValue)
            {
                if (Object1.HouseBuildingSpace != Object2.HouseBuildingSpace)
                {
                    flag = false;
                }
            }
            else if (!Object1.HouseBuildingSpace.HasValue ^ !Object2.HouseBuildingSpace.HasValue)
            {
                flag = false;
            }
            if (Object1.BsBuildingSpace.HasValue && Object2.BsBuildingSpace.HasValue)
            {
                if (Object1.BsBuildingSpace != Object2.BsBuildingSpace)
                {
                    flag = false;
                }
            }
            else if (!Object1.BsBuildingSpace.HasValue ^ !Object2.BsBuildingSpace.HasValue)
            {
                flag = false;
            }
            if ((Object1.Manager != null) && (Object2.Manager != null))
            {
                if (Object1.Manager != Object2.Manager)
                {
                    flag = false;
                }
            }
            else if ((Object1.Manager == null) ^ (Object2.Manager == null))
            {
                flag = false;
            }
            if ((Object1.U8Code != null) && (Object2.U8Code != null))
            {
                if (Object1.U8Code != Object2.U8Code)
                {
                    flag = false;
                }
            }
            else if ((Object1.U8Code == null) ^ (Object2.U8Code == null))
            {
                flag = false;
            }
            if ((Object1.DevelopUnitAddress != null) && (Object2.DevelopUnitAddress != null))
            {
                if (Object1.DevelopUnitAddress != Object2.DevelopUnitAddress)
                {
                    flag = false;
                }
            }
            else if ((Object1.DevelopUnitAddress == null) ^ (Object2.DevelopUnitAddress == null))
            {
                flag = false;
            }
            if (Object1.Waterspace.HasValue && Object2.Waterspace.HasValue)
            {
                if (Object1.Waterspace != Object2.Waterspace)
                {
                    flag = false;
                }
            }
            else if (!Object1.Waterspace.HasValue ^ !Object2.Waterspace.HasValue)
            {
                flag = false;
            }
            if (Object1.Peripheryspace.HasValue && Object2.Peripheryspace.HasValue)
            {
                if (Object1.Peripheryspace != Object2.Peripheryspace)
                {
                    flag = false;
                }
            }
            else if (!Object1.Peripheryspace.HasValue ^ !Object2.Peripheryspace.HasValue)
            {
                flag = false;
            }
            if ((Object1.IsUseShortName != null) && (Object2.IsUseShortName != null))
            {
                if (Object1.IsUseShortName != Object2.IsUseShortName)
                {
                    flag = false;
                }
                return flag;
            }
            if ((Object1.IsUseShortName == null) ^ (Object2.IsUseShortName == null))
            {
                flag = false;
            }
            return flag;
        }

        public static object MakeCopyOf(object x)
        {
            if (x is ICloneable)
            {
                return ((ICloneable) x).Clone();
            }
            throw new NotSupportedException("Object Does Not Implement the ICloneable Interface.");
        }

        public void OnCancelAddNew()
        {
            if (!base.SuppressEntityEvents)
            {
                CancelAddNewEventHandler cancelAddNew = this.CancelAddNew;
                if (cancelAddNew != null)
                {
                    cancelAddNew(this, EventArgs.Empty);
                }
            }
        }

        public void OnColumnChanged(ProjectColumn column)
        {
            this.OnColumnChanged(column, null);
        }

        public void OnColumnChanged(ProjectColumn column, object value)
        {
            if (!base.SuppressEntityEvents)
            {
                ProjectEventHandler columnChanged = this.ColumnChanged;
                if (columnChanged != null)
                {
                    columnChanged(this, new ProjectEventArgs(column, value));
                }
                this.OnEntityChanged();
            }
        }

        public void OnColumnChanging(ProjectColumn column)
        {
            this.OnColumnChanging(column, null);
        }

        public void OnColumnChanging(ProjectColumn column, object value)
        {
            if (base.IsEntityTracked && (this.EntityState != EntityState.Added))
            {
                EntityManager.StopTracking(this.EntityTrackingKey);
            }
            if (!base.SuppressEntityEvents)
            {
                ProjectEventHandler columnChanging = this.ColumnChanging;
                if (columnChanging != null)
                {
                    columnChanging(this, new ProjectEventArgs(column, value));
                }
            }
        }

        private void OnEntityChanged()
        {
            if ((!base.SuppressEntityEvents && !this.inTxn) && (this.parentCollection != null))
            {
                this.parentCollection.EntityChanged(this as Project);
            }
        }

        void IEditableObject.BeginEdit()
        {
            if (!this.inTxn)
            {
                this.backupData = this.entityData.Clone() as ProjectEntityData;
                this.inTxn = true;
            }
        }

        void IEditableObject.CancelEdit()
        {
            if (this.inTxn)
            {
                this.entityData = this.backupData;
                this.backupData = null;
                this.inTxn = false;
                if (base.bindingIsNew && (this.parentCollection != null))
                {
                    this.parentCollection.Remove((Project) this);
                }
            }
        }

        void IEditableObject.EndEdit()
        {
            if (this.inTxn)
            {
                this.backupData = null;
                if (base.IsDirty)
                {
                    if (base.bindingIsNew)
                    {
                        this.EntityState = EntityState.Added;
                        base.bindingIsNew = false;
                    }
                    else if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                }
                base.bindingIsNew = false;
                this.inTxn = false;
            }
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{53}{52}- ProjectCode: {0}{52}- ProjectName: {1}{52}- City: {2}{52}- Area: {3}{52}- BlockName: {4}{52}- BlockID: {5}{52}- BuildSpace: {6}{52}- AfforestingRate: {7}{52}- TotalBuildingSpace: {8}{52}- TotalFloorSpace: {9}{52}- PlannedVolumeRate: {10}{52}- BuildingDensity: {11}{52}- BuildingSpaceForVolumeRate: {12}{52}- BuildingSpaceNotVolumeRate: {13}{52}- IsEstimate: {14}{52}- Remark: {15}{52}- Tj_date: {16}{52}- SubjectSetCode: {17}{52}- Status: {18}{52}- ProjectId: {19}{52}- ProjectAddress: {20}{52}- ImagePath: {21}{52}- UnitCode: {22}{52}- Jd: {23}{52}- Jdxz: {24}{52}- JDBM: {25}{52}- DevelopUnit: {26}{52}- SalProjectCode: {27}{52}- KgDate: {28}{52}- JgDate: {29}{52}- PlanStartDate: {30}{52}- PlanEndDate: {31}{52}- ProjectShortName: {32}{52}- UnderBuildingSpace: {33}{52}- UnderFloorSpace: {34}{52}- AfforestingSpace: {35}{52}- CenterAfforestingSpace: {36}{52}- CenterAfforestingRate: {37}{52}- ParkingSpace: {38}{52}- UnderParkingSpace: {39}{52}- HouseCount: {40}{52}- HouseUse: {41}{52}- PTFeeType: {42}{52}- PTFeeVoucherID: {43}{52}- HouseBuildingSpace: {44}{52}- BsBuildingSpace: {45}{52}- Manager: {46}{52}- U8Code: {47}{52}- DevelopUnitAddress: {48}{52}- Waterspace: {49}{52}- Peripheryspace: {50}{52}- IsUseShortName: {51}{52}", new object[] { 
                this.ProjectCode, (this.ProjectName == null) ? string.Empty : this.ProjectName.ToString(), (this.City == null) ? string.Empty : this.City.ToString(), (this.Area == null) ? string.Empty : this.Area.ToString(), (this.BlockName == null) ? string.Empty : this.BlockName.ToString(), (this.BlockID == null) ? string.Empty : this.BlockID.ToString(), !this.BuildSpace.HasValue ? string.Empty : this.BuildSpace.ToString(), !this.AfforestingRate.HasValue ? string.Empty : this.AfforestingRate.ToString(), !this.TotalBuildingSpace.HasValue ? string.Empty : this.TotalBuildingSpace.ToString(), !this.TotalFloorSpace.HasValue ? string.Empty : this.TotalFloorSpace.ToString(), !this.PlannedVolumeRate.HasValue ? string.Empty : this.PlannedVolumeRate.ToString(), !this.BuildingDensity.HasValue ? string.Empty : this.BuildingDensity.ToString(), !this.BuildingSpaceForVolumeRate.HasValue ? string.Empty : this.BuildingSpaceForVolumeRate.ToString(), !this.BuildingSpaceNotVolumeRate.HasValue ? string.Empty : this.BuildingSpaceNotVolumeRate.ToString(), !this.IsEstimate.HasValue ? string.Empty : this.IsEstimate.ToString(), (this.Remark == null) ? string.Empty : this.Remark.ToString(), 
                !this.Tj_date.HasValue ? string.Empty : this.Tj_date.ToString(), (this.SubjectSetCode == null) ? string.Empty : this.SubjectSetCode.ToString(), (this.Status == null) ? string.Empty : this.Status.ToString(), (this.ProjectId == null) ? string.Empty : this.ProjectId.ToString(), (this.ProjectAddress == null) ? string.Empty : this.ProjectAddress.ToString(), (this.ImagePath == null) ? string.Empty : this.ImagePath.ToString(), (this.UnitCode == null) ? string.Empty : this.UnitCode.ToString(), (this.Jd == null) ? string.Empty : this.Jd.ToString(), (this.Jdxz == null) ? string.Empty : this.Jdxz.ToString(), (this.JDBM == null) ? string.Empty : this.JDBM.ToString(), (this.DevelopUnit == null) ? string.Empty : this.DevelopUnit.ToString(), (this.SalProjectCode == null) ? string.Empty : this.SalProjectCode.ToString(), !this.KgDate.HasValue ? string.Empty : this.KgDate.ToString(), !this.JgDate.HasValue ? string.Empty : this.JgDate.ToString(), !this.PlanStartDate.HasValue ? string.Empty : this.PlanStartDate.ToString(), !this.PlanEndDate.HasValue ? string.Empty : this.PlanEndDate.ToString(), 
                (this.ProjectShortName == null) ? string.Empty : this.ProjectShortName.ToString(), !this.UnderBuildingSpace.HasValue ? string.Empty : this.UnderBuildingSpace.ToString(), !this.UnderFloorSpace.HasValue ? string.Empty : this.UnderFloorSpace.ToString(), !this.AfforestingSpace.HasValue ? string.Empty : this.AfforestingSpace.ToString(), !this.CenterAfforestingSpace.HasValue ? string.Empty : this.CenterAfforestingSpace.ToString(), !this.CenterAfforestingRate.HasValue ? string.Empty : this.CenterAfforestingRate.ToString(), !this.ParkingSpace.HasValue ? string.Empty : this.ParkingSpace.ToString(), !this.UnderParkingSpace.HasValue ? string.Empty : this.UnderParkingSpace.ToString(), !this.HouseCount.HasValue ? string.Empty : this.HouseCount.ToString(), (this.HouseUse == null) ? string.Empty : this.HouseUse.ToString(), (this.PTFeeType == null) ? string.Empty : this.PTFeeType.ToString(), (this.PTFeeVoucherID == null) ? string.Empty : this.PTFeeVoucherID.ToString(), !this.HouseBuildingSpace.HasValue ? string.Empty : this.HouseBuildingSpace.ToString(), !this.BsBuildingSpace.HasValue ? string.Empty : this.BsBuildingSpace.ToString(), (this.Manager == null) ? string.Empty : this.Manager.ToString(), (this.U8Code == null) ? string.Empty : this.U8Code.ToString(), 
                (this.DevelopUnitAddress == null) ? string.Empty : this.DevelopUnitAddress.ToString(), !this.Waterspace.HasValue ? string.Empty : this.Waterspace.ToString(), !this.Peripheryspace.HasValue ? string.Empty : this.Peripheryspace.ToString(), (this.IsUseShortName == null) ? string.Empty : this.IsUseShortName.ToString(), Environment.NewLine, base.GetType()
             });
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true), Description("")]
        public virtual decimal? AfforestingRate
        {
            get
            {
                return this.entityData.AfforestingRate;
            }
            set
            {
                if (this.entityData.AfforestingRate != value)
                {
                    this.OnColumnChanging(ProjectColumn.AfforestingRate, this.entityData.AfforestingRate);
                    this.entityData.AfforestingRate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.AfforestingRate, this.entityData.AfforestingRate);
                    this.OnPropertyChanged("AfforestingRate");
                }
            }
        }

        [DataObjectField(false, false, true), TiannuoPM.Entities.Bindable, Description("")]
        public virtual decimal? AfforestingSpace
        {
            get
            {
                return this.entityData.AfforestingSpace;
            }
            set
            {
                if (this.entityData.AfforestingSpace != value)
                {
                    this.OnColumnChanging(ProjectColumn.AfforestingSpace, this.entityData.AfforestingSpace);
                    this.entityData.AfforestingSpace = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.AfforestingSpace, this.entityData.AfforestingSpace);
                    this.OnPropertyChanged("AfforestingSpace");
                }
            }
        }

        [DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable, Description("")]
        public virtual string Area
        {
            get
            {
                return this.entityData.Area;
            }
            set
            {
                if (this.entityData.Area != value)
                {
                    this.OnColumnChanging(ProjectColumn.Area, this.entityData.Area);
                    this.entityData.Area = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.Area, this.entityData.Area);
                    this.OnPropertyChanged("Area");
                }
            }
        }

        [DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable, Description("")]
        public virtual string BlockID
        {
            get
            {
                return this.entityData.BlockID;
            }
            set
            {
                if (this.entityData.BlockID != value)
                {
                    this.OnColumnChanging(ProjectColumn.BlockID, this.entityData.BlockID);
                    this.entityData.BlockID = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.BlockID, this.entityData.BlockID);
                    this.OnPropertyChanged("BlockID");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string BlockName
        {
            get
            {
                return this.entityData.BlockName;
            }
            set
            {
                if (this.entityData.BlockName != value)
                {
                    this.OnColumnChanging(ProjectColumn.BlockName, this.entityData.BlockName);
                    this.entityData.BlockName = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.BlockName, this.entityData.BlockName);
                    this.OnPropertyChanged("BlockName");
                }
            }
        }

        [DataObjectField(false, false, true), Description(""), TiannuoPM.Entities.Bindable]
        public virtual decimal? BsBuildingSpace
        {
            get
            {
                return this.entityData.BsBuildingSpace;
            }
            set
            {
                if (this.entityData.BsBuildingSpace != value)
                {
                    this.OnColumnChanging(ProjectColumn.BsBuildingSpace, this.entityData.BsBuildingSpace);
                    this.entityData.BsBuildingSpace = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.BsBuildingSpace, this.entityData.BsBuildingSpace);
                    this.OnPropertyChanged("BsBuildingSpace");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true), Description("")]
        public virtual decimal? BuildingDensity
        {
            get
            {
                return this.entityData.BuildingDensity;
            }
            set
            {
                if (this.entityData.BuildingDensity != value)
                {
                    this.OnColumnChanging(ProjectColumn.BuildingDensity, this.entityData.BuildingDensity);
                    this.entityData.BuildingDensity = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.BuildingDensity, this.entityData.BuildingDensity);
                    this.OnPropertyChanged("BuildingDensity");
                }
            }
        }

        [DataObjectField(false, false, true), Description(""), TiannuoPM.Entities.Bindable]
        public virtual decimal? BuildingSpaceForVolumeRate
        {
            get
            {
                return this.entityData.BuildingSpaceForVolumeRate;
            }
            set
            {
                if (this.entityData.BuildingSpaceForVolumeRate != value)
                {
                    this.OnColumnChanging(ProjectColumn.BuildingSpaceForVolumeRate, this.entityData.BuildingSpaceForVolumeRate);
                    this.entityData.BuildingSpaceForVolumeRate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.BuildingSpaceForVolumeRate, this.entityData.BuildingSpaceForVolumeRate);
                    this.OnPropertyChanged("BuildingSpaceForVolumeRate");
                }
            }
        }

        [Description(""), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? BuildingSpaceNotVolumeRate
        {
            get
            {
                return this.entityData.BuildingSpaceNotVolumeRate;
            }
            set
            {
                if (this.entityData.BuildingSpaceNotVolumeRate != value)
                {
                    this.OnColumnChanging(ProjectColumn.BuildingSpaceNotVolumeRate, this.entityData.BuildingSpaceNotVolumeRate);
                    this.entityData.BuildingSpaceNotVolumeRate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.BuildingSpaceNotVolumeRate, this.entityData.BuildingSpaceNotVolumeRate);
                    this.OnPropertyChanged("BuildingSpaceNotVolumeRate");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? BuildSpace
        {
            get
            {
                return this.entityData.BuildSpace;
            }
            set
            {
                if (this.entityData.BuildSpace != value)
                {
                    this.OnColumnChanging(ProjectColumn.BuildSpace, this.entityData.BuildSpace);
                    this.entityData.BuildSpace = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.BuildSpace, this.entityData.BuildSpace);
                    this.OnPropertyChanged("BuildSpace");
                }
            }
        }

        [DataObjectField(false, false, true), TiannuoPM.Entities.Bindable, Description("")]
        public virtual decimal? CenterAfforestingRate
        {
            get
            {
                return this.entityData.CenterAfforestingRate;
            }
            set
            {
                if (this.entityData.CenterAfforestingRate != value)
                {
                    this.OnColumnChanging(ProjectColumn.CenterAfforestingRate, this.entityData.CenterAfforestingRate);
                    this.entityData.CenterAfforestingRate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.CenterAfforestingRate, this.entityData.CenterAfforestingRate);
                    this.OnPropertyChanged("CenterAfforestingRate");
                }
            }
        }

        [Description(""), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? CenterAfforestingSpace
        {
            get
            {
                return this.entityData.CenterAfforestingSpace;
            }
            set
            {
                if (this.entityData.CenterAfforestingSpace != value)
                {
                    this.OnColumnChanging(ProjectColumn.CenterAfforestingSpace, this.entityData.CenterAfforestingSpace);
                    this.entityData.CenterAfforestingSpace = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.CenterAfforestingSpace, this.entityData.CenterAfforestingSpace);
                    this.OnPropertyChanged("CenterAfforestingSpace");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string City
        {
            get
            {
                return this.entityData.City;
            }
            set
            {
                if (this.entityData.City != value)
                {
                    this.OnColumnChanging(ProjectColumn.City, this.entityData.City);
                    this.entityData.City = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.City, this.entityData.City);
                    this.OnPropertyChanged("City");
                }
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<Contract> ContractCollection
        {
            get
            {
                return this.entityData.ContractCollection;
            }
            set
            {
                this.entityData.ContractCollection = value;
            }
        }

        [DataObjectField(false, false, true, 50), Description(""), TiannuoPM.Entities.Bindable]
        public virtual string DevelopUnit
        {
            get
            {
                return this.entityData.DevelopUnit;
            }
            set
            {
                if (this.entityData.DevelopUnit != value)
                {
                    this.OnColumnChanging(ProjectColumn.DevelopUnit, this.entityData.DevelopUnit);
                    this.entityData.DevelopUnit = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.DevelopUnit, this.entityData.DevelopUnit);
                    this.OnPropertyChanged("DevelopUnit");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description(""), DataObjectField(false, false, true, 200)]
        public virtual string DevelopUnitAddress
        {
            get
            {
                return this.entityData.DevelopUnitAddress;
            }
            set
            {
                if (this.entityData.DevelopUnitAddress != value)
                {
                    this.OnColumnChanging(ProjectColumn.DevelopUnitAddress, this.entityData.DevelopUnitAddress);
                    this.entityData.DevelopUnitAddress = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.DevelopUnitAddress, this.entityData.DevelopUnitAddress);
                    this.OnPropertyChanged("DevelopUnitAddress");
                }
            }
        }

        [XmlIgnore]
        public ProjectKey EntityId
        {
            get
            {
                if (this._entityId == null)
                {
                    this._entityId = new ProjectKey(this);
                }
                return this._entityId;
            }
            set
            {
                if (value != null)
                {
                    value.Entity = this;
                }
                this._entityId = value;
            }
        }

        [XmlIgnore]
        public override string EntityTrackingKey
        {
            get
            {
                if (this.entityTrackingKey == null)
                {
                    this.entityTrackingKey = "Project" + this.ProjectCode.ToString();
                }
                return this.entityTrackingKey;
            }
            set
            {
                if (value != null)
                {
                    this.entityTrackingKey = value;
                }
            }
        }

        [DataObjectField(false, false, true), Description(""), TiannuoPM.Entities.Bindable]
        public virtual decimal? HouseBuildingSpace
        {
            get
            {
                return this.entityData.HouseBuildingSpace;
            }
            set
            {
                if (this.entityData.HouseBuildingSpace != value)
                {
                    this.OnColumnChanging(ProjectColumn.HouseBuildingSpace, this.entityData.HouseBuildingSpace);
                    this.entityData.HouseBuildingSpace = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.HouseBuildingSpace, this.entityData.HouseBuildingSpace);
                    this.OnPropertyChanged("HouseBuildingSpace");
                }
            }
        }

        [DataObjectField(false, false, true), Description(""), TiannuoPM.Entities.Bindable]
        public virtual decimal? HouseCount
        {
            get
            {
                return this.entityData.HouseCount;
            }
            set
            {
                if (this.entityData.HouseCount != value)
                {
                    this.OnColumnChanging(ProjectColumn.HouseCount, this.entityData.HouseCount);
                    this.entityData.HouseCount = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.HouseCount, this.entityData.HouseCount);
                    this.OnPropertyChanged("HouseCount");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description(""), DataObjectField(false, false, true, 50)]
        public virtual string HouseUse
        {
            get
            {
                return this.entityData.HouseUse;
            }
            set
            {
                if (this.entityData.HouseUse != value)
                {
                    this.OnColumnChanging(ProjectColumn.HouseUse, this.entityData.HouseUse);
                    this.entityData.HouseUse = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.HouseUse, this.entityData.HouseUse);
                    this.OnPropertyChanged("HouseUse");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 200)]
        public virtual string ImagePath
        {
            get
            {
                return this.entityData.ImagePath;
            }
            set
            {
                if (this.entityData.ImagePath != value)
                {
                    this.OnColumnChanging(ProjectColumn.ImagePath, this.entityData.ImagePath);
                    this.entityData.ImagePath = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.ImagePath, this.entityData.ImagePath);
                    this.OnPropertyChanged("ImagePath");
                }
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<InspectSituation> InspectSituationCollection
        {
            get
            {
                return this.entityData.InspectSituationCollection;
            }
            set
            {
                this.entityData.InspectSituationCollection = value;
            }
        }

        [DataObjectField(false, false, true), Description(""), TiannuoPM.Entities.Bindable]
        public virtual int? IsEstimate
        {
            get
            {
                return this.entityData.IsEstimate;
            }
            set
            {
                if (this.entityData.IsEstimate != value)
                {
                    this.OnColumnChanging(ProjectColumn.IsEstimate, this.entityData.IsEstimate);
                    this.entityData.IsEstimate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.IsEstimate, this.entityData.IsEstimate);
                    this.OnPropertyChanged("IsEstimate");
                }
            }
        }

        [DataObjectField(false, false, true, 100), Description(""), TiannuoPM.Entities.Bindable]
        public virtual string IsUseShortName
        {
            get
            {
                return this.entityData.IsUseShortName;
            }
            set
            {
                if (this.entityData.IsUseShortName != value)
                {
                    this.OnColumnChanging(ProjectColumn.IsUseShortName, this.entityData.IsUseShortName);
                    this.entityData.IsUseShortName = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.IsUseShortName, this.entityData.IsUseShortName);
                    this.OnPropertyChanged("IsUseShortName");
                }
            }
        }

        [DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable, Description("")]
        public virtual string Jd
        {
            get
            {
                return this.entityData.Jd;
            }
            set
            {
                if (this.entityData.Jd != value)
                {
                    this.OnColumnChanging(ProjectColumn.Jd, this.entityData.Jd);
                    this.entityData.Jd = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.Jd, this.entityData.Jd);
                    this.OnPropertyChanged("Jd");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description(""), TiannuoPM.Entities.Bindable]
        public virtual string JDBM
        {
            get
            {
                return this.entityData.JDBM;
            }
            set
            {
                if (this.entityData.JDBM != value)
                {
                    this.OnColumnChanging(ProjectColumn.JDBM, this.entityData.JDBM);
                    this.entityData.JDBM = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.JDBM, this.entityData.JDBM);
                    this.OnPropertyChanged("JDBM");
                }
            }
        }

        [DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable, Description("")]
        public virtual string Jdxz
        {
            get
            {
                return this.entityData.Jdxz;
            }
            set
            {
                if (this.entityData.Jdxz != value)
                {
                    this.OnColumnChanging(ProjectColumn.Jdxz, this.entityData.Jdxz);
                    this.entityData.Jdxz = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.Jdxz, this.entityData.Jdxz);
                    this.OnPropertyChanged("Jdxz");
                }
            }
        }

        [DataObjectField(false, false, true), Description(""), TiannuoPM.Entities.Bindable]
        public virtual DateTime? JgDate
        {
            get
            {
                return this.entityData.JgDate;
            }
            set
            {
                if (this.entityData.JgDate != value)
                {
                    this.OnColumnChanging(ProjectColumn.JgDate, this.entityData.JgDate);
                    this.entityData.JgDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.JgDate, this.entityData.JgDate);
                    this.OnPropertyChanged("JgDate");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual DateTime? KgDate
        {
            get
            {
                return this.entityData.KgDate;
            }
            set
            {
                if (this.entityData.KgDate != value)
                {
                    this.OnColumnChanging(ProjectColumn.KgDate, this.entityData.KgDate);
                    this.entityData.KgDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.KgDate, this.entityData.KgDate);
                    this.OnPropertyChanged("KgDate");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description(""), DataObjectField(false, false, true, 150)]
        public virtual string Manager
        {
            get
            {
                return this.entityData.Manager;
            }
            set
            {
                if (this.entityData.Manager != value)
                {
                    this.OnColumnChanging(ProjectColumn.Manager, this.entityData.Manager);
                    this.entityData.Manager = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.Manager, this.entityData.Manager);
                    this.OnPropertyChanged("Manager");
                }
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<MaterialPurchas> MaterialPurchasCollection
        {
            get
            {
                return this.entityData.MaterialPurchasCollection;
            }
            set
            {
                this.entityData.MaterialPurchasCollection = value;
            }
        }

        [Browsable(false)]
        public virtual string OriginalProjectCode
        {
            get
            {
                return this.entityData.OriginalProjectCode;
            }
            set
            {
                this.entityData.OriginalProjectCode = value;
            }
        }

        [Browsable(false), XmlIgnore]
        public override object ParentCollection
        {
            get
            {
                return this.parentCollection;
            }
            set
            {
                this.parentCollection = value as TList<Project>;
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? ParkingSpace
        {
            get
            {
                return this.entityData.ParkingSpace;
            }
            set
            {
                if (this.entityData.ParkingSpace != value)
                {
                    this.OnColumnChanging(ProjectColumn.ParkingSpace, this.entityData.ParkingSpace);
                    this.entityData.ParkingSpace = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.ParkingSpace, this.entityData.ParkingSpace);
                    this.OnPropertyChanged("ParkingSpace");
                }
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<Payment> PaymentCollection
        {
            get
            {
                return this.entityData.PaymentCollection;
            }
            set
            {
                this.entityData.PaymentCollection = value;
            }
        }

        [TiannuoPM.Entities.Bindable]
        public TList<Payout> PayoutCollection
        {
            get
            {
                return this.entityData.PayoutCollection;
            }
            set
            {
                this.entityData.PayoutCollection = value;
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? Peripheryspace
        {
            get
            {
                return this.entityData.Peripheryspace;
            }
            set
            {
                if (this.entityData.Peripheryspace != value)
                {
                    this.OnColumnChanging(ProjectColumn.Peripheryspace, this.entityData.Peripheryspace);
                    this.entityData.Peripheryspace = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.Peripheryspace, this.entityData.Peripheryspace);
                    this.OnPropertyChanged("Peripheryspace");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual DateTime? PlanEndDate
        {
            get
            {
                return this.entityData.PlanEndDate;
            }
            set
            {
                if (this.entityData.PlanEndDate != value)
                {
                    this.OnColumnChanging(ProjectColumn.PlanEndDate, this.entityData.PlanEndDate);
                    this.entityData.PlanEndDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.PlanEndDate, this.entityData.PlanEndDate);
                    this.OnPropertyChanged("PlanEndDate");
                }
            }
        }

        [Description(""), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? PlannedVolumeRate
        {
            get
            {
                return this.entityData.PlannedVolumeRate;
            }
            set
            {
                if (this.entityData.PlannedVolumeRate != value)
                {
                    this.OnColumnChanging(ProjectColumn.PlannedVolumeRate, this.entityData.PlannedVolumeRate);
                    this.entityData.PlannedVolumeRate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.PlannedVolumeRate, this.entityData.PlannedVolumeRate);
                    this.OnPropertyChanged("PlannedVolumeRate");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual DateTime? PlanStartDate
        {
            get
            {
                return this.entityData.PlanStartDate;
            }
            set
            {
                if (this.entityData.PlanStartDate != value)
                {
                    this.OnColumnChanging(ProjectColumn.PlanStartDate, this.entityData.PlanStartDate);
                    this.entityData.PlanStartDate = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.PlanStartDate, this.entityData.PlanStartDate);
                    this.OnPropertyChanged("PlanStartDate");
                }
            }
        }

        [Description(""), DataObjectField(false, false, true, 200), TiannuoPM.Entities.Bindable]
        public virtual string ProjectAddress
        {
            get
            {
                return this.entityData.ProjectAddress;
            }
            set
            {
                if (this.entityData.ProjectAddress != value)
                {
                    this.OnColumnChanging(ProjectColumn.ProjectAddress, this.entityData.ProjectAddress);
                    this.entityData.ProjectAddress = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.ProjectAddress, this.entityData.ProjectAddress);
                    this.OnPropertyChanged("ProjectAddress");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(true, false, false, 50)]
        public virtual string ProjectCode
        {
            get
            {
                return this.entityData.ProjectCode;
            }
            set
            {
                if (this.entityData.ProjectCode != value)
                {
                    this.OnColumnChanging(ProjectColumn.ProjectCode, this.entityData.ProjectCode);
                    this.entityData.ProjectCode = value;
                    this.EntityId.ProjectCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.ProjectCode, this.entityData.ProjectCode);
                    this.OnPropertyChanged("ProjectCode");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string ProjectId
        {
            get
            {
                return this.entityData.ProjectId;
            }
            set
            {
                if (this.entityData.ProjectId != value)
                {
                    this.OnColumnChanging(ProjectColumn.ProjectId, this.entityData.ProjectId);
                    this.entityData.ProjectId = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.ProjectId, this.entityData.ProjectId);
                    this.OnPropertyChanged("ProjectId");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description(""), DataObjectField(false, false, true, 50)]
        public virtual string ProjectName
        {
            get
            {
                return this.entityData.ProjectName;
            }
            set
            {
                if (this.entityData.ProjectName != value)
                {
                    this.OnColumnChanging(ProjectColumn.ProjectName, this.entityData.ProjectName);
                    this.entityData.ProjectName = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.ProjectName, this.entityData.ProjectName);
                    this.OnPropertyChanged("ProjectName");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description(""), TiannuoPM.Entities.Bindable]
        public virtual string ProjectShortName
        {
            get
            {
                return this.entityData.ProjectShortName;
            }
            set
            {
                if (this.entityData.ProjectShortName != value)
                {
                    this.OnColumnChanging(ProjectColumn.ProjectShortName, this.entityData.ProjectShortName);
                    this.entityData.ProjectShortName = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.ProjectShortName, this.entityData.ProjectShortName);
                    this.OnPropertyChanged("ProjectShortName");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description(""), TiannuoPM.Entities.Bindable]
        public virtual string PTFeeType
        {
            get
            {
                return this.entityData.PTFeeType;
            }
            set
            {
                if (this.entityData.PTFeeType != value)
                {
                    this.OnColumnChanging(ProjectColumn.PTFeeType, this.entityData.PTFeeType);
                    this.entityData.PTFeeType = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.PTFeeType, this.entityData.PTFeeType);
                    this.OnPropertyChanged("PTFeeType");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description(""), TiannuoPM.Entities.Bindable]
        public virtual string PTFeeVoucherID
        {
            get
            {
                return this.entityData.PTFeeVoucherID;
            }
            set
            {
                if (this.entityData.PTFeeVoucherID != value)
                {
                    this.OnColumnChanging(ProjectColumn.PTFeeVoucherID, this.entityData.PTFeeVoucherID);
                    this.entityData.PTFeeVoucherID = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.PTFeeVoucherID, this.entityData.PTFeeVoucherID);
                    this.OnPropertyChanged("PTFeeVoucherID");
                }
            }
        }

        [DataObjectField(false, false, true, 800), Description(""), TiannuoPM.Entities.Bindable]
        public virtual string Remark
        {
            get
            {
                return this.entityData.Remark;
            }
            set
            {
                if (this.entityData.Remark != value)
                {
                    this.OnColumnChanging(ProjectColumn.Remark, this.entityData.Remark);
                    this.entityData.Remark = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.Remark, this.entityData.Remark);
                    this.OnPropertyChanged("Remark");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string SalProjectCode
        {
            get
            {
                return this.entityData.SalProjectCode;
            }
            set
            {
                if (this.entityData.SalProjectCode != value)
                {
                    this.OnColumnChanging(ProjectColumn.SalProjectCode, this.entityData.SalProjectCode);
                    this.entityData.SalProjectCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.SalProjectCode, this.entityData.SalProjectCode);
                    this.OnPropertyChanged("SalProjectCode");
                }
            }
        }

        [SoapIgnore, Browsable(false), XmlIgnore]
        public ISite Site
        {
            get
            {
                return this._site;
            }
            set
            {
                this._site = value;
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50)]
        public virtual string Status
        {
            get
            {
                return this.entityData.Status;
            }
            set
            {
                if (this.entityData.Status != value)
                {
                    this.OnColumnChanging(ProjectColumn.Status, this.entityData.Status);
                    this.entityData.Status = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.Status, this.entityData.Status);
                    this.OnPropertyChanged("Status");
                }
            }
        }

        [DataObjectField(false, false, true, 50), TiannuoPM.Entities.Bindable, Description("")]
        public virtual string SubjectSetCode
        {
            get
            {
                return this.entityData.SubjectSetCode;
            }
            set
            {
                if (this.entityData.SubjectSetCode != value)
                {
                    this.OnColumnChanging(ProjectColumn.SubjectSetCode, this.entityData.SubjectSetCode);
                    this.entityData.SubjectSetCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.SubjectSetCode, this.entityData.SubjectSetCode);
                    this.OnPropertyChanged("SubjectSetCode");
                }
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string[] TableColumns
        {
            get
            {
                return new string[] { 
                    "ProjectCode", "ProjectName", "City", "Area", "BlockName", "BlockID", "BuildSpace", "AfforestingRate", "TotalBuildingSpace", "TotalFloorSpace", "PlannedVolumeRate", "BuildingDensity", "BuildingSpaceForVolumeRate", "BuildingSpaceNotVolumeRate", "IsEstimate", "Remark", 
                    "Tj_date", "SubjectSetCode", "Status", "ProjectId", "ProjectAddress", "ImagePath", "UnitCode", "jd", "jdxz", "JDBM", "DevelopUnit", "SalProjectCode", "kgDate", "jgDate", "PlanStartDate", "PlanEndDate", 
                    "ProjectShortName", "UnderBuildingSpace", "UnderFloorSpace", "AfforestingSpace", "CenterAfforestingSpace", "CenterAfforestingRate", "ParkingSpace", "UnderParkingSpace", "HouseCount", "HouseUse", "PTFeeType", "PTFeeVoucherID", "HouseBuildingSpace", "BsBuildingSpace", "Manager", "U8Code", 
                    "DevelopUnitAddress", "waterspace", "peripheryspace", "IsUseShortName"
                 };
            }
        }

        [XmlIgnore, Browsable(false)]
        public override string TableName
        {
            get
            {
                return "Project";
            }
        }

        [Description(""), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual DateTime? Tj_date
        {
            get
            {
                return this.entityData.Tj_date;
            }
            set
            {
                if (this.entityData.Tj_date != value)
                {
                    this.OnColumnChanging(ProjectColumn.Tj_date, this.entityData.Tj_date);
                    this.entityData.Tj_date = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.Tj_date, this.entityData.Tj_date);
                    this.OnPropertyChanged("Tj_date");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, Description(""), DataObjectField(false, false, true)]
        public virtual decimal? TotalBuildingSpace
        {
            get
            {
                return this.entityData.TotalBuildingSpace;
            }
            set
            {
                if (this.entityData.TotalBuildingSpace != value)
                {
                    this.OnColumnChanging(ProjectColumn.TotalBuildingSpace, this.entityData.TotalBuildingSpace);
                    this.entityData.TotalBuildingSpace = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.TotalBuildingSpace, this.entityData.TotalBuildingSpace);
                    this.OnPropertyChanged("TotalBuildingSpace");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? TotalFloorSpace
        {
            get
            {
                return this.entityData.TotalFloorSpace;
            }
            set
            {
                if (this.entityData.TotalFloorSpace != value)
                {
                    this.OnColumnChanging(ProjectColumn.TotalFloorSpace, this.entityData.TotalFloorSpace);
                    this.entityData.TotalFloorSpace = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.TotalFloorSpace, this.entityData.TotalFloorSpace);
                    this.OnPropertyChanged("TotalFloorSpace");
                }
            }
        }

        [DataObjectField(false, false, true, 50), Description(""), TiannuoPM.Entities.Bindable]
        public virtual string U8Code
        {
            get
            {
                return this.entityData.U8Code;
            }
            set
            {
                if (this.entityData.U8Code != value)
                {
                    this.OnColumnChanging(ProjectColumn.U8Code, this.entityData.U8Code);
                    this.entityData.U8Code = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.U8Code, this.entityData.U8Code);
                    this.OnPropertyChanged("U8Code");
                }
            }
        }

        [Description(""), TiannuoPM.Entities.Bindable, DataObjectField(false, false, true)]
        public virtual decimal? UnderBuildingSpace
        {
            get
            {
                return this.entityData.UnderBuildingSpace;
            }
            set
            {
                if (this.entityData.UnderBuildingSpace != value)
                {
                    this.OnColumnChanging(ProjectColumn.UnderBuildingSpace, this.entityData.UnderBuildingSpace);
                    this.entityData.UnderBuildingSpace = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.UnderBuildingSpace, this.entityData.UnderBuildingSpace);
                    this.OnPropertyChanged("UnderBuildingSpace");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true), Description("")]
        public virtual decimal? UnderFloorSpace
        {
            get
            {
                return this.entityData.UnderFloorSpace;
            }
            set
            {
                if (this.entityData.UnderFloorSpace != value)
                {
                    this.OnColumnChanging(ProjectColumn.UnderFloorSpace, this.entityData.UnderFloorSpace);
                    this.entityData.UnderFloorSpace = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.UnderFloorSpace, this.entityData.UnderFloorSpace);
                    this.OnPropertyChanged("UnderFloorSpace");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true), Description("")]
        public virtual decimal? UnderParkingSpace
        {
            get
            {
                return this.entityData.UnderParkingSpace;
            }
            set
            {
                if (this.entityData.UnderParkingSpace != value)
                {
                    this.OnColumnChanging(ProjectColumn.UnderParkingSpace, this.entityData.UnderParkingSpace);
                    this.entityData.UnderParkingSpace = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.UnderParkingSpace, this.entityData.UnderParkingSpace);
                    this.OnPropertyChanged("UnderParkingSpace");
                }
            }
        }

        [TiannuoPM.Entities.Bindable, DataObjectField(false, false, true, 50), Description("")]
        public virtual string UnitCode
        {
            get
            {
                return this.entityData.UnitCode;
            }
            set
            {
                if (this.entityData.UnitCode != value)
                {
                    this.OnColumnChanging(ProjectColumn.UnitCode, this.entityData.UnitCode);
                    this.entityData.UnitCode = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.UnitCode, this.entityData.UnitCode);
                    this.OnPropertyChanged("UnitCode");
                }
            }
        }

        [Description(""), DataObjectField(false, false, true), TiannuoPM.Entities.Bindable]
        public virtual decimal? Waterspace
        {
            get
            {
                return this.entityData.Waterspace;
            }
            set
            {
                if (this.entityData.Waterspace != value)
                {
                    this.OnColumnChanging(ProjectColumn.Waterspace, this.entityData.Waterspace);
                    this.entityData.Waterspace = value;
                    if (this.EntityState == EntityState.Unchanged)
                    {
                        this.EntityState = EntityState.Changed;
                    }
                    this.OnColumnChanged(ProjectColumn.Waterspace, this.entityData.Waterspace);
                    this.OnPropertyChanged("Waterspace");
                }
            }
        }

        public delegate void CancelAddNewEventHandler(object sender, EventArgs e);

        [Serializable, EditorBrowsable(EditorBrowsableState.Never)]
        internal class ProjectEntityData : ICloneable
        {
            public decimal? AfforestingRate = null;
            public decimal? AfforestingSpace = null;
            public string Area = null;
            public string BlockID = null;
            public string BlockName = null;
            public decimal? BsBuildingSpace = null;
            public decimal? BuildingDensity = null;
            public decimal? BuildingSpaceForVolumeRate = null;
            public decimal? BuildingSpaceNotVolumeRate = null;
            public decimal? BuildSpace = null;
            public decimal? CenterAfforestingRate = null;
            public decimal? CenterAfforestingSpace = null;
            public string City = null;
            private TList<Contract> contractProjectCode;
            public string DevelopUnit = null;
            public string DevelopUnitAddress = null;
            public decimal? HouseBuildingSpace = null;
            public decimal? HouseCount = null;
            public string HouseUse = null;
            public string ImagePath = null;
            private TList<InspectSituation> inspectSituationProjectCode;
            public int? IsEstimate = null;
            public string IsUseShortName = null;
            public string Jd = null;
            public string JDBM = null;
            public string Jdxz = null;
            public DateTime? JgDate = null;
            public DateTime? KgDate = null;
            public string Manager = null;
            private TList<MaterialPurchas> materialPurchasProjectCode;
            public string OriginalProjectCode;
            public decimal? ParkingSpace = null;
            private TList<Payment> paymentProjectCode;
            private TList<Payout> payoutProjectCode;
            public decimal? Peripheryspace = null;
            public DateTime? PlanEndDate = null;
            public decimal? PlannedVolumeRate = null;
            public DateTime? PlanStartDate = null;
            public string ProjectAddress = null;
            public string ProjectCode;
            public string ProjectId = null;
            public string ProjectName = null;
            public string ProjectShortName = null;
            public string PTFeeType = null;
            public string PTFeeVoucherID = null;
            public string Remark = null;
            public string SalProjectCode = null;
            public string Status = null;
            public string SubjectSetCode = null;
            public DateTime? Tj_date = null;
            public decimal? TotalBuildingSpace = null;
            public decimal? TotalFloorSpace = null;
            public string U8Code = null;
            public decimal? UnderBuildingSpace = null;
            public decimal? UnderFloorSpace = null;
            public decimal? UnderParkingSpace = null;
            public string UnitCode = null;
            public decimal? Waterspace = null;

            public object Clone()
            {
                ProjectBase.ProjectEntityData data = new ProjectBase.ProjectEntityData();
                data.ProjectCode = this.ProjectCode;
                data.OriginalProjectCode = this.OriginalProjectCode;
                data.ProjectName = this.ProjectName;
                data.City = this.City;
                data.Area = this.Area;
                data.BlockName = this.BlockName;
                data.BlockID = this.BlockID;
                data.BuildSpace = this.BuildSpace;
                data.AfforestingRate = this.AfforestingRate;
                data.TotalBuildingSpace = this.TotalBuildingSpace;
                data.TotalFloorSpace = this.TotalFloorSpace;
                data.PlannedVolumeRate = this.PlannedVolumeRate;
                data.BuildingDensity = this.BuildingDensity;
                data.BuildingSpaceForVolumeRate = this.BuildingSpaceForVolumeRate;
                data.BuildingSpaceNotVolumeRate = this.BuildingSpaceNotVolumeRate;
                data.IsEstimate = this.IsEstimate;
                data.Remark = this.Remark;
                data.Tj_date = this.Tj_date;
                data.SubjectSetCode = this.SubjectSetCode;
                data.Status = this.Status;
                data.ProjectId = this.ProjectId;
                data.ProjectAddress = this.ProjectAddress;
                data.ImagePath = this.ImagePath;
                data.UnitCode = this.UnitCode;
                data.Jd = this.Jd;
                data.Jdxz = this.Jdxz;
                data.JDBM = this.JDBM;
                data.DevelopUnit = this.DevelopUnit;
                data.SalProjectCode = this.SalProjectCode;
                data.KgDate = this.KgDate;
                data.JgDate = this.JgDate;
                data.PlanStartDate = this.PlanStartDate;
                data.PlanEndDate = this.PlanEndDate;
                data.ProjectShortName = this.ProjectShortName;
                data.UnderBuildingSpace = this.UnderBuildingSpace;
                data.UnderFloorSpace = this.UnderFloorSpace;
                data.AfforestingSpace = this.AfforestingSpace;
                data.CenterAfforestingSpace = this.CenterAfforestingSpace;
                data.CenterAfforestingRate = this.CenterAfforestingRate;
                data.ParkingSpace = this.ParkingSpace;
                data.UnderParkingSpace = this.UnderParkingSpace;
                data.HouseCount = this.HouseCount;
                data.HouseUse = this.HouseUse;
                data.PTFeeType = this.PTFeeType;
                data.PTFeeVoucherID = this.PTFeeVoucherID;
                data.HouseBuildingSpace = this.HouseBuildingSpace;
                data.BsBuildingSpace = this.BsBuildingSpace;
                data.Manager = this.Manager;
                data.U8Code = this.U8Code;
                data.DevelopUnitAddress = this.DevelopUnitAddress;
                data.Waterspace = this.Waterspace;
                data.Peripheryspace = this.Peripheryspace;
                data.IsUseShortName = this.IsUseShortName;
                return data;
            }

            public TList<Contract> ContractCollection
            {
                get
                {
                    if (this.contractProjectCode == null)
                    {
                        this.contractProjectCode = new TList<Contract>();
                    }
                    return this.contractProjectCode;
                }
                set
                {
                    this.contractProjectCode = value;
                }
            }

            public TList<InspectSituation> InspectSituationCollection
            {
                get
                {
                    if (this.inspectSituationProjectCode == null)
                    {
                        this.inspectSituationProjectCode = new TList<InspectSituation>();
                    }
                    return this.inspectSituationProjectCode;
                }
                set
                {
                    this.inspectSituationProjectCode = value;
                }
            }

            public TList<MaterialPurchas> MaterialPurchasCollection
            {
                get
                {
                    if (this.materialPurchasProjectCode == null)
                    {
                        this.materialPurchasProjectCode = new TList<MaterialPurchas>();
                    }
                    return this.materialPurchasProjectCode;
                }
                set
                {
                    this.materialPurchasProjectCode = value;
                }
            }

            public TList<Payment> PaymentCollection
            {
                get
                {
                    if (this.paymentProjectCode == null)
                    {
                        this.paymentProjectCode = new TList<Payment>();
                    }
                    return this.paymentProjectCode;
                }
                set
                {
                    this.paymentProjectCode = value;
                }
            }

            public TList<Payout> PayoutCollection
            {
                get
                {
                    if (this.payoutProjectCode == null)
                    {
                        this.payoutProjectCode = new TList<Payout>();
                    }
                    return this.payoutProjectCode;
                }
                set
                {
                    this.payoutProjectCode = value;
                }
            }
        }
    }
}

