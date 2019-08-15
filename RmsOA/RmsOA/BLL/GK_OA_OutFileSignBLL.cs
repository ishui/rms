namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class GK_OA_OutFileSignBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            GK_OA_OutFileSignDAL ndal = new GK_OA_OutFileSignDAL(Transaction);
            return ndal.Delete(Code);
        }

        public GK_OA_OutFileSignModel GetModel(int Code, SqlConnection Connection)
        {
            GK_OA_OutFileSignDAL ndal = new GK_OA_OutFileSignDAL(Connection);
            return ndal.GetModel(Code);
        }

        public GK_OA_OutFileSignModel GetModel(int Code, SqlTransaction Transaction)
        {
            GK_OA_OutFileSignDAL ndal = new GK_OA_OutFileSignDAL(Transaction);
            return ndal.GetModel(Code);
        }

        public List<GK_OA_OutFileSignModel> GetModels(SqlConnection Connection)
        {
            GK_OA_OutFileSignDAL ndal = new GK_OA_OutFileSignDAL(Connection);
            return ndal.Select();
        }

        public List<GK_OA_OutFileSignModel> GetModels(SqlTransaction Transaction)
        {
            GK_OA_OutFileSignDAL ndal = new GK_OA_OutFileSignDAL(Transaction);
            return ndal.Select();
        }

        public List<GK_OA_OutFileSignModel> GetModels(GK_OA_OutFileSignQueryModel ObjQueryModel, SqlConnection Connection)
        {
            GK_OA_OutFileSignDAL ndal = new GK_OA_OutFileSignDAL(Connection);
            return ndal.Select(ObjQueryModel);
        }

        public List<GK_OA_OutFileSignModel> GetModels(GK_OA_OutFileSignQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            GK_OA_OutFileSignDAL ndal = new GK_OA_OutFileSignDAL(Transaction);
            return ndal.Select(ObjQueryModel);
        }

        public int Insert(GK_OA_OutFileSignModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_OutFileSignDAL ndal = new GK_OA_OutFileSignDAL(Transaction);
            return ndal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_OutFileSignModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyBankOutAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_OutFileSignModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_OutFileSignModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "0";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_OutFileSignModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "3";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            GK_OA_OutFileSignModel objModel = this.GetModel(Code, Transaction);
            objModel.Status = "2";
            return this.Update(objModel, Transaction);
        }

        public int Update(GK_OA_OutFileSignModel ObjModel, SqlTransaction Transaction)
        {
            GK_OA_OutFileSignDAL ndal = new GK_OA_OutFileSignDAL(Transaction);
            return ndal.Update(ObjModel);
        }
    }
}

