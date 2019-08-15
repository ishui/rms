namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TC_OA_LingYongMenonySumBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonySumDAL mdal = new TC_OA_LingYongMenonySumDAL(Transaction);
            return mdal.Delete(Code);
        }

        public TC_OA_LingYongMenonySumModel GetModel(int Code, SqlConnection Connection)
        {
            TC_OA_LingYongMenonySumDAL mdal = new TC_OA_LingYongMenonySumDAL(Connection);
            return mdal.GetModel(Code);
        }

        public TC_OA_LingYongMenonySumModel GetModel(int Code, SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonySumDAL mdal = new TC_OA_LingYongMenonySumDAL(Transaction);
            return mdal.GetModel(Code);
        }

        public List<TC_OA_LingYongMenonySumModel> GetModels(SqlConnection Connection)
        {
            TC_OA_LingYongMenonySumDAL mdal = new TC_OA_LingYongMenonySumDAL(Connection);
            return mdal.Select();
        }

        public List<TC_OA_LingYongMenonySumModel> GetModels(SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonySumDAL mdal = new TC_OA_LingYongMenonySumDAL(Transaction);
            return mdal.Select();
        }

        public List<TC_OA_LingYongMenonySumModel> GetModels(TC_OA_LingYongMenonySumQueryModel ObjQueryModel, SqlConnection Connection)
        {
            TC_OA_LingYongMenonySumDAL mdal = new TC_OA_LingYongMenonySumDAL(Connection);
            return mdal.Select(ObjQueryModel);
        }

        public List<TC_OA_LingYongMenonySumModel> GetModels(TC_OA_LingYongMenonySumQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonySumDAL mdal = new TC_OA_LingYongMenonySumDAL(Transaction);
            return mdal.Select(ObjQueryModel);
        }

        public int Insert(TC_OA_LingYongMenonySumModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonySumDAL mdal = new TC_OA_LingYongMenonySumDAL(Transaction);
            return mdal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonySumModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 2;
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonySumModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 1;
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonySumModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 4;
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonySumModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 3;
            return this.Update(objModel, Transaction);
        }

        public int Update(TC_OA_LingYongMenonySumModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_LingYongMenonySumDAL mdal = new TC_OA_LingYongMenonySumDAL(Transaction);
            return mdal.Update(ObjModel);
        }
    }
}

