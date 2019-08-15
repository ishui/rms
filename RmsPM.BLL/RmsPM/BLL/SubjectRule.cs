namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;

    public sealed class SubjectRule
    {
        public static string CheckSubject(string SubjectCode, string SubjectSetCode, string HintDesc)
        {
            string text3;
            try
            {
                string subjectName = "";
                text3 = CheckSubject(SubjectCode, SubjectSetCode, HintDesc, ref subjectName);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string CheckSubject(string SubjectCode, string SubjectSetCode, string HintDesc, ref string SubjectName)
        {
            string text2;
            try
            {
                string text = "";
                SubjectName = "";
                if (SubjectCode == "")
                {
                    return text;
                }
                EntityData subjectByCode = SubjectDAO.GetSubjectByCode(SubjectCode, SubjectSetCode);
                if (subjectByCode.HasRecord())
                {
                    SubjectName = subjectByCode.GetString("SubjectName");
                    if (subjectByCode.GetInt("ChildNodesCount") > 0)
                    {
                        text = string.Format("{0}不是末级科目 ！", HintDesc);
                    }
                }
                else if (HintDesc == "")
                {
                    text = "科目不存在 ！";
                }
                else
                {
                    text = string.Format("{0}不存在 ！", HintDesc);
                }
                subjectByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string CheckSubject(string SubjectCode, string SubjectSetCode, string HintDesc, ref string SubjectName, ref string SubjectFullName)
        {
            string text2;
            try
            {
                SubjectFullName = "";
                string text = CheckSubject(SubjectCode, SubjectSetCode, HintDesc, ref SubjectName);
                SubjectFullName = GetSubjectFullName(SubjectCode, SubjectSetCode);
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static bool CheckSubjectNode(string subjectCode, string subjectSetCode)
        {
            bool flag2;
            try
            {
                bool flag = false;
                if (subjectCode != "")
                {
                    EntityData subjectByCode = SubjectDAO.GetSubjectByCode(subjectCode, subjectSetCode);
                    if (subjectByCode.HasRecord() && (subjectByCode.GetInt("ChildNodesCount") == 0))
                    {
                        flag = true;
                    }
                    subjectByCode.Dispose();
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static void DeleteAllSubjectBySet(string subjectSetCode)
        {
            try
            {
                QueryAgent agent = new QueryAgent();
                try
                {
                    agent.ExecuteSql(string.Format("delete Subject where SubjectSetCode = '{0}'", subjectSetCode));
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

        public static void DeleteSubject(string subjectCode, string subjectSetCode)
        {
            try
            {
                EntityData entity = SubjectDAO.GetSubjectByCode(subjectCode, subjectSetCode);
                SubjectDAO.DeleteSubject(entity);
                entity.Dispose();
                QueryAgent agent = new QueryAgent();
                try
                {
                    agent.ExecuteSql(string.Format("delete Subject where SubjectSetCode = '{0}' and SubjectCode like '{1}%'", subjectSetCode, subjectCode));
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

        public static string GetFinanceInterfaceName(string code)
        {
            string text2;
            try
            {
                string text = "";
                DataRow[] rowArray = GetFinanceInterfaceTable().Select("FinanceInterfaceCode = '" + code + "'");
                if (rowArray.Length > 0)
                {
                    text = ConvertRule.ToString(rowArray[0]["FinanceInterfaceName"]);
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static DataTable GetFinanceInterfaceTable()
        {
            DataTable table2;
            try
            {
                DataTable table = new DataTable("FinanceInterface");
                table.Columns.Add("FinanceInterfaceCode");
                table.Columns.Add("FinanceInterfaceName");
                table.Rows.Add(new object[] { "UFSoft", "用友V8及以后版本" });
                table.Rows.Add(new object[] { "UFSoft_V7", "用友V8以前版本" });
                table.Rows.Add(new object[] { "Sun", "Sun" });
                table2 = table;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table2;
        }

        public static string GetNextSubjectSetCode()
        {
            string text2;
            try
            {
                string text = "";
                QueryAgent agent = new QueryAgent();
                try
                {
                    text = (ConvertRule.ToInt(agent.ExecuteScalar("select max(cast(SubjectSetCode as int)) from SubjectSet where IsNumeric(SubjectSetCode) = 1")) + 1).ToString();
                }
                finally
                {
                    agent.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetParentSubjectCode(string subjectCode, string ruleCode)
        {
            string text3;
            try
            {
                string[] textArray = ruleCode.Split(".".ToCharArray());
                string text = "";
                string text2 = subjectCode;
                for (int i = 0; i < textArray.Length; i++)
                {
                    int length = ConvertRule.ToInt(textArray[i]);
                    if (text2.Length > length)
                    {
                        text = text + text2.Substring(0, length);
                        text2 = text2.Substring(length, text2.Length - length);
                    }
                    else
                    {
                        break;
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

        public static string GetProjectSubjectSet(string projectCode, ref int isSelfAccount)
        {
            string text3;
            try
            {
                string code = SystemRule.GetProjectUnitCode(projectCode);
                string text2 = "";
                EntityData unitByCode = OBSDAO.GetUnitByCode(code);
                if (unitByCode.HasRecord())
                {
                    text2 = unitByCode.GetString("SubjectSetCode");
                    isSelfAccount = unitByCode.GetInt("SelfAccount");
                }
                unitByCode.Dispose();
                text3 = text2;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetSelfAccountUnitName(string subjectSetCode)
        {
            string text3;
            try
            {
                string text = "";
                UnitStrategyBuilder builder = new UnitStrategyBuilder();
                builder.AddStrategy(new Strategy(UnitStrategyName.SubjectSetCode, subjectSetCode));
                builder.AddStrategy(new Strategy(UnitStrategyName.SelfAccount, "1"));
                string queryString = builder.BuildMainQueryString();
                QueryAgent agent = new QueryAgent();
                EntityData data = agent.FillEntityData("Unit", queryString);
                agent.Dispose();
                if (data.HasRecord())
                {
                    text = data.GetString("UnitName");
                }
                text3 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text3;
        }

        public static string GetSubjectFullName(string subjectCode, string subjectSetCode)
        {
            string text2;
            try
            {
                string text = "";
                QueryAgent agent = new QueryAgent();
                try
                {
                    text = agent.ExecuteScalar(string.Format("select dbo.GetSubjectFullName('{0}', '{1}')", subjectCode, subjectSetCode)).ToString();
                }
                finally
                {
                    agent.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSubjectName(string subjectCode, string subjectSetCode)
        {
            string text2;
            try
            {
                string text = "";
                if (subjectCode == "")
                {
                    return text;
                }
                EntityData subjectByCode = SubjectDAO.GetSubjectByCode(subjectCode, subjectSetCode);
                if (subjectByCode.HasRecord())
                {
                    text = subjectByCode.GetString("SubjectName");
                }
                subjectByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSubjectParentCode(string subjectCode, string subjectSetCode)
        {
            string text2;
            try
            {
                string text = "";
                QueryAgent agent = new QueryAgent();
                try
                {
                    text = agent.ExecuteScalar(string.Format("select dbo.GetSubjectParent('{0}', '{1}')", subjectCode, subjectSetCode)).ToString();
                }
                finally
                {
                    agent.Dispose();
                }
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSubjectSetFinanceInterface(string subjectSetCode)
        {
            string text2;
            try
            {
                string text = "";
                if (subjectSetCode == "")
                {
                    return text;
                }
                EntityData subjectSetByCode = SubjectDAO.GetSubjectSetByCode(subjectSetCode);
                if (subjectSetByCode.HasRecord())
                {
                    text = subjectSetByCode.GetString("FinanceInterface");
                }
                subjectSetByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSubjectSetName(string subjectSetCode)
        {
            string text2;
            try
            {
                string text = "";
                if (subjectSetCode == "")
                {
                    return text;
                }
                EntityData subjectSetByCode = SubjectDAO.GetSubjectSetByCode(subjectSetCode);
                if (subjectSetByCode.HasRecord())
                {
                    text = subjectSetByCode.GetString("SubjectSetName");
                }
                subjectSetByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetSubjectSetRuleCode(string subjectsetCode)
        {
            string text2;
            try
            {
                string text = "";
                EntityData subjectSetByCode = SubjectDAO.GetSubjectSetByCode(subjectsetCode);
                if (subjectSetByCode.HasRecord())
                {
                    text = subjectSetByCode.GetString("SubjectRule");
                }
                subjectSetByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static string GetUnitSubjectSet(string unitCode, ref int isSelfAccount)
        {
            string text2;
            try
            {
                string text = "";
                EntityData unitByCode = OBSDAO.GetUnitByCode(unitCode);
                if (unitByCode.HasRecord())
                {
                    text = unitByCode.GetString("SubjectSetCode");
                    isSelfAccount = unitByCode.GetInt("SelfAccount");
                }
                unitByCode.Dispose();
                text2 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text2;
        }

        public static bool IsFitRule(string subjectCode, string ruleCode)
        {
            bool flag2;
            try
            {
                string[] textArray = ruleCode.Split(".".ToCharArray());
                bool flag = false;
                string text = subjectCode;
                for (int i = 0; i < textArray.Length; i++)
                {
                    if (text.Length >= int.Parse(textArray[i]))
                    {
                        flag = true;
                        text = text.Substring(int.Parse(textArray[i]) - 1, text.Length - int.Parse(textArray[i]));
                        if (text == "")
                        {
                            break;
                        }
                    }
                    else
                    {
                        flag = false;
                        break;
                    }
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag2;
        }

        public static bool IsLeafSubjectNode(string subjectCode, string subjectSetCode)
        {
            bool flag2;
            try
            {
                if (subjectCode == "")
                {
                    throw new ApplicationException("输入空科目编号。");
                }
                EntityData subjectByCode = SubjectDAO.GetSubjectByCode(subjectCode, subjectSetCode);
                if (!subjectByCode.HasRecord())
                {
                    throw new ApplicationException("没有这个科目编号。");
                }
                int @int = subjectByCode.GetInt("ChildNodesCount");
                subjectByCode.Dispose();
                bool flag = false;
                if (@int == 0)
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
    }
}

