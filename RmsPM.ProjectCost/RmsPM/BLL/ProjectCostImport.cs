namespace RmsPM.BLL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Text;
    using RmsPM.DAL;
    using RmsPM.DAL.EntityDAO;
    using TiannuoPM.MODEL;

    public class ProjectCostImport : ImportRule.ImportBase
    {
        public SqlConnection conn;
        private string InputPerson;
        private DataTable m_tbSystemGroup = null;

        public string Import(Stream stream, string a_InputPerson)
        {
            string text4;
            try
            {
                string text = "";
                this.InputPerson = a_InputPerson;
                int num = 0;
                int num2 = 0;
                SqlDataProcess process = new SqlDataProcess(this.conn);
                this.m_tbSystemGroup = process.GetDataSet("select dbo.GetSystemGroupFullName(GroupCode) as FullName, * from SystemGroup where ClassCode = '1521'").Tables[0];
                StreamReader reader = new StreamReader(stream, Encoding.Default);
                int num3 = 0;
                if (reader.Peek() >= 0)
                {
                    reader.ReadLine();
                    num3++;
                }
                while (reader.Peek() >= 0)
                {
                    string val = reader.ReadLine();
                    num3++;
                    string text3 = this.ImportSingle(val);
                    if (text3 == "")
                    {
                        num++;
                    }
                    else
                    {
                        num2++;
                        text = text + string.Format("第 {0} 行导入出错：", num3) + text3 + "\n";
                    }
                }
                if (num2 > 0)
                {
                    text = string.Format("导入完成，{0}条成功， {1}条出错：", num, num2) + "\n" + text;
                }
                else
                {
                    text = string.Format("{0}条导入成功", num);
                }
                text4 = text;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return text4;
        }

        private string ImportSingle(string val)
        {
            try
            {
                string[] arr = ImportRule.SplitCsvLine(val);
                if (arr.Length < base.MinColCount)
                {
                    return string.Format("列不足{0}个", base.MinColCount);
                }
                string text = arr[0].Trim();
                if (text == "")
                {
                    return "项目名称为空";
                }
                string text2 = arr[1].Trim();
                string text3 = "";
                if (text2 != "")
                {
                    DataRow[] rowArray = this.m_tbSystemGroup.Select("FullName = '" + text2 + "'");
                    if (rowArray.Length > 0)
                    {
                        text3 = rowArray[0]["GroupCode"].ToString();
                    }
                }
                if (text3 == "")
                {
                    return string.Format("未知的费用项“{0}”", text2);
                }
                ProjectCostDAL tdal = new ProjectCostDAL(this.conn);
                ProjectCostModel mObj = new ProjectCostModel();
                mObj.ProjectCostCode = int.Parse(SystemManageDAO.GetNewSysCode("ProjectCostCode"));
                mObj.ProjectName = text;
                mObj.GroupCode = text3;
                mObj.Unit = ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 3)).Trim();
                mObj.InputPerson = this.InputPerson;
                try
                {
                    if (ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 2)).Trim() == "")
                    {
                        mObj.Area = 0M;
                    }
                    else
                    {
                        mObj.Area = decimal.Parse(ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 2)).Trim());
                    }
                }
                catch
                {
                    return string.Format("面积“{0}”不是有效的数值", ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 2)).Trim());
                }
                try
                {
                    if (ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 3)).Trim() == "")
                    {
                        mObj.Price = 0M;
                    }
                    else
                    {
                        mObj.Price = decimal.Parse(ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 3)).Trim());
                    }
                }
                catch
                {
                    return string.Format("单方造价“{0}”不是有效的数值", ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 3)).Trim());
                }
                try
                {
                    if (ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 4)).Trim() == "")
                    {
                        mObj.Money = 0M;
                    }
                    else
                    {
                        mObj.Money = decimal.Parse(ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 4)).Trim());
                    }
                }
                catch
                {
                    return string.Format("总价“{0}”不是有效的数值", ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 4)).Trim());
                }
                try
                {
                    if (ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 5)).Trim() == "")
                    {
                        mObj.Qty = 0M;
                    }
                    else
                    {
                        mObj.Qty = decimal.Parse(ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 5)).Trim());
                    }
                }
                catch
                {
                    return string.Format("数量“{0}”不是有效的数值", ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 5)).Trim());
                }
                if (((mObj.Money == 0M) && (mObj.Price != 0M)) && (mObj.Area != 0M))
                {
                    mObj.Money = Math.Round((decimal) (mObj.Price * mObj.Area), 2);
                }
                if (((mObj.Price == 0M) && (mObj.Money != 0M)) && (mObj.Area != 0M))
                {
                    mObj.Price = Math.Round((decimal) (mObj.Money / mObj.Area), 2);
                }
                mObj.Remark = ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 5)).Trim();
                tdal.Insert(mObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return "";
        }

        protected override void InitDefine()
        {
            base.MinColCount = 2;
            base.tbDefine.Rows.Add(new object[] { "ProjectName", "项目名称", true });
            base.tbDefine.Rows.Add(new object[] { "GroupCode", "费用项全名", true });
            base.tbDefine.Rows.Add(new object[] { "Area", "面积" });
            base.tbDefine.Rows.Add(new object[] { "Price", "单方造价" });
            base.tbDefine.Rows.Add(new object[] { "Money", "总价" });
            base.tbDefine.Rows.Add(new object[] { "Qty", "数量" });
            base.tbDefine.Rows.Add(new object[] { "Unit", "单位" });
            base.tbDefine.Rows.Add(new object[] { "Remark", "说明" });
        }
    }
}

