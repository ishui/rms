namespace RmsOA.BFL
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using RmsOA.DAL;
    using RmsOA.MODEL;
    using RmsPM.BLL;

    public class RS_ScoreExtend
    {
        private const char spChar1 = ';';
        private const char spChar2 = ',';
        private const char spChar3 = '|';

        public List<UnitModel> GetAllUnit()
        {
            RS_ScoreExpandDAL ddal = new RS_ScoreExpandDAL();
            List<UnitModel> list = new List<UnitModel>();
            List<UnitModel> allUnit = new List<UnitModel>();
            allUnit = ddal.GetAllUnit();
            foreach (UnitModel model in allUnit)
            {
                model.UnitName = this.GetDeptNameByDeptCode(model.UnitCode);
                list.Add(model);
            }
            return list;
        }

        public string GetDeptNameByDeptCode(string deptCode)
        {
            return SystemRule.GetUnitName(deptCode);
        }

        public string GetMarkerLeaderDeptCode(string userCode)
        {
            RS_ScoreExpandDAL ddal = new RS_ScoreExpandDAL();
            return ddal.GetLeaderDept(userCode);
        }

        public static List<DeptSupplyModel> GetModel()
        {
            List<DeptSupplyModel> list = new List<DeptSupplyModel>();
            DeptSupplyModel item = new DeptSupplyModel();
            string text = ConfigurationManager.AppSettings["ScoreDepts"];
            if (string.IsNullOrEmpty(text))
            {
                throw new Exception("违法操作，请在配置文件里面配置好扩展部门");
            }
            string[] textArray = text.Split(new char[] { '|' });
            foreach (string text2 in textArray)
            {
                string[] textArray2 = text2.Split(new char[] { ';', ',' });
                item = new DeptSupplyModel();
                item.DeptName = textArray2[0];
                item.DeptCode = textArray2[1];
                List<string> list2 = new List<string>();
                list2.Add(textArray2[2]);
                list2.Add(textArray2[3]);
                list.Add(item);
            }
            return list;
        }

        public virtual List<EmployViewModel> GetScoreByFKCode()
        {
            return null;
        }

        public List<UnitModel> GetUnitByUserCode(string userCode)
        {
            RS_ScoreExpandDAL ddal = new RS_ScoreExpandDAL();
            List<UnitModel> list = new List<UnitModel>();
            List<UnitModel> unitByUserCode = new List<UnitModel>();
            unitByUserCode = ddal.GetUnitByUserCode(userCode);
            foreach (UnitModel model in unitByUserCode)
            {
                model.UnitName = this.GetDeptNameByDeptCode(model.UnitCode);
                list.Add(model);
            }
            return list;
        }

        public string GetUserNameByCode(string code)
        {
            RS_ScoreExpandDAL ddal = new RS_ScoreExpandDAL();
            return ddal.GetUserNameByCode(code);
        }

        public string GetUserNameByUserCode(string userCode)
        {
            return SystemRule.GetUserName(userCode);
        }

        public List<UserModel> GetUsersByCode(string userCode)
        {
            RS_ScoreExpandDAL ddal = new RS_ScoreExpandDAL();
            List<UserModel> list = new List<UserModel>();
            return ddal.GetUsersByCode(userCode);
        }

        public List<UserModel> GetUsersByCode(string userCode, string deptCode)
        {
            RS_ScoreExpandDAL ddal = new RS_ScoreExpandDAL();
            List<UserModel> list = new List<UserModel>();
            return ddal.GetUsersByCode(userCode, deptCode);
        }

        public static DateTime CheckMonth
        {
            get
            {
                CheckTime time = new ThisMonthCheck();
                return time.CheckMonth;
            }
        }

        public static bool HasExtends
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["EnableScoreExtend"]))
                {
                    return false;
                }
                return bool.Parse(ConfigurationManager.AppSettings["EnableScoreExtend"]);
            }
        }
    }
}

