namespace RmsPM.BLL
{
    using System;
    using System.Collections;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public class CostRule
    {
        private static void AddApportion(string projectCode, DataTable dt, string alloType, string buildingCode, decimal money, EntityData buildings, EntityData PBSUnits, string AreaField)
        {
            string buildingName = "";
            decimal buildingArea = 0M;
            string alloTypeName = GetAlloTypeName(alloType);
            GetBuildingInfo(alloType, buildingCode, ref buildingName, ref buildingArea, PBSUnits, buildings, AreaField);
            DataRow[] rowArray = dt.Select(string.Format(" AlloType='{0}' and BuildingCode='{1}' ", alloType, buildingCode));
            DataRow row = null;
            if (rowArray.Length > 0)
            {
                row = rowArray[0];
                row["ApportionMoney"] = ConvertRule.ToDecimal(row["ApportionMoney"]) + money;
            }
            else
            {
                row = dt.NewRow();
                row["AlloType"] = alloType;
                row["BuildingName"] = buildingName;
                row["BuildingArea"] = buildingArea;
                if (alloType == "P")
                {
                    row["SortID"] = 0;
                }
                else if (alloType == "U")
                {
                    row["SortID"] = 1;
                }
                else if (alloType == "B")
                {
                    row["SortID"] = 2;
                }
                row["AlloTypeName"] = alloTypeName;
                row["BuildingCode"] = buildingCode;
                row["ApportionMoney"] = money;
                dt.Rows.Add(row);
            }
        }

        private static void AddApportion(string projectCode, DataTable dt, string alloType, string buildingCode, decimal money, decimal totalArea, EntityData buildings, EntityData PBSUnits, string AreaField)
        {
            string buildingName = "";
            decimal buildingArea = 0M;
            string alloTypeName = GetAlloTypeName(alloType);
            decimal num2 = 0M;
            GetBuildingInfo(alloType, buildingCode, ref buildingName, ref buildingArea, PBSUnits, buildings, AreaField);
            if (totalArea > 0M)
            {
                num2 = Math.Round((decimal) ((money * buildingArea) / totalArea), 2);
            }
            DataRow[] rowArray = dt.Select(string.Format(" AlloType='{0}' and BuildingCode='{1}' ", alloType, buildingCode));
            DataRow row = null;
            if (rowArray.Length > 0)
            {
                row = rowArray[0];
                row["ApportionMoney"] = ConvertRule.ToDecimal(row["ApportionMoney"]) + num2;
            }
            else
            {
                row = dt.NewRow();
                row["BuildingName"] = buildingName;
                row["BuildingArea"] = buildingArea;
                if (alloType == "P")
                {
                    row["SortID"] = 0;
                    row["BuildingName"] = ProjectRule.GetProjectName(projectCode);
                }
                else if (alloType == "U")
                {
                    row["SortID"] = 1;
                }
                else if (alloType == "B")
                {
                    row["SortID"] = 2;
                }
                row["AlloType"] = alloType;
                row["AlloTypeName"] = alloTypeName;
                row["BuildingCode"] = buildingCode;
                row["ApportionMoney"] = num2;
                dt.Rows.Add(row);
            }
        }

        private static void AddBuildingCostApportion(string projectCode, DataTable dt, string alloType, string buildingCode, decimal money, EntityData buildings, EntityData PBSUnits, EntityData CBS, string subjectCode, string AreaField)
        {
            string buildingName = "";
            decimal buildingArea = 0M;
            string alloTypeName = GetAlloTypeName(alloType);
            GetBuildingInfo(alloType, buildingCode, ref buildingName, ref buildingArea, PBSUnits, buildings, AreaField);
            string costCode = "";
            string costName = "";
            string fullCode = "";
            string costSortID = "";
            GetCostDetail(subjectCode, CBS, ref costCode, ref costName, ref fullCode, ref costSortID);
            DataRow[] rowArray = dt.Select(string.Format(" AlloType='{0}' and BuildingCode='{1}' and costCode='{2}' ", alloType, buildingCode, costCode));
            DataRow row = null;
            if (rowArray.Length > 0)
            {
                row = rowArray[0];
                row["ApportionMoney"] = ConvertRule.ToDecimal(row["ApportionMoney"]) + money;
            }
            else
            {
                row = dt.NewRow();
                row["AlloType"] = alloType;
                row["BuildingName"] = buildingName;
                row["BuildingArea"] = buildingArea;
                if (alloType == "P")
                {
                    row["SortID"] = 0;
                    row["BuildingName"] = ProjectRule.GetProjectName(projectCode);
                }
                else if (alloType == "U")
                {
                    row["SortID"] = 1;
                }
                else if (alloType == "B")
                {
                    row["SortID"] = 2;
                }
                row["AlloTypeName"] = alloTypeName;
                row["BuildingCode"] = buildingCode;
                row["ApportionMoney"] = money;
                row["CostCode"] = costCode;
                row["CostName"] = costName;
                row["FullCode"] = fullCode;
                row["CostSortID"] = costSortID;
                dt.Rows.Add(row);
            }
        }

        private static void AddBuildingCostApportion(string projectCode, DataTable dt, string alloType, string buildingCode, decimal money, decimal totalArea, EntityData buildings, EntityData PBSUnits, EntityData CBS, string subjectCode, string AreaField)
        {
            string buildingName = "";
            decimal buildingArea = 0M;
            string alloTypeName = GetAlloTypeName(alloType);
            decimal num2 = 0M;
            GetBuildingInfo(alloType, buildingCode, ref buildingName, ref buildingArea, PBSUnits, buildings, AreaField);
            string costCode = "";
            string costName = "";
            string fullCode = "";
            string costSortID = "";
            GetCostDetail(subjectCode, CBS, ref costCode, ref costName, ref fullCode, ref costSortID);
            if (totalArea > 0M)
            {
                num2 = Math.Round((decimal) ((money * buildingArea) / totalArea), 2);
            }
            DataRow[] rowArray = dt.Select(string.Format(" AlloType='{0}' and BuildingCode='{1}' and costCode='{2}' ", alloType, buildingCode, costCode));
            DataRow row = null;
            if (rowArray.Length > 0)
            {
                row = rowArray[0];
                row["ApportionMoney"] = ConvertRule.ToDecimal(row["ApportionMoney"]) + num2;
            }
            else
            {
                row = dt.NewRow();
                row["BuildingName"] = buildingName;
                row["BuildingArea"] = buildingArea;
                if (alloType == "P")
                {
                    row["SortID"] = 0;
                    row["BuildingName"] = ProjectRule.GetProjectName(projectCode);
                }
                else if (alloType == "U")
                {
                    row["SortID"] = 1;
                }
                else if (alloType == "B")
                {
                    row["SortID"] = 2;
                }
                row["AlloType"] = alloType;
                row["AlloTypeName"] = alloTypeName;
                row["BuildingCode"] = buildingCode;
                row["ApportionMoney"] = num2;
                row["CostCode"] = costCode;
                row["CostName"] = costName;
                row["FullCode"] = fullCode;
                row["CostSortID"] = costSortID;
                dt.Rows.Add(row);
            }
        }

        public static DataTable ApportionAllPayout(string projectCode, string AreaField)
        {
            DataTable table2;
            try
            {
                EntityData buildings = ProductDAO.GetBuildingNotAreaByProjectCode(projectCode);
                EntityData pBSUnits = PBSDAO.GetPBSUnitByProject(projectCode);
                DataTable dtApportion = BuildApportionTable();
                EntityData payoutByProjectCode = PaymentDAO.GetPayoutByProjectCode(projectCode);
                foreach (DataRow row in payoutByProjectCode.CurrentTable.Rows)
                {
                    string payoutCode = ConvertRule.ToString(row["PayoutCode"]);
                    ApportionOnePayout(projectCode, payoutCode, pBSUnits, buildings, dtApportion, AreaField);
                }
                payoutByProjectCode.Dispose();
                pBSUnits.Dispose();
                buildings.Dispose();
                table2 = dtApportion;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static DataTable ApportionOnePayout(string projectCode, string payoutCode, string AreaField)
        {
            DataTable table2;
            try
            {
                EntityData buildings = ProductDAO.GetBuildingNotAreaByProjectCode(projectCode);
                EntityData pBSUnits = PBSDAO.GetAllPBSUnit();
                DataTable dtApportion = BuildApportionTable();
                ApportionOnePayout(projectCode, payoutCode, pBSUnits, buildings, dtApportion, AreaField);
                pBSUnits.Dispose();
                buildings.Dispose();
                table2 = dtApportion;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        private static void ApportionOnePayout(string projectCode, string payoutCode, EntityData PBSUnits, EntityData buildings, DataTable dtApportion, string AreaField)
        {
            try
            {
                DataTable dt = BuildApportionTable();
                EntityData data = PaymentDAO.GetStandard_PayoutByCode(payoutCode);
                foreach (DataRow row in data.Tables["PayoutItem"].Rows)
                {
                    dt.Clear();
                    string alloType = ConvertRule.ToString(row["AlloType"]);
                    string text2 = ConvertRule.ToString(row["PayoutItemCode"]);
                    int num = ConvertRule.ToInt(row["IsManualAlloc"]);
                    decimal money = ConvertRule.ToDecimal(row["PayoutMoney"]);
                    switch (alloType)
                    {
                        case "P":
                            AddApportion(projectCode, dt, alloType, projectCode, money, buildings, PBSUnits, AreaField);
                            break;

                        case "U":
                        case "B":
                        {
                            DataRow[] drsSelect = data.Tables["PayoutItemBuilding"].Select(string.Format(" PayoutItemCode='{0}' ", text2));
                            decimal totalArea = SumTotalArea(drsSelect, buildings, PBSUnits, alloType, AreaField);
                            foreach (DataRow row2 in drsSelect)
                            {
                                string buildingCode = ConvertRule.ToString(row2["BuildingCode"]);
                                string text4 = ConvertRule.ToString(row2["PBSUnitCode"]);
                                decimal num4 = ConvertRule.ToDecimal(row2["ItemBuildingMoney"]);
                                if (num == 1)
                                {
                                    if (alloType == "U")
                                    {
                                        AddApportion(projectCode, dt, alloType, text4, num4, buildings, PBSUnits, AreaField);
                                    }
                                    else
                                    {
                                        AddApportion(projectCode, dt, alloType, buildingCode, num4, buildings, PBSUnits, AreaField);
                                    }
                                }
                                else if (alloType == "U")
                                {
                                    AddApportion(projectCode, dt, alloType, text4, money, totalArea, buildings, PBSUnits, AreaField);
                                }
                                else
                                {
                                    AddApportion(projectCode, dt, alloType, buildingCode, money, totalArea, buildings, PBSUnits, AreaField);
                                }
                            }
                            break;
                        }
                    }
                    ReCalcLastMoney(dt, money, "ApportionMoney");
                    SaveTempToFormal(dt, dtApportion);
                }
                dt.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static DataTable BuildApportionTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("AlloType");
            table.Columns.Add("AlloTypeName");
            table.Columns.Add("BuildingCode");
            table.Columns.Add("BuildingName");
            table.Columns.Add("BuildingArea", Type.GetType("System.Decimal"));
            table.Columns.Add("SortID");
            table.Columns.Add("ApportionMoney", Type.GetType("System.Decimal"));
            return table;
        }

        private static DataTable BuildBuildingCostApportionTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("AlloType");
            table.Columns.Add("AlloTypeName");
            table.Columns.Add("BuildingCode");
            table.Columns.Add("BuildingName");
            table.Columns.Add("BuildingArea", Type.GetType("System.Decimal"));
            table.Columns.Add("SortID");
            table.Columns.Add("ApportionMoney", Type.GetType("System.Decimal"));
            table.Columns.Add("CostCode");
            table.Columns.Add("FullCode");
            table.Columns.Add("CostName");
            table.Columns.Add("CostSortID");
            return table;
        }

        public static DataTable BuildingCostApportionAllPayout(string projectCode, string DateBegin, string DateEnd, string AreaField)
        {
            DataTable table2;
            try
            {
                EntityData buildings = ProductDAO.GetBuildingNotAreaByProjectCode(projectCode);
                EntityData pBSUnits = PBSDAO.GetPBSUnitByProject(projectCode);
                EntityData cBS = CBSDAO.GetCBSByProject(projectCode);
                DataTable dtApportion = BuildBuildingCostApportionTable();
                PayoutStrategyBuilder builder = new PayoutStrategyBuilder("Payout");
                builder.AddStrategy(new Strategy(PayoutStrategyName.ProjectCode, projectCode));
                if ((DateBegin != "") || (DateEnd != ""))
                {
                    ArrayList pas = new ArrayList();
                    pas.Add(DateBegin);
                    pas.Add(DateEnd);
                    builder.AddStrategy(new Strategy(PayoutStrategyName.PayoutDateRange, pas));
                }
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data4 = agent.FillEntityData("Payout", queryString);
                agent.Dispose();
                foreach (DataRow row in data4.CurrentTable.Rows)
                {
                    string payoutCode = ConvertRule.ToString(row["PayoutCode"]);
                    BuildingCostApportionOnePayout(projectCode, payoutCode, pBSUnits, buildings, cBS, dtApportion, AreaField);
                }
                data4.Dispose();
                pBSUnits.Dispose();
                buildings.Dispose();
                cBS.Dispose();
                table2 = dtApportion;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        private static void BuildingCostApportionOnePayout(string projectCode, string payoutCode, EntityData PBSUnits, EntityData buildings, EntityData CBS, DataTable dtApportion, string AreaField)
        {
            try
            {
                DataTable dt = BuildBuildingCostApportionTable();
                EntityData data = PaymentDAO.GetStandard_PayoutByCode(payoutCode);
                foreach (DataRow row in data.Tables["PayoutItem"].Rows)
                {
                    dt.Clear();
                    string alloType = ConvertRule.ToString(row["AlloType"]);
                    string text2 = ConvertRule.ToString(row["PayoutItemCode"]);
                    int num = ConvertRule.ToInt(row["IsManualAlloc"]);
                    string subjectCode = ConvertRule.ToString(row["SubjectCode"]);
                    decimal money = ConvertRule.ToDecimal(row["PayoutMoney"]);
                    switch (alloType)
                    {
                        case "P":
                            AddBuildingCostApportion(projectCode, dt, alloType, "项目", money, buildings, PBSUnits, CBS, subjectCode, AreaField);
                            break;

                        case "U":
                        case "B":
                        {
                            DataRow[] drsSelect = data.Tables["PayoutItemBuilding"].Select(string.Format(" PayoutItemCode='{0}' ", text2));
                            decimal totalArea = SumTotalArea(drsSelect, buildings, PBSUnits, alloType, AreaField);
                            foreach (DataRow row2 in drsSelect)
                            {
                                string buildingCode = ConvertRule.ToString(row2["BuildingCode"]);
                                string text5 = ConvertRule.ToString(row2["PBSUnitCode"]);
                                decimal num4 = ConvertRule.ToDecimal(row2["ItemBuildingMoney"]);
                                if (num == 1)
                                {
                                    if (alloType == "U")
                                    {
                                        AddBuildingCostApportion(projectCode, dt, alloType, text5, num4, buildings, PBSUnits, CBS, subjectCode, AreaField);
                                    }
                                    else
                                    {
                                        AddBuildingCostApportion(projectCode, dt, alloType, buildingCode, num4, buildings, PBSUnits, CBS, subjectCode, AreaField);
                                    }
                                }
                                else if (alloType == "U")
                                {
                                    AddBuildingCostApportion(projectCode, dt, alloType, text5, money, totalArea, buildings, PBSUnits, CBS, subjectCode, AreaField);
                                }
                                else
                                {
                                    AddBuildingCostApportion(projectCode, dt, alloType, buildingCode, money, totalArea, buildings, PBSUnits, CBS, subjectCode, AreaField);
                                }
                            }
                            break;
                        }
                    }
                    ReCalcLastMoney(dt, money, "ApportionMoney");
                    SaveTempToFormalByCost(dt, dtApportion);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable CostApportionExcel(string projectCode, string AreaField)
        {
            DataTable table4;
            try
            {
                DataTable table = BuildingCostApportionAllPayout(projectCode, "", "", AreaField);
                EntityData buildingNotAreaByProjectCode = ProductDAO.GetBuildingNotAreaByProjectCode(projectCode);
                EntityData pBSUnitByProject = PBSDAO.GetPBSUnitByProject(projectCode);
                DataTable currentTable = buildingNotAreaByProjectCode.CurrentTable;
                currentTable.Columns.Add("BuildingArea", Type.GetType("System.Decimal"));
                currentTable.Columns.Add("CostTd", typeof(decimal));
                currentTable.Columns.Add("CostQq", typeof(decimal));
                currentTable.Columns.Add("CostKf", typeof(decimal));
                currentTable.Columns.Add("CostZt", typeof(decimal));
                currentTable.Columns.Add("CostTj", typeof(decimal));
                currentTable.Columns.Add("CostQt", typeof(decimal));
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("BuildingCode");
                dtTemp.Columns.Add("BuildingArea", typeof(decimal));
                dtTemp.Columns.Add("TotalCost", typeof(decimal));
                foreach (DataRow row in buildingNotAreaByProjectCode.CurrentTable.Rows)
                {
                    row["TotalCost"] = DBNull.Value;
                    row["CostPrice"] = DBNull.Value;
                    row["BuildingArea"] = row["Area"];
                }
                foreach (DataRow row in table.Rows)
                {
                    DataRow row3;
                    dtTemp.Clear();
                    string text = ConvertRule.ToString(row["AlloType"]);
                    decimal totalMoney = ConvertRule.ToDecimal(row["ApportionMoney"]);
                    decimal num2 = ConvertRule.ToDecimal(row["BuildingArea"]);
                    string costCode = ConvertRule.ToString(row["CostCode"]);
                    string costSortID = ConvertRule.ToString(row["CostSortID"]);
                    if (num2 <= 0M)
                    {
                        break;
                    }
                    switch (text)
                    {
                        case "P":
                            foreach (DataRow row2 in buildingNotAreaByProjectCode.CurrentTable.Rows)
                            {
                                decimal num3 = ConvertRule.ToDecimal(row2["BuildingArea"]);
                                decimal num4 = ConvertRule.ToDecimal(row2["TotalCost"]);
                                row3 = dtTemp.NewRow();
                                row3["BuildingCode"] = row2["BuildingCode"];
                                row3["BuildingArea"] = row2["BuildingArea"];
                                row3["TotalCost"] = Math.Round((decimal) ((num3 * totalMoney) / num2), 2);
                                dtTemp.Rows.Add(row3);
                            }
                            ReCalcLastMoney(dtTemp, totalMoney, "TotalCost");
                            SaveBuildingCostTempToFormalByCostCode(dtTemp, buildingNotAreaByProjectCode.CurrentTable, costCode, costSortID);
                            break;

                        case "U":
                        {
                            string text4 = ConvertRule.ToString(row["BuildingCode"]);
                            foreach (DataRow row2 in buildingNotAreaByProjectCode.CurrentTable.Select(string.Format("PBSUnitCode='{0}'", text4)))
                            {
                                row3 = dtTemp.NewRow();
                                row3["BuildingCode"] = row2["BuildingCode"];
                                row3["BuildingArea"] = row2["BuildingArea"];
                                row3["TotalCost"] = Math.Round((decimal) ((ConvertRule.ToDecimal(row2["BuildingArea"]) * totalMoney) / num2), 2);
                                dtTemp.Rows.Add(row3);
                            }
                            SaveBuildingCostTempToFormalByCostCode(dtTemp, buildingNotAreaByProjectCode.CurrentTable, costCode, costSortID);
                            break;
                        }
                        default:
                            if (text == "B")
                            {
                                row3 = dtTemp.NewRow();
                                row3["BuildingCode"] = row["BuildingCode"];
                                row3["BuildingArea"] = num2;
                                row3["TotalCost"] = totalMoney;
                                dtTemp.Rows.Add(row3);
                                SaveBuildingCostTempToFormalByCostCode(dtTemp, buildingNotAreaByProjectCode.CurrentTable, costCode, costSortID);
                            }
                            break;
                    }
                }
                table.Dispose();
                pBSUnitByProject.Dispose();
                table4 = currentTable;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table4;
        }

        private static string GetAlloTypeName(string alloType)
        {
            string text = "";
            if (alloType == "P")
            {
                return "项目";
            }
            if (alloType == "U")
            {
                return "单位工程";
            }
            if (alloType == "B")
            {
                text = "楼栋";
            }
            return text;
        }

        public static string GetApportionAreaField(string ProjectCode)
        {
            string text2;
            try
            {
                string projectConfigValue = SystemRule.GetProjectConfigValue(ProjectCode, "CostApportionBuildingAreaField");
                if (projectConfigValue == "")
                {
                    projectConfigValue = "HouseArea";
                }
                text2 = projectConfigValue;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        private static decimal GetBuildingAreaSum(string buildingCode, EntityData buildings, string AreaField)
        {
            decimal num = 0M;
            DataRow[] rowArray = buildings.CurrentTable.Select(string.Format("BuildingCode='{0}'", buildingCode));
            if (rowArray.Length > 0)
            {
                num = ConvertRule.ToDecimal(rowArray[0][AreaField]);
            }
            return num;
        }

        private static void GetBuildingInfo(string alloType, string buildingCode, ref string buildingName, ref decimal buildingArea, EntityData PBSUnits, EntityData buildings, string AreaField)
        {
            if (alloType == "P")
            {
                buildingName = ProjectRule.GetProjectName(buildingCode);
                buildingArea = MathRule.SumColumn(buildings.CurrentTable, AreaField);
            }
            else
            {
                DataRow[] rowArray;
                if (alloType == "U")
                {
                    rowArray = PBSUnits.CurrentTable.Select(string.Format("PBSUnitCode='{0}'", buildingCode));
                    if (rowArray.Length > 0)
                    {
                        buildingName = ConvertRule.ToString(rowArray[0]["PBSUnitName"]);
                        buildingArea = ConvertRule.ToDecimal(rowArray[0]["BuildingAreaSum"]);
                    }
                }
                else if (alloType == "B")
                {
                    rowArray = buildings.CurrentTable.Select(string.Format("BuildingCode='{0}'", buildingCode));
                    if (rowArray.Length > 0)
                    {
                        buildingName = ConvertRule.ToString(rowArray[0]["BuildingName"]);
                        buildingArea = ConvertRule.ToDecimal(rowArray[0][AreaField]);
                    }
                }
            }
        }

        private static void GetCostDetail(string subjectCode, EntityData CBS, ref string costCode, ref string costName, ref string fullCode, ref string costSortID)
        {
            DataRow[] rowArray = CBS.CurrentTable.Select(string.Format(" SubjectCode='{0}' ", subjectCode));
            if (rowArray.Length > 0)
            {
                costCode = ConvertRule.ToString(rowArray[0]["CostCode"]);
                costName = ConvertRule.ToString(rowArray[0]["CostName"]);
                fullCode = ConvertRule.ToString(rowArray[0]["FullCode"]);
                costSortID = ConvertRule.ToString(rowArray[0]["SortID"]);
            }
        }

        private static decimal GetPBSUnitArea(string PBSUnitCode, EntityData PBSUnits)
        {
            return MathRule.SumColumn(PBSUnits.CurrentTable, "BuildingAreaSum", string.Format("PBSUnitCode='{0}'", PBSUnitCode));
        }

        private static decimal GetPrice(decimal cost, decimal area)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                if (area != 0M)
                {
                    num = Math.Round((decimal) (cost / area), 2);
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static decimal GetTotalCost(string projectCode)
        {
            decimal num2;
            try
            {
                decimal num = 0M;
                QueryAgent agent = new QueryAgent();
                try
                {
                    string format = "select sum(isnull(money, 0)) as TotalCost from Payout where ProjectCode = '{0}'";
                    num = ConvertRule.ToDecimal(agent.ExecuteScalar(string.Format(format, projectCode)));
                }
                finally
                {
                    agent.Dispose();
                }
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        public static void ProjectCostApportion(string projectCode, string AreaField)
        {
            try
            {
                decimal cost;
                string text3;
                DataTable table = ApportionAllPayout(projectCode, AreaField);
                EntityData entity = ProductDAO.GetBuildingNotAreaByProjectCode(projectCode);
                EntityData pBSUnitByProject = PBSDAO.GetPBSUnitByProject(projectCode);
                entity.CurrentTable.Columns.Add("BuildingArea", Type.GetType("System.Decimal"));
                foreach (DataRow row in entity.CurrentTable.Rows)
                {
                    row["TotalCost"] = DBNull.Value;
                    row["CostPrice"] = DBNull.Value;
                    row["BuildingArea"] = row[AreaField];
                }
                foreach (DataRow row in table.Rows)
                {
                    decimal num3;
                    string text = ConvertRule.ToString(row["AlloType"]);
                    decimal totalMoney = ConvertRule.ToDecimal(row["ApportionMoney"]);
                    decimal num2 = ConvertRule.ToDecimal(row["BuildingArea"]);
                    if (num2 <= 0M)
                    {
                        break;
                    }
                    switch (text)
                    {
                        case "P":
                        {
                            DataTable dtTemp = new DataTable();
                            dtTemp.Columns.Add("BuildingCode");
                            dtTemp.Columns.Add("BuildingArea", typeof(decimal));
                            dtTemp.Columns.Add("TotalCost", typeof(decimal));
                            foreach (DataRow row2 in entity.CurrentTable.Rows)
                            {
                                num3 = ConvertRule.ToDecimal(row2["BuildingArea"]);
                                cost = ConvertRule.ToDecimal(row2["TotalCost"]);
                                DataRow row3 = dtTemp.NewRow();
                                row3["BuildingCode"] = row2["BuildingCode"];
                                row3["BuildingArea"] = row2["BuildingArea"];
                                row3["TotalCost"] = Math.Round((decimal) ((num3 * totalMoney) / num2), 2);
                                dtTemp.Rows.Add(row3);
                            }
                            ReCalcLastMoney(dtTemp, totalMoney, "TotalCost");
                            SaveBuildingCostTempToFormal(dtTemp, entity.CurrentTable);
                            break;
                        }
                        case "U":
                        {
                            string text2 = ConvertRule.ToString(row["BuildingCode"]);
                            foreach (DataRow row2 in entity.CurrentTable.Select(string.Format("PBSUnitCode='{0}'", text2)))
                            {
                                num3 = ConvertRule.ToDecimal(row2["BuildingArea"]);
                                cost = ConvertRule.ToDecimal(row2["TotalCost"]);
                                row2["TotalCost"] = cost + Math.Round((decimal) ((num3 * totalMoney) / num2), 2);
                            }
                            break;
                        }
                        case "B":
                            text3 = ConvertRule.ToString(row["BuildingCode"]);
                            foreach (DataRow row2 in entity.CurrentTable.Select(string.Format("BuildingCode='{0}'", text3)))
                            {
                                cost = ConvertRule.ToDecimal(row2["TotalCost"]);
                                row2["TotalCost"] = cost + totalMoney;
                            }
                            break;
                    }
                }
                EntityData roomByProjectCode = ProductDAO.GetRoomByProjectCode(projectCode);
                foreach (DataRow row2 in entity.CurrentTable.Rows)
                {
                    text3 = ConvertRule.ToString(row2["BuildingCode"]);
                    decimal area = ConvertRule.ToDecimal(row2["BuildingArea"]);
                    cost = ConvertRule.ToDecimal(row2["TotalCost"]);
                    decimal price = GetPrice(cost, area);
                    row2["CostPrice"] = price;
                    DataRow[] rowArray = roomByProjectCode.CurrentTable.Select(string.Format("BuildingCode='{0}'", text3));
                    int length = rowArray.Length;
                    decimal num8 = 0M;
                    for (int i = 0; i < length; i++)
                    {
                        DataRow row4 = rowArray[i];
                        decimal num10 = ConvertRule.ToDecimal(row4["BuildArea"]);
                        row4["CostPrice"] = price;
                        if (i < (length - 1))
                        {
                            decimal num11 = Math.Round((decimal) (num10 * price), 2);
                            num8 += num11;
                            row4["Cost"] = num11;
                        }
                        else
                        {
                            row4["Cost"] = cost - num8;
                        }
                    }
                }
                ProductDAO.SubmitAllBuilding(entity);
                ProductDAO.SubmitAllRoom(roomByProjectCode);
                table.Dispose();
                entity.Dispose();
                roomByProjectCode.Dispose();
                pBSUnitByProject.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void ReCalcLastMoney(DataTable dtTemp, decimal TotalMoney, string MoneyField)
        {
            try
            {
                decimal num = 0M;
                if (dtTemp.Rows.Count > 0)
                {
                    int count = dtTemp.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        DataRow row = dtTemp.Rows[i];
                        if (i < (count - 1))
                        {
                            num += ConvertRule.ToDecimal(row[MoneyField]);
                        }
                        else
                        {
                            row[MoneyField] = TotalMoney - num;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void SaveBuildingCostTempToFormal(DataTable dtTemp, DataTable dtBuilding)
        {
            try
            {
                foreach (DataRow row in dtTemp.Rows)
                {
                    string text = ConvertRule.ToString(row["BuildingCode"]);
                    DataRow[] rowArray = dtBuilding.Select(string.Format(" BuildingCode='{0}' ", text));
                    DataRow row2 = null;
                    if (rowArray.Length > 0)
                    {
                        row2 = rowArray[0];
                        row2["TotalCost"] = ConvertRule.ToDecimal(row2["TotalCost"]) + ConvertRule.ToDecimal(row["TotalCost"]);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void SaveBuildingCostTempToFormalByCostCode(DataTable dtTemp, DataTable dtBuilding, string CostCode, string CostSortID)
        {
            try
            {
                foreach (DataRow row in dtTemp.Rows)
                {
                    string text = ConvertRule.ToString(row["BuildingCode"]);
                    DataRow[] rowArray = dtBuilding.Select(string.Format(" BuildingCode='{0}' ", text));
                    DataRow row2 = null;
                    if (rowArray.Length > 0)
                    {
                        row2 = rowArray[0];
                        row2["TotalCost"] = ConvertRule.ToDecimal(row2["TotalCost"]) + ConvertRule.ToDecimal(row["TotalCost"]);
                        string text2 = "";
                        if (CostSortID.StartsWith("030407"))
                        {
                            text2 = "CostKf";
                        }
                        else if (CostSortID == "0301")
                        {
                            text2 = "CostTj";
                        }
                        else if (CostSortID.StartsWith("01"))
                        {
                            text2 = "CostTd";
                        }
                        else if (CostSortID.StartsWith("02"))
                        {
                            text2 = "CostQq";
                        }
                        else if (CostSortID.StartsWith("04") || CostSortID.StartsWith("030406"))
                        {
                            text2 = "CostZt";
                        }
                        else
                        {
                            text2 = "CostZt";
                        }
                        row2[text2] = ConvertRule.ToDecimal(row2[text2]) + ConvertRule.ToDecimal(row["TotalCost"]);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void SaveTempToFormal(DataTable dtTemp, DataTable dt)
        {
            try
            {
                foreach (DataRow row in dtTemp.Rows)
                {
                    string text = ConvertRule.ToString(row["AlloType"]);
                    string text2 = ConvertRule.ToString(row["buildingCode"]);
                    DataRow[] rowArray = dt.Select(string.Format(" AlloType='{0}' and BuildingCode='{1}' ", text, text2));
                    DataRow drDst = null;
                    if (rowArray.Length > 0)
                    {
                        drDst = rowArray[0];
                        drDst["ApportionMoney"] = ConvertRule.ToDecimal(drDst["ApportionMoney"]) + ConvertRule.ToDecimal(row["ApportionMoney"]);
                    }
                    else
                    {
                        drDst = dt.NewRow();
                        ConvertRule.DataRowCopy(row, drDst, dtTemp, dt);
                        dt.Rows.Add(drDst);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static void SaveTempToFormalByCost(DataTable dtTemp, DataTable dt)
        {
            try
            {
                foreach (DataRow row in dtTemp.Rows)
                {
                    string text = ConvertRule.ToString(row["AlloType"]);
                    string text2 = ConvertRule.ToString(row["buildingCode"]);
                    string text3 = ConvertRule.ToString(row["costCode"]);
                    DataRow[] rowArray = dt.Select(string.Format(" AlloType='{0}' and BuildingCode='{1}' and CostCode='{2}' ", text, text2, text3));
                    DataRow drDst = null;
                    if (rowArray.Length > 0)
                    {
                        drDst = rowArray[0];
                        drDst["ApportionMoney"] = ConvertRule.ToDecimal(drDst["ApportionMoney"]) + ConvertRule.ToDecimal(row["ApportionMoney"]);
                    }
                    else
                    {
                        drDst = dt.NewRow();
                        ConvertRule.DataRowCopy(row, drDst, dtTemp, dt);
                        dt.Rows.Add(drDst);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static decimal SumTotalArea(DataRow[] drsSelect, EntityData buildings, EntityData PBSUnits, string alloType, string AreaField)
        {
            decimal pBSUnitArea;
            decimal num = 0M;
            if (alloType == "U")
            {
                foreach (DataRow row in drsSelect)
                {
                    pBSUnitArea = GetPBSUnitArea(ConvertRule.ToString(row["PBSUnitCode"]), PBSUnits);
                    num += pBSUnitArea;
                }
                return num;
            }
            foreach (DataRow row in drsSelect)
            {
                pBSUnitArea = GetBuildingAreaSum(ConvertRule.ToString(row["BuildingCode"]), buildings, AreaField);
                num += pBSUnitArea;
            }
            return num;
        }
    }
}

