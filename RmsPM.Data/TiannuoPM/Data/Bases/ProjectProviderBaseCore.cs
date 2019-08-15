namespace TiannuoPM.Data.Bases
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Entities;

    public abstract class ProjectProviderBaseCore : EntityProviderBase<Project, ProjectKey>
    {
        protected ProjectProviderBaseCore()
        {
        }

        internal override void DeepLoad(TransactionManager transactionManager, Project entity, bool deep, DeepLoadType deepLoadType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity != null)
            {
                if (base.CanDeepLoad(entity, "List<MaterialPurchas>", "MaterialPurchasCollection", deepLoadType, innerList))
                {
                    entity.MaterialPurchasCollection = DataRepository.MaterialPurchasProvider.GetByProjectCode(transactionManager, entity.ProjectCode);
                    if (deep && (entity.MaterialPurchasCollection.Count > 0))
                    {
                        DataRepository.MaterialPurchasProvider.DeepLoad(transactionManager, entity.MaterialPurchasCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
                if (base.CanDeepLoad(entity, "List<Payout>", "PayoutCollection", deepLoadType, innerList))
                {
                    entity.PayoutCollection = DataRepository.PayoutProvider.GetByProjectCode(transactionManager, entity.ProjectCode);
                    if (deep && (entity.PayoutCollection.Count > 0))
                    {
                        DataRepository.PayoutProvider.DeepLoad(transactionManager, entity.PayoutCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
                if (base.CanDeepLoad(entity, "List<Contract>", "ContractCollection", deepLoadType, innerList))
                {
                    entity.ContractCollection = DataRepository.ContractProvider.GetByProjectCode(transactionManager, entity.ProjectCode);
                    if (deep && (entity.ContractCollection.Count > 0))
                    {
                        DataRepository.ContractProvider.DeepLoad(transactionManager, entity.ContractCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
                if (base.CanDeepLoad(entity, "List<InspectSituation>", "InspectSituationCollection", deepLoadType, innerList))
                {
                    entity.InspectSituationCollection = DataRepository.InspectSituationProvider.GetByProjectCode(transactionManager, entity.ProjectCode);
                    if (deep && (entity.InspectSituationCollection.Count > 0))
                    {
                        DataRepository.InspectSituationProvider.DeepLoad(transactionManager, entity.InspectSituationCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
                if (base.CanDeepLoad(entity, "List<Payment>", "PaymentCollection", deepLoadType, innerList))
                {
                    entity.PaymentCollection = DataRepository.PaymentProvider.GetByProjectCode(transactionManager, entity.ProjectCode);
                    if (deep && (entity.PaymentCollection.Count > 0))
                    {
                        DataRepository.PaymentProvider.DeepLoad(transactionManager, entity.PaymentCollection, deep, deepLoadType, childTypes, innerList);
                    }
                }
            }
        }

        internal override bool DeepSave(TransactionManager transactionManager, Project entity, DeepSaveType deepSaveType, Type[] childTypes, ChildEntityTypesList innerList)
        {
            if (entity == null)
            {
                return false;
            }
            this.Save(transactionManager, entity);
            if (base.CanDeepSave(entity, "List<MaterialPurchas>", "MaterialPurchasCollection", deepSaveType, innerList))
            {
                foreach (MaterialPurchas purchas in entity.MaterialPurchasCollection)
                {
                    purchas.ProjectCode = entity.ProjectCode;
                }
                if ((entity.MaterialPurchasCollection.Count > 0) || (entity.MaterialPurchasCollection.DeletedItems.Count > 0))
                {
                    DataRepository.MaterialPurchasProvider.DeepSave(transactionManager, entity.MaterialPurchasCollection, deepSaveType, childTypes, innerList);
                }
            }
            if (base.CanDeepSave(entity, "List<Payout>", "PayoutCollection", deepSaveType, innerList))
            {
                foreach (Payout payout in entity.PayoutCollection)
                {
                    payout.ProjectCode = entity.ProjectCode;
                }
                if ((entity.PayoutCollection.Count > 0) || (entity.PayoutCollection.DeletedItems.Count > 0))
                {
                    DataRepository.PayoutProvider.DeepSave(transactionManager, entity.PayoutCollection, deepSaveType, childTypes, innerList);
                }
            }
            if (base.CanDeepSave(entity, "List<Contract>", "ContractCollection", deepSaveType, innerList))
            {
                foreach (Contract contract in entity.ContractCollection)
                {
                    contract.ProjectCode = entity.ProjectCode;
                }
                if ((entity.ContractCollection.Count > 0) || (entity.ContractCollection.DeletedItems.Count > 0))
                {
                    DataRepository.ContractProvider.DeepSave(transactionManager, entity.ContractCollection, deepSaveType, childTypes, innerList);
                }
            }
            if (base.CanDeepSave(entity, "List<InspectSituation>", "InspectSituationCollection", deepSaveType, innerList))
            {
                foreach (InspectSituation situation in entity.InspectSituationCollection)
                {
                    situation.ProjectCode = entity.ProjectCode;
                }
                if ((entity.InspectSituationCollection.Count > 0) || (entity.InspectSituationCollection.DeletedItems.Count > 0))
                {
                    DataRepository.InspectSituationProvider.DeepSave(transactionManager, entity.InspectSituationCollection, deepSaveType, childTypes, innerList);
                }
            }
            if (base.CanDeepSave(entity, "List<Payment>", "PaymentCollection", deepSaveType, innerList))
            {
                foreach (Payment payment in entity.PaymentCollection)
                {
                    payment.ProjectCode = entity.ProjectCode;
                }
                if ((entity.PaymentCollection.Count > 0) || (entity.PaymentCollection.DeletedItems.Count > 0))
                {
                    DataRepository.PaymentProvider.DeepSave(transactionManager, entity.PaymentCollection, deepSaveType, childTypes, innerList);
                }
            }
            return true;
        }

        public bool Delete(string projectCode)
        {
            return this.Delete(null, projectCode);
        }

        public abstract bool Delete(TransactionManager transactionManager, string projectCode);
        public override bool Delete(TransactionManager transactionManager, ProjectKey key)
        {
            return this.Delete(transactionManager, key.ProjectCode);
        }

        public static TList<Project> Fill(IDataReader reader, TList<Project> rows, int start, int pageLength)
        {
            int num;
            for (num = 0; num < start; num++)
            {
                if (!reader.Read())
                {
                    return rows;
                }
            }
            for (num = 0; num < pageLength; num++)
            {
                if (!reader.Read())
                {
                    return rows;
                }
                string text = null;
                Project item = null;
                if (DataRepository.Provider.UseEntityFactory)
                {
                    text = "Project" + (reader.IsDBNull(reader.GetOrdinal("ProjectCode")) ? string.Empty : ((string) reader["ProjectCode"])).ToString();
                    item = EntityManager.LocateOrCreate<Project>(text.ToString(), "Project", DataRepository.Provider.EntityCreationalFactoryType, DataRepository.Provider.EnableEntityTracking);
                }
                else
                {
                    item = new Project();
                }
                if (!(DataRepository.Provider.EnableEntityTracking && (item.EntityState != EntityState.Added)))
                {
                    decimal? nullable;
                    DateTime? nullable3;
                    item.SuppressEntityEvents = true;
                    item.ProjectCode = (string) reader["ProjectCode"];
                    item.OriginalProjectCode = item.ProjectCode;
                    item.ProjectName = reader.IsDBNull(reader.GetOrdinal("ProjectName")) ? null : ((string) reader["ProjectName"]);
                    item.City = reader.IsDBNull(reader.GetOrdinal("City")) ? null : ((string) reader["City"]);
                    item.Area = reader.IsDBNull(reader.GetOrdinal("Area")) ? null : ((string) reader["Area"]);
                    item.BlockName = reader.IsDBNull(reader.GetOrdinal("BlockName")) ? null : ((string) reader["BlockName"]);
                    item.BlockID = reader.IsDBNull(reader.GetOrdinal("BlockID")) ? null : ((string) reader["BlockID"]);
                    item.BuildSpace = reader.IsDBNull(reader.GetOrdinal("BuildSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["BuildSpace"]);
                    item.AfforestingRate = reader.IsDBNull(reader.GetOrdinal("AfforestingRate")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["AfforestingRate"]);
                    item.TotalBuildingSpace = reader.IsDBNull(reader.GetOrdinal("TotalBuildingSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["TotalBuildingSpace"]);
                    item.TotalFloorSpace = reader.IsDBNull(reader.GetOrdinal("TotalFloorSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["TotalFloorSpace"]);
                    item.PlannedVolumeRate = reader.IsDBNull(reader.GetOrdinal("PlannedVolumeRate")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["PlannedVolumeRate"]);
                    item.BuildingDensity = reader.IsDBNull(reader.GetOrdinal("BuildingDensity")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["BuildingDensity"]);
                    item.BuildingSpaceForVolumeRate = reader.IsDBNull(reader.GetOrdinal("BuildingSpaceForVolumeRate")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["BuildingSpaceForVolumeRate"]);
                    item.BuildingSpaceNotVolumeRate = reader.IsDBNull(reader.GetOrdinal("BuildingSpaceNotVolumeRate")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["BuildingSpaceNotVolumeRate"]);
                    item.IsEstimate = reader.IsDBNull(reader.GetOrdinal("IsEstimate")) ? null : ((int?) reader["IsEstimate"]);
                    item.Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? null : ((string) reader["Remark"]);
                    item.Tj_date = reader.IsDBNull(reader.GetOrdinal("Tj_date")) ? ((DateTime?) (nullable3 = null)) : ((DateTime?) reader["Tj_date"]);
                    item.SubjectSetCode = reader.IsDBNull(reader.GetOrdinal("SubjectSetCode")) ? null : ((string) reader["SubjectSetCode"]);
                    item.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : ((string) reader["Status"]);
                    item.ProjectId = reader.IsDBNull(reader.GetOrdinal("ProjectId")) ? null : ((string) reader["ProjectId"]);
                    item.ProjectAddress = reader.IsDBNull(reader.GetOrdinal("ProjectAddress")) ? null : ((string) reader["ProjectAddress"]);
                    item.ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : ((string) reader["ImagePath"]);
                    item.UnitCode = reader.IsDBNull(reader.GetOrdinal("UnitCode")) ? null : ((string) reader["UnitCode"]);
                    item.Jd = reader.IsDBNull(reader.GetOrdinal("jd")) ? null : ((string) reader["jd"]);
                    item.Jdxz = reader.IsDBNull(reader.GetOrdinal("jdxz")) ? null : ((string) reader["jdxz"]);
                    item.JDBM = reader.IsDBNull(reader.GetOrdinal("JDBM")) ? null : ((string) reader["JDBM"]);
                    item.DevelopUnit = reader.IsDBNull(reader.GetOrdinal("DevelopUnit")) ? null : ((string) reader["DevelopUnit"]);
                    item.SalProjectCode = reader.IsDBNull(reader.GetOrdinal("SalProjectCode")) ? null : ((string) reader["SalProjectCode"]);
                    item.KgDate = reader.IsDBNull(reader.GetOrdinal("kgDate")) ? ((DateTime?) (nullable3 = null)) : ((DateTime?) reader["kgDate"]);
                    item.JgDate = reader.IsDBNull(reader.GetOrdinal("jgDate")) ? ((DateTime?) (nullable3 = null)) : ((DateTime?) reader["jgDate"]);
                    item.PlanStartDate = reader.IsDBNull(reader.GetOrdinal("PlanStartDate")) ? ((DateTime?) (nullable3 = null)) : ((DateTime?) reader["PlanStartDate"]);
                    item.PlanEndDate = reader.IsDBNull(reader.GetOrdinal("PlanEndDate")) ? ((DateTime?) (nullable3 = null)) : ((DateTime?) reader["PlanEndDate"]);
                    item.ProjectShortName = reader.IsDBNull(reader.GetOrdinal("ProjectShortName")) ? null : ((string) reader["ProjectShortName"]);
                    item.UnderBuildingSpace = reader.IsDBNull(reader.GetOrdinal("UnderBuildingSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["UnderBuildingSpace"]);
                    item.UnderFloorSpace = reader.IsDBNull(reader.GetOrdinal("UnderFloorSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["UnderFloorSpace"]);
                    item.AfforestingSpace = reader.IsDBNull(reader.GetOrdinal("AfforestingSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["AfforestingSpace"]);
                    item.CenterAfforestingSpace = reader.IsDBNull(reader.GetOrdinal("CenterAfforestingSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["CenterAfforestingSpace"]);
                    item.CenterAfforestingRate = reader.IsDBNull(reader.GetOrdinal("CenterAfforestingRate")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["CenterAfforestingRate"]);
                    item.ParkingSpace = reader.IsDBNull(reader.GetOrdinal("ParkingSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ParkingSpace"]);
                    item.UnderParkingSpace = reader.IsDBNull(reader.GetOrdinal("UnderParkingSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["UnderParkingSpace"]);
                    item.HouseCount = reader.IsDBNull(reader.GetOrdinal("HouseCount")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["HouseCount"]);
                    item.HouseUse = reader.IsDBNull(reader.GetOrdinal("HouseUse")) ? null : ((string) reader["HouseUse"]);
                    item.PTFeeType = reader.IsDBNull(reader.GetOrdinal("PTFeeType")) ? null : ((string) reader["PTFeeType"]);
                    item.PTFeeVoucherID = reader.IsDBNull(reader.GetOrdinal("PTFeeVoucherID")) ? null : ((string) reader["PTFeeVoucherID"]);
                    item.HouseBuildingSpace = reader.IsDBNull(reader.GetOrdinal("HouseBuildingSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["HouseBuildingSpace"]);
                    item.BsBuildingSpace = reader.IsDBNull(reader.GetOrdinal("BsBuildingSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["BsBuildingSpace"]);
                    item.Manager = reader.IsDBNull(reader.GetOrdinal("Manager")) ? null : ((string) reader["Manager"]);
                    item.U8Code = reader.IsDBNull(reader.GetOrdinal("U8Code")) ? null : ((string) reader["U8Code"]);
                    item.DevelopUnitAddress = reader.IsDBNull(reader.GetOrdinal("DevelopUnitAddress")) ? null : ((string) reader["DevelopUnitAddress"]);
                    item.Waterspace = reader.IsDBNull(reader.GetOrdinal("waterspace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["waterspace"]);
                    item.Peripheryspace = reader.IsDBNull(reader.GetOrdinal("peripheryspace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["peripheryspace"]);
                    item.IsUseShortName = reader.IsDBNull(reader.GetOrdinal("IsUseShortName")) ? null : ((string) reader["IsUseShortName"]);
                    item.EntityTrackingKey = text;
                    item.AcceptChanges();
                    item.SuppressEntityEvents = false;
                }
                rows.Add(item);
            }
            return rows;
        }

        public override Project Get(TransactionManager transactionManager, ProjectKey key, int start, int pageLength)
        {
            return this.GetByProjectCode(transactionManager, key.ProjectCode, start, pageLength);
        }

        public Project GetByProjectCode(string projectCode)
        {
            int count = -1;
            return this.GetByProjectCode(null, projectCode, 0, 0x7fffffff, out count);
        }

        public Project GetByProjectCode(TransactionManager transactionManager, string projectCode)
        {
            int count = -1;
            return this.GetByProjectCode(transactionManager, projectCode, 0, 0x7fffffff, out count);
        }

        public Project GetByProjectCode(string projectCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByProjectCode(null, projectCode, start, pageLength, out count);
        }

        public Project GetByProjectCode(string projectCode, int start, int pageLength, out int count)
        {
            return this.GetByProjectCode(null, projectCode, start, pageLength, out count);
        }

        public Project GetByProjectCode(TransactionManager transactionManager, string projectCode, int start, int pageLength)
        {
            int count = -1;
            return this.GetByProjectCode(transactionManager, projectCode, start, pageLength, out count);
        }

        public abstract Project GetByProjectCode(TransactionManager transactionManager, string projectCode, int start, int pageLength, out int count);
        public static void RefreshEntity(DataSet dataSet, Project entity)
        {
            decimal? nullable;
            DateTime? nullable3;
            DataRow row = dataSet.Tables[0].Rows[0];
            entity.ProjectCode = (string) row["ProjectCode"];
            entity.OriginalProjectCode = (string) row["ProjectCode"];
            entity.ProjectName = Convert.IsDBNull(row["ProjectName"]) ? null : ((string) row["ProjectName"]);
            entity.City = Convert.IsDBNull(row["City"]) ? null : ((string) row["City"]);
            entity.Area = Convert.IsDBNull(row["Area"]) ? null : ((string) row["Area"]);
            entity.BlockName = Convert.IsDBNull(row["BlockName"]) ? null : ((string) row["BlockName"]);
            entity.BlockID = Convert.IsDBNull(row["BlockID"]) ? null : ((string) row["BlockID"]);
            entity.BuildSpace = Convert.IsDBNull(row["BuildSpace"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["BuildSpace"]);
            entity.AfforestingRate = Convert.IsDBNull(row["AfforestingRate"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["AfforestingRate"]);
            entity.TotalBuildingSpace = Convert.IsDBNull(row["TotalBuildingSpace"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["TotalBuildingSpace"]);
            entity.TotalFloorSpace = Convert.IsDBNull(row["TotalFloorSpace"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["TotalFloorSpace"]);
            entity.PlannedVolumeRate = Convert.IsDBNull(row["PlannedVolumeRate"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["PlannedVolumeRate"]);
            entity.BuildingDensity = Convert.IsDBNull(row["BuildingDensity"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["BuildingDensity"]);
            entity.BuildingSpaceForVolumeRate = Convert.IsDBNull(row["BuildingSpaceForVolumeRate"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["BuildingSpaceForVolumeRate"]);
            entity.BuildingSpaceNotVolumeRate = Convert.IsDBNull(row["BuildingSpaceNotVolumeRate"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["BuildingSpaceNotVolumeRate"]);
            entity.IsEstimate = Convert.IsDBNull(row["IsEstimate"]) ? null : ((int?) row["IsEstimate"]);
            entity.Remark = Convert.IsDBNull(row["Remark"]) ? null : ((string) row["Remark"]);
            entity.Tj_date = Convert.IsDBNull(row["Tj_date"]) ? ((DateTime?) (nullable3 = null)) : ((DateTime?) row["Tj_date"]);
            entity.SubjectSetCode = Convert.IsDBNull(row["SubjectSetCode"]) ? null : ((string) row["SubjectSetCode"]);
            entity.Status = Convert.IsDBNull(row["Status"]) ? null : ((string) row["Status"]);
            entity.ProjectId = Convert.IsDBNull(row["ProjectId"]) ? null : ((string) row["ProjectId"]);
            entity.ProjectAddress = Convert.IsDBNull(row["ProjectAddress"]) ? null : ((string) row["ProjectAddress"]);
            entity.ImagePath = Convert.IsDBNull(row["ImagePath"]) ? null : ((string) row["ImagePath"]);
            entity.UnitCode = Convert.IsDBNull(row["UnitCode"]) ? null : ((string) row["UnitCode"]);
            entity.Jd = Convert.IsDBNull(row["jd"]) ? null : ((string) row["jd"]);
            entity.Jdxz = Convert.IsDBNull(row["jdxz"]) ? null : ((string) row["jdxz"]);
            entity.JDBM = Convert.IsDBNull(row["JDBM"]) ? null : ((string) row["JDBM"]);
            entity.DevelopUnit = Convert.IsDBNull(row["DevelopUnit"]) ? null : ((string) row["DevelopUnit"]);
            entity.SalProjectCode = Convert.IsDBNull(row["SalProjectCode"]) ? null : ((string) row["SalProjectCode"]);
            entity.KgDate = Convert.IsDBNull(row["kgDate"]) ? ((DateTime?) (nullable3 = null)) : ((DateTime?) row["kgDate"]);
            entity.JgDate = Convert.IsDBNull(row["jgDate"]) ? ((DateTime?) (nullable3 = null)) : ((DateTime?) row["jgDate"]);
            entity.PlanStartDate = Convert.IsDBNull(row["PlanStartDate"]) ? ((DateTime?) (nullable3 = null)) : ((DateTime?) row["PlanStartDate"]);
            entity.PlanEndDate = Convert.IsDBNull(row["PlanEndDate"]) ? null : ((DateTime?) row["PlanEndDate"]);
            entity.ProjectShortName = Convert.IsDBNull(row["ProjectShortName"]) ? null : ((string) row["ProjectShortName"]);
            entity.UnderBuildingSpace = Convert.IsDBNull(row["UnderBuildingSpace"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["UnderBuildingSpace"]);
            entity.UnderFloorSpace = Convert.IsDBNull(row["UnderFloorSpace"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["UnderFloorSpace"]);
            entity.AfforestingSpace = Convert.IsDBNull(row["AfforestingSpace"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["AfforestingSpace"]);
            entity.CenterAfforestingSpace = Convert.IsDBNull(row["CenterAfforestingSpace"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["CenterAfforestingSpace"]);
            entity.CenterAfforestingRate = Convert.IsDBNull(row["CenterAfforestingRate"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["CenterAfforestingRate"]);
            entity.ParkingSpace = Convert.IsDBNull(row["ParkingSpace"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["ParkingSpace"]);
            entity.UnderParkingSpace = Convert.IsDBNull(row["UnderParkingSpace"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["UnderParkingSpace"]);
            entity.HouseCount = Convert.IsDBNull(row["HouseCount"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["HouseCount"]);
            entity.HouseUse = Convert.IsDBNull(row["HouseUse"]) ? null : ((string) row["HouseUse"]);
            entity.PTFeeType = Convert.IsDBNull(row["PTFeeType"]) ? null : ((string) row["PTFeeType"]);
            entity.PTFeeVoucherID = Convert.IsDBNull(row["PTFeeVoucherID"]) ? null : ((string) row["PTFeeVoucherID"]);
            entity.HouseBuildingSpace = Convert.IsDBNull(row["HouseBuildingSpace"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["HouseBuildingSpace"]);
            entity.BsBuildingSpace = Convert.IsDBNull(row["BsBuildingSpace"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["BsBuildingSpace"]);
            entity.Manager = Convert.IsDBNull(row["Manager"]) ? null : ((string) row["Manager"]);
            entity.U8Code = Convert.IsDBNull(row["U8Code"]) ? null : ((string) row["U8Code"]);
            entity.DevelopUnitAddress = Convert.IsDBNull(row["DevelopUnitAddress"]) ? null : ((string) row["DevelopUnitAddress"]);
            entity.Waterspace = Convert.IsDBNull(row["waterspace"]) ? ((decimal?) (nullable = null)) : ((decimal?) row["waterspace"]);
            entity.Peripheryspace = Convert.IsDBNull(row["peripheryspace"]) ? null : ((decimal?) row["peripheryspace"]);
            entity.IsUseShortName = Convert.IsDBNull(row["IsUseShortName"]) ? null : ((string) row["IsUseShortName"]);
            entity.AcceptChanges();
        }

        public static void RefreshEntity(IDataReader reader, Project entity)
        {
            if (reader.Read())
            {
                decimal? nullable;
                DateTime? nullable3;
                entity.ProjectCode = (string) reader["ProjectCode"];
                entity.OriginalProjectCode = (string) reader["ProjectCode"];
                entity.ProjectName = reader.IsDBNull(reader.GetOrdinal("ProjectName")) ? null : ((string) reader["ProjectName"]);
                entity.City = reader.IsDBNull(reader.GetOrdinal("City")) ? null : ((string) reader["City"]);
                entity.Area = reader.IsDBNull(reader.GetOrdinal("Area")) ? null : ((string) reader["Area"]);
                entity.BlockName = reader.IsDBNull(reader.GetOrdinal("BlockName")) ? null : ((string) reader["BlockName"]);
                entity.BlockID = reader.IsDBNull(reader.GetOrdinal("BlockID")) ? null : ((string) reader["BlockID"]);
                entity.BuildSpace = reader.IsDBNull(reader.GetOrdinal("BuildSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["BuildSpace"]);
                entity.AfforestingRate = reader.IsDBNull(reader.GetOrdinal("AfforestingRate")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["AfforestingRate"]);
                entity.TotalBuildingSpace = reader.IsDBNull(reader.GetOrdinal("TotalBuildingSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["TotalBuildingSpace"]);
                entity.TotalFloorSpace = reader.IsDBNull(reader.GetOrdinal("TotalFloorSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["TotalFloorSpace"]);
                entity.PlannedVolumeRate = reader.IsDBNull(reader.GetOrdinal("PlannedVolumeRate")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["PlannedVolumeRate"]);
                entity.BuildingDensity = reader.IsDBNull(reader.GetOrdinal("BuildingDensity")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["BuildingDensity"]);
                entity.BuildingSpaceForVolumeRate = reader.IsDBNull(reader.GetOrdinal("BuildingSpaceForVolumeRate")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["BuildingSpaceForVolumeRate"]);
                entity.BuildingSpaceNotVolumeRate = reader.IsDBNull(reader.GetOrdinal("BuildingSpaceNotVolumeRate")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["BuildingSpaceNotVolumeRate"]);
                entity.IsEstimate = reader.IsDBNull(reader.GetOrdinal("IsEstimate")) ? null : ((int?) reader["IsEstimate"]);
                entity.Remark = reader.IsDBNull(reader.GetOrdinal("Remark")) ? null : ((string) reader["Remark"]);
                entity.Tj_date = reader.IsDBNull(reader.GetOrdinal("Tj_date")) ? ((DateTime?) (nullable3 = null)) : ((DateTime?) reader["Tj_date"]);
                entity.SubjectSetCode = reader.IsDBNull(reader.GetOrdinal("SubjectSetCode")) ? null : ((string) reader["SubjectSetCode"]);
                entity.Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? null : ((string) reader["Status"]);
                entity.ProjectId = reader.IsDBNull(reader.GetOrdinal("ProjectId")) ? null : ((string) reader["ProjectId"]);
                entity.ProjectAddress = reader.IsDBNull(reader.GetOrdinal("ProjectAddress")) ? null : ((string) reader["ProjectAddress"]);
                entity.ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : ((string) reader["ImagePath"]);
                entity.UnitCode = reader.IsDBNull(reader.GetOrdinal("UnitCode")) ? null : ((string) reader["UnitCode"]);
                entity.Jd = reader.IsDBNull(reader.GetOrdinal("jd")) ? null : ((string) reader["jd"]);
                entity.Jdxz = reader.IsDBNull(reader.GetOrdinal("jdxz")) ? null : ((string) reader["jdxz"]);
                entity.JDBM = reader.IsDBNull(reader.GetOrdinal("JDBM")) ? null : ((string) reader["JDBM"]);
                entity.DevelopUnit = reader.IsDBNull(reader.GetOrdinal("DevelopUnit")) ? null : ((string) reader["DevelopUnit"]);
                entity.SalProjectCode = reader.IsDBNull(reader.GetOrdinal("SalProjectCode")) ? null : ((string) reader["SalProjectCode"]);
                entity.KgDate = reader.IsDBNull(reader.GetOrdinal("kgDate")) ? ((DateTime?) (nullable3 = null)) : ((DateTime?) reader["kgDate"]);
                entity.JgDate = reader.IsDBNull(reader.GetOrdinal("jgDate")) ? ((DateTime?) (nullable3 = null)) : ((DateTime?) reader["jgDate"]);
                entity.PlanStartDate = reader.IsDBNull(reader.GetOrdinal("PlanStartDate")) ? ((DateTime?) (nullable3 = null)) : ((DateTime?) reader["PlanStartDate"]);
                entity.PlanEndDate = reader.IsDBNull(reader.GetOrdinal("PlanEndDate")) ? null : ((DateTime?) reader["PlanEndDate"]);
                entity.ProjectShortName = reader.IsDBNull(reader.GetOrdinal("ProjectShortName")) ? null : ((string) reader["ProjectShortName"]);
                entity.UnderBuildingSpace = reader.IsDBNull(reader.GetOrdinal("UnderBuildingSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["UnderBuildingSpace"]);
                entity.UnderFloorSpace = reader.IsDBNull(reader.GetOrdinal("UnderFloorSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["UnderFloorSpace"]);
                entity.AfforestingSpace = reader.IsDBNull(reader.GetOrdinal("AfforestingSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["AfforestingSpace"]);
                entity.CenterAfforestingSpace = reader.IsDBNull(reader.GetOrdinal("CenterAfforestingSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["CenterAfforestingSpace"]);
                entity.CenterAfforestingRate = reader.IsDBNull(reader.GetOrdinal("CenterAfforestingRate")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["CenterAfforestingRate"]);
                entity.ParkingSpace = reader.IsDBNull(reader.GetOrdinal("ParkingSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["ParkingSpace"]);
                entity.UnderParkingSpace = reader.IsDBNull(reader.GetOrdinal("UnderParkingSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["UnderParkingSpace"]);
                entity.HouseCount = reader.IsDBNull(reader.GetOrdinal("HouseCount")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["HouseCount"]);
                entity.HouseUse = reader.IsDBNull(reader.GetOrdinal("HouseUse")) ? null : ((string) reader["HouseUse"]);
                entity.PTFeeType = reader.IsDBNull(reader.GetOrdinal("PTFeeType")) ? null : ((string) reader["PTFeeType"]);
                entity.PTFeeVoucherID = reader.IsDBNull(reader.GetOrdinal("PTFeeVoucherID")) ? null : ((string) reader["PTFeeVoucherID"]);
                entity.HouseBuildingSpace = reader.IsDBNull(reader.GetOrdinal("HouseBuildingSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["HouseBuildingSpace"]);
                entity.BsBuildingSpace = reader.IsDBNull(reader.GetOrdinal("BsBuildingSpace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["BsBuildingSpace"]);
                entity.Manager = reader.IsDBNull(reader.GetOrdinal("Manager")) ? null : ((string) reader["Manager"]);
                entity.U8Code = reader.IsDBNull(reader.GetOrdinal("U8Code")) ? null : ((string) reader["U8Code"]);
                entity.DevelopUnitAddress = reader.IsDBNull(reader.GetOrdinal("DevelopUnitAddress")) ? null : ((string) reader["DevelopUnitAddress"]);
                entity.Waterspace = reader.IsDBNull(reader.GetOrdinal("waterspace")) ? ((decimal?) (nullable = null)) : ((decimal?) reader["waterspace"]);
                entity.Peripheryspace = reader.IsDBNull(reader.GetOrdinal("peripheryspace")) ? null : ((decimal?) reader["peripheryspace"]);
                entity.IsUseShortName = reader.IsDBNull(reader.GetOrdinal("IsUseShortName")) ? null : ((string) reader["IsUseShortName"]);
                entity.AcceptChanges();
            }
        }
    }
}

