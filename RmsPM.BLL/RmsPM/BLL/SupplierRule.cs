namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class SupplierRule
    {
        public static void DeleteAllSupplier()
        {
            try
            {
                QueryAgent agent = new QueryAgent();
                try
                {
                    string queryString = "select top 1 a.ContractName, b.SupplierName from Contract a, Supplier b where a.SupplierCode = b.SupplierCode";
                    DataSet set = agent.ExecSqlForDataSet(queryString);
                    try
                    {
                        if (set.Tables[0].Rows.Count > 0)
                        {
                            throw new Exception(string.Format("供应商“{0}”已生成合同，不能删除", ConvertRule.ToString(set.Tables[0].Rows[0]["SupplierName"])));
                        }
                    }
                    finally
                    {
                        set.Dispose();
                    }
                    agent.ExecuteSql("delete SupplierSubjectSet");
                    agent.ExecuteSql("delete Supplier");
                }
                finally
                {
                    agent.Dispose();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static string GetIsAuditted(int typeid)
        {
            switch (typeid)
            {
                case 0:
                    return "未审";

                case 1:
                    return "已审";

                case 2:
                    return "审核中";
            }
            return "未知状态";
        }

        public static string GetSupplierAbbreviation(string code)
        {
            string text2;
            try
            {
                string text = "";
                if (code == "")
                {
                    return text;
                }
                EntityData data = new EntityData("Supplier");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Supplier"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                if (data.HasRecord())
                {
                    text = data.GetString("Abbreviation");
                }
                data.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static EntityData GetSupplierByCode(string code)
        {
            EntityData data2;
            try
            {
                EntityData data = new EntityData("Supplier");
                if (code == "")
                {
                    return data;
                }
                using (SingleEntityDAO ydao = new SingleEntityDAO("Supplier"))
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

        public static string GetSupplierName(string code)
        {
            string text2;
            try
            {
                string text = "";
                if (code == "")
                {
                    return text;
                }
                EntityData data = new EntityData("Supplier");
                using (SingleEntityDAO ydao = new SingleEntityDAO("Supplier"))
                {
                    data = ydao.SelectbyPrimaryKey(code);
                }
                if (data.HasRecord())
                {
                    text = data.GetString("SupplierName");
                }
                data.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetTypeName(string typeid)
        {
            switch (typeid)
            {
                case "1":
                    return "有";

                case "0":
                    return "无";
            }
            return "未选择";
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
    }
}

