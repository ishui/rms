namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TC_OA_LingYongMenonyBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonyDAL ydal = new TC_OA_LingYongMenonyDAL(Transaction);
            return ydal.Delete(Code);
        }

        public TC_OA_LingYongMenonyModel GetModel(int Code, SqlConnection Connection)
        {
            TC_OA_LingYongMenonyDAL ydal = new TC_OA_LingYongMenonyDAL(Connection);
            return ydal.GetModel(Code);
        }

        public TC_OA_LingYongMenonyModel GetModel(int Code, SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonyDAL ydal = new TC_OA_LingYongMenonyDAL(Transaction);
            return ydal.GetModel(Code);
        }

        public List<TC_OA_LingYongMenonyModel> GetModels(SqlConnection Connection)
        {
            TC_OA_LingYongMenonyDAL ydal = new TC_OA_LingYongMenonyDAL(Connection);
            return ydal.Select();
        }

        public List<TC_OA_LingYongMenonyModel> GetModels(SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonyDAL ydal = new TC_OA_LingYongMenonyDAL(Transaction);
            return ydal.Select();
        }

        public List<TC_OA_LingYongMenonyModel> GetModels(TC_OA_LingYongMenonyQueryModel ObjQueryModel, SqlConnection Connection)
        {
            TC_OA_LingYongMenonyDAL ydal = new TC_OA_LingYongMenonyDAL(Connection);
            return ydal.Select(ObjQueryModel);
        }

        public List<TC_OA_LingYongMenonyModel> GetModels(TC_OA_LingYongMenonyQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonyDAL ydal = new TC_OA_LingYongMenonyDAL(Transaction);
            return ydal.Select(ObjQueryModel);
        }

        public int Insert(TC_OA_LingYongMenonyModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonyDAL ydal = new TC_OA_LingYongMenonyDAL(Transaction);
            return ydal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonyModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 2;
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonyModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 1;
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonyModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 9;
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonyModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 3;
            return this.Update(objModel, Transaction);
        }

        public int Update(TC_OA_LingYongMenonyModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonyDAL ydal = new TC_OA_LingYongMenonyDAL(Transaction);
            return ydal.Update(ObjModel);
        }
    }
}

