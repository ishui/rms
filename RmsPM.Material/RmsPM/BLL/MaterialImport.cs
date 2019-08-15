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
  
    public class MaterialImport : ImportRule.ImportBase
    {

    
        public SqlConnection conn;
        private string InputPerson;
        private DataTable m_tbSystemGroup;

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
                this.m_tbSystemGroup = process.GetDataSet("select dbo.GetSystemGroupFullName(GroupCode) as FullName, * from SystemGroup where ClassCode = '1501'").Tables[0];
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
                string text = arr[1].Trim();
                if (text == "")
                {
                    return "材料名称为空";
                }
                string text2 = arr[0].Trim();
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
                    return string.Format("未知的类型“{0}”", text2);
                }
                MaterialDAL ldal = new MaterialDAL(this.conn);
                MaterialModel mObj = new MaterialModel();
                mObj.MaterialCode = int.Parse(SystemManageDAO.GetNewSysCode("MaterialCode"));
                mObj.MaterialName = text;
                mObj.GroupCode = text3;
                mObj.Spec = ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 2)).Trim();
                mObj.Unit = ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 3)).Trim();
                mObj.InputPerson = this.InputPerson;
                try
                {
                    if (ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 4)).Trim() == "")
                    {
                        mObj.StandardPrice = 0M;
                    }
                    else
                    {
                        mObj.StandardPrice = decimal.Parse(ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 4)).Trim());
                    }
                }
                catch
                {
                    return string.Format("参考价“{0}”不是有效的数值", ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 4)).Trim());
                }
                mObj.Remark = ConvertRule.ToString(ConvertRule.GetArrayItem(arr, 5)).Trim();
                ldal.Insert(mObj);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return "";
        }

        protected override void InitDefine()
        {
            base.MinColCount = 3;
            base.tbDefine.Rows.Add(new object[] { "GroupCode", "类型全名", true });
            base.tbDefine.Rows.Add(new object[] { "MaterialName", "材料名称", true });
            base.tbDefine.Rows.Add(new object[] { "Spec", "规格" });
            base.tbDefine.Rows.Add(new object[] { "Unit", "单位" });
            base.tbDefine.Rows.Add(new object[] { "StandardPrice", "参考价" });
            base.tbDefine.Rows.Add(new object[] { "Remark", "备注" });
        }
    }
}

