namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class ContractBillBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            ContractBillDAL ldal = new ContractBillDAL(Transaction);
            return ldal.Delete(Code);
        }

        public ContractBillModel GetModel(int Code, SqlConnection Connection)
        {
            ContractBillDAL ldal = new ContractBillDAL(Connection);
            return ldal.GetModel(Code);
        }

        public ContractBillModel GetModel(int Code, SqlTransaction Transaction)
        {
            ContractBillDAL ldal = new ContractBillDAL(Transaction);
            return ldal.GetModel(Code);
        }

        public List<ContractBillModel> GetModels(SqlConnection Connection)
        {
            ContractBillDAL ldal = new ContractBillDAL(Connection);
            return ldal.Select();
        }

        public List<ContractBillModel> GetModels(SqlTransaction Transaction)
        {
            ContractBillDAL ldal = new ContractBillDAL(Transaction);
            return ldal.Select();
        }

        public List<ContractBillModel> GetModels(ContractBillQueryModel ObjQueryModel, SqlConnection Connection)
        {
            ContractBillDAL ldal = new ContractBillDAL(Connection);
            return ldal.Select(ObjQueryModel);
        }

        public List<ContractBillModel> GetModels(ContractBillQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            ContractBillDAL ldal = new ContractBillDAL(Transaction);
            return ldal.Select(ObjQueryModel);
        }

        public int Insert(ContractBillModel ObjModel, SqlTransaction Transaction)
        {
            ContractBillDAL ldal = new ContractBillDAL(Transaction);
            return ldal.Insert(ObjModel);
        }

        public int Update(ContractBillModel ObjModel, SqlTransaction Transaction)
        {
            ContractBillDAL ldal = new ContractBillDAL(Transaction);
            return ldal.Update(ObjModel);
        }
    }
}

