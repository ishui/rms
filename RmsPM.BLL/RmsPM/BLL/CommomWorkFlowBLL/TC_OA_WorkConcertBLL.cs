namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TC_OA_WorkConcertBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            TC_OA_WorkConcertDAL tdal = new TC_OA_WorkConcertDAL(Transaction);
            return tdal.Delete(Code);
        }

        public TC_OA_WorkConcertModel GetModel(int Code, SqlConnection Connection)
        {
            TC_OA_WorkConcertDAL tdal = new TC_OA_WorkConcertDAL(Connection);
            return tdal.GetModel(Code);
        }

        public TC_OA_WorkConcertModel GetModel(int Code, SqlTransaction Transaction)
        {
            TC_OA_WorkConcertDAL tdal = new TC_OA_WorkConcertDAL(Transaction);
            return tdal.GetModel(Code);
        }

        public List<TC_OA_WorkConcertModel> GetModels(SqlConnection Connection)
        {
            TC_OA_WorkConcertDAL tdal = new TC_OA_WorkConcertDAL(Connection);
            return tdal.Select();
        }

        public List<TC_OA_WorkConcertModel> GetModels(SqlTransaction Transaction)
        {
            TC_OA_WorkConcertDAL tdal = new TC_OA_WorkConcertDAL(Transaction);
            return tdal.Select();
        }

        public List<TC_OA_WorkConcertModel> GetModels(TC_OA_WorkConcertQueryModel ObjQueryModel, SqlConnection Connection)
        {
            TC_OA_WorkConcertDAL tdal = new TC_OA_WorkConcertDAL(Connection);
            return tdal.Select(ObjQueryModel);
        }

        public List<TC_OA_WorkConcertModel> GetModels(TC_OA_WorkConcertQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            TC_OA_WorkConcertDAL tdal = new TC_OA_WorkConcertDAL(Transaction);
            return tdal.Select(ObjQueryModel);
        }

        public int Insert(TC_OA_WorkConcertModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_WorkConcertDAL tdal = new TC_OA_WorkConcertDAL(Transaction);
            return tdal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_WorkConcertModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 2;
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_WorkConcertModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 1;
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_WorkConcertModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 4;
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_WorkConcertModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 3;
            return this.Update(objModel, Transaction);
        }

        public int Update(TC_OA_WorkConcertModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_WorkConcertDAL tdal = new TC_OA_WorkConcertDAL(Transaction);
            return tdal.Update(ObjModel);
        }
    }
}

