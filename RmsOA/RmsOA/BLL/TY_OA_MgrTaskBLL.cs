namespace RmsOA.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using RmsOA.DAL;
    using RmsOA.MODEL;

    public class TY_OA_MgrTaskBLL
    {
        public int Delete(int Code, SqlTransaction Transaction)
        {
            TY_OA_MgrTaskDAL kdal = new TY_OA_MgrTaskDAL(Transaction);
            TY_OA_MgrTaskDtlDAL ldal = new TY_OA_MgrTaskDtlDAL(Transaction);
            TY_OA_MgrTaskDtlQueryModel qmObj = new TY_OA_MgrTaskDtlQueryModel();
            qmObj.MgrCodeIDEqual = Code;
            foreach (TY_OA_MgrTaskDtlModel model2 in ldal.Select(qmObj))
            {
                ldal.Delete(model2.MgrDtlCode);
            }
            return kdal.Delete(Code);
        }

        public TY_OA_MgrTaskModel GetModel(int Code, SqlConnection Connection)
        {
            TY_OA_MgrTaskDAL kdal = new TY_OA_MgrTaskDAL(Connection);
            return kdal.GetModel(Code);
        }

        public TY_OA_MgrTaskModel GetModel(int Code, SqlTransaction Transaction)
        {
            TY_OA_MgrTaskDAL kdal = new TY_OA_MgrTaskDAL(Transaction);
            return kdal.GetModel(Code);
        }

        public List<TY_OA_MgrTaskModel> GetModels(SqlConnection Connection)
        {
            TY_OA_MgrTaskDAL kdal = new TY_OA_MgrTaskDAL(Connection);
            return kdal.Select();
        }

        public List<TY_OA_MgrTaskModel> GetModels(SqlTransaction Transaction)
        {
            TY_OA_MgrTaskDAL kdal = new TY_OA_MgrTaskDAL(Transaction);
            return kdal.Select();
        }

        public List<TY_OA_MgrTaskModel> GetModels(TY_OA_MgrTaskQueryModel ObjQueryModel, SqlConnection Connection)
        {
            TY_OA_MgrTaskDAL kdal = new TY_OA_MgrTaskDAL(Connection);
            return kdal.Select(ObjQueryModel);
        }

        public List<TY_OA_MgrTaskModel> GetModels(TY_OA_MgrTaskQueryModel ObjQueryModel, SqlTransaction Transaction)
        {
            TY_OA_MgrTaskDAL kdal = new TY_OA_MgrTaskDAL(Transaction);
            return kdal.Select(ObjQueryModel);
        }

        public static string GetStatusName(string status)
        {
            switch (status)
            {
                case "1":
                    return "待审";

                case "2":
                    return "评审中";

                case "3":
                    return "已审";

                case "4":
                    return "未通过";
            }
            return "未知状态";
        }

        public int Insert(TY_OA_MgrTaskModel ObjModel, SqlTransaction Transaction)
        {
            TY_OA_MgrTaskDAL kdal = new TY_OA_MgrTaskDAL(Transaction);
            return kdal.Insert(ObjModel);
        }

        public int ModifyAlreadyAuditing(int Code, SqlTransaction Transaction)
        {
            TY_OA_MgrTaskModel objModel = this.GetModel(Code, Transaction);
            objModel.State = "2";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotAuditing(int Code, SqlTransaction Transaction)
        {
            TY_OA_MgrTaskModel objModel = this.GetModel(Code, Transaction);
            objModel.State = "1";
            return this.Update(objModel, Transaction);
        }

        public int ModifyNotPassAuditing(int Code, SqlTransaction Transaction)
        {
            TY_OA_MgrTaskModel objModel = this.GetModel(Code, Transaction);
            objModel.State = "4";
            return this.Update(objModel, Transaction);
        }

        public int ModifyPassAuditing(int Code, SqlTransaction Transaction)
        {
            TY_OA_MgrTaskModel objModel = this.GetModel(Code, Transaction);
            objModel.State = "3";
            return this.Update(objModel, Transaction);
        }

        public int Update(TY_OA_MgrTaskModel ObjModel, SqlTransaction Transaction)
        {
            TY_OA_MgrTaskDAL kdal = new TY_OA_MgrTaskDAL(Transaction);
            return kdal.Update(ObjModel);
        }
    }
}

