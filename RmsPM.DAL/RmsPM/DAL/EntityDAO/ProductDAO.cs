namespace RmsPM.DAL.EntityDAO
{
    using System;
    using Rms.ORMap;
    using RmsPM.DAL.QueryStrategy;

    public sealed class ProductDAO
    {
        public static void DeleteBuilding(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Building"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteBuildingFloor(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingFloor"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteBuildingFloorProgress(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingFloorProgress"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteBuildingFunction(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingFunction"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteBuildingModel(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingModel"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteBuildingStation(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingStation"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteBuildingSubjectSet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingSubjectSet"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteChamber(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Chamber"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteConstructPlanStep(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructPlanStep"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteModel(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Model"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeletePBS(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBS"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeletePhotos(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Photos"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteRoom(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Room"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteStandard_Building(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Building"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteStandard_Model(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Model"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteStandard_TempRoomOut(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_TempRoomOut"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteTempRoomOut(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TempRoomOut"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteTempRoomStructure(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TempRoomStructure"))
                {
                    ydao.DeleteAllRow(entity);
                    ydao.DeleteEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static EntityData GetAllBuilding()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Building"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllBuildingFloor()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingFloor"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllBuildingFloorProgress()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingFloorProgress"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllBuildingFunction()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingFunction"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllBuildingModel()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingModel"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllBuildingStation()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingStation"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllBuildingSubjectSet()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingSubjectSet"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllChamber()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Chamber"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllConstructPlanStep()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructPlanStep"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllModel()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Model"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllPBS()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBS"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllPhotos()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Photos"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllRoom()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Room"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllTempRoomOut()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TempRoomOut"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllTempRoomStructure()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TempRoomStructure"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllV_BuildingModel()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_BuildingModel"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetAllV_ROOM()
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_ROOM"))
                {
                    data = ydao.SelectAll();
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildAreaByProjectCode(string ProjectCode)
        {
            EntityData data2;
            try
            {
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(BuildingStrategyName.IsArea, "1"));
                builder.AddOrder("FullID", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Building", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Building"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingByOutListCode(string code)
        {
            EntityData data2;
            try
            {
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingStrategyName.OutListCode, code));
                builder.AddOrder("BuildingName", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Building", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingByOutListCode(string code, string ViewName)
        {
            EntityData data2;
            try
            {
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder(ViewName);
                builder.AddStrategy(new Strategy(BuildingStrategyName.OutListCode, code));
                builder.AddOrder("BuildingName", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData(ViewName, queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingByParentCode(string ParentCode)
        {
            EntityData data2;
            try
            {
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingStrategyName.ParentCode, ParentCode));
                builder.AddOrder("FullID", true);
                string queryString = builder.BuildQueryChildCountString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Building", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingByPaymentItemCode(string PaymentItemCode, string AlloType)
        {
            EntityData data2;
            try
            {
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder();
                if (AlloType.ToUpper() == "U")
                {
                    builder.AddStrategy(new Strategy(BuildingStrategyName.PaymentItemCodeU, PaymentItemCode));
                }
                else
                {
                    builder.AddStrategy(new Strategy(BuildingStrategyName.PaymentItemCodeB, PaymentItemCode));
                }
                builder.AddStrategy(new Strategy(BuildingStrategyName.IsArea, "2"));
                builder.AddOrder("BuildingName", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Building", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingByPBSTypeCode(string PBSTypeCode, string ProjectCode)
        {
            EntityData data2;
            try
            {
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(BuildingStrategyName.PBSTypeCode, PBSTypeCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Building", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingByPBSUnitCode(string PBSUnitCode)
        {
            EntityData data2;
            try
            {
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingStrategyName.PBSUnitCode, PBSUnitCode));
                builder.AddOrder("BuildingName", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Building", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingByProjectCode(string ProjectCode)
        {
            EntityData data2;
            try
            {
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingStrategyName.ProjectCode, ProjectCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Building", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingByProjectParentCode(string ProjectCode, string ParentCode)
        {
            EntityData data2;
            try
            {
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(BuildingStrategyName.ParentCode, ParentCode));
                builder.AddOrder("FullID", true);
                string queryString = builder.BuildQueryChildCountString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Building", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingFloorByBuildingCode(string BuildingCode)
        {
            EntityData data2;
            try
            {
                BuildingFloorStrategyBuilder builder = new BuildingFloorStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingFloorStrategyName.BuildingCode, BuildingCode));
                builder.AddOrder("FloorIndex", false);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("BuildingFloor", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingFloorByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingFloor"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingFloorProgressByBuildingCode(string BuildingCode)
        {
            EntityData data2;
            try
            {
                BuildingFloorProgressStrategyBuilder builder = new BuildingFloorProgressStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingFloorProgressStrategyName.BuildingCode, BuildingCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("BuildingFloorProgress", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingFloorProgressByBuildingCodeVisualProgress(string BuildingCode, string VisualProgressCode)
        {
            EntityData data2;
            try
            {
                BuildingFloorProgressStrategyBuilder builder = new BuildingFloorProgressStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingFloorProgressStrategyName.BuildingCode, BuildingCode));
                builder.AddStrategy(new Strategy(BuildingFloorProgressStrategyName.VisualProgressCode, VisualProgressCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("BuildingFloorProgress", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingFloorProgressByBuildingFloorCode(string BuildingFloorCode)
        {
            EntityData data2;
            try
            {
                BuildingFloorProgressStrategyBuilder builder = new BuildingFloorProgressStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingFloorProgressStrategyName.BuildingFloorCode, BuildingFloorCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("BuildingFloorProgress", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingFloorProgressByBuildingFloorWBSCode(string BuildingFloorCode, string WBSCode)
        {
            EntityData data2;
            try
            {
                BuildingFloorProgressStrategyBuilder builder = new BuildingFloorProgressStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingFloorProgressStrategyName.BuildingFloorCode, BuildingFloorCode));
                builder.AddStrategy(new Strategy(BuildingFloorProgressStrategyName.WBSCode, WBSCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("BuildingFloorProgress", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingFloorProgressByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingFloorProgress"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static string GetBuildingFullIDByCode(string code)
        {
            string text2;
            try
            {
                string text = "";
                EntityData buildingByCode = GetBuildingByCode(code);
                if (buildingByCode.HasRecord())
                {
                    text = buildingByCode.GetString("FullID");
                }
                buildingByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static EntityData GetBuildingFullNameByProjectCode(string ProjectCode)
        {
            EntityData data2;
            try
            {
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingStrategyName.ProjectCode, ProjectCode));
                builder.AddOrder("1", true);
                string queryString = builder.BuildQueryFullNameString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Building", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingFunctionByBuildingCode(string BuildingCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("BuildingFunction");
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingFunction"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("BuildingFunction", "SelectByBuildingCode").SqlString, "@BuildingCode", BuildingCode, entitydata, "BuildingFunction");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingFunctionByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingFunction"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingModelByBuildingCode(string BuildingCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("BuildingModel");
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingModel"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("BuildingModel", "SelectByBuildingCode").SqlString, "@BuildingCode", BuildingCode, entitydata, "BuildingModel");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingModelByBuildingFunctionCode(string BuildingFunctionCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("BuildingModel");
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingModel"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("BuildingModel", "SelectByBuildingFunctionCode").SqlString, "@BuildingFunctionCode", BuildingFunctionCode, entitydata, "BuildingModel");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingModelByBuildingStationCode(string BuildingStationCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("BuildingModel");
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingModel"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("BuildingModel", "SelectByBuildingStationCode").SqlString, "@BuildingStationCode", BuildingStationCode, entitydata, "BuildingModel");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingModelByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingModel"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingModelByModelCode(string ModelCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("BuildingModel");
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingModel"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("BuildingModel", "SelectByModelCode").SqlString, "@ModelCode", ModelCode, entitydata, "BuildingModel");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingNotAreaByProjectCode(string ProjectCode)
        {
            EntityData data2;
            try
            {
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(BuildingStrategyName.IsArea, "2"));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Building", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingStationByBuildingCode(string BuildingCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("BuildingStation");
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingStation"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("BuildingStation", "SelectByBuildingCode").SqlString, "@BuildingCode", BuildingCode, entitydata, "BuildingStation");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingStationByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingStation"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingSubjectSetByBuilding(string BuildingCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("BuildingSubjectSet");
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingSubjectSet"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("BuildingSubjectSet", "SelectByBuildingCode").SqlString, "@BuildingCode", BuildingCode, entitydata, "BuildingSubjectSet");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingSubjectSetByBuilding(string BuildingCode, string SubjectSetCode)
        {
            EntityData data2;
            try
            {
                BuildingSubjectSetStrategyBuilder builder = new BuildingSubjectSetStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingSubjectSetStrategyName.BuildingCode, BuildingCode));
                builder.AddStrategy(new Strategy(BuildingSubjectSetStrategyName.SubjectSetCode, SubjectSetCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("BuildingSubjectSet", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildingSubjectSetByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingSubjectSet"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildNoAreaByProjectCode(string ProjectCode)
        {
            EntityData data2;
            try
            {
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(BuildingStrategyName.IsArea, "2"));
                builder.AddOrder("FullID", true);
                string queryString = builder.BuildQueryChildCountString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Building", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetBuildNoAreaFullNameByProjectCode(string ProjectCode)
        {
            EntityData data2;
            try
            {
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(BuildingStrategyName.IsArea, "2"));
                builder.AddOrder("1", true);
                string queryString = builder.BuildQueryFullNameString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Building", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData getChamberByBuildingCode(string buildingCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Chamber");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Chamber"))
                {
                    string[] Params = new string[] { "@BuildingCode" };
                    object[] values = new object[] { buildingCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("Chamber", "SelectByBuildingCode").SqlString, Params, values, entitydata, "Chamber");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetChamberByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Chamber"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetChildBuildingByProjectCode(string ProjectCode, string ParentCode)
        {
            EntityData data2;
            try
            {
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(BuildingStrategyName.ParentCode, ParentCode));
                builder.AddOrder("FullID", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Building", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetConstructPlanStepByBuildingCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("ConstructPlanStep");
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructPlanStep"))
                {
                    string[] Params = new string[] { "@BuildingCode" };
                    object[] values = new object[] { code };
                    ydao.FillEntity(SqlManager.GetSqlStruct("ConstructPlanStep", "SelectByBuildingCode").GetSqlStringWithOrder(), Params, values, entitydata, "ConstructPlanStep");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetConstructPlanStepByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructPlanStep"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetCurrentFloorCountAndBeforeInvestByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Building");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Building"))
                {
                    string[] Params = new string[] { "@BuildingCode" };
                    object[] values = new object[] { code };
                    ydao.FillEntity(SqlManager.GetSqlStruct("Building", "SelectFloorCountByBuildingCode").SqlString, Params, values, entitydata, "Building");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetModelByBuildingCode(string BuildingCode, string ProjectCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Model");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Model"))
                {
                    string[] Params = new string[] { "@ProjectCode", "@BuildingCode" };
                    object[] values = new object[] { ProjectCode, BuildingCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("Model", "SelectCountByBuildingCode").SqlString, Params, values, entitydata, "Model");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetModelByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Model"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetModelByProjectCode(string ProjectCode)
        {
            EntityData data2;
            try
            {
                ModelStrategyBuilder builder = new ModelStrategyBuilder();
                builder.AddStrategy(new Strategy(ModelStrategyName.ProjectCode, ProjectCode));
                builder.AddOrder("ModelName", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Model", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetModelNoUseByBuildingCode(string BuildingCode, string ProjectCode, string BuildingModelCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Model");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Model"))
                {
                    string[] Params = new string[] { "@ProjectCode", "@BuildingCode", "@BuildingModelCode" };
                    object[] values = new object[] { ProjectCode, BuildingCode, BuildingModelCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("Model", "SelectNoUseByBuildingCode").SqlString, Params, values, entitydata, "Model");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetPBSByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBS"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetPBSByParentCode(string projectCode, string parentCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("PBS");
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBS"))
                {
                    string[] Params = new string[] { "@ProjectCode", "@ParentCode" };
                    object[] values = new object[] { projectCode, parentCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("PBS", "SelectByParentCode").GetSqlStringWithOrder(), Params, values, entitydata, "PBS");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetPhotosByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Photos"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetRoomByBuildingChamberRoomName(string BuildingName, string ChamberName, string RoomName, string ProjectCode)
        {
            EntityData data2;
            try
            {
                RoomStrategyBuilder builder = new RoomStrategyBuilder("V_ROOM");
                builder.AddStrategy(new Strategy(RoomStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(RoomStrategyName.BuildingName, BuildingName));
                builder.AddStrategy(new Strategy(RoomStrategyName.ChamberName, ChamberName));
                builder.AddStrategy(new Strategy(RoomStrategyName.RoomName, RoomName));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Room", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetRoomByBuildingCode(string BuildingCode)
        {
            EntityData data2;
            try
            {
                RoomStrategyBuilder builder = new RoomStrategyBuilder("Room");
                builder.AddStrategy(new Strategy(RoomStrategyName.BuildingCode, BuildingCode));
                builder.AddOrder("ChamberCode", true);
                builder.AddOrder("FloorIndex", true);
                builder.AddOrder("RoomName", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Room", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetRoomByBuildingCodeAndPos(string buildingCode, int x, int y)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Room");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Room"))
                {
                    string[] Params = new string[] { "@BuildingCode", "@RoomIndex", "@FloorIndex" };
                    object[] values = new object[] { buildingCode, x, y };
                    ydao.FillEntity(SqlManager.GetSqlStruct("Room", "SelectByBuildingCodeAndPos").SqlString, Params, values, entitydata, "Room");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetRoomByChamberRoomName(string ChamberName, string RoomName, string ProjectCode)
        {
            EntityData data2;
            try
            {
                RoomStrategyBuilder builder = new RoomStrategyBuilder("V_ROOM");
                builder.AddStrategy(new Strategy(RoomStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(RoomStrategyName.ChamberName, ChamberName));
                builder.AddStrategy(new Strategy(RoomStrategyName.RoomName, RoomName));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Room", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetRoomByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("Room"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetRoomByOutListCode(string code)
        {
            EntityData roomByOutListCode;
            try
            {
                roomByOutListCode = GetRoomByOutListCode(code, "ROOM");
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return roomByOutListCode;
        }

        public static EntityData GetRoomByOutListCode(string code, string ViewName)
        {
            EntityData data2;
            try
            {
                RoomStrategyBuilder builder = new RoomStrategyBuilder(ViewName);
                builder.AddStrategy(new Strategy(RoomStrategyName.OutListCode, code));
                builder.AddOrder("BuildingName", true);
                builder.AddOrder("ChamberCode", true);
                builder.AddOrder("FloorIndex", true);
                builder.AddOrder("RoomName", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData(ViewName, queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetRoomByProjectCode(string ProjectCode)
        {
            EntityData data2;
            try
            {
                RoomStrategyBuilder builder = new RoomStrategyBuilder("Room");
                builder.AddStrategy(new Strategy(RoomStrategyName.ProjectCode, ProjectCode));
                builder.AddOrder("ChamberCode", true);
                builder.AddOrder("FloorIndex", true);
                builder.AddOrder("RoomName", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Room", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSelectRoomInBuildingCode(string buildingCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Room");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Room"))
                {
                    string[] Params = new string[] { "@BuildingCode" };
                    object[] values = new object[] { buildingCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("Room", "SelectRoomInBuildingCode").SqlString, Params, values, entitydata, "Room");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetSelectRoomInBuildingCodeAndChamberCode(string buildingCode, string chamberCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Room");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Room"))
                {
                    string[] Params = new string[] { "@BuildingCode", "@ChamberCode" };
                    object[] values = new object[] { buildingCode, chamberCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("Room", "SelectRoomByBuildingCodeAndChamberCode").SqlString, Params, values, entitydata, "Room");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_BuildingByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_Building");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Building"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Building", "Select").SqlString, "@BuildingCode", code, entitydata, "Building");
                    ydao.FillEntity(SqlManager.GetSqlStruct("Chamber", "SelectByBuildingCode").SqlString, "@BuildingCode", code, entitydata, "Chamber");
                    ydao.FillEntity(SqlManager.GetSqlStruct("Room", "SelectByBuildingCode").SqlString, "@BuildingCode", code, entitydata, "Room");
                    ydao.FillEntity(SqlManager.GetSqlStruct("BuildingModel", "SelectByBuildingCode").SqlString, "@BuildingCode", code, entitydata, "BuildingModel");
                    ydao.FillEntity(SqlManager.GetSqlStruct("BuildingFunction", "SelectByBuildingCode").SqlString, "@BuildingCode", code, entitydata, "BuildingFunction");
                    ydao.FillEntity(SqlManager.GetSqlStruct("BuildingStation", "SelectByBuildingCode").SqlString, "@BuildingCode", code, entitydata, "BuildingStation");
                    ydao.FillEntity(SqlManager.GetSqlStruct("BuildingSubjectSet", "SelectByBuildingCode").SqlString, "@BuildingCode", code, entitydata, "BuildingSubjectSet");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_ModelByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_Model");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Model"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("Model", "Select").SqlString, "@ModelCode", code, entitydata, "Model");
                    ydao.FillEntity(SqlManager.GetSqlStruct("BuildingModel", "SelectByModelCode").SqlString, "@ModelCode", code, entitydata, "BuildingModel");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStandard_TempRoomOutByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Standard_TempRoomOut");
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_TempRoomOut"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("TempRoomOut", "Select").SqlString, "@OutListCode", code, entitydata, "TempRoomOut");
                    ydao.FillEntity(SqlManager.GetSqlStruct("TempRoomStructure", "SelectByOutListCode").SqlString, "@OutListCode", code, entitydata, "TempRoomStructure");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStrinctBuildingCodeByProjectCode(string projectCode, string tempState)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TempRoomStructure");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TempRoomStructure"))
                {
                    string[] Params = new string[] { "@ProjectCode", "@TempState" };
                    object[] values = new object[] { projectCode, tempState };
                    ydao.FillEntity(SqlManager.GetSqlStruct("TempRoomStructure", "SelectStrinctBuildingCodeByProjectCode").SqlString, Params, values, entitydata, "TempRoomStructure");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetStrinctChamberCodeBybuildingCode(string buildingCode, string tempState)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("Chamber");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Chamber"))
                {
                    string[] Params = new string[] { "@BuildingCode", "@TempState" };
                    object[] values = new object[] { buildingCode, tempState };
                    ydao.FillEntity(SqlManager.GetSqlStruct("Chamber", "SelectDistinctChamberCodeByBuildingCode").SqlString, Params, values, entitydata, "Chamber");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTempRoomOutAllRoomIndex(string outListCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TempRoomOut");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TempRoomOut"))
                {
                    string[] Params = new string[] { "@OutListCode" };
                    object[] values = new object[] { outListCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("TempRoomOut", "SelectAllRoomNumAndDim").SqlString, Params, values, entitydata, "TempRoomOut");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTempRoomOutByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TempRoomOut"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTempRoomOutByRoomCodeAndState(string roomCode, string state)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TempRoomOut");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TempRoomOut"))
                {
                    string[] Params = new string[] { "@roomCode", "@Out_State" };
                    object[] values = new object[] { roomCode, state };
                    ydao.FillEntity(SqlManager.GetSqlStruct("TempRoomOut", "SelectTempRoomOutByRoomCodeAndState").SqlString, Params, values, entitydata, "TempRoomOut");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTempRoomStructureByBuildingCode(string buildingCode, string tempState)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TempRoomStructure");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TempRoomStructure"))
                {
                    string[] Params = new string[] { "@TempBuildingCode", "@TempState" };
                    object[] values = new object[] { buildingCode, tempState };
                    ydao.FillEntity(SqlManager.GetSqlStruct("TempRoomStructure", "SelectTempRoomStructureByBuildingCode").SqlString, Params, values, entitydata, "TempRoomStructure");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTempRoomStructureByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("TempRoomStructure"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTempRoomStructureByOutListCode(string outListCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TempRoomStructure");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TempRoomStructure"))
                {
                    string[] Params = new string[] { "@OutListCode" };
                    object[] values = new object[] { outListCode };
                    ydao.FillEntity(SqlManager.GetSqlStruct("TempRoomStructure", "SelectByOutListCode").SqlString, Params, values, entitydata, "TempRoomStructure");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTempRoomStructureByProjectCode(string projectCode, string tempState)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TempRoomStructure");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TempRoomStructure"))
                {
                    string[] Params = new string[] { "@ProjectCode", "@TempState" };
                    object[] values = new object[] { projectCode, tempState };
                    ydao.FillEntity(SqlManager.GetSqlStruct("TempRoomStructure", "SelectTempRoomStructureByProjectCode").SqlString, Params, values, entitydata, "TempRoomStructure");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetTempRoomStructureByRoomCode(string roomCode, string tempState)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("TempRoomStructure");
                using (SingleEntityDAO ydao = new SingleEntityDAO("TempRoomStructure"))
                {
                    string[] Params = new string[] { "@TempRoomCode", "@TempState" };
                    object[] values = new object[] { roomCode, tempState };
                    ydao.FillEntity(SqlManager.GetSqlStruct("TempRoomStructure", "SelectTempRoomStructureByRoomCode").SqlString, Params, values, entitydata, "TempRoomStructure");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_BuildingByCode(string BuildingCode)
        {
            EntityData data2;
            try
            {
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingStrategyName.BuildingCode, BuildingCode));
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Building", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_BuildingFloorProgressByBuildingFloorCode(string BuildingFloorCode)
        {
            EntityData data2;
            try
            {
                BuildingFloorProgressStrategyBuilder builder = new BuildingFloorProgressStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingFloorProgressStrategyName.BuildingFloorCode, BuildingFloorCode));
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("BuildingFloorProgress", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_BuildingFloorProgressByBuildingFloorWBSCode(string BuildingFloorCode, string WBSCode)
        {
            EntityData data2;
            try
            {
                BuildingFloorProgressStrategyBuilder builder = new BuildingFloorProgressStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingFloorProgressStrategyName.BuildingFloorCode, BuildingFloorCode));
                builder.AddStrategy(new Strategy(BuildingFloorProgressStrategyName.WBSCode, WBSCode));
                string queryString = builder.BuildQueryViewString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("BuildingFloorProgress", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_BuildingModelByBuildingCode(string BuildingCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("V_BuildingModel");
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_BuildingModel"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("V_BuildingModel", "SelectByBuildingCode").SqlString, "@BuildingCode", BuildingCode, entitydata, "V_BuildingModel");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_BuildingModelByBuildingFunctionCode(string BuildingFunctionCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("V_BuildingModel");
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_BuildingModel"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("V_BuildingModel", "SelectByBuildingFunctionCode").SqlString, "@BuildingFunctionCode", BuildingFunctionCode, entitydata, "V_BuildingModel");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_BuildingModelByBuildingModelCode(string BuildingModelCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("V_BuildingModel");
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_BuildingModel"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("V_BuildingModel", "SelectByBuildingModelCode").SqlString, "@BuildingModelCode", BuildingModelCode, entitydata, "V_BuildingModel");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_BuildingModelByBuildingStationCode(string BuildingStationCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("V_BuildingModel");
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_BuildingModel"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("V_BuildingModel", "SelectByBuildingStationCode").SqlString, "@BuildingStationCode", BuildingStationCode, entitydata, "V_BuildingModel");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_BuildingModelByModelCode(string ModelCode)
        {
            EntityData data2;
            try
            {
                EntityData entitydata = new EntityData("V_BuildingModel");
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_BuildingModel"))
                {
                    ydao.FillEntity(SqlManager.GetSqlStruct("V_BuildingModel", "SelectByModelCode").SqlString, "@ModelCode", ModelCode, entitydata, "V_BuildingModel");
                }
                data2 = entitydata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_ROOMByBuildingCode(string BuildingCode)
        {
            EntityData data2;
            try
            {
                RoomStrategyBuilder builder = new RoomStrategyBuilder("V_ROOM");
                builder.AddStrategy(new Strategy(RoomStrategyName.BuildingCode, BuildingCode));
                builder.AddOrder("ChamberCode", true);
                builder.AddOrder("FloorIndex", true);
                builder.AddOrder("RoomName", true);
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("V_ROOM", queryString);
                agent.Dispose();
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static EntityData GetV_ROOMByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data;
                using (SingleEntityDAO ydao = new SingleEntityDAO("V_ROOM"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                data2 = data;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data2;
        }

        public static void InsertBuilding(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Building"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertBuildingFloor(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingFloor"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertBuildingFloorProgress(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingFloorProgress"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertBuildingFunction(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingFunction"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertBuildingModel(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingModel"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertBuildingStation(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingStation"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertBuildingSubjectSet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingSubjectSet"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertChamber(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Chamber"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertConstructPlanStep(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructPlanStep"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertModel(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Model"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertPBS(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBS"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertPhotos(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Photos"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertRoom(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Room"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_Building(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Building"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_Model(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Model"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertStandard_TempRoomOut(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_TempRoomOut"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertTempRoomOut(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TempRoomOut"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertTempRoomStructure(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TempRoomStructure"))
                {
                    ydao.InsertEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SubmitAllBuilding(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Building"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllBuildingFloor(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingFloor"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllBuildingFloorProgress(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingFloorProgress"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllBuildingFunction(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingFunction"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllBuildingModel(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingModel"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllBuildingStation(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingStation"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllBuildingSubjectSet(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingSubjectSet"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllConstructPlanStep(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructPlanStep"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllRoom(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Room"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllStandard_Building(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Building"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllStandard_Model(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Model"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllStandard_TempRoomOut(EntityData entity)
        {
            Exception exception;
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_TempRoomOut"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllTempRoomOut(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TempRoomOut"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void SubmitAllTempRoomStructure(EntityData entity)
        {
            Exception exception;
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TempRoomStructure"))
                {
                    ydao.BeginTrans();
                    try
                    {
                        ydao.SubmitEntity(entity);
                        ydao.CommitTrans();
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ydao.RollBackTrans();
                        throw exception;
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                throw exception;
            }
        }

        public static void UpdateBuilding(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Building"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateBuildingFloor(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingFloor"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateBuildingFloorProgress(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingFloorProgress"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateBuildingFunction(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingFunction"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateBuildingModel(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingModel"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateBuildingStation(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingStation"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateBuildingSubjectSet(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("BuildingSubjectSet"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateChamber(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Chamber"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateConstructPlanStep(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("ConstructPlanStep"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateModel(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Model"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdatePBS(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("PBS"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdatePhotos(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Photos"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateRoom(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("Room"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_Building(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Building"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_Model(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_Model"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateStandard_TempRoomOut(EntityData entity)
        {
            try
            {
                using (StandardEntityDAO ydao = new StandardEntityDAO("Standard_TempRoomOut"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateTempRoomOut(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TempRoomOut"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateTempRoomStructure(EntityData entity)
        {
            try
            {
                using (SingleEntityDAO ydao = new SingleEntityDAO("TempRoomStructure"))
                {
                    ydao.UpdateEntity(entity);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

