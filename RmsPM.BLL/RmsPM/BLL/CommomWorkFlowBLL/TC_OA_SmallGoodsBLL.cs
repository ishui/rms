namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsPM.DAL.CommonWorkFlowDAL;

    public class TC_OA_SmallGoodsBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            TC_OA_SmallGoodsDAL sdal = new TC_OA_SmallGoodsDAL(Transaction);
            return sdal.Delete(Code);
        }

        public TC_OA_SmallGoodsModel GetModel(int Code, SqlConnection Connection)
        {
            TC_OA_SmallGoodsDAL sdal = new TC_OA_SmallGoodsDAL(Connection);
            return sdal.GetModel(Code);
        }

        public TC_OA_SmallGoodsModel GetModel(int Code, SqlTransaction Transaction)
        {
            TC_OA_SmallGoodsDAL sdal = new TC_OA_SmallGoodsDAL(Transaction);
            return sdal.GetModel(Code);
        }

        public List<TC_OA_SmallGoodsModel> GetModels(SqlConnection Connection)
        {
            TC_OA_SmallGoodsDAL sdal = new TC_OA_SmallGoodsDAL(Connection);
            return sdal.Select();
        }

        public List<TC_OA_SmallGoodsModel> GetModels(SqlTransaction Transaction)
        {
            TC_OA_SmallGoodsDAL sdal = new TC_OA_SmallGoodsDAL(Transaction);
            return sdal.Select();
        }

        public List<TC_OA_SmallGoodsModel> GetModels(TC_OA_SmallGoodsQueryModel ObjQueryModel, SqlConnection Connection)
        {
            TC_OA_SmallGoodsDAL sdal = new TC_OA_SmallGoodsDAL(Connection);
            return sdal.Select(ObjQueryModel);
        }

        public List<TC_OA_SmallGoodsModel> GetModels(TC_OA_SmallGoodsQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            TC_OA_SmallGoodsDAL sdal = new TC_OA_SmallGoodsDAL(Transaction);
            return sdal.Select(ObjQueryModel);
        }

        public int Insert(TC_OA_SmallGoodsModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_SmallGoodsDAL sdal = new TC_OA_SmallGoodsDAL(Transaction);
            return sdal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_SmallGoodsModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 2;
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_SmallGoodsModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 1;
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_SmallGoodsModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 4;
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            TC_OA_SmallGoodsModel objModel = this.GetModel(Code, Transaction);
            objModel.Auditing = 3;
            return this.Update(objModel, Transaction);
        }

        public int Update(TC_OA_SmallGoodsModel ObjModel, SqlTransaction Transaction)
        {
            TC_OA_SmallGoodsDAL sdal = new TC_OA_SmallGoodsDAL(Transaction);
            return sdal.Update(ObjModel);
        }
    }
}

