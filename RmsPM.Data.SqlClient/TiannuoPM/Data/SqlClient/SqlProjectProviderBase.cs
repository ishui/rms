namespace TiannuoPM.Data.SqlClient
{
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using TiannuoPM.Data;
    using TiannuoPM.Data.Bases;
    using TiannuoPM.Entities;

    public class SqlProjectProviderBase : ProjectProviderBase
    {
        private string _connectionString;
        private string _providerInvariantName;
        private bool _useStoredProcedure;

        public SqlProjectProviderBase()
        {
        }

        public SqlProjectProviderBase(string connectionString, bool useStoredProcedure, string providerInvariantName)
        {
            this._connectionString = connectionString;
            this._useStoredProcedure = useStoredProcedure;
            this._providerInvariantName = providerInvariantName;
        }

        public override void BulkInsert(TransactionManager transactionManager, TList<Project> entities)
        {
            SqlBulkCopy copy = null;
            if ((transactionManager != null) && transactionManager.IsOpen)
            {
                SqlConnection connection = transactionManager.TransactionObject.Connection as SqlConnection;
                SqlTransaction externalTransaction = transactionManager.TransactionObject as SqlTransaction;
                copy = new SqlBulkCopy(connection, SqlBulkCopyOptions.CheckConstraints, externalTransaction);
            }
            else
            {
                copy = new SqlBulkCopy(this._connectionString, SqlBulkCopyOptions.CheckConstraints);
            }
            copy.BulkCopyTimeout = 360;
            copy.DestinationTableName = "Project";
            DataTable table = new DataTable();
            table.Columns.Add("ProjectCode", typeof(string)).AllowDBNull = false;
            table.Columns.Add("ProjectName", typeof(string)).AllowDBNull = true;
            table.Columns.Add("City", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Area", typeof(string)).AllowDBNull = true;
            table.Columns.Add("BlockName", typeof(string)).AllowDBNull = true;
            table.Columns.Add("BlockID", typeof(string)).AllowDBNull = true;
            table.Columns.Add("BuildSpace", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("AfforestingRate", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("TotalBuildingSpace", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("TotalFloorSpace", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("PlannedVolumeRate", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("BuildingDensity", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("BuildingSpaceForVolumeRate", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("BuildingSpaceNotVolumeRate", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("IsEstimate", typeof(int)).AllowDBNull = true;
            table.Columns.Add("Remark", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Tj_date", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("SubjectSetCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("Status", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ProjectId", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ProjectAddress", typeof(string)).AllowDBNull = true;
            table.Columns.Add("ImagePath", typeof(string)).AllowDBNull = true;
            table.Columns.Add("UnitCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("jd", typeof(string)).AllowDBNull = true;
            table.Columns.Add("jdxz", typeof(string)).AllowDBNull = true;
            table.Columns.Add("JDBM", typeof(string)).AllowDBNull = true;
            table.Columns.Add("DevelopUnit", typeof(string)).AllowDBNull = true;
            table.Columns.Add("SalProjectCode", typeof(string)).AllowDBNull = true;
            table.Columns.Add("kgDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("jgDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("PlanStartDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("PlanEndDate", typeof(DateTime)).AllowDBNull = true;
            table.Columns.Add("ProjectShortName", typeof(string)).AllowDBNull = true;
            table.Columns.Add("UnderBuildingSpace", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("UnderFloorSpace", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("AfforestingSpace", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("CenterAfforestingSpace", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("CenterAfforestingRate", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("ParkingSpace", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("UnderParkingSpace", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("HouseCount", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("HouseUse", typeof(string)).AllowDBNull = true;
            table.Columns.Add("PTFeeType", typeof(string)).AllowDBNull = true;
            table.Columns.Add("PTFeeVoucherID", typeof(string)).AllowDBNull = true;
            table.Columns.Add("HouseBuildingSpace", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("BsBuildingSpace", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("Manager", typeof(string)).AllowDBNull = true;
            table.Columns.Add("U8Code", typeof(string)).AllowDBNull = true;
            table.Columns.Add("DevelopUnitAddress", typeof(string)).AllowDBNull = true;
            table.Columns.Add("waterspace", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("peripheryspace", typeof(decimal)).AllowDBNull = true;
            table.Columns.Add("IsUseShortName", typeof(string)).AllowDBNull = true;
            copy.ColumnMappings.Add("ProjectCode", "ProjectCode");
            copy.ColumnMappings.Add("ProjectName", "ProjectName");
            copy.ColumnMappings.Add("City", "City");
            copy.ColumnMappings.Add("Area", "Area");
            copy.ColumnMappings.Add("BlockName", "BlockName");
            copy.ColumnMappings.Add("BlockID", "BlockID");
            copy.ColumnMappings.Add("BuildSpace", "BuildSpace");
            copy.ColumnMappings.Add("AfforestingRate", "AfforestingRate");
            copy.ColumnMappings.Add("TotalBuildingSpace", "TotalBuildingSpace");
            copy.ColumnMappings.Add("TotalFloorSpace", "TotalFloorSpace");
            copy.ColumnMappings.Add("PlannedVolumeRate", "PlannedVolumeRate");
            copy.ColumnMappings.Add("BuildingDensity", "BuildingDensity");
            copy.ColumnMappings.Add("BuildingSpaceForVolumeRate", "BuildingSpaceForVolumeRate");
            copy.ColumnMappings.Add("BuildingSpaceNotVolumeRate", "BuildingSpaceNotVolumeRate");
            copy.ColumnMappings.Add("IsEstimate", "IsEstimate");
            copy.ColumnMappings.Add("Remark", "Remark");
            copy.ColumnMappings.Add("Tj_date", "Tj_date");
            copy.ColumnMappings.Add("SubjectSetCode", "SubjectSetCode");
            copy.ColumnMappings.Add("Status", "Status");
            copy.ColumnMappings.Add("ProjectId", "ProjectId");
            copy.ColumnMappings.Add("ProjectAddress", "ProjectAddress");
            copy.ColumnMappings.Add("ImagePath", "ImagePath");
            copy.ColumnMappings.Add("UnitCode", "UnitCode");
            copy.ColumnMappings.Add("jd", "jd");
            copy.ColumnMappings.Add("jdxz", "jdxz");
            copy.ColumnMappings.Add("JDBM", "JDBM");
            copy.ColumnMappings.Add("DevelopUnit", "DevelopUnit");
            copy.ColumnMappings.Add("SalProjectCode", "SalProjectCode");
            copy.ColumnMappings.Add("kgDate", "kgDate");
            copy.ColumnMappings.Add("jgDate", "jgDate");
            copy.ColumnMappings.Add("PlanStartDate", "PlanStartDate");
            copy.ColumnMappings.Add("PlanEndDate", "PlanEndDate");
            copy.ColumnMappings.Add("ProjectShortName", "ProjectShortName");
            copy.ColumnMappings.Add("UnderBuildingSpace", "UnderBuildingSpace");
            copy.ColumnMappings.Add("UnderFloorSpace", "UnderFloorSpace");
            copy.ColumnMappings.Add("AfforestingSpace", "AfforestingSpace");
            copy.ColumnMappings.Add("CenterAfforestingSpace", "CenterAfforestingSpace");
            copy.ColumnMappings.Add("CenterAfforestingRate", "CenterAfforestingRate");
            copy.ColumnMappings.Add("ParkingSpace", "ParkingSpace");
            copy.ColumnMappings.Add("UnderParkingSpace", "UnderParkingSpace");
            copy.ColumnMappings.Add("HouseCount", "HouseCount");
            copy.ColumnMappings.Add("HouseUse", "HouseUse");
            copy.ColumnMappings.Add("PTFeeType", "PTFeeType");
            copy.ColumnMappings.Add("PTFeeVoucherID", "PTFeeVoucherID");
            copy.ColumnMappings.Add("HouseBuildingSpace", "HouseBuildingSpace");
            copy.ColumnMappings.Add("BsBuildingSpace", "BsBuildingSpace");
            copy.ColumnMappings.Add("Manager", "Manager");
            copy.ColumnMappings.Add("U8Code", "U8Code");
            copy.ColumnMappings.Add("DevelopUnitAddress", "DevelopUnitAddress");
            copy.ColumnMappings.Add("waterspace", "waterspace");
            copy.ColumnMappings.Add("peripheryspace", "peripheryspace");
            copy.ColumnMappings.Add("IsUseShortName", "IsUseShortName");
            foreach (Project project in entities)
            {
                if (project.EntityState == EntityState.Added)
                {
                    DataRow row = table.NewRow();
                    row["ProjectCode"] = project.ProjectCode;
                    row["ProjectName"] = project.ProjectName;
                    row["City"] = project.City;
                    row["Area"] = project.Area;
                    row["BlockName"] = project.BlockName;
                    row["BlockID"] = project.BlockID;
                    row["BuildSpace"] = project.BuildSpace.HasValue ? ((object) project.BuildSpace) : ((object) DBNull.Value);
                    row["AfforestingRate"] = project.AfforestingRate.HasValue ? ((object) project.AfforestingRate) : ((object) DBNull.Value);
                    row["TotalBuildingSpace"] = project.TotalBuildingSpace.HasValue ? ((object) project.TotalBuildingSpace) : ((object) DBNull.Value);
                    row["TotalFloorSpace"] = project.TotalFloorSpace.HasValue ? ((object) project.TotalFloorSpace) : ((object) DBNull.Value);
                    row["PlannedVolumeRate"] = project.PlannedVolumeRate.HasValue ? ((object) project.PlannedVolumeRate) : ((object) DBNull.Value);
                    row["BuildingDensity"] = project.BuildingDensity.HasValue ? ((object) project.BuildingDensity) : ((object) DBNull.Value);
                    row["BuildingSpaceForVolumeRate"] = project.BuildingSpaceForVolumeRate.HasValue ? ((object) project.BuildingSpaceForVolumeRate) : ((object) DBNull.Value);
                    row["BuildingSpaceNotVolumeRate"] = project.BuildingSpaceNotVolumeRate.HasValue ? ((object) project.BuildingSpaceNotVolumeRate) : ((object) DBNull.Value);
                    row["IsEstimate"] = project.IsEstimate.HasValue ? ((object) project.IsEstimate) : ((object) DBNull.Value);
                    row["Remark"] = project.Remark;
                    row["Tj_date"] = project.Tj_date.HasValue ? ((object) project.Tj_date) : ((object) DBNull.Value);
                    row["SubjectSetCode"] = project.SubjectSetCode;
                    row["Status"] = project.Status;
                    row["ProjectId"] = project.ProjectId;
                    row["ProjectAddress"] = project.ProjectAddress;
                    row["ImagePath"] = project.ImagePath;
                    row["UnitCode"] = project.UnitCode;
                    row["jd"] = project.Jd;
                    row["jdxz"] = project.Jdxz;
                    row["JDBM"] = project.JDBM;
                    row["DevelopUnit"] = project.DevelopUnit;
                    row["SalProjectCode"] = project.SalProjectCode;
                    row["kgDate"] = project.KgDate.HasValue ? ((object) project.KgDate) : ((object) DBNull.Value);
                    row["jgDate"] = project.JgDate.HasValue ? ((object) project.JgDate) : ((object) DBNull.Value);
                    row["PlanStartDate"] = project.PlanStartDate.HasValue ? ((object) project.PlanStartDate) : ((object) DBNull.Value);
                    row["PlanEndDate"] = project.PlanEndDate.HasValue ? ((object) project.PlanEndDate) : ((object) DBNull.Value);
                    row["ProjectShortName"] = project.ProjectShortName;
                    row["UnderBuildingSpace"] = project.UnderBuildingSpace.HasValue ? ((object) project.UnderBuildingSpace) : ((object) DBNull.Value);
                    row["UnderFloorSpace"] = project.UnderFloorSpace.HasValue ? ((object) project.UnderFloorSpace) : ((object) DBNull.Value);
                    row["AfforestingSpace"] = project.AfforestingSpace.HasValue ? ((object) project.AfforestingSpace) : ((object) DBNull.Value);
                    row["CenterAfforestingSpace"] = project.CenterAfforestingSpace.HasValue ? ((object) project.CenterAfforestingSpace) : ((object) DBNull.Value);
                    row["CenterAfforestingRate"] = project.CenterAfforestingRate.HasValue ? ((object) project.CenterAfforestingRate) : ((object) DBNull.Value);
                    row["ParkingSpace"] = project.ParkingSpace.HasValue ? ((object) project.ParkingSpace) : ((object) DBNull.Value);
                    row["UnderParkingSpace"] = project.UnderParkingSpace.HasValue ? ((object) project.UnderParkingSpace) : ((object) DBNull.Value);
                    row["HouseCount"] = project.HouseCount.HasValue ? ((object) project.HouseCount) : ((object) DBNull.Value);
                    row["HouseUse"] = project.HouseUse;
                    row["PTFeeType"] = project.PTFeeType;
                    row["PTFeeVoucherID"] = project.PTFeeVoucherID;
                    row["HouseBuildingSpace"] = project.HouseBuildingSpace.HasValue ? ((object) project.HouseBuildingSpace) : ((object) DBNull.Value);
                    row["BsBuildingSpace"] = project.BsBuildingSpace.HasValue ? ((object) project.BsBuildingSpace) : ((object) DBNull.Value);
                    row["Manager"] = project.Manager;
                    row["U8Code"] = project.U8Code;
                    row["DevelopUnitAddress"] = project.DevelopUnitAddress;
                    row["waterspace"] = project.Waterspace.HasValue ? ((object) project.Waterspace) : ((object) DBNull.Value);
                    row["peripheryspace"] = project.Peripheryspace.HasValue ? ((object) project.Peripheryspace) : ((object) DBNull.Value);
                    row["IsUseShortName"] = project.IsUseShortName;
                    table.Rows.Add(row);
                }
            }
            copy.WriteToServer(table);
            foreach (Project project in entities)
            {
                if (project.EntityState == EntityState.Added)
                {
                    project.AcceptChanges();
                }
            }
        }

        public override bool Delete(TransactionManager transactionManager, string projectCode)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Project_Delete", this._useStoredProcedure);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, projectCode);
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            if (DataRepository.Provider.EnableEntityTracking)
            {
                EntityManager.StopTracking(EntityLocator.ConstructKeyFromPkItems(typeof(Project), new object[] { projectCode }));
            }
            if (num == 0)
            {
                return false;
            }
            return Convert.ToBoolean(num);
        }

        public override TList<Project> Find(TransactionManager transactionManager, string whereClause, int start, int pageLength, out int count)
        {
            count = -1;
            if (whereClause.IndexOf(";") > -1)
            {
                return new TList<Project>();
            }
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Project_Find", this._useStoredProcedure);
            bool flag = false;
            if (whereClause.IndexOf(" OR ") > 0)
            {
                flag = true;
            }
            database.AddInParameter(command, "@SearchUsingOR", DbType.Boolean, flag);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ProjectName", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@City", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Area", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@BlockName", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@BlockID", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@BuildSpace", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@AfforestingRate", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@TotalBuildingSpace", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@TotalFloorSpace", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@PlannedVolumeRate", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@BuildingDensity", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@BuildingSpaceForVolumeRate", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@BuildingSpaceNotVolumeRate", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@IsEstimate", DbType.Int32, DBNull.Value);
            database.AddInParameter(command, "@Remark", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Tj_date", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@SubjectSetCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Status", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ProjectId", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ProjectAddress", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@ImagePath", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@UnitCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Jd", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Jdxz", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@JDBM", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@DevelopUnit", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@SalProjectCode", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@KgDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@JgDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@PlanStartDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@PlanEndDate", DbType.DateTime, DBNull.Value);
            database.AddInParameter(command, "@ProjectShortName", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@UnderBuildingSpace", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@UnderFloorSpace", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@AfforestingSpace", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@CenterAfforestingSpace", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@CenterAfforestingRate", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@ParkingSpace", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@UnderParkingSpace", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@HouseCount", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@HouseUse", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PTFeeType", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@PTFeeVoucherID", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@HouseBuildingSpace", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@BsBuildingSpace", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@Manager", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@U8Code", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@DevelopUnitAddress", DbType.AnsiString, DBNull.Value);
            database.AddInParameter(command, "@Waterspace", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@Peripheryspace", DbType.Decimal, DBNull.Value);
            database.AddInParameter(command, "@IsUseShortName", DbType.AnsiString, DBNull.Value);
            whereClause = whereClause.Replace(" AND ", "|").Replace(" OR ", "|");
            string[] textArray = whereClause.ToLower().Split(new char[] { '|' });
            char[] trimChars = new char[] { '=' };
            char[] chArray2 = new char[] { '\'' };
            foreach (string text in textArray)
            {
                if (text.Trim().StartsWith("projectcode ") || text.Trim().StartsWith("projectcode="))
                {
                    database.SetParameterValue(command, "@ProjectCode", text.Replace("projectcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("projectname ") || text.Trim().StartsWith("projectname="))
                {
                    database.SetParameterValue(command, "@ProjectName", text.Replace("projectname", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("city ") || text.Trim().StartsWith("city="))
                {
                    database.SetParameterValue(command, "@City", text.Replace("city", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("area ") || text.Trim().StartsWith("area="))
                {
                    database.SetParameterValue(command, "@Area", text.Replace("area", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("blockname ") || text.Trim().StartsWith("blockname="))
                {
                    database.SetParameterValue(command, "@BlockName", text.Replace("blockname", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("blockid ") || text.Trim().StartsWith("blockid="))
                {
                    database.SetParameterValue(command, "@BlockID", text.Replace("blockid", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("buildspace ") || text.Trim().StartsWith("buildspace="))
                {
                    database.SetParameterValue(command, "@BuildSpace", text.Replace("buildspace", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("afforestingrate ") || text.Trim().StartsWith("afforestingrate="))
                {
                    database.SetParameterValue(command, "@AfforestingRate", text.Replace("afforestingrate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("totalbuildingspace ") || text.Trim().StartsWith("totalbuildingspace="))
                {
                    database.SetParameterValue(command, "@TotalBuildingSpace", text.Replace("totalbuildingspace", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("totalfloorspace ") || text.Trim().StartsWith("totalfloorspace="))
                {
                    database.SetParameterValue(command, "@TotalFloorSpace", text.Replace("totalfloorspace", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("plannedvolumerate ") || text.Trim().StartsWith("plannedvolumerate="))
                {
                    database.SetParameterValue(command, "@PlannedVolumeRate", text.Replace("plannedvolumerate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("buildingdensity ") || text.Trim().StartsWith("buildingdensity="))
                {
                    database.SetParameterValue(command, "@BuildingDensity", text.Replace("buildingdensity", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("buildingspaceforvolumerate ") || text.Trim().StartsWith("buildingspaceforvolumerate="))
                {
                    database.SetParameterValue(command, "@BuildingSpaceForVolumeRate", text.Replace("buildingspaceforvolumerate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("buildingspacenotvolumerate ") || text.Trim().StartsWith("buildingspacenotvolumerate="))
                {
                    database.SetParameterValue(command, "@BuildingSpaceNotVolumeRate", text.Replace("buildingspacenotvolumerate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("isestimate ") || text.Trim().StartsWith("isestimate="))
                {
                    database.SetParameterValue(command, "@IsEstimate", text.Replace("isestimate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("remark ") || text.Trim().StartsWith("remark="))
                {
                    database.SetParameterValue(command, "@Remark", text.Replace("remark", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("tj_date ") || text.Trim().StartsWith("tj_date="))
                {
                    database.SetParameterValue(command, "@Tj_date", text.Replace("tj_date", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("subjectsetcode ") || text.Trim().StartsWith("subjectsetcode="))
                {
                    database.SetParameterValue(command, "@SubjectSetCode", text.Replace("subjectsetcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("status ") || text.Trim().StartsWith("status="))
                {
                    database.SetParameterValue(command, "@Status", text.Replace("status", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("projectid ") || text.Trim().StartsWith("projectid="))
                {
                    database.SetParameterValue(command, "@ProjectId", text.Replace("projectid", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("projectaddress ") || text.Trim().StartsWith("projectaddress="))
                {
                    database.SetParameterValue(command, "@ProjectAddress", text.Replace("projectaddress", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("imagepath ") || text.Trim().StartsWith("imagepath="))
                {
                    database.SetParameterValue(command, "@ImagePath", text.Replace("imagepath", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("unitcode ") || text.Trim().StartsWith("unitcode="))
                {
                    database.SetParameterValue(command, "@UnitCode", text.Replace("unitcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("jd ") || text.Trim().StartsWith("jd="))
                {
                    database.SetParameterValue(command, "@jd", text.Replace("jd", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("jdxz ") || text.Trim().StartsWith("jdxz="))
                {
                    database.SetParameterValue(command, "@jdxz", text.Replace("jdxz", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("jdbm ") || text.Trim().StartsWith("jdbm="))
                {
                    database.SetParameterValue(command, "@JDBM", text.Replace("jdbm", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("developunit ") || text.Trim().StartsWith("developunit="))
                {
                    database.SetParameterValue(command, "@DevelopUnit", text.Replace("developunit", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("salprojectcode ") || text.Trim().StartsWith("salprojectcode="))
                {
                    database.SetParameterValue(command, "@SalProjectCode", text.Replace("salprojectcode", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("kgdate ") || text.Trim().StartsWith("kgdate="))
                {
                    database.SetParameterValue(command, "@kgDate", text.Replace("kgdate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("jgdate ") || text.Trim().StartsWith("jgdate="))
                {
                    database.SetParameterValue(command, "@jgDate", text.Replace("jgdate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("planstartdate ") || text.Trim().StartsWith("planstartdate="))
                {
                    database.SetParameterValue(command, "@PlanStartDate", text.Replace("planstartdate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("planenddate ") || text.Trim().StartsWith("planenddate="))
                {
                    database.SetParameterValue(command, "@PlanEndDate", text.Replace("planenddate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("projectshortname ") || text.Trim().StartsWith("projectshortname="))
                {
                    database.SetParameterValue(command, "@ProjectShortName", text.Replace("projectshortname", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("underbuildingspace ") || text.Trim().StartsWith("underbuildingspace="))
                {
                    database.SetParameterValue(command, "@UnderBuildingSpace", text.Replace("underbuildingspace", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("underfloorspace ") || text.Trim().StartsWith("underfloorspace="))
                {
                    database.SetParameterValue(command, "@UnderFloorSpace", text.Replace("underfloorspace", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("afforestingspace ") || text.Trim().StartsWith("afforestingspace="))
                {
                    database.SetParameterValue(command, "@AfforestingSpace", text.Replace("afforestingspace", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("centerafforestingspace ") || text.Trim().StartsWith("centerafforestingspace="))
                {
                    database.SetParameterValue(command, "@CenterAfforestingSpace", text.Replace("centerafforestingspace", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("centerafforestingrate ") || text.Trim().StartsWith("centerafforestingrate="))
                {
                    database.SetParameterValue(command, "@CenterAfforestingRate", text.Replace("centerafforestingrate", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("parkingspace ") || text.Trim().StartsWith("parkingspace="))
                {
                    database.SetParameterValue(command, "@ParkingSpace", text.Replace("parkingspace", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("underparkingspace ") || text.Trim().StartsWith("underparkingspace="))
                {
                    database.SetParameterValue(command, "@UnderParkingSpace", text.Replace("underparkingspace", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("housecount ") || text.Trim().StartsWith("housecount="))
                {
                    database.SetParameterValue(command, "@HouseCount", text.Replace("housecount", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("houseuse ") || text.Trim().StartsWith("houseuse="))
                {
                    database.SetParameterValue(command, "@HouseUse", text.Replace("houseuse", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("ptfeetype ") || text.Trim().StartsWith("ptfeetype="))
                {
                    database.SetParameterValue(command, "@PTFeeType", text.Replace("ptfeetype", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("ptfeevoucherid ") || text.Trim().StartsWith("ptfeevoucherid="))
                {
                    database.SetParameterValue(command, "@PTFeeVoucherID", text.Replace("ptfeevoucherid", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("housebuildingspace ") || text.Trim().StartsWith("housebuildingspace="))
                {
                    database.SetParameterValue(command, "@HouseBuildingSpace", text.Replace("housebuildingspace", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("bsbuildingspace ") || text.Trim().StartsWith("bsbuildingspace="))
                {
                    database.SetParameterValue(command, "@BsBuildingSpace", text.Replace("bsbuildingspace", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("manager ") || text.Trim().StartsWith("manager="))
                {
                    database.SetParameterValue(command, "@Manager", text.Replace("manager", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("u8code ") || text.Trim().StartsWith("u8code="))
                {
                    database.SetParameterValue(command, "@U8Code", text.Replace("u8code", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("developunitaddress ") || text.Trim().StartsWith("developunitaddress="))
                {
                    database.SetParameterValue(command, "@DevelopUnitAddress", text.Replace("developunitaddress", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("waterspace ") || text.Trim().StartsWith("waterspace="))
                {
                    database.SetParameterValue(command, "@waterspace", text.Replace("waterspace", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else if (text.Trim().StartsWith("peripheryspace ") || text.Trim().StartsWith("peripheryspace="))
                {
                    database.SetParameterValue(command, "@peripheryspace", text.Replace("peripheryspace", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
                else
                {
                    if (!text.Trim().StartsWith("isuseshortname ") && !text.Trim().StartsWith("isuseshortname="))
                    {
                        throw new ArgumentException("Unable to use this part of the where clause in this version of Find: " + text);
                    }
                    database.SetParameterValue(command, "@IsUseShortName", text.Replace("isuseshortname", "").Trim().TrimStart(trimChars).Trim().Trim(chArray2));
                }
            }
            IDataReader reader = null;
            TList<Project> rows = new TList<Project>();
            try
            {
                if (transactionManager != null)
                {
                    reader = Utility.ExecuteReader(transactionManager, command);
                }
                else
                {
                    reader = Utility.ExecuteReader(database, command);
                }
                ProjectProviderBaseCore.Fill(reader, rows, start, pageLength);
                if (reader.NextResult() && reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return rows;
        }

        public override TList<Project> Find(TransactionManager transactionManager, SqlFilterParameterCollection parameters, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Project_Find_Dynamic", typeof(ProjectColumn), parameters, orderBy, start, pageLength);
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    SqlFilterParameter parameter = parameters[i];
                    database.AddInParameter(command, parameter.Name, parameter.DbType, parameter.Value);
                }
            }
            TList<Project> rows = new TList<Project>();
            IDataReader reader = null;
            try
            {
                if (transactionManager != null)
                {
                    reader = Utility.ExecuteReader(transactionManager, command);
                }
                else
                {
                    reader = Utility.ExecuteReader(database, command);
                }
                ProjectProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
                count = rows.Count;
                if (!reader.NextResult())
                {
                    return rows;
                }
                if (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return rows;
        }

        public override TList<Project> GetAll(TransactionManager transactionManager, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand dbCommand = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Project_Get_List", this._useStoredProcedure);
            IDataReader reader = null;
            TList<Project> rows = new TList<Project>();
            try
            {
                if (transactionManager != null)
                {
                    reader = Utility.ExecuteReader(transactionManager, dbCommand);
                }
                else
                {
                    reader = Utility.ExecuteReader(database, dbCommand);
                }
                ProjectProviderBaseCore.Fill(reader, rows, start, pageLength);
                count = -1;
                if (!reader.NextResult())
                {
                    return rows;
                }
                if (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return rows;
        }

        public override Project GetByProjectCode(TransactionManager transactionManager, string projectCode, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Project_GetByProjectCode", this._useStoredProcedure);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, projectCode);
            IDataReader reader = null;
            TList<Project> rows = new TList<Project>();
            try
            {
                if (transactionManager != null)
                {
                    reader = Utility.ExecuteReader(transactionManager, command);
                }
                else
                {
                    reader = Utility.ExecuteReader(database, command);
                }
                ProjectProviderBaseCore.Fill(reader, rows, start, pageLength);
                count = -1;
                if (reader.NextResult() && reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            if (rows.Count == 1)
            {
                return rows[0];
            }
            if (rows.Count != 0)
            {
                throw new DataException("Cannot find the unique instance of the class.");
            }
            return null;
        }

        public override TList<Project> GetPaged(TransactionManager transactionManager, string whereClause, string orderBy, int start, int pageLength, out int count)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Project_GetPaged", this._useStoredProcedure);
            database.AddInParameter(command, "@WhereClause", DbType.String, whereClause);
            database.AddInParameter(command, "@OrderBy", DbType.String, orderBy);
            database.AddInParameter(command, "@PageIndex", DbType.Int32, start);
            database.AddInParameter(command, "@PageSize", DbType.Int32, pageLength);
            IDataReader reader = null;
            TList<Project> rows = new TList<Project>();
            try
            {
                try
                {
                    if (transactionManager != null)
                    {
                        reader = Utility.ExecuteReader(transactionManager, command);
                    }
                    else
                    {
                        reader = Utility.ExecuteReader(database, command);
                    }
                    ProjectProviderBaseCore.Fill(reader, rows, 0, 0x7fffffff);
                    count = rows.Count;
                    if (reader.NextResult() && reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }
                    return rows;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return rows;
        }

        public override bool Insert(TransactionManager transactionManager, Project entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Project_Insert", this._useStoredProcedure);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, entity.ProjectCode);
            database.AddInParameter(command, "@ProjectName", DbType.AnsiString, entity.ProjectName);
            database.AddInParameter(command, "@City", DbType.AnsiString, entity.City);
            database.AddInParameter(command, "@Area", DbType.AnsiString, entity.Area);
            database.AddInParameter(command, "@BlockName", DbType.AnsiString, entity.BlockName);
            database.AddInParameter(command, "@BlockID", DbType.AnsiString, entity.BlockID);
            database.AddInParameter(command, "@BuildSpace", DbType.Decimal, entity.BuildSpace.HasValue ? ((object) entity.BuildSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AfforestingRate", DbType.Decimal, entity.AfforestingRate.HasValue ? ((object) entity.AfforestingRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@TotalBuildingSpace", DbType.Decimal, entity.TotalBuildingSpace.HasValue ? ((object) entity.TotalBuildingSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@TotalFloorSpace", DbType.Decimal, entity.TotalFloorSpace.HasValue ? ((object) entity.TotalFloorSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PlannedVolumeRate", DbType.Decimal, entity.PlannedVolumeRate.HasValue ? ((object) entity.PlannedVolumeRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@BuildingDensity", DbType.Decimal, entity.BuildingDensity.HasValue ? ((object) entity.BuildingDensity) : ((object) DBNull.Value));
            database.AddInParameter(command, "@BuildingSpaceForVolumeRate", DbType.Decimal, entity.BuildingSpaceForVolumeRate.HasValue ? ((object) entity.BuildingSpaceForVolumeRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@BuildingSpaceNotVolumeRate", DbType.Decimal, entity.BuildingSpaceNotVolumeRate.HasValue ? ((object) entity.BuildingSpaceNotVolumeRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@IsEstimate", DbType.Int32, entity.IsEstimate.HasValue ? ((object) entity.IsEstimate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Remark", DbType.AnsiString, entity.Remark);
            database.AddInParameter(command, "@Tj_date", DbType.DateTime, entity.Tj_date.HasValue ? ((object) entity.Tj_date) : ((object) DBNull.Value));
            database.AddInParameter(command, "@SubjectSetCode", DbType.AnsiString, entity.SubjectSetCode);
            database.AddInParameter(command, "@Status", DbType.AnsiString, entity.Status);
            database.AddInParameter(command, "@ProjectId", DbType.AnsiString, entity.ProjectId);
            database.AddInParameter(command, "@ProjectAddress", DbType.AnsiString, entity.ProjectAddress);
            database.AddInParameter(command, "@ImagePath", DbType.AnsiString, entity.ImagePath);
            database.AddInParameter(command, "@UnitCode", DbType.AnsiString, entity.UnitCode);
            database.AddInParameter(command, "@Jd", DbType.AnsiString, entity.Jd);
            database.AddInParameter(command, "@Jdxz", DbType.AnsiString, entity.Jdxz);
            database.AddInParameter(command, "@JDBM", DbType.AnsiString, entity.JDBM);
            database.AddInParameter(command, "@DevelopUnit", DbType.AnsiString, entity.DevelopUnit);
            database.AddInParameter(command, "@SalProjectCode", DbType.AnsiString, entity.SalProjectCode);
            database.AddInParameter(command, "@KgDate", DbType.DateTime, entity.KgDate.HasValue ? ((object) entity.KgDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@JgDate", DbType.DateTime, entity.JgDate.HasValue ? ((object) entity.JgDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PlanStartDate", DbType.DateTime, entity.PlanStartDate.HasValue ? ((object) entity.PlanStartDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PlanEndDate", DbType.DateTime, entity.PlanEndDate.HasValue ? ((object) entity.PlanEndDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ProjectShortName", DbType.AnsiString, entity.ProjectShortName);
            database.AddInParameter(command, "@UnderBuildingSpace", DbType.Decimal, entity.UnderBuildingSpace.HasValue ? ((object) entity.UnderBuildingSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@UnderFloorSpace", DbType.Decimal, entity.UnderFloorSpace.HasValue ? ((object) entity.UnderFloorSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AfforestingSpace", DbType.Decimal, entity.AfforestingSpace.HasValue ? ((object) entity.AfforestingSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CenterAfforestingSpace", DbType.Decimal, entity.CenterAfforestingSpace.HasValue ? ((object) entity.CenterAfforestingSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CenterAfforestingRate", DbType.Decimal, entity.CenterAfforestingRate.HasValue ? ((object) entity.CenterAfforestingRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ParkingSpace", DbType.Decimal, entity.ParkingSpace.HasValue ? ((object) entity.ParkingSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@UnderParkingSpace", DbType.Decimal, entity.UnderParkingSpace.HasValue ? ((object) entity.UnderParkingSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@HouseCount", DbType.Decimal, entity.HouseCount.HasValue ? ((object) entity.HouseCount) : ((object) DBNull.Value));
            database.AddInParameter(command, "@HouseUse", DbType.AnsiString, entity.HouseUse);
            database.AddInParameter(command, "@PTFeeType", DbType.AnsiString, entity.PTFeeType);
            database.AddInParameter(command, "@PTFeeVoucherID", DbType.AnsiString, entity.PTFeeVoucherID);
            database.AddInParameter(command, "@HouseBuildingSpace", DbType.Decimal, entity.HouseBuildingSpace.HasValue ? ((object) entity.HouseBuildingSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@BsBuildingSpace", DbType.Decimal, entity.BsBuildingSpace.HasValue ? ((object) entity.BsBuildingSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Manager", DbType.AnsiString, entity.Manager);
            database.AddInParameter(command, "@U8Code", DbType.AnsiString, entity.U8Code);
            database.AddInParameter(command, "@DevelopUnitAddress", DbType.AnsiString, entity.DevelopUnitAddress);
            database.AddInParameter(command, "@Waterspace", DbType.Decimal, entity.Waterspace.HasValue ? ((object) entity.Waterspace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Peripheryspace", DbType.Decimal, entity.Peripheryspace.HasValue ? ((object) entity.Peripheryspace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@IsUseShortName", DbType.AnsiString, entity.IsUseShortName);
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            entity.OriginalProjectCode = entity.ProjectCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public override bool Update(TransactionManager transactionManager, Project entity)
        {
            SqlDatabase database = new SqlDatabase(this._connectionString);
            DbCommand command = StoredProcedureProvider.GetCommandWrapper(database, "dbo.Project_Update", this._useStoredProcedure);
            database.AddInParameter(command, "@ProjectCode", DbType.AnsiString, entity.ProjectCode);
            database.AddInParameter(command, "@OriginalProjectCode", DbType.AnsiString, entity.OriginalProjectCode);
            database.AddInParameter(command, "@ProjectName", DbType.AnsiString, entity.ProjectName);
            database.AddInParameter(command, "@City", DbType.AnsiString, entity.City);
            database.AddInParameter(command, "@Area", DbType.AnsiString, entity.Area);
            database.AddInParameter(command, "@BlockName", DbType.AnsiString, entity.BlockName);
            database.AddInParameter(command, "@BlockID", DbType.AnsiString, entity.BlockID);
            database.AddInParameter(command, "@BuildSpace", DbType.Decimal, entity.BuildSpace.HasValue ? ((object) entity.BuildSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AfforestingRate", DbType.Decimal, entity.AfforestingRate.HasValue ? ((object) entity.AfforestingRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@TotalBuildingSpace", DbType.Decimal, entity.TotalBuildingSpace.HasValue ? ((object) entity.TotalBuildingSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@TotalFloorSpace", DbType.Decimal, entity.TotalFloorSpace.HasValue ? ((object) entity.TotalFloorSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PlannedVolumeRate", DbType.Decimal, entity.PlannedVolumeRate.HasValue ? ((object) entity.PlannedVolumeRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@BuildingDensity", DbType.Decimal, entity.BuildingDensity.HasValue ? ((object) entity.BuildingDensity) : ((object) DBNull.Value));
            database.AddInParameter(command, "@BuildingSpaceForVolumeRate", DbType.Decimal, entity.BuildingSpaceForVolumeRate.HasValue ? ((object) entity.BuildingSpaceForVolumeRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@BuildingSpaceNotVolumeRate", DbType.Decimal, entity.BuildingSpaceNotVolumeRate.HasValue ? ((object) entity.BuildingSpaceNotVolumeRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@IsEstimate", DbType.Int32, entity.IsEstimate.HasValue ? ((object) entity.IsEstimate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Remark", DbType.AnsiString, entity.Remark);
            database.AddInParameter(command, "@Tj_date", DbType.DateTime, entity.Tj_date.HasValue ? ((object) entity.Tj_date) : ((object) DBNull.Value));
            database.AddInParameter(command, "@SubjectSetCode", DbType.AnsiString, entity.SubjectSetCode);
            database.AddInParameter(command, "@Status", DbType.AnsiString, entity.Status);
            database.AddInParameter(command, "@ProjectId", DbType.AnsiString, entity.ProjectId);
            database.AddInParameter(command, "@ProjectAddress", DbType.AnsiString, entity.ProjectAddress);
            database.AddInParameter(command, "@ImagePath", DbType.AnsiString, entity.ImagePath);
            database.AddInParameter(command, "@UnitCode", DbType.AnsiString, entity.UnitCode);
            database.AddInParameter(command, "@Jd", DbType.AnsiString, entity.Jd);
            database.AddInParameter(command, "@Jdxz", DbType.AnsiString, entity.Jdxz);
            database.AddInParameter(command, "@JDBM", DbType.AnsiString, entity.JDBM);
            database.AddInParameter(command, "@DevelopUnit", DbType.AnsiString, entity.DevelopUnit);
            database.AddInParameter(command, "@SalProjectCode", DbType.AnsiString, entity.SalProjectCode);
            database.AddInParameter(command, "@KgDate", DbType.DateTime, entity.KgDate.HasValue ? ((object) entity.KgDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@JgDate", DbType.DateTime, entity.JgDate.HasValue ? ((object) entity.JgDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PlanStartDate", DbType.DateTime, entity.PlanStartDate.HasValue ? ((object) entity.PlanStartDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@PlanEndDate", DbType.DateTime, entity.PlanEndDate.HasValue ? ((object) entity.PlanEndDate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ProjectShortName", DbType.AnsiString, entity.ProjectShortName);
            database.AddInParameter(command, "@UnderBuildingSpace", DbType.Decimal, entity.UnderBuildingSpace.HasValue ? ((object) entity.UnderBuildingSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@UnderFloorSpace", DbType.Decimal, entity.UnderFloorSpace.HasValue ? ((object) entity.UnderFloorSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@AfforestingSpace", DbType.Decimal, entity.AfforestingSpace.HasValue ? ((object) entity.AfforestingSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CenterAfforestingSpace", DbType.Decimal, entity.CenterAfforestingSpace.HasValue ? ((object) entity.CenterAfforestingSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@CenterAfforestingRate", DbType.Decimal, entity.CenterAfforestingRate.HasValue ? ((object) entity.CenterAfforestingRate) : ((object) DBNull.Value));
            database.AddInParameter(command, "@ParkingSpace", DbType.Decimal, entity.ParkingSpace.HasValue ? ((object) entity.ParkingSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@UnderParkingSpace", DbType.Decimal, entity.UnderParkingSpace.HasValue ? ((object) entity.UnderParkingSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@HouseCount", DbType.Decimal, entity.HouseCount.HasValue ? ((object) entity.HouseCount) : ((object) DBNull.Value));
            database.AddInParameter(command, "@HouseUse", DbType.AnsiString, entity.HouseUse);
            database.AddInParameter(command, "@PTFeeType", DbType.AnsiString, entity.PTFeeType);
            database.AddInParameter(command, "@PTFeeVoucherID", DbType.AnsiString, entity.PTFeeVoucherID);
            database.AddInParameter(command, "@HouseBuildingSpace", DbType.Decimal, entity.HouseBuildingSpace.HasValue ? ((object) entity.HouseBuildingSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@BsBuildingSpace", DbType.Decimal, entity.BsBuildingSpace.HasValue ? ((object) entity.BsBuildingSpace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Manager", DbType.AnsiString, entity.Manager);
            database.AddInParameter(command, "@U8Code", DbType.AnsiString, entity.U8Code);
            database.AddInParameter(command, "@DevelopUnitAddress", DbType.AnsiString, entity.DevelopUnitAddress);
            database.AddInParameter(command, "@Waterspace", DbType.Decimal, entity.Waterspace.HasValue ? ((object) entity.Waterspace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@Peripheryspace", DbType.Decimal, entity.Peripheryspace.HasValue ? ((object) entity.Peripheryspace) : ((object) DBNull.Value));
            database.AddInParameter(command, "@IsUseShortName", DbType.AnsiString, entity.IsUseShortName);
            int num = 0;
            if (transactionManager != null)
            {
                num = Utility.ExecuteNonQuery(transactionManager, command);
            }
            else
            {
                num = Utility.ExecuteNonQuery(database, command);
            }
            if (DataRepository.Provider.EnableEntityTracking)
            {
                EntityManager.StopTracking(entity.EntityTrackingKey);
            }
            entity.OriginalProjectCode = entity.ProjectCode;
            entity.AcceptChanges();
            return Convert.ToBoolean(num);
        }

        public string ConnectionString
        {
            get
            {
                return this._connectionString;
            }
            set
            {
                this._connectionString = value;
            }
        }

        public string ProviderInvariantName
        {
            get
            {
                return this._providerInvariantName;
            }
            set
            {
                this._providerInvariantName = value;
            }
        }

        public bool UseStoredProcedure
        {
            get
            {
                return this._useStoredProcedure;
            }
            set
            {
                this._useStoredProcedure = value;
            }
        }
    }
}

