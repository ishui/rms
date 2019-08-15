namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TC_OA_UnusualBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            TC_OA_UnusualDAL ldal = new TC_OA_UnusualDAL(Transaction);
            return ldal.Delete(Code);
        }

        public TC_OA_UnusualModel GetModel(int Code, SqlConnection Connection)
        {
            TC_OA_UnusualDAL ldal = new TC_OA_UnusualDAL(Connection);
            return ldal.GetModel(Code);
        }

        public TC_OA_UnusualModel GetModel(int Code, SqlTransaction Transaction)
        {
            TC_OA_UnusualDAL ldal = new TC_OA_UnusualDAL(Transaction);
            return ldal.GetModel(Code);
        }

        public List<TC_OA_UnusualModel> GetModels(SqlConnection Connection)
        {
            TC_OA_UnusualDAL ldal = new TC_OA_UnusualDAL(Connection);
            return ldal.Select();
        }

        public List<TC_OA_UnusualModel> GetModels(SqlTransaction Transaction)
        {
            TC_OA_UnusualDAL ldal = new TC_OA_UnusualDAL(Transaction);
            return ldal.Select();
        }

        public List<TC_OA_UnusualModel> GetModels(TC_OA_UnusualQueryModel ObjQueryModel, SqlConnection Connection)
        {
            TC_OA_UnusualDAL ldal = new TC_OA_UnusualDAL(Connection);
            return ldal.Select(ObjQueryModel);
        }

        public List<TC_OA_UnusualModel> GetModels(TC_OA_UnusualQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            TC_OA_UnusualDAL ldal = new TC_OA_UnusualDAL(Transaction);
            return ldal.Select(ObjQueryModel);
        }

        public int Insert(TC_OA_UnusualModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_UnusualDAL ldal = new TC_OA_UnusualDAL(Transaction);
            return ldal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_UnusualModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 2;
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_UnusualModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 1;
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_UnusualModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 4;
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_UnusualModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 3;
            return this.Update(objModel, Transaction);
        }

        public int Update(TC_OA_UnusualModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_UnusualDAL ldal = new TC_OA_UnusualDAL(Transaction);
            return ldal.Update(ObjModel);
        }
    }
}

