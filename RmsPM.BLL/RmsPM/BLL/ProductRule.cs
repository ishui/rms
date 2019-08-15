namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public sealed class ProductRule
    {
        public static void CancelCheckTempRoomOut(string OutListCode, string User)
        {
            if (OutListCode != "")
            {
                Exception exception;
                try
                {
                    using (StandardEntityDAO ydao = new StandardEntityDAO("TempRoomOut"))
                    {
                        ydao.BeginTrans();
                        try
                        {
                            ydao.EntityName = "TempRoomOut";
                            EntityData entitydata = new EntityData("TempRoomOut");
                            entitydata = ydao.SelectbyPrimaryKey(OutListCode);
                            if (entitydata.HasRecord())
                            {
                                string text = entitydata.GetString("Out_State");
                                object obj2 = ConvertRule.ToDate(entitydata.GetDateTime("Out_Date"));
                                EntityData tempRoomStructureByOutListCode = ProductDAO.GetTempRoomStructureByOutListCode(OutListCode);
                                int count = tempRoomStructureByOutListCode.CurrentTable.Rows.Count;
                                for (int i = 0; i < count; i++)
                                {
                                    tempRoomStructureByOutListCode.SetCurrentRow(i);
                                    string keyvalues = tempRoomStructureByOutListCode.GetString("TempRoomCode");
                                    ydao.EntityName = "Room";
                                    EntityData data3 = new EntityData("Room");
                                    data3 = ydao.SelectbyPrimaryKey(keyvalues);
                                    if (!data3.HasRecord())
                                    {
                                        goto Label_0294;
                                    }
                                    DataRow currentRow = data3.CurrentRow;
                                    string text3 = text;
                                    if (text3 != null)
                                    {
                                        if (text3 != "入库")
                                        {
                                            if (text3 == "出库")
                                            {
                                                goto Label_019A;
                                            }
                                            if (text3 == "退库")
                                            {
                                                goto Label_0222;
                                            }
                                            if ((text3 != "预拨") && (text3 == "调拨"))
                                            {
                                                goto Label_0240;
                                            }
                                        }
                                        else
                                        {
                                            currentRow["InvState"] = GetRoomInvStateByIO(keyvalues, OutListCode, "");
                                            currentRow["InCode"] = DBNull.Value;
                                            currentRow["InDate"] = DBNull.Value;
                                        }
                                    }
                                    goto Label_028A;
                                Label_019A:
                                    currentRow["InvState"] = GetRoomInvStateByIO(keyvalues, OutListCode, "");
                                    currentRow["OutCode"] = DBNull.Value;
                                    currentRow["OutDate"] = DBNull.Value;
                                    currentRow["OutState"] = DBNull.Value;
                                    currentRow["OutAspect"] = DBNull.Value;
                                    currentRow["BofangCode"] = DBNull.Value;
                                    currentRow["BofangDate"] = DBNull.Value;
                                    goto Label_028A;
                                Label_0222:
                                    currentRow["InvState"] = GetRoomInvStateByIO(keyvalues, OutListCode, "");
                                    goto Label_028A;
                                Label_0240:
                                    currentRow["OutState"] = DBNull.Value;
                                    currentRow["OutAspect"] = DBNull.Value;
                                    currentRow["BofangCode"] = DBNull.Value;
                                    currentRow["BofangDate"] = DBNull.Value;
                                Label_028A:
                                    ydao.UpdateEntity(data3);
                                Label_0294:
                                    data3.Dispose();
                                }
                                DataRow row2 = entitydata.CurrentRow;
                                row2["CheckState"] = "0";
                                row2["CheckDate"] = DBNull.Value;
                                row2["CheckPerson"] = DBNull.Value;
                                ydao.UpdateEntity(entitydata);
                            }
                            entitydata.Dispose();
                            ydao.CommitTrans();
                        }
                        catch (Exception exception1)
                        {
                            exception = exception1;
                            try
                            {
                                ydao.RollBackTrans();
                            }
                            catch
                            {
                            }
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
        }

        public static string CanModifyBuildingStructure(string BuildingCode)
        {
            string text2;
            try
            {
                string text = "";
                if (IsBuildingIn(BuildingCode))
                {
                    return "楼栋已入库，不能再修改结构";
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string CanModifyRoomArea(string BuildingCode)
        {
            string text2;
            try
            {
                string text = "";
                if (IsBuildingIn(BuildingCode))
                {
                    return "楼栋已入库，不能再修改房源面积";
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static void CheckTempRoomOut(string OutListCode, string User)
        {
            if (OutListCode != "")
            {
                Exception exception;
                try
                {
                    using (StandardEntityDAO ydao = new StandardEntityDAO("TempRoomOut"))
                    {
                        ydao.BeginTrans();
                        try
                        {
                            ydao.EntityName = "TempRoomOut";
                            EntityData entitydata = new EntityData("TempRoomOut");
                            entitydata = ydao.SelectbyPrimaryKey(OutListCode);
                            if (entitydata.HasRecord())
                            {
                                string text = entitydata.GetString("Out_State");
                                string text2 = entitydata.GetString("OutAspect");
                                object obj2 = ConvertRule.ToDate(entitydata.GetDateTime("Out_Date"));
                                string minIODate = "";
                                if (obj2 != DBNull.Value)
                                {
                                    minIODate = ((DateTime) obj2).ToString("yyyy-MM-dd");
                                }
                                string text4 = "";
                                EntityData tempRoomStructureByOutListCode = ProductDAO.GetTempRoomStructureByOutListCode(OutListCode);
                                int count = tempRoomStructureByOutListCode.CurrentTable.Rows.Count;
                                for (int i = 0; i < count; i++)
                                {
                                    tempRoomStructureByOutListCode.SetCurrentRow(i);
                                    string keyvalues = tempRoomStructureByOutListCode.GetString("TempRoomCode");
                                    string code = tempRoomStructureByOutListCode.GetString("TempBuildingCode");
                                    if ((text == "预拨") && (text2 != ""))
                                    {
                                        EntityData entity = ProductDAO.GetBuildingByCode(code);
                                        if (entity.HasRecord() && (entity.GetString("Whither") != text2))
                                        {
                                            entity.CurrentRow["Whither"] = text2;
                                            ProductDAO.UpdateBuilding(entity);
                                        }
                                        entity.Dispose();
                                    }
                                    ydao.EntityName = "Room";
                                    EntityData data4 = new EntityData("Room");
                                    data4 = ydao.SelectbyPrimaryKey(keyvalues);
                                    if (!data4.HasRecord())
                                    {
                                        goto Label_03CE;
                                    }
                                    DataRow currentRow = data4.CurrentRow;
                                    string text7 = text;
                                    if (text7 != null)
                                    {
                                        if (text7 != "入库")
                                        {
                                            if (text7 == "出库")
                                            {
                                                goto Label_0292;
                                            }
                                            if (text7 == "退库")
                                            {
                                                goto Label_033B;
                                            }
                                            if ((text7 != "预拨") && (text7 == "调拨"))
                                            {
                                                goto Label_037F;
                                            }
                                        }
                                        else
                                        {
                                            text4 = GetRoomInvStateByIO(keyvalues, OutListCode, minIODate);
                                            if (text4 != "")
                                            {
                                                currentRow["InvState"] = text4;
                                            }
                                            else
                                            {
                                                currentRow["InvState"] = text;
                                            }
                                            currentRow["InCode"] = OutListCode;
                                            currentRow["InDate"] = obj2;
                                        }
                                    }
                                    goto Label_03C4;
                                Label_0292:
                                    text4 = GetRoomInvStateByIO(keyvalues, OutListCode, minIODate);
                                    if (text4 != "")
                                    {
                                        currentRow["InvState"] = text4;
                                    }
                                    else
                                    {
                                        currentRow["InvState"] = text;
                                    }
                                    currentRow["OutCode"] = OutListCode;
                                    currentRow["OutDate"] = obj2;
                                    currentRow["OutState"] = "调拨";
                                    currentRow["OutAspect"] = entitydata.GetString("OutAspect");
                                    currentRow["BofangCode"] = OutListCode;
                                    currentRow["BofangDate"] = obj2;
                                    goto Label_03C4;
                                Label_033B:
                                    text4 = GetRoomInvStateByIO(keyvalues, OutListCode, minIODate);
                                    if (text4 != "")
                                    {
                                        currentRow["InvState"] = text4;
                                    }
                                    else
                                    {
                                        currentRow["InvState"] = text;
                                    }
                                    goto Label_03C4;
                                Label_037F:
                                    currentRow["OutState"] = text;
                                    currentRow["OutAspect"] = entitydata.GetString("OutAspect");
                                    currentRow["BofangCode"] = OutListCode;
                                    currentRow["BofangDate"] = obj2;
                                Label_03C4:
                                    ydao.UpdateEntity(data4);
                                Label_03CE:
                                    data4.Dispose();
                                }
                                DataRow row3 = entitydata.CurrentRow;
                                row3["CheckState"] = "1";
                                row3["CheckDate"] = DateTime.Today;
                                row3["CheckPerson"] = User;
                                ydao.UpdateEntity(entitydata);
                            }
                            entitydata.Dispose();
                            ydao.CommitTrans();
                        }
                        catch (Exception exception1)
                        {
                            exception = exception1;
                            try
                            {
                                ydao.RollBackTrans();
                            }
                            catch
                            {
                            }
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
        }

        public static void DeleteBuilding(string BuildingCode)
        {
            if (BuildingCode != "")
            {
                try
                {
                    EntityData entity = ProductDAO.GetBuildingByParentCode(BuildingCode);
                    if (entity.HasRecord())
                    {
                        throw new Exception("该区域下有楼栋，不能删除");
                    }
                    entity.Dispose();
                    QueryAgent agent = new QueryAgent();
                    try
                    {
                        string queryString = string.Format("select top 1 1 from CostBudgetSet where PBSType = 'B' and PBSCode = '{0}'", BuildingCode);
                        if (ConvertRule.ToString(agent.ExecuteScalar(queryString)) != "")
                        {
                            throw new Exception("楼栋已添加到预算设置表，不能删除");
                        }
                    }
                    finally
                    {
                        agent.Dispose();
                    }
                    entity = ProductDAO.GetStandard_BuildingByCode(BuildingCode);
                    ProductDAO.DeleteStandard_Building(entity);
                    entity.Dispose();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        public static void DeleteBuildingStructure(string BuildingCode)
        {
            if (BuildingCode != "")
            {
                try
                {
                    EntityData entity = ProductDAO.GetRoomByBuildingCode(BuildingCode);
                    if (entity.HasRecord())
                    {
                        ProductDAO.DeleteRoom(entity);
                    }
                    entity.Dispose();
                    EntityData data2 = ProductDAO.getChamberByBuildingCode(BuildingCode);
                    if (data2.HasRecord())
                    {
                        ProductDAO.DeleteChamber(data2);
                    }
                    data2.Dispose();
                    entity = ProductDAO.GetBuildingByCode(BuildingCode);
                    if (entity.HasRecord())
                    {
                        DataRow currentRow = entity.CurrentRow;
                        currentRow["FloorList"] = "";
                        currentRow["Room_List"] = "";
                        currentRow["RoomArea"] = 0;
                        ProductDAO.UpdateBuilding(entity);
                    }
                    entity.Dispose();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        public static void DeleteModel(string ModelCode)
        {
            try
            {
                EntityData entity = ProductDAO.GetStandard_ModelByCode(ModelCode);
                if (0 < entity.Tables["BuildingModel"].Rows.Count)
                {
                    throw new Exception("该户型有楼栋正在使用，不能删除");
                }
                if (0 < entity.Tables["Model"].Rows.Count)
                {
                    string code = entity.Tables["Model"].Rows[0]["ImageCode"].ToString();
                    if ("" != code)
                    {
                        EntityData photosByCode = ProductDAO.GetPhotosByCode(code);
                        if (photosByCode.HasRecord())
                        {
                            ProductDAO.DeletePhotos(photosByCode);
                        }
                        photosByCode.Dispose();
                    }
                }
                ProductDAO.DeleteStandard_Model(entity);
                entity.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DeleteTempRoomOut(string OutListCode)
        {
            if (OutListCode != "")
            {
                try
                {
                    EntityData entity = ProductDAO.GetStandard_TempRoomOutByCode(OutListCode);
                    ProductDAO.DeleteStandard_TempRoomOut(entity);
                    entity.Dispose();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        public static string GenerateOutListName(string ProjectCode, string PBSTypeCode, string aState, int year, string OldCode, ref int SumNo)
        {
            string text6;
            try
            {
                string text = "";
                int num = 1;
                QueryAgent agent = new QueryAgent();
                string pBSTypeName = PBSRule.GetPBSTypeName(PBSTypeCode);
                if (pBSTypeName == "")
                {
                    throw new Exception("未找到产品组合");
                }
                string text3 = aState.Substring(0, 1);
                string text4 = pBSTypeName + "(" + text3 + ")<" + year.ToString() + ">";
                try
                {
                    string queryString = string.Format("select max(SumNo) as SumNo from TempRoomOut where OutListName like '{0}%' and OutListName <> '{1}'", text4, OldCode);
                    object obj2 = agent.ExecuteScalar(queryString);
                    if ((obj2 != null) && (obj2 != DBNull.Value))
                    {
                        num = int.Parse(obj2.ToString()) + 1;
                    }
                }
                finally
                {
                    agent.Dispose();
                }
                text = text4 + num.ToString();
                SumNo = num;
                text6 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text6;
        }

        public static string GetAllBuildingNameByCode(object BuildingCode)
        {
            string text2;
            try
            {
                string text = "";
                if ((BuildingCode != null) && (BuildingCode.ToString() != ""))
                {
                    QueryAgent agent = new QueryAgent();
                    try
                    {
                        object obj2 = agent.ExecuteScalar(string.Format("select dbo.getAllBuildingName('{0}')", BuildingCode));
                        if ((obj2 != null) && (obj2 != DBNull.Value))
                        {
                            text = obj2.ToString();
                        }
                    }
                    finally
                    {
                        agent.Dispose();
                    }
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static decimal GetBuildingArea(string buildingCode, string projectCode)
        {
            decimal num7;
            try
            {
                decimal num = 0M;
                EntityData buildingByProjectCode = ProductDAO.GetBuildingByProjectCode(projectCode);
                string text = "";
                DataRow[] rowArray = buildingByProjectCode.CurrentTable.Select(string.Format("BuildingCode='{0}'", buildingCode));
                if (rowArray.Length > 0)
                {
                    text = (string) rowArray[0]["FullID"];
                }
                DataRow[] rowArray2 = buildingByProjectCode.CurrentTable.Select(string.Format(" FullID like '{0}%' ", text));
                int length = rowArray2.Length;
                for (int i = 0; i < length; i++)
                {
                    decimal num4 = 0M;
                    decimal num5 = 0M;
                    decimal num6 = 0M;
                    if (!rowArray2[i].IsNull("HouseArea"))
                    {
                        num4 = (decimal) rowArray2[i]["HouseArea"];
                    }
                    if (!rowArray2[i].IsNull("toBuildArea"))
                    {
                        num5 = (decimal) rowArray2[i]["toBuildArea"];
                    }
                    if (!rowArray2[i].IsNull("otherArea"))
                    {
                        num6 = (decimal) rowArray2[i]["otherArea"];
                    }
                    num += (num4 + num5) + num6;
                }
                buildingByProjectCode.Dispose();
                num7 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num7;
        }

        public static DataTable GetBuildingAreaField()
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable("BuildingAreaField");
                table.Columns.Add("FieldName");
                table.Columns.Add("FieldDesc");
                table.Rows.Add(new object[] { "HouseArea", "计划面积" });
                table.Rows.Add(new object[] { "RoomArea", "实测面积" });
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static string GetBuildingAreaFieldDesc(string FieldName)
        {
            string text2;
            try
            {
                string text = "";
                DataRow[] rowArray = GetBuildingAreaField().Select("FieldName = '" + FieldName + "'");
                if (rowArray.Length > 0)
                {
                    text = ConvertRule.ToString(rowArray[0]["FieldDesc"]);
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static int GetBuildingChildCount(string BuildingCode)
        {
            int num2;
            try
            {
                int num = 0;
                if ((BuildingCode != null) && (BuildingCode.ToString() != ""))
                {
                    QueryAgent agent = new QueryAgent();
                    try
                    {
                        object obj2 = agent.ExecuteScalar(string.Format("select count(*) from Building where ParentCode = '{0}'", BuildingCode));
                        if ((obj2 != null) && (obj2 != DBNull.Value))
                        {
                            num = int.Parse(obj2.ToString());
                        }
                    }
                    finally
                    {
                        agent.Dispose();
                    }
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static string GetBuildingColor(string IsArea)
        {
            string text2;
            try
            {
                string text = "";
                string text3 = IsArea;
                if ((text3 != null) && (text3 == "1"))
                {
                    text = "yellow";
                }
                else
                {
                    text = "#F3F5F8";
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetBuildingFunctionName(object BuildingFunctionCode)
        {
            string text3;
            try
            {
                string text = "";
                EntityData buildingFunctionByCode = ProductDAO.GetBuildingFunctionByCode(BuildingFunctionCode.ToString());
                if (buildingFunctionByCode.HasRecord())
                {
                    text = buildingFunctionByCode.GetString("FunctionName");
                }
                buildingFunctionByCode.Dispose();
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static DataTable GetBuildingLocationLegend(DataTable dtBuilding)
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("State", typeof(string));
                table.Columns.Add("Count", typeof(int));
                table.Columns.Add("Color", typeof(string));
                int num = 0;
                int num2 = 0;
                foreach (DataRow row in dtBuilding.Rows)
                {
                    string text = row["BuildingCode"].ToString();
                    if (ConvertRule.ToString(row["IsArea"]) == "1")
                    {
                        num++;
                    }
                    else
                    {
                        num2++;
                    }
                }
                DataRow row2 = table.NewRow();
                row2["State"] = "区域";
                row2["Count"] = num;
                row2["Color"] = GetBuildingColor("1");
                table.Rows.Add(row2);
                row2 = table.NewRow();
                row2["State"] = "楼栋";
                row2["Count"] = num2;
                row2["Color"] = GetBuildingColor("2");
                table.Rows.Add(row2);
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static string GetBuildingName(object BuildingCode)
        {
            string text2;
            try
            {
                string text = "";
                if ((BuildingCode != null) && (BuildingCode.ToString() != ""))
                {
                    EntityData buildingByCode = ProductDAO.GetBuildingByCode(BuildingCode.ToString());
                    if (buildingByCode.HasRecord())
                    {
                        text = buildingByCode.GetString("BuildingName");
                    }
                    buildingByCode.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetBuildingNameByPBSUnit(string PBSUnitCode)
        {
            string text2;
            try
            {
                string text = "";
                if (PBSUnitCode == "")
                {
                    return text;
                }
                EntityData buildingByPBSUnitCode = ProductDAO.GetBuildingByPBSUnitCode(PBSUnitCode);
                text = ConvertRule.Concat(buildingByPBSUnitCode.CurrentTable, "BuildingName", ",");
                buildingByPBSUnitCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetBuildingParentCode(object BuildingCode)
        {
            string text2;
            try
            {
                string text = "";
                if ((BuildingCode != null) && (BuildingCode.ToString() != ""))
                {
                    EntityData buildingByCode = ProductDAO.GetBuildingByCode(BuildingCode.ToString());
                    if (buildingByCode.HasRecord())
                    {
                        text = buildingByCode.GetString("ParentCode");
                    }
                    buildingByCode.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetBuildingShortName(object BuildingCode)
        {
            string text2;
            try
            {
                string text = "";
                if ((BuildingCode != null) && (BuildingCode.ToString() != ""))
                {
                    EntityData buildingByCode = ProductDAO.GetBuildingByCode(BuildingCode.ToString());
                    if (buildingByCode.HasRecord())
                    {
                        text = buildingByCode.GetString("BuildingShortName");
                    }
                    buildingByCode.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetBuildingStationName(object BuildingStationCode)
        {
            string text3;
            try
            {
                string text = "";
                EntityData buildingStationByCode = ProductDAO.GetBuildingStationByCode(BuildingStationCode.ToString());
                if (buildingStationByCode.HasRecord())
                {
                    text = buildingStationByCode.GetString("StationName");
                }
                buildingStationByCode.Dispose();
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static decimal GetBuildingTotalRoomArea(string BuildingCode)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                if ((BuildingCode != null) && (BuildingCode.ToString() != ""))
                {
                    QueryAgent agent = new QueryAgent();
                    try
                    {
                        object val = agent.ExecuteScalar(string.Format("select sum(isnull(BuildArea, 0)) as BuildArea from Room where BuildingCode = '{0}'", BuildingCode));
                        if ((val != null) && (val != DBNull.Value))
                        {
                            num = ConvertRule.ToDecimal(val);
                        }
                    }
                    finally
                    {
                        agent.Dispose();
                    }
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static string GetChamberNameByCode(string chamberCode)
        {
            string text2;
            try
            {
                string text = "";
                EntityData chamberByCode = ProductDAO.GetChamberByCode(chamberCode);
                if (chamberByCode.HasRecord())
                {
                    text = chamberByCode.GetString("ChamberName");
                }
                chamberByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetFloorNameByBuildingFloorIndex(object BuildingCode, object FloorIndex)
        {
            string text2;
            try
            {
                string text = "";
                if ((((BuildingCode != null) && (BuildingCode.ToString() != "")) && (FloorIndex != null)) && (FloorIndex.ToString() != ""))
                {
                    QueryAgent agent = new QueryAgent();
                    try
                    {
                        object obj2 = agent.ExecuteScalar(string.Format("select top 1 FloorName from room where BuildingCode = '{0}' and FloorIndex = '{1}'", BuildingCode, FloorIndex));
                        if ((obj2 != null) && (obj2 != DBNull.Value))
                        {
                            text = obj2.ToString();
                        }
                    }
                    finally
                    {
                        agent.Dispose();
                    }
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetPBSUnitName(string pbsUnitCode)
        {
            string text2;
            try
            {
                string text = "";
                if (pbsUnitCode == "")
                {
                    return text;
                }
                EntityData pBSUnitByCode = PBSDAO.GetPBSUnitByCode(pbsUnitCode);
                if (pBSUnitByCode.HasRecord())
                {
                    text = pBSUnitByCode.GetString("PBSUnitName");
                }
                pBSUnitByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetProjectCodeFromBuilding(object BuildingCode)
        {
            string text2;
            try
            {
                string text = "";
                if ((BuildingCode != null) && (BuildingCode.ToString() != ""))
                {
                    EntityData buildingByCode = ProductDAO.GetBuildingByCode(BuildingCode.ToString());
                    if (buildingByCode.HasRecord())
                    {
                        text = buildingByCode.GetString("ProjectCode");
                    }
                    buildingByCode.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetRoomCodeByChamberRoomName(string ChamberName, string RoomName, string ProjectCode)
        {
            string text3;
            try
            {
                string text = "";
                if (((ChamberName == "") || (RoomName == "")) || (ProjectCode == ""))
                {
                    return text;
                }
                EntityData data = ProductDAO.GetRoomByChamberRoomName(ChamberName, RoomName, ProjectCode);
                data.Dispose();
                if (data.HasRecord())
                {
                    text = data.GetString("RoomCode");
                }
                else if (RoomName.Substring(0, 1) == "0")
                {
                    string roomName = RoomName.Substring(1);
                    data = ProductDAO.GetRoomByChamberRoomName(ChamberName, roomName, ProjectCode);
                    data.Dispose();
                    if (data.HasRecord())
                    {
                        text = data.GetString("RoomCode");
                    }
                }
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetRoomColor(DataRow dr)
        {
            string roomColor;
            try
            {
                if (dr == null)
                {
                    return "";
                }
                string invState = ConvertRule.ToString(dr["InvState"]);
                string salState = ConvertRule.ToString(dr["SalState"]);
                roomColor = GetRoomColor(invState, salState);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return roomColor;
        }

        public static string GetRoomColor(string InvState, string SalState)
        {
            string text2;
            try
            {
                string text = "";
                string text3 = SalState;
                if ((text3 != null) && (text3 == "已售"))
                {
                    text = "red";
                }
                else
                {
                    text3 = InvState;
                    if (text3 == null)
                    {
                        goto Label_0064;
                    }
                    if (text3 != "入库")
                    {
                        if (text3 == "出库")
                        {
                            goto Label_005A;
                        }
                        if (text3 != "退库")
                        {
                            goto Label_0064;
                        }
                    }
                    text = "#9AC87C";
                }
                goto Label_006E;
            Label_005A:
                text = "yellow";
                goto Label_006E;
            Label_0064:
                text = "#d2dbe6";
            Label_006E:
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static DataTable GetRoomCountGroupByState(EntityData entity)
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("State", typeof(string));
                table.Columns.Add("InvState", typeof(string));
                table.Columns.Add("SalState", typeof(string));
                table.Columns.Add("Count", typeof(int));
                table.Columns.Add("Color", typeof(string));
                int num = 0;
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                foreach (DataRow row in entity.CurrentTable.Rows)
                {
                    string text = ConvertRule.ToString(row["InvState"]);
                    string text3 = ConvertRule.ToString(row["SalState"]);
                    if ((text3 != null) && (text3 == "已售"))
                    {
                        num4++;
                    }
                    else
                    {
                        text3 = text;
                        if (text3 == null)
                        {
                            goto Label_0147;
                        }
                        if (text3 != "入库")
                        {
                            if (text3 == "出库")
                            {
                                goto Label_013F;
                            }
                            if (text3 != "退库")
                            {
                                goto Label_0147;
                            }
                        }
                        num2++;
                    }
                    goto Label_014F;
                Label_013F:
                    num3++;
                    goto Label_014F;
                Label_0147:
                    num++;
                Label_014F:;
                }
                DataRow row2 = table.NewRow();
                row2["State"] = "未入库";
                row2["InvState"] = row2["State"];
                row2["SalState"] = "";
                row2["Count"] = num;
                row2["Color"] = GetRoomColor(row2["InvState"].ToString(), row2["SalState"].ToString());
                table.Rows.Add(row2);
                row2 = table.NewRow();
                row2["State"] = "入库";
                row2["InvState"] = row2["State"];
                row2["SalState"] = "";
                row2["Count"] = num2;
                row2["Color"] = GetRoomColor(row2["InvState"].ToString(), row2["SalState"].ToString());
                table.Rows.Add(row2);
                row2 = table.NewRow();
                row2["State"] = "出库";
                row2["InvState"] = row2["State"];
                row2["SalState"] = "";
                row2["Count"] = num3;
                row2["Color"] = GetRoomColor(row2["InvState"].ToString(), row2["SalState"].ToString());
                table.Rows.Add(row2);
                row2 = table.NewRow();
                row2["State"] = "已售";
                row2["InvState"] = "";
                row2["SalState"] = "已售";
                row2["Count"] = num4;
                row2["Color"] = GetRoomColor(row2["InvState"].ToString(), row2["SalState"].ToString());
                table.Rows.Add(row2);
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static string GetRoomInBuildingName(object objOutListCode)
        {
            string text3;
            try
            {
                string text = "";
                string code = ConvertRule.ToString(objOutListCode);
                if (code == "")
                {
                    return text;
                }
                EntityData buildingByOutListCode = ProductDAO.GetBuildingByOutListCode(code);
                text = ConvertRule.GetDistinctStr(buildingByOutListCode.CurrentTable, "BuildingName", "", ",");
                buildingByOutListCode.Dispose();
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetRoomInvStateByIO(string RoomCode)
        {
            string text3;
            try
            {
                string text = "";
                QueryAgent agent = new QueryAgent();
                string format = "select top 1 out_state from v_TempRoomStructure where TempRoomCode = '{0}' and CheckState > 1 order by out_date desc";
                format = string.Format(format, RoomCode);
                text = ConvertRule.ToString(agent.ExecuteScalar(format));
                agent.Dispose();
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetRoomInvStateByIO(string RoomCode, string ExceptOutListCode, string MinIODate)
        {
            string text3;
            try
            {
                string text = "";
                QueryAgent agent = new QueryAgent();
                string queryString = "select top 1 out_state from v_TempRoomStructure where TempRoomCode = '" + RoomCode + "' and OutListCode <> '" + ExceptOutListCode + "'";
                if (MinIODate != "")
                {
                    queryString = queryString + " and Out_Date > convert(datetime, '" + MinIODate + "', 121)";
                }
                queryString = queryString + " and CheckState >= 1 order by out_date desc";
                text = ConvertRule.ToString(agent.ExecuteScalar(queryString));
                agent.Dispose();
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetTempRoomOutCheckStateName(object CheckState)
        {
            string text2;
            try
            {
                string text = "";
                if (ConvertRule.ToInt(CheckState) == 1)
                {
                    text = "已审";
                }
                else
                {
                    text = "未审";
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static bool IsBuildingIn(string BuildingCode)
        {
            bool flag2;
            try
            {
                bool flag = false;
                string projectCodeFromBuilding = GetProjectCodeFromBuilding(BuildingCode);
                RoomStrategyBuilder builder = new RoomStrategyBuilder();
                builder.AddStrategy(new Strategy(RoomStrategyName.ProjectCode, projectCodeFromBuilding));
                builder.AddStrategy(new Strategy(RoomStrategyName.InBuildingCode, BuildingCode));
                builder.AddStrategy(new Strategy(RoomStrategyName.InInvState, "入库,出库,退库"));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                agent.SetTopNumber(1);
                DataTable table = agent.ExecSqlForDataSet(queryString).Tables[0];
                if (table.Rows.Count > 0)
                {
                    flag = true;
                }
                agent.Dispose();
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static bool IsBuildingNameExists(string name, string code, string ProjectCode, string ParentCode)
        {
            bool flag2;
            try
            {
                bool flag = false;
                BuildingStrategyBuilder builder = new BuildingStrategyBuilder();
                builder.AddStrategy(new Strategy(BuildingStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(BuildingStrategyName.BuildingName, name));
                builder.AddStrategy(new Strategy(BuildingStrategyName.ParentCode, ParentCode));
                builder.AddStrategy(new Strategy(BuildingStrategyName.BuildingCodeNot, code));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Building", queryString);
                agent.Dispose();
                if (data.HasRecord())
                {
                    flag = true;
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static bool IsChamberNameExists(string name, string code, string ProjectCode)
        {
            bool flag2;
            try
            {
                bool flag = false;
                ChamberStrategyBuilder builder = new ChamberStrategyBuilder();
                builder.AddStrategy(new Strategy(ChamberStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(ChamberStrategyName.ChamberName, name));
                builder.AddStrategy(new Strategy(ChamberStrategyName.ChamberCodeNot, code));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Chamber", queryString);
                agent.Dispose();
                if (data.HasRecord())
                {
                    flag = true;
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static bool IsModelNameExists(string name, string code, string ProjectCode)
        {
            bool flag2;
            try
            {
                bool flag = false;
                ModelStrategyBuilder builder = new ModelStrategyBuilder();
                builder.AddStrategy(new Strategy(ModelStrategyName.ProjectCode, ProjectCode));
                builder.AddStrategy(new Strategy(ModelStrategyName.ModelName, name));
                builder.AddStrategy(new Strategy(ModelStrategyName.ModelCodeNot, code));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Model", queryString);
                agent.Dispose();
                if (data.HasRecord())
                {
                    flag = true;
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static string TransTempRoomOutIOType(string val, bool isToName)
        {
            string text2;
            try
            {
                int index;
                string text = "";
                string[] textArray = new string[] { "1", "2", "3", "4" };
                string[] textArray2 = new string[] { "入库", "出库", "退库", "预拨" };
                if (isToName)
                {
                    for (index = 0; index < textArray.Length; index++)
                    {
                        if (textArray[index].ToString() == val)
                        {
                            return textArray2[index];
                        }
                    }
                }
                else
                {
                    for (index = 0; index < textArray2.Length; index++)
                    {
                        if (textArray2[index].ToString() == val)
                        {
                            return textArray[index];
                        }
                    }
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static void UpdateBuildingTotalRoomArea(DataTable tbBuilding)
        {
            try
            {
                foreach (DataRow row in tbBuilding.Rows)
                {
                    UpdateBuildingTotalRoomArea(ConvertRule.ToString(row["BuildingCode"]));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateBuildingTotalRoomArea(string[] arrBuildingCode)
        {
            try
            {
                foreach (string text in arrBuildingCode)
                {
                    UpdateBuildingTotalRoomArea(text);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpdateBuildingTotalRoomArea(string BuildingCode)
        {
            try
            {
                if (BuildingCode != "")
                {
                    EntityData entity = ProductDAO.GetBuildingByCode(BuildingCode);
                    if (entity.HasRecord())
                    {
                        DataRow currentRow = entity.CurrentRow;
                        decimal buildingTotalRoomArea = GetBuildingTotalRoomArea(BuildingCode);
                        if (buildingTotalRoomArea != ConvertRule.ToDecimal(currentRow["RoomArea"]))
                        {
                            currentRow["RoomArea"] = buildingTotalRoomArea;
                            ProductDAO.UpdateBuilding(entity);
                        }
                    }
                    entity.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}

