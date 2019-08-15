namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Rms.ORMap;
    using RmsPM.BLL.RefSal;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public sealed class PageFacade
    {
        public static void FillListGroupFromDictionary(CheckBoxList cbl, string dictionaryName, string selectedValues)
        {
            try
            {
                EntityData dictionaryItemByName = SystemManageDAO.GetDictionaryItemByName(dictionaryName);
                int count = dictionaryItemByName.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    dictionaryItemByName.SetCurrentRow(i);
                    cbl.Items.Add(new ListItem(dictionaryItemByName.GetString("Name"), dictionaryItemByName.GetString("Name")));
                }
                dictionaryItemByName.Dispose();
                if (selectedValues != "")
                {
                    SetListGroupSelectedValues(cbl, selectedValues);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static string GetListGroupSelectedTexts(CheckBoxList cbl)
        {
            string text = "";
            foreach (ListItem item in cbl.Items)
            {
                if (item.Selected)
                {
                    if (text != "")
                    {
                        text = text + ",";
                    }
                    text = text + item.Text;
                }
            }
            return text;
        }

        public static string GetListGroupSelectedValues(CheckBoxList cbl)
        {
            string text = "";
            foreach (ListItem item in cbl.Items)
            {
                if (item.Selected)
                {
                    if (text != "")
                    {
                        text = text + ",";
                    }
                    if (text == "4")
                    {
                        text = text + "0";
                    }
                    else
                    {
                        text = text + item.Value;
                    }
                }
            }
            return text;
        }

        public static int GetSelectIndexByText(HtmlSelect slt, string FindText)
        {
            int num3;
            try
            {
                int num = -1;
                int num2 = -1;
                foreach (ListItem item in slt.Items)
                {
                    num2++;
                    if (item.Text == FindText)
                    {
                        num = num2;
                    }
                }
                num3 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num3;
        }

        public static void LoadAllRoleSelect(HtmlSelect slt, string selectedValue)
        {
            try
            {
                EntityData allRole = SystemManageDAO.GetAllRole();
                int count = allRole.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    allRole.SetCurrentRow(i);
                    slt.Items.Add(new ListItem(allRole.GetString("RoleName"), allRole.GetString("RoleCode")));
                }
                slt.Value = selectedValue;
                allRole.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadAllUserSelect(HtmlSelect slt, string selectedValue)
        {
            try
            {
                EntityData allSystemUser = SystemManageDAO.GetAllSystemUser();
                DataView view = new DataView(allSystemUser.CurrentTable, "", "UserName", DataViewRowState.CurrentRows);
                foreach (DataRowView view2 in view)
                {
                    slt.Items.Add(new ListItem(ConvertRule.ToString(view2.Row["UserName"]), ConvertRule.ToString(view2.Row["UserCode"])));
                }
                slt.Value = selectedValue;
                allSystemUser.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadBuildingAndAreaSelect(HtmlSelect slt, string selectedValue, string projectCode)
        {
            try
            {
                EntityData buildingFullNameByProjectCode = ProductDAO.GetBuildingFullNameByProjectCode(projectCode);
                if (buildingFullNameByProjectCode.HasRecord())
                {
                    for (int i = 0; i <= (buildingFullNameByProjectCode.CurrentTable.Rows.Count - 1); i++)
                    {
                        buildingFullNameByProjectCode.SetCurrentRow(i);
                        string text = buildingFullNameByProjectCode.GetString("BuildingCode");
                        string text2 = buildingFullNameByProjectCode.GetString("BuildingFullName");
                        slt.Items.Add(new ListItem(text2, text));
                    }
                }
                buildingFullNameByProjectCode.Dispose();
                if (selectedValue != null)
                {
                    slt.Value = selectedValue;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadBuildingAreaFieldSelect(HtmlSelect slt, string selectedValue)
        {
            try
            {
                DataTable buildingAreaField = ProductRule.GetBuildingAreaField();
                foreach (DataRow row in buildingAreaField.Rows)
                {
                    slt.Items.Add(new ListItem(ConvertRule.ToString(row["FieldDesc"]), ConvertRule.ToString(row["FieldName"])));
                }
                slt.Value = selectedValue;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadBuildingFunctionSelect(HtmlSelect slt, string selectedValue, string buildingCode)
        {
            try
            {
                EntityData buildingFunctionByBuildingCode = ProductDAO.GetBuildingFunctionByBuildingCode(buildingCode);
                if (buildingFunctionByBuildingCode.HasRecord())
                {
                    for (int i = 0; i < buildingFunctionByBuildingCode.CurrentTable.Rows.Count; i++)
                    {
                        buildingFunctionByBuildingCode.SetCurrentRow(i);
                        slt.Items.Add(new ListItem(buildingFunctionByBuildingCode.GetString("FunctionName"), buildingFunctionByBuildingCode.GetString("BuildingFunctionCode")));
                    }
                    slt.Value = selectedValue;
                }
                buildingFunctionByBuildingCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadBuildingProgressChildTaskSelect(HtmlSelect slt, string selectedValue, string BuildingCode, string VisualProgressWBSCode)
        {
            try
            {
                EntityData childTask = WBSDAO.GetChildTask(VisualProgressWBSCode);
                if (childTask.HasRecord())
                {
                    for (int i = 0; i < childTask.CurrentTable.Rows.Count; i++)
                    {
                        childTask.SetCurrentRow(i);
                        EntityData data2 = WBSDAO.GetChildTask(childTask.GetString("WBSCode"));
                        if (data2.HasRecord())
                        {
                            for (int j = 0; j < data2.CurrentTable.Rows.Count; j++)
                            {
                                data2.SetCurrentRow(j);
                                slt.Items.Add(new ListItem(childTask.GetString("TaskName") + "->" + data2.GetString("TaskName"), data2.GetString("WBSCode")));
                            }
                        }
                        else
                        {
                            slt.Items.Add(new ListItem(childTask.GetString("TaskName"), childTask.GetString("WBSCode")));
                        }
                        data2.Dispose();
                    }
                }
                slt.Value = selectedValue;
                childTask.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadBuildingProgressFloorSelect(HtmlSelect slt, string selectedValue, string BuildingCode)
        {
            try
            {
                EntityData buildingFloorByBuildingCode = ProductDAO.GetBuildingFloorByBuildingCode(BuildingCode);
                if (buildingFloorByBuildingCode.HasRecord())
                {
                    for (int i = 0; i <= (buildingFloorByBuildingCode.CurrentTable.Rows.Count - 1); i++)
                    {
                        buildingFloorByBuildingCode.SetCurrentRow(i);
                        ListItem item = new ListItem(buildingFloorByBuildingCode.GetString("FloorName").ToString(), buildingFloorByBuildingCode.GetString("BuildingFloorCode").ToString());
                        item.Attributes["FloorIndex"] = buildingFloorByBuildingCode.GetInt("FloorIndex").ToString();
                        slt.Items.Add(item);
                    }
                }
                slt.Value = selectedValue;
                buildingFloorByBuildingCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadBuildingSelect(HtmlSelect slt, string selectedValue, string projectCode)
        {
            try
            {
                EntityData buildNoAreaFullNameByProjectCode = ProductDAO.GetBuildNoAreaFullNameByProjectCode(projectCode);
                if (buildNoAreaFullNameByProjectCode.HasRecord())
                {
                    for (int i = 0; i <= (buildNoAreaFullNameByProjectCode.CurrentTable.Rows.Count - 1); i++)
                    {
                        buildNoAreaFullNameByProjectCode.SetCurrentRow(i);
                        string buildingCode = buildNoAreaFullNameByProjectCode.GetString("BuildingCode");
                        string text = buildNoAreaFullNameByProjectCode.GetString("BuildingName");
                        if (buildNoAreaFullNameByProjectCode.GetString("ParentCode") != "")
                        {
                            text = ProductRule.GetAllBuildingNameByCode(buildingCode);
                        }
                        slt.Items.Add(new ListItem(text, buildingCode));
                    }
                }
                buildNoAreaFullNameByProjectCode.Dispose();
                if (selectedValue != null)
                {
                    slt.Value = selectedValue;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadBuildingStationSelect(HtmlSelect slt, string selectedValue, string buildingCode)
        {
            try
            {
                EntityData buildingStationByBuildingCode = ProductDAO.GetBuildingStationByBuildingCode(buildingCode);
                if (buildingStationByBuildingCode.HasRecord())
                {
                    for (int i = 0; i < buildingStationByBuildingCode.CurrentTable.Rows.Count; i++)
                    {
                        buildingStationByBuildingCode.SetCurrentRow(i);
                        slt.Items.Add(new ListItem(buildingStationByBuildingCode.GetString("StationName"), buildingStationByBuildingCode.GetString("BuildingStationCode")));
                    }
                    slt.Value = selectedValue;
                }
                buildingStationByBuildingCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadBuildingTaskVisualProgressSelect(HtmlSelect slt, string selectedValue, string[] arrBuildingCode, string ProjectCode)
        {
            try
            {
                DataTable buildingTaskVisualProgress = ConstructProgRule.GetBuildingTaskVisualProgress(arrBuildingCode, ProjectCode);
                foreach (DataRow row in buildingTaskVisualProgress.Rows)
                {
                    slt.Items.Add(new ListItem(ConvertRule.ToString(row["TaskName"]), ConvertRule.ToString(row["TaskName"])));
                }
                slt.Value = selectedValue;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadBuildingTaskVisualProgressSelect(HtmlSelect slt, string selectedValue, string BuildingCode, string ProjectCode)
        {
            try
            {
                EntityData buildingTaskVisualProgress = ConstructProgRule.GetBuildingTaskVisualProgress(BuildingCode, ProjectCode);
                if (buildingTaskVisualProgress.HasRecord())
                {
                    for (int i = 0; i <= (buildingTaskVisualProgress.CurrentTable.Rows.Count - 1); i++)
                    {
                        buildingTaskVisualProgress.SetCurrentRow(i);
                        slt.Items.Add(new ListItem(buildingTaskVisualProgress.GetString("TaskName").ToString(), buildingTaskVisualProgress.GetString("WBSCode").ToString()));
                    }
                }
                slt.Value = selectedValue;
                buildingTaskVisualProgress.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadCBSModule(HtmlSelect slt, string selectedValue, string type)
        {
            try
            {
                EntityData allCBSModule = CBSDAO.GetAllCBSModule();
                int count = allCBSModule.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    allCBSModule.SetCurrentRow(i);
                    slt.Items.Add(new ListItem(allCBSModule.GetString("TITLE"), allCBSModule.GetString("TempletCode")));
                }
                slt.Value = selectedValue;
                allCBSModule.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadCBSParentSelect(HtmlSelect slt, string selectedValue, string ProjectCode, string CurrentCostCode)
        {
            try
            {
                QueryAgent agent = new QueryAgent();
                try
                {
                    string queryString = string.Format("select * from cbs where ProjectCode = '{0}'", ProjectCode);
                    if (CurrentCostCode != "")
                    {
                        string cBSFullCode = CBSRule.GetCBSFullCode(CurrentCostCode);
                        if (cBSFullCode != "")
                        {
                            queryString = queryString + string.Format(" and FullCode not like '{0}%'", cBSFullCode);
                        }
                    }
                    queryString = queryString + " order by FullCode";
                    EntityData data = agent.FillEntityData("CBS", queryString);
                    int count = data.CurrentTable.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        data.SetCurrentRow(i);
                        slt.Items.Add(new ListItem(CBSRule.GetCostFullName(data.GetString("CostCode"), true), data.GetString("CostCode")));
                    }
                    data.Dispose();
                }
                finally
                {
                    agent.Dispose();
                }
                slt.Value = selectedValue;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadConstructPlanYearSelect(HtmlSelect slt, string selectedValue, string ProjectCode, bool isDefaultYear)
        {
            try
            {
                DataTable constructAnnualPlanYearByProject = ConstructDAO.GetConstructAnnualPlanYearByProject(ProjectCode);
                foreach (DataRow row in constructAnnualPlanYearByProject.Rows)
                {
                    slt.Items.Add(new ListItem(ConvertRule.ToString(row["IYear"]), ConvertRule.ToString(row["IYear"])));
                }
                if ((constructAnnualPlanYearByProject.Rows.Count == 0) && isDefaultYear)
                {
                    string text = DateTime.Today.Year.ToString();
                    slt.Items.Add(new ListItem(text, text));
                }
                try
                {
                    slt.Value = selectedValue;
                }
                catch
                {
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadContractModelSelect(HtmlSelect slt, string selectedValue)
        {
            try
            {
                EntityData allContractModel = ContractDAO.GetAllContractModel();
                int count = allContractModel.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    allContractModel.SetCurrentRow(i);
                    slt.Items.Add(new ListItem(allContractModel.GetString("ContractModelName"), allContractModel.GetString("ContractModelCode")));
                }
                slt.Value = selectedValue;
                allContractModel.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadCostBudgetBackupSelect(HtmlSelect slt, string a_selectedValue, string projectCode)
        {
            try
            {
                string text = a_selectedValue;
                slt.Items.Clear();
                DataTable costBudgetBackupSelectTable = CostBudgetRule.GetCostBudgetBackupSelectTable(projectCode, text);
                foreach (DataRow row in costBudgetBackupSelectTable.Rows)
                {
                    slt.Items.Add(new ListItem(ConvertRule.ToString(row["Desc"]), ConvertRule.ToString(row["CostBudgetBackupCode"])));
                }
                try
                {
                    slt.Value = text;
                }
                catch
                {
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadCostBudgetSetSelect(HtmlSelect slt, string selectedValue, string ProjectCode)
        {
            try
            {
                LoadCostBudgetSetSelect(slt, selectedValue, ProjectCode, null);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadCostBudgetSetSelect(HtmlSelect slt, string selectedValue, string ProjectCode, EntityData AccessUnit)
        {
            try
            {
                int rowIndex;
                EntityData data = CostBudgetDAO.GetV_CostBudgetSetByProjectCode(ProjectCode, CostBudgetRule.m_BaseSetType);
                if (AccessUnit != null)
                {
                    for (rowIndex = data.CurrentTable.Rows.Count - 1; rowIndex >= 0; rowIndex--)
                    {
                        data.SetCurrentRow(rowIndex);
                        string text = data.GetString("UnitCode");
                        if (AccessUnit.CurrentTable.Select("UnitCode='" + text + "'").Length <= 0)
                        {
                            data.CurrentTable.Rows.Remove(data.CurrentRow);
                        }
                    }
                }
                if (data.HasRecord())
                {
                    for (rowIndex = 0; rowIndex <= (data.CurrentTable.Rows.Count - 1); rowIndex++)
                    {
                        data.SetCurrentRow(rowIndex);
                        ListItem item = new ListItem(data.GetString("CostBudgetSetName").ToString(), data.GetString("CostBudgetSetCode").ToString());
                        item.Attributes["PBSType"] = data.GetString("PBSType");
                        item.Attributes["PBSCode"] = data.GetString("PBSCode");
                        item.Attributes["PBSName"] = data.GetString("PBSName");
                        slt.Items.Add(item);
                    }
                }
                try
                {
                    slt.Value = selectedValue;
                }
                catch
                {
                }
                data.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadCostBudgetSetTypeSelect(HtmlSelect slt, string selectedValue)
        {
            try
            {
                DataTable costBudgetSetTypeTable = CostBudgetRule.GetCostBudgetSetTypeTable();
                foreach (DataRow row in costBudgetSetTypeTable.Rows)
                {
                    slt.Items.Add(new ListItem(ConvertRule.ToString(row["Name"]), ConvertRule.ToString(row["Code"])));
                }
                try
                {
                    slt.Value = selectedValue;
                }
                catch
                {
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadCostByPBSSelect(HtmlSelect slt, string selectedValue, string ProjectCode)
        {
            try
            {
                EntityData buildingByProjectCode = ProductDAO.GetBuildingByProjectCode(ProjectCode);
                if (buildingByProjectCode.HasRecord())
                {
                    for (int i = 0; i <= (buildingByProjectCode.CurrentTable.Rows.Count - 1); i++)
                    {
                        buildingByProjectCode.SetCurrentRow(i);
                        ListItem item = new ListItem(buildingByProjectCode.GetString("BuildingName"), buildingByProjectCode.GetString("BuildingCode"));
                        slt.Items.Add(item);
                    }
                }
                slt.Items.Add(new ListItem("不可划分", "P"));
                try
                {
                    slt.Value = selectedValue;
                }
                catch
                {
                }
                buildingByProjectCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadDictionarySelect(HtmlSelect sltDict, string dictionaryName, string selectedValue)
        {
            try
            {
                LoadDictionarySelect(sltDict, dictionaryName, selectedValue, "");
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadDictionarySelect(DropDownList ddlDict, string dictionaryName, string selectedValue)
        {
            try
            {
                LoadDictionarySelect(ddlDict, dictionaryName, selectedValue, "");
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadDictionarySelect(HtmlSelect sltDict, string dictionaryName, string selectedValue, string projectCode)
        {
            try
            {
                EntityData dictionaryItemByNameProject = SystemManageDAO.GetDictionaryItemByNameProject(dictionaryName, projectCode);
                int count = dictionaryItemByNameProject.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    dictionaryItemByNameProject.SetCurrentRow(i);
                    sltDict.Items.Add(new ListItem(dictionaryItemByNameProject.GetString("Name"), dictionaryItemByNameProject.GetString("Name")));
                }
                dictionaryItemByNameProject.Dispose();
                sltDict.Value = selectedValue;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadDictionarySelect(DropDownList ddlDict, string dictionaryName, string selectedValue, string projectCode)
        {
            try
            {
                EntityData dictionaryItemByNameProject = SystemManageDAO.GetDictionaryItemByNameProject(dictionaryName, projectCode);
                int count = dictionaryItemByNameProject.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    dictionaryItemByNameProject.SetCurrentRow(i);
                    ListItem item = new ListItem(dictionaryItemByNameProject.GetString("Name"), dictionaryItemByNameProject.GetString("Name"));
                    ddlDict.Items.Add(item);
                }
                dictionaryItemByNameProject.Dispose();
                ddlDict.SelectedIndex = ddlDict.Items.IndexOf(ddlDict.Items.FindByValue(selectedValue));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadDictionarySelect(DropDownList ddlDict, string dictionaryName, string selectedText, string selectedValue, string projectCode)
        {
            try
            {
                EntityData dictionaryItemByNameProject = SystemManageDAO.GetDictionaryItemByNameProject(dictionaryName, projectCode);
                int count = dictionaryItemByNameProject.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    dictionaryItemByNameProject.SetCurrentRow(i);
                    ListItem item = new ListItem(dictionaryItemByNameProject.GetString("Name"), dictionaryItemByNameProject.GetString("DictionaryItemCode"));
                    ddlDict.Items.Add(item);
                }
                dictionaryItemByNameProject.Dispose();
                if (selectedValue != null)
                {
                    ddlDict.SelectedIndex = ddlDict.Items.IndexOf(ddlDict.Items.FindByValue(selectedValue));
                }
                else
                {
                    ddlDict.SelectedIndex = ddlDict.Items.IndexOf(ddlDict.Items.FindByText(selectedText));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadFinanceInterfaceAnalysisTypeSelect(HtmlSelect slt, string selectedValue)
        {
            try
            {
                DataTable financeInterfaceAnalysisTypeTable = FinanceRule.GetFinanceInterfaceAnalysisTypeTable();
                foreach (DataRow row in financeInterfaceAnalysisTypeTable.Rows)
                {
                    slt.Items.Add(new ListItem(ConvertRule.ToString(row["FinanceInterfaceAnalysisTypeName"]), ConvertRule.ToString(row["FinanceInterfaceAnalysisTypeCode"])));
                }
                try
                {
                    slt.Value = selectedValue;
                }
                catch
                {
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadFinanceInterfaceSelect(HtmlSelect slt, string selectedValue)
        {
            try
            {
                DataTable financeInterfaceTable = SubjectRule.GetFinanceInterfaceTable();
                foreach (DataRow row in financeInterfaceTable.Rows)
                {
                    slt.Items.Add(new ListItem(ConvertRule.ToString(row["FinanceInterfaceName"]), ConvertRule.ToString(row["FinanceInterfaceCode"])));
                }
                try
                {
                    slt.Value = selectedValue;
                }
                catch
                {
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadFixedDocumentTypeCodeSelect(HtmlSelect slt, string selectedValue)
        {
            try
            {
                EntityData allDocumentType = DocumentDAO.GetAllDocumentType(1);
                int count = allDocumentType.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    allDocumentType.SetCurrentRow(i);
                    slt.Items.Add(new ListItem(allDocumentType.GetString("TypeName"), allDocumentType.GetString("DocumentTypeCode")));
                }
                try
                {
                    slt.Value = selectedValue;
                }
                catch
                {
                }
                allDocumentType.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadGroundWorkSelect(HtmlSelect slt, string selectedValue, string ProjectCode)
        {
            try
            {
                EntityData groundWorkRootByProjectCode = ConstructDAO.GetGroundWorkRootByProjectCode(ProjectCode);
                if (groundWorkRootByProjectCode.HasRecord())
                {
                    for (int i = 0; i <= (groundWorkRootByProjectCode.CurrentTable.Rows.Count - 1); i++)
                    {
                        groundWorkRootByProjectCode.SetCurrentRow(i);
                        ListItem item = new ListItem(groundWorkRootByProjectCode.GetString("TaskName").ToString(), groundWorkRootByProjectCode.GetString("GroundWorkCode").ToString());
                        item.Attributes["WBSCode"] = groundWorkRootByProjectCode.GetString("WBSCode");
                        item.Attributes["GroundWorkCode"] = groundWorkRootByProjectCode.GetString("GroundWorkCode");
                        slt.Items.Add(item);
                    }
                }
                slt.Value = selectedValue;
                groundWorkRootByProjectCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadModelSelect(HtmlSelect slt, string selectedValue, string projectCode)
        {
            try
            {
                EntityData modelByProjectCode = ProductDAO.GetModelByProjectCode(projectCode);
                if (modelByProjectCode.HasRecord())
                {
                    for (int i = 0; i <= (modelByProjectCode.CurrentTable.Rows.Count - 1); i++)
                    {
                        modelByProjectCode.SetCurrentRow(i);
                        slt.Items.Add(new ListItem(modelByProjectCode.GetString("ModelName"), modelByProjectCode.GetString("ModelCode")));
                    }
                }
                modelByProjectCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadNoUseModelSelect(HtmlSelect slt, string selectedValue, string projectCode, string buildingCode, string buildingModelCode)
        {
            try
            {
                EntityData data = ProductDAO.GetModelNoUseByBuildingCode(buildingCode, projectCode, buildingModelCode);
                if (data.HasRecord())
                {
                    for (int i = 0; i < data.CurrentTable.Rows.Count; i++)
                    {
                        data.SetCurrentRow(i);
                        slt.Items.Add(new ListItem(data.GetString("ModelName"), data.GetString("ModelCode")));
                    }
                    slt.Value = selectedValue;
                }
                data.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadPaymentItemSelectContractAllocation(HtmlSelect slt, string selectedValue, string ContractCode)
        {
            try
            {
                DataTable table = ContractDAO.GetStandard_ContractByCode(ContractCode).Tables["ContractAllocation"];
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    DataRow row = table.Rows[i];
                    string text = ConvertRule.ToString(row["AllocateCode"]);
                    string text2 = ConvertRule.ToString(row["ItemName"]);
                    string text3 = ConvertRule.ToString(row["CostCode"]);
                    decimal num3 = ConvertRule.ToDecimal(row["Money"]);
                    string text4 = text2;
                    slt.Items.Add(new ListItem(text4, text));
                }
                try
                {
                    slt.Value = selectedValue;
                }
                catch
                {
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadPaymentItemSelectContractAllocation(DropDownList slt, string selectedValue, string ContractCode)
        {
            try
            {
                DataTable table = ContractDAO.GetStandard_ContractByCode(ContractCode).Tables["ContractAllocation"];
                int count = table.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    DataRow row = table.Rows[i];
                    string text = ConvertRule.ToString(row["AllocateCode"]);
                    string text2 = ConvertRule.ToString(row["ItemName"]);
                    string text3 = ConvertRule.ToString(row["CostCode"]);
                    decimal num3 = ConvertRule.ToDecimal(row["Money"]);
                    string text4 = text2;
                    slt.Items.Add(new ListItem(text4, text));
                }
                try
                {
                    slt.SelectedValue = selectedValue;
                }
                catch
                {
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadPBSAreaSelect(HtmlSelect slt, string selectedValue, string projectCode)
        {
            EntityData transentity = null;
            LoadPBSAreaSelect(slt, selectedValue, projectCode, transentity);
        }

        public static void LoadPBSAreaSelect(HtmlSelect slt, string selectedValue, string projectCode, EntityData transentity)
        {
            try
            {
                EntityData buildAreaByProjectCode = ProductDAO.GetBuildAreaByProjectCode(projectCode);
                if (buildAreaByProjectCode.HasRecord())
                {
                    for (int i = 0; i <= (buildAreaByProjectCode.CurrentTable.Rows.Count - 1); i++)
                    {
                        buildAreaByProjectCode.SetCurrentRow(i);
                        if ((transentity == null) || (buildAreaByProjectCode.GetString("BuildingName") != transentity.GetString("BuildingName")))
                        {
                            slt.Items.Add(new ListItem(buildAreaByProjectCode.GetString("BuildingName"), buildAreaByProjectCode.GetString("BuildingCode")));
                        }
                    }
                }
                buildAreaByProjectCode.Dispose();
                if (selectedValue != null)
                {
                    slt.Value = selectedValue;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadPBSTypeSelect(HtmlSelect slt, string selectedValue, string projectCode)
        {
            try
            {
                EntityData pBSTypeByProject = PBSDAO.GetPBSTypeByProject("0");
                if (pBSTypeByProject.HasRecord())
                {
                    for (int i = 0; i <= (pBSTypeByProject.CurrentTable.Rows.Count - 1); i++)
                    {
                        pBSTypeByProject.SetCurrentRow(i);
                        if (pBSTypeByProject.GetInt("deep") == 2)
                        {
                            string pBSTypeCode = pBSTypeByProject.GetString("PBSTypeCode");
                            string text = PBSRule.GetPBSTypeFullName(pBSTypeCode);
                            slt.Items.Add(new ListItem(text, pBSTypeCode));
                        }
                    }
                }
                pBSTypeByProject.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadPBSTypeSelectAll(HtmlSelect slt, string selectedValue)
        {
            try
            {
                LoadPBSTypeSelectAll(slt, selectedValue, "0");
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadPBSTypeSelectAll(HtmlSelect slt, string selectedValue, string projectCode)
        {
            try
            {
                EntityData pBSTypeByProject = PBSDAO.GetPBSTypeByProject("0");
                if (pBSTypeByProject.HasRecord())
                {
                    for (int i = 0; i <= (pBSTypeByProject.CurrentTable.Rows.Count - 1); i++)
                    {
                        pBSTypeByProject.SetCurrentRow(i);
                        string pBSTypeCode = pBSTypeByProject.GetString("PBSTypeCode");
                        string text = PBSRule.GetPBSTypeFullName(pBSTypeCode);
                        slt.Items.Add(new ListItem(text, pBSTypeCode));
                    }
                }
                pBSTypeByProject.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadPBSTypeSelectFirstLevel(HtmlSelect slt, string selectedValue)
        {
            try
            {
                EntityData pBSTypeByProject = PBSDAO.GetPBSTypeByProject("0");
                if (pBSTypeByProject.HasRecord())
                {
                    for (int i = 0; i <= (pBSTypeByProject.CurrentTable.Rows.Count - 1); i++)
                    {
                        pBSTypeByProject.SetCurrentRow(i);
                        if (pBSTypeByProject.GetInt("deep") == 1)
                        {
                            string pBSTypeCode = pBSTypeByProject.GetString("PBSTypeCode");
                            string text = PBSRule.GetPBSTypeFullName(pBSTypeCode);
                            slt.Items.Add(new ListItem(text, pBSTypeCode));
                        }
                    }
                }
                pBSTypeByProject.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadPBSUnitSelect(HtmlSelect slt, string selectedValue, string ProjectCode)
        {
            try
            {
                DataTable table = PBSDAO.GetPBSUnitByProject(ProjectCode).Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    slt.Items.Add(new ListItem(row["PBSUnitName"].ToString(), row["PBSUnitCode"].ToString()));
                }
                try
                {
                    slt.Value = selectedValue;
                }
                catch
                {
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadPBSUnitSelect(DropDownList slt, string selectedValue, string ProjectCode)
        {
            try
            {
                DataTable table = PBSDAO.GetPBSUnitByProject(ProjectCode).Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    slt.Items.Add(new ListItem(row["PBSUnitName"].ToString(), row["PBSUnitCode"].ToString()));
                }
                try
                {
                    slt.SelectedValue = selectedValue;
                }
                catch
                {
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadProjectSelect(HtmlSelect slt, string selectedValue)
        {
            try
            {
                EntityData allProject = ProjectDAO.GetAllProject();
                int count = allProject.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    allProject.SetCurrentRow(i);
                    slt.Items.Add(new ListItem(allProject.GetString("ProjectName"), allProject.GetString("ProjectCode")));
                }
                slt.Value = selectedValue;
                allProject.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadProjectSelect(DropDownList slt, string selectedValue)
        {
            try
            {
                EntityData allProject = ProjectDAO.GetAllProject();
                if (allProject.HasRecord())
                {
                    for (int i = 0; i <= (allProject.CurrentTable.Rows.Count - 1); i++)
                    {
                        allProject.SetCurrentRow(i);
                        slt.Items.Add(new ListItem(allProject.GetString("ProjectName"), allProject.GetString("ProjectCode")));
                    }
                }
                allProject.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadProjectStatusSelect(HtmlSelect slt, bool isSelectFirst)
        {
            try
            {
                LoadDictionarySelect(slt, "项目阶段", "");
                if (isSelectFirst && (slt.Items.Count > 0))
                {
                    int num = -1;
                    foreach (ListItem item in slt.Items)
                    {
                        num++;
                        if (item.Value.Trim() != "")
                        {
                            slt.SelectedIndex = num;
                            return;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadProjectStatusSelect(HtmlSelect slt, string selectedValue)
        {
            try
            {
                LoadDictionarySelect(slt, "项目阶段", selectedValue);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadProjectUserSelect(HtmlSelect slt, string selectedValue, string projectCode)
        {
            try
            {
                UserStrategyBuilder builder = new UserStrategyBuilder();
                builder.AddStrategy(new Strategy(UserStrategyName.ProjectCode, projectCode));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("SystemUser", queryString);
                agent.Dispose();
                int count = data.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    data.SetCurrentRow(i);
                    slt.Items.Add(new ListItem(data.GetString("UserName"), data.GetString("UserCode")));
                }
                slt.Value = selectedValue;
                data.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadRiskIndexSelect(HtmlSelect slt, string selectedValue)
        {
            try
            {
                EntityData allRiskIndex = ConstructDAO.GetAllRiskIndex();
                if (allRiskIndex.HasRecord())
                {
                    for (int i = 0; i <= (allRiskIndex.CurrentTable.Rows.Count - 1); i++)
                    {
                        allRiskIndex.SetCurrentRow(i);
                        slt.Items.Add(new ListItem(allRiskIndex.GetString("IndexName").ToString(), allRiskIndex.GetString("IndexCode").ToString()));
                    }
                }
                slt.Value = selectedValue;
                allRiskIndex.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadSalBuildingSelect(HtmlSelect slt, string selectedValue, string ProjectCode)
        {
            try
            {
                DataTable table = SalDAO.GetSalBuildingByProjectCode(ProjectCode).Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    slt.Items.Add(new ListItem(row["BuildingName"].ToString(), row["BuildingName"].ToString()));
                }
                try
                {
                    slt.Value = selectedValue;
                }
                catch
                {
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadSalBuildingSelect(DropDownList slt, string selectedValue, string ProjectCode)
        {
            try
            {
                DataTable table = SalDAO.GetSalBuildingByProjectCode(ProjectCode).Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    slt.Items.Add(new ListItem(row["BuildingName"].ToString(), row["BuildingName"].ToString()));
                }
                try
                {
                    slt.SelectedValue = selectedValue;
                }
                catch
                {
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadSalProjectSelect(HtmlSelect slt, string selectedValue)
        {
            try
            {
                EntityData allSalProjectCode = ProjectDAO.GetAllSalProjectCode();
                DataTable currentTable = allSalProjectCode.CurrentTable;
                currentTable.Columns.Add("SalProjectName", typeof(string));
                int count = allSalProjectCode.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    allSalProjectCode.SetCurrentRow(i);
                    string salProjectNameByCode = DtsPayRule.GetSalProjectNameByCode(allSalProjectCode.GetString("SalProjectCode"));
                    allSalProjectCode.CurrentRow["SalProjectName"] = salProjectNameByCode;
                }
                DataView view = new DataView(currentTable, "", "SalProjectName", DataViewRowState.CurrentRows);
                foreach (DataRowView view2 in view)
                {
                    string text3 = ConvertRule.ToString(view2["ProjectCode"]);
                    string text = ConvertRule.ToString(view2["ProjectName"]);
                    text = ConvertRule.ToString(view2["SalProjectName"]) + " -> " + text;
                    slt.Items.Add(new ListItem(text, text3));
                }
                slt.Value = selectedValue;
                allSalProjectCode.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadSalSystemProjectSelect(HtmlSelect slt, string selectedValue)
        {
            try
            {
                SalService service = new SalService();
                DataTable table = service.GetSalProject().Tables[0];
                DataView view = new DataView(table, "", "Proj_Name", DataViewRowState.CurrentRows);
                foreach (DataRowView view2 in view)
                {
                    slt.Items.Add(new ListItem(view2["Proj_Name"].ToString(), view2["Proj_Code"].ToString()));
                }
                try
                {
                    slt.Value = selectedValue;
                }
                catch
                {
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadSubjectSetSelect(HtmlSelect slt, string selectedValue)
        {
            try
            {
                EntityData allSubjectSet = SubjectDAO.GetAllSubjectSet();
                int count = allSubjectSet.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    allSubjectSet.SetCurrentRow(i);
                    slt.Items.Add(new ListItem(allSubjectSet.GetString("SubjectSetName"), allSubjectSet.GetString("SubjectSetCode")));
                }
                slt.Value = selectedValue;
                allSubjectSet.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadSupplierSelect(HtmlSelect slt, string selectedValue)
        {
            try
            {
                EntityData allSupplier = ProjectDAO.GetAllSupplier();
                int count = allSupplier.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    allSupplier.SetCurrentRow(i);
                    slt.Items.Add(new ListItem(allSupplier.GetString("SupplierName"), allSupplier.GetString("SupplierCode")));
                }
                slt.Value = selectedValue;
                allSupplier.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadTempletSelect(HtmlSelect slt, string selectedValue, string type)
        {
            try
            {
                EntityData templetByType = CBSDAO.GetTempletByType(type);
                int count = templetByType.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    templetByType.SetCurrentRow(i);
                    slt.Items.Add(new ListItem(templetByType.GetString("TITLE"), templetByType.GetString("TempletCode")));
                }
                slt.Value = selectedValue;
                templetByType.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadUnitSelect(HtmlSelect sltUnit, string selectedUnitCode)
        {
            try
            {
                EntityData allUnit = OBSDAO.GetAllUnit();
                SetFullName(allUnit.CurrentTable);
                int count = allUnit.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    allUnit.SetCurrentRow(i);
                    sltUnit.Items.Add(new ListItem(allUnit.GetString("FullName"), allUnit.GetString("UnitCode")));
                }
                allUnit.Dispose();
                sltUnit.Value = selectedUnitCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadUnitSelect(HtmlSelect sltUnit, string selectedUnitCode, string projectCode)
        {
            try
            {
                EntityData unitUnderProject = SystemRule.GetUnitUnderProject(projectCode);
                SetFullName(unitUnderProject.CurrentTable);
                int count = unitUnderProject.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    unitUnderProject.SetCurrentRow(i);
                    sltUnit.Items.Add(new ListItem(unitUnderProject.GetString("FullName"), unitUnderProject.GetString("UnitCode")));
                }
                unitUnderProject.Dispose();
                sltUnit.Value = selectedUnitCode;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadVisualProgressSelect(HtmlSelect slt, string selectedValue)
        {
            try
            {
                EntityData allVisualProgress = ConstructDAO.GetAllVisualProgress();
                if (allVisualProgress.HasRecord())
                {
                    for (int i = 0; i <= (allVisualProgress.CurrentTable.Rows.Count - 1); i++)
                    {
                        allVisualProgress.SetCurrentRow(i);
                        slt.Items.Add(new ListItem(allVisualProgress.GetString("VisualProgress").ToString(), allVisualProgress.GetString("SystemId").ToString()));
                    }
                }
                try
                {
                    slt.Value = selectedValue;
                }
                catch
                {
                }
                allVisualProgress.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadVoucherTypeSelect(HtmlSelect slt, string selectedValue)
        {
            try
            {
                EntityData allVoucherType = PaymentDAO.GetAllVoucherType();
                int count = allVoucherType.CurrentTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    allVoucherType.SetCurrentRow(i);
                    slt.Items.Add(new ListItem(allVoucherType.GetString("Name"), allVoucherType.GetString("Code")));
                }
                slt.Value = selectedValue;
                allVoucherType.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadWBSTaskFullNameSelect(HtmlSelect slt, string selectedValue, string ProjectCode)
        {
            try
            {
                DataTable wBSTaskFullNameTable = WBSRule.GetWBSTaskFullNameTable(ProjectCode);
                int count = wBSTaskFullNameTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    DataRow row = wBSTaskFullNameTable.Rows[i];
                    string text = ConvertRule.ToString(row["WBSCode"]);
                    string text2 = ConvertRule.ToString(row["FullName"]);
                    slt.Items.Add(new ListItem(text2, text));
                }
                try
                {
                    slt.Value = selectedValue;
                }
                catch
                {
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadYearSelect(HtmlSelect slt, string selectedValue)
        {
            try
            {
                bool allowNull = false;
                if (selectedValue.Trim() == "")
                {
                    allowNull = true;
                }
                LoadYearSelect(slt, selectedValue, allowNull);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void LoadYearSelect(HtmlSelect slt, string selectedValue, bool AllowNull)
        {
            try
            {
                int num3;
                int num4;
                slt.Items.Clear();
                if (AllowNull)
                {
                    slt.Items.Add(new ListItem("--请选择--", ""));
                }
                int num = 6;
                int year = 0;
                if (selectedValue != "")
                {
                    year = ConvertRule.ToInt(selectedValue.Trim());
                }
                if (year == 0)
                {
                    year = DateTime.Today.Year;
                }
                for (num3 = -num; num3 < 0; num3++)
                {
                    num4 = year + num3;
                    slt.Items.Add(new ListItem(num4.ToString(), num4.ToString()));
                }
                for (num3 = 0; num3 <= num; num3++)
                {
                    num4 = year + num3;
                    slt.Items.Add(new ListItem(num4.ToString(), num4.ToString()));
                }
                slt.Value = selectedValue;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SelectCopy(HtmlSelect sltSrc, HtmlSelect sltDes)
        {
            try
            {
                sltDes.Items.Clear();
                foreach (ListItem item in sltSrc.Items)
                {
                    sltDes.Items.Add(new ListItem(item.Text, item.Value));
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void SetDropDownListSelected(DropDownList ddlDict, string selectedValue)
        {
            foreach (ListItem item in ddlDict.Items)
            {
                if (item.Value == selectedValue)
                {
                    item.Selected = true;
                }
            }
        }

        public static void SetFullName(DataTable sourceTable)
        {
            sourceTable.Columns.Add("FullName");
            int count = sourceTable.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataRow row = sourceTable.Rows[i];
                string text = "";
                if (!row.IsNull("ParentUnitCode"))
                {
                    text = (string) row["ParentUnitCode"];
                }
                row["FullName"] = (string) row["UnitName"];
                if (text.Length != 0)
                {
                    DataRow[] rowArray = sourceTable.Select(string.Format("UnitCode='{0}'", text));
                    if (rowArray.Length > 0)
                    {
                        string text2 = "";
                        if (!rowArray[0].IsNull("FullName"))
                        {
                            text2 = (string) rowArray[0]["FullName"];
                        }
                        if (text2.Length > 0)
                        {
                            row["FullName"] = text2 + "->" + ((string) row["UnitName"]);
                        }
                    }
                }
            }
        }

        public static void SetListGroupSelectedValues(CheckBoxList cbl, string selectedValues)
        {
            foreach (string text in selectedValues.Split(new char[] { ',' }))
            {
                foreach (ListItem item in cbl.Items)
                {
                    if (item.Value == text)
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        public static void SetListGroupSelectedValues(CheckBoxList cbl, string[] selectedValues)
        {
            foreach (string text in selectedValues)
            {
                foreach (ListItem item in cbl.Items)
                {
                    if (item.Value == text)
                    {
                        item.Selected = true;
                    }
                }
            }
        }
    }
}

