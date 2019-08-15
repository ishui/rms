namespace RmsPM.DAL.QueryStrategy
{
    using System;
    using System.Collections;

    public sealed class SystemClassDescription
    {
        public static Hashtable m_Items = new Hashtable();

        public static ArrayList GetItemByClassCode(string classCode)
        {
            ArrayList list = new ArrayList();
            foreach (object obj2 in m_Items.Values)
            {
                if (((SystemClassItem) obj2).ClassCode == classCode)
                {
                    list.Add(obj2);
                }
            }
            return list;
        }

        public static string GetItemClassCode(string className)
        {
            if (!m_Items.Contains(className))
            {
                return "";
            }
            return ((SystemClassItem) m_Items[className]).ClassCode;
        }

        public static string GetItemCreateUserColumnName(string className)
        {
            if (!m_Items.Contains(className))
            {
                return "";
            }
            return ((SystemClassItem) m_Items[className]).CreateUserColumnName;
        }

        public static string GetItemInfoByClassCode(string classCode)
        {
            string text = "";
            ArrayList itemByClassCode = GetItemByClassCode(classCode);
            foreach (SystemClassItem item in itemByClassCode)
            {
                if (text.Length > 0)
                {
                    text = text + ",";
                }
                text = item.TableName + "|" + item.TypeColumnName;
            }
            return text;
        }

        public static string GetItemKeyColumnName(string className)
        {
            if (!m_Items.Contains(className))
            {
                return "";
            }
            return ((SystemClassItem) m_Items[className]).KeyColumnName;
        }

        public static string GetItemTableName(string className)
        {
            if (!m_Items.Contains(className))
            {
                return "";
            }
            return ((SystemClassItem) m_Items[className]).TableName;
        }

        public static string GetItemTypeColumnName(string className)
        {
            if (!m_Items.Contains(className))
            {
                return "";
            }
            return ((SystemClassItem) m_Items[className]).TypeColumnName;
        }

        public static SystemClassItem GetSystemClassItem(string className)
        {
            if (!m_Items.Contains(className))
            {
                return null;
            }
            return (SystemClassItem) m_Items[className];
        }

        public static void LoadItem()
        {
            m_Items.Add("Contract", new SystemClassItem("0501", "Contract", "ContractCode", "Type", "CreatePerson"));
            m_Items.Add("Document", new SystemClassItem("1001", "Document", "DocumentCode", "GroupCode", "CreatePerson"));
            m_Items.Add("Payment", new SystemClassItem("0601", "Payment", "PaymentCode", "GroupCode", "ApplyPerson"));
            m_Items.Add("Payout", new SystemClassItem("0602", "Payout", "PayoutCode", "GroupCode", "InputPerson"));
            m_Items.Add("Supplier", new SystemClassItem("1401", "Supplier", "SupplierCode", "SupplierTypeCode", "CreatePerson"));
            m_Items.Add("MaterialCost", new SystemClassItem("1411", "MaterialCost", "MaterialCostCode", "GroupCode", "CreatePerson"));
            m_Items.Add("SupplierMaterial", new SystemClassItem("1413", "SupplierMaterial", "SupplierMaterialCode", "GroupCode", "CreatePerson"));
            m_Items.Add("Material", new SystemClassItem("1501", "Material", "MaterialCode", "GroupCode", "InputPerson"));
            m_Items.Add("MaterialIn", new SystemClassItem("1503", "MaterialIn", "MaterialInCode", "GroupCode", "InputPerson"));
            m_Items.Add("MaterialOut", new SystemClassItem("1505", "MaterialOut", "MaterialOutCode", "GroupCode", "InputPerson"));
            m_Items.Add("ProjectCost", new SystemClassItem("1521", "ProjectCost", "ProjectCostCode", "GroupCode", "InputPerson"));
            m_Items.Add("Enquiry", new SystemClassItem("1301", "Enquiry", "PurchaseCode", "ClassType", "EnquiryPerson"));
            m_Items.Add("Purchase", new SystemClassItem("1301", "Purchase", "PurchaseCode", "ClassType", "CreatePerson"));
            m_Items.Add("CostBudgetSet", new SystemClassItem("0411", "CostBudgetSet", "CostBudgetSetCode", "GroupCode", "ModifyPerson"));
            m_Items.Add("CostBudget", new SystemClassItem("0411", "CostBudget", "CostBudgetSetCode", "GroupCode", "ModifyPerson"));
            m_Items.Add("CostBudgetBackupSet", new SystemClassItem("0411", "CostBudgetBackupSet", "CostBudgetSetCode", "GroupCode", "ModifyPerson"));
            m_Items.Add("WorkFlowCommon", new SystemClassItem("0902", "WorkFlowCommon", "WorkFlowCommonCode", "Type", "Transactor"));
            m_Items.Add("WorkFlowProcedure", new SystemClassItem("0902", "WorkFlowProcedure", "ProcedureCode", "SysType", "Creator"));
        }
    }
}

