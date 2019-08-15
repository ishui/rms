namespace RmsPM.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Rms.ORMap;
    using RmsPM.DAL.EntityDAO;

    public class IncomeBugetFacade
    {
        public void Delete(IncomeBugetModel ibModel)
        {
            string format = "DELETE FROM RptFinIn WHERE SYSTEMID='{0}'";
            QueryAgent agent = new QueryAgent();
            agent.ExecuteSql(string.Format(format, ibModel.ID));
            agent.Dispose();
        }

        public void Insert(IncomeBugetModel ibModel)
        {
            if (string.IsNullOrEmpty(ibModel.ID))
            {
                if (!string.IsNullOrEmpty(ibModel.Money.ToString()))
                {
                    string format = "INSERT INTO RptFinIn(SYSTEMID,VERID,IYEAR,IMONTH,MONEY,PROJECTCODE) VALUES('{0}',1,{1},{2},{3},'{4}')";
                    string newSysCode = SystemManageDAO.GetNewSysCode("RptFinIn");
                    QueryAgent agent = new QueryAgent();
                    agent.ExecuteSql(string.Format(format, new object[] { newSysCode, ibModel.Year, ibModel.Month, ibModel.Money * 10000M, ibModel.ProjectCode }));
                    agent.Dispose();
                }
            }
            else
            {
                this.UpDate(ibModel);
            }
        }

        public List<IncomeBugetModel> Select(int year, string projectCode)
        {
            string format = "SELECT * FROM RPTFININ WHERE IYEAR={0} AND PROJECTCODE='{1}' ORDER BY IMONTH ASC ";
            QueryAgent agent = new QueryAgent();
            EntityData data = agent.FillEntityData("RptFinIn", string.Format(format, year, projectCode));
            agent.Dispose();
            int num = 1;
            List<IncomeBugetModel> list = new List<IncomeBugetModel>();
            IncomeBugetModel item = null;
            if ((data == null) || (data.CurrentTable.Rows.Count == 0))
            {
                return null;
            }
            foreach (DataRow row in data.CurrentTable.Rows)
            {
                int num2 = int.Parse(row["IMonth"].ToString());
                if (num2 == num)
                {
                    item = new IncomeBugetModel();
                    item.ID = row["SystemID"].ToString();
                    item.Year = year;
                    item.ProjectCode = projectCode;
                    item.Month = num;
                    item.Money = decimal.Parse(row["Money"].ToString()) / 10000M;
                    list.Add(item);
                    num++;
                }
                else
                {
                    while (num < num2)
                    {
                        item = new IncomeBugetModel();
                        item.Year = year;
                        item.Month = num;
                        item.ProjectCode = projectCode;
                        list.Add(item);
                        num++;
                    }
                    if (num2 == num)
                    {
                        item = new IncomeBugetModel();
                        item.ID = row["SystemID"].ToString();
                        item.ProjectCode = projectCode;
                        item.Year = year;
                        item.Month = num;
                        item.Money = decimal.Parse(row["Money"].ToString()) / 10000M;
                        list.Add(item);
                        num++;
                    }
                }
            }
            int count = list.Count;
            if (count < 12)
            {
                for (int i = count + 1; i <= 12; i++)
                {
                    item = new IncomeBugetModel();
                    item.Month = i;
                    item.Year = year;
                    item.ProjectCode = projectCode;
                    list.Add(item);
                }
            }
            return list;
        }

        public int? Select(int year, string projectCode, string temp)
        {
            string format = "SELECT * FROM RPTFININ WHERE IYEAR={0} AND PROJECTCODE='{1}' ORDER BY IMONTH ASC ";
            QueryAgent agent = new QueryAgent();
            EntityData data = agent.FillEntityData("RptFinIn", string.Format(format, year, projectCode));
            agent.Dispose();
            if (data == null)
            {
                return null;
            }
            return new int?(data.CurrentTable.Rows.Count);
        }

        public List<IncomeBugetModel> SetForInsert(int year, string projectCode)
        {
            List<IncomeBugetModel> list = new List<IncomeBugetModel>();
            IncomeBugetModel item = null;
            for (int i = 1; i <= 12; i++)
            {
                item = new IncomeBugetModel();
                item.ProjectCode = projectCode;
                item.Year = year;
                item.Month = i;
                list.Add(item);
            }
            return list;
        }

        public void UpDate(IncomeBugetModel ibModel)
        {
            string format = "UPDATE RptFinIn SET MONEY={0} WHERE SYSTEMID='{1}'";
            QueryAgent agent = new QueryAgent();
            agent.ExecuteSql(string.Format(format, ibModel.Money * 10000M, ibModel.ID));
            agent.Dispose();
        }
    }
}

