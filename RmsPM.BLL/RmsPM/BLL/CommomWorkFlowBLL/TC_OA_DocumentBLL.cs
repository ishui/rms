namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TC_OA_DocumentBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            TC_OA_DocumentDAL tdal = new TC_OA_DocumentDAL(Transaction);
            return tdal.Delete(Code);
        }

        public TC_OA_DocumentModel GetModel(int Code, SqlConnection Connection)
        {
            TC_OA_DocumentDAL tdal = new TC_OA_DocumentDAL(Connection);
            return tdal.GetModel(Code);
        }

        public TC_OA_DocumentModel GetModel(int Code, SqlTransaction Transaction)
        {
            TC_OA_DocumentDAL tdal = new TC_OA_DocumentDAL(Transaction);
            return tdal.GetModel(Code);
        }

        public List<TC_OA_DocumentModel> GetModels(SqlConnection Connection)
        {
            TC_OA_DocumentDAL tdal = new TC_OA_DocumentDAL(Connection);
            return tdal.Select();
        }

        public List<TC_OA_DocumentModel> GetModels(SqlTransaction Transaction)
        {
            TC_OA_DocumentDAL tdal = new TC_OA_DocumentDAL(Transaction);
            return tdal.Select();
        }

        public List<TC_OA_DocumentModel> GetModels(TC_OA_DocumentQueryModel ObjQueryModel, SqlConnection Connection)
        {
            TC_OA_DocumentDAL tdal = new TC_OA_DocumentDAL(Connection);
            return tdal.Select(ObjQueryModel);
        }

        public List<TC_OA_DocumentModel> GetModels(TC_OA_DocumentQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            TC_OA_DocumentDAL tdal = new TC_OA_DocumentDAL(Transaction);
            return tdal.Select(ObjQueryModel);
        }

        public int Insert(TC_OA_DocumentModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_DocumentDAL tdal = new TC_OA_DocumentDAL(Transaction);
            return tdal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_DocumentModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 2;
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_DocumentModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 1;
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_DocumentModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 4;
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_DocumentModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 3;
            return this.Update(objModel, Transaction);
        }

        public int Update(TC_OA_DocumentModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_DocumentDAL tdal = new TC_OA_DocumentDAL(Transaction);
            return tdal.Update(ObjModel);
        }
    }
}

