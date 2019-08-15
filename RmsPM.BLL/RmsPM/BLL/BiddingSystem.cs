namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Rms.ORMap;
    using RmsPM.DAL.QueryStrategy;

    public class BiddingSystem
    {
        public static DataTable BiddingMessageByStateName(BiddingStateName[] StateNameList)
        {
            int length = StateNameList.Length;
            string[] nameList = new string[length];
            for (int i = 0; i < length; i++)
            {
                nameList[i] = GetStateByName(StateNameList[i]);
            }
            string queryString = new BuliderOrSelect().GetOrSqlString("Bidding", "State", nameList);
            QueryAgent agent = new QueryAgent();
            DataSet set = agent.ExecSqlForDataSet(queryString);
            agent.Dispose();
            return set.Tables[0];
        }

        public static void DelHistoryPlace(string biddingCode)
        {
            try
            {
                string queryString = "update BiddingReturn set State ='' where BiddingEmitCode in (Select BiddingEmitCode from BiddingEmit where BiddingCode=" + biddingCode + ")";
                new QueryAgent().ExecuteSql(queryString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static DataTable Get_All_BiddingSupplier(string BiddingPrejudicationCode)
        {
            DataTable table;
            try
            {
                table = Get_AllMessage(BiddingPrejudicationCode);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return table;
        }

        public static DataTable Get_AllMessage(string BiddingPrejudicationCode)
        {
            string queryString = "select a.DepartmentIdearID,a.BiddingSupplierCode,a.Depart_Build,a.Depart_Project,a.Depart_Agreement,a.Md_Item,a.Md_Project,a.Md_Agreement,a.Director_Project,a.Director_Agreement,a.Director_Finance,a.DepartmentRemark,a.DepartmentRemark1,a.DepartmentRemark2,a.DepartmentRemark3, a.BiddingPrejudicationCode ,b.* from Bidding_SupplierDepartmentIdea a,V_BiddingSupplier b where a.BiddingPrejudicationCode = b.BiddingPrejudicationCode and a.BiddingPrejudicationCode ='" + BiddingPrejudicationCode + "' and a.BiddingSupplierCode = b.BiddingSupplierCode";
            QueryAgent agent = new QueryAgent();
            DataSet set = agent.ExecSqlForDataSet(queryString);
            agent.Dispose();
            return set.Tables[0];
        }

        public static DataTable Get_Bidding_SupplierDepartmentIdea(string BiddingPrejudicationCode)
        {
            QueryAgent agent = new QueryAgent();
            string queryString = "select * from Bidding_SupplierDepartmentIdea where BiddingPrejudicationCode='" + BiddingPrejudicationCode + "'";
            DataSet set = agent.ExecSqlForDataSet(queryString);
            agent.Dispose();
            return set.Tables[0];
        }

        public static string Get_BiddingState(string biddingCode)
        {
            Bidding bidding = new Bidding();
            bidding.BiddingCode = biddingCode;
            try
            {
                return bidding.State;
            }
            catch
            {
                return "0";
            }
        }

        public static DataTable Get_LastBiddingPrejudicationByPass(string BiddingPrejudicationCode)
        {
            string queryString = "select a.DepartmentIdearID,a.BiddingSupplierCode,a.Depart_Build,a.Depart_Project,a.Depart_Agreement,a.Md_Item,a.Md_Project,a.Md_Agreement,a.Director_Project,a.Director_Agreement,a.Director_Finance,a.DepartmentRemark,a.DepartmentRemark1,a.DepartmentRemark2,a.DepartmentRemark3, a.BiddingPrejudicationCode ,b.* from Bidding_SupplierDepartmentIdea a,V_BiddingSupplier b where a.BiddingPrejudicationCode = b.BiddingPrejudicationCode and a.flag='1' and a.BiddingPrejudicationCode ='" + BiddingPrejudicationCode + "' and a.BiddingSupplierCode = b.BiddingSupplierCode";
            QueryAgent agent = new QueryAgent();
            DataSet set = agent.ExecSqlForDataSet(queryString);
            agent.Dispose();
            return set.Tables[0];
        }

        public static DataTable Get_V_BiddingSupplier(string BiddingPrejudicationCode)
        {
            V_BiddingSupplier supplier = new V_BiddingSupplier();
            supplier.BiddingPrejudicationCode = BiddingPrejudicationCode;
            return supplier.GetV_BiddingSuppliers();
        }

        public static DataTable GetAuditingMessage(DataTable dt, string biddingCode, string biddingEmits)
        {
            int count = dt.Rows.Count;
            BiddingEmit emit = new BiddingEmit();
            string state = "";
            for (int i = 0; i < count; i++)
            {
                string text = emit.GetPriceAtAuditing(biddingCode, dt.Rows[i]["SupplierCode"].ToString(), biddingEmits, dt.Rows[i]["BiddingDtlCode"].ToString(), ref state);
                if (text != "-1")
                {
                    dt.Rows[i]["Money"] = text;
                    dt.Rows[i]["State"] = state;
                }
            }
            return dt;
        }

        public static string GetBiddingTypeName(string state)
        {
            switch (state)
            {
                case "0":
                    return BiddingTypeStateName.议标.ToString();

                case "1":
                    return BiddingTypeStateName.招标.ToString();
            }
            return "";
        }

        public static string GetCashMoneyByBiddingReturnCode(string BiddingReturnCode)
        {
            Cash_Message message = new Cash_Message();
            Cash_Detail detail = new Cash_Detail();
            message.CashMessageTypeCode = BiddingReturnCode;
            DataTable table = message.GetCash_Messages();
            DataTable table2 = new DataTable();
            if (table.Rows.Count != 0)
            {
                detail.Cash_MessageCode = table.Rows[table.Rows.Count - 1]["CashMessageCode"].ToString();
                table2 = detail.GetCash_Details();
            }
            string text = "";
            if (table2 != null)
            {
                foreach (DataRow row in table2.Select())
                {
                    text = (text + row["Cash"].ToString() + "&nbsp") + row["MoneyType"].ToString() + "<br>";
                }
            }
            return text;
        }

        public static string GetContractNemberByBiddingCode(string code)
        {
            BiddingPrejudication prejudication = new BiddingPrejudication();
            prejudication.BiddingCode = code;
            DataTable biddingPrejudications = prejudication.GetBiddingPrejudications();
            if (biddingPrejudications != null)
            {
                DataRow[] rowArray = biddingPrejudications.Select();
                int index = 0;
                while (index < rowArray.Length)
                {
                    DataRow row = rowArray[index];
                    return row["Number"].ToString();
                }
            }
            return "";
        }

        public static string GetStateByName(BiddingStateName StateName)
        {
            switch (StateName)
            {
                case BiddingStateName.招标计划:
                    return "0";

                case BiddingStateName.单位预审:
                    return "1";

                case BiddingStateName.发标:
                    return "2";

                case BiddingStateName.回标:
                    return "3";

                case BiddingStateName.项目评审中:
                    return "5";

                case BiddingStateName.压价回价:
                    return "6";

                case BiddingStateName.项目已评审:
                    return "4";

                case BiddingStateName.通知书评审中:
                    return "41";

                case BiddingStateName.通知书已评审:
                    return "42";

                case BiddingStateName.已创建合同:
                    return "43";
            }
            return "-1";
        }

        public static string GetStateMessage(string state)
        {
            switch (state)
            {
                case "0":
                    return BiddingStateName.招标计划.ToString();

                case "1":
                    return BiddingStateName.单位预审.ToString();

                case "2":
                    return BiddingStateName.发标.ToString();

                case "3":
                    return BiddingStateName.回标.ToString();

                case "5":
                    return BiddingStateName.项目评审中.ToString();

                case "6":
                    return BiddingStateName.压价回价.ToString();

                case "7":
                    return BiddingStateName.招标执行.ToString();

                case "4":
                    return BiddingStateName.项目已评审.ToString();

                case "41":
                    return BiddingStateName.通知书评审中.ToString();

                case "42":
                    return BiddingStateName.通知书已评审.ToString();

                case "43":
                    return BiddingStateName.已创建合同.ToString();
            }
            return "未知状态";
        }

        public static void InsertDepartMent(string BiddingPrejudicationCode)
        {
            try
            {
                DataTable table = Get_V_BiddingSupplier(BiddingPrejudicationCode);
                DataTable table2 = Get_Bidding_SupplierDepartmentIdea(BiddingPrejudicationCode);
                DataView view = new DataView(table);
                QueryAgent agent = new QueryAgent();
                StringBuilder builder = new StringBuilder();
                foreach (DataRowView view2 in view)
                {
                    builder.Append("insert Bidding_SupplierDepartmentIdea(BiddingSupplierCode,BiddingPrejudicationCode) values ('" + view2["BiddingSupplierCode"].ToString() + "','" + view2["BiddingPrejudicationCode"].ToString() + "') ");
                }
                agent.ExecuteSql(builder.ToString());
                table2 = Get_Bidding_SupplierDepartmentIdea(BiddingPrejudicationCode);
                agent.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void InsertDepartMent(string BiddingPrejudicationCode, string BiddingSupplierCode)
        {
            try
            {
                QueryAgent agent = new QueryAgent();
                string queryString = "insert Bidding_SupplierDepartmentIdea(BiddingSupplierCode,BiddingPrejudicationCode) values ('" + BiddingSupplierCode + "','" + BiddingPrejudicationCode + "')";
                agent.ExecuteSql(queryString);
                agent.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static bool IsUseForeignMoney(string BiddingEmitCode)
        {
            BiddingReturn return2 = new BiddingReturn();
            return2.BiddingEmitCode = BiddingEmitCode;
            DataTable biddingReturns = return2.GetBiddingReturns();
            foreach (DataRow row in biddingReturns.Select())
            {
                Cash_Message message = new Cash_Message();
                message.CashMessageTypeCode = row["BiddingReturnCode"].ToString();
                DataTable table2 = message.GetCash_Messages();
                if (table2.Rows.Count != 0)
                {
                    Cash_Detail detail = new Cash_Detail();
                    detail.Cash_MessageCode = table2.Rows[table2.Rows.Count - 1]["CashMessageCode"].ToString();
                    DataTable table3 = detail.GetCash_Details();
                    foreach (DataRow row2 in table3.Select())
                    {
                        if (Convert.ToDecimal(row2["ExchangeRate"]) != 1M)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static void Set_BiddingState(string state, string BiddingCode)
        {
            Bidding bidding = new Bidding();
            bidding.BiddingCode = BiddingCode;
            bidding.State = state;
            bidding.BiddingSubmit();
        }

        public static void Set_BiddingState(string state, string BiddingCode, StandardEntityDAO dao)
        {
            Bidding bidding = new Bidding();
            bidding.BiddingCode = BiddingCode;
            bidding.dao = dao;
            bidding.State = state;
            bidding.BiddingSubmit();
        }

        public static void Set_BiddingTypeDictionary(HtmlSelect selBiddingType)
        {
            try
            {
                selBiddingType.Items.Clear();
                ListItem item = new ListItem("--选择--", "");
                ListItem item2 = new ListItem("议标", "0");
                ListItem item3 = new ListItem("招标", "1");
                selBiddingType.Items.Add(item);
                selBiddingType.Items.Add(item2);
                selBiddingType.Items.Add(item3);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpDataPrejudicationCode(string newBiddingPrejudicationCode, string olBiddingPrejudicationCode)
        {
            try
            {
                string queryString = "update Bidding_SupplierDepartmentIdea set BiddingPrejudicationCode='" + newBiddingPrejudicationCode + "' where BiddingPrejudicationCode='" + olBiddingPrejudicationCode + "'";
                QueryAgent agent = new QueryAgent();
                agent.ExecuteSql(queryString);
                agent.Dispose();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void UpDatePrejudicationSelect(string key, string flag, string BiddingSupplierCode)
        {
            try
            {
                string queryString = "update Bidding_SupplierDepartmentIdea set " + key + " = '" + flag + "' where BiddingSupplierCode='" + BiddingSupplierCode + "'";
                new QueryAgent().ExecuteSql(queryString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public enum BiddingStateName
        {
            招标计划,
            单位预审,
            发标,
            回标,
            项目评审中,
            压价回价,
            项目已评审,
            通知书评审中,
            通知书已评审,
            已创建合同,
            招标执行
        }

        public enum BiddingTypeStateName
        {
            议标,
            招标
        }

        public enum DepartMentName
        {
            建筑部,
            工程部,
            合约部,
            项目总监,
            工程总监,
            合约总监,
            财务总监,
            工程执董,
            合约执董,
            财务执董,
            销售执董,
            显示所有情况
        }
    }
}

