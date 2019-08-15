namespace RmsPM.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using RmsPM.DAL;
    using TiannuoPM.MODEL;

    public class LocaleViseBLL
    {
        public int Balance(int ViseCode, SqlTransaction Transaction)
        {
            LocaleViseModel viseModel = new LocaleViseModel();
            viseModel.ViseCode = ViseCode;
            viseModel.ViseBalanceStatus = 2;
            return this.Update(viseModel, Transaction);
        }

        public int BatchBalance(string ViseCodes, SqlTransaction Transaction)
        {
            int num = 0;
            char[] separator = ",".ToCharArray();
            string[] textArray = ViseCodes.Split(separator);
            foreach (string text in textArray)
            {
                num += this.Balance(int.Parse(text), Transaction);
            }
            return num;
        }

        public int BatchNoBalance(string ViseCodes, SqlTransaction Transaction)
        {
            int num = 0;
            char[] separator = ",".ToCharArray();
            string[] textArray = ViseCodes.Split(separator);
            foreach (string text in textArray)
            {
                num += this.noBalance(int.Parse(text), Transaction);
            }
            return num;
        }

        public int BatchWaitBalance(string ViseCodes, SqlTransaction Transaction)
        {
            int num = 0;
            char[] separator = ",".ToCharArray();
            string[] textArray = ViseCodes.Split(separator);
            foreach (string text in textArray)
            {
                num += this.waitBalance(int.Parse(text), Transaction);
            }
            return num;
        }

        public int Delete(int Code, SqlTransaction Transaction)
        {
            LocaleViseDAL edal = new LocaleViseDAL(Transaction);
            LocaleViseCostDAL tdal = new LocaleViseCostDAL(Transaction);
            LocaleViseCostQueryModel qmObj = new LocaleViseCostQueryModel();
            qmObj.ViseCode = Code;
            foreach (LocaleViseCostModel model2 in tdal.Select(qmObj))
            {
                tdal.Delete(model2.ViseCostCode);
            }
            return edal.Delete(Code);
        }

        public int Delete(LocaleViseModel ViseModel, SqlTransaction Transaction)
        {
            return this.Delete(ViseModel.ViseCode, Transaction);
        }

        public int DeleteCost(int Code, SqlTransaction Transaction)
        {
            LocaleViseCostDAL tdal = new LocaleViseCostDAL(Transaction);
            return tdal.Delete(Code);
        }

        public int DeleteCost(LocaleViseCostModel ViseCostModel, SqlTransaction Transaction)
        {
            LocaleViseCostDAL tdal = new LocaleViseCostDAL(Transaction);
            return tdal.Delete(ViseCostModel);
        }

        public LocaleViseModel GetModel(int Code, SqlConnection Connection)
        {
            LocaleViseDAL edal = new LocaleViseDAL(Connection);
            return edal.GetModel(Code);
        }

        public LocaleViseModel GetModel(int Code, SqlTransaction Transaction)
        {
            LocaleViseDAL edal = new LocaleViseDAL(Transaction);
            return edal.GetModel(Code);
        }

        public decimal GetSumCheckMoneyByContract(string ContractCode, SqlConnection Connection)
        {
            HandMadeDAL edal = new HandMadeDAL(Connection);
            return edal.GetSumCheckMoneyByContract(ContractCode);
        }

        public DataSet GetVisesMoneyByCostGroup(string ViseCodes, SqlConnection Connection)
        {
            HandMadeDAL edal = new HandMadeDAL(Connection);
            return edal.GetVisesMoneySqlStringByCostGroup(ViseCodes);
        }

        public DataSet GetVisesMoneyByCostGroup(string ViseCodes, SqlTransaction Transaction)
        {
            HandMadeDAL edal = new HandMadeDAL(Transaction);
            return edal.GetVisesMoneySqlStringByCostGroup(ViseCodes);
        }

        public decimal GetViseSumCheckMoney(int ViseCode, SqlConnection Connection)
        {
            decimal num = 0M;
            LocaleViseCostQueryModel viseCostQueryModel = new LocaleViseCostQueryModel();
            viseCostQueryModel.ViseCode = ViseCode;
            List<LocaleViseCostModel> list = this.SelectCost(viseCostQueryModel, Connection);
            foreach (LocaleViseCostModel model2 in list)
            {
                num += model2.CheckMoney;
            }
            return num;
        }

        public decimal GetViseSumCheckMoney(int ViseCode, SqlTransaction Transaction)
        {
            decimal num = 0M;
            LocaleViseCostQueryModel viseCostQueryModel = new LocaleViseCostQueryModel();
            viseCostQueryModel.ViseCode = ViseCode;
            List<LocaleViseCostModel> list = this.SelectCost(viseCostQueryModel, Transaction);
            foreach (LocaleViseCostModel model2 in list)
            {
                num += model2.CheckMoney;
            }
            return num;
        }

        public decimal GetViseSumMoney(int ViseCode, SqlConnection Connection)
        {
            decimal num = 0M;
            LocaleViseCostQueryModel viseCostQueryModel = new LocaleViseCostQueryModel();
            viseCostQueryModel.ViseCode = ViseCode;
            List<LocaleViseCostModel> list = this.SelectCost(viseCostQueryModel, Connection);
            foreach (LocaleViseCostModel model2 in list)
            {
                num += model2.Money;
            }
            return num;
        }

        public decimal GetViseSumMoney(int ViseCode, SqlTransaction Transaction)
        {
            decimal num = 0M;
            LocaleViseCostQueryModel viseCostQueryModel = new LocaleViseCostQueryModel();
            viseCostQueryModel.ViseCode = ViseCode;
            List<LocaleViseCostModel> list = this.SelectCost(viseCostQueryModel, Transaction);
            foreach (LocaleViseCostModel model2 in list)
            {
                num += model2.Money;
            }
            return num;
        }

        public int Insert(LocaleViseModel ViseModel, SqlTransaction Transaction)
        {
            LocaleViseDAL edal = new LocaleViseDAL(Transaction);
            ViseModel.ViseBalanceStatus = 1;
            ViseModel.ViseStatus = 1;
            ViseModel.ViseComeToMoney = 0M;
            return edal.Insert(ViseModel);
        }

        public int InsertCost(LocaleViseCostModel ViseCostModel, SqlTransaction Transaction)
        {
            LocaleViseCostDAL tdal = new LocaleViseCostDAL(Transaction);
            ViseCostModel.Flag = 0;
            ViseCostModel.OtherMoney = 0M;
            ViseCostModel.OtherMoneyRate = 0M;
            ViseCostModel.OtherMoneyType = "RMB";
            ViseCostModel.State = 0;
            ViseCostModel.CheckMoney = 0M;
            return tdal.Insert(ViseCostModel);
        }

        public int noBalance(int ViseCode, SqlTransaction Transaction)
        {
            LocaleViseModel viseModel = this.GetModel(ViseCode, Transaction);
            viseModel.ViseBalanceStatus = 1;
            return this.Update(viseModel, Transaction);
        }

        public int NoPassAudit(int ViseCode, SqlTransaction Transaction)
        {
            LocaleViseModel viseModel = this.GetModel(ViseCode, Transaction);
            viseModel.ViseStatus = 4;
            return this.Update(viseModel, Transaction);
        }

        public int PassAudit(int ViseCode, SqlTransaction Transaction)
        {
            LocaleViseModel viseModel = this.GetModel(ViseCode, Transaction);
            viseModel.ViseStatus = 3;
            return this.Update(viseModel, Transaction);
        }

        public int ReturnWait(int ViseCode, SqlTransaction Transaction)
        {
            LocaleViseModel viseModel = this.GetModel(ViseCode, Transaction);
            viseModel.ViseStatus = 1;
            return this.Update(viseModel, Transaction);
        }

        public List<LocaleViseModel> Select(SqlTransaction Transaction)
        {
            LocaleViseDAL edal = new LocaleViseDAL(Transaction);
            return edal.Select();
        }

        public List<LocaleViseModel> Select(LocaleViseQueryModel ViseQueryModel, SqlConnection Connection)
        {
            LocaleViseDAL edal = new LocaleViseDAL(Connection);
            return edal.Select(ViseQueryModel);
        }

        public List<LocaleViseModel> Select(LocaleViseQueryModel ViseQueryModel, SqlTransaction Transaction)
        {
            LocaleViseDAL edal = new LocaleViseDAL(Transaction);
            return edal.Select(ViseQueryModel);
        }

        public List<LocaleViseCostModel> SelectCost(LocaleViseCostQueryModel ViseCostQueryModel, SqlConnection Connection)
        {
            LocaleViseCostDAL tdal = new LocaleViseCostDAL(Connection);
            return tdal.Select(ViseCostQueryModel);
        }

        public List<LocaleViseCostModel> SelectCost(LocaleViseCostQueryModel ViseCostQueryModel, SqlTransaction Transaction)
        {
            LocaleViseCostDAL tdal = new LocaleViseCostDAL(Transaction);
            return tdal.Select(ViseCostQueryModel);
        }

        public int StartAudit(int ViseCode, SqlTransaction Transaction)
        {
            LocaleViseModel viseModel = this.GetModel(ViseCode, Transaction);
            viseModel.ViseStatus = 2;
            return this.Update(viseModel, Transaction);
        }

        public int Update(LocaleViseModel ViseModel, SqlTransaction Transaction)
        {
            LocaleViseDAL edal = new LocaleViseDAL(Transaction);
            return edal.Update(ViseModel);
        }

        public int UpdateCost(LocaleViseCostModel ViseCostModel, SqlTransaction Transaction)
        {
            LocaleViseCostDAL tdal = new LocaleViseCostDAL(Transaction);
            return tdal.Update(ViseCostModel);
        }

        public int waitBalance(int ViseCode, SqlTransaction Transaction)
        {
            LocaleViseModel viseModel = this.GetModel(ViseCode, Transaction);
            viseModel.ViseBalanceStatus = 3;
            return this.Update(viseModel, Transaction);
        }
    }
}

